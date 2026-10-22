using System.Drawing;
using System.Windows.Forms;

namespace BlinkBlink.Admin.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.panelSidebar = new Panel();
        this.lblBrand = new Label();
        this.btnSales = new Button();
        this.btnProducts = new Button();
        this.btnWarranty = new Button();
        this.btnUsers = new Button();
        this.btnReports = new Button();
        this.panelHeader = new Panel();
        this.lblHeaderTitle = new Label();
        this.lblUserRole = new Label();
        this.panelContent = new Panel();
        this.statusStrip = new StatusStrip();
        this.statusLabel = new ToolStripStatusLabel();

        // 
        // panelSidebar
        // 
        this.panelSidebar.BackColor = Color.FromArgb(24, 28, 36);
        this.panelSidebar.Dock = DockStyle.Left;
        this.panelSidebar.Width = 280;
        this.panelSidebar.Controls.Add(this.btnReports);
        this.panelSidebar.Controls.Add(this.btnWarranty);
        this.panelSidebar.Controls.Add(this.btnProducts);
        this.panelSidebar.Controls.Add(this.btnSales);
        this.panelSidebar.Controls.Add(this.btnUsers);
        this.panelSidebar.Controls.Add(this.lblBrand);

        // 
        // lblBrand
        // 
        this.lblBrand.Dock = DockStyle.Top;
        this.lblBrand.Height = 80;
        this.lblBrand.Text = "BLINK BLINK\nJEWELRY";
        this.lblBrand.ForeColor = Color.FromArgb(212, 175, 55); // Luxury Champagne Gold
        this.lblBrand.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        this.lblBrand.TextAlign = ContentAlignment.MiddleCenter;
        this.lblBrand.UseMnemonic = false;

        // 
        // Sidebar Buttons Helper Styling
        // 
        ConfigureSidebarButton(this.btnSales, "🛒 Quản Lý Bán Hàng", 85);
        ConfigureSidebarButton(this.btnProducts, "💎 Sản Phẩm & Kho", 139);
        ConfigureSidebarButton(this.btnWarranty, "🛡️ Dịch Vụ & Bảo Hành", 193);
        ConfigureSidebarButton(this.btnUsers, "👥 Khách Hàng & Nhân Sự", 247);
        ConfigureSidebarButton(this.btnReports, "📊 Báo Cáo & Thống Kê", 301);

        // 
        // panelHeader
        // 
        this.panelHeader.BackColor = Color.FromArgb(32, 38, 48);
        this.panelHeader.Dock = DockStyle.Top;
        this.panelHeader.Height = 60;
        this.panelHeader.Controls.Add(this.lblHeaderTitle);
        this.panelHeader.Controls.Add(this.lblUserRole);

        // 
        // lblHeaderTitle
        // 
        this.lblHeaderTitle.Dock = DockStyle.Left;
        this.lblHeaderTitle.AutoSize = true;
        this.lblHeaderTitle.UseMnemonic = false;
        this.lblHeaderTitle.Padding = new Padding(20, 0, 0, 0);
        this.lblHeaderTitle.Text = "Bảng Điều Khiển Hệ Thống Quản Trị";
        this.lblHeaderTitle.ForeColor = Color.White;
        this.lblHeaderTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        this.lblHeaderTitle.TextAlign = ContentAlignment.MiddleLeft;

        // 
        // lblUserRole
        // 
        this.lblUserRole.Dock = DockStyle.Right;
        this.lblUserRole.AutoSize = true;
        this.lblUserRole.UseMnemonic = false;
        this.lblUserRole.Padding = new Padding(0, 0, 20, 0);
        this.lblUserRole.Text = "Tài khoản: Quản Trị Viên (Admin) | .NET 8 WinForms";
        this.lblUserRole.ForeColor = Color.FromArgb(170, 180, 195);
        this.lblUserRole.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
        this.lblUserRole.TextAlign = ContentAlignment.MiddleRight;

        // 
        // panelContent
        // 
        this.panelContent.BackColor = Color.FromArgb(245, 247, 250);
        this.panelContent.Dock = DockStyle.Fill;

        // 
        // statusStrip
        // 
        this.statusStrip.Items.Add(this.statusLabel);
        this.statusLabel.Text = "Hệ thống sẵn sàng | Clean Architecture .NET 8 LTS";

        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new SizeF(8F, 20F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(1480, 880);
        this.WindowState = FormWindowState.Maximized;
        this.MinimumSize = new Size(1280, 720);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Blink Blink Jewelry - Quản Trị Hệ Thống";
        this.Controls.Add(this.panelContent);
        this.Controls.Add(this.panelHeader);
        this.Controls.Add(this.panelSidebar);
        this.Controls.Add(this.statusStrip);
    }

    private void ConfigureSidebarButton(Button btn, string text, int top)
    {
        btn.Text = text;
        btn.Top = top;
        btn.Left = 0;
        btn.Width = 280;
        btn.Height = 50;
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.ForeColor = Color.FromArgb(220, 225, 235);
        btn.BackColor = Color.FromArgb(24, 28, 36);
        btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
        btn.TextAlign = ContentAlignment.MiddleLeft;
        btn.Padding = new Padding(18, 0, 8, 0);
        btn.AutoEllipsis = false;
        btn.Cursor = Cursors.Hand;
        btn.UseMnemonic = false;
    }

    private Panel panelSidebar;
    private Label lblBrand;
    private Button btnSales;
    private Button btnProducts;
    private Button btnWarranty;
    private Button btnUsers;
    private Button btnReports;
    private Panel panelHeader;
    private Label lblHeaderTitle;
    private Label lblUserRole;
    private Panel panelContent;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel statusLabel;
}
