using System;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
    /// <summary>
    /// Kết quả của thao tác di chuyển/chỉnh sửa thời khóa biểu
    /// </summary>
    public class MoveResult
    {
        /// <summary>
        /// True nếu thao tác thành công, False nếu có lỗi
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Thông báo lỗi hoặc thành công
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Tạo kết quả thành công
        /// </summary>
        public static MoveResult Success(string message = "Thao tác thành công!")
        {
            return new MoveResult
            {
                IsSuccess = true,
                Message = message
            };
        }

        /// <summary>
        /// Tạo kết quả thất bại
        /// </summary>
        public static MoveResult Fail(string message)
        {
            return new MoveResult
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}

