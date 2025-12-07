# Hướng dẫn sử dụng Web Server cho QR Code

## Tổng quan

Hệ thống QR Code đã được nâng cấp để hiển thị trang web chi tiết thông tin học sinh khi quét bằng điện thoại. Web server tự động khởi động khi mở form `XemChiTietHocSinh`.

## Cấu hình

### 1. Chế độ Local (Mặc định)
- Chỉ truy cập được trong cùng mạng LAN
- URL: `http://localhost:8080/student/{maHocSinh}`
- Không cần cấu hình thêm

### 2. Chế độ Ngrok (Truy cập từ Internet)
Để quét QR từ điện thoại khác mạng WiFi:

**Bước 1:** Tải Ngrok từ https://ngrok.com/download

**Bước 2:** Cấu hình trong `App.config`:
```xml
<add key="WebServerMode" value="Ngrok" />
<add key="NgrokPath" value="C:\ngrok\ngrok.exe" />
<!-- Optional: Để có URL cố định -->
<add key="NgrokAuthToken" value="your-ngrok-auth-token" />
```

**Bước 3:** Khởi động lại ứng dụng

**Lưu ý:** 
- URL Ngrok sẽ thay đổi mỗi lần khởi động (trừ khi dùng auth token)
- Ngrok miễn phí có giới hạn số lượng request

### 3. Chế độ Custom (URL tùy chỉnh)
Nếu bạn có server riêng hoặc domain:

```xml
<add key="WebServerMode" value="Custom" />
<add key="CustomPublicUrl" value="https://yourdomain.com" />
```

### 4. Chế độ Auto
Tự động chọn giữa Local và Ngrok (ưu tiên Ngrok nếu có):

```xml
<add key="WebServerMode" value="Auto" />
```

## Cấu hình Port

Mặc định port là 8080. Để thay đổi:

```xml
<add key="WebServerPort" value="8080" />
```

**Lưu ý:** Nếu port đã được sử dụng, server sẽ không khởi động được.

## Kiểm tra hoạt động

1. Mở form `XemChiTietHocSinh` cho một học sinh
2. Quét QR code trên thẻ học sinh
3. Trình duyệt sẽ mở trang web hiển thị đầy đủ thông tin:
   - Thông tin cá nhân
   - Thông tin lớp học
   - Thông tin phụ huynh

## Troubleshooting

### Server không khởi động
- Kiểm tra port có đang được sử dụng không
- Kiểm tra quyền admin (cần quyền để bind port)
- Xem log trong Debug Output

### Ngrok không hoạt động
- Kiểm tra đường dẫn đến `ngrok.exe` đúng chưa
- Đảm bảo Ngrok đã được cài đặt và có thể chạy từ command line
- Kiểm tra firewall không chặn Ngrok

### QR code không mở được trang web
- Kiểm tra web server có đang chạy không (xem Debug Output)
- Kiểm tra URL trong QR code có đúng không
- Thử truy cập URL trực tiếp trên trình duyệt

## Bảo mật

⚠️ **Lưu ý quan trọng:**
- Web server hiện tại chỉ phục vụ thông tin công khai
- Không có authentication/authorization
- Chỉ nên sử dụng trong môi trường nội bộ hoặc với Ngrok có password protection
- Không nên expose trực tiếp ra internet mà không có bảo mật

## Tùy chỉnh giao diện

HTML template được tạo trong `StudentWebServer.cs`, method `GenerateStudentHTML()`. 
Bạn có thể chỉnh sửa CSS và HTML để phù hợp với thiết kế của trường.

