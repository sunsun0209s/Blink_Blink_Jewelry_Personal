using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlinkBlink.Application.DTOs.Catalog;
using BlinkBlink.Application.Services.Catalog;

namespace BlinkBlink.Web.Controllers;

/// <summary>
/// Controller xử lý các tác vụ quản lý sản phẩm trang sức.
/// Khớp 100% với Class Diagram (Interface Adapters / Presentation Layer).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SanPhamController : ControllerBase
{
    private readonly QuanLySanPhamUseCase _quanLySanPhamUseCase;

    public SanPhamController(QuanLySanPhamUseCase quanLySanPhamUseCase)
    {
        _quanLySanPhamUseCase = quanLySanPhamUseCase ?? throw new ArgumentNullException(nameof(quanLySanPhamUseCase));
    }

    /// <summary>
    /// Thêm sản phẩm trang sức mới.
    /// Khớp hàm: + HttpPost TaoSanPham(request: SanPhamDTO): HttpResponse trong Diagram.
    /// </summary>
    /// <param name="request">Thông tin sản phẩm cần tạo</param>
    /// <param name="cancellationToken">Token hủy tác vụ</param>
    /// <returns>HTTP Status 200/201 nếu thành công, 400 nếu dữ liệu lỗi</returns>
    [HttpPost]
    public async Task<IActionResult> TaoSanPham([FromBody] SanPhamDTO request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            return BadRequest(new { Message = "Dữ liệu sản phẩm không hợp lệ." });
        }

        var result = await _quanLySanPhamUseCase.ThemSanPhamMoiAsync(request, cancellationToken);
        if (!result)
        {
            return BadRequest(new { Message = "Không thể tạo sản phẩm. Kiểm tra lại thông tin hoặc mã sản phẩm đã tồn tại." });
        }

        return Ok(new { Success = true, Message = "Tạo mới sản phẩm trang sức thành công." });
    }

    /// <summary>
    /// Cập nhật giá bán sản phẩm trang sức.
    /// Khớp hàm: + HttpPut DoiGiaBan(maSanPham: string, request: DoiGiaDTO): HttpResponse trong Diagram.
    /// </summary>
    /// <param name="maSanPham">Mã sản phẩm</param>
    /// <param name="request">DTO chứa giá bán mới</param>
    /// <param name="cancellationToken">Token hủy tác vụ</param>
    /// <returns>HTTP Status 200 nếu thành công, 400 hoặc 404 nếu lỗi</returns>
    [HttpPut("{maSanPham}/doi-gia")]
    public async Task<IActionResult> DoiGiaBan(string maSanPham, [FromBody] DoiGiaDTO request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(maSanPham) || request == null || request.GiaMoi <= 0)
        {
            return BadRequest(new { Message = "Mã sản phẩm hoặc giá bán mới không hợp lệ." });
        }

        var result = await _quanLySanPhamUseCase.CapNhatGiaBanAsync(maSanPham, request.GiaMoi, cancellationToken);
        if (!result)
        {
            return NotFound(new { Message = $"Không tìm thấy sản phẩm mã '{maSanPham}' để điều chỉnh giá." });
        }

        return Ok(new { Success = true, Message = $"Đã cập nhật giá bán mới thành {request.GiaMoi:N0} VNĐ cho sản phẩm '{maSanPham}'." });
    }

    /// <summary>
    /// Cập nhật thông tin chi tiết sản phẩm trang sức (tên, danh mục, chất liệu, kích cỡ).
    /// Khớp hàm: + HttpPut CapNhatThongTin(request: CapNhatSanPhamDTO): HttpResponse trong Diagram.
    /// </summary>
    /// <param name="request">DTO chứa thông tin sản phẩm cập nhật</param>
    /// <param name="cancellationToken">Token hủy tác vụ</param>
    /// <returns>HTTP Status 200 nếu thành công, 400 hoặc 404 nếu lỗi</returns>
    [HttpPut("cap-nhat-thong-tin")]
    public async Task<IActionResult> CapNhatThongTin([FromBody] CapNhatSanPhamDTO request, CancellationToken cancellationToken)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.MaSanPham))
        {
            return BadRequest(new { Message = "Thông tin cập nhật sản phẩm không hợp lệ." });
        }

        var result = await _quanLySanPhamUseCase.CapNhatThongTinAsync(request, cancellationToken);
        if (!result)
        {
            return NotFound(new { Message = $"Không tìm thấy sản phẩm mã '{request.MaSanPham}' để cập nhật thông tin." });
        }

        return Ok(new { Success = true, Message = $"Cập nhật thông tin sản phẩm '{request.MaSanPham}' thành công." });
    }
}
