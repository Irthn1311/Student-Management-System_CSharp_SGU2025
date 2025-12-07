using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.GUI.Services
{
    /// <summary>
    /// Web server đơn giản để hiển thị thông tin học sinh qua QR code
    /// Hỗ trợ cả local và public internet (qua Ngrok)
    /// </summary>
    public class StudentWebServer
    {
        private HttpListener listener;
        private bool isRunning = false;
        private Thread serverThread;
        private int port;
        private HocSinhBLL hocSinhBLL;
        private PhuHuynhBLL phuHuynhBLL;
        private HocSinhPhuHuynhBLL hocSinhPhuHuynhBLL;
        private LopHocBUS lopHocBUS;
        private PhanLopBLL phanLopBLL;
        private HocKyBUS hocKyBUS;

        public bool IsRunning => isRunning;

        public StudentWebServer()
        {
            hocSinhBLL = new HocSinhBLL();
            phuHuynhBLL = new PhuHuynhBLL();
            hocSinhPhuHuynhBLL = new HocSinhPhuHuynhBLL();
            lopHocBUS = new LopHocBUS();
            phanLopBLL = new PhanLopBLL();
            hocKyBUS = new HocKyBUS();

            // Đọc port từ App.config
            port = int.Parse(ConfigurationManager.AppSettings["WebServerPort"] ?? "8080");
        }

        /// <summary>
        /// Lấy Base URL từ UrlResolver
        /// </summary>
        public string GetBaseUrl()
        {
            try
            {
                return UrlResolver.ResolveBaseUrl();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error resolving base URL: {ex.Message}");
                return $"http://localhost:{port}";
            }
        }

        /// <summary>
        /// Khởi động web server
        /// </summary>
        public void Start()
        {
            if (isRunning)
                return;

            try
            {
                listener = new HttpListener();
                
                // Bind với + để chấp nhận mọi hostname (localhost, IP address, domain name)
                // Điều này cho phép truy cập từ cả localhost và LAN IP
                listener.Prefixes.Add($"http://+:{port}/");
                
                // Nếu bind với + thất bại (có thể cần quyền admin), thử localhost
                try
                {
                    listener.Start();
                }
                catch (HttpListenerException ex)
                {
                    // Nếu lỗi do quyền, thử chỉ bind localhost
                    if (ex.ErrorCode == 5) // Access denied
                    {
                        System.Diagnostics.Debug.WriteLine("Cannot bind to all interfaces, falling back to localhost only");
                        listener = new HttpListener();
                        listener.Prefixes.Add($"http://localhost:{port}/");
                        listener.Start();
                    }
                    else
                    {
                        throw;
                    }
                }
                
                isRunning = true;

                serverThread = new Thread(Listen);
                serverThread.IsBackground = true;
                serverThread.Start();

                // Thử mở firewall rule (nếu có quyền)
                TryAddFirewallRule();

                // Thử khởi động Ngrok nếu được cấu hình (chỉ khi mode = Ngrok hoặc Auto)
                TryStartNgrokIfNeeded();

                string baseUrl = GetBaseUrl();
                System.Diagnostics.Debug.WriteLine($"Web server started on port {port}, Base URL: {baseUrl}");
                
                // Log hướng dẫn nếu cần
                LogFirewallInstructions();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error starting web server: {ex.Message}");
                isRunning = false;
            }
        }

        /// <summary>
        /// Dừng web server
        /// </summary>
        public void Stop()
        {
            if (!isRunning)
                return;

            isRunning = false;
            try
            {
                listener?.Stop();
                listener?.Close();
            }
            catch { }

            // Dừng Ngrok nếu đang chạy (chỉ khi mode = Ngrok)
            StopNgrokIfNeeded();
        }

        /// <summary>
        /// Lắng nghe các request HTTP
        /// </summary>
        private void Listen()
        {
            while (isRunning)
            {
                try
                {
                    HttpListenerContext context = listener.GetContext();
                    Task.Run(() => HandleRequest(context));
                }
                catch (Exception ex)
                {
                    if (isRunning)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error in listener: {ex.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// Xử lý HTTP request
        /// </summary>
        private void HandleRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            try
            {
                string path = request.Url.AbsolutePath;
                string method = request.HttpMethod;

                // CORS headers
                response.AddHeader("Access-Control-Allow-Origin", "*");
                response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                response.AddHeader("Access-Control-Allow-Headers", "Content-Type");

                if (method == "OPTIONS")
                {
                    response.StatusCode = 200;
                    response.Close();
                    return;
                }

                // Route: /student/{maHocSinh}
                if (path.StartsWith("/student/"))
                {
                    string maHocSinhStr = path.Substring("/student/".Length).Trim('/');
                    if (int.TryParse(maHocSinhStr, out int maHocSinh))
                    {
                        HandleStudentPage(response, maHocSinh);
                    }
                    else
                    {
                        SendError(response, 400, "Mã học sinh không hợp lệ");
                    }
                }
                else if (path == "/" || path == "/index.html")
                {
                    SendWelcomePage(response);
                }
                else
                {
                    SendError(response, 404, "Không tìm thấy trang");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error handling request: {ex.Message}");
                SendError(response, 500, "Lỗi server: " + ex.Message);
            }
        }

        /// <summary>
        /// Xử lý trang chi tiết học sinh
        /// </summary>
        private void HandleStudentPage(HttpListenerResponse response, int maHocSinh)
        {
            try
            {
                // Lấy thông tin học sinh
                HocSinhDTO hocSinh = hocSinhBLL.GetHocSinhById(maHocSinh);
                if (hocSinh == null)
                {
                    SendError(response, 404, "Không tìm thấy học sinh");
                    return;
                }

                // Lấy thông tin lớp hiện tại
                string tenLop = "";
                string tenGVCN = "";
                string sdtGVCN = "";
                try
                {
                    int maHocKyHienTai = GetCurrentSemesterId();
                    if (maHocKyHienTai > 0)
                    {
                        int maLop = phanLopBLL.GetLopByHocSinh(maHocSinh, maHocKyHienTai);
                        if (maLop > 0)
                        {
                            var lop = lopHocBUS.LayLopTheoId(maLop);
                            if (lop != null)
                            {
                                tenLop = lop.tenLop;
                                if (!string.IsNullOrEmpty(lop.maGVCN))
                                {
                                    try
                                    {
                                        GiaoVienBUS giaoVienBUS = new GiaoVienBUS();
                                        GiaoVienDTO gv = giaoVienBUS.LayGiaoVienTheoMa(lop.maGVCN);
                                        if (gv != null)
                                        {
                                            tenGVCN = gv.HoTen;
                                            sdtGVCN = gv.SoDienThoai ?? "";
                                        }
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                }
                catch { }

                // Lấy danh sách phụ huynh
                List<(PhuHuynhDTO phuHuynh, string moiQuanHe)> dsPhuHuynh = new List<(PhuHuynhDTO, string)>();
                try
                {
                    dsPhuHuynh = hocSinhPhuHuynhBLL.GetPhuHuynhByHocSinh(maHocSinh);
                }
                catch { }

                // Tạo HTML
                string html = GenerateStudentHTML(hocSinh, tenLop, tenGVCN, sdtGVCN, dsPhuHuynh);
                SendHTML(response, html);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error generating student page: {ex.Message}");
                SendError(response, 500, "Lỗi khi tạo trang: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy ID học kỳ hiện tại
        /// </summary>
        private int GetCurrentSemesterId()
        {
            try
            {
                List<HocKyDTO> dsHocKy = hocKyBUS.DocDSHocKy();
                if (dsHocKy != null && dsHocKy.Count > 0)
                {
                    var hocKyDangDienRa = dsHocKy.Find(hk => hk.TrangThai == "Đang diễn ra");
                    if (hocKyDangDienRa != null)
                    {
                        return hocKyDangDienRa.MaHocKy;
                    }
                    else
                    {
                        // Lấy học kỳ mới nhất
                        dsHocKy.Sort((a, b) => 
                        {
                            if (b.NgayBD == null && a.NgayBD == null) return 0;
                            if (b.NgayBD == null) return -1;
                            if (a.NgayBD == null) return 1;
                            return b.NgayBD.Value.CompareTo(a.NgayBD.Value);
                        });
                        if (dsHocKy.Count > 0)
                        {
                            return dsHocKy[0].MaHocKy;
                        }
                    }
                }
            }
            catch { }
            return 0;
        }

        /// <summary>
        /// Tạo HTML cho trang chi tiết học sinh
        /// </summary>
        private string GenerateStudentHTML(
            HocSinhDTO hocSinh,
            string tenLop,
            string tenGVCN,
            string sdtGVCN,
            List<(PhuHuynhDTO phuHuynh, string moiQuanHe)> dsPhuHuynh)
        {
            // Lấy ảnh học sinh (base64)
            string avatarBase64 = GetStudentAvatarBase64(hocSinh);

            // Format ngày sinh
            string ngaySinh = hocSinh.NgaySinh.ToString("dd/MM/yyyy");
            DateTime ngayHetHan = DateTime.Now.AddYears(5);
            string ngayHetHanStr = ngayHetHan.ToString("dd/MM/yyyy");

            // Tạo bảng phụ huynh
            StringBuilder phuHuynhTable = new StringBuilder();
            if (dsPhuHuynh != null && dsPhuHuynh.Count > 0)
            {
                foreach (var item in dsPhuHuynh)
                {
                    phuHuynhTable.AppendLine($@"
                    <tr>
                        <td>{HttpUtility.HtmlEncode(item.phuHuynh.HoTen)}</td>
                        <td>{HttpUtility.HtmlEncode(item.phuHuynh.SoDienThoai ?? "N/A")}</td>
                        <td>{HttpUtility.HtmlEncode(item.phuHuynh.Email ?? "N/A")}</td>
                        <td>{HttpUtility.HtmlEncode(item.moiQuanHe)}</td>
                    </tr>");
                }
            }
            else
            {
                phuHuynhTable.AppendLine(@"
                    <tr>
                        <td colspan=""4"" style=""text-align: center; color: #666;"">Chưa có thông tin phụ huynh</td>
                    </tr>");
            }

            return $@"<!DOCTYPE html>
<html lang=""vi"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Thông tin học sinh - {HttpUtility.HtmlEncode(hocSinh.HoTen)}</title>
    <style>
        * {{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }}
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            padding: 20px;
            color: #333;
        }}
        .container {{
            max-width: 900px;
            margin: 0 auto;
            background: white;
            border-radius: 20px;
            box-shadow: 0 20px 60px rgba(0,0,0,0.3);
            overflow: hidden;
        }}
        .header {{
            background: linear-gradient(135deg, #1e40af 0%, #3b82f6 100%);
            color: white;
            padding: 30px;
            text-align: center;
            position: relative;
        }}
        .header::after {{
            content: '';
            position: absolute;
            bottom: 0;
            left: 0;
            right: 0;
            height: 3px;
            background: linear-gradient(90deg, #fcd34d, #fbbf24);
        }}
        .header h1 {{
            font-size: 24px;
            margin-bottom: 10px;
            color: #fcd34d;
        }}
        .header p {{
            font-size: 14px;
            opacity: 0.9;
        }}
        .content {{
            padding: 30px;
        }}
        .student-info {{
            display: flex;
            gap: 30px;
            margin-bottom: 30px;
            flex-wrap: wrap;
        }}
        .avatar-section {{
            flex-shrink: 0;
        }}
        .avatar {{
            width: 180px;
            height: 230px;
            border-radius: 15px;
            border: 4px solid #3b82f6;
            object-fit: cover;
            box-shadow: 0 10px 30px rgba(0,0,0,0.2);
        }}
        .info-section {{
            flex: 1;
            min-width: 300px;
        }}
        .info-group {{
            margin-bottom: 20px;
        }}
        .info-label {{
            font-weight: bold;
            color: #1e40af;
            font-size: 14px;
            margin-bottom: 5px;
            display: block;
        }}
        .info-value {{
            font-size: 16px;
            color: #333;
            padding: 8px 12px;
            background: #f0f9ff;
            border-radius: 8px;
            border-left: 4px solid #3b82f6;
        }}
        .info-value.class {{
            color: #d97706;
            font-weight: bold;
            background: #fef3c7;
            border-left-color: #f59e0b;
        }}
        .section {{
            margin-bottom: 30px;
            background: #f9fafb;
            padding: 20px;
            border-radius: 12px;
            border: 1px solid #e5e7eb;
        }}
        .section-title {{
            font-size: 18px;
            font-weight: bold;
            color: #1e40af;
            margin-bottom: 15px;
            padding-bottom: 10px;
            border-bottom: 2px solid #3b82f6;
        }}
        .table-container {{
            overflow-x: auto;
        }}
        table {{
            width: 100%;
            border-collapse: collapse;
            background: white;
            border-radius: 8px;
            overflow: hidden;
        }}
        thead {{
            background: linear-gradient(135deg, #1e40af 0%, #3b82f6 100%);
            color: white;
        }}
        th {{
            padding: 12px;
            text-align: left;
            font-weight: 600;
        }}
        td {{
            padding: 12px;
            border-bottom: 1px solid #e5e7eb;
        }}
        tbody tr:hover {{
            background: #f0f9ff;
        }}
        .footer {{
            text-align: center;
            padding: 20px;
            background: #1e40af;
            color: white;
            font-size: 12px;
        }}
        @media (max-width: 768px) {{
            .student-info {{
                flex-direction: column;
            }}
            .avatar {{
                width: 150px;
                height: 200px;
                margin: 0 auto;
            }}
            .content {{
                padding: 20px;
            }}
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h1>TRƯỜNG THPT SÀI GÒN</h1>
            <p>123 Đường Nguyễn Văn Cừ, Quận 5, Thành phố Hồ Chí Minh</p>
        </div>
        <div class=""content"">
            <div class=""student-info"">
                <div class=""avatar-section"">
                    <img src=""{avatarBase64}"" alt=""Ảnh học sinh"" class=""avatar"">
                </div>
                <div class=""info-section"">
                    <div class=""info-group"">
                        <span class=""info-label"">Mã học sinh:</span>
                        <div class=""info-value"">{hocSinh.MaHS:D6}</div>
                    </div>
                    <div class=""info-group"">
                        <span class=""info-label"">Họ và tên:</span>
                        <div class=""info-value"">{HttpUtility.HtmlEncode(hocSinh.HoTen.ToUpper())}</div>
                    </div>
                    <div class=""info-group"">
                        <span class=""info-label"">Ngày sinh:</span>
                        <div class=""info-value"">{ngaySinh}</div>
                    </div>
                    <div class=""info-group"">
                        <span class=""info-label"">Giới tính:</span>
                        <div class=""info-value"">{HttpUtility.HtmlEncode(hocSinh.GioiTinh)}</div>
                    </div>
                    <div class=""info-group"">
                        <span class=""info-label"">Ngày hết hạn:</span>
                        <div class=""info-value"">{ngayHetHanStr}</div>
                    </div>
                    <div class=""info-group"">
                        <span class=""info-label"">Trạng thái:</span>
                        <div class=""info-value"">{HttpUtility.HtmlEncode(hocSinh.TrangThai)}</div>
                    </div>
                </div>
            </div>

            <div class=""section"">
                <div class=""section-title"">📚 Thông tin lớp học</div>
                <div class=""info-group"">
                    <span class=""info-label"">Lớp hiện tại:</span>
                    <div class=""info-value class"">{HttpUtility.HtmlEncode(string.IsNullOrEmpty(tenLop) ? "Chưa phân lớp" : tenLop)}</div>
                </div>
                <div class=""info-group"">
                    <span class=""info-label"">Giáo viên chủ nhiệm:</span>
                    <div class=""info-value"">{HttpUtility.HtmlEncode(string.IsNullOrEmpty(tenGVCN) ? "Chưa phân công" : tenGVCN)}</div>
                </div>
                {(string.IsNullOrEmpty(sdtGVCN) ? "" : $@"
                <div class=""info-group"">
                    <span class=""info-label"">SĐT GVCN:</span>
                    <div class=""info-value"">{HttpUtility.HtmlEncode(sdtGVCN)}</div>
                </div>")}
            </div>

            <div class=""section"">
                <div class=""section-title"">👨‍👩‍👧‍👦 Thông tin phụ huynh</div>
                <div class=""table-container"">
                    <table>
                        <thead>
                            <tr>
                                <th>Họ và tên</th>
                                <th>SĐT</th>
                                <th>Email</th>
                                <th>Mối quan hệ</th>
                            </tr>
                        </thead>
                        <tbody>
                            {phuHuynhTable}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class=""footer"">
            <p>Mã học sinh: {hocSinh.MaHS:D6} | Hệ thống Quản lý Học sinh SGU 2025</p>
        </div>
    </div>
</body>
</html>";
        }

        /// <summary>
        /// Lấy ảnh học sinh dạng base64
        /// </summary>
        private string GetStudentAvatarBase64(HocSinhDTO hocSinh)
        {
            try
            {
                if (!string.IsNullOrEmpty(hocSinh.AnhDaiDien) && File.Exists(hocSinh.AnhDaiDien))
                {
                    byte[] imageBytes = File.ReadAllBytes(hocSinh.AnhDaiDien);
                    string base64 = Convert.ToBase64String(imageBytes);
                    string extension = Path.GetExtension(hocSinh.AnhDaiDien).ToLower();
                    string mimeType = extension == ".png" ? "image/png" : "image/jpeg";
                    return $"data:{mimeType};base64,{base64}";
                }
            }
            catch { }

            // Trả về placeholder
            return CreatePlaceholderAvatar();
        }

        /// <summary>
        /// Tạo placeholder avatar base64
        /// </summary>
        private string CreatePlaceholderAvatar()
        {
            // SVG placeholder đơn giản
            string svg = @"<svg width='180' height='230' xmlns='http://www.w3.org/2000/svg'>
                <rect width='180' height='230' fill='#bfdbfe'/>
                <text x='90' y='120' font-family='Arial' font-size='60' fill='#1e40af' text-anchor='middle' dominant-baseline='middle'>HS</text>
            </svg>";
            byte[] svgBytes = Encoding.UTF8.GetBytes(svg);
            string base64 = Convert.ToBase64String(svgBytes);
            return $"data:image/svg+xml;base64,{base64}";
        }

        /// <summary>
        /// Gửi HTML response
        /// </summary>
        private void SendHTML(HttpListenerResponse response, string html)
        {
            response.ContentType = "text/html; charset=utf-8";
            response.StatusCode = 200;
            byte[] buffer = Encoding.UTF8.GetBytes(html);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.Close();
        }

        /// <summary>
        /// Gửi trang welcome
        /// </summary>
        private void SendWelcomePage(HttpListenerResponse response)
        {
            string html = @"<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>Student Management System</title>
    <style>
        body { font-family: Arial; text-align: center; padding: 50px; background: #667eea; color: white; }
        h1 { margin-bottom: 20px; }
    </style>
</head>
<body>
    <h1>Hệ thống Quản lý Học sinh SGU 2025</h1>
    <p>Quét QR code trên thẻ học sinh để xem thông tin chi tiết</p>
</body>
</html>";
            SendHTML(response, html);
        }

        /// <summary>
        /// Gửi error response
        /// </summary>
        private void SendError(HttpListenerResponse response, int statusCode, string message)
        {
            response.StatusCode = statusCode;
            response.ContentType = "text/html; charset=utf-8";
            string html = $@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>Lỗi {statusCode}</title>
    <style>
        body {{ font-family: Arial; text-align: center; padding: 50px; background: #fee; color: #c33; }}
        h1 {{ margin-bottom: 20px; }}
    </style>
</head>
<body>
    <h1>Lỗi {statusCode}</h1>
    <p>{HttpUtility.HtmlEncode(message)}</p>
</body>
</html>";
            byte[] buffer = Encoding.UTF8.GetBytes(html);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.Close();
        }

        /// <summary>
        /// Thử khởi động Ngrok nếu được cấu hình (chỉ khi mode = Ngrok hoặc Auto)
        /// </summary>
        private void TryStartNgrokIfNeeded()
        {
            string mode = ConfigurationManager.AppSettings["WebServerMode"] ?? "Local";
            if (mode != "Ngrok" && mode != "Auto")
                return;

            string ngrokPath = ConfigurationManager.AppSettings["NgrokPath"] ?? "";
            string ngrokAuthToken = ConfigurationManager.AppSettings["NgrokAuthToken"] ?? "";

            // Nếu không có đường dẫn Ngrok, bỏ qua (UrlResolver sẽ xử lý fallback)
            if (string.IsNullOrEmpty(ngrokPath) || !File.Exists(ngrokPath))
            {
                System.Diagnostics.Debug.WriteLine("Ngrok path not configured. UrlResolver will use fallback.");
                return;
            }

            try
            {
                // Kiểm tra xem Ngrok đã chạy chưa
                var existingProcesses = System.Diagnostics.Process.GetProcessesByName("ngrok");
                if (existingProcesses.Length > 0)
                {
                    System.Diagnostics.Debug.WriteLine("Ngrok is already running.");
                    return;
                }

                // Khởi động Ngrok trong background (không block)
                Task.Run(() =>
                {
                    try
                    {
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = ngrokPath,
                            Arguments = $"http {port}",
                            UseShellExecute = false,
                            CreateNoWindow = true,
                            RedirectStandardOutput = true,
                            RedirectStandardError = true
                        };

                        if (!string.IsNullOrEmpty(ngrokAuthToken))
                        {
                            startInfo.EnvironmentVariables["NGROK_AUTHTOKEN"] = ngrokAuthToken;
                        }

                        System.Diagnostics.Process.Start(startInfo);
                        System.Diagnostics.Debug.WriteLine("Ngrok process started in background.");
                        
                        // Clear cache để UrlResolver resolve lại sau khi Ngrok khởi động
                        Task.Delay(3000).ContinueWith(_ => UrlResolver.ClearCache());
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error starting Ngrok: {ex.Message}");
                    }
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking/starting Ngrok: {ex.Message}");
            }
        }

        /// <summary>
        /// Dừng Ngrok process nếu cần (chỉ khi mode = Ngrok)
        /// Lưu ý: Chỉ dừng khi mode = Ngrok, không dừng khi Auto (user tự quản lý)
        /// </summary>
        private void StopNgrokIfNeeded()
        {
            string mode = ConfigurationManager.AppSettings["WebServerMode"] ?? "Local";
            // Chỉ dừng Ngrok nếu mode = Ngrok (không dừng nếu Auto vì có thể user tự start)
            if (mode != "Ngrok")
                return;

            try
            {
                var processes = System.Diagnostics.Process.GetProcessesByName("ngrok");
                foreach (var process in processes)
                {
                    try
                    {
                        process.Kill();
                    }
                    catch { }
                }
            }
            catch { }
        }

        /// <summary>
        /// Thử thêm firewall rule để cho phép kết nối từ bên ngoài
        /// </summary>
        private void TryAddFirewallRule()
        {
            try
            {
                string ruleName = $"StudentWebServer_Port_{port}";
                
                // Kiểm tra xem rule đã tồn tại chưa
                System.Diagnostics.ProcessStartInfo checkInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = $"advfirewall firewall show rule name=\"{ruleName}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (System.Diagnostics.Process checkProcess = System.Diagnostics.Process.Start(checkInfo))
                {
                    if (checkProcess != null)
                    {
                        string output = checkProcess.StandardOutput.ReadToEnd();
                        checkProcess.WaitForExit();
                        
                        // Nếu rule đã tồn tại, không cần thêm lại
                        if (output.Contains(ruleName) && output.Contains("Enabled"))
                        {
                            System.Diagnostics.Debug.WriteLine($"Firewall rule '{ruleName}' already exists");
                            return;
                        }
                    }
                }

                // Thử thêm firewall rule (không dùng runas để tránh popup)
                System.Diagnostics.ProcessStartInfo addInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = $"advfirewall firewall add rule name=\"{ruleName}\" dir=in action=allow protocol=TCP localport={port}",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (System.Diagnostics.Process addProcess = System.Diagnostics.Process.Start(addInfo))
                {
                    if (addProcess != null)
                    {
                        string output = addProcess.StandardOutput.ReadToEnd();
                        string error = addProcess.StandardError.ReadToEnd();
                        addProcess.WaitForExit();
                        
                        if (addProcess.ExitCode == 0)
                        {
                            System.Diagnostics.Debug.WriteLine($"Firewall rule '{ruleName}' added successfully");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"Could not add firewall rule (may need admin rights): {error}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Không crash nếu không thể thêm firewall rule
                System.Diagnostics.Debug.WriteLine($"Error adding firewall rule: {ex.Message}");
            }
        }

        /// <summary>
        /// Log hướng dẫn về firewall nếu cần
        /// </summary>
        private void LogFirewallInstructions()
        {
            try
            {
                string lanIp = UrlResolver.GetLocalIPAddress();
                if (!string.IsNullOrEmpty(lanIp))
                {
                    System.Diagnostics.Debug.WriteLine("========================================");
                    System.Diagnostics.Debug.WriteLine("FIREWALL INSTRUCTIONS:");
                    System.Diagnostics.Debug.WriteLine($"If phone cannot access http://{lanIp}:{port}");
                    System.Diagnostics.Debug.WriteLine("Run PowerShell as Administrator and execute:");
                    System.Diagnostics.Debug.WriteLine($"  netsh advfirewall firewall add rule name=\"StudentWebServer_Port_{port}\" dir=in action=allow protocol=TCP localport={port}");
                    System.Diagnostics.Debug.WriteLine("Or manually add Windows Firewall rule:");
                    System.Diagnostics.Debug.WriteLine($"  - Port: {port}");
                    System.Diagnostics.Debug.WriteLine($"  - Protocol: TCP");
                    System.Diagnostics.Debug.WriteLine($"  - Action: Allow");
                    System.Diagnostics.Debug.WriteLine("========================================");
                }
            }
            catch { }
        }
    }
}



