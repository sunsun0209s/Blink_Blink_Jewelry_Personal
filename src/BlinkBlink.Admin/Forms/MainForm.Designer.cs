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
        this.panelSidebar = new System.Windows.Forms.Panel();
        this.lblBrand = new System.Windows.Forms.Label();
        this.btnSales = new System.Windows.Forms.Button();
        this.btnProducts = new System.Windows.Forms.Button();
        this.btnWarranty = new System.Windows.Forms.Button();
        this.btnUsers = new System.Windows.Forms.Button();
        this.btnReports = new System.Windows.Forms.Button();
        this.panelHeader = new System.Windows.Forms.Panel();
        this.lblHeaderTitle = new System.Windows.Forms.Label();
        this.lblUserRole = new System.Windows.Forms.Label();
        this.panelContent = new System.Windows.Forms.Panel();
        this.lblContentPlaceholder = new System.Windows.Forms.Label();
        this.statusStrip = new System.Windows.Forms.StatusStrip();
        this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();

        this.panelSidebar.SuspendLayout();
        this.panelHeader.SuspendLayout();
        this.panelContent.SuspendLayout();
        this.statusStrip.SuspendLayout();
        this.SuspendLayout();

        // 
        // panelSidebar
        // 
        this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(24, 28, 36);
        this.panelSidebar.Controls.Add(this.btnReports);
        this.panelSidebar.Controls.Add(this.btnUsers);
        this.panelSidebar.Controls.Add(this.btnWarranty);
        this.panelSidebar.Controls.Add(this.btnProducts);
        this.panelSidebar.Controls.Add(this.btnSales);
        this.panelSidebar.Controls.Add(this.lblBrand);
        this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
        this.panelSidebar.Location = new System.Drawing.Point(0, 0);
        this.panelSidebar.Name = "panelSidebar";
        this.panelSidebar.Size = new System.Drawing.Size(280, 856);
        this.panelSidebar.TabIndex = 0;

        // 
        // lblBrand
        // 
        this.lblBrand.Dock = System.Windows.Forms.DockStyle.Top;
        this.lblBrand.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
        this.lblBrand.ForeColor = System.Drawing.Color.FromArgb(212, 175, 55);
        this.lblBrand.Location = new System.Drawing.Point(0, 0);
        this.lblBrand.Name = "lblBrand";
        this.lblBrand.Size = new System.Drawing.Size(280, 80);
        this.lblBrand.TabIndex = 0;
        this.lblBrand.Text = "BLINK BLINK\nJEWELRY";
        this.lblBrand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.lblBrand.UseMnemonic = false;

        // 
        // btnSales
        // 
        this.btnSales.BackColor = System.Drawing.Color.FromArgb(24, 28, 36);
        this.btnSales.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btnSales.FlatAppearance.BorderSize = 0;
        this.btnSales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnSales.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
        this.btnSales.ForeColor = System.Drawing.Color.FromArgb(220, 225, 235);
        this.btnSales.Location = new System.Drawing.Point(0, 85);
        this.btnSales.Name = "btnSales";
        this.btnSales.Padding = new System.Windows.Forms.Padding(18, 0, 8, 0);
        this.btnSales.Size = new System.Drawing.Size(280, 50);
        this.btnSales.TabIndex = 1;
        this.btnSales.Text = "🛒 Quản Lý Bán Hàng";
        this.btnSales.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnSales.UseVisualStyleBackColor = false;
        this.btnSales.UseMnemonic = false;

        // 
        // btnProducts
        // 
        this.btnProducts.BackColor = System.Drawing.Color.FromArgb(24, 28, 36);
        this.btnProducts.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btnProducts.FlatAppearance.BorderSize = 0;
        this.btnProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnProducts.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
        this.btnProducts.ForeColor = System.Drawing.Color.FromArgb(220, 225, 235);
        this.btnProducts.Location = new System.Drawing.Point(0, 139);
        this.btnProducts.Name = "btnProducts";
        this.btnProducts.Padding = new System.Windows.Forms.Padding(18, 0, 8, 0);
        this.btnProducts.Size = new System.Drawing.Size(280, 50);
        this.btnProducts.TabIndex = 2;
        this.btnProducts.Text = "💎 Sản Phẩm & Kho";
        this.btnProducts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnProducts.UseVisualStyleBackColor = false;
        this.btnProducts.UseMnemonic = false;

        // 
        // btnWarranty
        // 
        this.btnWarranty.BackColor = System.Drawing.Color.FromArgb(24, 28, 36);
        this.btnWarranty.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btnWarranty.FlatAppearance.BorderSize = 0;
        this.btnWarranty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnWarranty.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
        this.btnWarranty.ForeColor = System.Drawing.Color.FromArgb(220, 225, 235);
        this.btnWarranty.Location = new System.Drawing.Point(0, 193);
        this.btnWarranty.Name = "btnWarranty";
        this.btnWarranty.Padding = new System.Windows.Forms.Padding(18, 0, 8, 0);
        this.btnWarranty.Size = new System.Drawing.Size(280, 50);
        this.btnWarranty.TabIndex = 3;
        this.btnWarranty.Text = "🛡️ Dịch Vụ & Bảo Hành";
        this.btnWarranty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnWarranty.UseVisualStyleBackColor = false;
        this.btnWarranty.UseMnemonic = false;

        // 
        // btnUsers
        // 
        this.btnUsers.BackColor = System.Drawing.Color.FromArgb(24, 28, 36);
        this.btnUsers.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btnUsers.FlatAppearance.BorderSize = 0;
        this.btnUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnUsers.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
        this.btnUsers.ForeColor = System.Drawing.Color.FromArgb(220, 225, 235);
        this.btnUsers.Location = new System.Drawing.Point(0, 247);
        this.btnUsers.Name = "btnUsers";
        this.btnUsers.Padding = new System.Windows.Forms.Padding(18, 0, 8, 0);
        this.btnUsers.Size = new System.Drawing.Size(280, 50);
        this.btnUsers.TabIndex = 4;
        this.btnUsers.Text = "👥 Khách Hàng & Nhân Sự";
        this.btnUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnUsers.UseVisualStyleBackColor = false;
        this.btnUsers.UseMnemonic = false;

        // 
        // btnReports
        // 
        this.btnReports.BackColor = System.Drawing.Color.FromArgb(24, 28, 36);
        this.btnReports.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btnReports.FlatAppearance.BorderSize = 0;
        this.btnReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnReports.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
        this.btnReports.ForeColor = System.Drawing.Color.FromArgb(220, 225, 235);
        this.btnReports.Location = new System.Drawing.Point(0, 301);
        this.btnReports.Name = "btnReports";
        this.btnReports.Padding = new System.Windows.Forms.Padding(18, 0, 8, 0);
        this.btnReports.Size = new System.Drawing.Size(280, 50);
        this.btnReports.TabIndex = 5;
        this.btnReports.Text = "📊 Báo Cáo & Thống Kê";
        this.btnReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.btnReports.UseVisualStyleBackColor = false;
        this.btnReports.UseMnemonic = false;

        // 
        // panelHeader
        // 
        this.panelHeader.BackColor = System.Drawing.Color.FromArgb(32, 38, 48);
        this.panelHeader.Controls.Add(this.lblHeaderTitle);
        this.panelHeader.Controls.Add(this.lblUserRole);
        this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
        this.panelHeader.Location = new System.Drawing.Point(280, 0);
        this.panelHeader.Name = "panelHeader";
        this.panelHeader.Size = new System.Drawing.Size(1200, 60);
        this.panelHeader.TabIndex = 1;

        // 
        // lblHeaderTitle
        // 
        this.lblHeaderTitle.AutoSize = true;
        this.lblHeaderTitle.Dock = System.Windows.Forms.DockStyle.Left;
        this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
        this.lblHeaderTitle.Location = new System.Drawing.Point(0, 0);
        this.lblHeaderTitle.Name = "lblHeaderTitle";
        this.lblHeaderTitle.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
        this.lblHeaderTitle.Size = new System.Drawing.Size(370, 28);
        this.lblHeaderTitle.TabIndex = 0;
        this.lblHeaderTitle.Text = "Bảng Điều Khiển Hệ Thống Quản Trị";
        this.lblHeaderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.lblHeaderTitle.UseMnemonic = false;

        // 
        // lblUserRole
        // 
        this.lblUserRole.AutoSize = true;
        this.lblUserRole.Dock = System.Windows.Forms.DockStyle.Right;
        this.lblUserRole.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
        this.lblUserRole.ForeColor = System.Drawing.Color.FromArgb(170, 180, 195);
        this.lblUserRole.Location = new System.Drawing.Point(825, 0);
        this.lblUserRole.Name = "lblUserRole";
        this.lblUserRole.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
        this.lblUserRole.Size = new System.Drawing.Size(375, 21);
        this.lblUserRole.TabIndex = 1;
        this.lblUserRole.Text = "Tài khoản: Quản Trị Viên (Admin) | .NET 8 WinForms";
        this.lblUserRole.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.lblUserRole.UseMnemonic = false;

        // 
        // panelContent
        // 
        this.panelContent.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
        this.panelContent.Controls.Add(this.lblContentPlaceholder);
        this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
        this.panelContent.Location = new System.Drawing.Point(280, 60);
        this.panelContent.Name = "panelContent";
        this.panelContent.Size = new System.Drawing.Size(1200, 796);
        this.panelContent.TabIndex = 2;

        // 
        // lblContentPlaceholder
        // 
        this.lblContentPlaceholder.Dock = System.Windows.Forms.DockStyle.Fill;
        this.lblContentPlaceholder.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
        this.lblContentPlaceholder.ForeColor = System.Drawing.Color.FromArgb(140, 150, 165);
        this.lblContentPlaceholder.Location = new System.Drawing.Point(0, 0);
        this.lblContentPlaceholder.Name = "lblContentPlaceholder";
        this.lblContentPlaceholder.Size = new System.Drawing.Size(1200, 796);
        this.lblContentPlaceholder.TabIndex = 0;
        this.lblContentPlaceholder.Text = "VÙNG HIỂN THỊ PHÂN HỆ QUẢN TRỊ (DYNAMIC MODULE HOST)\r\n\r\nKhi chạy ứng dụng, phân hệ Quản Lý Bán Hàng (OrdersView) sẽ tự động được nạp vào đây.\r\nĐể thiết kế chi tiết từng trang, mở trực tiếp các file UserControl trong Views/UserControls/.";
        this.lblContentPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        // 
        // statusStrip
        // 
        this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
        this.statusStrip.Location = new System.Drawing.Point(0, 856);
        this.statusStrip.Name = "statusStrip";
        this.statusStrip.Size = new System.Drawing.Size(1480, 24);
        this.statusStrip.TabIndex = 3;
        // 
        // statusLabel
        // 
        this.statusLabel.Name = "statusLabel";
        this.statusLabel.Size = new System.Drawing.Size(280, 19);
        this.statusLabel.Text = "Hệ thống sẵn sàng | Clean Architecture .NET 8 LTS";

        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1480, 880);
        this.Controls.Add(this.panelContent);
        this.Controls.Add(this.panelHeader);
        this.Controls.Add(this.panelSidebar);
        this.Controls.Add(this.statusStrip);
        this.MinimumSize = new System.Drawing.Size(1280, 720);
        this.Name = "MainForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Blink Blink Jewelry - Quản Trị Hệ Thống";
        this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

        this.panelSidebar.ResumeLayout(false);
        this.panelHeader.ResumeLayout(false);
        this.panelHeader.PerformLayout();
        this.panelContent.ResumeLayout(false);
        this.statusStrip.ResumeLayout(false);
        this.statusStrip.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
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
    private Label lblContentPlaceholder;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel statusLabel;
}
