using System;
using Microsoft.AspNetCore.Mvc;
using BlinkBlink.Application.DTOs.Identity;
using BlinkBlink.Application.Services.Identity;
using BlinkBlink.Domain.Interfaces;

namespace BlinkBlink.Web.Controllers;

/// <summary>
/// Controller xử lý các nghiệp vụ Khách hàng (Đăng ký tài khoản, Tra cứu hồ sơ).
/// Bản vẽ kỹ thuật: Phân hệ quản lý người dùng & phân quyền.xml - KhachHangController
/// Khớp 100% với Class Diagram kỹ thuật.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class KhachHangController : ControllerBase
{
    private readonly DangKyKhachHangUseCase _dangKyUseCase;
    private readonly QuanLyKhachHangUseCase _quanLyKhachHangUseCase;
    private readonly IKhachHangRepository _khachHangRepo;

    public KhachHangController(
        DangKyKhachHangUseCase dangKyUseCase,
        QuanLyKhachHangUseCase quanLyKhachHangUseCase,
        IKhachHangRepository khachHangRepo)
    {
        _dangKyUseCase = dangKyUseCase ?? throw new ArgumentNullException(nameof(dangKyUseCase));
        _quanLyKhachHangUseCase = quanLyKhachHangUseCase ?? throw new ArgumentNullException(nameof(quanLyKhachHangUseCase));
        _khachHangRepo = khachHangRepo ?? throw new ArgumentNullException(nameof(khachHangRepo));
    }

    /// <summary>
    /// Đăng ký tài khoản khách hàng mới.
    /// Khớp phương thức: + HttpPost DangKy(request: DangKyKhachHangDTO): HttpResponse trong Diagram.
    /// </summary>
    /// <param name="request">Thông tin đăng ký của khách hàng</param>
    /// <returns>HTTP Status 200 nếu thành công, 400 nếu dữ liệu không hợp lệ hoặc đã tồn tại</returns>
    [HttpPost("dang-ky")]
    public IActionResult DangKy([FromBody] DangKyKhachHangDTO request)
    {
        if (request == null)
        {
            return BadRequest(new { Success = false, Message = "Dữ liệu đăng ký không được để trống." });
        }

        if (string.IsNullOrWhiteSpace(request.HoTen) ||
            string.IsNullOrWhiteSpace(request.SoDienThoai) ||
            string.IsNullOrWhiteSpace(request.TenDangNhap) ||
            string.IsNullOrWhiteSpace(request.MatKhau))
        {
            return BadRequest(new { Success = false, Message = "Vui lòng nhập đầy đủ các thông tin bắt buộc." });
        }

        try
        {
            var result = _dangKyUseCase.Execute(request);
            if (!result)
            {
                return BadRequest(new { Success = false, Message = "Không thể đăng ký tài khoản. Thông tin không hợp lệ." });
            }

            return Ok(new
            {
                Success = true,
                Message = "Đăng ký tài khoản khách hàng thành công.",
                Data = new
                {
                    HoTen = request.HoTen,
                    TenDangNhap = request.TenDangNhap,
                    SoDienThoai = request.SoDienThoai
                }
            });
        }
        catch (Microsoft.Data.SqlClient.SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
        {
            return BadRequest(new { Success = false, Message = "Tên đăng nhập này đã được sử dụng. Quý khách vui lòng chọn tên đăng nhập khác." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Success = false, Message = "Lỗi khi đăng ký tài khoản: " + ex.Message });
        }
    }

    /// <summary>
    /// Cập nhật hồ sơ thông tin cá nhân khách hàng.
    /// Khớp Use Case: QuanLyKhachHangUseCase.CapNhatThongTin
    /// </summary>
    /// <param name="request">Dữ liệu cập nhật khách hàng</param>
    /// <returns>HTTP Status 200 nếu thành công, 400 nếu thất bại</returns>
    [HttpPut("cap-nhat")]
    public IActionResult CapNhatThongTin([FromBody] CapNhatKhachHangDTO request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.MaKhachHang))
        {
            return BadRequest(new { Success = false, Message = "Dữ liệu cập nhật không hợp lệ." });
        }

        var khHienTai = _khachHangRepo.TimTheoMa(request.MaKhachHang);
        if (khHienTai == null)
        {
            return NotFound(new { Success = false, Message = "Không tìm thấy hồ sơ khách hàng để cập nhật." });
        }

        if (!khHienTai.TrangThai)
        {
            return StatusCode(403, new { Success = false, Message = "Tài khoản của quý khách hiện đang bị tạm khóa. Vui lòng liên hệ bộ phận hỗ trợ khách hàng." });
        }

        var result = _quanLyKhachHangUseCase.CapNhatThongTin(request);
        if (!result)
        {
            return BadRequest(new { Success = false, Message = "Không thể cập nhật hồ sơ khách hàng. Vui lòng kiểm tra lại thông tin." });
        }

        var khMoi = _khachHangRepo.TimTheoMa(request.MaKhachHang);
        return Ok(new
        {
            Success = true,
            Message = "Cập nhật thông tin cá nhân thành công.",
            Data = new
            {
                khMoi?.MaKhachHang,
                khMoi?.MaNguoiDung,
                khMoi?.HoTen,
                khMoi?.SoDienThoai,
                khMoi?.TenDangNhap,
                khMoi?.NgaySinh,
                khMoi?.TrangThai
            }
        });
    }

    /// <summary>
    /// Tra cứu thông tin hồ sơ khách hàng theo mã khách hàng hoặc mã người dùng.
    /// </summary>
    /// <param name="maKh">Mã khách hàng hoặc mã người dùng</param>
    /// <returns>Hồ sơ khách hàng</returns>
    [HttpGet("{maKh}")]
    public IActionResult LayThongTin(string maKh)
    {
        if (string.IsNullOrWhiteSpace(maKh))
        {
            return BadRequest(new { Success = false, Message = "Mã khách hàng không hợp lệ." });
        }

        var khachHang = _khachHangRepo.TimTheoMa(maKh);
        if (khachHang == null)
        {
            return NotFound(new { Success = false, Message = "Không tìm thấy hồ sơ khách hàng." });
        }

        return Ok(new
        {
            Success = true,
            Data = new
            {
                khachHang.MaKhachHang,
                khachHang.MaNguoiDung,
                khachHang.HoTen,
                khachHang.SoDienThoai,
                khachHang.TenDangNhap,
                khachHang.NgaySinh,
                khachHang.TrangThai
            }
        });
    }
}
