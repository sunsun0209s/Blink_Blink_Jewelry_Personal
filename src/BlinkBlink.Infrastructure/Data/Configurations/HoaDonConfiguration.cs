using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlinkBlink.Domain.Entities.Orders;

namespace BlinkBlink.Infrastructure.Data.Configurations;

/// <summary>
/// Cấu hình ánh xạ Fluent API cho thực thể HoaDon.
/// Khớp 100% với Class Diagram kỹ thuật và cấu trúc bảng CSDL SQL Server.
/// </summary>
public class HoaDonConfiguration : IEntityTypeConfiguration<HoaDon>
{
    public void Configure(EntityTypeBuilder<HoaDon> builder)
    {
        builder.ToTable("HoaDon");

        builder.HasKey(h => h.MaHoaDon);

        builder.Property(h => h.MaHoaDon)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(h => h.NgayLap)
            .IsRequired();

        builder.Property(h => h.MaNhanVien)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired(false);

        builder.Property(h => h.MaKhachHang)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(h => h.DiaChiGiaoHang)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(h => h.SoDienThoaiNhan)
            .HasMaxLength(20)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(h => h.PhuongThucThanhToan)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(h => h.TrangThaiDonHang)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(h => h.TongTienHang)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(h => h.TienGiamGia)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(h => h.TongThanhToan)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(h => h.GhiChu)
            .HasMaxLength(500);

        // Quan hệ với KhachHang
        builder.HasOne(h => h.KhachHang)
            .WithMany()
            .HasForeignKey(h => h.MaKhachHang)
            .OnDelete(DeleteBehavior.Restrict);

        // Quan hệ với NhanVien (nhân viên duyệt đơn, có thể null ban đầu)
        builder.HasOne(h => h.NhanVien)
            .WithMany()
            .HasForeignKey(h => h.MaNhanVien)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        // Quan hệ 1-N với ChiTietHoaDon
        builder.HasMany(h => h.ChiTietHoaDons)
            .WithOne(c => c.HoaDon)
            .HasForeignKey(c => c.SoHoaDon)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
