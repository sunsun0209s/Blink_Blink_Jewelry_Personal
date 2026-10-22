using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using BlinkBlink.Admin.Services;
using BlinkBlink.Admin.Views.Helpers;
using BlinkBlink.Application.DTOs.Inventory;
using BlinkBlink.Domain.Entities.Catalog;
using BlinkBlink.Domain.Entities.Inventory;

namespace BlinkBlink.Admin.Views.UserControls.Inventory;

/// <summary>
/// Màn hình Quản lý Nhập Kho Trang Sức Blink Blink Jewelry.
/// Cho phép lập phiếu nhập kho, theo dõi lịch sử nhập hàng và tự động cập nhật tồn kho sẵn sàng bán.
/// </summary>
public class InventoryInwardView : UserControl
{
    private readonly AdminServiceScope _services;
    private List<PhieuNhapKho> _allReceipts = new();

    private TextBox _txtSearch = null!;
    private Button _btnCreateReceipt = null!;
    private Button _btnRefresh = null!;

    private Label _lblKpiTotalReceipts = null!;
    private Label _lblKpiTotalQuantity = null!;
    private Label _lblKpiTotalValue = null!;

    private DataGridView _dgvReceipts = null!;
    private DataGridView _dgvDetails = null!;

    public InventoryInwardView()
    {
        _services = AdminServiceScope.Instance;
        InitializeComponents();
        LoadReceiptsData();
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
            Text = "QUẢN LÝ NHẬP KHO & TIẾP NHẬN TRANG SỨC",
            Font = UITheme.HeaderFont,
            ForeColor = UITheme.TextPrimary,
            Location = new Point(20, 12),
            AutoSize = true,
            UseMnemonic = false
        };

        var lblSubTitle = new Label
        {
            Text = "Lập phiếu nhập kho từ nhà cung cấp, kiểm đếm số lượng và đưa sản phẩm vào trạng thái sẵn sàng giao dịch",
            Font = UITheme.RegularFont,
            ForeColor = UITheme.TextSecondary,
            Location = new Point(20, 38),
            AutoSize = true,
            UseMnemonic = false
        };

        var pnlKpis = new FlowLayoutPanel
        {
            Dock = DockStyle.Right,
            Width = 570,
            Height = 90,
            FlowDirection = FlowDirection.LeftToRight
        };

        pnlKpis.Controls.Add(CreateKpiCard("Tổng Phiếu Nhập", out _lblKpiTotalReceipts, UITheme.TextPrimary));
        pnlKpis.Controls.Add(CreateKpiCard("Tổng Món Đã Nhập", out _lblKpiTotalQuantity, UITheme.SuccessColor));
        pnlKpis.Controls.Add(CreateKpiCard("Tổng Giá Trị Nhập (VND)", out _lblKpiTotalValue, UITheme.PrimaryGold));

        topHeaderPanel.Controls.Add(lblTitle);
        topHeaderPanel.Controls.Add(lblSubTitle);
        topHeaderPanel.Controls.Add(pnlKpis);

        // Toolbar Panel Responsive chống tràn chữ
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
            Width = 220,
            Height = 28,
            Font = UITheme.RegularFont,
            PlaceholderText = "Mã phiếu, nhà cung cấp...",
            Margin = new Padding(0, 4, 15, 4)
        };
        _txtSearch.TextChanged += (s, e) => FilterReceipts();

        _btnCreateReceipt = UITheme.CreateFlatButton("+ Lập Phiếu Nhập Kho", UITheme.PrimaryGold, Color.Black);
        _btnCreateReceipt.Margin = new Padding(0, 2, 8, 2);
        _btnCreateReceipt.Click += (s, e) => ShowCreateReceiptDialog();

        _btnRefresh = UITheme.CreateFlatButton("⟳ Nạp Lại", Color.FromArgb(241, 245, 249), UITheme.TextPrimary);
        _btnRefresh.Margin = new Padding(0, 2, 0, 2);
        _btnRefresh.Click += (s, e) => LoadReceiptsData();

        toolbarPanel.Controls.Add(lblSearch);
        toolbarPanel.Controls.Add(_txtSearch);
        toolbarPanel.Controls.Add(_btnCreateReceipt);
        toolbarPanel.Controls.Add(_btnRefresh);

        // Main Container với SplitContainer chia bảng phiếu và bảng chi tiết
        var pnlGridContainer = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(20, 10, 20, 20),
            BackColor = UITheme.CanvasBackground
        };

        var splitContainer = new SplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Horizontal,
            SplitterDistance = 320,
            SplitterWidth = 8,
            BackColor = UITheme.CanvasBackground
        };

        // Bảng phiếu nhập kho
        var pnlReceiptsCard = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(1) };
        _dgvReceipts = new DataGridView();
        UITheme.ApplyGridViewStyle(_dgvReceipts);
        ConfigureReceiptGridColumns();
        _dgvReceipts.SelectionChanged += DgvReceipts_SelectionChanged;
        pnlReceiptsCard.Controls.Add(_dgvReceipts);
        splitContainer.Panel1.Controls.Add(pnlReceiptsCard);

        // Bảng chi tiết dòng hàng
        var pnlDetailsCard = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(1) };
        var pnlDetailsHeader = new Panel { Dock = DockStyle.Top, Height = 34, BackColor = Color.FromArgb(248, 250, 252), Padding = new Padding(12, 8, 12, 4) };
        var lblDetailsTitle = new Label { Text = "CHI TIẾT MẶT HÀNG TRANG SỨC THEO PHIẾU NHẬP ĐANG CHỌN", Font = UITheme.SubHeaderFont, ForeColor = UITheme.TextPrimary, AutoSize = true };
        pnlDetailsHeader.Controls.Add(lblDetailsTitle);

        _dgvDetails = new DataGridView();
        UITheme.ApplyGridViewStyle(_dgvDetails);
        ConfigureDetailGridColumns();

        pnlDetailsCard.Controls.Add(_dgvDetails);
        pnlDetailsCard.Controls.Add(pnlDetailsHeader);
        splitContainer.Panel2.Controls.Add(pnlDetailsCard);

        pnlGridContainer.Controls.Add(splitContainer);

        this.Controls.Add(pnlGridContainer);
        this.Controls.Add(toolbarPanel);
        this.Controls.Add(topHeaderPanel);
    }

    private Panel CreateKpiCard(string title, out Label lblValue, Color valueColor)
    {
        var card = new Panel
        {
            Width = 180,
            Height = 75,
            BackColor = Color.White,
            Margin = new Padding(5),
            Padding = new Padding(12, 8, 12, 8)
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

    private void ConfigureReceiptGridColumns()
    {
        _dgvReceipts.Columns.Clear();
        _dgvReceipts.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaPhieuNhap", HeaderText = "MÃ PHIẾU NHẬP", FillWeight = 110, MinimumWidth = 120 });
        _dgvReceipts.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayNhap", HeaderText = "NGÀY NHẬP", FillWeight = 120, MinimumWidth = 130 });
        _dgvReceipts.Columns.Add(new DataGridViewTextBoxColumn { Name = "NhaCungCap", HeaderText = "NHÀ CUNG CẤP TRANG SỨC", FillWeight = 220, MinimumWidth = 200 });
        _dgvReceipts.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaNhanVien", HeaderText = "NGƯỜI TIẾP NHẬN", FillWeight = 100, MinimumWidth = 110 });
        _dgvReceipts.Columns.Add(new DataGridViewTextBoxColumn { Name = "TongSoLuong", HeaderText = "TỔNG SỐ LƯỢNG", FillWeight = 95, MinimumWidth = 105, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = UITheme.TabularNumericFont } });
        _dgvReceipts.Columns.Add(new DataGridViewTextBoxColumn { Name = "TongGiaTri", HeaderText = "TỔNG TIỀN NHẬP (VND)", FillWeight = 140, MinimumWidth = 150, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Font = UITheme.TabularNumericFont, ForeColor = UITheme.PrimaryGold } });
    }

    private void ConfigureDetailGridColumns()
    {
        _dgvDetails.Columns.Clear();
        _dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaSanPham", HeaderText = "MÃ SẢN PHẨM", FillWeight = 100, MinimumWidth = 100 });
        _dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenSanPham", HeaderText = "TÊN TRANG SỨC", FillWeight = 220, MinimumWidth = 200 });
        _dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoLuong", HeaderText = "SỐ LƯỢNG", FillWeight = 80, MinimumWidth = 80, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = UITheme.TabularNumericFont } });
        _dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "GiaVonNhap", HeaderText = "ĐƠN GIÁ VỐN (VND)", FillWeight = 120, MinimumWidth = 130, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Font = UITheme.TabularNumericFont } });
        _dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "ThanhTien", HeaderText = "THÀNH TIỀN (VND)", FillWeight = 130, MinimumWidth = 140, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Font = UITheme.TabularNumericFont, ForeColor = UITheme.PrimaryGold } });
        _dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoChungTu", HeaderText = "SỐ CHỨNG TỪ", FillWeight = 110, MinimumWidth = 110 });
    }

    public void LoadReceiptsData()
    {
        _allReceipts = _services.PhieuNhapKhoRepo.LayTatCa();
        UpdateKpis();
        FilterReceipts();
    }

    private void UpdateKpis()
    {
        var viVn = new CultureInfo("vi-VN");
        _lblKpiTotalReceipts.Text = _allReceipts.Count.ToString("N0", viVn);
        _lblKpiTotalQuantity.Text = _allReceipts.Sum(r => r.TongSoSanPham).ToString("N0", viVn);
        _lblKpiTotalValue.Text = _allReceipts.Sum(r => r.TongGiaTri).ToString("N0", viVn);
    }

    private void FilterReceipts()
    {
        var query = _txtSearch.Text.Trim().ToLowerInvariant();

        var filtered = _allReceipts.Where(r =>
            string.IsNullOrEmpty(query) ||
            r.MaPhieuNhap.ToLowerInvariant().Contains(query) ||
            r.NhaCungCap.ToLowerInvariant().Contains(query) ||
            r.MaNhanVien.ToLowerInvariant().Contains(query)
        ).ToList();

        _dgvReceipts.Rows.Clear();
        var viVn = new CultureInfo("vi-VN");

        foreach (var r in filtered)
        {
            _dgvReceipts.Rows.Add(
                r.MaPhieuNhap,
                r.NgayNhap.ToString("dd/MM/yyyy HH:mm"),
                r.NhaCungCap,
                r.MaNhanVien,
                r.TongSoSanPham.ToString("N0", viVn),
                r.TongGiaTri.ToString("N0", viVn)
            );
        }

        if (_dgvReceipts.Rows.Count > 0)
        {
            _dgvReceipts.Rows[0].Selected = true;
            DisplaySelectedReceiptDetails();
        }
        else
        {
            _dgvDetails.Rows.Clear();
        }
    }

    private void DgvReceipts_SelectionChanged(object? sender, EventArgs e)
    {
        DisplaySelectedReceiptDetails();
    }

    private void DisplaySelectedReceiptDetails()
    {
        _dgvDetails.Rows.Clear();
        if (_dgvReceipts.SelectedRows.Count == 0) return;

        var maPhieu = _dgvReceipts.SelectedRows[0].Cells["MaPhieuNhap"].Value?.ToString();
        var receipt = _allReceipts.FirstOrDefault(r => r.MaPhieuNhap == maPhieu);
        if (receipt == null || receipt.ChiTietNhapKhos == null) return;

        var viVn = new CultureInfo("vi-VN");
        foreach (var ct in receipt.ChiTietNhapKhos)
        {
            var tenSp = ct.SanPhamTrangSuc?.TenSanPham ?? ct.MaSanPham;
            var thanhTien = ct.TinhGiaTri();

            _dgvDetails.Rows.Add(
                ct.MaSanPham,
                tenSp,
                ct.SoLuong.ToString("N0", viVn),
                ct.GiaVonNhap.ToString("N0", viVn),
                thanhTien.ToString("N0", viVn),
                string.IsNullOrEmpty(ct.SoChungTu) ? "-" : ct.SoChungTu
            );
        }
    }

    private void ShowCreateReceiptDialog()
    {
        var allProducts = _services.SanPhamRepo.LayTatCa();
        if (allProducts.Count == 0)
        {
            MessageBox.Show("Hiện chưa có sản phẩm nào trong danh mục. Vui lòng thêm sản phẩm trước khi lập phiếu nhập kho.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var dialog = new Form
        {
            Text = "Lập Phiếu Nhập Kho Trang Sức Mới - Blink Blink Jewelry",
            Width = 840,
            Height = 620,
            StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false,
            BackColor = Color.White,
            Font = UITheme.RegularFont
        };

        var pnlHead = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = UITheme.DarkHeader, Padding = new Padding(24, 12, 24, 10) };
        var lblTitle = new Label { Text = "LẬP PHIẾU NHẬP KHO KIM HOÀN", Font = UITheme.HeaderFont, ForeColor = UITheme.PrimaryGold, Dock = DockStyle.Top, Height = 24 };
        var lblSub = new Label { Text = "Xác nhận nhập số lượng trang sức vào kho hàng và tự động cập nhật trạng thái 'Sẵn sàng giao dịch'", Font = new Font("Segoe UI", 9F), ForeColor = Color.FromArgb(203, 213, 225), Dock = DockStyle.Fill };
        pnlHead.Controls.Add(lblSub);
        pnlHead.Controls.Add(lblTitle);

        var pnlFoot = new Panel { Dock = DockStyle.Bottom, Height = 65, BackColor = UITheme.CanvasBackground, Padding = new Padding(24, 12, 24, 12) };
        var flpFoot = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };

        var btnCancel = UITheme.CreateFlatButton("Hủy Bỏ", Color.FromArgb(226, 232, 240), UITheme.TextPrimary);
        btnCancel.Width = 110;
        btnCancel.Height = 36;
        btnCancel.Margin = new Padding(12, 0, 0, 0);
        btnCancel.Click += (s, e) => dialog.Close();

        var btnConfirm = UITheme.CreateFlatButton("Xác Nhận Nhập Kho", UITheme.PrimaryGold, Color.Black);
        btnConfirm.Width = 170;
        btnConfirm.Height = 36;
        btnConfirm.Margin = new Padding(0);

        flpFoot.Controls.Add(btnCancel);
        flpFoot.Controls.Add(btnConfirm);
        pnlFoot.Controls.Add(flpFoot);

        var pnlBody = new Panel { Dock = DockStyle.Fill, Padding = new Padding(24, 15, 24, 10) };

        // Thông tin phiếu
        var pnlInfo = new Panel { Dock = DockStyle.Top, Height = 100 };
        var lblMaP = new Label { Text = "Mã Phiếu Nhập:", Location = new Point(0, 8), AutoSize = true, ForeColor = UITheme.TextSecondary };
        var txtMaP = new TextBox { Text = $"PNK{DateTime.Now:yyyyMMdd}_{_allReceipts.Count + 1:D2}", Location = new Point(130, 4), Width = 190, ReadOnly = true, BackColor = Color.FromArgb(241, 245, 249) };

        var lblNcc = new Label { Text = "Nhà Cung Cấp (*):", Location = new Point(350, 8), AutoSize = true, ForeColor = UITheme.TextSecondary };
        var txtNcc = new TextBox { Text = "Tập đoàn Kim Hoàn Doji Atelier", Location = new Point(480, 4), Width = 310 };

        var lblNv = new Label { Text = "Mã Nhân Viên:", Location = new Point(0, 50), AutoSize = true, ForeColor = UITheme.TextSecondary };
        var txtNv = new TextBox { Text = "USER_NV002", Location = new Point(130, 46), Width = 190 };

        var lblChungTu = new Label { Text = "Số Chứng Từ / HĐ:", Location = new Point(350, 50), AutoSize = true, ForeColor = UITheme.TextSecondary };
        var txtChungTu = new TextBox { Text = $"CT_{DateTime.Now:yyMMdd}_IN", Location = new Point(480, 46), Width = 310 };

        pnlInfo.Controls.AddRange(new Control[] { lblMaP, txtMaP, lblNcc, txtNcc, lblNv, txtNv, lblChungTu, txtChungTu });

        // Thêm mặt hàng nhập
        var pnlAddItem = new Panel { Dock = DockStyle.Top, Height = 75, BackColor = Color.FromArgb(248, 250, 252), Padding = new Padding(12, 10, 12, 10) };
        var lblSp = new Label { Text = "Sản Phẩm Nhập:", Location = new Point(12, 12), AutoSize = true, ForeColor = UITheme.TextSecondary };
        var cboSp = new ComboBox { Location = new Point(120, 8), Width = 260, DropDownStyle = ComboBoxStyle.DropDownList };
        foreach (var sp in allProducts)
        {
            cboSp.Items.Add(new ProductComboItem(sp.MaSanPham, sp.TenSanPham, sp.GiaNhap));
        }
        if (cboSp.Items.Count > 0) cboSp.SelectedIndex = 0;

        var lblSl = new Label { Text = "Số Lượng:", Location = new Point(390, 12), AutoSize = true, ForeColor = UITheme.TextSecondary };
        var nudSl = new NumericUpDown { Location = new Point(460, 8), Width = 70, Minimum = 1, Maximum = 1000, Value = 5 };

        var lblGiaVon = new Label { Text = "Đơn Giá Vốn:", Location = new Point(545, 12), AutoSize = true, ForeColor = UITheme.TextSecondary };
        var txtGiaVon = new TextBox { Location = new Point(635, 8), Width = 150, Font = UITheme.TabularNumericFont, Text = "12000000" };

        cboSp.SelectedIndexChanged += (s, e) =>
        {
            if (cboSp.SelectedItem is ProductComboItem selected)
            {
                txtGiaVon.Text = selected.DefaultGiaNhap.ToString("0");
            }
        };

        var btnAddRow = UITheme.CreateFlatButton("+ Thêm Vào Phiếu", UITheme.DarkHeader, Color.White);
        btnAddRow.Location = new Point(635, 38);
        btnAddRow.Width = 150;
        btnAddRow.Height = 30;

        pnlAddItem.Controls.AddRange(new Control[] { lblSp, cboSp, lblSl, nudSl, lblGiaVon, txtGiaVon, btnAddRow });

        // Bảng các dòng chi tiết tạm
        var dgvTemp = new DataGridView();
        UITheme.ApplyGridViewStyle(dgvTemp);
        dgvTemp.Columns.Add("MaSanPham", "MÃ SẢN PHẨM");
        dgvTemp.Columns.Add("TenSanPham", "TÊN TRANG SỨC");
        dgvTemp.Columns.Add("SoLuong", "SỐ LƯỢNG");
        dgvTemp.Columns.Add("GiaVon", "GIÁ VỐN (VND)");
        dgvTemp.Columns.Add("ThanhTien", "THÀNH TIỀN (VND)");

        var tempItems = new List<ChiTietNhapKhoDTO>();
        var viVn = new CultureInfo("vi-VN");

        var pnlTotalBar = new Panel { Dock = DockStyle.Bottom, Height = 40, BackColor = Color.FromArgb(248, 250, 252), Padding = new Padding(12, 10, 12, 4) };
        var lblSummary = new Label { Text = "Tổng Số Lượng: 0  |  Tổng Tiền Nhập: 0 VND", Font = UITheme.SubHeaderFont, ForeColor = UITheme.PrimaryGold, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight };
        pnlTotalBar.Controls.Add(lblSummary);

        void RefreshTempGrid()
        {
            dgvTemp.Rows.Clear();
            decimal totalVal = 0;
            int totalQty = 0;

            foreach (var item in tempItems)
            {
                var p = allProducts.FirstOrDefault(s => s.MaSanPham == item.MaSanPham);
                var tt = item.SoLuong * item.GiaVonNhap;
                totalQty += item.SoLuong;
                totalVal += tt;

                dgvTemp.Rows.Add(
                    item.MaSanPham,
                    p?.TenSanPham ?? item.MaSanPham,
                    item.SoLuong.ToString("N0", viVn),
                    item.GiaVonNhap.ToString("N0", viVn),
                    tt.ToString("N0", viVn)
                );
            }

            lblSummary.Text = $"Tổng Số Lượng: {totalQty:N0}  |  Tổng Tiền Nhập: {totalVal:N0} VND";
        }

        btnAddRow.Click += (s, e) =>
        {
            if (cboSp.SelectedItem is not ProductComboItem item) return;
            if (!decimal.TryParse(txtGiaVon.Text.Trim(), out var gv) || gv <= 0)
            {
                MessageBox.Show("Vui lòng nhập giá vốn hợp lệ (> 0).", "Lỗi nhập giá", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var qty = (int)nudSl.Value;

            // Kiểm tra trùng
            var existing = tempItems.FirstOrDefault(x => x.MaSanPham == item.MaSP);
            if (existing != null)
            {
                existing.SoLuong += qty;
                existing.GiaVonNhap = gv;
            }
            else
            {
                tempItems.Add(new ChiTietNhapKhoDTO
                {
                    MaSanPham = item.MaSP,
                    SoLuong = qty,
                    GiaVonNhap = gv,
                    SoChungTu = txtChungTu.Text.Trim()
                });
            }

            RefreshTempGrid();
        };

        // Nạp sẵn 1 mặt hàng đầu tiên làm mẫu
        if (cboSp.SelectedItem is ProductComboItem firstSp)
        {
            decimal.TryParse(txtGiaVon.Text, out var gv);
            tempItems.Add(new ChiTietNhapKhoDTO
            {
                MaSanPham = firstSp.MaSP,
                SoLuong = 5,
                GiaVonNhap = gv > 0 ? gv : 15000000,
                SoChungTu = txtChungTu.Text.Trim()
            });
            RefreshTempGrid();
        }

        btnConfirm.Click += (s, e) =>
        {
            if (string.IsNullOrWhiteSpace(txtNcc.Text))
            {
                MessageBox.Show("Vui lòng nhập Nhà cung cấp.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNcc.Focus();
                return;
            }

            if (tempItems.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất 1 dòng sản phẩm vào phiếu nhập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var request = new NhapKhoDTO
            {
                MaPhieuNhap = txtMaP.Text.Trim(),
                NhaCungCap = txtNcc.Text.Trim(),
                MaNhanVien = txtNv.Text.Trim(),
                DanhSachSanPham = tempItems
            };

            try
            {
                _services.XacNhanNhapKho.Execute(request);
                MessageBox.Show($"Xác nhận phiếu nhập kho [{request.MaPhieuNhap}] thành công!\n\nToàn bộ sản phẩm trong phiếu đã được cập nhật sang trạng thái 'Sẵn sàng giao dịch'.", "Nhập kho thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dialog.DialogResult = DialogResult.OK;
                dialog.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi trong quá trình nhập kho: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };

        var pnlGridWrapper = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 10, 0, 10) };
        pnlGridWrapper.Controls.Add(dgvTemp);

        pnlBody.Controls.Add(pnlGridWrapper);
        pnlBody.Controls.Add(pnlTotalBar);
        pnlBody.Controls.Add(pnlAddItem);
        pnlBody.Controls.Add(pnlInfo);

        dialog.Controls.Add(pnlBody);
        dialog.Controls.Add(pnlFoot);
        dialog.Controls.Add(pnlHead);

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            LoadReceiptsData();
        }
    }

    private class ProductComboItem
    {
        public string MaSP { get; }
        public string TenSP { get; }
        public decimal DefaultGiaNhap { get; }

        public ProductComboItem(string maSp, string tenSp, decimal defaultGiaNhap)
        {
            MaSP = maSp;
            TenSP = tenSp;
            DefaultGiaNhap = defaultGiaNhap;
        }

        public override string ToString() => $"[{MaSP}] {TenSP}";
    }
}
