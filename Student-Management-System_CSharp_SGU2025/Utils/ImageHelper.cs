using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.Utils
{
    /// <summary>
    /// Helper class để xử lý ảnh đại diện học sinh
    /// </summary>
    public static class ImageHelper
    {
        // Đường dẫn folder chứa ảnh học sinh
        private static readonly string STUDENT_IMAGES_FOLDER = Path.Combine(Application.StartupPath, "Images", "Students");
        private static readonly string DEFAULT_AVATAR = Path.Combine(STUDENT_IMAGES_FOLDER, "default-avatar.png");
        
        // Kích thước tối đa cho ảnh (width x height)
        private const int MAX_WIDTH = 800;
        private const int MAX_HEIGHT = 800;
        
        // Kích thước file tối đa (5MB)
        private const long MAX_FILE_SIZE = 5 * 1024 * 1024;

        /// <summary>
        /// Khởi tạo folder chứa ảnh nếu chưa tồn tại
        /// </summary>
        public static void InitializeImageFolder()
        {
            try
            {
                if (!Directory.Exists(STUDENT_IMAGES_FOLDER))
                {
                    Directory.CreateDirectory(STUDENT_IMAGES_FOLDER);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo folder ảnh: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lưu ảnh đại diện cho học sinh
        /// </summary>
        /// <param name="sourceImagePath">Đường dẫn ảnh gốc</param>
        /// <param name="maHocSinh">Mã học sinh</param>
        /// <returns>Đường dẫn tương đối của ảnh đã lưu, null nếu thất bại</returns>
        public static string SaveStudentAvatar(string sourceImagePath, int maHocSinh)
        {
            try
            {
                // Kiểm tra file tồn tại
                if (!File.Exists(sourceImagePath))
                {
                    MessageBox.Show("File ảnh không tồn tại!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                // Kiểm tra kích thước file
                FileInfo fileInfo = new FileInfo(sourceImagePath);
                if (fileInfo.Length > MAX_FILE_SIZE)
                {
                    MessageBox.Show($"Kích thước file quá lớn! Tối đa {MAX_FILE_SIZE / (1024 * 1024)}MB", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                // Tạo tên file mới
                string extension = Path.GetExtension(sourceImagePath).ToLower();
                string fileName = $"HS_{maHocSinh}{extension}";
                string fullPath = Path.Combine(STUDENT_IMAGES_FOLDER, fileName);

                // Load và resize ảnh nếu cần
                using (Image originalImage = Image.FromFile(sourceImagePath))
                {
                    Image resizedImage = ResizeImage(originalImage, MAX_WIDTH, MAX_HEIGHT);
                    
                    // Lưu ảnh
                    resizedImage.Save(fullPath, GetImageFormat(extension));
                    resizedImage.Dispose();
                }

                // Trả về đường dẫn tương đối
                return Path.Combine("Images", "Students", fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu ảnh: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Xóa ảnh đại diện cũ của học sinh
        /// </summary>
        /// <param name="relativePath">Đường dẫn tương đối của ảnh</param>
        public static void DeleteStudentAvatar(string relativePath)
        {
            try
            {
                if (string.IsNullOrEmpty(relativePath))
                    return;

                string fullPath = Path.Combine(Application.StartupPath, relativePath);
                
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xóa ảnh: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy đường dẫn đầy đủ của ảnh từ đường dẫn tương đối
        /// </summary>
        /// <param name="relativePath">Đường dẫn tương đối</param>
        /// <returns>Đường dẫn đầy đủ</returns>
        public static string GetFullImagePath(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                return DEFAULT_AVATAR;

            string fullPath = Path.Combine(Application.StartupPath, relativePath);
            
            return File.Exists(fullPath) ? fullPath : DEFAULT_AVATAR;
        }

        /// <summary>
        /// Load ảnh từ đường dẫn tương đối
        /// </summary>
        /// <param name="relativePath">Đường dẫn tương đối</param>
        /// <returns>Image object, hoặc ảnh mặc định nếu không tìm thấy</returns>
        public static Image LoadStudentAvatar(string relativePath)
        {
            try
            {
                string fullPath = GetFullImagePath(relativePath);
                return Image.FromFile(fullPath);
            }
            catch
            {
                // Trả về ảnh mặc định nếu có lỗi
                try
                {
                    return Image.FromFile(DEFAULT_AVATAR);
                }
                catch
                {
                    // Tạo ảnh placeholder đơn giản
                    Bitmap placeholder = new Bitmap(200, 200);
                    using (Graphics g = Graphics.FromImage(placeholder))
                    {
                        g.Clear(Color.LightGray);
                        g.DrawString("No Image", new Font("Arial", 12), Brushes.Black, 50, 90);
                    }
                    return placeholder;
                }
            }
        }

        /// <summary>
        /// Resize ảnh giữ nguyên tỷ lệ
        /// </summary>
        private static Image ResizeImage(Image image, int maxWidth, int maxHeight)
        {
            // Tính tỷ lệ resize
            double ratioX = (double)maxWidth / image.Width;
            double ratioY = (double)maxHeight / image.Height;
            double ratio = Math.Min(ratioX, ratioY);

            // Nếu ảnh nhỏ hơn max size thì không resize
            if (ratio >= 1)
                return new Bitmap(image);

            int newWidth = (int)(image.Width * ratio);
            int newHeight = (int)(image.Height * ratio);

            Bitmap newImage = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }

        /// <summary>
        /// Lấy ImageFormat từ extension
        /// </summary>
        private static ImageFormat GetImageFormat(string extension)
        {
            switch (extension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    return ImageFormat.Jpeg;
                case ".png":
                    return ImageFormat.Png;
                case ".bmp":
                    return ImageFormat.Bmp;
                case ".gif":
                    return ImageFormat.Gif;
                default:
                    return ImageFormat.Jpeg;
            }
        }

        /// <summary>
        /// Chọn ảnh từ máy tính
        /// </summary>
        /// <returns>Đường dẫn file ảnh được chọn, null nếu hủy</returns>
        public static string SelectImageFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Chọn ảnh đại diện";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
            }
            return null;
        }
    }
}
