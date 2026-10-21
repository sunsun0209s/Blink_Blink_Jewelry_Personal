using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlinkBlink.Domain.Entities.Cart;
using BlinkBlink.Domain.Interfaces;
using BlinkBlink.Infrastructure.Data;

namespace BlinkBlink.Infrastructure.Repositories;

/// <summary>
/// Triển khai Kho lưu trữ giỏ hàng (GioHangRepository) sử dụng Entity Framework Core.
/// Khớp 100% với Class Diagram kỹ thuật và XML sơ đồ phân hệ bán hàng.
/// </summary>
public class GioHangRepository : IGioHangRepository
{
    private readonly AppDbContext _dbContext;

    public GioHangRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>
    /// Lấy giỏ hàng của khách hàng theo mã định danh khách hàng (đồng bộ).
    /// Khớp phương thức: + LayGioHang(maKH: string): GioHang trong Diagram.
    /// </summary>
    /// <param name="maKH">Mã khách hàng</param>
    /// <returns>Giỏ hàng cùng danh sách sản phẩm hoặc null</returns>
    public GioHang? LayGioHang(string maKH)
    {
        if (string.IsNullOrWhiteSpace(maKH))
        {
            return null;
        }

        return _dbContext.GioHangs
            .Include(g => g.DanhSachSanPham)
                .ThenInclude(c => c.SanPham)
            .FirstOrDefault(g => g.MaKhachHang == maKH);
    }

    /// <summary>
    /// Lấy giỏ hàng của khách hàng theo mã định danh khách hàng (bất đồng bộ).
    /// </summary>
    public async Task<GioHang?> LayGioHangAsync(string maKH, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(maKH))
        {
            return null;
        }

        return await _dbContext.GioHangs
            .Include(g => g.DanhSachSanPham)
                .ThenInclude(c => c.SanPham)
            .FirstOrDefaultAsync(g => g.MaKhachHang == maKH, cancellationToken);
    }

    /// <summary>
    /// Lưu mới giỏ hàng vào cơ sở dữ liệu (bất đồng bộ).
    /// </summary>
    public async Task LuuGioHangAsync(GioHang gh, CancellationToken cancellationToken = default)
    {
        if (gh == null)
        {
            throw new ArgumentNullException(nameof(gh), "Giỏ hàng không được để trống.");
        }

        await _dbContext.GioHangs.AddAsync(gh, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Cập nhật giỏ hàng và danh sách sản phẩm bên trong (đồng bộ).
    /// Khớp phương thức: + CapNhatGioHang(gh: GioHang): void trong Diagram.
    /// </summary>
    /// <param name="gh">Thực thể giỏ hàng</param>
    public void CapNhatGioHang(GioHang gh)
    {
        if (gh == null)
        {
            throw new ArgumentNullException(nameof(gh), "Giỏ hàng không được để trống.");
        }

        var entry = _dbContext.GioHangs.Local.FirstOrDefault(g => g.MaGioHang == gh.MaGioHang);
        if (entry == null)
        {
            _dbContext.GioHangs.Update(gh);
        }

        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Cập nhật giỏ hàng và danh sách sản phẩm bên trong (bất đồng bộ).
    /// </summary>
    public async Task CapNhatGioHangAsync(GioHang gh, CancellationToken cancellationToken = default)
    {
        if (gh == null)
        {
            throw new ArgumentNullException(nameof(gh), "Giỏ hàng không được để trống.");
        }

        var entry = _dbContext.GioHangs.Local.FirstOrDefault(g => g.MaGioHang == gh.MaGioHang);
        if (entry == null)
        {
            _dbContext.GioHangs.Update(gh);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
