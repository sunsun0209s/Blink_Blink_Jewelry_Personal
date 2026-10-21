using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlinkBlink.Domain.Entities.Orders;

namespace BlinkBlink.Infrastructure.Data.Configurations;

/// <summary>
/// Cấu hình ánh xạ Fluent API cho thực thể ChiTietHoaDon.
/// Sử dụng khóa chính kết hợp (Composite Key): SoHoaDon + MaSanPham.
/// Khớp 100% với Class Diagram kỹ thuật.
/// </summary>
public class ChiTietHoaDonConfiguration : IEntityTypeConfiguration<ChiTietHoaDon>
{
    public void Configure(EntityTypeBuilder<ChiTietHoaDon> builder)
    {
        builder.ToTable("ChiTietHoaDon");

        builder.HasKey(c => new { c.SoHoaDon, c.MaSanPham });

        builder.Property(c => c.SoHoaDon)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(c => c.MaSanPham)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(c => c.DonGia)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(c => c.SoLuong)
            .IsRequired();

        // Quan hệ tham chiếu đến HoaDon
        builder.HasOne(c => c.HoaDon)
            .WithMany(h => h.ChiTietHoaDons)
            .HasForeignKey(c => c.SoHoaDon)
            .OnDelete(DeleteBehavior.Cascade);

        // Quan hệ tham chiếu đến SanPhamTrangSuc
        builder.HasOne(c => c.SanPham)
            .WithMany()
            .HasForeignKey(c => c.MaSanPham)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
