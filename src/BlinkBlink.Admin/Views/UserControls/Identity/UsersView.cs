using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using BlinkBlink.Admin.Services;
using BlinkBlink.Admin.Views.Helpers;
using BlinkBlink.Application.DTOs.Identity;
using BlinkBlink.Domain.Entities.Customers;
using BlinkBlink.Domain.Entities.Identity;

namespace BlinkBlink.Admin.Views.UserControls.Identity;

/// <summary>
/// Màn hình Quản lý Khách hàng & Phân quyền Nhân sự Blink Blink Jewelry.
/// Quản lý hồ sơ nhân viên, phân quyền chức vụ, danh sách khách hàng thân thiết.
/// Khớp 100% bản vẽ kỹ thuật NhanVien và KhachHang.
/// </summary>
public class UsersView : UserControl
{
    private readonly AdminServiceScope _services;
    private TabControl _tabControl = null!;

    // Tab Nhân viên
    private List<NhanVien> _allStaff = new();
    private TextBox _txtSearchStaff = null!;
    private DataGridView _dgvStaff = null!;
    private Label _lblKpiStaffTotal = null!;
    private Label _lblKpiStaffAdmin = null!;

    // Tab Khách hàng
    private List<KhachHang> _allCustomers = new();
    private TextBox _txtSearchCustomer = null!;
    private DataGridView _dgvCustomers = null!;
    private Label _lblKpiCustomerTotal = null!;
    private Label _lblKpiCustomerActive = null!;

    public UsersView()
    {
        _services = AdminServiceScope.Instance;
        InitializeComponents();
        LoadStaffData();
        LoadCustomerData();
    }

    private void InitializeComponents()
    {
        this.Dock = DockStyle.Fill;
        this.BackColor = UITheme.CanvasBackground;
        this.Font = UITheme.RegularFont;

        // Header Panel
        var topHeaderPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 85,
            Padding = new Padding(20, 15, 20, 10),
            BackColor = UITheme.CanvasBackground
        };

        var lblTitle = new Label
        {
            Text = "QUẢN LÝ KHÁCH HÀNG & PHÂN QUYỀN NHÂN SỰ",
            Font = UITheme.HeaderFont,
            ForeColor = UITheme.TextPrimary,
            Location = new Point(20, 12),
            AutoSize = true
        };

        var lblSubTitle = new Label
        {
            Text = "Kiểm soát tài khoản nhân viên, thiết lập vai trò phân quyền và chăm sóc thông tin khách hàng",
            Font = UITheme.RegularFont,
            ForeColor = UITheme.TextSecondary,
            Location = new Point(20, 38),
            AutoSize = true
        };

        topHeaderPanel.Controls.Add(lblTitle);
        topHeaderPanel.Controls.Add(lblSubTitle);

        // Tab Control
        _tabControl = new TabControl
        {
            Dock = DockStyle.Fill,
            Font = new Font(FontFamily.GenericSansSerif, 10F, FontStyle.Regular),
            Padding = new Point(16, 8)
        };

        var tabStaff = new TabPage("👥 Nhân Viên & Phân Quyền");
        tabStaff.BackColor = UITheme.CanvasBackground;
        BuildStaffTab(tabStaff);

        var tabCustomers = new TabPage("👑 Khách Hàng Thân Thiết");
        tabCustomers.BackColor = UITheme.CanvasBackground;
        BuildCustomersTab(tabCustomers);

        _tabControl.TabPages.Add(tabStaff);
        _tabControl.TabPages.Add(tabCustomers);

        this.Controls.Add(_tabControl);
        this.Controls.Add(topHeaderPanel);
    }

    private void BuildStaffTab(TabPage tab)
    {
        var pnlKpis = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 80,
            Padding = new Padding(15, 5, 15, 5),
            BackColor = UITheme.CanvasBackground
        };
        pnlKpis.Controls.Add(CreateKpiCard("Tổng Nhân Viên", out _lblKpiStaffTotal, UITheme.TextPrimary));
        pnlKpis.Controls.Add(CreateKpiCard("Quản Trị Viên (Admin)", out _lblKpiStaffAdmin, UITheme.PrimaryGold));

        var toolbar = UITheme.CreateResponsiveToolbar();

        var lblS = new Label { Text = "Tìm kiếm:", AutoSize = true, ForeColor = UITheme.TextSecondary, Margin = new Padding(0, 8, 6, 0) };
        _txtSearchStaff = new TextBox { Width = 200, Height = 28, PlaceholderText = "Tên, mã NV, username...", Margin = new Padding(0, 4, 15, 4) };
        _txtSearchStaff.TextChanged += (s, e) => FilterStaff();

        var btnAdd = UITheme.CreateFlatButton("+ Thêm Nhân Viên", UITheme.PrimaryGold, Color.Black);
        btnAdd.Margin = new Padding(0, 2, 8, 2);
        btnAdd.Click += BtnAddStaff_Click;

        var btnLock = UITheme.CreateFlatButton("🔒 Khóa Tài Khoản", UITheme.DangerColor, Color.White);
        btnLock.Margin = new Padding(0, 2, 8, 2);
        btnLock.Click += BtnLockStaff_Click;

        var btnReset = UITheme.CreateFlatButton("🔑 Đặt Lại Mật Khẩu", UITheme.DarkHeader, Color.White);
        btnReset.Margin = new Padding(0, 2, 8, 2);
        btnReset.Click += BtnResetPassword_Click;

        var btnRef = UITheme.CreateFlatButton("⟳ Nạp Lại", Color.FromArgb(241, 245, 249), UITheme.TextPrimary);
        btnRef.Margin = new Padding(0, 2, 0, 2);
        btnRef.Click += (s, e) => LoadStaffData();

        toolbar.Controls.Add(lblS);
        toolbar.Controls.Add(_txtSearchStaff);
        toolbar.Controls.Add(btnAdd);
        toolbar.Controls.Add(btnLock);
        toolbar.Controls.Add(btnReset);
        toolbar.Controls.Add(btnRef);

        var pnlGrid = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15, 10, 15, 15), BackColor = UITheme.CanvasBackground };
        var pnlWhite = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(1) };

        _dgvStaff = new DataGridView();
        UITheme.ApplyGridViewStyle(_dgvStaff);
        _dgvStaff.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaNV", HeaderText = "MÃ NHÂN VIÊN", FillWeight = 85, MinimumWidth = 100 });
        _dgvStaff.Columns.Add(new DataGridViewTextBoxColumn { Name = "HoTen", HeaderText = "HỌ VÀ TÊN", FillWeight = 170, MinimumWidth = 180 });
        _dgvStaff.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenDangNhap", HeaderText = "TÊN ĐĂNG NHẬP", FillWeight = 120, MinimumWidth = 130 });
        _dgvStaff.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoDienThoai", HeaderText = "SỐ ĐIỆN THOẠI", FillWeight = 110, MinimumWidth = 115 });
        _dgvStaff.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaVaiTro", HeaderText = "MÃ VAI TRÒ", FillWeight = 85, MinimumWidth = 95 });
        _dgvStaff.Columns.Add(new DataGridViewTextBoxColumn { Name = "LuongCoBan", HeaderText = "LƯƠNG CƠ BẢN (VND)", FillWeight = 120, MinimumWidth = 140, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Font = UITheme.TabularNumericFont, ForeColor = UITheme.PrimaryGold } });
        _dgvStaff.Columns.Add(new DataGridViewTextBoxColumn { Name = "TrangThai", HeaderText = "TRẠNG THÁI", FillWeight = 90, MinimumWidth = 100 });

        pnlWhite.Controls.Add(_dgvStaff);
        pnlGrid.Controls.Add(pnlWhite);

        tab.Controls.Add(pnlGrid);
        tab.Controls.Add(toolbar);
        tab.Controls.Add(pnlKpis);
    }

    private void BuildCustomersTab(TabPage tab)
    {
        var pnlKpis = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 80,
            Padding = new Padding(15, 5, 15, 5),
            BackColor = UITheme.CanvasBackground
        };
        pnlKpis.Controls.Add(CreateKpiCard("Tổng Khách Hàng", out _lblKpiCustomerTotal, UITheme.TextPrimary));
        pnlKpis.Controls.Add(CreateKpiCard("Đang Hoạt Động", out _lblKpiCustomerActive, UITheme.SuccessColor));

        var toolbar = UITheme.CreateResponsiveToolbar();

        var lblS = new Label { Text = "Tìm kiếm:", AutoSize = true, ForeColor = UITheme.TextSecondary, Margin = new Padding(0, 8, 6, 0) };
        _txtSearchCustomer = new TextBox { Width = 200, Height = 28, PlaceholderText = "Tên, mã KH, SĐT...", Margin = new Padding(0, 4, 15, 4) };
        _txtSearchCustomer.TextChanged += (s, e) => FilterCustomers();

        var btnLockCust = UITheme.CreateFlatButton("🔒 Khóa / Mở Khóa", UITheme.DangerColor, Color.White);
        btnLockCust.Margin = new Padding(0, 2, 8, 2);
        btnLockCust.Click += BtnLockCustomer_Click;

        var btnRef = UITheme.CreateFlatButton("⟳ Nạp Lại", Color.FromArgb(241, 245, 249), UITheme.TextPrimary);
        btnRef.Margin = new Padding(0, 2, 0, 2);
        btnRef.Click += (s, e) => LoadCustomerData();

        toolbar.Controls.Add(lblS);
        toolbar.Controls.Add(_txtSearchCustomer);
        toolbar.Controls.Add(btnLockCust);
        toolbar.Controls.Add(btnRef);

        var pnlGrid = new Panel { Dock = DockStyle.Fill, Padding = new Padding(15, 10, 15, 15), BackColor = UITheme.CanvasBackground };
        var pnlWhite = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(1) };

        _dgvCustomers = new DataGridView();
        UITheme.ApplyGridViewStyle(_dgvCustomers);
        _dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaKH", HeaderText = "MÃ KHÁCH HÀNG", FillWeight = 90, MinimumWidth = 100 });
        _dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "HoTen", HeaderText = "HỌ VÀ TÊN", FillWeight = 180, MinimumWidth = 180 });
        _dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenDangNhap", HeaderText = "TÊN ĐĂNG NHẬP", FillWeight = 120, MinimumWidth = 130 });
        _dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoDienThoai", HeaderText = "SỐ ĐIỆN THOẠI", FillWeight = 110, MinimumWidth = 115 });
        _dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgaySinh", HeaderText = "NGÀY SINH", FillWeight = 100, MinimumWidth = 105, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = UITheme.TabularNumericFont } });
        _dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { Name = "TrangThai", HeaderText = "TRẠNG THÁI", FillWeight = 90, MinimumWidth = 100 });

        pnlWhite.Controls.Add(_dgvCustomers);
        pnlGrid.Controls.Add(pnlWhite);

        tab.Controls.Add(pnlGrid);
        tab.Controls.Add(toolbar);
        tab.Controls.Add(pnlKpis);
    }

    private Panel CreateKpiCard(string title, out Label lblValue, Color valueColor)
    {
        var card = new Panel
        {
            Width = 175,
            Height = 70,
            BackColor = Color.White,
            Margin = new Padding(5, 5, 5, 5),
            Padding = new Padding(10, 6, 10, 6)
        };

        var lblT = new Label
        {
            Text = title,
            ForeColor = UITheme.TextSecondary,
            Font = new Font(FontFamily.GenericSansSerif, 8.5F, FontStyle.Regular),
            Dock = DockStyle.Top,
            Height = 18
        };

        lblValue = new Label
        {
            Text = "0",
            ForeColor = valueColor,
            Font = UITheme.KpiNumberFont,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft
        };

        card.Controls.Add(lblValue);
        card.Controls.Add(lblT);
        return card;
    }

    private void LoadStaffData()
    {
        var nv1 = _services.NhanVienRepo.TimTheoMa("NV001");
        var nv2 = _services.NhanVienRepo.TimTheoMa("NV002");
        _allStaff = new List<NhanVien>();
        if (nv1 != null) _allStaff.Add(nv1);
        if (nv2 != null) _allStaff.Add(nv2);

        var viVn = new CultureInfo("vi-VN");
        _lblKpiStaffTotal.Text = _allStaff.Count.ToString("N0", viVn);
        _lblKpiStaffAdmin.Text = _allStaff.Count(n => n.MaVaiTro == "VT01").ToString("N0", viVn);
        FilterStaff();
    }

    private void FilterStaff()
    {
        var q = _txtSearchStaff.Text.Trim().ToLowerInvariant();
        var list = _allStaff.Where(n => string.IsNullOrEmpty(q) ||
                                        n.HoTen.ToLowerInvariant().Contains(q) ||
                                        n.MaNhanVien.ToLowerInvariant().Contains(q) ||
                                        n.TenDangNhap.ToLowerInvariant().Contains(q) ||
                                        n.SoDienThoai.Contains(q)).ToList();

        _dgvStaff.Rows.Clear();
        var viVn = new CultureInfo("vi-VN");

        foreach (var nv in list)
        {
            var rowIdx = _dgvStaff.Rows.Add(
                nv.MaNhanVien,
                nv.HoTen,
                nv.TenDangNhap,
                nv.SoDienThoai,
                nv.MaVaiTro,
                nv.LuongCoBan.ToString("N0", viVn),
                nv.TrangThai ? "Hoạt động" : "Đã khóa"
            );

            if (!nv.TrangThai)
                _dgvStaff.Rows[rowIdx].Cells["TrangThai"].Style.ForeColor = UITheme.DangerColor;
            else
                _dgvStaff.Rows[rowIdx].Cells["TrangThai"].Style.ForeColor = UITheme.SuccessColor;
        }
    }

    private void LoadCustomerData()
    {
        var kh1 = _services.KhachHangRepo.TimTheoMa("KH001");
        var kh2 = _services.KhachHangRepo.TimTheoMa("KH002");
        _allCustomers = new List<KhachHang>();
        if (kh1 != null) _allCustomers.Add(kh1);
        if (kh2 != null) _allCustomers.Add(kh2);

        var viVn = new CultureInfo("vi-VN");
        _lblKpiCustomerTotal.Text = _allCustomers.Count.ToString("N0", viVn);
        _lblKpiCustomerActive.Text = _allCustomers.Count(c => c.TrangThai).ToString("N0", viVn);
        FilterCustomers();
    }

    private void FilterCustomers()
    {
        var q = _txtSearchCustomer.Text.Trim().ToLowerInvariant();
        var list = _allCustomers.Where(c => string.IsNullOrEmpty(q) ||
                                            c.HoTen.ToLowerInvariant().Contains(q) ||
                                            c.MaKhachHang.ToLowerInvariant().Contains(q) ||
                                            c.TenDangNhap.ToLowerInvariant().Contains(q) ||
                                            c.SoDienThoai.Contains(q)).ToList();

        _dgvCustomers.Rows.Clear();
        foreach (var c in list)
        {
            var rowIdx = _dgvCustomers.Rows.Add(
                c.MaKhachHang,
                c.HoTen,
                c.TenDangNhap,
                c.SoDienThoai,
                c.NgaySinh.ToString("dd/MM/yyyy"),
                c.TrangThai ? "Hoạt động" : "Đã khóa"
            );

            if (!c.TrangThai)
                _dgvCustomers.Rows[rowIdx].Cells["TrangThai"].Style.ForeColor = UITheme.DangerColor;
            else
                _dgvCustomers.Rows[rowIdx].Cells["TrangThai"].Style.ForeColor = UITheme.SuccessColor;
        }
    }

    private void BtnAddStaff_Click(object? sender, EventArgs e)
    {
        using var dialog = new Form
        {
            Text = "Thêm Nhân Viên Mới - Blink Blink Jewelry",
            Width = 440,
            Height = 380,
            StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false,
            BackColor = Color.White
        };

        var lblHead = new Label { Text = "HỒ SƠ NHÂN SỰ VÀ TÀI KHOẢN", Font = UITheme.SubHeaderFont, ForeColor = UITheme.TextPrimary, Location = new Point(25, 18), AutoSize = true };
        dialog.Controls.Add(lblHead);

        int top = 55;
        TextBox AddField(string labelText, string defaultVal)
        {
            var l = new Label { Text = labelText, Location = new Point(25, top), AutoSize = true, ForeColor = UITheme.TextSecondary };
            var t = new TextBox { Text = defaultVal, Location = new Point(160, top - 3), Width = 230 };
            dialog.Controls.Add(l);
            dialog.Controls.Add(t);
            top += 40;
            return t;
        }

        var txtHoTen = AddField("Họ và Tên:", "Nguyễn Hoàng Nam");
        var txtPhone = AddField("Số Điện Thoại:", "0933445566");
        var txtUser = AddField("Tên Đăng Nhập:", "nam.nguyen");
        var txtPass = AddField("Mật Khẩu Khởi Tạo:", "BlinkBlink@2026");
        var txtLuong = AddField("Lương Cơ Bản:", "12000000");

        var btnSave = UITheme.CreateFlatButton("Tạo Nhân Viên", UITheme.PrimaryGold, Color.Black);
        btnSave.Location = new Point(170, top + 15);
        btnSave.Click += (s, ev) =>
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text) || string.IsNullOrWhiteSpace(txtUser.Text))
            {
                MessageBox.Show("Vui lòng điền họ tên và tên đăng nhập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal.TryParse(txtLuong.Text, out var luong);
            var req = new TaoNhanVienDTO
            {
                HoTen = txtHoTen.Text.Trim(),
                SoDienThoai = txtPhone.Text.Trim(),
                TenDangNhap = txtUser.Text.Trim(),
                MatKhau = txtPass.Text.Trim(),
                LuongCoBan = luong,
                MaVaiTro = "VT02"
            };

            var ok = _services.QuanLyNhanVien.TaoNhanVien(req);
            if (ok)
            {
                MessageBox.Show("Tạo hồ sơ và tài khoản nhân viên mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dialog.DialogResult = DialogResult.OK;
                dialog.Close();
            }
            else
            {
                MessageBox.Show("Không thể tạo nhân viên. Vui lòng kiểm tra tên đăng nhập có bị trùng lặp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };

        dialog.Controls.Add(btnSave);

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            LoadStaffData();
        }
    }

    private void BtnLockStaff_Click(object? sender, EventArgs e)
    {
        if (_dgvStaff.SelectedRows.Count == 0)
        {
            MessageBox.Show("Vui lòng chọn nhân viên cần khóa tài khoản.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var maNv = _dgvStaff.SelectedRows[0].Cells["MaNV"].Value?.ToString();
        if (string.IsNullOrEmpty(maNv)) return;

        var ok = _services.QuanLyNhanVien.KhoaTaiKhoanNhanVien(maNv);
        if (ok)
        {
            MessageBox.Show($"Đã thay đổi trạng thái khóa tài khoản nhân viên [{maNv}].", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadStaffData();
        }
        else
        {
            MessageBox.Show("Không tìm thấy nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnResetPassword_Click(object? sender, EventArgs e)
    {
        if (_dgvStaff.SelectedRows.Count == 0)
        {
            MessageBox.Show("Vui lòng chọn nhân viên cần đặt lại mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var maNv = _dgvStaff.SelectedRows[0].Cells["MaNV"].Value?.ToString();
        if (string.IsNullOrEmpty(maNv)) return;

        var ok = _services.QuanLyNhanVien.ResetMatKhau(maNv);
        if (ok)
        {
            MessageBox.Show($"Đã đặt lại mật khẩu mặc định cho nhân viên [{maNv}]: 123456", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void BtnLockCustomer_Click(object? sender, EventArgs e)
    {
        if (_dgvCustomers.SelectedRows.Count == 0)
        {
            MessageBox.Show("Vui lòng chọn khách hàng cần thao tác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var maKh = _dgvCustomers.SelectedRows[0].Cells["MaKH"].Value?.ToString();
        if (string.IsNullOrEmpty(maKh)) return;

        var ok = _services.QuanLyKhachHang.KhoaTaiKhoanKhachHang(maKh);
        if (ok)
        {
            MessageBox.Show($"Đã thay đổi trạng thái khóa tài khoản khách hàng [{maKh}].", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadCustomerData();
        }
    }
}
