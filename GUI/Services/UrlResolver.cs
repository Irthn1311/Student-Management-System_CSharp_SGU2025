using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Student_Management_System_CSharp_SGU2025.GUI.Services
{
    /// <summary>
    /// Resolver để tự động xác định Base URL cho web server
    /// Hỗ trợ: Custom, Ngrok, Local, Auto (tự động chọn)
    /// </summary>
    public static class UrlResolver
    {
        private static string _cachedBaseUrl = null;
        private static readonly object _lock = new object();
        private static DateTime _lastResolveTime = DateTime.MinValue;
        private static readonly TimeSpan CacheTimeout = TimeSpan.FromMinutes(5); // Cache 5 phút

        /// <summary>
        /// Resolve base URL dựa trên cấu hình
        /// </summary>
        public static string ResolveBaseUrl()
        {
            lock (_lock)
            {
                // Kiểm tra cache
                if (_cachedBaseUrl != null && DateTime.Now - _lastResolveTime < CacheTimeout)
                {
                    return _cachedBaseUrl;
                }

                string mode = ConfigurationManager.AppSettings["WebServerMode"] ?? "Local";
                int port = int.Parse(ConfigurationManager.AppSettings["WebServerPort"] ?? "8080");
                string customUrl = ConfigurationManager.AppSettings["CustomPublicUrl"] ?? "";

                string resolvedUrl = null;

                try
                {
                    switch (mode.ToLower())
                    {
                        case "custom":
                            resolvedUrl = ResolveCustomUrl(customUrl);
                            break;

                        case "ngrok":
                            resolvedUrl = ResolveNgrokUrl(port);
                            break;

                        case "local":
                            resolvedUrl = $"http://localhost:{port}";
                            break;

                        case "auto":
                            resolvedUrl = ResolveAutoUrl(port);
                            break;

                        default:
                            resolvedUrl = $"http://localhost:{port}";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error resolving URL: {ex.Message}");
                    // Fallback về localhost
                    resolvedUrl = $"http://localhost:{port}";
                }

                // Cache kết quả
                _cachedBaseUrl = resolvedUrl;
                _lastResolveTime = DateTime.Now;

                return resolvedUrl;
            }
        }

        /// <summary>
        /// Resolve Custom URL
        /// </summary>
        private static string ResolveCustomUrl(string customUrl)
        {
            if (string.IsNullOrWhiteSpace(customUrl))
            {
                throw new InvalidOperationException("CustomPublicUrl is not configured");
            }
            return NormalizeUrl(customUrl.TrimEnd('/'));
        }

        /// <summary>
        /// Resolve Ngrok URL
        /// </summary>
        private static string ResolveNgrokUrl(int port)
        {
            string ngrokUrl = TryGetNgrokUrl();
            if (string.IsNullOrEmpty(ngrokUrl))
            {
                throw new InvalidOperationException("Ngrok is not running or not accessible");
            }
            return NormalizeUrl(ngrokUrl);
        }

        /// <summary>
        /// Resolve Auto URL (ưu tiên: Ngrok → LAN IP → localhost)
        /// </summary>
        private static string ResolveAutoUrl(int port)
        {
            // 1. Thử Ngrok
            try
            {
                string ngrokUrl = TryGetNgrokUrl();
                if (!string.IsNullOrEmpty(ngrokUrl))
                {
                    System.Diagnostics.Debug.WriteLine($"Auto mode: Using Ngrok URL - {ngrokUrl}");
                    return ngrokUrl;
                }
            }
            catch
            {
                // Ngrok không có, tiếp tục
            }

            // 2. Thử LAN IP
            try
            {
                string lanIp = GetLocalIPAddress();
                if (!string.IsNullOrEmpty(lanIp))
                {
                    // Loại bỏ "www." prefix nếu có (không hợp lệ cho IP)
                    lanIp = lanIp.TrimStart('w', 'W', '.');
                    string lanUrl = $"http://{lanIp}:{port}";
                    System.Diagnostics.Debug.WriteLine($"Auto mode: Using LAN IP - {lanUrl}");
                    return NormalizeUrl(lanUrl);
                }
            }
            catch
            {
                // Không lấy được LAN IP, tiếp tục
            }

            // 3. Fallback về localhost
            System.Diagnostics.Debug.WriteLine($"Auto mode: Using localhost - http://localhost:{port}");
            return $"http://localhost:{port}";
        }

        /// <summary>
        /// Thử lấy URL từ Ngrok API
        /// </summary>
        public static string TryGetNgrokUrl()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(2); // Timeout ngắn để không block
                    string response = client.GetStringAsync("http://127.0.0.1:4040/api/tunnels").Result;

                    // Parse JSON đơn giản: tìm "public_url":"https://xxx.ngrok.io"
                    int startIndex = response.IndexOf("\"public_url\":\"");
                    if (startIndex > 0)
                    {
                        startIndex += "\"public_url\":\"".Length;
                        int endIndex = response.IndexOf("\"", startIndex);
                        if (endIndex > startIndex)
                        {
                            string url = response.Substring(startIndex, endIndex - startIndex);
                            if (url.StartsWith("http://") || url.StartsWith("https://"))
                            {
                                return NormalizeUrl(url);
                            }
                        }
                    }

                    // Thử tìm trong mảng tunnels
                    int tunnelsStart = response.IndexOf("\"tunnels\":[");
                    if (tunnelsStart > 0)
                    {
                        int urlIndex = response.IndexOf("\"public_url\":\"", tunnelsStart);
                        if (urlIndex > 0)
                        {
                            urlIndex += "\"public_url\":\"".Length;
                            int urlEnd = response.IndexOf("\"", urlIndex);
                            if (urlEnd > urlIndex)
                            {
                                string url = response.Substring(urlIndex, urlEnd - urlIndex);
                                if (url.StartsWith("http://") || url.StartsWith("https://"))
                                {
                                    return NormalizeUrl(url);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Không có Ngrok hoặc API chưa sẵn sàng - không throw, chỉ log
                System.Diagnostics.Debug.WriteLine($"Ngrok API not available: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Lấy LAN IPv4 address
        /// </summary>
        public static string GetLocalIPAddress()
        {
            try
            {
                // Thử lấy IP từ network interfaces
                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    // Chỉ lấy interface đang up và không phải loopback
                    if (ni.OperationalStatus == OperationalStatus.Up &&
                        ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    {
                        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            // Chỉ lấy IPv4
                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                string ipString = ip.Address.ToString();
                                
                                // Loại bỏ "www." prefix nếu có (không hợp lệ cho IP)
                                ipString = ipString.TrimStart('w', 'W', '.');
                                
                                // Bỏ qua localhost và link-local
                                if (!ipString.StartsWith("127.") && 
                                    !ipString.StartsWith("169.254.") &&
                                    !ipString.Equals("0.0.0.0"))
                                {
                                    return ipString;
                                }
                            }
                        }
                    }
                }

                // Fallback: Thử lấy IP từ Dns
                try
                {
                    string hostName = Dns.GetHostName();
                    IPAddress[] addresses = Dns.GetHostAddresses(hostName);
                    foreach (IPAddress addr in addresses)
                    {
                        if (addr.AddressFamily == AddressFamily.InterNetwork &&
                            !addr.ToString().StartsWith("127."))
                        {
                            string ipString = addr.ToString();
                            // Loại bỏ "www." prefix nếu có
                            ipString = ipString.TrimStart('w', 'W', '.');
                            return ipString;
                        }
                    }
                }
                catch { }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error getting LAN IP: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Clear cache để force resolve lại
        /// </summary>
        public static void ClearCache()
        {
            lock (_lock)
            {
                _cachedBaseUrl = null;
                _lastResolveTime = DateTime.MinValue;
            }
        }

        /// <summary>
        /// Resolve URL async (không block)
        /// </summary>
        public static async Task<string> ResolveBaseUrlAsync()
        {
            return await Task.Run(() => ResolveBaseUrl());
        }

        /// <summary>
        /// Normalize URL: đảm bảo có protocol, loại bỏ www. không hợp lệ
        /// </summary>
        private static string NormalizeUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return url;

            url = url.Trim();

            // Loại bỏ "www." prefix nếu không có protocol (không hợp lệ)
            if (url.StartsWith("www.", StringComparison.OrdinalIgnoreCase) && 
                !url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && 
                !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = url.Substring(4); // Bỏ "www."
            }

            // Đảm bảo có protocol
            if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && 
                !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                // Nếu là IP address hoặc localhost, dùng http
                if (url.Contains("localhost") || IsIPAddress(url.Split(':')[0]))
                {
                    url = "http://" + url;
                }
                else
                {
                    // Domain name, dùng https mặc định
                    url = "https://" + url;
                }
            }

            return url.TrimEnd('/');
        }

        /// <summary>
        /// Kiểm tra xem string có phải là IP address không
        /// </summary>
        private static bool IsIPAddress(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            // Loại bỏ "www." nếu có
            input = input.TrimStart('w', 'W', '.');

            return IPAddress.TryParse(input, out _);
        }
    }
}

