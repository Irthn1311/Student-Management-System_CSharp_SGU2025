namespace Student_Management_System_CSharp_SGU2025.DTO
{
    /// <summary>
    /// DTO for PhanCongCandidate - used for temporary assignment storage
    /// </summary>
    public class PhanCongCandidateDTO
    {
        public int MaLop { get; set; }
        public int MaMonHoc { get; set; }
        public string MaGiaoVien { get; set; } = string.Empty;
        public int SoTietTuan { get; set; }
        public int Score { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}
