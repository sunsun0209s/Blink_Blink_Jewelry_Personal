using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlinkBlink.Domain.Entities.Inventory;
using BlinkBlink.Domain.Interfaces;
using BlinkBlink.Infrastructure.Data;

namespace BlinkBlink.Infrastructure.Repositories;

/// <summary>
/// Triển khai Repository cho thực thể PhieuNhapKho.
/// Khớp 100% tên lớp PhieuNhapKhoRepositoryImpl trong Class Diagram.
/// </summary>
public class PhieuNhapKhoRepositoryImpl : IPhieuNhapKhoRepository
{
    private readonly AppDbContext _db;

    public PhieuNhapKhoRepositoryImpl(AppDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    /// <summary>
    /// Lưu phiếu nhập kho và các dòng chi tiết sản phẩm xuống cơ sở dữ liệu.
    /// Khớp hàm: + LuuPhieuNhap(phieu: PhieuNhapKho): void trong Diagram.
    /// </summary>
    /// <param name="phieu">Thực thể phiếu nhập kho</param>
    public void LuuPhieuNhap(PhieuNhapKho phieu)
    {
        if (phieu == null)
        {
            throw new ArgumentNullException(nameof(phieu), "Phiếu nhập kho không được để trống.");
        }

        var entry = _db.PhieuNhapKhos
            .Include(p => p.ChiTietNhapKhos)
            .FirstOrDefault(p => p.MaPhieuNhap == phieu.MaPhieuNhap);

        if (entry == null)
        {
            _db.PhieuNhapKhos.Add(phieu);
        }
        else
        {
            _db.Entry(entry).CurrentValues.SetValues(phieu);
        }

        _db.SaveChanges();
    }

    // =========================================================================
    // MỞ RỘNG BẤT ĐỒNG BỘ CHUẨN CLEAN ARCHITECTURE .NET 8 LTS
    // =========================================================================

    public async Task LuuPhieuNhapAsync(PhieuNhapKho phieu, CancellationToken cancellationToken = default)
    {
        if (phieu == null)
        {
            throw new ArgumentNullException(nameof(phieu), "Phiếu nhập kho không được để trống.");
        }

        var entry = await _db.PhieuNhapKhos
            .Include(p => p.ChiTietNhapKhos)
            .FirstOrDefaultAsync(p => p.MaPhieuNhap == phieu.MaPhieuNhap, cancellationToken);

        if (entry == null)
        {
            await _db.PhieuNhapKhos.AddAsync(phieu, cancellationToken);
        }
        else
        {
            _db.Entry(entry).CurrentValues.SetValues(phieu);
        }

        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<PhieuNhapKho?> TimTheoIdAsync(string maPhieu, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(maPhieu))
        {
            return null;
        }

        return await _db.PhieuNhapKhos
            .Include(p => p.ChiTietNhapKhos)
                .ThenInclude(c => c.SanPhamTrangSuc)
            .FirstOrDefaultAsync(p => p.MaPhieuNhap == maPhieu.Trim(), cancellationToken);
    }
}
