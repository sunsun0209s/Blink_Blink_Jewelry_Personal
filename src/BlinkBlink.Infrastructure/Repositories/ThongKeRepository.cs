using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlinkBlink.Domain.Entities.Catalog;
using BlinkBlink.Domain.Entities.Inventory;
using BlinkBlink.Domain.Entities.Orders;
using BlinkBlink.Domain.Interfaces;
using BlinkBlink.Infrastructure.Data;

namespace BlinkBlink.Infrastructure.Repositories;

/// <summary>
/// Triển khai Kho lưu trữ báo cáo & thống kê (ThongKeRepository) sử dụng Entity Framework Core.
/// Tối ưu hóa hiệu năng truy vấn dữ liệu dạng chỉ đọc với AsNoTracking().
/// Khớp 100% với Class Diagram kỹ thuật và XML sơ đồ phân hệ báo cáo & thống kê.
/// </summary>
public class ThongKeRepository : IThongKeRepository
{
    private readonly AppDbContext _dbContext;

    public ThongKeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>
    /// Truy vấn danh sách hóa đơn bán hàng trong khoảng thời gian (đồng bộ).
    /// Khớp phương thức: + TruyVanDoanhThu(tuNgay: DateTime, denNgay: DateTime): List&lt;HoaDon&gt; trong Diagram.
    /// </summary>
    public List<HoaDon> TruyVanDoanhThu(DateTime tuNgay, DateTime denNgay)
    {
        return _dbContext.HoaDons
            .AsNoTracking()
            .Include(h => h.ChiTietHoaDons)
                .ThenInclude(c => c.SanPham)
            .Where(h => h.NgayLap >= tuNgay && h.NgayLap <= denNgay)
            .ToList();
    }

    /// <summary>
    /// Truy vấn danh sách hóa đơn bán hàng trong khoảng thời gian (bất đồng bộ).
    /// </summary>
    public async Task<List<HoaDon>> TruyVanDoanhThuAsync(DateTime tuNgay, DateTime denNgay, CancellationToken cancellationToken = default)
    {
        return await _dbContext.HoaDons
            .AsNoTracking()
            .Include(h => h.ChiTietHoaDons)
                .ThenInclude(c => c.SanPham)
            .Where(h => h.NgayLap >= tuNgay && h.NgayLap <= denNgay)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Truy vấn danh sách sản phẩm trang sức đang tồn kho sẵn sàng phục vụ bán lẻ (đồng bộ).
    /// Khớp phương thức: + TruyVanSanPhamTonKho(): List&lt;SanPhamTrangSuc&gt; trong Diagram.
    /// </summary>
    public List<SanPhamTrangSuc> TruyVanSanPhamTonKho()
    {
        return _dbContext.SanPhamTrangSucs
            .AsNoTracking()
            .Where(s => s.TrangThai == "SanSang")
            .ToList();
    }

    /// <summary>
    /// Truy vấn danh sách sản phẩm trang sức đang tồn kho sẵn sàng phục vụ bán lẻ (bất đồng bộ).
    /// </summary>
    public async Task<List<SanPhamTrangSuc>> TruyVanSanPhamTonKhoAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SanPhamTrangSucs
            .AsNoTracking()
            .Where(s => s.TrangThai == "SanSang")
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Truy vấn các dòng chi tiết kiểm kê có phát sinh chênh lệch (đồng bộ).
    /// Khớp phương thức: + TruyVanChenhLechKiemKe(): List&lt;ChiTietKiemKe&gt; trong Diagram.
    /// </summary>
    public List<ChiTietKiemKe> TruyVanChenhLechKiemKe()
    {
        return _dbContext.ChiTietKiemKes
            .AsNoTracking()
            .Where(k => k.ChenhLech != 0)
            .ToList();
    }

    /// <summary>
    /// Truy vấn các dòng chi tiết kiểm kê có phát sinh chênh lệch (bất đồng bộ).
    /// </summary>
    public async Task<List<ChiTietKiemKe>> TruyVanChenhLechKiemKeAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.ChiTietKiemKes
            .AsNoTracking()
            .Where(k => k.ChenhLech != 0)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Truy vấn danh sách toàn bộ sản phẩm trang sức để tính toán các sản phẩm doanh thu thấp (đồng bộ).
    /// Khớp phương thức: + TruyVanSanPhamDoanhThuThap(): List&lt;SanPhamTrangSuc&gt; trong Diagram.
    /// </summary>
    public List<SanPhamTrangSuc> TruyVanSanPhamDoanhThuThap()
    {
        return _dbContext.SanPhamTrangSucs
            .AsNoTracking()
            .ToList();
    }

    /// <summary>
    /// Truy vấn danh sách toàn bộ sản phẩm trang sức để tính toán các sản phẩm doanh thu thấp (bất đồng bộ).
    /// </summary>
    public async Task<List<SanPhamTrangSuc>> TruyVanSanPhamDoanhThuThapAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SanPhamTrangSucs
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
