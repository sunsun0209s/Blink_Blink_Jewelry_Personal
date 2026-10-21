using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlinkBlink.Domain.Entities.Catalog;
using BlinkBlink.Domain.Interfaces;
using BlinkBlink.Infrastructure.Data;

namespace BlinkBlink.Infrastructure.Repositories;

/// <summary>
/// Triển khai Repository cho thực thể DanhMuc sử dụng Entity Framework Core.
/// Khớp 100% với Class Diagram.
/// </summary>
public class DanhMucRepository : IDanhMucRepository
{
    private readonly AppDbContext _dbContext;

    public DanhMucRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>
    /// Lưu danh mục xuống CSDL.
    /// Khớp hàm: + LuuDanhMuc(dm: DanhMuc): void trong Diagram.
    /// </summary>
    /// <param name="dm">Thực thể danh mục</param>
    public void LuuDanhMuc(DanhMuc dm)
    {
        if (dm == null)
        {
            throw new ArgumentNullException(nameof(dm), "Danh mục không được để trống.");
        }

        var entry = _dbContext.DanhMucs.Find(dm.MaDanhMuc);
        if (entry == null)
        {
            _dbContext.DanhMucs.Add(dm);
        }
        else
        {
            _dbContext.Entry(entry).CurrentValues.SetValues(dm);
        }

        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Tìm danh mục theo mã định danh.
    /// </summary>
    /// <param name="maDanhMuc">Mã danh mục</param>
    /// <returns>Thực thể danh mục hoặc null</returns>
    public DanhMuc? TimTheoId(string maDanhMuc)
    {
        if (string.IsNullOrWhiteSpace(maDanhMuc))
        {
            return null;
        }

        return _dbContext.DanhMucs
            .Include(d => d.SanPhamTrangSucs)
            .FirstOrDefault(d => d.MaDanhMuc == maDanhMuc.Trim());
    }

    // =========================================================================
    // MỞ RỘNG BẤT ĐỒNG BỘ CHUẨN CLEAN ARCHITECTURE .NET 8 LTS
    // =========================================================================

    public async Task LuuDanhMucAsync(DanhMuc dm, CancellationToken cancellationToken = default)
    {
        if (dm == null)
        {
            throw new ArgumentNullException(nameof(dm), "Danh mục không được để trống.");
        }

        var entry = await _dbContext.DanhMucs.FindAsync(new object?[] { dm.MaDanhMuc }, cancellationToken);
        if (entry == null)
        {
            await _dbContext.DanhMucs.AddAsync(dm, cancellationToken);
        }
        else
        {
            _dbContext.Entry(entry).CurrentValues.SetValues(dm);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<DanhMuc?> TimTheoIdAsync(string maDanhMuc, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(maDanhMuc))
        {
            return null;
        }

        return await _dbContext.DanhMucs
            .Include(d => d.SanPhamTrangSucs)
            .FirstOrDefaultAsync(d => d.MaDanhMuc == maDanhMuc.Trim(), cancellationToken);
    }
}
