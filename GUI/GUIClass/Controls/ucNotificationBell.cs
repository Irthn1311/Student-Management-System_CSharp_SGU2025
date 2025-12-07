using Student_Management_System_CSharp_SGU2025.BUS;
using Student_Management_System_CSharp_SGU2025.BUS.Utils;
using Student_Management_System_CSharp_SGU2025.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Student_Management_System_CSharp_SGU2025.GUI.Controls
{
    /// <summary>
    /// UserControl hiển thị notification bell với badge số thông báo mới
    /// </summary>
    public partial class ucNotificationBell : UserControl
    {
        private ThongBaoBUS thongBaoBUS;
        private Timer refreshTimer;
        private int soThongBaoChuaDoc = 0;
        private Panel dropdownPanel;
        private bool isDropdownVisible = false;

        public ucNotificationBell()
        {
            InitializeComponent();
            thongBaoBUS = new ThongBaoBUS();
            
            // Timer để refresh mỗi 30 giây
            refreshTimer = new Timer();
            refreshTimer.Interval = 30000; // 30 seconds
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
        }

        private void ucNotificationBell_Load(object sender, EventArgs e)
        {
            LoadNotificationCount();
            SetupDropdown();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            LoadNotificationCount();
        }

        private void LoadNotificationCount()
        {
            try
            {
                if (!SessionManager.IsLoggedIn())
                    return;

                soThongBaoChuaDoc = thongBaoBUS.DemThongBaoChuaDoc(SessionManager.TenDangNhap);
                UpdateBadge();
            }
            catch (Exception ex)
            {
                // Silent fail - không hiển thị lỗi cho user
                Console.WriteLine("Lỗi load notification count: " + ex.Message);
            }
        }

        private void UpdateBadge()
        {
            if (lblBadge != null)
            {
                if (soThongBaoChuaDoc > 0)
                {
                    lblBadge.Visible = true;
                    lblBadge.Text = soThongBaoChuaDoc > 99 ? "99+" : soThongBaoChuaDoc.ToString();
                }
                else
                {
                    lblBadge.Visible = false;
                }
            }
        }

        private void SetupDropdown()
        {
            dropdownPanel = new Panel
            {
                Size = new Size(350, 400),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false,
                AutoScroll = true
            };

            // Thêm dropdown vào parent form
            if (this.Parent != null)
            {
                this.Parent.Controls.Add(dropdownPanel);
                dropdownPanel.BringToFront();
            }
        }

        private void btnBell_Click(object sender, EventArgs e)
        {
            ToggleDropdown();
        }

        private void ToggleDropdown()
        {
            if (dropdownPanel == null) return;

            isDropdownVisible = !isDropdownVisible;
            dropdownPanel.Visible = isDropdownVisible;

            if (isDropdownVisible)
            {
                // Đặt vị trí dropdown bên dưới bell
                Point bellLocation = this.PointToScreen(Point.Empty);
                Point parentLocation = this.Parent.PointToScreen(Point.Empty);
                
                dropdownPanel.Location = new Point(
                    bellLocation.X - parentLocation.X - dropdownPanel.Width + this.Width,
                    bellLocation.Y - parentLocation.Y + this.Height + 5
                );

                LoadDropdownContent();
            }
        }

        private void LoadDropdownContent()
        {
            try
            {
                dropdownPanel.Controls.Clear();

                // Header
                var header = new Label
                {
                    Text = "Thông báo mới",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Location = new Point(10, 10),
                    AutoSize = true
                };
                dropdownPanel.Controls.Add(header);

                // Lấy 5 thông báo gần nhất
                var danhSach = thongBaoBUS.LayDanhSachThongBao(
                    SessionManager.TenDangNhap,
                    null, false, null, 1, 5
                );

                int yPos = 45;
                if (danhSach.Count == 0)
                {
                    var lblEmpty = new Label
                    {
                        Text = "Không có thông báo mới",
                        Location = new Point(10, yPos),
                        AutoSize = true,
                        ForeColor = Color.Gray
                    };
                    dropdownPanel.Controls.Add(lblEmpty);
                }
                else
                {
                    foreach (var tb in danhSach)
                    {
                        var item = CreateNotificationItem(tb, yPos);
                        dropdownPanel.Controls.Add(item);
                        yPos += item.Height + 5;
                    }
                }

                // Footer - link xem tất cả
                var linkViewAll = new LinkLabel
                {
                    Text = "Xem tất cả thông báo",
                    Location = new Point(10, yPos + 10),
                    AutoSize = true
                };
                linkViewAll.LinkClicked += (s, e) =>
                {
                    // TODO: Mở form quản lý thông báo
                    ToggleDropdown();
                };
                dropdownPanel.Controls.Add(linkViewAll);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load thông báo: " + ex.Message);
            }
        }

        private Panel CreateNotificationItem(ThongBaoDTO tb, int yPos)
        {
            var panel = new Panel
            {
                Size = new Size(330, 80),
                Location = new Point(5, yPos),
                BackColor = Color.FromArgb(250, 250, 250),
                Cursor = Cursors.Hand
            };

            var lblTitle = new Label
            {
                Text = tb.TieuDe,
                Location = new Point(10, 10),
                Size = new Size(310, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoEllipsis = true
            };

            var lblContent = new Label
            {
                Text = tb.NoiDung != null && tb.NoiDung.Length > 60
                    ? tb.NoiDung.Substring(0, 60) + "..."
                    : tb.NoiDung,
                Location = new Point(10, 35),
                Size = new Size(310, 20),
                ForeColor = Color.Gray,
                AutoEllipsis = true
            };

            var lblTime = new Label
            {
                Text = tb.GetRelativeTime(),
                Location = new Point(10, 60),
                Size = new Size(310, 15),
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.DarkGray
            };

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblContent);
            panel.Controls.Add(lblTime);

            // Click để xem chi tiết
            panel.Click += (s, e) =>
            {
                // TODO: Mở form chi tiết thông báo
                ToggleDropdown();
            };

            return panel;
        }

        public void RefreshNotifications()
        {
            LoadNotificationCount();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                refreshTimer?.Stop();
                refreshTimer?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer Code (Minimal)

        private System.ComponentModel.IContainer components = null;
        private Guna.UI2.WinForms.Guna2CircleButton btnBell;
        private System.Windows.Forms.Label lblBadge;

        private void InitializeComponent()
        {
            this.btnBell = new Guna.UI2.WinForms.Guna2CircleButton();
            this.lblBadge = new System.Windows.Forms.Label();
            this.SuspendLayout();
            
            // btnBell
            this.btnBell.BackColor = System.Drawing.Color.Transparent;
            this.btnBell.Location = new System.Drawing.Point(0, 0);
            this.btnBell.Name = "btnBell";
            this.btnBell.Size = new System.Drawing.Size(40, 40);
            this.btnBell.TabIndex = 0;
            this.btnBell.Click += new System.EventHandler(this.btnBell_Click);
            // TODO: Set icon from resources
            
            // lblBadge
            this.lblBadge.BackColor = System.Drawing.Color.Red;
            this.lblBadge.ForeColor = System.Drawing.Color.White;
            this.lblBadge.Location = new System.Drawing.Point(25, 0);
            this.lblBadge.Name = "lblBadge";
            this.lblBadge.Size = new System.Drawing.Size(20, 20);
            this.lblBadge.TabIndex = 1;
            this.lblBadge.Text = "0";
            this.lblBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBadge.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblBadge.Visible = false;
            
            // ucNotificationBell
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblBadge);
            this.Controls.Add(this.btnBell);
            this.Name = "ucNotificationBell";
            this.Size = new System.Drawing.Size(45, 40);
            this.Load += new System.EventHandler(this.ucNotificationBell_Load);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
