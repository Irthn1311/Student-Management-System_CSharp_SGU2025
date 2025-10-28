# TODO - DANH SÁCH CÔNG VIỆC CHI TIẾT

**Project:** Student Management System CSharp SGU2025  
**Date:** ${new Date().toISOString().split('T')[0]}  
**Total Tasks:** 78

---

## 1. CRITICAL BUSINESS LOGIC (Day 1-2)

### Database Improvements
- [ ] Create trigger for `DiemTrungBinh` calculation
- [ ] Add CHECK constraint for `DiemSo` (0-10)
- [ ] Add missing indexes (HoTen, composite indexes)
- [ ] Fix duplicate tables (GiaoVien_MonHoc vs GiaoVienChuyenMon)
- [ ] Create seed data script (500 HS, full data)
- [ ] Test seed data import

### Implement Calculate DiemTrungBinh
- [ ] Create method `CalculateDiemTrungBinh(float diemMieng, float diem15Phut, float diemGiuaKy, float diemCuoiKy)` in BUS
- [ ] Formula: 10% Miệng + 20% 15ph + 30% GK + 40% CK
- [ ] Call automatically when inserting/updating DiemSo
- [ ] Test with sample data
- [ ] Update GUI to show calculated result

### Implement Calculate HocLuc
- [ ] Create method `CalculateHocLuc(float diemTrungBinh)` in BUS
- [ ] Criteria:
  - ĐTB >= 8.5 → "Giỏi"
  - ĐTB >= 7.0 → "Khá"
  - ĐTB >= 5.5 → "Trung bình"
  - ĐTB >= 3.5 → "Yếu"
  - ĐTB < 3.5 → "Kém"
- [ ] Auto-update XepLoai table
- [ ] Test with sample data
- [ ] Update GUI

### Implement Classify HanhKiem
- [ ] Create method `ClassifyHanhKiem(float diemTrungBinh, string behavior)` in BUS
- [ ] Criteria:
  - ĐTB >= 8.0 → "Tốt"
  - ĐTB >= 6.5 → "Khá"
  - ĐTB >= 5.0 → "Trung bình"
  - ĐTB < 5.0 → "Yếu"
- [ ] Test with sample data
- [ ] Update GUI

---

## 2. GUI COMPLETION (Day 2)

### DiemSo Module
- [ ] Fix `GUI/DiemSo/DiemSo_NhapDiem.cs`
- [ ] Add export Excel functionality
- [ ] Add export PDF functionality (iTextSharp)
- [ ] Test CRUD operations
- [ ] Test calculation display
- [ ] Add validation messages

### HanhKiem Module
- [ ] Fix `GUI/HanhKiem/HanhKiem.cs`
- [ ] Add view hạnh kiểm
- [ ] Add edit hạnh kiểm
- [ ] Show auto-classification result
- [ ] Test CRUD operations
- [ ] Add filters

### XepLoai Module
- [ ] Fix `GUI/XepLoai/ucXepLoai.cs`
- [ ] Add view xếp loại học lực
- [ ] Show calculated HocLuc
- [ ] Add export functionality
- [ ] Test calculation
- [ ] Add filters

---

## 3. TEACHING ASSIGNMENT (Day 3)

### Conflict Checking
- [ ] Create method `CheckConflict(PhanCongGiangDayDTO phanCong)` in BUS
- [ ] Logic: Check if GV dạy trùng lịch (cùng lớp, cùng thứ, cùng tiết)
- [ ] Return error if conflict
- [ ] Update GUI to show conflicts
- [ ] Test with conflict data
- [ ] Test with no conflict

### PhanCongGiangDay GUI
- [ ] Fix `GUI/PhanCongGiangDay/PhanCongGiangDay.cs`
- [ ] Add conflict validation
- [ ] Add conflict display
- [ ] Test CRUD operations
- [ ] Test conflict checking
- [ ] Add filters

---

## 4. SCHEDULE (TKB) MODULE (Day 3)

### TKB by Class
- [ ] Fix `GUI/ThoiKhoaBieu/ThoiKhoaBieu.cs`
- [ ] Display schedule for a class
- [ ] Add filters (week, semester)
- [ ] Test display
- [ ] Add export functionality
- [ ] Improve UI

### TKB by Teacher
- [ ] Create query to get TKB for a teacher
- [ ] Add GUI to view teacher's schedule
- [ ] Test display
- [ ] Add filters
- [ ] Add export functionality
- [ ] Improve UI

---

## 5. REPORTS & STATISTICS (Day 4)

### PDF Export
- [ ] Implement PDF export using iTextSharp
- [ ] Export bảng điểm
- [ ] Export báo cáo học lực
- [ ] Add formatting
- [ ] Test exports
- [ ] Add error handling

### Statistics Charts
- [ ] Add Chart control to project
- [ ] Create pie chart for Thống kê học lực
- [ ] Create line chart for Thống kê điểm
- [ ] Create bar chart for Thống kê giới tính
- Test display
- Add export functionality

### Reports Module
- [ ] Fix `GUI/BaoCao/ucBaoCao.cs`
- [ ] Add more report types
- [ ] Add filters
- [ ] Add export options
- [ ] Test all reports
- [ ] Improve UI

---

## 6. AUTHENTICATION & SECURITY (Day 5)

### Password Security
- [ ] Hash passwords (SHA256 or BCrypt)
- [ ] Update login logic
- [ ] Update seed data with hashed passwords
- [ ] Test login with hashed passwords
- [ ] Test forgot password
- [ ] Test change password

### RBAC Implementation
- [ ] Implement menu visibility by role
- [ ] Hide/show menu items based on user role
- [ ] Test with admin user
- [ ] Test with teacher user
- [ ] Test with student user
- [ ] Test with parent user

### Session Management
- [ ] Create Session table in database
- [ ] Track login/logout
- [ ] Implement session timeout
- [ ] Test session management
- [ ] Add logging

---

## 7. POLISH & UI IMPROVEMENTS (Day 5)

### UI Fixes
- [ ] Fix UI inconsistencies
- [ ] Add tooltips
- [ ] Improve error messages
- [ ] Add loading indicators
- [ ] Improve button styles
- [ ] Fix responsive layout

### Error Handling
- [ ] Add consistent error handling
- [ ] Add user-friendly error messages
- [ ] Log errors to file
- [ ] Test error scenarios
- [ ] Add error recovery

---

## 8. TESTING (Day 2-7)

### Unit Tests
- [ ] Write unit tests for BUS layer (DiemSo, HanhKiem, XepLoai)
- [ ] Write unit tests for calculation methods
- [ ] Test conflict checking logic
- [ ] Run all unit tests
- [ ] Fix failing tests
- [ ] Achieve 80% code coverage

### Integration Tests
- [ ] Test end-to-end workflow: Login → Dashboard → HocSinh
- [ ] Test workflow: Thêm HS → Phân lớp → Nhập điểm → Xếp loại
- [ ] Test export functions
- [ ] Test RBAC
- [ ] Test error handling
- [ ] Run all integration tests

### Smoke Tests
- [ ] Run smoke test suite (see SMOKE_TEST.md)
- [ ] Verify all critical paths
- [ ] Fix critical bugs
- [ ] Re-test
- [ ] Document results

### Manual Tests
- [ ] Test all CRUD operations for each module
- [ ] Test search/filter functionality
- [ ] Test export functionality
- [ ] Test RBAC
- [ ] Test error scenarios
- [ ] Document test results

---

## 9. DOCUMENTATION (Day 6)

### Screenshots
- [ ] Take screenshot of Login screen
- [ ] Take screenshot of Dashboard
- [ ] Take screenshot of HocSinh module
- [ ] Take screenshot of GiaoVien module
- [ ] Take screenshot of MonHoc module
- [ ] Take screenshot of LopHoc module
- [ ] Take screenshot of DiemSo module
- [ ] Take screenshot of HanhKiem module
- [ ] Take screenshot of XepLoai module
- [ ] Take screenshot of ThoiKhoaBieu module
- [ ] Take screenshot of Reports
- [ ] Save to `docs/screenshots/`

### Documentation Updates
- [ ] Update README.md
- [ ] Create setup guide
- [ ] Create user manual
- [ ] Create API documentation
- [ ] Update INVENTORY.md
- [ ] Update PROGRESS.md
- [ ] Update COVERAGE.md
- [ ] Update CODE_HEALTH.md

---

## 10. RELEASE PREPARATION (Day 7)

### Final Testing
- [ ] Run all tests
- [ ] Fix remaining bugs
- [ ] Re-test
- [ ] Verify all features working
- [ ] Check performance
- [ ] Check security

### Release Package
- [ ] Create release package
- [ ] Include all dependencies
- [ ] Create installer (optional)
- [ ] Create user guide
- [ ] Create changelog
- [ ] Create demo video

### Final Review
- [ ] Code review
- [ ] Documentation review
- [ ] Test review
- [ ] Security review
- [ ] Performance review
- [ ] Stakeholder approval

---

## 11. TRACKING

### Progress Tracking

**Daily:**
- Update completion status
- Log hours worked
- Note blockers
- Update estimate

**Weekly:**
- Review progress
- Adjust plan if needed
- Report to stakeholders

### Metrics

**Tasks:** 78  
**Completed:** 0  
**In Progress:** 0  
**Blocked:** 0  
**Remaining:** 78

**Completion:** 0%  
**Target:** 100% by end of week

---

## 12. PRIORITY RANKING

### P0 - Critical (Must Have)
1. Implement DiemTrungBinh calculation
2. Implement HocLuc calculation
3. Implement HanhKiem classification
4. Fix password security
5. Complete core GUI modules

### P1 - High (Should Have)
6. Conflict checking for PhanCongGiangDay
7. Complete TKB module
8. PDF export
9. Charts & statistics
10. RBAC implementation

### P2 - Medium (Nice to Have)
11. Session management
12. Advanced reports
13. Import functionality
14. Email notifications
15. Backup/restore

### P3 - Low (Future)
16. Auto-scheduler
17. Mobile app
18. API layer
19. Performance optimization
20. Advanced analytics

---

## 13. DEFINITION OF DONE

For each task:
- [ ] Code complete
- [ ] Tested
- [ ] Reviewed
- [ ] Documented
- [ ] No bugs

For release:
- [ ] All P0 tasks done
- [ ] All P1 tasks done (or documented why not)
- [ ] All tests passing
- [ ] Documentation complete
- [ ] Stakeholder approval
