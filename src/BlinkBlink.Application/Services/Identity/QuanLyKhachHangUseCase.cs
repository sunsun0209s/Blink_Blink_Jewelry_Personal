using System;
using BlinkBlink.Application.DTOs.Identity;
using BlinkBlink.Domain.Entities.Customers;
using BlinkBlink.Domain.Interfaces;

namespace BlinkBlink.Application.Services.Identity;

/// <summary>
/// Use Case quản lý thông tin và tài khoản khách hàng trên hệ thống.
/// Bản vẽ kỹ thuật: Phân hệ quản lý người dùng & phân quyền.xml - QuanLyKhachHangUseCase
/// </summary>
public class QuanLyKhachHangUseCase
{
    private readonly IKhachHangRepository _khachHangRepo;

    /// <summary>
    /// Khởi tạo Use Case với repository quản lý khách hàng
    /// </summary>
    /// <param name="khachHangRepo">Kho lưu trữ dữ liệu khách hàng</param>
    public QuanLyKhachHangUseCase(IKhachHangRepository khachHangRepo)
    {
        _khachHangRepo = khachHangRepo ?? throw new ArgumentNullException(nameof(khachHangRepo));
    }

    /// <summary>
    /// Cập nhật thông tin hồ sơ cá nhân của khách hàng
    /// </summary>
    /// <param name="request">Dữ liệu thông tin khách hàng cần cập nhật</param>
    /// <returns>True nếu cập nhật thành công; ngược lại False</returns>
    public bool CapNhatThongTin(CapNhatKhachHangDTO request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.MaKhachHang) || string.IsNullOrWhiteSpace(request.HoTen))
        {
            return false;
        }

        var khachHang = _khachHangRepo.TimTheoMa(request.MaKhachHang.Trim());
        if (khachHang == null)
        {
            return false;
        }

        khachHang.HoTen = request.HoTen.Trim();
        khachHang.SoDienThoai = request.SoDienThoai?.Trim() ?? string.Empty;
        khachHang.NgaySinh = request.NgaySinh;

        _khachHangRepo.LuuKhachHang(khachHang);
        return true;
    }

    /// <summary>
    /// Khóa tài khoản khách hàng trong trường hợp vi phạm chính sách hoặc theo yêu cầu
    /// </summary>
    /// <param name="maKh">Mã khách hàng cần khóa tài khoản</param>
    /// <returns>True nếu khóa tài khoản thành công; ngược lại False</returns>
    public bool KhoaTaiKhoanKhachHang(string maKh)
    {
        if (string.IsNullOrWhiteSpace(maKh))
        {
            return false;
        }

        var khachHang = _khachHangRepo.TimTheoMa(maKh.Trim());
        if (khachHang == null)
        {
            return false;
        }

        if (khachHang.TrangThai)
        {
            khachHang.KhoaTaiKhoan();
        }
        else
        {
            khachHang.TrangThai = true;
        }

        _khachHangRepo.LuuKhachHang(khachHang);
        return true;
    }
}
