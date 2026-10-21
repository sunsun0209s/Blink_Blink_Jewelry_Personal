using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlinkBlink.Domain.Entities.Orders;
using BlinkBlink.Domain.Interfaces;
using BlinkBlink.Infrastructure.Data;

namespace BlinkBlink.Infrastructure.Repositories;

/// <summary>
/// Triển khai Kho lưu trữ hóa đơn / đơn hàng (HoaDonRepository) sử dụng Entity Framework Core.
/// Khớp 100% với Class Diagram kỹ thuật và XML sơ đồ phân hệ bán hàng.
/// </summary>
public class HoaDonRepository : IHoaDonRepository
{
    private readonly AppDbContext _dbContext;

    public HoaDonRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>
    /// Lưu hóa đơn mới vào cơ sở dữ liệu (đồng bộ).
    /// Khớp phương thức: + LuuHoaDon(hd: HoaDon): void trong Diagram.
    /// </summary>
    /// <param name="hd">Thực thể hóa đơn</param>
    public void LuuHoaDon(HoaDon hd)
    {
        if (hd == null)
        {
            throw new ArgumentNullException(nameof(hd), "Hóa đơn không được để trống.");
        }

        _dbContext.HoaDons.Add(hd);
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Lưu hóa đơn mới vào cơ sở dữ liệu (bất đồng bộ).
    /// </summary>
    public async Task LuuHoaDonAsync(HoaDon hd, CancellationToken cancellationToken = default)
    {
        if (hd == null)
        {
            throw new ArgumentNullException(nameof(hd), "Hóa đơn không được để trống.");
        }

        await _dbContext.HoaDons.AddAsync(hd, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Tìm hóa đơn theo mã định danh (đồng bộ).
    /// Khớp phương thức: + TimTheoMa(maHoaDon: string): HoaDon trong Diagram.
    /// </summary>
    /// <param name="maHoaDon">Mã hóa đơn</param>
    /// <returns>Hóa đơn kèm chi tiết hoặc null</returns>
    public HoaDon? TimTheoMa(string maHoaDon)
    {
        if (string.IsNullOrWhiteSpace(maHoaDon))
        {
            return null;
        }

        return _dbContext.HoaDons
            .Include(h => h.ChiTietHoaDons)
                .ThenInclude(c => c.SanPham)
            .Include(h => h.KhachHang)
            .Include(h => h.NhanVien)
            .FirstOrDefault(h => h.MaHoaDon == maHoaDon);
    }

    /// <summary>
    /// Tìm hóa đơn theo mã định danh (bất đồng bộ).
    /// </summary>
    public async Task<HoaDon?> TimTheoMaAsync(string maHoaDon, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(maHoaDon))
        {
            return null;
        }

        return await _dbContext.HoaDons
            .Include(h => h.ChiTietHoaDons)
                .ThenInclude(c => c.SanPham)
            .Include(h => h.KhachHang)
            .Include(h => h.NhanVien)
            .FirstOrDefaultAsync(h => h.MaHoaDon == maHoaDon, cancellationToken);
    }

    /// <summary>
    /// Lấy danh sách lịch sử đơn hàng của khách hàng (đồng bộ).
    /// Khớp phương thức: + LayTheoKhachHang(maKH: string): List&lt;HoaDon&gt; trong Diagram.
    /// </summary>
    /// <param name="maKH">Mã khách hàng</param>
    /// <returns>Danh sách hóa đơn của khách</returns>
    public List<HoaDon> LayTheoKhachHang(string maKH)
    {
        if (string.IsNullOrWhiteSpace(maKH))
        {
            return new List<HoaDon>();
        }

        return _dbContext.HoaDons
            .Include(h => h.ChiTietHoaDons)
                .ThenInclude(c => c.SanPham)
            .Where(h => h.MaKhachHang == maKH)
            .OrderByDescending(h => h.NgayLap)
            .ToList();
    }

    /// <summary>
    /// Lấy danh sách lịch sử đơn hàng của khách hàng (bất đồng bộ).
    /// </summary>
    public async Task<List<HoaDon>> LayTheoKhachHangAsync(string maKH, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(maKH))
        {
            return new List<HoaDon>();
        }

        return await _dbContext.HoaDons
            .Include(h => h.ChiTietHoaDons)
                .ThenInclude(c => c.SanPham)
            .Where(h => h.MaKhachHang == maKH)
            .OrderByDescending(h => h.NgayLap)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Lấy toàn bộ danh sách đơn đặt hàng trong hệ thống (đồng bộ).
    /// Khớp phương thức: + LayDanhSachDonHang(): List&lt;HoaDon&gt; trong Diagram.
    /// </summary>
    /// <returns>Danh sách tất cả hóa đơn</returns>
    public List<HoaDon> LayDanhSachDonHang()
    {
        return _dbContext.HoaDons
            .Include(h => h.ChiTietHoaDons)
                .ThenInclude(c => c.SanPham)
            .Include(h => h.KhachHang)
            .Include(h => h.NhanVien)
            .OrderByDescending(h => h.NgayLap)
            .ToList();
    }

    /// <summary>
    /// Lấy toàn bộ danh sách đơn đặt hàng trong hệ thống (bất đồng bộ).
    /// </summary>
    public async Task<List<HoaDon>> LayDanhSachDonHangAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.HoaDons
            .Include(h => h.ChiTietHoaDons)
                .ThenInclude(c => c.SanPham)
            .Include(h => h.KhachHang)
            .Include(h => h.NhanVien)
            .OrderByDescending(h => h.NgayLap)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Cập nhật thông tin hóa đơn (đồng bộ).
    /// Khớp phương thức: + CapNhatHoaDon(hd: HoaDon): void trong Diagram.
    /// </summary>
    /// <param name="hd">Thực thể hóa đơn</param>
    public void CapNhatHoaDon(HoaDon hd)
    {
        if (hd == null)
        {
            throw new ArgumentNullException(nameof(hd), "Hóa đơn không được để trống.");
        }

        var tracked = _dbContext.HoaDons.Local.FirstOrDefault(e => e.MaHoaDon == hd.MaHoaDon);
        if (tracked == null)
        {
            _dbContext.HoaDons.Update(hd);
        }
        else if (!ReferenceEquals(tracked, hd))
        {
            _dbContext.Entry(tracked).CurrentValues.SetValues(hd);
        }

        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Cập nhật thông tin hóa đơn (bất đồng bộ).
    /// </summary>
    public async Task CapNhatHoaDonAsync(HoaDon hd, CancellationToken cancellationToken = default)
    {
        if (hd == null)
        {
            throw new ArgumentNullException(nameof(hd), "Hóa đơn không được để trống.");
        }

        var tracked = _dbContext.HoaDons.Local.FirstOrDefault(e => e.MaHoaDon == hd.MaHoaDon);
        if (tracked == null)
        {
            _dbContext.HoaDons.Update(hd);
        }
        else if (!ReferenceEquals(tracked, hd))
        {
            _dbContext.Entry(tracked).CurrentValues.SetValues(hd);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
