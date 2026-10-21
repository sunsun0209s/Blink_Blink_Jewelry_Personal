using Microsoft.EntityFrameworkCore;
using BlinkBlink.Domain.Entities.Identity;
using BlinkBlink.Domain.Entities.Customers;
using BlinkBlink.Domain.Entities.Catalog;
using BlinkBlink.Domain.Entities.Inventory;
using BlinkBlink.Domain.Entities.Orders;
using BlinkBlink.Domain.Entities.Cart;

namespace BlinkBlink.Infrastructure.Data;

/// <summary>
/// Lớp ngữ cảnh cơ sở dữ liệu Entity Framework Core trung tâm cho toàn bộ hệ thống Blink Blink Jewelry.
/// Phục vụ kết nối SQL Server và ánh xạ các thực thể sang các bảng dữ liệu quan hệ.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // =========================================================================
    // PHÂN HỆ NGƯỜI DÙNG & PHÂN QUYỀN 
    // =========================================================================
    public DbSet<VaiTro> VaiTros => Set<VaiTro>();
    public DbSet<NguoiDung> NguoiDungs => Set<NguoiDung>();
    public DbSet<NhanVien> NhanViens => Set<NhanVien>();
    public DbSet<KhachHang> KhachHangs => Set<KhachHang>();

    // =========================================================================
    // PHÂN HỆ QUẢN LÝ SẢN PHẨM & KHO HÀNG
    // =========================================================================
    public DbSet<SanPhamTrangSuc> SanPhamTrangSucs => Set<SanPhamTrangSuc>();
    public DbSet<DanhMuc> DanhMucs => Set<DanhMuc>();
    public DbSet<PhieuNhapKho> PhieuNhapKhos => Set<PhieuNhapKho>();
    public DbSet<ChiTietNhapKho> ChiTietNhapKhos => Set<ChiTietNhapKho>();
    public DbSet<PhieuKiemKe> PhieuKiemKes => Set<PhieuKiemKe>();
    public DbSet<ChiTietKiemKe> ChiTietKiemKes => Set<ChiTietKiemKe>();

    // =========================================================================
    // PHÂN HỆ QUẢN LÝ BÁN HÀNG
    // =========================================================================
    public DbSet<HoaDon> HoaDons => Set<HoaDon>();
    public DbSet<ChiTietHoaDon> ChiTietHoaDons => Set<ChiTietHoaDon>();
    public DbSet<GioHang> GioHangs => Set<GioHang>();
    public DbSet<ChiTietGioHang> ChiTietGioHangs => Set<ChiTietGioHang>();

    // =========================================================================
    // HƯỚNG DẪN CHO CÁC PHÂN HỆ KHÁC (Bán hàng, Dịch vụ & Bảo hành):
    // 1. Khai báo thêm DbSet<TEntity> của phân hệ bạn tại đây.
    // 2. Tạo file cấu hình Fluent API riêng trong thư mục Data/Configurations/ kế thừa IEntityTypeConfiguration<TEntity>.
    // 3. Hàm ApplyConfigurationsFromAssembly bên dưới sẽ TỰ ĐỘNG nạp cấu hình của bạn mà KHÔNG cần chỉnh sửa OnModelCreating.
    // =========================================================================

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Tự động quét và áp dụng toàn bộ EntityTypeConfiguration trong Assembly này
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
