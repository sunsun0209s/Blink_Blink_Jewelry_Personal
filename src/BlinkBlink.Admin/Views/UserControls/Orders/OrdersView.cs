using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using BlinkBlink.Admin.Services;
using BlinkBlink.Admin.Views.Helpers;
using BlinkBlink.Domain.Entities.Orders;

namespace BlinkBlink.Admin.Views.UserControls.Orders;

/// <summary>
/// Màn hình Quản lý Bán hàng & Phê duyệt đơn hàng dành cho Nhân viên và Quản trị viên.
/// Thực thi các nghiệp vụ: Phê duyệt đơn đặt hàng online, chuyển trạng thái giao hàng, hủy đơn và hoàn kho.
/// </summary>
public class OrdersView : UserControl
{
    private readonly AdminServiceScope _services;
    private List<HoaDon> _allOrders = new();

    private TextBox _txtSearch = null!;
    private ComboBox _cboStatus = null!;
    private Button _btnApprove = null!;
    private Button _btnShipping = null!;
    private Button _btnDelivered = null!;
    private Button _btnCancel = null!;
    private Button _btnRefresh = null!;

    private Label _lblKpiTotal = null!;
    private Label _lblKpiPending = null!;
    private Label _lblKpiDelivered = null!;

    private DataGridView _dgvOrders = null!;
    private DataGridView _dgvOrderItems = null!;
    private Label _lblDetailTitle = null!;

    public OrdersView()
    {
        _services = AdminServiceScope.Instance;
        InitializeComponents();
        LoadOrderData();
    }

    private void InitializeComponents()
    {
        this.Dock = DockStyle.Fill;
        this.BackColor = UITheme.CanvasBackground;
        this.Font = UITheme.RegularFont;

        // Panel tiêu đề và thẻ chỉ số KPI tóm tắt
        var topHeaderPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 110,
            Padding = new Padding(20, 15, 20, 10),
            BackColor = UITheme.CanvasBackground
        };

        var lblTitle = new Label
        {
            Text = "QUẢN LÝ BÁN HÀNG & PHÊ DUYỆT ĐƠN HÀNG",
            Font = UITheme.HeaderFont,
            ForeColor = UITheme.TextPrimary,
            Location = new Point(20, 12),
            AutoSize = true
        };

        var lblSubTitle = new Label
        {
            Text = "Theo dõi đơn hàng online, phê duyệt xử lý đóng gói và bàn giao vận chuyển trang sức",
            Font = UITheme.RegularFont,
            ForeColor = UITheme.TextSecondary,
            Location = new Point(20, 38),
            AutoSize = true
        };

        // Khối 3 thẻ chỉ số nhanh
        var pnlKpis = new FlowLayoutPanel
        {
            Dock = DockStyle.Right,
            Width = 520,
            Height = 90,
            FlowDirection = FlowDirection.LeftToRight
        };

        pnlKpis.Controls.Add(CreateKpiCard("Tổng Đơn", out _lblKpiTotal, UITheme.TextPrimary));
        pnlKpis.Controls.Add(CreateKpiCard("Chờ Duyệt", out _lblKpiPending, UITheme.WarningColor));
        pnlKpis.Controls.Add(CreateKpiCard("Đã Giao", out _lblKpiDelivered, UITheme.SuccessColor));

        topHeaderPanel.Controls.Add(lblTitle);
        topHeaderPanel.Controls.Add(lblSubTitle);
        topHeaderPanel.Controls.Add(pnlKpis);

        // Thanh công cụ tác vụ và tìm kiếm
        var toolbarPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 52,
            Padding = new Padding(20, 8, 20, 8),
            BackColor = Color.White
        };

        var lblSearch = new Label
        {
            Text = "Tìm kiếm:",
            Location = new Point(20, 16),
            AutoSize = true,
            ForeColor = UITheme.TextSecondary
        };

        _txtSearch = new TextBox
        {
            Location = new Point(88, 13),
            Width = 180,
            Height = 26,
            Font = UITheme.RegularFont,
            PlaceholderText = "Mã đơn, SĐT, Khách..."
        };
        _txtSearch.TextChanged += (s, e) => FilterOrders();

        var lblStatusFilter = new Label
        {
            Text = "Trạng thái:",
            Location = new Point(285, 16),
            AutoSize = true,
            ForeColor = UITheme.TextSecondary
        };

        _cboStatus = new ComboBox
        {
            Location = new Point(355, 13),
            Width = 140,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = UITheme.RegularFont
        };
        _cboStatus.Items.AddRange(new object[] { "Tất Cả", "ChoDuyet", "DangDongGoi", "DangGiao", "DaGiao", "DaHuy" });
        _cboStatus.SelectedIndex = 0;
        _cboStatus.SelectedIndexChanged += (s, e) => FilterOrders();

        // Nút hành động
        _btnApprove = UITheme.CreateFlatButton("✓ Duyệt Đơn", UITheme.PrimaryGold, Color.Black);
        _btnApprove.Location = new Point(515, 9);
        _btnApprove.Click += BtnApprove_Click;

        _btnShipping = UITheme.CreateFlatButton("🚚 Đang Giao", UITheme.DarkHeader, Color.White);
        _btnShipping.Location = new Point(625, 9);
        _btnShipping.Click += (s, e) => UpdateOrderStatus("DangGiao");

        _btnDelivered = UITheme.CreateFlatButton("★ Đã Giao", UITheme.SuccessColor, Color.White);
        _btnDelivered.Location = new Point(740, 9);
        _btnDelivered.Click += (s, e) => UpdateOrderStatus("DaGiao");

        _btnCancel = UITheme.CreateFlatButton("✕ Hủy Đơn", UITheme.DangerColor, Color.White);
        _btnCancel.Location = new Point(835, 9);
        _btnCancel.Click += BtnCancel_Click;

        _btnRefresh = UITheme.CreateFlatButton("⟳ Nạp Lại", Color.FromArgb(241, 245, 249), UITheme.TextPrimary);
        _btnRefresh.Location = new Point(935, 9);
        _btnRefresh.Click += (s, e) => LoadOrderData();

        toolbarPanel.Controls.Add(lblSearch);
        toolbarPanel.Controls.Add(_txtSearch);
        toolbarPanel.Controls.Add(lblStatusFilter);
        toolbarPanel.Controls.Add(_cboStatus);
        toolbarPanel.Controls.Add(_btnApprove);
        toolbarPanel.Controls.Add(_btnShipping);
        toolbarPanel.Controls.Add(_btnDelivered);
        toolbarPanel.Controls.Add(_btnCancel);
        toolbarPanel.Controls.Add(_btnRefresh);

        // Vùng hiển thị phân chia (Split Container)
        var splitContainer = new SplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Horizontal,
            SplitterDistance = 330,
            SplitterWidth = 8,
            BackColor = UITheme.CanvasBackground,
            Padding = new Padding(20, 10, 20, 10)
        };

        // Bảng danh sách đơn hàng
        _dgvOrders = new DataGridView();
        UITheme.ApplyGridViewStyle(_dgvOrders);
        ConfigureOrdersGrid();
        _dgvOrders.SelectionChanged += DgvOrders_SelectionChanged;

        var pnlOrdersGrid = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            Padding = new Padding(1)
        };
        pnlOrdersGrid.Controls.Add(_dgvOrders);
        splitContainer.Panel1.Controls.Add(pnlOrdersGrid);

        // Vùng chi tiết đơn hàng
        var pnlDetailContainer = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            Padding = new Padding(12)
        };

        _lblDetailTitle = new Label
        {
            Text = "DANH SÁCH MẶT HÀNG TRANG SỨC TRONG ĐƠN",
            Font = UITheme.SubHeaderFont,
            ForeColor = UITheme.TextPrimary,
            Dock = DockStyle.Top,
            Height = 32
        };

        _dgvOrderItems = new DataGridView();
        UITheme.ApplyGridViewStyle(_dgvOrderItems);
        ConfigureOrderItemsGrid();

        pnlDetailContainer.Controls.Add(_dgvOrderItems);
        pnlDetailContainer.Controls.Add(_lblDetailTitle);
        splitContainer.Panel2.Controls.Add(pnlDetailContainer);

        this.Controls.Add(splitContainer);
        this.Controls.Add(toolbarPanel);
        this.Controls.Add(topHeaderPanel);
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

    private void ConfigureOrdersGrid()
    {
        _dgvOrders.Columns.Clear();
        _dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaHoaDon", HeaderText = "MÃ ĐƠN", FillWeight = 65 });
        _dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayLap", HeaderText = "NGÀY LẬP", FillWeight = 85 });
        _dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaKhachHang", HeaderText = "KHÁCH HÀNG", FillWeight = 75 });
        _dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoDienThoai", HeaderText = "SỐ ĐT NHẬN", FillWeight = 80 });
        _dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "DiaChi", HeaderText = "ĐỊA CHỈ GIAO HÀNG", FillWeight = 140 });
        _dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "ThanhToan", HeaderText = "PHƯƠNG THỨC", FillWeight = 75 });
        _dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "TongHang", HeaderText = "TIỀN HÀNG (VND)", FillWeight = 90, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Font = UITheme.TabularNumericFont } });
        _dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "GiamGia", HeaderText = "GIẢM GIÁ (VND)", FillWeight = 75, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Font = UITheme.TabularNumericFont } });
        _dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "TongCong", HeaderText = "TỔNG TT (VND)", FillWeight = 95, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Font = UITheme.TabularNumericFont, ForeColor = UITheme.PrimaryGold } });
        _dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "TrangThai", HeaderText = "TRẠNG THÁI", FillWeight = 80 });
    }

    private void ConfigureOrderItemsGrid()
    {
        _dgvOrderItems.Columns.Clear();
        _dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaSanPham", HeaderText = "MÃ SẢN PHẨM", FillWeight = 80 });
        _dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoLuong", HeaderText = "SỐ LƯỢNG", FillWeight = 50, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = UITheme.TabularNumericFont } });
        _dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "DonGia", HeaderText = "ĐƠN GIÁ (VND)", FillWeight = 90, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Font = UITheme.TabularNumericFont } });
        _dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "ThanhTien", HeaderText = "THÀNH TIỀN (VND)", FillWeight = 100, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Font = UITheme.TabularNumericFont, ForeColor = UITheme.PrimaryGold } });
    }

    private void LoadOrderData()
    {
        _allOrders = _services.HoaDonRepo.LayDanhSachDonHang();
        UpdateKpis();
        FilterOrders();
    }

    private void UpdateKpis()
    {
        var viVn = new CultureInfo("vi-VN");
        _lblKpiTotal.Text = _allOrders.Count.ToString("N0", viVn);
        _lblKpiPending.Text = _allOrders.Count(o => string.Equals(o.TrangThaiDonHang, "ChoDuyet", StringComparison.OrdinalIgnoreCase)).ToString("N0", viVn);
        _lblKpiDelivered.Text = _allOrders.Count(o => string.Equals(o.TrangThaiDonHang, "DaGiao", StringComparison.OrdinalIgnoreCase)).ToString("N0", viVn);
    }

    private void FilterOrders()
    {
        var query = _txtSearch.Text.Trim().ToLowerInvariant();
        var selectedStatus = _cboStatus.SelectedItem?.ToString() ?? "Tất Cả";

        var filtered = _allOrders.Where(o =>
        {
            var matchSearch = string.IsNullOrEmpty(query) ||
                              o.MaHoaDon.ToLowerInvariant().Contains(query) ||
                              o.SoDienThoaiNhan.Contains(query) ||
                              o.MaKhachHang.ToLowerInvariant().Contains(query) ||
                              o.DiaChiGiaoHang.ToLowerInvariant().Contains(query);

            var matchStatus = selectedStatus == "Tất Cả" ||
                              string.Equals(o.TrangThaiDonHang, selectedStatus, StringComparison.OrdinalIgnoreCase);

            return matchSearch && matchStatus;
        }).ToList();

        _dgvOrders.Rows.Clear();
        var viVn = new CultureInfo("vi-VN");

        foreach (var hd in filtered)
        {
            var rowIdx = _dgvOrders.Rows.Add(
                hd.MaHoaDon,
                hd.NgayLap.ToString("dd/MM/yyyy HH:mm"),
                hd.MaKhachHang,
                hd.SoDienThoaiNhan,
                hd.DiaChiGiaoHang,
                hd.PhuongThucThanhToan,
                hd.TongTienHang.ToString("N0", viVn),
                hd.TienGiamGia.ToString("N0", viVn),
                hd.TongThanhToan.ToString("N0", viVn),
                hd.TrangThaiDonHang
            );

            // Gán màu sắc trực quan cho trạng thái
            var cellStatus = _dgvOrders.Rows[rowIdx].Cells["TrangThai"];
            if (string.Equals(hd.TrangThaiDonHang, "ChoDuyet", StringComparison.OrdinalIgnoreCase))
                cellStatus.Style.ForeColor = UITheme.WarningColor;
            else if (string.Equals(hd.TrangThaiDonHang, "DaGiao", StringComparison.OrdinalIgnoreCase))
                cellStatus.Style.ForeColor = UITheme.SuccessColor;
            else if (string.Equals(hd.TrangThaiDonHang, "DaHuy", StringComparison.OrdinalIgnoreCase))
                cellStatus.Style.ForeColor = UITheme.DangerColor;
        }

        if (_dgvOrders.Rows.Count > 0)
        {
            _dgvOrders.Rows[0].Selected = true;
            ShowSelectedOrderDetails();
        }
        else
        {
            _dgvOrderItems.Rows.Clear();
            _lblDetailTitle.Text = "DANH SÁCH MẶT HÀNG TRANG SỨC (Chưa chọn đơn)";
        }
    }

    private void DgvOrders_SelectionChanged(object? sender, EventArgs e)
    {
        ShowSelectedOrderDetails();
    }

    private void ShowSelectedOrderDetails()
    {
        if (_dgvOrders.SelectedRows.Count == 0) return;

        var maHoaDon = _dgvOrders.SelectedRows[0].Cells["MaHoaDon"].Value?.ToString();
        if (string.IsNullOrEmpty(maHoaDon)) return;

        var hd = _allOrders.FirstOrDefault(h => h.MaHoaDon == maHoaDon);
        if (hd == null) return;

        _lblDetailTitle.Text = $"DANH SÁCH MẶT HÀNG TRANG SỨC - ĐƠN HÀNG #{hd.MaHoaDon} ({hd.TrangThaiDonHang})";
        _dgvOrderItems.Rows.Clear();

        var viVn = new CultureInfo("vi-VN");
        if (hd.ChiTietHoaDons != null)
        {
            foreach (var item in hd.ChiTietHoaDons)
            {
                _dgvOrderItems.Rows.Add(
                    item.MaSanPham,
                    item.SoLuong.ToString("N0", viVn),
                    item.DonGia.ToString("N0", viVn),
                    item.TinhThanhTien().ToString("N0", viVn)
                );
            }
        }
    }

    private void BtnApprove_Click(object? sender, EventArgs e)
    {
        if (_dgvOrders.SelectedRows.Count == 0)
        {
            MessageBox.Show("Vui lòng chọn đơn hàng cần duyệt trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var maHoaDon = _dgvOrders.SelectedRows[0].Cells["MaHoaDon"].Value?.ToString();
        if (string.IsNullOrEmpty(maHoaDon)) return;

        var hd = _allOrders.FirstOrDefault(h => h.MaHoaDon == maHoaDon);
        if (hd == null) return;

        if (!string.Equals(hd.TrangThaiDonHang, "ChoDuyet", StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show($"Đơn hàng #{maHoaDon} đang ở trạng thái '{hd.TrangThaiDonHang}', chỉ có thể duyệt đơn 'ChoDuyet'.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Thực thi Use Case phê duyệt đơn
        var success = _services.XuLyDonHang.DuyetDonHang(maHoaDon, "NV001");
        if (success)
        {
            MessageBox.Show($"Đã phê duyệt thành công đơn hàng #{maHoaDon}. Trạng thái đã chuyển sang: DangDongGoi.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadOrderData();
        }
        else
        {
            MessageBox.Show("Không thể duyệt đơn hàng này do lỗi ràng buộc nghiệp vụ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void UpdateOrderStatus(string newStatus)
    {
        if (_dgvOrders.SelectedRows.Count == 0)
        {
            MessageBox.Show("Vui lòng chọn đơn hàng cần cập nhật trạng thái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var maHoaDon = _dgvOrders.SelectedRows[0].Cells["MaHoaDon"].Value?.ToString();
        if (string.IsNullOrEmpty(maHoaDon)) return;

        var success = _services.XuLyDonHang.CapNhatTrangThai(maHoaDon, newStatus, "NV001");
        if (success)
        {
            MessageBox.Show($"Đã cập nhật trạng thái đơn #{maHoaDon} sang '{newStatus}'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadOrderData();
        }
        else
        {
            MessageBox.Show("Không thể cập nhật trạng thái đơn hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        if (_dgvOrders.SelectedRows.Count == 0)
        {
            MessageBox.Show("Vui lòng chọn đơn hàng muốn hủy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var maHoaDon = _dgvOrders.SelectedRows[0].Cells["MaHoaDon"].Value?.ToString();
        if (string.IsNullOrEmpty(maHoaDon)) return;

        var confirm = MessageBox.Show($"Bạn có chắc chắn muốn hủy đơn hàng #{maHoaDon} không? Các sản phẩm đã bán sẽ được hoàn kho.", "Xác nhận hủy đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (confirm != DialogResult.Yes) return;

        var success = _services.XuLyDonHang.HuyDonHang(maHoaDon);
        if (success)
        {
            MessageBox.Show($"Đơn hàng #{maHoaDon} đã hủy thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadOrderData();
        }
        else
        {
            MessageBox.Show("Không thể hủy đơn hàng này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
