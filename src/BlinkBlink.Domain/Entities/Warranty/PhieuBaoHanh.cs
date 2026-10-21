using System;
using BlinkBlink.Domain.Entities.Catalog;
using BlinkBlink.Domain.Entities.Customers;

namespace BlinkBlink.Domain.Entities.Warranty;

/// <summary>
/// Thực thể Phiếu bảo hành (PhieuBaoHanh) quản lý dịch vụ bảo hành và hậu mãi cho trang sức.
/// Khớp 100% với Class Diagram kỹ thuật và XML sơ đồ phân hệ dịch vụ & bảo hành.
/// </summary>
public class PhieuBaoHanh
{
    /// <summary>
    /// Số phiếu bảo hành (Khóa chính, ví dụ: BH20261022001)
    /// Khớp thuộc tính: - _soPhieuBH: string trong Diagram
    /// </summary>
    public string SoPhieuBH { get; set; } = string.Empty;

    /// <summary>
    /// Mã khách hàng gửi sản phẩm bảo hành
    /// Khớp thuộc tính: - _maKhachHang: string trong Diagram
    /// </summary>
    public string MaKhachHang { get; set; } = string.Empty;

    /// <summary>
    /// Mã sản phẩm trang sức cần bảo hành
    /// Khớp thuộc tính: - _maSanPham: string trong Diagram
    /// </summary>
    public string MaSanPham { get; set; } = string.Empty;

    /// <summary>
    /// Lý do hoặc nội dung dịch vụ bảo hành (đánh bóng, thu/nới size, gắn đá rơi)
    /// Khớp thuộc tính: - _lyDoBaoHanh: string trong Diagram
    /// </summary>
    public string LyDoBaoHanh { get; set; } = string.Empty;

    /// <summary>
    /// Chi phí bảo hành thực tế (bằng 0 nếu miễn phí trọn đời)
    /// Khớp thuộc tính: - _chiPhiBaoHanh: decimal trong Diagram
    /// </summary>
    public decimal ChiPhiBaoHanh { get; set; }

    /// <summary>
    /// Trạng thái xử lý: DangBaoHanh, DaHoanTat, DaHuy
    /// Khớp thuộc tính: - _trangThaiXuLy: string trong Diagram
    /// </summary>
    public string TrangThaiXuLy { get; set; } = "DangBaoHanh";

    /// <summary>
    /// Thời điểm lập phiếu tiếp nhận sản phẩm
    /// </summary>
    public DateTime NgayTiepNhan { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Ngày hẹn bàn giao lại trang sức cho khách hàng
    /// </summary>
    public DateTime? NgayHenTra { get; set; }

    // Quan hệ điều hướng (Navigation properties)
    public virtual KhachHang? KhachHang { get; set; }
    public virtual SanPhamTrangSuc? SanPham { get; set; }

    public PhieuBaoHanh() { }

    public PhieuBaoHanh(
        string soPhieuBH,
        string maKhachHang,
        string maSanPham,
        string lyDoBaoHanh,
        decimal chiPhiBaoHanh = 0m,
        string trangThaiXuLy = "DangBaoHanh",
        DateTime? ngayTiepNhan = null,
        DateTime? ngayHenTra = null)
    {
        SoPhieuBH = soPhieuBH;
        MaKhachHang = maKhachHang;
        MaSanPham = maSanPham;
        LyDoBaoHanh = lyDoBaoHanh;
        ChiPhiBaoHanh = chiPhiBaoHanh;
        TrangThaiXuLy = trangThaiXuLy;
        NgayTiepNhan = ngayTiepNhan ?? DateTime.UtcNow;
        NgayHenTra = ngayHenTra;
    }

    /// <summary>
    /// Cập nhật tiến độ xử lý kỹ thuật của phiếu bảo hành.
    /// Khớp phương thức: + CapNhatTrangThai(tt: string): bool trong Diagram.
    /// </summary>
    /// <param name="tt">Trạng thái mới</param>
    /// <returns>True nếu cập nhật hợp lệ, False nếu chuỗi rỗng</returns>
    public bool CapNhatTrangThai(string tt)
    {
        if (string.IsNullOrWhiteSpace(tt))
        {
            return false;
        }

        TrangThaiXuLy = tt.Trim();
        return true;
    }

    /// <summary>
    /// Hoàn tất công tác bảo hành trang sức, chuyển trạng thái sang DaHoanTat.
    /// Khớp phương thức: + HoanTatBaoHanh(): bool trong Diagram.
    /// </summary>
    /// <returns>True nếu cập nhật thành công</returns>
    public bool HoanTatBaoHanh()
    {
        TrangThaiXuLy = "DaHoanTat";
        return true;
    }

    /// <summary>
    /// Tính toán hoặc kết xuất số tiền cần thanh toán cho dịch vụ bảo hành này.
    /// Khớp phương thức: + TinhPhiBaoHanh(): decimal trong Diagram.
    /// </summary>
    /// <returns>Số tiền chi phí bảo hành</returns>
    public decimal TinhPhiBaoHanh()
    {
        return Math.Max(0m, ChiPhiBaoHanh);
    }
}
