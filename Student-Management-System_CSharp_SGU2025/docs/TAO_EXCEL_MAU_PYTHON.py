# -*- coding: utf-8 -*-
"""
Script Python để tạo file Excel mẫu cho chức năng PHÂN LỚP CHUYỂN TRƯỜNG
File Excel này sẽ có 6 worksheet: HocSinh, PhuHuynh, MoiQuanHe, Diem, HanhKiem, XepLoai

⚠️ QUAN TRỌNG: 
- Dữ liệu điểm, hạnh kiểm, xếp loại CHỈ DÙNG ĐỂ KIỂM TRA ĐIỀU KIỆN, KHÔNG LƯU VÀO DATABASE!
- Bạn PHẢI kiểm tra database để xác định đúng Mã học kỳ cần thiết theo logic dưới đây
"""

import pandas as pd
from datetime import datetime

# =====================================================================
# LOGIC KIỂM TRA ĐIỂM, HẠNH KIỂM, XẾP LOẠI THEO KHỐI
# =====================================================================
#
# 📌 KHỐI 10:
#   - Nếu học kỳ đang diễn ra là HK1 → KHÔNG cần check điểm nào
#   - Nếu học kỳ đang diễn ra là HK2 → Cần check HK1 của năm học hiện tại
#
# 📌 KHỐI 11:
#   - Tương tự khối 10 (check HK1 nếu HK2 đang diễn ra)
#   - + Cần check 2 học kỳ của năm học trước (HK1, HK2 của khối 10)
#
# 📌 KHỐI 12:
#   - Tương tự khối 10 (check HK1 nếu HK2 đang diễn ra)
#   - + Cần check 4 học kỳ của 2 năm học trước:
#     * 2 học kỳ của năm học trước (HK1, HK2 của khối 11)
#     * 2 học kỳ của năm học trước nữa (HK1, HK2 của khối 10)
#
# VÍ DỤ: Giả sử học kỳ hiện tại là HK1 năm học 2025-2026 (MaHocKy=3)
# - Khối 10: KHÔNG cần check điểm nào (vì HK1 đang diễn ra)
# - Khối 11: Cần check HK1, HK2 của năm học 2024-2025 (MaHocKy=1, 2)
# - Khối 12: Cần check HK1, HK2 của năm học 2024-2025 (MaHocKy=1, 2) + HK1, HK2 của năm học 2023-2024
#
# VÍ DỤ: Giả sử học kỳ hiện tại là HK2 năm học 2025-2026 (MaHocKy=4)
# - Khối 10: Cần check HK1 của năm học 2025-2026 (MaHocKy=3)
# - Khối 11: Cần check HK1 của năm học 2025-2026 (MaHocKy=3) + HK1, HK2 của năm học 2024-2025 (MaHocKy=1, 2)
# - Khối 12: Cần check HK1 của năm học 2025-2026 (MaHocKy=3) + HK1, HK2 của năm học 2024-2025 (MaHocKy=1, 2) + HK1, HK2 của năm học 2023-2024
#
# ⚠️ BẠN PHẢI KIỂM TRA DATABASE ĐỂ XÁC ĐỊNH ĐÚNG MÃ HỌC KỲ CẦN THIẾT!
# =====================================================================

# 1. Dữ liệu mẫu cho Worksheet "HocSinh"
# Lưu ý: KHÔNG có cột "Mã HS" vì MaHocSinh là AUTO_INCREMENT
data_hocsinh = {
    "Họ và tên": ["Nguyễn Văn A", "Trần Thị B", "Lê Văn C", "Phạm Thị D", "Hoàng Văn E"],
    "Ngày sinh": ["15/05/2008", "20/10/2008", "05/01/2008", "12/12/2008", "08/03/2008"],
    "Giới tính": ["Nam", "Nữ", "Nam", "Nữ", "Nam"],  # Phải là "Nam" hoặc "Nữ"
    "SĐT": ["0901234561", "0901234562", "0901234563", "0901234564", "0901234565"],  # Có thể để trống
    "Email": ["hs1@school.edu.vn", "hs2@school.edu.vn", "hs3@school.edu.vn", "hs4@school.edu.vn", "hs5@school.edu.vn"],  # Có thể để trống, phải unique
    "Trạng thái": ["", "", "", "", ""],  # Có thể để trống, hệ thống sẽ tự động đặt "Đang học(CT)"
    "Khối": ["10", "10", "11", "11", "12"],  # BẮT BUỘC, phải là 10, 11, hoặc 12
    "Ngày chuyển vào": ["01/09/2025", "05/09/2025", "01/09/2025", "10/09/2025", "01/09/2025"],  # BẮT BUỘC, phải TRƯỚC 1/3 học kỳ
    "Nguyện vọng chuyển lớp": ["10A1", "10A2", "11A1", "", "12A1"]  # Có thể để trống, nếu có phải cùng khối (theo seed: 10A1-10A8, 11A1-11A8, 12A1-12A8)
}

# 2. Dữ liệu mẫu cho Worksheet "Diem"
# ⚠️ QUAN TRỌNG: Dữ liệu này CHỈ DÙNG ĐỂ KIỂM TRA ĐIỀU KIỆN, KHÔNG LƯU VÀO DATABASE!
#
# Mã môn học phải khớp với database (13 môn):
#   1=Ngữ văn, 2=Toán, 3=Tiếng Anh, 4=Lịch sử, 5=Địa lý, 6=GD Kinh tế & Pháp luật,
#   7=Vật lý, 8=Hóa học, 9=Sinh học, 10=Công nghệ, 11=Tin học, 12=Giáo dục thể chất, 13=GDQP-AN
#
# ⚠️ LƯU Ý: File Excel này chỉ là mẫu, bạn cần điều chỉnh theo khối và học kỳ thực tế!
# Ví dụ mẫu: Giả sử học kỳ hiện tại là HK1 năm 2025-2026
# - HS1, HS2 (Khối 10): 0 học kỳ (không cần check vì HK1 đang diễn ra)
# - HS3, HS4 (Khối 11): 2 học kỳ (HK1, HK2 năm 2024-2025) = 26 dòng/HS
# - HS5 (Khối 12): 4 học kỳ (HK1, HK2 năm 2024-2025 + HK1, HK2 năm 2023-2024) = 52 dòng/HS
#
# ⚠️ BẠN PHẢI KIỂM TRA DATABASE VÀ ĐIỀU CHỈNH MÃ HỌC KỲ THEO LOGIC Ở TRÊN!
# 
# ✅ DỮ LIỆU ĐẦY ĐỦ CHO 5 HỌC SINH:
# - Nguyễn Văn A (Khối 10): Không cần điểm (HK1 đang diễn ra)
# - Trần Thị B (Khối 10): Không cần điểm (HK1 đang diễn ra)
# - Lê Văn C (Khối 11): Cần HK1, HK2 năm 2024-2025 = 26 dòng (13 môn × 2 học kỳ)
# - Phạm Thị D (Khối 11): Cần HK1, HK2 năm 2024-2025 = 26 dòng (13 môn × 2 học kỳ)
# - Hoàng Văn E (Khối 12): Cần HK1, HK2 năm 2024-2025 + HK1, HK2 năm 2023-2024 = 52 dòng (13 môn × 4 học kỳ)
data_diem = {
    # Thứ tự: Tất cả môn của Học kỳ I trước, sau đó mới đến tất cả môn của Học kỳ II
    # Lê Văn C (khối 11): 13 môn HK1 (2024-2025) + 13 môn HK2 (2024-2025) = 26 dòng
    # Phạm Thị D (khối 11): 13 môn HK1 (2024-2025) + 13 môn HK2 (2024-2025) = 26 dòng
    # Hoàng Văn E (khối 12): 13 môn HK1 (2024-2025) + 13 môn HK2 (2024-2025) + 13 môn HK1 (2023-2024) + 13 môn HK2 (2023-2024) = 52 dòng
    "Họ và tên": (["Lê Văn C"]*26 + ["Phạm Thị D"]*26 + ["Hoàng Văn E"]*52),
    "Tên học kỳ": (["Học kỳ I"]*13 + ["Học kỳ II"]*13)*2 + (["Học kỳ I"]*13 + ["Học kỳ II"]*13 + ["Học kỳ I"]*13 + ["Học kỳ II"]*13),  # 2 HS khối 11: 2 học kỳ, 1 HS khối 12: 4 học kỳ
    "Năm học": (["2024-2025"]*13 + ["2024-2025"]*13)*2 + (["2024-2025"]*13 + ["2024-2025"]*13 + ["2023-2024"]*13 + ["2023-2024"]*13),  # Khối 11: 2024-2025, Khối 12: 2024-2025 + 2023-2024
    "Mã môn học": ([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]*2)*2 + ([1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]*4),  # 2 HS khối 11: 2 học kỳ, 1 HS khối 12: 4 học kỳ
    "Tên môn học": (["Ngữ văn", "Toán", "Tiếng Anh", "Lịch sử", "Địa lý", "GD Kinh tế & Pháp luật",
                     "Vật lý", "Hóa học", "Sinh học", "Công nghệ", "Tin học", "Giáo dục thể chất", "GDQP-AN"]*2*2 + 
                    ["Ngữ văn", "Toán", "Tiếng Anh", "Lịch sử", "Địa lý", "GD Kinh tế & Pháp luật",
                     "Vật lý", "Hóa học", "Sinh học", "Công nghệ", "Tin học", "Giáo dục thể chất", "GDQP-AN"]*4),
    # Mỗi học sinh có điểm cho các học kỳ cần thiết
    # Thứ tự: 13 môn HK1 + 13 môn HK2 (cho 2 HS khối 11) + 13 môn HK1 (2024-2025) + 13 môn HK2 (2024-2025) + 13 môn HK1 (2023-2024) + 13 môn HK2 (2023-2024) (cho HS khối 12)
    "Điểm thường xuyên": ([8.0, 7.5, 9.0, 7.0, 7.5, 8.0, 8.5, 8.0, 7.5, 8.0, 8.5, 9.0, 8.5]*2*2 + [8.0, 7.5, 9.0, 7.0, 7.5, 8.0, 8.5, 8.0, 7.5, 8.0, 8.5, 9.0, 8.5]*4),
    "Điểm giữa kỳ": ([8.5, 7.0, 8.5, 7.5, 8.0, 8.5, 9.0, 8.5, 8.0, 8.5, 9.0, 9.5, 9.0]*2*2 + [8.5, 7.0, 8.5, 7.5, 8.0, 8.5, 9.0, 8.5, 8.0, 8.5, 9.0, 9.5, 9.0]*4),
    "Điểm cuối kỳ": ([9.0, 8.0, 9.0, 8.0, 8.5, 9.0, 9.5, 9.0, 8.5, 9.0, 9.5, 10.0, 9.5]*2*2 + [9.0, 8.0, 9.0, 8.0, 8.5, 9.0, 9.5, 9.0, 8.5, 9.0, 9.5, 10.0, 9.5]*4),
    "Điểm trung bình": ([8.6, 7.6, 8.9, 7.6, 8.0, 8.6, 9.1, 8.6, 8.0, 8.6, 9.1, 9.6, 9.1]*2*2 + [8.6, 7.6, 8.9, 7.6, 8.0, 8.6, 9.1, 8.6, 8.0, 8.6, 9.1, 9.6, 9.1]*4)
}

# 3. Dữ liệu mẫu cho Worksheet "HanhKiem"
# ⚠️ QUAN TRỌNG: Dữ liệu này CHỈ DÙNG ĐỂ KIỂM TRA ĐIỀU KIỆN, KHÔNG LƯU VÀO DATABASE!
# - Xếp loại: "Tốt", "Khá", "Trung bình", "Yếu" (theo schema)
# ✅ DỮ LIỆU ĐẦY ĐỦ CHO 5 HỌC SINH:
# - Nguyễn Văn A, Trần Thị B (Khối 10): Không cần hạnh kiểm (HK1 đang diễn ra)
# - Lê Văn C (Khối 11): Cần HK1, HK2 năm 2024-2025 = 2 dòng
# - Phạm Thị D (Khối 11): Cần HK1, HK2 năm 2024-2025 = 2 dòng
# - Hoàng Văn E (Khối 12): Cần HK1, HK2 năm 2024-2025 + HK1, HK2 năm 2023-2024 = 4 dòng
data_hanhkiem = {
    # Thứ tự: Học kỳ I trước, sau đó mới đến Học kỳ II
    "Họ và tên": (["Lê Văn C"]*2 + ["Phạm Thị D"]*2 + ["Hoàng Văn E"]*4),
    "Tên học kỳ": (["Học kỳ I", "Học kỳ II"]*2 + ["Học kỳ I", "Học kỳ II", "Học kỳ I", "Học kỳ II"]),  # 2 HS khối 11: 2 học kỳ, 1 HS khối 12: 4 học kỳ
    "Năm học": (["2024-2025", "2024-2025"]*2 + ["2024-2025", "2024-2025", "2023-2024", "2023-2024"]),  # Khối 11: 2024-2025, Khối 12: 2024-2025 + 2023-2024
    "Xếp loại": (["Tốt", "Tốt"]*2 + ["Tốt", "Tốt", "Tốt", "Tốt"]),  # Tất cả đều "Tốt"
    "Nhận xét": (["Ngoan, lễ phép"]*2 + ["Cần cố gắng hơn"]*2 + ["Gương mẫu"]*4)
}

# 4. Dữ liệu mẫu cho Worksheet "XepLoai"
# ⚠️ QUAN TRỌNG: Dữ liệu này CHỈ DÙNG ĐỂ KIỂM TRA ĐIỀU KIỆN, KHÔNG LƯU VÀO DATABASE!
# - Học lực: "Giỏi", "Khá", "Trung bình", "Yếu", "Kém" (theo schema)
# - ⚠️ ĐIỀU KIỆN BẮT BUỘC: Học lực KHÔNG ĐƯỢC là "Yếu" hoặc "Kém" ở bất kỳ học kỳ nào
# ✅ DỮ LIỆU ĐẦY ĐỦ CHO 5 HỌC SINH:
# - Nguyễn Văn A, Trần Thị B (Khối 10): Không cần xếp loại (HK1 đang diễn ra)
# - Lê Văn C (Khối 11): Cần HK1, HK2 năm 2024-2025 = 2 dòng
# - Phạm Thị D (Khối 11): Cần HK1, HK2 năm 2024-2025 = 2 dòng
# - Hoàng Văn E (Khối 12): Cần HK1, HK2 năm 2024-2025 + HK1, HK2 năm 2023-2024 = 4 dòng
data_xeploai = {
    # Thứ tự: Học kỳ I trước, sau đó mới đến Học kỳ II
    "Họ và tên": (["Lê Văn C"]*2 + ["Phạm Thị D"]*2 + ["Hoàng Văn E"]*4),
    "Tên học kỳ": (["Học kỳ I", "Học kỳ II"]*2 + ["Học kỳ I", "Học kỳ II", "Học kỳ I", "Học kỳ II"]),  # 2 HS khối 11: 2 học kỳ, 1 HS khối 12: 4 học kỳ
    "Năm học": (["2024-2025", "2024-2025"]*2 + ["2024-2025", "2024-2025", "2023-2024", "2023-2024"]),  # Khối 11: 2024-2025, Khối 12: 2024-2025 + 2023-2024
    "Học lực": (["Giỏi", "Giỏi"]*2 + ["Giỏi", "Giỏi", "Giỏi", "Giỏi"]),  # ✅ KHÔNG có "Yếu" hoặc "Kém"
    "Ghi chú": ([""]*2 + [""]*2 + ["Học bổng"]*4)
}

# 5. Dữ liệu mẫu cho Worksheet "PhuHuynh"
# Lưu ý: KHÔNG có cột "Mã PH" vì MaPhuHuynh là AUTO_INCREMENT
data_phuhuynh = {
    "Họ và tên": ["Nguyễn Văn B", "Trần Thị C", "Lê Văn D", "Phạm Thị E", "Hoàng Văn F"],
    "SĐT": ["0912345671", "0912345672", "0912345673", "0912345674", "0912345675"],  # BẮT BUỘC, không trùng
    "Email": ["ph1@school.edu.vn", "ph2@school.edu.vn", "ph3@school.edu.vn", "ph4@school.edu.vn", "ph5@school.edu.vn"],  # Có thể để trống, không trùng
    "Địa chỉ": ["123 Đường ABC, Quận 1, TP.HCM", "456 Đường XYZ, Quận 2, TP.HCM", "789 Đường DEF, Quận 3, TP.HCM", "321 Đường GHI, Quận 4, TP.HCM", "654 Đường JKL, Quận 5, TP.HCM"]  # BẮT BUỘC: Không được để trống
}

# 6. Dữ liệu mẫu cho Worksheet "MoiQuanHe"
# Lưu ý:
# - Họ và tên học sinh và phụ huynh phải khớp chính xác với worksheet "HocSinh" và "PhuHuynh"
# - Mối quan hệ: "Cha", "Mẹ", "Ông", "Bà", "Người giám hộ"
# - ⚠️ XỬ LÝ TRÙNG TÊN:
#   + Hệ thống sẽ ưu tiên match theo dòng Excel (học sinh dòng 2 → phụ huynh dòng 2 → mối quan hệ dòng 2)
#   + Nếu không match theo dòng, sẽ match theo tên
#   + Nếu có nhiều học sinh/phụ huynh trùng tên, hệ thống sẽ chọn người đầu tiên và hiển thị cảnh báo
#   + Để tránh nhầm lẫn, nên đảm bảo mỗi học sinh/phụ huynh ở cùng dòng trong các worksheet
data_moiquanhe = {
    "Họ và tên học sinh": ["Nguyễn Văn A", "Trần Thị B", "Lê Văn C", "Phạm Thị D", "Hoàng Văn E"],
    "Họ và tên phụ huynh": ["Nguyễn Văn B", "Trần Thị C", "Lê Văn D", "Phạm Thị E", "Hoàng Văn F"],
    "Mối quan hệ": ["Cha", "Mẹ", "Cha", "Mẹ", "Cha"]
}

# =====================================================================
# TẠO FILE EXCEL
# =====================================================================

# Tạo DataFrame cho từng worksheet
df_hocsinh = pd.DataFrame(data_hocsinh)
df_phuhuynh = pd.DataFrame(data_phuhuynh)
df_moiquanhe = pd.DataFrame(data_moiquanhe)
df_diem = pd.DataFrame(data_diem)
df_hanhkiem = pd.DataFrame(data_hanhkiem)
df_xeploai = pd.DataFrame(data_xeploai)

# Tạo file Excel với nhiều worksheet
output_file = "Mau_Excel_PhanLop_ChuyenTruong.xlsx"
with pd.ExcelWriter(output_file, engine='openpyxl') as writer:
    df_hocsinh.to_excel(writer, sheet_name='HocSinh', index=False)
    df_phuhuynh.to_excel(writer, sheet_name='PhuHuynh', index=False)
    df_moiquanhe.to_excel(writer, sheet_name='MoiQuanHe', index=False)
    df_diem.to_excel(writer, sheet_name='Diem', index=False)
    df_hanhkiem.to_excel(writer, sheet_name='HanhKiem', index=False)
    df_xeploai.to_excel(writer, sheet_name='XepLoai', index=False)

print(f"✅ Đã tạo file Excel: {output_file}")
print(f"📊 Số worksheet: 6 (HocSinh, PhuHuynh, MoiQuanHe, Diem, HanhKiem, XepLoai)")
print(f"📝 Số học sinh: {len(data_hocsinh['Họ và tên'])}")
print(f"📝 Số dòng điểm: {len(data_diem['Họ và tên'])} (Lê Văn C: 26, Phạm Thị D: 26, Hoàng Văn E: 52)")
print(f"📝 Số dòng hạnh kiểm: {len(data_hanhkiem['Họ và tên'])} (Lê Văn C: 2, Phạm Thị D: 2, Hoàng Văn E: 4)")
print(f"📝 Số dòng xếp loại: {len(data_xeploai['Họ và tên'])} (Lê Văn C: 2, Phạm Thị D: 2, Hoàng Văn E: 4)")
print(f"")
print(f"📌 LƯU Ý:")
print(f"   - Nguyễn Văn A và Trần Thị B (Khối 10) KHÔNG cần điểm/hạnh kiểm/xếp loại (HK1 đang diễn ra)")
print(f"   - Lê Văn C và Phạm Thị D (Khối 11) cần HK1, HK2 năm 2024-2025")
print(f"   - Hoàng Văn E (Khối 12) cần HK1, HK2 năm 2024-2025 + HK1, HK2 năm 2023-2024")
print(f"⚠️ Bạn PHẢI kiểm tra database và điều chỉnh Tên học kỳ và Năm học trong Excel theo logic ở trên!")
