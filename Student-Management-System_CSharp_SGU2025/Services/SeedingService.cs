using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.Services
{
    /// <summary>
    /// Service for seeding test data for stress testing the Timetable Algorithm
    /// </summary>
    public class SeedingService
    {
        /// <summary>
        /// Curriculum structure for stress testing
        /// Maps subject names to periods per week
        /// </summary>
        private static readonly Dictionary<string, int> CurriculumRules = new Dictionary<string, int>
        {
            // Core subjects: 4 periods/week
            { "Toán", 4 },
            { "Văn", 4 },
            { "Anh", 4 },
            
            // Science subjects: 2 periods/week
            { "Lý", 2 },
            { "Hóa", 2 },
            { "Sinh", 2 },
            
            // Social subjects: 1 period/week
            { "Sử", 1 },
            { "Địa", 1 },
            { "GDCD", 1 },
            
            // Other subjects: 1 period/week
            { "Tin", 1 },
            { "Công Nghệ", 1 },
            { "Thể Dục", 1 },
            { "GDQP", 1 }
        };

        /// <summary>
        /// Seed full assignments for all classes in the given semester
        /// </summary>
        /// <param name="maHocKy">Semester ID</param>
        /// <returns>Report string with statistics</returns>
        public string SeedFullAssignments(int maHocKy)
        {
            var report = new System.Text.StringBuilder();
            int totalAssignments = 0;
            int totalClasses = 0;
            int failures = 0;
            var failureMessages = new List<string>();

            try
            {
                // Step 1: Get semester info
                var hocKyDAO = new HocKyDAO();
                var hocKy = hocKyDAO.LayHocKyTheoMa(maHocKy);
                if (hocKy == null)
                {
                    throw new ArgumentException($"Học kỳ {maHocKy} không tồn tại");
                }

                if (!hocKy.NgayBD.HasValue || !hocKy.NgayKT.HasValue)
                {
                    throw new ArgumentException($"Học kỳ {maHocKy} chưa có ngày bắt đầu/kết thúc");
                }

                DateTime ngayBatDau = hocKy.NgayBD.Value;
                DateTime ngayKetThuc = hocKy.NgayKT.Value;

                report.AppendLine($"=== SEEDING ASSIGNMENTS FOR SEMESTER {maHocKy} ===");
                report.AppendLine($"Semester: {hocKy.TenHocKy}");
                report.AppendLine($"Date Range: {ngayBatDau:yyyy-MM-dd} to {ngayKetThuc:yyyy-MM-dd}");
                report.AppendLine();

                // Step 2: Update MonHoc table with curriculum rules
                report.AppendLine("Step 1: Updating subject periods according to curriculum...");
                UpdateSubjectPeriods();
                report.AppendLine("✓ Subject periods updated");
                report.AppendLine();

                // Step 3: Delete all existing assignments for this semester
                report.AppendLine("Step 2: Cleaning existing assignments...");
                int deletedCount = DeleteAssignmentsForSemester(maHocKy);
                report.AppendLine($"✓ Deleted {deletedCount} existing assignments");
                report.AppendLine();

                // Step 4: Get all classes
                var lopDAO = new LopDAO();
                var classes = lopDAO.DocDSLop();
                if (classes == null || classes.Count == 0)
                {
                    throw new Exception("Không tìm thấy lớp học nào trong hệ thống");
                }

                totalClasses = classes.Count;
                report.AppendLine($"Step 3: Found {totalClasses} classes");
                report.AppendLine();

                // Step 5: Create assignment policy
                var policy = new AssignmentPolicy
                {
                    MaxLoadPerTeacherPerWeek = 30, // Reasonable max load for stress test
                    AllowNonPrimarySpecialty = true, // ✅ Cho phép GV dạy môn không chuyên môn để tránh lỗi
                    SpecialtyWeight = 10,
                    PriorityWeight = 2,
                    LoadBalanceWeight = 5
                };

                // Step 6: Generate assignments for all classes at once
                report.AppendLine("Step 4: Generating auto assignments...");
                var autoService = new AssignmentAutoService();
                var result = autoService.GenerateAutoAssignments(maHocKy, policy);

                if (result.IsReadOnly)
                {
                    throw new Exception($"Học kỳ {maHocKy} đã kết thúc. Không thể tạo phân công mới.");
                }

                if (result.Report.HardViolations > 0)
                {
                    report.AppendLine($"⚠️ Warning: {result.Report.HardViolations} violations detected:");
                    foreach (var msg in result.Report.Messages)
                    {
                        report.AppendLine($"  - {msg}");
                    }
                    report.AppendLine();
                }

                int candidatesCount = result.Candidates?.Count ?? 0;
                report.AppendLine($"✓ Generated {candidatesCount} assignment candidates");
                report.AppendLine();

                if (candidatesCount == 0)
                {
                    throw new Exception("Không tạo được phân công nào. Kiểm tra lại dữ liệu giáo viên và môn học.");
                }

                // Step 7: Convert candidates to DTOs and save
                report.AppendLine("Step 5: Saving assignments to database...");
                var assignmentsToSave = ConvertCandidatesToDTOs(result.Candidates, maHocKy, ngayBatDau, ngayKetThuc);
                
                // Save in batch with transaction
                var phanCongDAO = new PhanCongGiangDayDAO();
                using (var conn = ConnectionDatabase.GetConnection())
                {
                    conn.Open();
                    using (var tx = conn.BeginTransaction())
                    {
                        try
                        {
                            phanCongDAO.InsertBatch(assignmentsToSave, tx);
                            tx.Commit();
                            totalAssignments = assignmentsToSave.Count;
                            report.AppendLine($"✓ Successfully saved {totalAssignments} assignments");
                        }
                        catch (Exception ex)
                        {
                            tx.Rollback();
                            failures++;
                            string errorMsg = $"Failed to save assignments: {ex.Message}";
                            failureMessages.Add(errorMsg);
                            report.AppendLine($"✗ {errorMsg}");
                            throw;
                        }
                    }
                }

                report.AppendLine();
                report.AppendLine("=== SEEDING COMPLETE ===");
                report.AppendLine($"Total Classes: {totalClasses}");
                report.AppendLine($"Total Assignments: {totalAssignments}");
                report.AppendLine($"Failures: {failures}");

                if (failures > 0)
                {
                    report.AppendLine();
                    report.AppendLine("Failure Details:");
                    foreach (var msg in failureMessages)
                    {
                        report.AppendLine($"  - {msg}");
                    }
                }
            }
            catch (Exception ex)
            {
                failures++;
                string errorMsg = $"Critical error during seeding: {ex.Message}";
                failureMessages.Add(errorMsg);
                report.AppendLine($"✗ {errorMsg}");
                report.AppendLine();
                report.AppendLine($"Total Failures: {failures}");
            }

            return report.ToString();
        }

        /// <summary>
        /// Update MonHoc table with curriculum rules (periods per week)
        /// </summary>
        private void UpdateSubjectPeriods()
        {
            var monHocDAO = new MonHocDAO();
            var allSubjects = monHocDAO.DocDSMH();

            using (var conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var subject in allSubjects)
                        {
                            // Try to match subject name with curriculum rules (case-insensitive, partial match)
                            var matchedRule = CurriculumRules.FirstOrDefault(kvp =>
                                subject.tenMon.ToLower().Contains(kvp.Key.ToLower()) ||
                                kvp.Key.ToLower().Contains(subject.tenMon.ToLower()));

                            if (matchedRule.Key != null)
                            {
                                // Update SoTiet if different
                                if (subject.soTiet != matchedRule.Value)
                                {
                                    string updateQuery = "UPDATE MonHoc SET SoTiet = @SoTiet WHERE MaMonHoc = @MaMonHoc";
                                    using (var cmd = new MySqlCommand(updateQuery, conn, tx))
                                    {
                                        cmd.Parameters.AddWithValue("@SoTiet", matchedRule.Value);
                                        cmd.Parameters.AddWithValue("@MaMonHoc", subject.maMon);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Delete all assignments for a given semester
        /// </summary>
        private int DeleteAssignmentsForSemester(int maHocKy)
        {
            using (var conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        string deleteQuery = "DELETE FROM PhanCongGiangDay WHERE MaHocKy = @MaHocKy";
                        using (var cmd = new MySqlCommand(deleteQuery, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
                            int deletedCount = cmd.ExecuteNonQuery();
                            tx.Commit();
                            return deletedCount;
                        }
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Convert PhanCongCandidate list to PhanCongGiangDayDTO list
        /// </summary>
        private List<PhanCongGiangDayDTO> ConvertCandidatesToDTOs(
            List<PhanCongCandidate> candidates,
            int maHocKy,
            DateTime ngayBatDau,
            DateTime ngayKetThuc)
        {
            var dtos = new List<PhanCongGiangDayDTO>();

            foreach (var candidate in candidates)
            {
                var dto = new PhanCongGiangDayDTO
                {
                    MaLop = candidate.MaLop,
                    MaGiaoVien = candidate.MaGiaoVien,
                    MaMonHoc = candidate.MaMonHoc,
                    MaHocKy = maHocKy,
                    NgayBatDau = ngayBatDau,
                    NgayKetThuc = ngayKetThuc
                };

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}

