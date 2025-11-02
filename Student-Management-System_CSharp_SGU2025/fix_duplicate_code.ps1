# Script tự động xóa code duplicate trong PhanLopTuDongBLL.cs

$filePath = "bus\PhanLopTuDongBLL.cs"

Write-Host "Reading file..." -ForegroundColor Yellow
$content = Get-Content $filePath -Raw

# Tìm vị trí bắt đầu và kết thúc của đoạn code cần xóa
$startMarker = "        // ===== CODE CŨ BẮT ĐẦU TỪ ĐÂY (GIỮ NGUYÊN) ====="
$endMarker = "        // Hàm helper phân bổ học sinh vào lớp"

# Tách nội dung thành các dòng
$lines = $content -split "`r?`n"

Write-Host "Total lines: $($lines.Count)" -ForegroundColor Cyan

# Tìm các index
$startIndex = -1
$endIndex1 = -1
$endIndex2 = -1

for ($i = 0; $i -lt $lines.Count; $i++) {
    if ($lines[$i] -eq $startMarker) {
        $startIndex = $i
        Write-Host "Found START marker at line $($i + 1)" -ForegroundColor Green
    }
    if ($lines[$i] -eq $endMarker) {
        if ($endIndex1 -eq -1) {
            $endIndex1 = $i
            Write-Host "Found FIRST END marker at line $($i + 1)" -ForegroundColor Green
        } else {
            $endIndex2 = $i
            Write-Host "Found SECOND END marker at line $($i + 1)" -ForegroundColor Green
        }
    }
}

if ($startIndex -eq -1 -or $endIndex2 -eq -1) {
    Write-Host "ERROR: Could not find markers!" -ForegroundColor Red
    Write-Host "START: $startIndex, END1: $endIndex1, END2: $endIndex2"
    exit 1
}

# Xóa từ startIndex đến endIndex1 (giữ endIndex2 và sau đó)
$newLines = @()
$newLines += $lines[0..($startIndex - 1)]  # Giữ phần trước
$newLines += ""
$newLines += "        // ===== CÁC HÀM HELPER CŨ (GIỮ NGUYÊN) ====="
$newLines += ""
$newLines += $lines[$endIndex2..$($lines.Count - 1)]  # Giữ phần sau

# Ghi lại file
$newContent = $newLines -join "`r`n"
Set-Content -Path $filePath -Value $newContent -Encoding UTF8

Write-Host "`n✅ SUCCESS! Removed lines $($startIndex + 1) to $($endIndex1 + 1)" -ForegroundColor Green
Write-Host "📝 New file has $($newLines.Count) lines (was $($lines.Count))" -ForegroundColor Cyan
Write-Host "`n🔧 Please rebuild the project now!" -ForegroundColor Yellow
