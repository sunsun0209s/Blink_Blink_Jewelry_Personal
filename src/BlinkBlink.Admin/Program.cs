using System;
using BlinkBlink.Admin.Forms;
using WinFormsApp = System.Windows.Forms.Application;

namespace BlinkBlink.Admin;

/// <summary>
/// Điểm khởi chạy chính của ứng dụng quản trị Windows Forms Blink Blink Jewelry.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Hàm Main khởi tạo ứng dụng desktop
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Khởi tạo cấu hình ứng dụng WinForms trong .NET 8 LTS (hỗ trợ High DPI, Visual Styles)
        ApplicationConfiguration.Initialize();
        WinFormsApp.Run(new MainForm());
    }
}
