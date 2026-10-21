using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlinkBlink.Domain.Entities.Catalog;
using BlinkBlink.Domain.Interfaces;
using BlinkBlink.Infrastructure.Data;

namespace BlinkBlink.Infrastructure.Repositories;

/// <summary>
/// Triển khai Repository cho thực thể SanPhamTrangSuc sử dụng Entity Framework Core.
/// Khớp 100% với Class Diagram.
/// </summary>
public class SanPhamRepository : ISanPhamRepository
{
    private readonly AppDbContext _dbContext;

    public SanPhamRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>
    /// Lưu (Thêm mới hoặc Cập nhật) sản phẩm trang sức xuống cơ sở dữ liệu.
    /// Khớp hàm: + LuuSanPham(sp: SanPhamTrangSuc): void trong Diagram.
    /// </summary>
    /// <param name="sp">Thực thể sản phẩm trang sức</param>
    public void LuuSanPham(SanPhamTrangSuc sp)
    {
        if (sp == null)
        {
            throw new ArgumentNullException(nameof(sp), "Sản phẩm không được để trống.");
        }

        var entry = _dbContext.SanPhamTrangSucs.Find(sp.MaSanPham);
        if (entry == null)
        {
            _dbContext.SanPhamTrangSucs.Add(sp);
        }
        else
        {
            _dbContext.Entry(entry).CurrentValues.SetValues(sp);
        }

        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Tìm kiếm thông tin sản phẩm trang sức theo khóa chính mã sản phẩm.
    /// Khớp hàm: + TimTheoId(maSanPham: string): SanPhamTrangSuc trong Diagram.
    /// </summary>
    /// <param name="maSanPham">Mã sản phẩm</param>
    /// <returns>Thực thể sản phẩm trang sức hoặc null nếu không tìm thấy</returns>
    public SanPhamTrangSuc? TimTheoId(string maSanPham)
    {
        if (string.IsNullOrWhiteSpace(maSanPham))
        {
            return null;
        }

        return _dbContext.SanPhamTrangSucs
            .Include(s => s.DanhMuc)
            .FirstOrDefault(s => s.MaSanPham == maSanPham.Trim());
    }

    // =========================================================================
    // MỞ RỘNG BẤT ĐỒNG BỘ CHUẨN CLEAN ARCHITECTURE .NET 8 LTS
    // =========================================================================

    public async Task LuuSanPhamAsync(SanPhamTrangSuc sp, CancellationToken cancellationToken = default)
    {
        if (sp == null)
        {
            throw new ArgumentNullException(nameof(sp), "Sản phẩm không được để trống.");
        }

        var entry = await _dbContext.SanPhamTrangSucs.FindAsync(new object?[] { sp.MaSanPham }, cancellationToken);
        if (entry == null)
        {
            await _dbContext.SanPhamTrangSucs.AddAsync(sp, cancellationToken);
        }
        else
        {
            _dbContext.Entry(entry).CurrentValues.SetValues(sp);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<SanPhamTrangSuc?> TimTheoIdAsync(string maSanPham, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(maSanPham))
        {
            return null;
        }

        return await _dbContext.SanPhamTrangSucs
            .Include(s => s.DanhMuc)
            .FirstOrDefaultAsync(s => s.MaSanPham == maSanPham.Trim(), cancellationToken);
    }
}
