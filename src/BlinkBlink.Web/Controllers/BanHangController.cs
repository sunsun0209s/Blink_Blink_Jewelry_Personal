using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlinkBlink.Application.DTOs.Orders;
using BlinkBlink.Application.Services.Orders;
using BlinkBlink.Domain.Interfaces;

namespace BlinkBlink.Web.Controllers;

/// <summary>
/// Controller xử lý các tác vụ nghiệp vụ Quản lý bán hàng và Đơn hàng.
/// Cung cấp Restful API cho giao diện Web bán lẻ trang sức và WinForms Admin.
/// Khớp 100% với Class Diagram kỹ thuật và XML sơ đồ phân hệ bán hàng.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BanHangController : ControllerBase
{
    private readonly DatHangUseCase _datHangUseCase;
    private readonly XuLyDonHangUseCase _xuLyDonHangUseCase;
    private readonly IHoaDonRepository _hoaDonRepo;

    public BanHangController(
        DatHangUseCase datHangUseCase,
        XuLyDonHangUseCase xuLyDonHangUseCase,
        IHoaDonRepository hoaDonRepo)
    {
        _datHangUseCase = datHangUseCase ?? throw new ArgumentNullException(nameof(datHangUseCase));
        _xuLyDonHangUseCase = xuLyDonHangUseCase ?? throw new ArgumentNullException(nameof(xuLyDonHangUseCase));
        _hoaDonRepo = hoaDonRepo ?? throw new ArgumentNullException(nameof(hoaDonRepo));
    }

    /// <summary>
    /// Đặt hàng trực tuyến từ giỏ hàng.
    /// Khớp phương thức: + HttpPost DatHang(request: DatHangDTO): HttpResponse trong Diagram.
    /// </summary>
    /// <param name="request">Thông tin đặt hàng</param>
    /// <param name="cancellationToken">Mã token hủy tác vụ</param>
    /// <returns>HTTP Status 200 nếu thành công, 400 nếu dữ liệu không hợp lệ</returns>
    [HttpPost("dat-hang")]
    public async Task<IActionResult> DatHang([FromBody] DatHangDTO request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            return BadRequest(new { Success = false, Message = "Dữ liệu đặt hàng không được để trống." });
        }

        var result = await _datHangUseCase.ExecuteAsync(request, cancellationToken);
        if (!result)
        {
            return BadRequest(new { Success = false, Message = "Không thể tạo đơn hàng. Giỏ hàng rỗng hoặc sản phẩm không khả dụng." });
        }

        return Ok(new { Success = true, Message = "Đặt hàng thành công. Đơn hàng đang chờ nhân viên duyệt." });
    }

    /// <summary>
    /// Tra cứu lịch sử đơn hàng của một khách hàng cụ thể.
    /// Khớp phương thức: + HttpGet LayDonTheoKhachHang(maKH: string): HttpResponse trong Diagram.
    /// </summary>
    /// <param name="maKH">Mã khách hàng</param>
    /// <param name="cancellationToken">Mã token hủy tác vụ</param>
    /// <returns>Danh sách đơn hàng của khách</returns>
    [HttpGet("khach-hang/{maKH}")]
    public async Task<IActionResult> LayDonTheoKhachHang(string maKH, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(maKH))
        {
            return BadRequest(new { Success = false, Message = "Mã khách hàng không hợp lệ." });
        }

        var danhSach = await _hoaDonRepo.LayTheoKhachHangAsync(maKH, cancellationToken);
        return Ok(new { Success = true, Data = danhSach });
    }

    /// <summary>
    /// Lấy toàn bộ danh sách đơn đặt hàng trong hệ thống dành cho quản trị viên và nhân viên.
    /// Khớp phương thức: + HttpGet LayDanhSachDonHang(): HttpResponse trong Diagram.
    /// </summary>
    /// <param name="cancellationToken">Mã token hủy tác vụ</param>
    /// <returns>Danh sách tất cả hóa đơn</returns>
    [HttpGet("danh-sach")]
    public async Task<IActionResult> LayDanhSachDonHang(CancellationToken cancellationToken)
    {
        var danhSach = await _hoaDonRepo.LayDanhSachDonHangAsync(cancellationToken);
        return Ok(new { Success = true, Data = danhSach });
    }

    /// <summary>
    /// Cập nhật tiến độ hoặc trạng thái đơn hàng.
    /// Khớp phương thức: + HttpPut CapNhatTrangThai(request: CapNhatDonHangDTO): HttpResponse trong Diagram.
    /// </summary>
    /// <param name="request">Thông tin trạng thái mới và mã nhân viên</param>
    /// <param name="cancellationToken">Mã token hủy tác vụ</param>
    /// <returns>HTTP Status 200 nếu thành công, 400 nếu thất bại</returns>
    [HttpPut("trang-thai")]
    public async Task<IActionResult> CapNhatTrangThai([FromBody] CapNhatDonHangDTO request, CancellationToken cancellationToken)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.MaHoaDon))
        {
            return BadRequest(new { Success = false, Message = "Dữ liệu cập nhật đơn hàng không hợp lệ." });
        }

        var result = await _xuLyDonHangUseCase.CapNhatTrangThaiAsync(request.MaHoaDon, request.TrangThaiMoi, request.MaNhanVien, cancellationToken);
        if (!result)
        {
            return BadRequest(new { Success = false, Message = "Không thể cập nhật trạng thái đơn hàng. Kiểm tra mã đơn hoặc trạng thái chuyển đổi." });
        }

        return Ok(new { Success = true, Message = "Cập nhật trạng thái đơn hàng thành công." });
    }

    /// <summary>
    /// Hủy đơn hàng và hoàn lại trạng thái sản phẩm vào kho.
    /// Khớp phương thức: + HttpPut HuyDonHang(maHoaDon: string): HttpResponse trong Diagram.
    /// </summary>
    /// <param name="maHoaDon">Mã hóa đơn cần hủy</param>
    /// <param name="cancellationToken">Mã token hủy tác vụ</param>
    /// <returns>HTTP Status 200 nếu thành công, 400 nếu không thể hủy</returns>
    [HttpPut("{maHoaDon}/huy")]
    public async Task<IActionResult> HuyDonHang(string maHoaDon, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(maHoaDon))
        {
            return BadRequest(new { Success = false, Message = "Mã hóa đơn không hợp lệ." });
        }

        var result = await _xuLyDonHangUseCase.HuyDonHangAsync(maHoaDon, cancellationToken);
        if (!result)
        {
            return BadRequest(new { Success = false, Message = "Không thể hủy đơn hàng. Đơn có thể đã hoàn tất hoặc không tồn tại." });
        }

        return Ok(new { Success = true, Message = "Hủy đơn hàng thành công." });
    }
}
