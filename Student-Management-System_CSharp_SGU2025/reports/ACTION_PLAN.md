# ACTION PLAN - KẾ HOẠCH HOÀN THÀNH TRONG TUẦN

**Project:** Student Management System CSharp SGU2025  
**Current Progress:** 52%  
**Target:** 95%  
**Timeline:** 1 tuần (7 ngày)  
**Date:** ${new Date().toISOString().split('T')[0]}

---

## 1. ROADMAP TỪNG NGÀY

### Day 1 (Monday) - Critical Business Logic

**Duration:** 8 hours  
**Focus:** Implement missing business logic

#### Morning (4h)
- [ ] **08:00-10:00** - Fix database issues (triggers, constraints, indexes)
  - Create trigger for `DiemTrungBinh` calculation
  - Add CHECK constraints for `DiemSo` (0-10)
  - Add missing indexes (see DB_AUDIT.md)
- [ ] **10:00-12:00** - Implement calculate `DiemTrungBinh` in BUS layer
  - File: `bus/ThemDiemBUS.cs` or new method in `DiemSoDAO.cs`
  - Formula: 10% Miệng + 20% 15ph + 30% GK + 40% CK

#### Afternoon (4h)
- [ ] **13:00-15:00** - Implement calculate `HocLuc` (Ranking)
  - File: `bus/HanhKiemBUS.cs` or create new `XepLoaiBUS.cs`
  - Formula: ĐTB >=8.5 → "Giỏi", >=7.0 → "Khá", >=5.5 → "TB", >=3.5 → "Yếu", <3.5 → "Kém"
- [ ] **15:00-17:00** - Implement auto-classify `HanhKiem`
  - File: `bus/HanhKiemBUS.cs`
  - Auto-based on ĐTB and behavior

**Deliverables:**
- ✅ Database improved
- ✅ DiemTrungBinh calculated automatically
- ✅ HocLuc calculated
- ✅ HanhKiem auto-classified

---

### Day 2 (Tuesday) - Complete Grade & Ranking Modules

**Duration:** 8 hours  
**Focus:** Complete DiemSo, HanhKiem, XepLoai modules

#### Morning (4h)
- [ ] **08:00-10:00** - Complete DiemSo GUI
  - Fix `GUI/DiemSo/DiemSo_NhapDiem.cs`
  - Add export functionality
  - Test CRUD operations
- [ ] **10:00-12:00** - Complete HanhKiem GUI
  - Fix `GUI/HanhKiem/HanhKiem.cs`
  - Add view/edit hạnh kiểm
  - Test auto-classify

#### Afternoon (4h)
- [ ] **13:00-15:00** - Complete XepLoai module
  - Fix `GUI/XepLoai/ucXepLoai.cs`
  - Add view xếp loại học lực
  - Test calculation
- [ ] **15:00-17:00** - Integration testing
  - Test workflow: Nhập điểm → Tính ĐTB → Xếp loại HK → Xếp loại HL

**Deliverables:**
- ✅ DiemSo module complete (95%)
- ✅ HanhKiem module complete (90%)
- ✅ XepLoai module complete (90%)
- ✅ All tests passing

---

### Day 3 (Wednesday) - Teaching Assignment & Schedule

**Duration:** 8 hours  
**Focus:** Fix PhanCongGiangDay and ThoiKhoaBieu

#### Morning (4h)
- [ ] **08:00-10:00** - Implement conflict checking
  - File: `bus/PhanCongGiangDayBUS.cs`
  - Check: GV không dạy 2 lớp cùng 1 tiết
  - Return error if conflict
- [ ] **10:00-12:00** - Complete PhanCongGiangDay GUI
  - Add validation
  - Add conflict display
  - Test

#### Afternoon (4h)
- [ ] **13:00-15:00** - Complete TKB by class
  - Fix `GUI/ThoiKhoaBieu/ThoiKhoaBieu.cs`
  - Display schedule for a class
  - Add filters (week, semester)
- [ ] **15:00-17:00** - Implement TKB by teacher
  - Add new query to get TKB for a teacher
  - Add GUI to view teacher's schedule

**Deliverables:**
- ✅ PhanCongGiangDay with conflict checking (90%)
- ✅ TKB by class complete
- ✅ TKB by teacher complete (80%)
- ✅ No conflicts in schedule

---

### Day 4 (Thursday) - Reports & Statistics

**Duration:** 8 hours  
**Focus:** Complete Reports module

#### Morning (4h)
- [ ] **08:00-10:00** - Implement PDF export
  - Use iTextSharp (already in packages)
  - Export bảng điểm
  - Export báo cáo học lực
- [ ] **10:00-12:00** - Add statistics charts
  - Use Chart control
  - Thống kê học lực (pie chart)
  - Thống kê điểm (line chart)

#### Afternoon (4h)
- [ ] **13:00-15:00** - Complete Báo cáo module
  - Fix `GUI/BaoCao/ucBaoCao.cs`
  - Add filters
  - Add export options
- [ ] **15:00-17:00** - Testing
  - Test all report types
  - Test export functions

**Deliverables:**
- ✅ PDF export working
- ✅ Charts displaying
- ✅ Báo cáo module complete (85%)
- ✅ All exports tested

---

### Day 5 (Friday) - Authentication & Polish

**Duration:** 8 hours  
**Focus:** Fix auth, RBAC, and polish

#### Morning (4h)
- [ ] **08:00-10:00** - Fix password security
  - Hash passwords (SHA256 or BCrypt)
  - Update seed data
  - Test login
- [ ] **10:00-12:00** - Complete RBAC
  - Implement menu visibility by role
  - Test with different users

#### Afternoon (4h)
- [ ] **13:00-15:00** - Polish & UI fixes
  - Fix UI inconsistencies
  - Add tooltips
  - Improve error messages
- [ ] **15:00-17:00** - Final testing
  - Run smoke tests
  - Fix bugs found

**Deliverables:**
- ✅ Authentication secure
- ✅ RBAC working
- ✅ UI polished
- ✅ All bugs fixed

---

### Day 6 (Saturday) - Documentation & Screenshots

**Duration:** 6 hours  
**Focus:** Documentation

#### Morning (3h)
- [ ] **08:00-11:00** - Create screenshots
  - Take screenshots of all modules
  - Save to `docs/screenshots/`
  - Create user guide

#### Afternoon (3h)
- [ ] **13:00-16:00** - Final documentation
  - Update README.md
  - Update setup guide
  - Create API documentation

**Deliverables:**
- ✅ Screenshots complete
- ✅ Documentation complete
- ✅ README.md updated

---

### Day 7 (Sunday) - Final Testing & Release

**Duration:** 6 hours  
**Focus:** Testing & deployment

#### Morning (3h)
- [ ] **08:00-11:00** - Integration testing
  - Test end-to-end scenarios
  - Run all test cases
  - Fix any remaining bugs

#### Afternoon (3h)
- [ ] **13:00-16:00** - Deploy & demo
  - Prepare release package
  - Create demo video
  - Final review

**Deliverables:**
- ✅ All tests passing
- ✅ Release ready
- ✅ Demo ready

---

## 2. PHÂN CÔNG THEO VAI TRÒ

### 2.1 Backend Developer (Focus: DAO/BUS/Database)

**Tasks:**
- Day 1: Database improvements, triggers
- Day 1-2: Business logic implementation (DiemTrungBinh, HocLuc)
- Day 3: Conflict checking algorithms
- Day 4: Statistics queries
- Day 5: Authentication improvements

**Estimated:** 30 hours

### 2.2 Frontend Developer (Focus: GUI)

**Tasks:**
- Day 2: DiemSo, HanhKiem, XepLoai GUI
- Day 3: PhanCongGiangDay, TKB GUI
- Day 4: Reports GUI, Charts
- Day 5: UI polish, error messages

**Estimated:** 25 hours

### 2.3 Full-Stack Developer (Focus: Integration & Testing)

**Tasks:**
- All days: Integration testing
- Day 6: Documentation
- Day 7: Final testing & release

**Estimated:** 20 hours

---

## 3. DEFINITION OF DONE (DoD)

### 3.1 For Each Feature

- [ ] ✅ DAO layer complete (CRUD)
- [ ] ✅ BUS layer complete (validation & logic)
- [ ] ✅ GUI form complete (all fields, buttons)
- [ ] ✅ Data binding working
- [ ] ✅ Error handling implemented
- [ ] ✅ Success/error messages displayed
- [ ] ✅ RBAC working (menu visibility)
- [ ] ✅ Manual test passed
- [ ] ✅ No console errors
- [ ] ✅ Screenshot taken

### 3.2 For Each Module

- [ ] ✅ All features working
- [ ] ✅ Integration tested
- [ ] ✅ Documentation updated
- [ ] ✅ No critical bugs
- [ ] ✅ Performance acceptable
- [ ] ✅ Code reviewed

### 3.3 For Release

- [ ] ✅ All modules ≥90%
- [ ] ✅ All smoke tests passing
- [ ] ✅ Security audit passed
- [ ] ✅ Performance acceptable
- [ ] ✅ Documentation complete
- [ ] ✅ Screenshots ready
- [ ] ✅ Demo video ready

---

## 4. RỦI RO & PHƯƠNG ÁN DỰ PHÒNG

### 4.1 Risks

**Risk 1: Thiếu seed data**
- **Probability:** High
- **Impact:** High
- **Mitigation:** Create comprehensive seed data script (500 HS, full DiemSo, TKB, etc.)

**Risk 2: Bug trong business logic**
- **Probability:** Medium
- **Impact:** High
- **Mitigation:** Unit tests, integration tests, peer review

**Risk 3: Performance issues**
- **Probability:** Low
- **Impact:** Medium
- **Mitigation:** Add indexes, optimize queries, load testing

**Risk 4: UI/UX issues**
- **Probability:** Medium
- **Impact:** Low
- **Mitigation:** Usability testing, iterate on design

**Risk 5: Missing functionality**
- **Probability:** Low
- **Impact:** High
- **Mitigation:** Clear requirements, check against COVERAGE.md

### 4.2 Contingency Plans

**If behind schedule:**
- Focus on critical features only
- Postpone nice-to-have features
- Extend deadline if necessary

**If critical bug found:**
- Stop new development
- Fix bug immediately
- Re-test affected areas

**If database issues:**
- Use backup
- Fix schema issues
- Re-seed data

---

## 5. MILESTONES

### Milestone 1: Business Logic Complete (End of Day 1)
- ✅ DiemTrungBinh calculated
- ✅ HocLuc calculated
- ✅ HanhKiem classified
- ✅ Database improved

**Progress:** 52% → 65%

### Milestone 2: Modules Complete (End of Day 2)
- ✅ DiemSo module complete
- ✅ HanhKiem module complete
- ✅ XepLoai module complete

**Progress:** 65% → 80%

### Milestone 3: Advanced Features (End of Day 3)
- ✅ Conflict checking working
- ✅ TKB complete
- ✅ PhanCongGiangDay complete

**Progress:** 80% → 88%

### Milestone 4: Reports & Polish (End of Day 4)
- ✅ Reports complete
- ✅ PDF export working
- ✅ Charts displaying

**Progress:** 88% → 92%

### Milestone 5: Security & Final Polish (End of Day 5)
- ✅ Authentication secure
- ✅ RBAC complete
- ✅ UI polished

**Progress:** 92% → 95%

### Milestone 6: Release Ready (End of Day 7)
- ✅ All tests passing
- ✅ Documentation complete
- ✅ Demo ready

**Progress:** 95% → 95% (release)

---

## 6. DAILY STANDUPS

### Daily Agenda (Every Day)

**9:00 AM - Standup (15 minutes)**
- What did you do yesterday?
- What will you do today?
- Any blockers?

**Blockers resolution:**
- Solve immediately if possible
- Escalate if needed
- Document in BUGS.md

---

## 7. CHECKLIST TUẦN NÀY

### 7.1 Code Quality

- [ ] All code follows style guide
- [ ] No magic numbers
- [ ] No code duplication
- [ ] Error handling consistent
- [ ] Comments added

### 7.2 Database

- [ ] All triggers created
- [ ] All constraints added
- [ ] All indexes added
- [ ] Seed data complete
- [ ] Backup script ready

### 7.3 Testing

- [ ] Unit tests written (where applicable)
- [ ] Integration tests passing
- [ ] Smoke tests passing
- [ ] Manual tests done
- [ ] Bug fixes verified

### 7.4 Documentation

- [ ] INVENTORY.md updated
- [ ] DB_AUDIT.md updated
- [ ] COVERAGE.md updated
- [ ] PROGRESS.md updated
- [ ] CODE_HEALTH.md updated
- [ ] TODO.md updated
- [ ] SMOKE_TEST.md updated
- [ ] BUGS.md updated
- [ ] README.md updated

### 7.5 Release

- [ ] All features working
- [ ] All bugs fixed
- [ ] Screenshots taken
- [ ] Demo video recorded
- [ ] Release package created

---

## 8. SUCCESS CRITERIA

### 8.1 Minimum Viable Product (MVP)

- [ ] Core modules working (HS, GV, Lop, Mon, Diem)
- [ ] Basic authentication
- [ ] Basic reports
- [ ] No critical bugs

**Must have:** CRUD for core entities  
**Should have:** Reports, RBAC  
**Nice to have:** Charts, advanced reports

### 8.2 Release Criteria

- [ ] All modules ≥90% complete
- [ ] All tests passing
- [ ] Security audit passed
- [ ] Performance acceptable
- [ ] Documentation complete

**Acceptance:** Stakeholder approval

---

## 9. COMMUNICATION

### 9.1 Daily Updates

- **Morning:** Update progress in PROGRESS.md
- **Evening:**** Update TODO.md
- **Daily:**** Standup meeting

### 9.2 Reporting

- **Weekly:** Summary report
- **Issues:** Log in BUGS.md
- **Changes:** Update relevant docs

---

## 10. REVIEW & RETROSPECTIVE

### 10.1 End of Week Review

**Questions:**
- What went well?
- What didn't go well?
- What would we do differently?
- Action items for next time?

### 10.2 Metrics

- **Tasks completed:** ?
- **Bugs found:** ?
- **Bugs fixed:** ?
- **Time spent:** ~56 hours
- **Quality score:** TBD

---

## 11. CONCLUSION

**Target:** 95% completion by end of week  
**Current:** 52%  
**Gap:** +43% (73 points)  
**Timeline:** 7 days  
**Effort:** ~56 hours  
**Confidence:** 85%

**Next Steps:**
1. Start Day 1 tasks
2. Update progress daily
3. Address blockers immediately
4. Test continuously
5. Release on time
