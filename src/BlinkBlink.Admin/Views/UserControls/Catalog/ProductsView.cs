using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using BlinkBlink.Admin.Services;
using BlinkBlink.Admin.Views.Helpers;
using BlinkBlink.Application.DTOs.Catalog;
using BlinkBlink.Domain.Entities.Catalog;

namespace BlinkBlink.Admin.Views.UserControls.Catalog;

/// <summary>
/// Màn hình Quản lý Sản phẩm & Tồn kho trang sức Blink Blink Jewelry.
/// Hỗ trợ tìm kiếm, lọc danh mục, xem chi tiết thuộc tính kim hoàn, điều chỉnh giá bán và thêm mẫu trang sức mới.
/// </summary>
public class ProductsView : UserControl
{
    private readonly AdminServiceScope _services;
    private List<SanPhamTrangSuc> _allProducts = new();

    private TextBox _txtSearch = null!;
    private ComboBox _cboCategory = null!;
    private TextBox _txtEditPrice = null!;
    private Button _btnUpdatePrice = null!;
    private Button _btnAddProduct = null!;
    private Button _btnRefresh = null!;

    private Label _lblKpiTotalSku = null!;
    private Label _lblKpiInStock = null!;
    private Label _lblKpiTotalValue = null!;

    private DataGridView _dgvProducts = null!;

    public ProductsView()
    {
        _services = AdminServiceScope.Instance;
        InitializeComponents();
        LoadProductsData();
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
            Text = "QUẢN LÝ SẢN PHẨM & TỒN KHO KIM HOÀN",
            Font = UITheme.HeaderFont,
            ForeColor = UITheme.TextPrimary,
            Location = new Point(20, 12),
            AutoSize = true,
            UseMnemonic = false
        };

        var lblSubTitle = new Label
        {
            Text = "Theo dõi danh mục trang sức cao cấp, chất liệu, size, kiểm soát trạng thái kho và cập nhật giá niêm yết",
            Font = UITheme.RegularFont,
            ForeColor = UITheme.TextSecondary,
            Location = new Point(20, 38),
            AutoSize = true,
            UseMnemonic = false
        };

        var pnlKpis = new FlowLayoutPanel
        {
            Dock = DockStyle.Right,
            Width = 560,
            Height = 90,
            FlowDirection = FlowDirection.LeftToRight
        };

        pnlKpis.Controls.Add(CreateKpiCard("Tổng Mẫu (SKU)", out _lblKpiTotalSku, UITheme.TextPrimary));
        pnlKpis.Controls.Add(CreateKpiCard("Sẵn Sàng Giao Dịch", out _lblKpiInStock, UITheme.SuccessColor));
        pnlKpis.Controls.Add(CreateKpiCard("Tổng Giá Trị (VND)", out _lblKpiTotalValue, UITheme.PrimaryGold));

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
            Width = 170,
            Height = 28,
            Font = UITheme.RegularFont,
            PlaceholderText = "Tên, mã SP, chất liệu...",
            Margin = new Padding(0, 4, 15, 4)
        };
        _txtSearch.TextChanged += (s, e) => FilterProducts();

        var lblCat = new Label
        {
            Text = "Danh mục:",
            AutoSize = true,
            ForeColor = UITheme.TextSecondary,
            Margin = new Padding(0, 8, 6, 0)
        };

        _cboCategory = new ComboBox
        {
            Width = 145,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = UITheme.RegularFont,
            Margin = new Padding(0, 4, 15, 4)
        };
        _cboCategory.Items.AddRange(new object[] { "Tất Cả Danh Mục", "DM01", "DM02", "DM03", "DM04" });
        _cboCategory.SelectedIndex = 0;
        _cboCategory.SelectedIndexChanged += (s, e) => FilterProducts();

        // Cập nhật giá nhanh
        var lblPrice = new Label
        {
            Text = "Giá mới (VND):",
            AutoSize = true,
            ForeColor = UITheme.TextSecondary,
            Margin = new Padding(0, 8, 6, 0)
        };

        _txtEditPrice = new TextBox
        {
            Width = 115,
            Height = 28,
            Font = UITheme.TabularNumericFont,
            Margin = new Padding(0, 4, 12, 4)
        };

        _btnUpdatePrice = UITheme.CreateFlatButton("Cập Nhật Giá", UITheme.PrimaryGold, Color.Black);
        _btnUpdatePrice.Margin = new Padding(0, 2, 8, 2);
        _btnUpdatePrice.Click += BtnUpdatePrice_Click;

        _btnAddProduct = UITheme.CreateFlatButton("+ Thêm Sản Phẩm", UITheme.DarkHeader, Color.White);
        _btnAddProduct.Margin = new Padding(0, 2, 8, 2);
        _btnAddProduct.Click += BtnAddProduct_Click;

        _btnRefresh = UITheme.CreateFlatButton("⟳ Nạp Lại", Color.FromArgb(241, 245, 249), UITheme.TextPrimary);
        _btnRefresh.Margin = new Padding(0, 2, 0, 2);
        _btnRefresh.Click += (s, e) => LoadProductsData();

        toolbarPanel.Controls.Add(lblSearch);
        toolbarPanel.Controls.Add(_txtSearch);
        toolbarPanel.Controls.Add(lblCat);
        toolbarPanel.Controls.Add(_cboCategory);
        toolbarPanel.Controls.Add(lblPrice);
        toolbarPanel.Controls.Add(_txtEditPrice);
        toolbarPanel.Controls.Add(_btnUpdatePrice);
        toolbarPanel.Controls.Add(_btnAddProduct);
        toolbarPanel.Controls.Add(_btnRefresh);

        // Grid Container
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

        _dgvProducts = new DataGridView();
        UITheme.ApplyGridViewStyle(_dgvProducts);
        ConfigureGridColumns();
        _dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;

        pnlWhiteCard.Controls.Add(_dgvProducts);
        pnlGridContainer.Controls.Add(pnlWhiteCard);

        this.Controls.Add(pnlGridContainer);
        this.Controls.Add(toolbarPanel);
        this.Controls.Add(topHeaderPanel);
    }

    private Panel CreateKpiCard(string title, out Label lblValue, Color valueColor)
    {
        var card = new Panel
        {
            Width = 175,
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
        _dgvProducts.Columns.Clear();
        _dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaSanPham", HeaderText = "MÃ SP", FillWeight = 80, MinimumWidth = 95 });
        _dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenSanPham", HeaderText = "TÊN TRANG SỨC", FillWeight = 200, MinimumWidth = 220 });
        _dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaDanhMuc", HeaderText = "DANH MỤC", FillWeight = 80, MinimumWidth = 95 });
        _dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "ChatLieu", HeaderText = "CHẤT LIỆU", FillWeight = 130, MinimumWidth = 140 });
        _dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Size", HeaderText = "KÍCH CỠ", FillWeight = 75, MinimumWidth = 85 });
        _dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "GiaNhap", HeaderText = "GIÁ NHẬP (VND)", FillWeight = 110, MinimumWidth = 130, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Font = UITheme.TabularNumericFont } });
        _dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "GiaBan", HeaderText = "ĐƠN GIÁ BÁN (VND)", FillWeight = 120, MinimumWidth = 140, DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight, Font = UITheme.TabularNumericFont, ForeColor = UITheme.PrimaryGold } });
        _dgvProducts.Columns.Add(new DataGridViewTextBoxColumn { Name = "TrangThai", HeaderText = "TRẠNG THÁI KHO", FillWeight = 95, MinimumWidth = 115 });
    }

    private void LoadProductsData()
    {
        _allProducts = _services.ThongKeRepo.TruyVanSanPhamTonKho();
        UpdateKpis();
        FilterProducts();
    }

    private void UpdateKpis()
    {
        var viVn = new CultureInfo("vi-VN");
        _lblKpiTotalSku.Text = _allProducts.Count.ToString("N0", viVn);
        _lblKpiInStock.Text = _allProducts.Count(p => p.KiemTraTonKho()).ToString("N0", viVn);
        _lblKpiTotalValue.Text = _allProducts.Sum(p => p.GiaBan).ToString("N0", viVn);
    }

    private void FilterProducts()
    {
        var query = _txtSearch.Text.Trim().ToLowerInvariant();
        var selectedCat = _cboCategory.SelectedItem?.ToString() ?? "Tất Cả Danh Mục";

        var filtered = _allProducts.Where(p =>
        {
            var matchSearch = string.IsNullOrEmpty(query) ||
                              p.MaSanPham.ToLowerInvariant().Contains(query) ||
                              p.TenSanPham.ToLowerInvariant().Contains(query) ||
                              p.ChatLieu.ToLowerInvariant().Contains(query);

            var matchCat = selectedCat == "Tất Cả Danh Mục" ||
                           string.Equals(p.MaDanhMuc, selectedCat, StringComparison.OrdinalIgnoreCase);

            return matchSearch && matchCat;
        }).ToList();

        _dgvProducts.Rows.Clear();
        var viVn = new CultureInfo("vi-VN");

        foreach (var p in filtered)
        {
            var rowIdx = _dgvProducts.Rows.Add(
                p.MaSanPham,
                p.TenSanPham,
                p.MaDanhMuc,
                p.ChatLieu,
                p.Size,
                p.GiaNhap.ToString("N0", viVn),
                p.GiaBan.ToString("N0", viVn),
                p.TrangThai
            );

            var cellStatus = _dgvProducts.Rows[rowIdx].Cells["TrangThai"];
            if (string.Equals(p.TrangThai, "SanSang", StringComparison.OrdinalIgnoreCase))
                cellStatus.Style.ForeColor = UITheme.SuccessColor;
            else if (string.Equals(p.TrangThai, "DaBan", StringComparison.OrdinalIgnoreCase))
                cellStatus.Style.ForeColor = UITheme.TextSecondary;
            else
                cellStatus.Style.ForeColor = UITheme.WarningColor;
        }

        if (_dgvProducts.Rows.Count > 0)
        {
            _dgvProducts.Rows[0].Selected = true;
            UpdateSelectedPriceField();
        }
    }

    private void DgvProducts_SelectionChanged(object? sender, EventArgs e)
    {
        UpdateSelectedPriceField();
    }

    private void UpdateSelectedPriceField()
    {
        if (_dgvProducts.SelectedRows.Count == 0) return;

        var maSP = _dgvProducts.SelectedRows[0].Cells["MaSanPham"].Value?.ToString();
        var sp = _allProducts.FirstOrDefault(p => p.MaSanPham == maSP);
        if (sp != null)
        {
            _txtEditPrice.Text = sp.GiaBan.ToString("0");
        }
    }

    private void BtnUpdatePrice_Click(object? sender, EventArgs e)
    {
        if (_dgvProducts.SelectedRows.Count == 0)
        {
            MessageBox.Show("Vui lòng chọn sản phẩm cần sửa giá bán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var maSP = _dgvProducts.SelectedRows[0].Cells["MaSanPham"].Value?.ToString();
        if (string.IsNullOrEmpty(maSP)) return;

        if (!decimal.TryParse(_txtEditPrice.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var newPrice) &&
            !decimal.TryParse(_txtEditPrice.Text.Trim(), out newPrice))
        {
            MessageBox.Show("Vui lòng nhập mức giá mới hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (newPrice <= 0)
        {
            MessageBox.Show("Mức giá bán phải lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var success = _services.QuanLySanPham.CapNhatGiaBan(maSP, newPrice);
        if (success)
        {
            MessageBox.Show($"Đã cập nhật giá bán mới cho sản phẩm [{maSP}] thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadProductsData();
        }
        else
        {
            MessageBox.Show("Không thể cập nhật giá sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnAddProduct_Click(object? sender, EventArgs e)
    {
        using var dialog = new Form
        {
            Text = "Thêm Sản Phẩm Trang Sức Mới - Blink Blink Jewelry",
            Width = 480,
            Height = 440,
            StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false,
            BackColor = Color.White
        };

        var lblHead = new Label { Text = "THÔNG TIN MẶT HÀNG TRANG SỨC", Font = UITheme.SubHeaderFont, ForeColor = UITheme.TextPrimary, Location = new Point(25, 18), AutoSize = true };
        dialog.Controls.Add(lblHead);

        int top = 55;
        TextBox AddField(string labelText, string defaultVal)
        {
            var l = new Label { Text = labelText, Location = new Point(25, top), AutoSize = true, ForeColor = UITheme.TextSecondary };
            var t = new TextBox { Text = defaultVal, Location = new Point(170, top - 3), Width = 260 };
            dialog.Controls.Add(l);
            dialog.Controls.Add(t);
            top += 40;
            return t;
        }

        var txtMaSP = AddField("Mã Sản Phẩm:", $"SP{_allProducts.Count + 1:D3}");
        var txtTenSP = AddField("Tên Sản Phẩm:", "Nhẫn Kim Cương Luxury");
        var txtMaDM = AddField("Mã Danh Mục:", "DM01");
        var txtChatLieu = AddField("Chất Liệu:", "Vàng trắng 18K");
        var txtSize = AddField("Kích Cỡ / Size:", "Size 14");
        var txtGiaNhap = AddField("Giá Nhập (VND):", "15000000");
        var txtGiaBan = AddField("Giá Bán (VND):", "22500000");

        var btnSave = UITheme.CreateFlatButton("Lưu Sản Phẩm", UITheme.PrimaryGold, Color.Black);
        btnSave.Location = new Point(210, top + 10);
        btnSave.Click += (s, ev) =>
        {
            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal.TryParse(txtGiaNhap.Text, out var gNhap);
            decimal.TryParse(txtGiaBan.Text, out var gBan);

            var req = new SanPhamDTO
            {
                MaSanPham = txtMaSP.Text.Trim(),
                TenSanPham = txtTenSP.Text.Trim(),
                MaDanhMuc = txtMaDM.Text.Trim(),
                ChatLieu = txtChatLieu.Text.Trim(),
                Size = txtSize.Text.Trim(),
                GiaNhap = gNhap,
                GiaBan = gBan
            };

            var ok = _services.QuanLySanPham.ThemSanPhamMoi(req);
            if (ok)
            {
                MessageBox.Show("Thêm sản phẩm trang sức mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dialog.DialogResult = DialogResult.OK;
                dialog.Close();
            }
            else
            {
                MessageBox.Show("Không thể tạo sản phẩm. Vui lòng kiểm tra trùng mã hoặc dữ liệu nhập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };

        var btnCancel = UITheme.CreateFlatButton("Hủy Bỏ", Color.FromArgb(241, 245, 249), UITheme.TextPrimary);
        btnCancel.Location = new Point(345, top + 10);
        btnCancel.Click += (s, ev) => dialog.Close();

        dialog.Controls.Add(btnSave);
        dialog.Controls.Add(btnCancel);

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            LoadProductsData();
        }
    }
}
