using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlinkBlink.Domain.Entities.Inventory;
using BlinkBlink.Domain.Interfaces;
using BlinkBlink.Infrastructure.Data;

namespace BlinkBlink.Infrastructure.Repositories;

/// <summary>
/// Triển khai Repository cho thực thể PhieuKiemKe.
/// Khớp 100% với Class Diagram.
/// </summary>
public class PhieuKiemKeRepository : IPhieuKiemKeRepository
{
    private readonly AppDbContext _dbContext;

    public PhieuKiemKeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>
    /// Lưu phiếu kiểm kê và các chi tiết kiểm kê vào CSDL.
    /// Khớp hàm: + LuuPhieuKiemKe(phieu: PhieuKiemKe): void trong Diagram.
    /// </summary>
    /// <param name="phieu">Thực thể phiếu kiểm kê</param>
    public void LuuPhieuKiemKe(PhieuKiemKe phieu)
    {
        if (phieu == null)
        {
            throw new ArgumentNullException(nameof(phieu), "Phiếu kiểm kê không được để trống.");
        }

        var entry = _dbContext.PhieuKiemKes
            .Include(p => p.ChiTietKiemKes)
            .FirstOrDefault(p => p.MaPhieuKiemKe == phieu.MaPhieuKiemKe);

        if (entry == null)
        {
            _dbContext.PhieuKiemKes.Add(phieu);
        }
        else
        {
            _dbContext.Entry(entry).CurrentValues.SetValues(phieu);
        }

        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Tìm phiếu kiểm kê theo mã.
    /// Khớp hàm: + TimTheoId(maPhieu: string): PhieuKiemKe trong Diagram.
    /// </summary>
    /// <param name="maPhieu">Mã phiếu kiểm kê</param>
    /// <returns>Thực thể phiếu kiểm kê hoặc null</returns>
    public PhieuKiemKe? TimTheoId(string maPhieu)
    {
        if (string.IsNullOrWhiteSpace(maPhieu))
        {
            return null;
        }

        return _dbContext.PhieuKiemKes
            .Include(p => p.ChiTietKiemKes)
                .ThenInclude(c => c.SanPhamTrangSuc)
            .FirstOrDefault(p => p.MaPhieuKiemKe == maPhieu.Trim());
    }

    // =========================================================================
    // MỞ RỘNG BẤT ĐỒNG BỘ CHUẨN CLEAN ARCHITECTURE .NET 8 LTS
    // =========================================================================

    public async Task LuuPhieuKiemKeAsync(PhieuKiemKe phieu, CancellationToken cancellationToken = default)
    {
        if (phieu == null)
        {
            throw new ArgumentNullException(nameof(phieu), "Phiếu kiểm kê không được để trống.");
        }

        var entry = await _dbContext.PhieuKiemKes
            .Include(p => p.ChiTietKiemKes)
            .FirstOrDefaultAsync(p => p.MaPhieuKiemKe == phieu.MaPhieuKiemKe, cancellationToken);

        if (entry == null)
        {
            await _dbContext.PhieuKiemKes.AddAsync(phieu, cancellationToken);
        }
        else
        {
            _dbContext.Entry(entry).CurrentValues.SetValues(phieu);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<PhieuKiemKe?> TimTheoIdAsync(string maPhieu, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(maPhieu))
        {
            return null;
        }

        return await _dbContext.PhieuKiemKes
            .Include(p => p.ChiTietKiemKes)
                .ThenInclude(c => c.SanPhamTrangSuc)
            .FirstOrDefaultAsync(p => p.MaPhieuKiemKe == maPhieu.Trim(), cancellationToken);
    }
}
