using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlinkBlink.Application.DTOs.Inventory;
using BlinkBlink.Application.Services.Inventory;

namespace BlinkBlink.Web.Controllers;

/// <summary>
/// Controller xử lý các tác vụ liên quan đến nhập kho sản phẩm.
/// Khớp 100% với Class Diagram (Interface Adapters / Presentation Layer).
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PhieuNhapKhoController : ControllerBase
{
    private readonly XacNhanNhapKhoUseCase _usecase;

    public PhieuNhapKhoController(XacNhanNhapKhoUseCase usecase)
    {
        _usecase = usecase ?? throw new ArgumentNullException(nameof(usecase));
    }

    /// <summary>
    /// Thực hiện lập và xác nhận nhập kho sản phẩm.
    /// Khớp hàm: + NhapKho(request: NhapKhoDTO): HttpResponse trong Diagram.
    /// </summary>
    /// <param name="request">DTO phiếu nhập và các mặt hàng</param>
    /// <param name="cancellationToken">Token hủy tác vụ</param>
    /// <returns>HTTP Status 200 nếu thành công, 400 nếu dữ liệu lỗi</returns>
    [HttpPost("nhap-kho")]
    public async Task<IActionResult> NhapKho([FromBody] NhapKhoDTO request, CancellationToken cancellationToken)
    {
        if (request == null || request.DanhSachSanPham == null || request.DanhSachSanPham.Count == 0)
        {
            return BadRequest(new { Message = "Danh sách sản phẩm nhập kho không được để trống." });
        }

        try
        {
            await _usecase.ExecuteAsync(request, cancellationToken);
            return Ok(new { Success = true, Message = "Xác nhận nhập kho thành công." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = $"Lỗi xử lý nhập kho: {ex.Message}" });
        }
    }
}
