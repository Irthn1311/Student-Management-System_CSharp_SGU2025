using System.Collections.Generic;
using Student_Management_System_CSharp_SGU2025.DAO;
using Student_Management_System_CSharp_SGU2025.Scheduling;

namespace Student_Management_System_CSharp_SGU2025.BUS
{
	internal class ThoiKhoaBieuBUS
	{
		private readonly ThoiKhoaBieuDAO _dao;
		public ThoiKhoaBieuBUS()
		{
			_dao = new ThoiKhoaBieuDAO();
		}

		public void ClearTemp()
		{
			_dao.ClearTemp();
		}

		public void InsertTemp(int semesterId, int weekNo, ScheduleSolution sol)
		{
			_dao.InsertTemp(semesterId, weekNo, sol);
		}

		public void AcceptTempToOfficial(int semesterId, int weekNo)
		{
			_dao.AcceptTempToOfficial(semesterId, weekNo);
		}

		public List<AssignmentSlot> GetWeek(int semesterId, int weekNo)
		{
			return _dao.GetWeek(semesterId, weekNo);
		}

		public bool HasConflict(int semesterId, int weekNo, AssignmentSlot slot)
		{
			return _dao.HasConflict(semesterId, weekNo, slot);
		}
	}
}


