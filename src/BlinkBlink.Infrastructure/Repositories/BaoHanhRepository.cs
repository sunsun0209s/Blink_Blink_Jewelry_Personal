using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlinkBlink.Domain.Entities.Warranty;
using BlinkBlink.Domain.Interfaces;
using BlinkBlink.Infrastructure.Data;

namespace BlinkBlink.Infrastructure.Repositories;

/// <summary>
/// Triển khai Kho lưu trữ phiếu bảo hành (BaoHanhRepository) sử dụng Entity Framework Core.
/// Khớp 100% với Class Diagram kỹ thuật và XML sơ đồ phân hệ bảo hành.
/// </summary>
public class BaoHanhRepository : IBaoHanhRepository
{
    private readonly AppDbContext _dbContext;

    public BaoHanhRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>
    /// Lưu phiếu bảo hành mới vào cơ sở dữ liệu (đồng bộ).
    /// Khớp phương thức: + LuuPhieu(pbh: PhieuBaoHanh): void trong Diagram.
    /// </summary>
    /// <param name="pbh">Thực thể phiếu bảo hành</param>
    public void LuuPhieu(PhieuBaoHanh pbh)
    {
        if (pbh == null)
        {
            throw new ArgumentNullException(nameof(pbh), "Phiếu bảo hành không được để trống.");
        }

        _dbContext.PhieuBaoHanhs.Add(pbh);
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Lưu phiếu bảo hành mới vào cơ sở dữ liệu (bất đồng bộ).
    /// </summary>
    public async Task LuuPhieuAsync(PhieuBaoHanh pbh, CancellationToken cancellationToken = default)
    {
        if (pbh == null)
        {
            throw new ArgumentNullException(nameof(pbh), "Phiếu bảo hành không được để trống.");
        }

        await _dbContext.PhieuBaoHanhs.AddAsync(pbh, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Tìm phiếu bảo hành theo số phiếu định danh (đồng bộ).
    /// Khớp phương thức: + TimTheoSoPhieu(soPhieu: string): PhieuBaoHanh trong Diagram.
    /// </summary>
    /// <param name="soPhieu">Số phiếu bảo hành</param>
    /// <returns>Phiếu bảo hành kèm thông tin khách và sản phẩm hoặc null</returns>
    public PhieuBaoHanh? TimTheoSoPhieu(string soPhieu)
    {
        if (string.IsNullOrWhiteSpace(soPhieu))
        {
            return null;
        }

        return _dbContext.PhieuBaoHanhs
            .Include(p => p.KhachHang)
            .Include(p => p.SanPham)
            .FirstOrDefault(p => p.SoPhieuBH == soPhieu);
    }

    /// <summary>
    /// Tìm phiếu bảo hành theo số phiếu định danh (bất đồng bộ).
    /// </summary>
    public async Task<PhieuBaoHanh?> TimTheoSoPhieuAsync(string soPhieu, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(soPhieu))
        {
            return null;
        }

        return await _dbContext.PhieuBaoHanhs
            .Include(p => p.KhachHang)
            .Include(p => p.SanPham)
            .FirstOrDefaultAsync(p => p.SoPhieuBH == soPhieu, cancellationToken);
    }

    /// <summary>
    /// Lấy danh sách phiếu bảo hành của một khách hàng (đồng bộ).
    /// Khớp phương thức: + LayTheoKhachHang(maKH: string): List&lt;PhieuBaoHanh&gt; trong Diagram.
    /// </summary>
    /// <param name="maKH">Mã khách hàng</param>
    /// <returns>Danh sách phiếu bảo hành của khách</returns>
    public List<PhieuBaoHanh> LayTheoKhachHang(string maKH)
    {
        if (string.IsNullOrWhiteSpace(maKH))
        {
            return new List<PhieuBaoHanh>();
        }

        return _dbContext.PhieuBaoHanhs
            .Include(p => p.SanPham)
            .Where(p => p.MaKhachHang == maKH)
            .OrderByDescending(p => p.NgayTiepNhan)
            .ToList();
    }

    /// <summary>
    /// Lấy danh sách phiếu bảo hành của một khách hàng (bất đồng bộ).
    /// </summary>
    public async Task<List<PhieuBaoHanh>> LayTheoKhachHangAsync(string maKH, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(maKH))
        {
            return new List<PhieuBaoHanh>();
        }

        return await _dbContext.PhieuBaoHanhs
            .Include(p => p.SanPham)
            .Where(p => p.MaKhachHang == maKH)
            .OrderByDescending(p => p.NgayTiepNhan)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Lấy toàn bộ danh sách phiếu bảo hành trong hệ thống (đồng bộ).
    /// </summary>
    /// <returns>Danh sách tất cả phiếu bảo hành</returns>
    public List<PhieuBaoHanh> LayDanhSachPhieu()
    {
        return _dbContext.PhieuBaoHanhs
            .Include(p => p.KhachHang)
            .Include(p => p.SanPham)
            .OrderByDescending(p => p.NgayTiepNhan)
            .ToList();
    }

    /// <summary>
    /// Lấy toàn bộ danh sách phiếu bảo hành trong hệ thống (bất đồng bộ).
    /// </summary>
    public async Task<List<PhieuBaoHanh>> LayDanhSachPhieuAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.PhieuBaoHanhs
            .Include(p => p.KhachHang)
            .Include(p => p.SanPham)
            .OrderByDescending(p => p.NgayTiepNhan)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Cập nhật thông tin phiếu bảo hành (đồng bộ).
    /// </summary>
    /// <param name="pbh">Thực thể phiếu bảo hành</param>
    public void CapNhatPhieu(PhieuBaoHanh pbh)
    {
        if (pbh == null)
        {
            throw new ArgumentNullException(nameof(pbh), "Phiếu bảo hành không được để trống.");
        }

        var tracked = _dbContext.PhieuBaoHanhs.Local.FirstOrDefault(e => e.SoPhieuBH == pbh.SoPhieuBH);
        if (tracked == null)
        {
            _dbContext.PhieuBaoHanhs.Update(pbh);
        }
        else if (!ReferenceEquals(tracked, pbh))
        {
            _dbContext.Entry(tracked).CurrentValues.SetValues(pbh);
        }

        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Cập nhật thông tin phiếu bảo hành (bất đồng bộ).
    /// </summary>
    public async Task CapNhatPhieuAsync(PhieuBaoHanh pbh, CancellationToken cancellationToken = default)
    {
        if (pbh == null)
        {
            throw new ArgumentNullException(nameof(pbh), "Phiếu bảo hành không được để trống.");
        }

        var tracked = _dbContext.PhieuBaoHanhs.Local.FirstOrDefault(e => e.SoPhieuBH == pbh.SoPhieuBH);
        if (tracked == null)
        {
            _dbContext.PhieuBaoHanhs.Update(pbh);
        }
        else if (!ReferenceEquals(tracked, pbh))
        {
            _dbContext.Entry(tracked).CurrentValues.SetValues(pbh);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
