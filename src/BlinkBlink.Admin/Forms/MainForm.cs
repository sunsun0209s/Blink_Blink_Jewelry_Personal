using System;
using System.Drawing;
using System.Windows.Forms;

namespace BlinkBlink.Admin.Forms;

/// <summary>
/// Giao diện chính của hệ thống quản trị Blink Blink Jewelry trên nền Windows Forms.
/// Thiết kế chuẩn thanh lịch, sang trọng phù hợp với ngành kim hoàn.
/// </summary>
public partial class MainForm : Form
{
    private Button? _currentActiveButton;

    public MainForm()
    {
        InitializeComponent();
        RegisterEventHandlers();
        HighlightNavButton(btnSales);
    }

    private void RegisterEventHandlers()
    {
        btnSales.Click += (s, e) => NavigateTo("Quản Lý Bán Hàng & POS", btnSales);
        btnProducts.Click += (s, e) => NavigateTo("Quản Lý Sản Phẩm & Tồn Kho", btnProducts);
        btnWarranty.Click += (s, e) => NavigateTo("Dịch Vụ & Tiếp Nhận Bảo Hành", btnWarranty);
        btnUsers.Click += (s, e) => NavigateTo("Quản Lý Khách Hàng & Phân Quyền Nhân Sự", btnUsers);
        btnReports.Click += (s, e) => NavigateTo("Báo Cáo Doanh Thu & Thống Kê", btnReports);
    }

    private void NavigateTo(string moduleName, Button targetButton)
    {
        lblHeaderTitle.Text = moduleName;
        HighlightNavButton(targetButton);
        statusLabel.Text = $"Đang hiển thị phân hệ: {moduleName} | Trạng thái: Hoạt động bình thường";
    }

    private void HighlightNavButton(Button btn)
    {
        if (_currentActiveButton != null)
        {
            _currentActiveButton.BackColor = Color.FromArgb(24, 28, 36);
            _currentActiveButton.ForeColor = Color.FromArgb(220, 225, 235);
        }

        _currentActiveButton = btn;
        _currentActiveButton.BackColor = Color.FromArgb(40, 48, 62);
        _currentActiveButton.ForeColor = Color.FromArgb(212, 175, 55); // Highlight Gold
    }
}
