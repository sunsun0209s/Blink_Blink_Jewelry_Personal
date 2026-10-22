using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BlinkBlink.Admin.Services;
using BlinkBlink.Admin.Views.Helpers;
using BlinkBlink.Application.DTOs.Catalog;
using BlinkBlink.Domain.Entities.Catalog;

namespace BlinkBlink.Admin.Views.UserControls.Catalog;

/// <summary>
/// Hộp thoại modal thêm mới và chỉnh sửa thông tin sản phẩm trang sức.
/// Tự động thích ứng kích thước hiển thị chống đè nút trên màn hình độ phân giải cao (High DPI 125% - 150%).
/// Hỗ trợ chọn tệp ảnh thực tế, xem trước ảnh và chọn danh mục động từ CSDL.
/// </summary>
public class ProductEditDialog : Form
{
    private readonly bool _isEditMode;
    private readonly SanPhamTrangSuc? _targetProduct;
    private readonly AdminServiceScope _services;

    private TextBox _txtMaSP = null!;
    private TextBox _txtTenSP = null!;
    private ComboBox _cboDanhMuc = null!;
    private TextBox _txtChatLieu = null!;
    private TextBox _txtSize = null!;
    private TextBox _txtGiaNhap = null!;
    private TextBox _txtGiaBan = null!;
    private NumericUpDown _nudPhanTramGiamGia = null!;

    private PictureBox _picPreview = null!;
    private Label _lblImagePath = null!;
    private string _selectedImagePath = string.Empty;

    public ProductEditDialog(bool isEditMode, SanPhamTrangSuc? targetProduct = null)
    {
        _isEditMode = isEditMode;
        _targetProduct = targetProduct;
        _services = AdminServiceScope.Instance;

        InitializeDialogLayout();
        LoadCategoryOptions();
        BindInitialData();
    }

    private void InitializeDialogLayout()
    {
        this.Text = _isEditMode ? "Chỉnh Sửa Thông Tin Trang Sức - Blink Blink Jewelry" : "Thêm Mới Sản Phẩm Trang Sức - Blink Blink Jewelry";
        this.Width = 880;
        this.Height = 700;
        this.StartPosition = FormStartPosition.CenterParent;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.BackColor = Color.White;
        this.Font = UITheme.RegularFont;

        // Top Header
        var pnlHeader = new Panel
        {
            Dock = DockStyle.Top,
            Height = 80,
            BackColor = UITheme.DarkHeader,
            Padding = new Padding(24, 16, 24, 10)
        };

        var lblHeaderTitle = new Label
        {
            Text = _isEditMode ? "CHỈNH SỬA CHI TIẾT SẢN PHẨM TRANG SỨC" : "KHAI BÁO MẶT HÀNG TRANG SỨC MỚI",
            Font = UITheme.HeaderFont,
            ForeColor = UITheme.PrimaryGold,
            Dock = DockStyle.Top,
            Height = 26,
            UseMnemonic = false
        };

        var lblHeaderSub = new Label
        {
            Text = _isEditMode 
                ? "Cập nhật tên, danh mục, thuộc tính kim hoàn, đơn giá bán niêm yết, % giảm giá và hình ảnh thực tế."
                : "Sản phẩm mới khai báo có tồn kho = 0. Giá nhập sẽ được tự động tính bình quân gia quyền khi tạo phiếu nhập kho.",
            Font = new Font("Segoe UI", 9F, FontStyle.Regular),
            ForeColor = Color.FromArgb(203, 213, 225),
            Dock = DockStyle.Fill,
            UseMnemonic = false
        };

        pnlHeader.Controls.Add(lblHeaderSub);
        pnlHeader.Controls.Add(lblHeaderTitle);

        // Bottom Footer (Chứa các nút hành động, căn lề FlowLayoutPanel chống va chạm lấn đè)
        var pnlFooter = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 72,
            BackColor = UITheme.CanvasBackground,
            Padding = new Padding(24, 16, 24, 16)
        };

        var flpButtons = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.RightToLeft,
            WrapContents = false,
            AutoSize = false
        };

        var btnCancel = UITheme.CreateFlatButton("Hủy Bỏ", Color.FromArgb(226, 232, 240), UITheme.TextPrimary);
        btnCancel.Width = 115;
        btnCancel.Height = 40;
        btnCancel.Margin = new Padding(12, 0, 0, 0);
        btnCancel.Click += (s, e) => this.Close();

        var btnSave = UITheme.CreateFlatButton(_isEditMode ? "Lưu Thay Đổi" : "Lưu Sản Phẩm", UITheme.PrimaryGold, Color.Black);
        btnSave.Width = 155;
        btnSave.Height = 40;
        btnSave.Margin = new Padding(0, 0, 0, 0);
        btnSave.Click += BtnSave_Click;

        flpButtons.Controls.Add(btnCancel);
        flpButtons.Controls.Add(btnSave);
        pnlFooter.Controls.Add(flpButtons);

        // Content Body (Chia 2 cột trái: nhập liệu rộng rãi, phải: hình ảnh)
        var pnlBody = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(24, 20, 24, 10),
            BackColor = Color.White
        };

        // Cột trái: Nhập thông số sản phẩm
        var pnlLeft = new Panel
        {
            Location = new Point(24, 15),
            Width = 490,
            Height = 490
        };

        int curY = 0;
        void AddFormRow(string labelText, Control inputControl)
        {
            var lbl = new Label
            {
                Text = labelText,
                Location = new Point(0, curY + 4),
                Width = 175,
                ForeColor = UITheme.TextSecondary,
                Font = UITheme.RegularFont,
                UseMnemonic = false
            };
            inputControl.Location = new Point(180, curY);
            inputControl.Width = 300;
            inputControl.Font = UITheme.RegularFont;

            pnlLeft.Controls.Add(lbl);
            pnlLeft.Controls.Add(inputControl);
            curY += 50;
        }

        _txtMaSP = new TextBox { Height = 30 };
        if (_isEditMode)
        {
            _txtMaSP.ReadOnly = true;
            _txtMaSP.BackColor = Color.FromArgb(241, 245, 249);
            _txtMaSP.ForeColor = UITheme.TextSecondary;
        }

        _txtTenSP = new TextBox { Height = 30, PlaceholderText = "Nhập tên mẫu trang sức..." };
        _cboDanhMuc = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Height = 30 };
        _txtChatLieu = new TextBox { Height = 30, PlaceholderText = "Ví dụ: Vàng trắng 18K, Kim cương 5.4 ly" };
        _txtSize = new TextBox { Height = 30, PlaceholderText = "Ví dụ: Size 14 hoặc Size 45cm" };

        AddFormRow("Mã Sản Phẩm (*):", _txtMaSP);
        AddFormRow("Tên Trang Sức (*):", _txtTenSP);
        AddFormRow("Danh Mục (*):", _cboDanhMuc);
        AddFormRow("Chất Liệu Kim Hoàn:", _txtChatLieu);
        AddFormRow("Kích Cỡ / Size:", _txtSize);

        if (_isEditMode)
        {
            // Trong chế độ Chỉnh sửa: Hiển thị giá nhập bình quân (chỉ xem), cho phép sửa giá bán và % giảm giá
            _txtGiaNhap = new TextBox 
            { 
                Height = 30, 
                Font = UITheme.TabularNumericFont, 
                ReadOnly = true,
                BackColor = Color.FromArgb(241, 245, 249),
                ForeColor = UITheme.TextSecondary
            };
            _txtGiaBan = new TextBox { Height = 30, Font = UITheme.TabularNumericFont };
            
            _nudPhanTramGiamGia = new NumericUpDown
            {
                Height = 30,
                Font = UITheme.TabularNumericFont,
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                Increment = 1
            };

            AddFormRow("Giá Nhập Bình Quân (VND):", _txtGiaNhap);
            AddFormRow("Đơn Giá Bán (VND) (*):", _txtGiaBan);
            AddFormRow("% Giảm Giá (0 - 100%):", _nudPhanTramGiamGia);
        }
        else
        {
            // Trong chế độ Thêm mới: Bỏ nhập giá nhập và giá bán
            _txtGiaNhap = new TextBox { Text = "0", Visible = false };
            _txtGiaBan = new TextBox { Text = "0", Visible = false };
            _nudPhanTramGiamGia = new NumericUpDown { Value = 0, Visible = false };

            var pnlHint = new Panel
            {
                Location = new Point(0, curY + 10),
                Width = 480,
                Height = 85,
                BackColor = Color.FromArgb(248, 250, 252),
                Padding = new Padding(14)
            };
            var lblHint = new Label
            {
                Text = "💡 Lưu ý định giá:\n- Giá nhập sẽ được tự động tính bình quân gia quyền từ các phiếu nhập kho.\n- Giá bán và % giảm giá có thể điều chỉnh sau khi nhập hàng vào kho.",
                ForeColor = UITheme.TextSecondary,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                Dock = DockStyle.Fill
            };
            pnlHint.Controls.Add(lblHint);
            pnlLeft.Controls.Add(pnlHint);
        }

        // Cột phải: Hình ảnh sản phẩm & Xem trước (Preview)
        var pnlRight = new Panel
        {
            Location = new Point(535, 15),
            Width = 300,
            Height = 490
        };

        var lblPicTitle = new Label
        {
            Text = "HÌNH ẢNH SẢN PHẨM",
            Location = new Point(0, 4),
            Width = 280,
            Font = UITheme.SubHeaderFont,
            ForeColor = UITheme.TextPrimary,
            UseMnemonic = false
        };

        _picPreview = new PictureBox
        {
            Location = new Point(0, 34),
            Width = 280,
            Height = 240,
            BorderStyle = BorderStyle.FixedSingle,
            SizeMode = PictureBoxSizeMode.Zoom,
            BackColor = Color.FromArgb(248, 250, 252)
        };

        var btnChooseImage = UITheme.CreateFlatButton("📁 Chọn Tệp Ảnh...", UITheme.DarkHeader, Color.White);
        btnChooseImage.Location = new Point(0, 286);
        btnChooseImage.Width = 280;
        btnChooseImage.Height = 38;
        btnChooseImage.Click += BtnChooseImage_Click;

        var btnRemoveImage = UITheme.CreateFlatButton("✕ Xóa Ảnh", Color.FromArgb(241, 245, 249), UITheme.DangerColor);
        btnRemoveImage.Location = new Point(0, 332);
        btnRemoveImage.Width = 280;
        btnRemoveImage.Height = 34;
        btnRemoveImage.Click += (s, e) =>
        {
            _selectedImagePath = string.Empty;
            _picPreview.Image?.Dispose();
            _picPreview.Image = null;
            _lblImagePath.Text = "Chưa có hình ảnh";
        };

        _lblImagePath = new Label
        {
            Location = new Point(0, 376),
            Width = 280,
            Height = 45,
            ForeColor = UITheme.TextSecondary,
            Font = new Font("Segoe UI", 8.25F, FontStyle.Italic),
            Text = "Chưa có hình ảnh",
            UseMnemonic = false
        };

        pnlRight.Controls.Add(lblPicTitle);
        pnlRight.Controls.Add(_picPreview);
        pnlRight.Controls.Add(btnChooseImage);
        pnlRight.Controls.Add(btnRemoveImage);
        pnlRight.Controls.Add(_lblImagePath);

        pnlBody.Controls.Add(pnlLeft);
        pnlBody.Controls.Add(pnlRight);

        this.Controls.Add(pnlBody);
        this.Controls.Add(pnlFooter);
        this.Controls.Add(pnlHeader);
    }

    private void LoadCategoryOptions()
    {
        _cboDanhMuc.Items.Clear();
        var categories = _services.DanhMucRepo.LayTatCa();
        if (categories.Count > 0)
        {
            foreach (var cat in categories)
            {
                _cboDanhMuc.Items.Add(new ComboBoxItem(cat.TenDanhMuc, cat.MaDanhMuc));
            }
            _cboDanhMuc.SelectedIndex = 0;
        }
        else
        {
            _cboDanhMuc.Items.Add(new ComboBoxItem("Mặc định (DM01)", "DM01"));
            _cboDanhMuc.SelectedIndex = 0;
        }
    }

    private void BindInitialData()
    {
        if (_isEditMode && _targetProduct != null)
        {
            _txtMaSP.Text = _targetProduct.MaSanPham;
            _txtTenSP.Text = _targetProduct.TenSanPham;
            _txtChatLieu.Text = _targetProduct.ChatLieu;
            _txtSize.Text = _targetProduct.Size;
            _txtGiaNhap.Text = _targetProduct.GiaNhap.ToString("N0", new CultureInfo("vi-VN"));
            _txtGiaBan.Text = _targetProduct.GiaBan.ToString("0");
            _nudPhanTramGiamGia.Value = Math.Clamp(_targetProduct.PhanTramGiamGia, 0, 100);

            // Chọn danh mục tương ứng
            for (int i = 0; i < _cboDanhMuc.Items.Count; i++)
            {
                if (_cboDanhMuc.Items[i] is ComboBoxItem item &&
                    string.Equals(item.Value, _targetProduct.MaDanhMuc, StringComparison.OrdinalIgnoreCase))
                {
                    _cboDanhMuc.SelectedIndex = i;
                    break;
                }
            }

            // Nạp hình ảnh nếu có
            if (!string.IsNullOrWhiteSpace(_targetProduct.HinhAnh))
            {
                _selectedImagePath = _targetProduct.HinhAnh;
                _lblImagePath.Text = Path.GetFileName(_selectedImagePath);
                LoadPreviewImage(_selectedImagePath);
            }
        }
        else
        {
            // Tự sinh mã mới
            var allProducts = _services.SanPhamRepo.LayTatCa();
            _txtMaSP.Text = $"SP{allProducts.Count + 1:D3}";
            _txtTenSP.Text = "";
            _txtChatLieu.Text = "";
            _txtSize.Text = "";
        }
    }

    private void BtnChooseImage_Click(object? sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog
        {
            Title = "Chọn Hình Ảnh Trang Sức",
            Filter = "Tệp Hình Ảnh (*.jpg;*.jpeg;*.png;*.webp)|*.jpg;*.jpeg;*.png;*.webp|Tất cả tệp (*.*)|*.*",
            RestoreDirectory = true
        };

        if (ofd.ShowDialog() == DialogResult.OK)
        {
            _selectedImagePath = ofd.FileName;
            _lblImagePath.Text = Path.GetFileName(_selectedImagePath);
            LoadPreviewImage(_selectedImagePath);
        }
    }

    private void LoadPreviewImage(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                // Dùng MemoryStream để không khóa tệp ảnh
                using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                using var ms = new MemoryStream();
                fs.CopyTo(ms);
                ms.Position = 0;

                _picPreview.Image?.Dispose();
                _picPreview.Image = Image.FromStream(ms);
            }
        }
        catch
        {
            _lblImagePath.Text = "Không thể tải xem trước ảnh.";
        }
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        var maSP = _txtMaSP.Text.Trim();
        var tenSP = _txtTenSP.Text.Trim();
        var selectedItem = _cboDanhMuc.SelectedItem as ComboBoxItem;
        var maDM = selectedItem?.Value ?? "DM01";
        var chatLieu = _txtChatLieu.Text.Trim();
        var size = _txtSize.Text.Trim();

        if (string.IsNullOrWhiteSpace(maSP))
        {
            MessageBox.Show("Vui lòng nhập Mã Sản Phẩm.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtMaSP.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(tenSP))
        {
            MessageBox.Show("Vui lòng nhập Tên Sản Phẩm.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtTenSP.Focus();
            return;
        }

        if (_isEditMode)
        {
            if (!decimal.TryParse(_txtGiaBan.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var giaBan) &&
                !decimal.TryParse(_txtGiaBan.Text.Trim(), out giaBan))
            {
                MessageBox.Show("Đơn giá bán không hợp lệ.", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtGiaBan.Focus();
                return;
            }

            if (giaBan < 0)
            {
                MessageBox.Show("Đơn giá bán không được là số âm.", "Cảnh báo giá bán", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtGiaBan.Focus();
                return;
            }

            var phanTramGiamGia = (int)_nudPhanTramGiamGia.Value;

            var updateDto = new CapNhatSanPhamDTO
            {
                MaSanPham = maSP,
                TenSanPham = tenSP,
                MaDanhMuc = maDM,
                ChatLieu = chatLieu,
                Size = size,
                GiaNhap = _targetProduct?.GiaNhap ?? 0,
                GiaBan = giaBan,
                HinhAnh = _selectedImagePath,
                PhanTramGiamGia = phanTramGiamGia
            };

            var success = _services.QuanLySanPham.CapNhatThongTin(updateDto);
            if (success)
            {
                MessageBox.Show($"Cập nhật thành công thông tin sản phẩm [{maSP}]!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Không thể cập nhật sản phẩm. Vui lòng kiểm tra lại kết nối cơ sở dữ liệu.", "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        else
        {
            var createDto = new SanPhamDTO
            {
                MaSanPham = maSP,
                TenSanPham = tenSP,
                MaDanhMuc = maDM,
                ChatLieu = chatLieu,
                Size = size,
                GiaNhap = 0,
                GiaBan = 0,
                HinhAnh = _selectedImagePath,
                PhanTramGiamGia = 0
            };

            var success = _services.QuanLySanPham.ThemSanPhamMoi(createDto);
            if (success)
            {
                MessageBox.Show($"Đã tạo mới sản phẩm [{maSP}] thành công!\n\nLưu ý: Sản phẩm vừa tạo có trạng thái 'Hết Hàng' (Tồn kho = 0). Vui lòng vào phân hệ 'Nhập Kho' để lập phiếu nhập hàng, hệ thống sẽ tự động tính giá vốn bình quân gia quyền và chuyển trạng thái sang 'Sẵn Sàng Bán'.", "Tạo mới thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Không thể tạo sản phẩm mới. Có thể do trùng lặp mã sản phẩm hoặc dữ liệu không hợp lệ.", "Lỗi tạo mới", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// Lớp hỗ trợ hiển thị danh mục trong ComboBox
    /// </summary>
    private class ComboBoxItem
    {
        public string Text { get; }
        public string Value { get; }

        public ComboBoxItem(string text, string value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString() => Text;
    }
}
