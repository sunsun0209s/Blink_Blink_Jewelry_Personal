using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BlinkBlink.Admin.Services;
using BlinkBlink.Admin.Views.Helpers;
using BlinkBlink.Application.DTOs.Warranty;
using BlinkBlink.Domain.Entities.Warranty;

namespace BlinkBlink.Admin.Views.UserControls.Warranty;

/// <summary>
/// Màn hình Quản lý Dịch vụ & Bảo hành trang sức Blink Blink Jewelry.
/// Thực hiện tiếp nhận bảo dưỡng, sửa chữa, theo dõi tiến độ kỹ thuật và quyết toán chi phí kim hoàn.
/// Khớp 100% bản vẽ kỹ thuật PhieuBaoHanh và ma trận phân quyền 3 vai trò ADMIN, SALE, TECHNICIAN.
/// </summary>
public class WarrantyView : UserControl
{
    private readonly AdminServiceScope _services;
    private readonly CultureInfo _viVn = new("vi-VN");
    private List<PhieuBaoHanh> _allTickets = new();

    private TextBox _txtSearch = null!;
    private ComboBox _cboStatus = null!;

    // 5 nút chức năng chính theo đặc tả phân quyền
    private Button _btnNew = null!;          // Tiếp nhận mới (ADMIN, SALE)
    private Button _btnProcessing = null!;   // Đang xử lý (ADMIN, TECHNICIAN)
    private Button _btnComplete = null!;     // Đã xử lý (ADMIN, TECHNICIAN)
    private Button _btnFinish = null!;       // Hoàn tất & trả khách (ADMIN, SALE)
    private Button _btnCancelTicket = null!; // Hủy phiếu (ADMIN, SALE)
    private Button _btnRefresh = null!;

    private Label _lblKpiTotal = null!;
    private Label _lblKpiProcessing = null!;
    private Label _lblKpiCompleted = null!;

    private DataGridView _dgvWarranty = null!;

    public WarrantyView()
    {
        _services = AdminServiceScope.Instance;
        InitializeComponents();
        LoadWarrantyData();
        ApplyPermissions();
    }

    private void InitializeComponents()
    {
        this.Dock = DockStyle.Fill;
        this.BackColor = UITheme.CanvasBackground;
        this.Font = UITheme.RegularFont;

        // Top Header
        var topHeaderPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 110,
            Padding = new Padding(20, 15, 20, 10),
            BackColor = UITheme.CanvasBackground
        };

        var lblTitle = new Label
        {
            Text = "DỊCH VỤ & TIẾP NHẬN BẢO HÀNH KIM HOÀN",
            Font = UITheme.HeaderFont,
            ForeColor = UITheme.TextPrimary,
            Location = new Point(20, 12),
            AutoSize = true,
            UseMnemonic = false
        };

        var lblSubTitle = new Label
        {
            Text = "Quản lý bảo dưỡng trang sức, xi mạ, gắn đá quý, điều chỉnh kích cỡ và quyết toán chi phí dịch vụ hậu mãi",
            Font = UITheme.RegularFont,
            ForeColor = UITheme.TextSecondary,
            Location = new Point(20, 38),
            AutoSize = true,
            UseMnemonic = false
        };

        var pnlKpis = new FlowLayoutPanel
        {
            Dock = DockStyle.Right,
            Width = 520,
            Height = 90,
            FlowDirection = FlowDirection.LeftToRight
        };

        pnlKpis.Controls.Add(CreateKpiCard("Tổng Phiếu", out _lblKpiTotal, UITheme.TextPrimary));
        pnlKpis.Controls.Add(CreateKpiCard("Đang Xử Lý", out _lblKpiProcessing, UITheme.WarningColor));
        pnlKpis.Controls.Add(CreateKpiCard("Đã Hoàn Tất", out _lblKpiCompleted, UITheme.SuccessColor));

        topHeaderPanel.Controls.Add(lblTitle);
        topHeaderPanel.Controls.Add(lblSubTitle);
        topHeaderPanel.Controls.Add(pnlKpis);

        // Toolbar Panel Responsive chống tràn nút
        var toolbarPanel = UITheme.CreateResponsiveToolbar();

        var lblSearch = new Label
        {
            Text = "Tìm kiếm:",
            AutoSize = true,
            ForeColor = UITheme.TextSecondary,
            Margin = new Padding(0, 8, 6, 0)
        };

        _txtSearch = new TextBox
        {
            Width = 170,
            Height = 28,
            Font = UITheme.RegularFont,
            PlaceholderText = "Số phiếu, mã SP, khách...",
            Margin = new Padding(0, 4, 15, 4)
        };
        _txtSearch.TextChanged += (s, e) => FilterWarranty();

        var lblStatus = new Label
        {
            Text = "Trạng thái:",
            AutoSize = true,
            ForeColor = UITheme.TextSecondary,
            Margin = new Padding(0, 8, 6, 0)
        };

        _cboStatus = new ComboBox
        {
            Width = 145,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = UITheme.RegularFont,
            Margin = new Padding(0, 4, 15, 4)
        };
        _cboStatus.Items.AddRange(new object[] { "Tất Cả", "TiepNhan", "DangXuLy", "DaXuLy", "DaTraKhach", "DaHuy" });
        _cboStatus.SelectedIndex = 0;
        _cboStatus.SelectedIndexChanged += (s, e) => FilterWarranty();

        // 5 nút chức năng chuẩn theo yêu cầu dự án
        _btnNew = UITheme.CreateFlatButton("+ Tiếp Nhận Mới", UITheme.PrimaryGold, Color.Black);
        _btnNew.Margin = new Padding(0, 2, 8, 2);
        _btnNew.Click += BtnNew_Click;

        _btnProcessing = UITheme.CreateFlatButton("🔧 Đang Xử Lý", UITheme.DarkHeader, Color.White);
        _btnProcessing.Margin = new Padding(0, 2, 8, 2);
        _btnProcessing.Click += BtnProcessing_Click;

        _btnComplete = UITheme.CreateFlatButton("⚙️ Đã Xử Lý", UITheme.PrimaryGold, Color.Black);
        _btnComplete.Margin = new Padding(0, 2, 8, 2);
        _btnComplete.Click += BtnComplete_Click;

        _btnFinish = UITheme.CreateFlatButton("🎉 Hoàn Tất & Trả Khách", UITheme.SuccessColor, Color.White);
        _btnFinish.Margin = new Padding(0, 2, 8, 2);
        _btnFinish.Click += BtnFinish_Click;

        _btnCancelTicket = UITheme.CreateFlatButton("✕ Hủy Phiếu", UITheme.DangerColor, Color.White);
        _btnCancelTicket.Margin = new Padding(0, 2, 8, 2);
        _btnCancelTicket.Click += BtnCancelTicket_Click;

        _btnRefresh = UITheme.CreateFlatButton("⟳ Nạp Lại", Color.FromArgb(241, 245, 249), UITheme.TextPrimary);
        _btnRefresh.Margin = new Padding(0, 2, 0, 2);
        _btnRefresh.Click += (s, e) =>
        {
            LoadWarrantyData();
            ApplyPermissions();
        };

        toolbarPanel.Controls.Add(lblSearch);
        toolbarPanel.Controls.Add(_txtSearch);
        toolbarPanel.Controls.Add(lblStatus);
        toolbarPanel.Controls.Add(_cboStatus);
        toolbarPanel.Controls.Add(_btnNew);
        toolbarPanel.Controls.Add(_btnProcessing);
        toolbarPanel.Controls.Add(_btnComplete);
        toolbarPanel.Controls.Add(_btnFinish);
        toolbarPanel.Controls.Add(_btnCancelTicket);
        toolbarPanel.Controls.Add(_btnRefresh);

        // Data Grid Container
        var pnlGridContainer = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(20, 10, 20, 20),
            BackColor = UITheme.CanvasBackground
        };

        var pnlWhiteCard = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            Padding = new Padding(1)
        };

        _dgvWarranty = new DataGridView();
        UITheme.ApplyGridViewStyle(_dgvWarranty);
        _dgvWarranty.ScrollBars = ScrollBars.Both;
        ConfigureGridColumns();

        pnlWhiteCard.Controls.Add(_dgvWarranty);
        pnlGridContainer.Controls.Add(pnlWhiteCard);

        this.Controls.Add(pnlGridContainer);
        this.Controls.Add(toolbarPanel);
        this.Controls.Add(topHeaderPanel);
    }

    /// <summary>
    /// Áp dụng ma trận phân quyền 3 vai trò trên 5 nút chức năng:
    /// - ADMIN: Toàn quyền cả 5 nút
    /// - SALE: Bấm được Tiếp Nhận Mới, Hoàn Tất & Trả Khách, Hủy Phiếu. Bị khóa: Đang Xử Lý, Đã Xử Lý.
    /// - TECHNICIAN: Bấm được Đang Xử Lý, Đã Xử Lý. Bị khóa: Tiếp Nhận Mới, Hoàn Tất & Trả Khách, Hủy Phiếu.
    /// </summary>
    public void ApplyPermissions(string? role = null)
    {
        var currentRole = (role ?? _services.CurrentUser?.MaVaiTro ?? "ADMIN").ToUpperInvariant();

        switch (currentRole)
        {
            case "ADMIN":
                _btnNew.Enabled = true;
                _btnProcessing.Enabled = true;
                _btnComplete.Enabled = true;
                _btnFinish.Enabled = true;
                _btnCancelTicket.Enabled = true;
                break;

            case "SALE":
                _btnNew.Enabled = true;
                _btnProcessing.Enabled = false;
                _btnComplete.Enabled = false;
                _btnFinish.Enabled = true;
                _btnCancelTicket.Enabled = true;
                break;

            case "TECHNICIAN":
                _btnNew.Enabled = false;
                _btnProcessing.Enabled = true;
                _btnComplete.Enabled = true;
                _btnFinish.Enabled = false;
                _btnCancelTicket.Enabled = false;
                break;

            default:
                _btnNew.Enabled = false;
                _btnProcessing.Enabled = false;
                _btnComplete.Enabled = false;
                _btnFinish.Enabled = false;
                _btnCancelTicket.Enabled = false;
                break;
        }
    }

    private Panel CreateKpiCard(string title, out Label lblValue, Color valueColor)
    {
        var card = new Panel
        {
            Width = 160,
            Height = 75,
            BackColor = Color.White,
            Margin = new Padding(5, 5, 5, 5),
            Padding = new Padding(10, 8, 10, 8)
        };

        var lblT = new Label
        {
            Text = title,
            ForeColor = UITheme.TextSecondary,
            Font = new Font(FontFamily.GenericSansSerif, 8.5F, FontStyle.Regular),
            Dock = DockStyle.Top,
            Height = 20
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

    private void ConfigureGridColumns()
    {
        _dgvWarranty.Columns.Clear();
        _dgvWarranty.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoPhieuBH", HeaderText = "SỐ PHIẾU BH", FillWeight = 85, MinimumWidth = 95 });
        _dgvWarranty.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaSanPham", HeaderText = "MÃ SẢN PHẨM", FillWeight = 85, MinimumWidth = 100 });
        _dgvWarranty.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaKhachHang", HeaderText = "KHÁCH HÀNG", FillWeight = 80, MinimumWidth = 90 });
        _dgvWarranty.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayTiepNhan", HeaderText = "TIẾP NHẬN", FillWeight = 80, MinimumWidth = 90 });
        _dgvWarranty.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayHenTra", HeaderText = "HẸN TRẢ", FillWeight = 80, MinimumWidth = 90 });
        _dgvWarranty.Columns.Add(new DataGridViewTextBoxColumn { Name = "LyDoBaoHanh", HeaderText = "LÝ DO TIẾP NHẬN", FillWeight = 130, MinimumWidth = 130 });
        _dgvWarranty.Columns.Add(new DataGridViewTextBoxColumn { Name = "TinhTrangTiepNhan", HeaderText = "TÌNH TRẠNG KHI NHẬN", FillWeight = 140, MinimumWidth = 130 });
        _dgvWarranty.Columns.Add(new DataGridViewTextBoxColumn { Name = "GhiChu", HeaderText = "GHI CHÚ", FillWeight = 110, MinimumWidth = 100 });
        _dgvWarranty.Columns.Add(new DataGridViewTextBoxColumn { Name = "ChiPhiUocTinh", HeaderText = "CP ƯỚC TÍNH (VND)", FillWeight = 95, MinimumWidth = 110, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Font = UITheme.TabularNumericFont } });
        _dgvWarranty.Columns.Add(new DataGridViewTextBoxColumn { Name = "ChiPhiThucTe", HeaderText = "CP THỰC TẾ (VND)", FillWeight = 100, MinimumWidth = 115, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Font = UITheme.TabularNumericFont, ForeColor = UITheme.PrimaryGold } });
        _dgvWarranty.Columns.Add(new DataGridViewTextBoxColumn { Name = "TrangThaiXuLy", HeaderText = "TRẠNG THÁI", FillWeight = 95, MinimumWidth = 110 });
    }

    private void LoadWarrantyData()
    {
        _allTickets = _services.BaoHanhRepo.LayDanhSachPhieu();
        UpdateKpis();
        FilterWarranty();
    }

    private void UpdateKpis()
    {
        _lblKpiTotal.Text = _allTickets.Count.ToString("N0", _viVn);
        _lblKpiProcessing.Text = _allTickets.Count(t =>
            string.Equals(t.TrangThaiXuLy, "TiepNhan", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(t.TrangThaiXuLy, "DangXuLy", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(t.TrangThaiXuLy, "DangBaoHanh", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(t.TrangThaiXuLy, "DaXuLy", StringComparison.OrdinalIgnoreCase)
        ).ToString("N0", _viVn);

        _lblKpiCompleted.Text = _allTickets.Count(t =>
            string.Equals(t.TrangThaiXuLy, "DaTraKhach", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(t.TrangThaiXuLy, "DaHoanTat", StringComparison.OrdinalIgnoreCase)
        ).ToString("N0", _viVn);
    }

    private void FilterWarranty()
    {
        var query = _txtSearch.Text.Trim().ToLowerInvariant();
        var selectedStatus = _cboStatus.SelectedItem?.ToString() ?? "Tất Cả";

        var filtered = _allTickets.Where(t =>
        {
            var matchSearch = string.IsNullOrEmpty(query) ||
                              t.SoPhieuBH.ToLowerInvariant().Contains(query) ||
                              t.MaSanPham.ToLowerInvariant().Contains(query) ||
                              t.MaKhachHang.ToLowerInvariant().Contains(query) ||
                              t.LyDoBaoHanh.ToLowerInvariant().Contains(query) ||
                              (t.TinhTrangTiepNhan != null && t.TinhTrangTiepNhan.ToLowerInvariant().Contains(query));

            var matchStatus = selectedStatus == "Tất Cả" ||
                              string.Equals(t.TrangThaiXuLy, selectedStatus, StringComparison.OrdinalIgnoreCase) ||
                              (selectedStatus == "DaTraKhach" && string.Equals(t.TrangThaiXuLy, "DaHoanTat", StringComparison.OrdinalIgnoreCase)) ||
                              (selectedStatus == "DangXuLy" && string.Equals(t.TrangThaiXuLy, "DangBaoHanh", StringComparison.OrdinalIgnoreCase));

            return matchSearch && matchStatus;
        }).ToList();

        _dgvWarranty.Rows.Clear();

        foreach (var t in filtered)
        {
            var rowIdx = _dgvWarranty.Rows.Add(
                t.SoPhieuBH,
                t.MaSanPham,
                t.MaKhachHang,
                t.NgayTiepNhan.ToString("dd/MM/yyyy"),
                t.NgayHenTra?.ToString("dd/MM/yyyy") ?? "-",
                t.LyDoBaoHanh,
                t.TinhTrangTiepNhan,
                t.GhiChu,
                t.ChiPhiUocTinh.ToString("N0", _viVn),
                t.ChiPhiThucTe.ToString("N0", _viVn),
                FormatTrangThaiDisplay(t.TrangThaiXuLy)
            );

            var cellStatus = _dgvWarranty.Rows[rowIdx].Cells["TrangThaiXuLy"];
            var rawStatus = t.TrangThaiXuLy;
            if (string.Equals(rawStatus, "TiepNhan", StringComparison.OrdinalIgnoreCase))
                cellStatus.Style.ForeColor = Color.FromArgb(2, 132, 199); // Xanh dương
            else if (string.Equals(rawStatus, "DangXuLy", StringComparison.OrdinalIgnoreCase) || string.Equals(rawStatus, "DangBaoHanh", StringComparison.OrdinalIgnoreCase))
                cellStatus.Style.ForeColor = UITheme.WarningColor; // Cam
            else if (string.Equals(rawStatus, "DaXuLy", StringComparison.OrdinalIgnoreCase))
                cellStatus.Style.ForeColor = UITheme.PrimaryGold; // Vàng kim
            else if (string.Equals(rawStatus, "DaTraKhach", StringComparison.OrdinalIgnoreCase) || string.Equals(rawStatus, "DaHoanTat", StringComparison.OrdinalIgnoreCase))
                cellStatus.Style.ForeColor = UITheme.SuccessColor; // Xanh lá
            else if (string.Equals(rawStatus, "DaHuy", StringComparison.OrdinalIgnoreCase))
                cellStatus.Style.ForeColor = UITheme.DangerColor; // Đỏ
        }
    }

    private static string FormatTrangThaiDisplay(string raw)
    {
        return raw switch
        {
            "TiepNhan" => "Tiếp Nhận",
            "DangXuLy" => "Đang Xử Lý",
            "DangBaoHanh" => "Đang Xử Lý",
            "DaXuLy" => "Đã Xử Lý",
            "DaTraKhach" => "Đã Trả Khách",
            "DaHoanTat" => "Đã Trả Khách",
            "DaHuy" => "Đã Hủy",
            _ => raw
        };
    }

    private PhieuBaoHanh? GetSelectedTicket()
    {
        if (_dgvWarranty.SelectedRows.Count == 0)
        {
            MessageBox.Show("Vui lòng chọn một phiếu bảo hành trong danh sách để thực hiện.", "Chưa chọn phiếu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return null;
        }

        var soPhieu = _dgvWarranty.SelectedRows[0].Cells["SoPhieuBH"].Value?.ToString();
        if (string.IsNullOrEmpty(soPhieu)) return null;

        var ticket = _allTickets.FirstOrDefault(t => t.SoPhieuBH == soPhieu);
        if (ticket == null)
        {
            MessageBox.Show($"Không tìm thấy dữ liệu phiếu [{soPhieu}]. Vui lòng nạp lại danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        return ticket;
    }

    /// <summary>
    /// Chuyển trạng thái phiếu từ TiepNhan sang DangXuLy (Nhân viên kỹ thuật tiếp nhận sửa chữa).
    /// </summary>
    private void BtnProcessing_Click(object? sender, EventArgs e)
    {
        var ticket = GetSelectedTicket();
        if (ticket == null) return;

        if (!string.Equals(ticket.TrangThaiXuLy, "TiepNhan", StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show($"Phiếu bảo hành #{ticket.SoPhieuBH} đang ở trạng thái [{FormatTrangThaiDisplay(ticket.TrangThaiXuLy)}].\nChỉ có thể chuyển sang 'Đang Xử Lý' khi phiếu ở trạng thái 'Tiếp Nhận'.", "Không Thể Chuyển Trạng Thái", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var ok = _services.XuLyBaoHanh.CapNhatTienDo(ticket.SoPhieuBH, "DangXuLy");
        if (ok)
        {
            MessageBox.Show($"Phiếu #{ticket.SoPhieuBH} đã chuyển sang trạng thái: ĐANG XỬ LÝ (Kỹ thuật viên bắt đầu sửa chữa).", "Tiến Độ Bảo Hành", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadWarrantyData();
        }
        else
        {
            MessageBox.Show("Không thể cập nhật trạng thái phiếu bảo hành.", "Lỗi Xử Lý", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Kỹ thuật viên hoàn thành sửa chữa, nhập chi phí thực tế và chuyển trạng thái sang DaXuLy.
    /// </summary>
    private void BtnComplete_Click(object? sender, EventArgs e)
    {
        var ticket = GetSelectedTicket();
        if (ticket == null) return;

        if (!string.Equals(ticket.TrangThaiXuLy, "DangXuLy", StringComparison.OrdinalIgnoreCase) &&
            !string.Equals(ticket.TrangThaiXuLy, "DangBaoHanh", StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show($"Chỉ có thể chuyển sang 'Đã Xử Lý' khi phiếu đang ở trạng thái 'Đang Xử Lý'.\nTrạng thái hiện tại của phiếu #{ticket.SoPhieuBH}: [{FormatTrangThaiDisplay(ticket.TrangThaiXuLy)}].", "Quy Trình Chưa Hợp Lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var uocTinh = ticket.ChiPhiUocTinh;

        using var prompt = new Form
        {
            Text = $"Quyết Toán Chi Phí & Hoàn Thành Kỹ Thuật - Phiếu #{ticket.SoPhieuBH}",
            Width = 460,
            Height = 280,
            StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false,
            BackColor = Color.White
        };

        var lblUocTinh = new Label
        {
            Text = $"Chi phí ước tính ban đầu: {uocTinh:N0} VND",
            Location = new Point(25, 20),
            AutoSize = true,
            ForeColor = UITheme.TextSecondary
        };

        var lblPrompt = new Label
        {
            Text = "Chi phí thực tế kỹ thuật viên đã sửa chữa / thay thế (VND):",
            Location = new Point(25, 52),
            AutoSize = true,
            ForeColor = UITheme.TextPrimary,
            Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
        };

        var txtCost = new TextBox
        {
            Text = uocTinh > 0 ? uocTinh.ToString("F0") : "0",
            Location = new Point(25, 85),
            Width = 390,
            Font = UITheme.TabularNumericFont
        };

        var lblHint = new Label
        {
            Text = "(Nhập 0 nếu lỗi được bảo hành miễn phí theo chính sách của hãng)",
            Location = new Point(25, 118),
            AutoSize = true,
            ForeColor = UITheme.TextSecondary,
            Font = new Font("Segoe UI", 8.5F, FontStyle.Italic)
        };

        var btnOk = UITheme.CreateFlatButton("XÁC NHẬN HOÀN THÀNH KỸ THUẬT (ĐÃ XỬ LÝ)", UITheme.PrimaryGold, Color.Black);
        btnOk.Location = new Point(25, 160);
        btnOk.Size = new Size(390, 42);
        btnOk.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

        btnOk.Click += (s, ev) =>
        {
            if (!decimal.TryParse(txtCost.Text.Trim(), out var cost) || cost < 0)
            {
                MessageBox.Show("Vui lòng nhập chi phí thực tế hợp lệ (số tiền lớn hơn hoặc bằng 0).", "Lỗi Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var ok = _services.XuLyBaoHanh.QuyetToanBaoHanh(ticket.SoPhieuBH, cost);
            if (ok)
            {
                MessageBox.Show($"Phiếu #{ticket.SoPhieuBH} đã quyết toán chi phí thực tế: {cost:N0} VND.\nTrạng thái chuyển sang: ĐÃ XỬ LÝ (Sẵn sàng để bàn giao trả khách).", "Hoàn Tất Xử Lý Kỹ Thuật", MessageBoxButtons.OK, MessageBoxIcon.Information);
                prompt.DialogResult = DialogResult.OK;
                prompt.Close();
            }
            else
            {
                MessageBox.Show("Không thể quyết toán phiếu bảo hành.", "Lỗi Xử Lý", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };

        prompt.Controls.Add(lblUocTinh);
        prompt.Controls.Add(lblPrompt);
        prompt.Controls.Add(txtCost);
        prompt.Controls.Add(lblHint);
        prompt.Controls.Add(btnOk);

        if (prompt.ShowDialog() == DialogResult.OK)
        {
            LoadWarrantyData();
        }
    }

    /// <summary>
    /// Nhân viên bán hàng trả trang sức cho khách.
    /// Yêu cầu phiếu phải ở trạng thái DaXuLy.
    /// Nếu ChiPhiThucTe > 0: Mở popup thanh toán có hỗ trợ QR chuyển khoản và tiền mặt.
    /// Nếu ChiPhiThucTe == 0: Xác nhận bàn giao miễn phí và chuyển sang DaTraKhach.
    /// </summary>
    private void BtnFinish_Click(object? sender, EventArgs e)
    {
        var ticket = GetSelectedTicket();
        if (ticket == null) return;

        if (string.Equals(ticket.TrangThaiXuLy, "DaTraKhach", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(ticket.TrangThaiXuLy, "DaHoanTat", StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show($"Phiếu bảo hành #{ticket.SoPhieuBH} đã được hoàn tất và bàn giao cho khách trước đó.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (!string.Equals(ticket.TrangThaiXuLy, "DaXuLy", StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show($"Phiếu bảo hành chưa được xử lý xong bởi kỹ thuật viên.\nChỉ có thể bàn giao trả khách khi phiếu ở trạng thái 'Đã Xử Lý' (hiện tại: [{FormatTrangThaiDisplay(ticket.TrangThaiXuLy)}]).", "Chưa Thể Trả Khách", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Trường hợp 1: Có phát sinh chi phí thực tế (> 0) -> Bắt buộc thanh toán
        if (ticket.ChiPhiThucTe > 0)
        {
            ShowPaymentAndHandoverDialog(ticket);
        }
        else
        {
            // Trường hợp 2: Miễn phí bảo hành (0 VND) -> Xác nhận bàn giao trực tiếp
            var confirm = MessageBox.Show(
                $"Xác nhận bàn giao trang sức cho khách hàng (Dịch vụ bảo hành miễn phí 0 VND)?\nTrạng thái phiếu sẽ chuyển sang: ĐÃ TRẢ KHÁCH.",
                "Bàn Giao Trang Sức Cho Khách",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                var ok = _services.XuLyBaoHanh.HoanTatBaoHanh(ticket.SoPhieuBH);
                if (ok)
                {
                    MessageBox.Show($"Phiếu bảo hành #{ticket.SoPhieuBH} đã bàn giao cho khách thành công!", "Bàn Giao Hoàn Tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadWarrantyData();
                }
                else
                {
                    MessageBox.Show("Không thể hoàn tất phiếu bảo hành.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    /// <summary>
    /// Popup thanh toán chi phí bảo hành: Hỗ trợ tiền mặt hoặc quét mã QR chuyển khoản ngân hàng.
    /// </summary>
    private void ShowPaymentAndHandoverDialog(PhieuBaoHanh ticket)
    {
        var amount = ticket.ChiPhiThucTe;

        using var payForm = new Form
        {
            Text = $"Thanh Toán Dịch Vụ Bảo Hành - Phiếu #{ticket.SoPhieuBH}",
            Width = 500,
            Height = 560,
            StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false,
            BackColor = Color.White
        };

        var pnlHeader = new Panel { Dock = DockStyle.Top, Height = 65, Padding = new Padding(20, 10, 20, 5), BackColor = Color.FromArgb(248, 250, 252) };
        var lblTitle = new Label { Text = "XÁC NHẬN THANH TOÁN CHI PHÍ BẢO HÀNH", Font = UITheme.SubHeaderFont, ForeColor = UITheme.TextPrimary, AutoSize = true, Location = new Point(20, 8) };
        var lblSub = new Label { Text = $"Phiếu #{ticket.SoPhieuBH} | Số tiền cần thanh toán: {amount:N0} VND", ForeColor = UITheme.PrimaryGold, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold), AutoSize = true, Location = new Point(20, 34) };
        pnlHeader.Controls.Add(lblTitle);
        pnlHeader.Controls.Add(lblSub);

        var pnlBody = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 10, 20, 10), BackColor = Color.White };

        var pnlOptions = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 45, FlowDirection = FlowDirection.LeftToRight };
        var rdoCash = new RadioButton { Text = "💵 Tiền Mặt", Checked = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), AutoSize = true, Margin = new Padding(0, 5, 25, 0) };
        var rdoQr = new RadioButton { Text = "📱 Chuyển Khoản (Quét Mã QR)", Font = new Font("Segoe UI", 10F, FontStyle.Bold), AutoSize = true, Margin = new Padding(0, 5, 0, 0) };
        pnlOptions.Controls.Add(rdoCash);
        pnlOptions.Controls.Add(rdoQr);

        var pnlContent = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };

        // Panel tiền mặt
        var pnlCash = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(240, 253, 244), Padding = new Padding(20), Visible = true };
        var lblCashMsg = new Label
        {
            Text = $"✓ Thu ngân xác nhận thu tiền mặt trực tiếp tại quầy:\n\n   Tổng thanh toán:  {amount.ToString("N0", _viVn)} VND\n\nSau khi thu đủ tiền, vui lòng nhấn 'XÁC NHẬN THANH TOÁN & TRẢ KHÁCH' để hoàn tất.",
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10F, FontStyle.Regular),
            ForeColor = Color.FromArgb(22, 101, 52)
        };
        pnlCash.Controls.Add(lblCashMsg);

        // Panel QR Code
        var pnlQr = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Visible = false };
        var picQr = new PictureBox
        {
            Size = new Size(270, 270),
            Location = new Point(105, 5),
            SizeMode = PictureBoxSizeMode.Zoom,
            BorderStyle = BorderStyle.FixedSingle,
            Image = LoadQrImage()
        };
        var lblQrGuide = new Label
        {
            Text = "Quý khách quét mã QR để chuyển khoản chính xác số tiền dịch vụ.",
            Location = new Point(45, 285),
            AutoSize = true,
            ForeColor = UITheme.TextSecondary,
            Font = new Font("Segoe UI", 9F, FontStyle.Italic)
        };
        pnlQr.Controls.Add(picQr);
        pnlQr.Controls.Add(lblQrGuide);

        rdoCash.CheckedChanged += (s, e) =>
        {
            pnlCash.Visible = rdoCash.Checked;
            pnlQr.Visible = !rdoCash.Checked;
        };
        rdoQr.CheckedChanged += (s, e) =>
        {
            pnlQr.Visible = rdoQr.Checked;
            pnlCash.Visible = !rdoQr.Checked;
        };

        pnlContent.Controls.Add(pnlCash);
        pnlContent.Controls.Add(pnlQr);

        pnlBody.Controls.Add(pnlContent);
        pnlBody.Controls.Add(pnlOptions);

        var pnlBottom = new Panel { Dock = DockStyle.Bottom, Height = 60, Padding = new Padding(20, 10, 20, 10), BackColor = Color.FromArgb(248, 250, 252) };
        var btnConfirm = UITheme.CreateFlatButton("✓ XÁC NHẬN THANH TOÁN & TRẢ KHÁCH", UITheme.PrimaryGold, Color.Black);
        btnConfirm.Dock = DockStyle.Fill;
        btnConfirm.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        btnConfirm.Click += (s, e) =>
        {
            var ok = _services.XuLyBaoHanh.HoanTatBaoHanh(ticket.SoPhieuBH);
            if (ok)
            {
                MessageBox.Show($"Đã xác nhận thanh toán {amount:N0} VND và bàn giao trang sức cho khách hàng thành công!", "Bàn Giao Hoàn Tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                payForm.DialogResult = DialogResult.OK;
                payForm.Close();
            }
            else
            {
                MessageBox.Show("Không thể hoàn tất phiếu bảo hành.", "Lỗi Xử Lý", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };
        pnlBottom.Controls.Add(btnConfirm);

        payForm.Controls.Add(pnlBody);
        payForm.Controls.Add(pnlBottom);
        payForm.Controls.Add(pnlHeader);

        if (payForm.ShowDialog() == DialogResult.OK)
        {
            LoadWarrantyData();
        }
    }

    /// <summary>
    /// Hủy phiếu bảo hành.
    /// </summary>
    private void BtnCancelTicket_Click(object? sender, EventArgs e)
    {
        var ticket = GetSelectedTicket();
        if (ticket == null) return;

        if (string.Equals(ticket.TrangThaiXuLy, "DaTraKhach", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(ticket.TrangThaiXuLy, "DaHoanTat", StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show("Không thể hủy phiếu bảo hành đã bàn giao trả khách hoàn tất.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var confirm = MessageBox.Show(
            $"Bạn có chắc chắn muốn hủy phiếu bảo hành #{ticket.SoPhieuBH} không?",
            "Xác Nhận Hủy Phiếu",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (confirm != DialogResult.Yes) return;

        var ok = _services.XuLyBaoHanh.CapNhatTienDo(ticket.SoPhieuBH, "DaHuy");
        if (ok)
        {
            MessageBox.Show($"Phiếu bảo hành #{ticket.SoPhieuBH} đã được chuyển sang trạng thái ĐÃ HỦY.", "Đã Hủy Phiếu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadWarrantyData();
        }
        else
        {
            MessageBox.Show("Không thể hủy phiếu bảo hành.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Mở dialog tiếp nhận bảo dưỡng / sửa chữa mới.
    /// Hỗ trợ ghi nhận Tình trạng sản phẩm khi nhận, Ghi chú thêm, và chi phí ước tính có thể để trống hoặc = 0.
    /// </summary>
    private void BtnNew_Click(object? sender, EventArgs e)
    {
        using var dialog = new Form
        {
            Text = "Tiếp Nhận Bảo Hành Trang Sức Mới - Blink Blink Jewelry",
            Width = 520,
            Height = 520,
            StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false,
            BackColor = Color.White
        };

        var lblHead = new Label
        {
            Text = "PHIẾU TIẾP NHẬN DỊCH VỤ & BẢO HÀNH",
            Font = UITheme.SubHeaderFont,
            ForeColor = UITheme.TextPrimary,
            Location = new Point(25, 18),
            AutoSize = true
        };
        dialog.Controls.Add(lblHead);

        int top = 55;
        TextBox AddField(string labelText, string defaultVal)
        {
            var l = new Label { Text = labelText, Location = new Point(25, top), AutoSize = true, ForeColor = UITheme.TextSecondary };
            var t = new TextBox { Text = defaultVal, Location = new Point(190, top - 3), Width = 285 };
            dialog.Controls.Add(l);
            dialog.Controls.Add(t);
            top += 40;
            return t;
        }

        var txtMaKH = AddField("Mã Khách Hàng:", "KH001");
        var txtMaSP = AddField("Mã Sản Phẩm:", "SP001");
        var txtLyDo = AddField("Lỗi / Yêu Cầu:", "Đánh bóng, xi mạ làm mới");
        var txtTinhTrang = AddField("Tình Trạng Khi Nhận:", "Mặt nhẫn trầy nhẹ, chấu giữ hơi lỏng");
        var txtGhiChu = AddField("Ghi Chú Thêm:", "Khách cần gấp trước ngày hẹn");
        var txtCostEst = AddField("Chi Phí Ước Tính (VND):", "0");
        var txtHenTra = AddField("Ngày Hẹn Trả:", DateTime.Now.AddDays(3).ToString("dd/MM/yyyy"));

        var lblNote = new Label
        {
            Text = "* Chi phí ước tính có thể để trống hoặc 0 VND (Kỹ thuật sẽ quyết toán thực tế sau)",
            Location = new Point(25, top),
            AutoSize = true,
            ForeColor = UITheme.TextSecondary,
            Font = new Font("Segoe UI", 8.2F, FontStyle.Italic)
        };
        dialog.Controls.Add(lblNote);
        top += 30;

        var btnSave = UITheme.CreateFlatButton("Tạo Phiếu Tiếp Nhận", UITheme.PrimaryGold, Color.Black);
        btnSave.Location = new Point(190, top + 10);
        btnSave.Size = new Size(285, 40);
        btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

        btnSave.Click += (s, ev) =>
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text) || string.IsNullOrWhiteSpace(txtMaSP.Text) || string.IsNullOrWhiteSpace(txtLyDo.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ mã khách hàng, mã sản phẩm và lý do tiếp nhận.", "Thiếu Thông Tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime.TryParseExact(txtHenTra.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var henTraDate);

            decimal costEst = 0m;
            if (!string.IsNullOrWhiteSpace(txtCostEst.Text))
            {
                decimal.TryParse(txtCostEst.Text.Trim(), out costEst);
            }

            var req = new TaoBaoHanhDTO
            {
                MaKhachHang = txtMaKH.Text.Trim(),
                MaSanPham = txtMaSP.Text.Trim(),
                LyDoBaoHanh = txtLyDo.Text.Trim(),
                TinhTrangTiepNhan = txtTinhTrang.Text.Trim(),
                GhiChu = txtGhiChu.Text.Trim(),
                ChiPhiBaoHanh = costEst >= 0 ? costEst : 0m,
                NgayHenTra = henTraDate != default ? henTraDate : DateTime.Now.AddDays(3)
            };

            var ok = _services.TiepNhanBaoHanh.Execute(req);
            if (ok)
            {
                MessageBox.Show("Đã tiếp nhận sản phẩm và lập phiếu bảo hành mới thành công (Trạng thái: Tiếp Nhận)!", "Tiếp Nhận Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dialog.DialogResult = DialogResult.OK;
                dialog.Close();
            }
            else
            {
                MessageBox.Show("Không thể tạo phiếu bảo hành. Vui lòng kiểm tra mã khách hàng và mã sản phẩm.", "Lỗi Tạo Phiếu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };

        dialog.Controls.Add(btnSave);

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            LoadWarrantyData();
        }
    }

    private Image? LoadQrImage()
    {
        var candidates = new[]
        {
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QRChuyenKhoan.jpg"),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "QRChuyenKhoan.jpg"),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "QRChuyenKhoan.jpg"),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "..", "QRChuyenKhoan.jpg"),
            Path.GetFullPath("QRChuyenKhoan.jpg")
        };

        foreach (var path in candidates)
        {
            if (File.Exists(path))
            {
                try
                {
                    return Image.FromFile(path);
                }
                catch
                {
                    // Thử ứng viên tiếp theo nếu lỗi nạp
                }
            }
        }
        return null;
    }
}
