namespace Student_Management_System_CSharp_SGU2025.DTO
{
    /// <summary>
    /// Represents one placed lesson in the timetable.
    /// Used for data transfer between DAO, BUS, and scheduling services.
    /// </summary>
    public class AssignmentSlotDTO
    {
        public int MaLop { get; set; }
        public int Thu { get; set; }
        public int Tiet { get; set; }
        public int MaMon { get; set; }
        public string MaGV { get; set; } = string.Empty;
        public string Phong { get; set; } = string.Empty;
    }
}
