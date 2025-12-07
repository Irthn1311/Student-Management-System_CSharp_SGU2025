# BÁO CÁO KỸ THUẬT: HỆ THỐNG QR CODE WEB SERVER
## Hệ thống Quản lý Học sinh - SGU 2025

---

## 📋 MỤC LỤC

1. [Tổng quan hệ thống](#1-tổng-quan-hệ-thống)
2. [Kiến trúc và các thành phần](#2-kiến-trúc-và-các-thành-phần)
3. [Sơ đồ luồng hoạt động](#3-sơ-đồ-luồng-hoạt-động)
4. [Chi tiết từng module](#4-chi-tiết-từng-module)
5. [Cơ chế hoạt động](#5-cơ-chế-hoạt-động)
6. [Troubleshooting](#6-troubleshooting)
7. [Future Upgrade](#7-future-upgrade)

---

## 1. TỔNG QUAN HỆ THỐNG

### 1.1. Mục đích

Hệ thống QR Code Web Server được thiết kế để cho phép quét mã QR trên thẻ học sinh và hiển thị thông tin chi tiết trên trình duyệt web (đặc biệt là điện thoại di động). Hệ thống tự động xác định URL phù hợp nhất (Ngrok, LAN IP, hoặc localhost) và phục vụ trang web responsive với đầy đủ thông tin học sinh.

### 1.2. Yêu cầu kỹ thuật

- **.NET Framework 4.7.2+**
- **Windows OS** (do sử dụng HttpListener và Windows Firewall)
- **MySQL Database** (để lấy thông tin học sinh)
- **Network connectivity** (cho LAN IP hoặc Ngrok)

### 1.3. Tính năng chính

- ✅ Tự động khởi động web server khi mở form chi tiết học sinh
- ✅ Tự động resolve URL (Auto Mode: Ngrok → LAN IP → Localhost)
- ✅ Sinh QR code động với URL phù hợp
- ✅ Phục vụ HTML responsive, tối ưu cho mobile
- ✅ Không block UI thread
- ✅ Tự động xử lý firewall rules
- ✅ Hỗ trợ Ngrok để truy cập từ internet

---

## 2. KIẾN TRÚC VÀ CÁC THÀNH PHẦN

### 2.1. Sơ đồ kiến trúc tổng thể

```
┌─────────────────────────────────────────────────────────────┐
│                    USER INTERFACE LAYER                      │
│  ┌──────────────────┐         ┌──────────────────────┐    │
│  │ XemChiTietHocSinh│         │    StudentCard        │    │
│  │     (Form)       │────────▶│  (UserControl)        │    │
│  └────────┬─────────┘         └──────────┬───────────┘    │
│           │                               │                 │
│           │ StartServer()                 │ GenerateQRCode()│
│           │                               │                 │
└───────────┼───────────────────────────────┼─────────────────┘
            │                               │
            ▼                               ▼
┌─────────────────────────────────────────────────────────────┐
│                  SERVICE LAYER                               │
│  ┌──────────────────────┐    ┌──────────────────────┐     │
│  │StudentWebServerManager│    │    UrlResolver        │     │
│  │   (Singleton)         │    │   (Static Helper)     │     │
│  └──────────┬────────────┘    └──────────┬───────────┘     │
│             │                            │                  │
│             │ GetBaseUrl()               │ ResolveBaseUrl()│
│             │                            │                  │
└─────────────┼────────────────────────────┼──────────────────┘
              │                            │
              ▼                            ▼
┌─────────────────────────────────────────────────────────────┐
│                  SERVER LAYER                                │
│  ┌──────────────────────────────────────────────────────┐   │
│  │         StudentWebServer (HttpListener)               │   │
│  │  ┌──────────────────────────────────────────────┐    │   │
│  │  │  HTTP Request Handler                       │    │   │
│  │  │  - /student/{maHocSinh}                     │    │   │
│  │  │  - / (welcome page)                         │    │   │
│  │  └────────────────────────────────────────────┘    │   │
│  │                                                      │   │
│  │  ┌──────────────────────────────────────────────┐    │   │
│  │  │  HTML Generator                              │    │   │
│  │  │  - GenerateStudentHTML()                    │    │   │
│  │  │  - GetStudentAvatarBase64()                  │    │   │
│  │  └────────────────────────────────────────────┘    │   │
│  └──────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
              │
              ▼
┌─────────────────────────────────────────────────────────────┐
│                  DATA LAYER                                  │
│  ┌──────────┐  ┌──────────┐  ┌──────────┐  ┌──────────┐     │
│  │HocSinhBLL│  │PhuHuynhBLL│ │LopHocBUS│  │PhanLopBLL│     │
│  └──────────┘  └──────────┘  └──────────┘  └──────────┘     │
│       │             │             │             │            │
│       └─────────────┴─────────────┴─────────────┘            │
│                          │                                   │
│                          ▼                                   │
│                   MySQL Database                             │
└─────────────────────────────────────────────────────────────┘
```

### 2.2. Các thành phần chính

#### 2.2.1. **StudentWebServer** (Core Server)
- **Vai trò**: Web server HTTP đơn giản sử dụng HttpListener
- **Chức năng**: 
  - Lắng nghe HTTP requests trên port được cấu hình
  - Xử lý routing (/, /student/{id})
  - Generate HTML động từ dữ liệu database
  - Phục vụ static assets (nếu có)

#### 2.2.2. **StudentWebServerManager** (Singleton Manager)
- **Vai trò**: Quản lý lifecycle của web server
- **Chức năng**:
  - Đảm bảo chỉ có 1 instance server duy nhất
  - Khởi động/dừng server
  - Cung cấp Base URL cho các component khác

#### 2.2.3. **UrlResolver** (URL Resolution Engine)
- **Vai trò**: Tự động xác định URL tốt nhất cho web server
- **Chức năng**:
  - Resolve URL theo mode (Custom, Ngrok, Local, Auto)
  - Lấy Ngrok URL từ API (port 4040)
  - Detect LAN IP address
  - Cache URL để tối ưu performance

#### 2.2.4. **StudentCard** (UI Component)
- **Vai trò**: Hiển thị thẻ học sinh và QR code
- **Chức năng**:
  - Render thông tin học sinh
  - Generate QR code với URL động
  - Fallback về text nếu URL resolution thất bại

#### 2.2.5. **XemChiTietHocSinh** (Main Form)
- **Vai trò**: Form hiển thị chi tiết học sinh
- **Chức năng**:
  - Tự động khởi động web server khi mở
  - Load và hiển thị thông tin học sinh
  - Tích hợp StudentCard component

---

## 3. SƠ ĐỒ LUỒNG HOẠT ĐỘNG

### 3.1. Luồng khởi động hệ thống

```
┌─────────────────────────────────────────────────────────────┐
│                    APPLICATION START                         │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│              Program.cs - Application.Exit Event            │
│         Đăng ký: StopServer() khi app đóng                  │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│         User mở form XemChiTietHocSinh(maHocSinh)           │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│  XemChiTietHocSinh Constructor                              │
│  ┌────────────────────────────────────────────────────┐    │
│  │ 1. InitializeComponent()                           │    │
│  │ 2. Khởi tạo BLL/BUS objects                        │    │
│  │ 3. StudentWebServerManager.Instance.StartServer()  │    │
│  └────────────────────────────────────────────────────┘    │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│         StudentWebServerManager.StartServer()               │
│  ┌────────────────────────────────────────────────────┐    │
│  │ if (!webServer.IsRunning)                         │    │
│  │     webServer.Start()                             │    │
│  └────────────────────────────────────────────────────┘    │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│              StudentWebServer.Start()                        │
│  ┌────────────────────────────────────────────────────┐    │
│  │ 1. Tạo HttpListener                                │    │
│  │ 2. Bind: http://+:{port}/                         │    │
│  │ 3. Start listener                                  │    │
│  │ 4. Tạo background thread: Listen()                │    │
│  │ 5. TryAddFirewallRule()                           │    │
│  │ 6. TryStartNgrokIfNeeded()                        │    │
│  └────────────────────────────────────────────────────┘    │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│         Background Thread: Listen()                          │
│  ┌────────────────────────────────────────────────────┐    │
│  │ while (isRunning)                                  │    │
│  │     context = listener.GetContext()                │    │
│  │     Task.Run(() => HandleRequest(context))        │    │
│  └────────────────────────────────────────────────────┘    │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│              LoadThongTinHocSinh()                          │
│  ┌────────────────────────────────────────────────────┐    │
│  │ 1. Load student info từ database                   │    │
│  │ 2. studentCard.LoadStudentInfo()                    │    │
│  │ 3. LoadThongTinLop()                               │    │
│  │ 4. LoadDanhSachPhuHuynh()                          │    │
│  └────────────────────────────────────────────────────┘    │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│         StudentCard.DisplayStudentInfo()                    │
│  ┌────────────────────────────────────────────────────┐    │
│  │ 1. Render student info                              │    │
│  │ 2. LoadStudentPhoto()                               │    │
│  │ 3. GenerateQRCode() ← QUAN TRỌNG                    │    │
│  └────────────────────────────────────────────────────┘    │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│         StudentCard.GenerateQRCode()                         │
│  ┌────────────────────────────────────────────────────┐    │
│  │ 1. GetQRCodeUrl()                                   │    │
│  │    ├─ StudentWebServerManager.StartServer()        │    │
│  │    └─ UrlResolver.ResolveBaseUrl()                 │    │
│  │ 2. Tạo QR code với URL                              │    │
│  │ 3. Hiển thị QR code trên UI                         │    │
│  └────────────────────────────────────────────────────┘    │
└─────────────────────────────────────────────────────────────┘
```

### 3.2. Luồng URL Resolution (Auto Mode)

```
┌─────────────────────────────────────────────────────────────┐
│         UrlResolver.ResolveBaseUrl()                         │
│         Mode = "Auto"                                        │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
                    ┌───────────────┐
                    │ Check Cache? │
                    └───────┬───────┘
                            │ Yes → Return cached URL
                            │ No
                            ▼
┌─────────────────────────────────────────────────────────────┐
│              Step 1: Try Ngrok                               │
│  ┌────────────────────────────────────────────────────┐    │
│  │ TryGetNgrokUrl()                                   │    │
│  │ ├─ GET http://127.0.0.1:4040/api/tunnels          │    │
│  │ ├─ Parse JSON: "public_url"                        │    │
│  │ └─ Return: https://xxx.ngrok.io                   │    │
│  └────────────────────────────────────────────────────┘    │
└───────────────────────────┬─────────────────────────────────┘
                            │
                    ┌───────┴───────┐
                    │   Success?    │
                    └───────┬───────┘
                            │ Yes → Return Ngrok URL
                            │ No
                            ▼
┌─────────────────────────────────────────────────────────────┐
│              Step 2: Try LAN IP                              │
│  ┌────────────────────────────────────────────────────┐    │
│  │ GetLocalIPAddress()                                 │    │
│  │ ├─ NetworkInterface.GetAllNetworkInterfaces()       │    │
│  │ ├─ Filter: Up, Not Loopback, IPv4                  │    │
│  │ ├─ Skip: 127.x, 169.254.x, 0.0.0.0                │    │
│  │ └─ Return: 10.212.31.135                           │    │
│  └────────────────────────────────────────────────────┘    │
└───────────────────────────┬─────────────────────────────────┘
                            │
                    ┌───────┴───────┐
                    │   Success?    │
                    └───────┬───────┘
                            │ Yes → Return http://{LAN_IP}:{port}
                            │ No
                            ▼
┌─────────────────────────────────────────────────────────────┐
│              Step 3: Fallback to Localhost                  │
│  ┌────────────────────────────────────────────────────┐    │
│  │ Return: http://localhost:{port}                     │    │
│  └────────────────────────────────────────────────────┘    │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
                    ┌───────────────┐
                    │ Cache URL     │
                    │ (5 minutes)   │
                    └───────────────┘
```

### 3.3. Luồng xử lý HTTP Request

```
┌─────────────────────────────────────────────────────────────┐
│         User quét QR code trên điện thoại                    │
│         URL: http://10.212.31.135:8080/student/1            │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│         Browser gửi HTTP GET Request                         │
│         GET /student/1 HTTP/1.1                             │
│         Host: 10.212.31.135:8080                            │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│         HttpListener nhận request                            │
│         listener.GetContext()                                │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│         Task.Run(() => HandleRequest(context))              │
│         (Không block main thread)                            │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│         HandleRequest()                                      │
│  ┌────────────────────────────────────────────────────┐    │
│  │ 1. Parse path: /student/1                          │    │
│  │ 2. Extract maHocSinh = 1                           │    │
│  │ 3. HandleStudentPage(response, 1)                 │    │
│  └────────────────────────────────────────────────────┘    │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│         HandleStudentPage(maHocSinh = 1)                    │
│  ┌────────────────────────────────────────────────────┐    │
│  │ 1. hocSinhBLL.GetHocSinhById(1)                    │    │
│  │ 2. GetCurrentSemesterId()                          │    │
│  │ 3. phanLopBLL.GetLopByHocSinh(1, maHocKy)          │    │
│  │ 4. lopHocBUS.LayLopTheoId(maLop)                    │    │
│  │ 5. hocSinhPhuHuynhBLL.GetPhuHuynhByHocSinh(1)       │    │
│  │ 6. GenerateStudentHTML(...)                        │    │
│  │ 7. SendHTML(response, html)                         │    │
│  └────────────────────────────────────────────────────┘    │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│         GenerateStudentHTML()                                │
│  ┌────────────────────────────────────────────────────┐    │
│  │ 1. GetStudentAvatarBase64()                        │    │
│  │ 2. Format dates                                    │    │
│  │ 3. Build phuHuynhTable HTML                        │    │
│  │ 4. Generate complete HTML template                 │    │
│  │    - Header với logo trường                        │    │
│  │    - Student info section                          │    │
│  │    - Class info section                            │    │
│  │    - Parent info table                            │    │
│  │    - Responsive CSS                                │    │
│  └────────────────────────────────────────────────────┘    │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│         SendHTML(response, html)                             │
│  ┌────────────────────────────────────────────────────┐    │
│  │ 1. response.ContentType = "text/html; charset=utf-8"│    │
│  │ 2. response.StatusCode = 200                        │    │
│  │ 3. Convert HTML to bytes                           │    │
│  │ 4. response.OutputStream.Write(buffer)             │    │
│  │ 5. response.Close()                                 │    │
│  └────────────────────────────────────────────────────┘    │
└───────────────────────────┬─────────────────────────────────┘
                            │
                            ▼
┌─────────────────────────────────────────────────────────────┐
│         Browser nhận HTML và render                          │
│         Hiển thị trang web với thông tin học sinh            │
└─────────────────────────────────────────────────────────────┘
```

---

## 4. CHI TIẾT TỪNG MODULE

### 4.1. StudentWebServer.cs

#### 4.1.1. Cấu trúc class

```csharp
public class StudentWebServer
{
    private HttpListener listener;           // HTTP listener
    private bool isRunning;                  // Trạng thái server
    private Thread serverThread;             // Background thread
    private int port;                        // Port server
    private HocSinhBLL hocSinhBLL;          // Business logic
    // ... các BLL/BUS khác
}
```

#### 4.1.2. Phương thức Start()

**Mục đích**: Khởi động HTTP server

**Logic**:
1. Tạo `HttpListener` instance
2. Bind với `http://+:{port}/` để chấp nhận mọi hostname
3. Nếu bind thất bại (thiếu quyền), fallback về `localhost`
4. Start listener
5. Tạo background thread để lắng nghe requests
6. Thử thêm firewall rule
7. Thử khởi động Ngrok nếu cần

**Tại sao dùng `http://+:`**: 
- `+` là wildcard, chấp nhận mọi hostname (localhost, IP, domain)
- Cho phép truy cập từ cả localhost và LAN IP
- Cần quyền admin hoặc URL ACL trên Windows

#### 4.1.3. Phương thức Listen()

**Mục đích**: Lắng nghe HTTP requests trong background thread

**Logic**:
```csharp
while (isRunning)
{
    HttpListenerContext context = listener.GetContext();
    Task.Run(() => HandleRequest(context));  // Async, không block
}
```

**Tại sao dùng background thread**:
- HttpListener.GetContext() là blocking call
- Nếu chạy trên UI thread → UI sẽ đơ
- Background thread + Task.Run → không block UI

#### 4.1.4. Phương thức HandleRequest()

**Mục đích**: Xử lý từng HTTP request

**Routing logic**:
- `/student/{maHocSinh}` → HandleStudentPage()
- `/` hoặc `/index.html` → SendWelcomePage()
- Khác → 404 Error

**CORS headers**: Thêm headers để cho phép cross-origin requests

#### 4.1.5. Phương thức GenerateStudentHTML()

**Mục đích**: Tạo HTML động từ dữ liệu học sinh

**Data flow**:
1. Nhận: HocSinhDTO, tenLop, tenGVCN, dsPhuHuynh
2. Convert ảnh → base64
3. Format dates (dd/MM/yyyy)
4. Build HTML table cho phụ huynh
5. Generate complete HTML với:
   - Responsive CSS (mobile-first)
   - Header với logo trường
   - Student info section
   - Class info section
   - Parent info table
   - Footer

**HTML Template Structure**:
```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Thông tin học sinh</title>
    <style>/* Responsive CSS */</style>
</head>
<body>
    <div class="container">
        <div class="header">TRƯỜNG THPT SÀI GÒN</div>
        <div class="content">
            <div class="student-info">
                <img src="data:image/jpeg;base64,..." class="avatar">
                <div class="info-section">
                    <!-- Student details -->
                </div>
            </div>
            <div class="section">Thông tin lớp học</div>
            <div class="section">Thông tin phụ huynh</div>
        </div>
    </div>
</body>
</html>
```

### 4.2. StudentWebServerManager.cs

#### 4.2.1. Singleton Pattern

**Tại sao dùng Singleton**:
- Đảm bảo chỉ có 1 instance server duy nhất
- Tránh conflict port khi nhiều form mở cùng lúc
- Quản lý lifecycle tập trung
- Dễ dàng truy cập từ bất kỳ đâu

**Implementation**:
```csharp
private static StudentWebServerManager _instance;
private static readonly object _lock = new object();

public static StudentWebServerManager Instance
{
    get
    {
        if (_instance == null)
        {
            lock (_lock)  // Thread-safe
            {
                if (_instance == null)
                    _instance = new StudentWebServerManager();
            }
        }
        return _instance;
    }
}
```

**Double-checked locking**: Đảm bảo thread-safe khi khởi tạo

#### 4.2.2. Phương thức StartServer()

**Logic**:
- Kiểm tra `webServer.IsRunning`
- Nếu chưa chạy → gọi `webServer.Start()`
- Idempotent: Gọi nhiều lần cũng an toàn

### 4.3. UrlResolver.cs

#### 4.3.1. ResolveBaseUrl() - Core Method

**Mục đích**: Resolve URL dựa trên mode cấu hình

**Mode Logic**:

**Custom Mode**:
```csharp
if (mode == "Custom")
    return NormalizeUrl(customUrl);
```

**Ngrok Mode**:
```csharp
if (mode == "Ngrok")
{
    string url = TryGetNgrokUrl();
    if (url == null) throw exception;
    return url;
}
```

**Local Mode**:
```csharp
if (mode == "Local")
    return $"http://localhost:{port}";
```

**Auto Mode** (Quan trọng nhất):
```csharp
if (mode == "Auto")
{
    // 1. Try Ngrok
    string ngrok = TryGetNgrokUrl();
    if (ngrok != null) return ngrok;
    
    // 2. Try LAN IP
    string lanIp = GetLocalIPAddress();
    if (lanIp != null) return $"http://{lanIp}:{port}";
    
    // 3. Fallback
    return $"http://localhost:{port}";
}
```

#### 4.3.2. TryGetNgrokUrl()

**Mục đích**: Lấy public URL từ Ngrok API

**Logic**:
1. GET `http://127.0.0.1:4040/api/tunnels`
2. Parse JSON response
3. Tìm `"public_url"` field
4. Return URL hoặc null

**Error handling**: 
- Timeout 2 giây (không block)
- Catch exception → return null (không crash)

#### 4.3.3. GetLocalIPAddress()

**Mục đích**: Lấy LAN IPv4 address

**Logic**:
1. Lặp qua `NetworkInterface.GetAllNetworkInterfaces()`
2. Filter: `OperationalStatus.Up`, không phải Loopback
3. Lấy IPv4 addresses
4. Skip: 127.x (localhost), 169.254.x (link-local), 0.0.0.0
5. Return IP đầu tiên hợp lệ

**Fallback**: Nếu không tìm được → dùng `Dns.GetHostAddresses()`

#### 4.3.4. NormalizeUrl()

**Mục đích**: Chuẩn hóa URL (thêm protocol, loại bỏ www. không hợp lệ)

**Logic**:
1. Loại bỏ `www.` prefix nếu không có protocol
2. Thêm `http://` nếu thiếu (cho IP/localhost)
3. Thêm `https://` nếu thiếu (cho domain)
4. Trim trailing slash

#### 4.3.5. Caching Mechanism

**Mục đích**: Tối ưu performance, tránh resolve lại nhiều lần

**Implementation**:
- Cache URL trong 5 phút
- Clear cache khi cần (ví dụ: sau khi Ngrok khởi động)

### 4.4. StudentCard.cs

#### 4.4.1. GenerateQRCode()

**Mục đích**: Tạo QR code với URL động

**Logic**:
1. Gọi `GetQRCodeUrl()` để lấy URL
2. Tạo QR code với QRCoder library
3. Render QR code lên PictureBox
4. Fallback: Nếu lỗi → tạo QR với text đơn giản

**Tại sao URL động**:
- URL có thể thay đổi (Ngrok URL thay đổi mỗi lần start)
- LAN IP có thể thay đổi
- Cần resolve lại mỗi lần generate QR

#### 4.4.2. GetQRCodeUrl()

**Logic**:
1. Đảm bảo server đang chạy: `StartServer()`
2. Lấy base URL từ `UrlResolver.ResolveBaseUrl()`
3. Tạo full URL: `{baseUrl}/student/{maHocSinh}`
4. Fallback về localhost nếu lỗi

### 4.5. XemChiTietHocSinh.cs

#### 4.5.1. Constructor

**Logic**:
1. Initialize components
2. Khởi tạo BLL/BUS objects
3. **Quan trọng**: `StudentWebServerManager.Instance.StartServer()`
4. Load thông tin học sinh

**Tại sao start server trong constructor**:
- Đảm bảo server chạy trước khi generate QR
- Tự động, không cần user thao tác
- Chỉ start 1 lần (nhờ Singleton)

---

## 5. CƠ CHẾ HOẠT ĐỘNG

### 5.1. Tại sao dùng HttpListener thay vì ASP.NET?

**HttpListener**:
- ✅ Nhẹ, không cần IIS
- ✅ Dễ tích hợp vào WinForms app
- ✅ Đủ cho use case đơn giản (serve HTML)
- ✅ Không cần cấu hình phức tạp

**ASP.NET**:
- ❌ Quá nặng cho use case này
- ❌ Cần IIS hoặc Kestrel
- ❌ Phức tạp hơn nhiều

### 5.2. Tại sao UI và Server chạy song song không block?

**Kiến trúc**:
```
UI Thread (Main)          Background Thread          HTTP Requests
     │                            │                         │
     │ StartServer()              │                         │
     ├───────────────────────────▶│                         │
     │                            │ listener.Start()        │
     │                            │ while (isRunning)       │
     │                            │   context = GetContext()│
     │                            │   Task.Run(Handle...)  │
     │                            │                         │
     │ GenerateQRCode()           │                         │
     │ (Non-blocking)             │                         │
     │                            │                         │
     │ User interacts...          │ HandleRequest()         │
     │                            │ ├─ Query DB             │
     │                            │ ├─ Generate HTML        │
     │                            │ └─ Send Response        │
     │                            │                         │
     │ (UI vẫn responsive)        │                         │
```

**Các kỹ thuật**:
1. **Background Thread**: `serverThread.IsBackground = true`
2. **Async Request Handling**: `Task.Run(() => HandleRequest())`
3. **Non-blocking I/O**: Database queries không block UI thread
4. **Singleton Pattern**: Chỉ 1 server instance, không tạo mới mỗi lần

### 5.3. Tại sao Auto Mode là lựa chọn tối ưu?

**Ưu điểm**:
1. **Tự động adapt**: Tự chọn URL tốt nhất
2. **Fallback thông minh**: Ngrok → LAN IP → Localhost
3. **User-friendly**: Không cần cấu hình phức tạp
4. **Flexible**: Hoạt động trong mọi môi trường

**So sánh với các mode khác**:

| Mode | Ưu điểm | Nhược điểm | Use case |
|------|---------|------------|----------|
| **Auto** | Tự động, linh hoạt | Có thể chậm hơn (resolve) | **Khuyến nghị** |
| Ngrok | Truy cập internet | Cần cấu hình, URL thay đổi | Production với internet |
| Local | Đơn giản, nhanh | Chỉ localhost | Development |
| Custom | Kiểm soát hoàn toàn | Cần cấu hình domain | Production với domain riêng |

### 5.4. Tại sao LAN IP hoạt động nhưng localhost không?

**Localhost (127.0.0.1)**:
- Chỉ có thể truy cập từ chính máy đó
- Điện thoại không thể truy cập vì:
  - `localhost` trên điện thoại = chính điện thoại
  - Không phải máy tính chạy server

**LAN IP (10.212.31.135)**:
- IP address trong mạng LAN
- Điện thoại có thể truy cập vì:
  - Cùng mạng WiFi
  - Router route traffic đến máy tính
  - HttpListener bind với `+` chấp nhận request từ IP

**Network Flow**:
```
Điện thoại (192.168.1.50)          Router          Laptop (10.212.31.135)
      │                                │                    │
      │ GET /student/1                 │                    │
      ├───────────────────────────────▶│                    │
      │                                │                    │
      │                                │ Forward request    │
      │                                ├───────────────────▶│
      │                                │                    │
      │                                │                    │ HttpListener
      │                                │                    │ nhận request
      │                                │                    │
      │                                │ HTML Response      │
      │                                │◀───────────────────┤
      │                                │                    │
      │ HTML Response                  │                    │
      │◀───────────────────────────────┤                    │
      │                                │                    │
      │ Render web page                │                    │
```

### 5.5. Request Pipeline chi tiết

**Từ mở Form → Render HTML**:

```
1. User mở XemChiTietHocSinh(maHocSinh=1)
   │
   ├─ Constructor()
   │  ├─ InitializeComponent()
   │  ├─ Khởi tạo BLL/BUS
   │  └─ StudentWebServerManager.Instance.StartServer()
   │     │
   │     ├─ StudentWebServer.Start()
   │     │  ├─ HttpListener listener = new HttpListener()
   │     │  ├─ listener.Prefixes.Add("http://+:8080/")
   │     │  ├─ listener.Start()
   │     │  ├─ Thread serverThread = new Thread(Listen)
   │     │  ├─ serverThread.Start()  [Background]
   │     │  ├─ TryAddFirewallRule()
   │     │  └─ TryStartNgrokIfNeeded()
   │     │
   │     └─ Server đang chạy
   │
   ├─ LoadThongTinHocSinh()
   │  ├─ hocSinhBLL.GetHocSinhById(1)
   │  ├─ studentCard.LoadStudentInfo(hocSinh, tenLop, tenGVCN)
   │  │  │
   │  │  └─ DisplayStudentInfo()
   │  │     └─ GenerateQRCode()
   │  │        │
   │  │        ├─ GetQRCodeUrl()
   │  │        │  ├─ StudentWebServerManager.Instance.StartServer()
   │  │        │  └─ UrlResolver.ResolveBaseUrl()
   │  │        │     │
   │  │        │     ├─ Check cache (5 min)
   │  │        │     ├─ Mode = "Auto"
   │  │        │     │  ├─ TryGetNgrokUrl() → null
   │  │        │     │  ├─ GetLocalIPAddress() → "10.212.31.135"
   │  │        │     │  └─ Return "http://10.212.31.135:8080"
   │  │        │     │
   │  │        │     └─ Cache URL
   │  │        │
   │  │        └─ QR Code: "http://10.212.31.135:8080/student/1"
   │  │
   │  ├─ LoadThongTinLop()
   │  └─ LoadDanhSachPhuHuynh()
   │
   └─ Form hiển thị với QR code

2. User quét QR code trên điện thoại
   │
   └─ Browser mở: http://10.212.31.135:8080/student/1
      │
      ├─ HTTP GET Request
      │  GET /student/1 HTTP/1.1
      │  Host: 10.212.31.135:8080
      │
      └─ HttpListener nhận request
         │
         ├─ Background Thread: Listen()
         │  context = listener.GetContext()  [Blocking]
         │
         └─ Task.Run(() => HandleRequest(context))
            │
            ├─ HandleRequest()
            │  ├─ Parse path: "/student/1"
            │  ├─ Extract maHocSinh = 1
            │  └─ HandleStudentPage(response, 1)
            │     │
            │     ├─ hocSinhBLL.GetHocSinhById(1)
            │     │  └─ Query MySQL: SELECT * FROM HocSinh WHERE MaHS = 1
            │     │
            │     ├─ GetCurrentSemesterId()
            │     │  └─ Query MySQL: SELECT * FROM HocKy WHERE TrangThai = 'Đang diễn ra'
            │     │
            │     ├─ phanLopBLL.GetLopByHocSinh(1, maHocKy)
            │     │  └─ Query MySQL: SELECT * FROM PhanLop WHERE MaHS = 1 AND MaHocKy = ...
            │     │
            │     ├─ lopHocBUS.LayLopTheoId(maLop)
            │     │  └─ Query MySQL: SELECT * FROM LopHoc WHERE MaLop = ...
            │     │
            │     ├─ hocSinhPhuHuynhBLL.GetPhuHuynhByHocSinh(1)
            │     │  └─ Query MySQL: JOIN HocSinh_PhuHuynh với PhuHuynh
            │     │
            │     └─ GenerateStudentHTML(hocSinh, tenLop, tenGVCN, dsPhuHuynh)
            │        │
            │        ├─ GetStudentAvatarBase64()
            │        │  ├─ File.ReadAllBytes(hocSinh.AnhDaiDien)
            │        │  └─ Convert.ToBase64String()
            │        │
            │        ├─ Format dates: dd/MM/yyyy
            │        │
            │        ├─ Build phuHuynhTable HTML
            │        │  └─ StringBuilder với <tr><td>...
            │        │
            │        └─ String interpolation với template
            │           └─ Complete HTML string
            │
            └─ SendHTML(response, html)
               │
               ├─ response.ContentType = "text/html; charset=utf-8"
               ├─ response.StatusCode = 200
               ├─ byte[] buffer = Encoding.UTF8.GetBytes(html)
               ├─ response.ContentLength64 = buffer.Length
               ├─ response.OutputStream.Write(buffer, 0, buffer.Length)
               └─ response.Close()

3. Browser nhận HTML và render
   │
   └─ Hiển thị trang web với:
      ├─ Header: TRƯỜNG THPT SÀI GÒN
      ├─ Avatar học sinh (base64)
      ├─ Thông tin cá nhân
      ├─ Thông tin lớp học
      └─ Bảng phụ huynh
```

---

## 6. TROUBLESHOOTING

### 6.1. Lỗi "Bad Request - Invalid Hostname"

**Nguyên nhân**: HttpListener chỉ bind với `localhost`, không chấp nhận request từ IP

**Giải pháp**:
- Đảm bảo bind với `http://+:{port}/` (đã implement)
- Nếu vẫn lỗi, chạy lệnh:
  ```powershell
  netsh http add urlacl url=http://+:8080/ user=Everyone
  ```
- Hoặc chạy ứng dụng với quyền Admin

### 6.2. Điện thoại không truy cập được LAN IP

**Nguyên nhân**:
1. Windows Firewall chặn
2. Router chặn inter-device communication
3. IP address thay đổi

**Giải pháp**:
1. **Firewall**: 
   - Server tự động thử thêm rule
   - Hoặc thủ công: Windows Firewall → Inbound Rules → New Rule → Port 8080
2. **Router**: 
   - Kiểm tra AP Isolation (tắt nếu có)
   - Đảm bảo cùng subnet
3. **IP thay đổi**: 
   - Dùng static IP hoặc
   - Dùng Ngrok để có URL cố định

### 6.3. Ngrok không hoạt động

**Nguyên nhân**:
- Ngrok chưa được cài đặt
- Ngrok API (port 4040) chưa sẵn sàng
- Auth token không đúng

**Giải pháp**:
- Kiểm tra `NgrokPath` trong App.config
- Đảm bảo Ngrok đang chạy: `ngrok http 8080`
- Kiểm tra API: `curl http://127.0.0.1:4040/api/tunnels`
- Auto Mode sẽ tự động fallback về LAN IP nếu Ngrok không có

### 6.4. QR code hiển thị URL sai

**Nguyên nhân**:
- Cache URL cũ
- URL resolution thất bại

**Giải pháp**:
- Clear cache: `UrlResolver.ClearCache()`
- Restart ứng dụng
- Kiểm tra Debug Output để xem URL được resolve

### 6.5. Server không khởi động

**Nguyên nhân**:
- Port đã được sử dụng
- Thiếu quyền admin
- Firewall chặn

**Giải pháp**:
- Kiểm tra port: `netstat -ano | findstr :8080`
- Đổi port trong App.config
- Chạy với quyền Admin
- Kiểm tra Windows Firewall logs

### 6.6. HTML không hiển thị đúng trên mobile

**Nguyên nhân**:
- CSS không responsive
- Viewport meta tag thiếu

**Giải pháp**:
- Đã implement responsive CSS
- Đã có `<meta name="viewport" content="width=device-width, initial-scale=1.0">`
- Test trên nhiều device sizes

### 6.7. Database connection timeout

**Nguyên nhân**:
- MySQL server không chạy
- Connection string sai
- Network issue

**Giải pháp**:
- Kiểm tra MySQL service
- Kiểm tra connection string
- Test connection trước khi start server

---

## 7. FUTURE UPGRADE

### 7.1. SSL/HTTPS Support

**Mục tiêu**: Bảo mật kết nối với HTTPS

**Implementation**:
- Sử dụng `HttpListener` với certificate
- Hoặc deploy lên server có SSL (IIS, Nginx)
- Hoặc dùng Let's Encrypt cho domain riêng

**Code changes**:
```csharp
listener.Prefixes.Add($"https://+:{port}/");
// Cần certificate và quyền admin
```

### 7.2. Domain riêng và Cloud Deployment

**Mục tiêu**: Truy cập từ internet không cần Ngrok

**Options**:
1. **Azure App Service**: Deploy web app, có domain miễn phí
2. **AWS EC2**: VPS với domain riêng
3. **Heroku**: Platform as a Service
4. **DigitalOcean**: VPS đơn giản

**Migration path**:
- Tách web server thành separate project (ASP.NET Core)
- Deploy lên cloud
- Update QR code URL → domain mới

### 7.3. API Endpoints

**Mục tiêu**: Cung cấp REST API cho mobile app

**Endpoints đề xuất**:
```
GET /api/student/{id}           - Thông tin học sinh
GET /api/student/{id}/parents   - Danh sách phụ huynh
GET /api/student/{id}/class     - Thông tin lớp
GET /api/student/{id}/grades    - Điểm số
POST /api/student/{id}/contact  - Liên hệ phụ huynh
```

**Implementation**:
- Thêm routing cho `/api/*`
- Parse JSON request/response
- Authentication/Authorization
- Rate limiting

### 7.4. Caching và Performance

**Mục tiêu**: Tối ưu performance khi có nhiều requests

**Strategies**:
1. **Response caching**: Cache HTML cho mỗi học sinh (5 phút)
2. **Database connection pooling**: Tái sử dụng connections
3. **CDN**: Serve static assets từ CDN
4. **Compression**: Gzip compression cho HTML

**Implementation**:
```csharp
private static Dictionary<int, (string html, DateTime cached)> _htmlCache;

private string GetCachedHTML(int maHocSinh)
{
    if (_htmlCache.ContainsKey(maHocSinh))
    {
        var cached = _htmlCache[maHocSinh];
        if (DateTime.Now - cached.cached < TimeSpan.FromMinutes(5))
            return cached.html;
    }
    // Generate new HTML
    string html = GenerateStudentHTML(...);
    _htmlCache[maHocSinh] = (html, DateTime.Now);
    return html;
}
```

### 7.5. Authentication và Authorization

**Mục tiêu**: Bảo mật thông tin học sinh

**Options**:
1. **Token-based**: JWT tokens trong QR code
2. **Time-limited**: QR code chỉ valid trong thời gian nhất định
3. **Role-based**: Phụ huynh chỉ xem được con mình

**Implementation**:
```csharp
// QR code: http://domain.com/student/1?token=xxx
// Validate token trong HandleStudentPage()
if (!ValidateToken(token, maHocSinh))
    return 403 Forbidden;
```

### 7.6. Real-time Updates

**Mục tiêu**: Cập nhật thông tin real-time

**Options**:
- **SignalR**: WebSocket cho real-time
- **Server-Sent Events**: Push updates từ server
- **Polling**: Client poll server mỗi X giây

### 7.7. Analytics và Logging

**Mục tiêu**: Theo dõi usage và debug

**Features**:
- Log mọi request (IP, time, path, response code)
- Analytics: số lượt quét QR, học sinh được xem nhiều nhất
- Error tracking: Sentry, Application Insights

### 7.8. Multi-language Support

**Mục tiêu**: Hỗ trợ nhiều ngôn ngữ

**Implementation**:
- Resource files cho translations
- Detect language từ browser
- URL parameter: `/student/1?lang=en`

### 7.9. Offline Support (PWA)

**Mục tiêu**: Hoạt động offline với Service Worker

**Features**:
- Cache HTML và assets
- Offline-first approach
- Sync khi online lại

### 7.10. Migration sang ASP.NET Core

**Mục tiêu**: Modern, cross-platform, better performance

**Benefits**:
- Cross-platform (Linux, macOS, Windows)
- Better performance
- Built-in dependency injection
- Middleware pipeline
- Better async/await support

**Migration steps**:
1. Tạo ASP.NET Core Web API project
2. Port routing logic
3. Port HTML generation
4. Deploy lên cloud
5. Update QR code URLs

---

## 8. KẾT LUẬN

Hệ thống QR Code Web Server đã được thiết kế và triển khai với các nguyên tắc:

1. **Simplicity**: Đơn giản, dễ maintain
2. **Flexibility**: Auto Mode adapt với mọi môi trường
3. **Performance**: Non-blocking, async, caching
4. **Reliability**: Fallback mechanisms, error handling
5. **Scalability**: Dễ mở rộng với các tính năng mới

Hệ thống hiện tại đáp ứng đầy đủ yêu cầu ban đầu và có thể mở rộng theo các hướng đã đề xuất trong phần Future Upgrade.

---

**Tài liệu này được tạo tự động bởi hệ thống báo cáo kỹ thuật**
**Version**: 1.0.0
**Date**: 2025
**Author**: Student Management System - SGU 2025 Team

