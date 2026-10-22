using System.Collections.Generic;
using BlinkBlink.Domain.Entities.Customers;

namespace BlinkBlink.Domain.Interfaces;

/// <summary>
/// Giao diện kho dữ liệu truy xuất và quản lý hồ sơ Khách hàng.
/// Bản vẽ kỹ thuật: Phân hệ quản lý người dùng & phân quyền.xml - IKhachHangRepository
/// </summary>
public interface IKhachHangRepository
{
    /// <summary>
    /// Lưu mới hồ sơ khách hàng vào cơ sở dữ liệu
    /// </summary>
    /// <param name="kh">Thực thể khách hàng cần lưu</param>
    void LuuKhachHang(KhachHang kh);

    /// <summary>
    /// Tìm kiếm khách hàng theo mã khách hàng
    /// </summary>
    /// <param name="maKH">Mã khách hàng (ví dụ: KH001)</param>
    /// <returns>Thực thể KhachHang nếu tìm thấy; ngược lại null</returns>
    KhachHang? TimTheoMa(string maKH);

    /// <summary>
    /// Lấy toàn bộ danh sách khách hàng trong hệ thống phục vụ quản lý Admin
    /// </summary>
    /// <returns>Danh sách thực thể khách hàng</returns>
    List<KhachHang> LayTatCa();
}
