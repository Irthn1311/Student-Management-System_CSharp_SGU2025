using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MySql.Data.MySqlClient;
using Student_Management_System_CSharp_SGU2025.ConnectDatabase;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.DTO;

namespace Student_Management_System_CSharp_SGU2025.Services
{
    /// <summary>
    /// Assignment DTO for scheduling (combines PhanCongGiangDay with SoTietCanXep)
    /// </summary>
    public class AssignmentDTO
    {
        public int MaPhanCong { get; set; }
        public int MaLop { get; set; }
        public string MaGiaoVien { get; set; } = string.Empty;
        public int MaMonHoc { get; set; }
        public int MaHocKy { get; set; }
        public int SoTietCanXep { get; set; } // Số tiết cần xếp trong tuần
        public string TenMon { get; set; } = string.Empty; // For constraint checking
    }

    /// <summary>
    /// Result of timetable generation
    /// </summary>
    public class GenerationResult
    {
        public bool Success { get; set; }
        public int TotalSlots { get; set; }
        public int FailedAssignments { get; set; }
        public double InitialScore { get; set; }
        public double FinalScore { get; set; }
        public int Iterations { get; set; }
        public List<string> FailedAssignmentMessages { get; set; } = new List<string>();
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// Hybrid Auto-Scheduling Service: Greedy Initialization + Tabu Search Optimization
    /// CRITICAL: All operations use in-memory matrices - NO SQL queries in loops
    /// </summary>
    public class TimetableHybridService
    {
        // ========== IN-MEMORY DATA STRUCTURES (RAM MATRICES) ==========
        
        /// <summary>
        /// Class busy matrix: [ClassID, Day, Period]
        /// Dimensions: [MaxClassID + 1, 7, 11]
        /// Day: 2-6 (Thứ 2 to Thứ 6), Period: 1-10
        /// </summary>
        private bool[,,] _classBusy;

        /// <summary>
        /// Teacher busy matrix: [TeacherIndex, Day, Period]
        /// Dimensions: [MaxTeacherIndex + 1, 7, 11]
        /// Uses Dictionary to map TeacherID (string) to integer index
        /// </summary>
        private bool[,,] _teacherBusy;

        /// <summary>
        /// Current solution (list of placed slots)
        /// </summary>
        private List<TimeTableSlotDTO> _currentSolution = new List<TimeTableSlotDTO>();

        /// <summary>
        /// Teacher ID to Index mapping
        /// </summary>
        private Dictionary<string, int> _teacherIdToIndex = new Dictionary<string, int>();

        /// <summary>
        /// Index to Teacher ID mapping (reverse)
        /// </summary>
        private Dictionary<int, string> _indexToTeacherId = new Dictionary<int, string>();

        /// <summary>
        /// Failed assignments (could not be placed)
        /// </summary>
        private List<string> _failedAssignments = new List<string>();

        /// <summary>
        /// Tabu list for Tabu Search (stores move signatures)
        /// </summary>
        private Queue<string> _tabuList = new Queue<string>();

        /// <summary>
        /// Cached score for optimization (performance improvement)
        /// </summary>
        private double _cachedScore = double.MaxValue;

        /// <summary>
        /// Flag indicating if cached score is dirty and needs recalculation
        /// </summary>
        private bool _isScoreDirty = true;

        /// <summary>
        /// Maximum class ID encountered
        /// </summary>
        private int _maxClassId = 0;

        /// <summary>
        /// Maximum teacher index
        /// </summary>
        private int _maxTeacherIndex = 0;

        // Constants
        private const int MIN_DAY = 2;  // Thứ 2
        private const int MAX_DAY = 6;  // Thứ 6
        private const int MIN_PERIOD = 1;
        private const int MAX_PERIOD = 10;
        private const int TABU_TENURE = 9; // Tabu list size

        // DAO instances
        private readonly PhanCongGiangDayDAO _phanCongDAO;
        private readonly MonHocDAO _monHocDAO;
        private readonly ThoiKhoaBieuDAO _tkbDAO;

        public TimetableHybridService()
        {
            _phanCongDAO = new PhanCongGiangDayDAO();
            _monHocDAO = new MonHocDAO();
            _tkbDAO = new ThoiKhoaBieuDAO();
        }

        // ========== MAIN PUBLIC METHOD ==========

        /// <summary>
        /// Run the complete auto-scheduling process
        /// </summary>
        /// <param name="maHocKy">Mã học kỳ</param>
        /// <param name="progress">Progress reporter for UI updates (optional)</param>
        public GenerationResult RunAutoScheduling(int maHocKy, IProgress<string> progress = null)
        {
            var result = new GenerationResult();

            try
            {
                // Reset score cache
                _isScoreDirty = true;
                _cachedScore = double.MaxValue;

                // Step 1: Load all data from DAO (ONE-TIME SQL queries)
                progress?.Report("Đang tải dữ liệu phân công...");
                var assignments = LoadAssignments(maHocKy);
                if (assignments == null || assignments.Count == 0)
                {
                    result.Success = false;
                    result.Message = "Không có phân công giảng dạy nào cho học kỳ này.";
                    return result;
                }

                // Step 2: Map Teacher IDs to Integers
                progress?.Report("Đang khởi tạo ma trận...");
                BuildTeacherMapping(assignments);

                // Step 3: Initialize matrices
                InitializeMatrices(assignments);

                // Step 4: Phase 1 - Greedy Initialization
                progress?.Report("Đang xếp tự động (Greedy)...");
                InitialGreedyFill(maHocKy, assignments, progress);
                result.FailedAssignments = _failedAssignments.Count;
                result.FailedAssignmentMessages = new List<string>(_failedAssignments);
                result.TotalSlots = _currentSolution.Count;

                // Step 5: Calculate initial score
                result.InitialScore = CalculateScore();

                // Step 6: Phase 2 - Tabu Search Optimization
                progress?.Report("Đang tối ưu hóa (Tabu Search)...");
                int iterations = OptimizeSolution(maxIterations: 2000, progress: progress);
                result.Iterations = iterations;
                result.FinalScore = CalculateScore();

                // Step 7: Sync to Database
                progress?.Report("Đang lưu vào cơ sở dữ liệu...");
                SyncToDatabase(maHocKy);
                progress?.Report("Hoàn tất!");

                result.Success = true;
                result.Message = $"Đã tạo {result.TotalSlots} tiết học. " +
                               $"Tối ưu hóa: {result.InitialScore:F2} → {result.FinalScore:F2} " +
                               $"({iterations} iterations).";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Lỗi: {ex.Message}";
            }

            return result;
        }

        // ========== PHASE 1: GREEDY INITIALIZATION ==========

        /// <summary>
        /// Load assignments from database and enrich with SoTietCanXep
        /// </summary>
        private List<AssignmentDTO> LoadAssignments(int maHocKy)
        {
            // Load PhanCongGiangDay (ONE SQL query)
            var phanCongs = _phanCongDAO.LayPhanCongTheoHocKy(maHocKy);
            if (phanCongs == null || phanCongs.Count == 0)
                return new List<AssignmentDTO>();

            // Load all MonHoc (ONE SQL query)
            var monHocs = _monHocDAO.DocDSMH();
            var monHocDict = monHocs.ToDictionary(m => m.maMon, m => m);

            // Build AssignmentDTO list
            var assignments = new List<AssignmentDTO>();
            foreach (var pc in phanCongs)
            {
                var assignment = new AssignmentDTO
                {
                    MaPhanCong = pc.MaPhanCong,
                    MaLop = pc.MaLop,
                    MaGiaoVien = pc.MaGiaoVien,
                    MaMonHoc = pc.MaMonHoc,
                    MaHocKy = pc.MaHocKy
                };

                // Get SoTiet from MonHoc
                if (monHocDict.TryGetValue(pc.MaMonHoc, out var monHoc))
                {
                    assignment.SoTietCanXep = monHoc.soTiet;
                    assignment.TenMon = monHoc.tenMon;
                }
                else
                {
                    assignment.SoTietCanXep = 2; // Default
                }

                assignments.Add(assignment);
            }

            return assignments;
        }

        /// <summary>
        /// Build teacher ID to index mapping
        /// </summary>
        private void BuildTeacherMapping(List<AssignmentDTO> assignments)
        {
            _teacherIdToIndex.Clear();
            _indexToTeacherId.Clear();

            var uniqueTeachers = assignments.Select(a => a.MaGiaoVien).Distinct().ToList();
            int index = 0;
            foreach (var teacherId in uniqueTeachers)
            {
                _teacherIdToIndex[teacherId] = index;
                _indexToTeacherId[index] = teacherId;
                index++;
            }

            _maxTeacherIndex = index - 1;
        }

        /// <summary>
        /// Initialize and resize matrices
        /// </summary>
        private void InitializeMatrices(List<AssignmentDTO> assignments)
        {
            // Find max class ID
            _maxClassId = assignments.Max(a => a.MaLop);

            // Initialize matrices: [MaxID + 1, 7, 11]
            // Day dimension: 0-6 (we use 2-6, but allocate 0-6 for simplicity)
            // Period dimension: 0-10 (we use 1-10, but allocate 0-10)
            _classBusy = new bool[_maxClassId + 1, 7, 11];
            _teacherBusy = new bool[_maxTeacherIndex + 1, 7, 11];

            // Clear solution
            _currentSolution.Clear();
            _failedAssignments.Clear();
            _tabuList.Clear();
        }

        /// <summary>
        /// Phase 1: Greedy Initialization
        /// </summary>
        private void InitialGreedyFill(int maHocKy, List<AssignmentDTO> assignments, IProgress<string> progress = null)
        {
            // Step A: Clear matrices (already done in InitializeMatrices)

            // Step B: Sort assignments by priority
            var sortedAssignments = SortAssignmentsByPriority(assignments);

            // Step C: Iterate and place assignments
            int totalAssignments = sortedAssignments.Count;
            int processedCount = 0;
            foreach (var assignment in sortedAssignments)
            {
                processedCount++;
                if (processedCount % 50 == 0)
                {
                    progress?.Report($"Đang xếp tự động: {processedCount}/{totalAssignments} phân công...");
                }

                int placedCount = 0;
                int neededCount = assignment.SoTietCanXep;

                // Try to place each needed period
                for (int period = 0; period < neededCount; period++)
                {
                    bool placed = false;

                    // Scan all days and periods
                    for (int day = MIN_DAY; day <= MAX_DAY && !placed; day++)
                    {
                        for (int periodNum = MIN_PERIOD; periodNum <= MAX_PERIOD && !placed; periodNum++)
                        {
                            // Check if slot is free (RAM check only)
                            if (IsSlotFree(assignment.MaLop, assignment.MaGiaoVien, day, periodNum))
                            {
                                // Place the slot
                                var slot = new TimeTableSlotDTO
                                {
                                    MaThoiKhoaBieu = 0, // Will be set on insert
                                    MaPhanCong = assignment.MaPhanCong,
                                    Thu = day,
                                    Tiet = periodNum,
                                    MaLop = assignment.MaLop,
                                    MaGiaoVien = assignment.MaGiaoVien,
                                    TenMon = assignment.TenMon,
                                    TenLop = "", // Will be filled if needed
                                    TenGiaoVien = "" // Will be filled if needed
                                };

                                _currentSolution.Add(slot);
                                MarkSlotBusy(assignment.MaLop, assignment.MaGiaoVien, day, periodNum);
                                placed = true;
                                placedCount++;
                            }
                        }
                    }

                    // If couldn't place this period, continue to next
                    if (!placed)
                    {
                        break; // Can't place more periods for this assignment
                    }
                }

                // If couldn't place all periods, add to failed list
                if (placedCount < neededCount)
                {
                    _failedAssignments.Add(
                        $"Không thể xếp đủ {neededCount} tiết cho Lớp {assignment.MaLop}, " +
                        $"Môn {assignment.TenMon}, GV {assignment.MaGiaoVien}. " +
                        $"Đã xếp: {placedCount}/{neededCount}");
                }
            }
        }

        /// <summary>
        /// Sort assignments by priority
        /// </summary>
        private List<AssignmentDTO> SortAssignmentsByPriority(List<AssignmentDTO> assignments)
        {
            return assignments.OrderByDescending(a =>
            {
                int priority = 0;

                // Priority 1: Subjects with constraints (GDQP, Thể dục)
                string tenMonLower = a.TenMon.ToLower();
                if (tenMonLower.Contains("quốc phòng") || tenMonLower.Contains("gdqp") ||
                    tenMonLower.Contains("thể dục") || tenMonLower.Contains("giáo dục thể chất"))
                {
                    priority += 1000;
                }

                // Priority 2: Teachers with high SoTietCanXep (harder to fit)
                priority += a.SoTietCanXep * 10;

                return priority;
            }).ToList();
        }

        // ========== PHASE 2: TABU SEARCH OPTIMIZATION ==========

        /// <summary>
        /// Phase 2: Tabu Search Optimization
        /// </summary>
        private int OptimizeSolution(int maxIterations = 2000, IProgress<string> progress = null)
        {
            if (_currentSolution.Count == 0)
                return 0;

            double bestScore = CalculateScore();
            var bestSolution = CloneSolution(_currentSolution);
            int noImproveCount = 0;
            Random random = new Random();

            for (int iter = 0; iter < maxIterations; iter++)
            {
                // Report progress every 500 iterations
                if (iter % 500 == 0 && iter > 0)
                {
                    progress?.Report($"Đang tối ưu hóa: {iter}/{maxIterations} lần lặp...");
                }

                // Generate neighbor
                bool improved = false;

                // Randomly choose operator: Move or Swap
                if (random.Next(2) == 0)
                {
                    // Operator 1: Move
                    improved = TryMoveOperator(random);
                }
                else
                {
                    // Operator 2: Swap (same class)
                    improved = TrySwapOperator(random);
                }

                // Calculate new score (uses cache if not dirty)
                double newScore = CalculateScore();

                // Check if improved
                if (newScore < bestScore)
                {
                    bestScore = newScore;
                    bestSolution = CloneSolution(_currentSolution);
                    noImproveCount = 0;
                }
                else
                {
                    noImproveCount++;
                }

                // Early stopping if no improvement for too long
                if (noImproveCount > 500)
                {
                    // Restore best solution
                    _currentSolution = bestSolution;
                    RestoreMatricesFromSolution();
                    break;
                }

                // Update tabu list
                UpdateTabuList();
            }

            // Restore best solution
            _currentSolution = bestSolution;
            RestoreMatricesFromSolution();

            return maxIterations;
        }

        /// <summary>
        /// Try Move operator: Move a random slot to a random empty position
        /// </summary>
        private bool TryMoveOperator(Random random)
        {
            if (_currentSolution.Count == 0)
                return false;

            // Pick random slot
            int slotIndex = random.Next(_currentSolution.Count);
            var slot = _currentSolution[slotIndex];

            // Store original position
            int oldDay = slot.Thu;
            int oldPeriod = slot.Tiet;

            // Try random new position
            int newDay = random.Next(MIN_DAY, MAX_DAY + 1);
            int newPeriod = random.Next(MIN_PERIOD, MAX_PERIOD + 1);

            // Check if new position is free
            if (!IsSlotFree(slot.MaLop, slot.MaGiaoVien, newDay, newPeriod))
            {
                return false;
            }

            // Check tabu
            string moveSignature = $"MOVE_{slot.MaPhanCong}_{oldDay}_{oldPeriod}_{newDay}_{newPeriod}";
            if (_tabuList.Contains(moveSignature))
            {
                return false;
            }

            // Perform move
            UnmarkSlotBusy(slot.MaLop, slot.MaGiaoVien, oldDay, oldPeriod);
            slot.Thu = newDay;
            slot.Tiet = newPeriod;
            MarkSlotBusy(slot.MaLop, slot.MaGiaoVien, newDay, newPeriod);

            // Mark score as dirty (solution changed)
            _isScoreDirty = true;

            // Add to tabu
            _tabuList.Enqueue(moveSignature);
            return true;
        }

        /// <summary>
        /// Try Swap operator: Swap two slots of the same class
        /// </summary>
        private bool TrySwapOperator(Random random)
        {
            if (_currentSolution.Count < 2)
                return false;

            // Find slots of same class
            var sameClassSlots = _currentSolution
                .Select((s, i) => new { Slot = s, Index = i })
                .GroupBy(x => x.Slot.MaLop)
                .Where(g => g.Count() >= 2)
                .SelectMany(g => g)
                .ToList();

            if (sameClassSlots.Count < 2)
                return false;

            // Pick two random slots from same class
            int idx1 = random.Next(sameClassSlots.Count);
            int idx2 = random.Next(sameClassSlots.Count);
            if (idx1 == idx2)
            {
                if (sameClassSlots.Count > 1)
                    idx2 = (idx1 + 1) % sameClassSlots.Count;
                else
                    return false;
            }

            var slot1 = sameClassSlots[idx1].Slot;
            var slot2 = sameClassSlots[idx2].Slot;

            // Check if swap is valid (teachers not busy at new positions)
            if (!IsSlotFree(slot1.MaLop, slot1.MaGiaoVien, slot2.Thu, slot2.Tiet) ||
                !IsSlotFree(slot2.MaLop, slot2.MaGiaoVien, slot1.Thu, slot1.Tiet))
            {
                return false;
            }

            // Check tabu
            string swapSignature = $"SWAP_{slot1.MaPhanCong}_{slot2.MaPhanCong}";
            if (_tabuList.Contains(swapSignature))
            {
                return false;
            }

            // Perform swap
            UnmarkSlotBusy(slot1.MaLop, slot1.MaGiaoVien, slot1.Thu, slot1.Tiet);
            UnmarkSlotBusy(slot2.MaLop, slot2.MaGiaoVien, slot2.Thu, slot2.Tiet);

            int tempDay = slot1.Thu;
            int tempPeriod = slot1.Tiet;
            slot1.Thu = slot2.Thu;
            slot1.Tiet = slot2.Tiet;
            slot2.Thu = tempDay;
            slot2.Tiet = tempPeriod;

            MarkSlotBusy(slot1.MaLop, slot1.MaGiaoVien, slot1.Thu, slot1.Tiet);
            MarkSlotBusy(slot2.MaLop, slot2.MaGiaoVien, slot2.Thu, slot2.Tiet);

            // Mark score as dirty (solution changed)
            _isScoreDirty = true;

            // Add to tabu
            _tabuList.Enqueue(swapSignature);
            return true;
        }

        // ========== HELPER METHODS ==========

        /// <summary>
        /// Check if slot is free (RAM check only)
        /// </summary>
        private bool IsSlotFree(int maLop, string maGiaoVien, int day, int period)
        {
            // Check class busy
            if (_classBusy[maLop, day, period])
                return false;

            // Check teacher busy
            if (_teacherIdToIndex.TryGetValue(maGiaoVien, out int teacherIndex))
            {
                if (_teacherBusy[teacherIndex, day, period])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Mark slot as busy
        /// </summary>
        private void MarkSlotBusy(int maLop, string maGiaoVien, int day, int period)
        {
            _classBusy[maLop, day, period] = true;

            if (_teacherIdToIndex.TryGetValue(maGiaoVien, out int teacherIndex))
            {
                _teacherBusy[teacherIndex, day, period] = true;
            }
        }

        /// <summary>
        /// Unmark slot as busy
        /// </summary>
        private void UnmarkSlotBusy(int maLop, string maGiaoVien, int day, int period)
        {
            _classBusy[maLop, day, period] = false;

            if (_teacherIdToIndex.TryGetValue(maGiaoVien, out int teacherIndex))
            {
                _teacherBusy[teacherIndex, day, period] = false;
            }
        }

        /// <summary>
        /// Calculate fitness score (minimize gaps and dispersion)
        /// Uses caching to avoid expensive recalculation
        /// </summary>
        private double CalculateScore()
        {
            // Return cached score if not dirty
            if (!_isScoreDirty)
            {
                return _cachedScore;
            }

            double score = 0;

            // Group slots by teacher
            var teacherSlots = _currentSolution
                .GroupBy(s => s.MaGiaoVien)
                .ToList();

            foreach (var teacherGroup in teacherSlots)
            {
                var slots = teacherGroup.OrderBy(s => s.Thu).ThenBy(s => s.Tiet).ToList();

                // Calculate gaps (idle periods between classes)
                for (int i = 0; i < slots.Count - 1; i++)
                {
                    var slot1 = slots[i];
                    var slot2 = slots[i + 1];

                    // Same day: gap = period difference - 1
                    if (slot1.Thu == slot2.Thu)
                    {
                        int gap = slot2.Tiet - slot1.Tiet - 1;
                        if (gap > 0)
                        {
                            score += gap * 2; // Penalty for gaps
                        }
                    }
                    // Different days: no gap penalty (normal)
                }

                // Dispersion penalty: if teacher has too many different days
                int uniqueDays = slots.Select(s => s.Thu).Distinct().Count();
                if (uniqueDays > 3)
                {
                    score += (uniqueDays - 3) * 5; // Penalty for too many days
                }
            }

            // Cache the score
            _cachedScore = score;
            _isScoreDirty = false;
            return score;
        }

        /// <summary>
        /// Clone solution
        /// </summary>
        private List<TimeTableSlotDTO> CloneSolution(List<TimeTableSlotDTO> solution)
        {
            return solution.Select(s => new TimeTableSlotDTO
            {
                MaThoiKhoaBieu = s.MaThoiKhoaBieu,
                MaPhanCong = s.MaPhanCong,
                Thu = s.Thu,
                Tiet = s.Tiet,
                MaLop = s.MaLop,
                MaGiaoVien = s.MaGiaoVien,
                TenMon = s.TenMon,
                TenLop = s.TenLop,
                TenGiaoVien = s.TenGiaoVien
            }).ToList();
        }

        /// <summary>
        /// Restore matrices from current solution
        /// </summary>
        private void RestoreMatricesFromSolution()
        {
            // Clear matrices
            Array.Clear(_classBusy, 0, _classBusy.Length);
            Array.Clear(_teacherBusy, 0, _teacherBusy.Length);

            // Rebuild from solution
            foreach (var slot in _currentSolution)
            {
                MarkSlotBusy(slot.MaLop, slot.MaGiaoVien, slot.Thu, slot.Tiet);
            }

            // Mark score as dirty since solution was restored
            _isScoreDirty = true;
        }

        /// <summary>
        /// Update tabu list (maintain size)
        /// </summary>
        private void UpdateTabuList()
        {
            while (_tabuList.Count > TABU_TENURE)
            {
                _tabuList.Dequeue();
            }
        }

        /// <summary>
        /// Sync solution to database (bulk insert - OPTIMIZED)
        /// Uses single bulk INSERT statement instead of 550+ individual INSERTs
        /// </summary>
        private void SyncToDatabase(int maHocKy)
        {
            if (_currentSolution.Count == 0)
                return;

            // Delete old TKB for this semester
            const string deleteSql = @"
                DELETE tkb FROM ThoiKhoaBieu tkb
                JOIN PhanCongGiangDay pc ON tkb.MaPhanCong = pc.MaPhanCong
                WHERE pc.MaHocKy = @MaHocKy";

            // Build bulk INSERT with VALUES clause
            // Format: INSERT INTO ... VALUES (?, ?, ?), (?, ?, ?), ...
            var valuesBuilder = new StringBuilder();
            var parameters = new List<MySqlParameter>();
            
            for (int i = 0; i < _currentSolution.Count; i++)
            {
                if (i > 0)
                    valuesBuilder.Append(", ");
                
                var slot = _currentSolution[i];
                string paramPrefix = $"@p{i}_";
                
                valuesBuilder.Append($"({paramPrefix}MaPhanCong, {paramPrefix}ThuTrongTuan, {paramPrefix}TietBatDau, 1, NULL)");
                
                parameters.Add(new MySqlParameter($"{paramPrefix}MaPhanCong", slot.MaPhanCong));
                parameters.Add(new MySqlParameter($"{paramPrefix}ThuTrongTuan", $"Thu {slot.Thu}"));
                parameters.Add(new MySqlParameter($"{paramPrefix}TietBatDau", slot.Tiet));
            }

            string bulkInsertSql = $@"
                INSERT INTO ThoiKhoaBieu(MaPhanCong, ThuTrongTuan, TietBatDau, SoTiet, PhongHoc)
                VALUES {valuesBuilder.ToString()}";

            using (var conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        // Delete old TKB
                        using (var delCmd = new MySqlCommand(deleteSql, conn, tx))
                        {
                            delCmd.Parameters.AddWithValue("@MaHocKy", maHocKy);
                            delCmd.CommandTimeout = 120; // 2 minutes timeout for large deletes
                            delCmd.ExecuteNonQuery();
                        }

                        // Bulk insert all slots in ONE statement
                        using (var insCmd = new MySqlCommand(bulkInsertSql, conn, tx))
                        {
                            insCmd.Parameters.AddRange(parameters.ToArray());
                            insCmd.CommandTimeout = 120; // 2 minutes timeout for large inserts
                            insCmd.ExecuteNonQuery(); // Single round trip to database!
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
    }
}

