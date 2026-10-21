using System;
using System.Drawing;
using System.Windows.Forms;
using BlinkBlink.Admin.Services;
using BlinkBlink.Admin.Views.Helpers;
using BlinkBlink.Admin.Views.UserControls.Catalog;
using BlinkBlink.Admin.Views.UserControls.Identity;
using BlinkBlink.Admin.Views.UserControls.Orders;
using BlinkBlink.Admin.Views.UserControls.Reporting;
using BlinkBlink.Admin.Views.UserControls.Warranty;

namespace BlinkBlink.Admin.Forms;

/// <summary>
/// Giao diện chính của hệ thống quản trị Blink Blink Jewelry trên nền Windows Forms.
/// Thiết kế chuẩn thanh lịch, sang trọng phù hợp với ngành kim hoàn theo Taste Skill.
/// </summary>
public partial class MainForm : Form
{
    private Button? _currentActiveButton;
    private UserControl? _currentView;

    // Cache các UserControl phân hệ để tối ưu hiệu năng
    private OrdersView? _ordersView;
    private ProductsView? _productsView;
    private WarrantyView? _warrantyView;
    private UsersView? _usersView;
    private ReportingView? _reportingView;

    public MainForm()
    {
        InitializeComponent();
        RegisterEventHandlers();

        // Hiển thị trạng thái kết nối nguồn dữ liệu trên StatusStrip
        var isDbConnected = AdminServiceScope.Instance.IsDatabaseConnected;
        if (isDbConnected)
        {
            statusLabel.Text = "Hệ thống kết nối CSDL SQL Server thành công | Clean Architecture .NET 8 LTS";
            statusLabel.ForeColor = UITheme.SuccessColor;
        }
        else
        {
            statusLabel.Text = "Đang kích hoạt chế độ Dữ Liệu Trực Quan (In-Memory Studio Mode) | Clean Architecture .NET 8 LTS";
            statusLabel.ForeColor = UITheme.WarningColor;
        }

        // Khởi động với màn hình Quản lý Bán hàng & POS
        ShowView(GetOrdersView(), "Quản Lý Bán Hàng & Phê Duyệt Đơn", btnSales);
    }

    private void RegisterEventHandlers()
    {
        btnSales.Click += (s, e) => ShowView(GetOrdersView(), "Quản Lý Bán Hàng & Phê Duyệt Đơn", btnSales);
        btnProducts.Click += (s, e) => ShowView(GetProductsView(), "Quản Lý Sản Phẩm & Tồn Kho Kim Hoàn", btnProducts);
        btnWarranty.Click += (s, e) => ShowView(GetWarrantyView(), "Dịch Vụ & Tiếp Nhận Bảo Hành", btnWarranty);
        btnUsers.Click += (s, e) => ShowView(GetUsersView(), "Quản Lý Khách Hàng & Phân Quyền Nhân Sự", btnUsers);
        btnReports.Click += (s, e) => ShowView(GetReportingView(), "Báo Cáo Doanh Thu & Thống Kê Kho Hàng", btnReports);
    }

    private OrdersView GetOrdersView() => _ordersView ??= new OrdersView();
    private ProductsView GetProductsView() => _productsView ??= new ProductsView();
    private WarrantyView GetWarrantyView() => _warrantyView ??= new WarrantyView();
    private UsersView GetUsersView() => _usersView ??= new UsersView();
    private ReportingView GetReportingView() => _reportingView ??= new ReportingView();

    private void ShowView(UserControl view, string moduleTitle, Button navButton)
    {
        if (_currentView == view) return;

        lblHeaderTitle.Text = moduleTitle;
        HighlightNavButton(navButton);

        panelContent.SuspendLayout();
        panelContent.Controls.Clear();
        view.Dock = DockStyle.Fill;
        panelContent.Controls.Add(view);
        panelContent.ResumeLayout(true);

        _currentView = view;
    }

    private void HighlightNavButton(Button btn)
    {
        if (_currentActiveButton != null)
        {
            _currentActiveButton.BackColor = UITheme.DarkSidebar;
            _currentActiveButton.ForeColor = Color.FromArgb(220, 225, 235);
        }

        _currentActiveButton = btn;
        _currentActiveButton.BackColor = Color.FromArgb(40, 48, 62);
        _currentActiveButton.ForeColor = UITheme.PrimaryGold; // Highlight Vàng Champagne
    }
}
