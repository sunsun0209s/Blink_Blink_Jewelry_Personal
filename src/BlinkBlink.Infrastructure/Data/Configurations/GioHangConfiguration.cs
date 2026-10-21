using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlinkBlink.Domain.Entities.Cart;

namespace BlinkBlink.Infrastructure.Data.Configurations;

/// <summary>
/// Cấu hình ánh xạ Fluent API cho thực thể GioHang.
/// Khớp 100% với Class Diagram kỹ thuật.
/// </summary>
public class GioHangConfiguration : IEntityTypeConfiguration<GioHang>
{
    public void Configure(EntityTypeBuilder<GioHang> builder)
    {
        builder.ToTable("GioHang");

        builder.HasKey(g => g.MaGioHang);

        builder.Property(g => g.MaGioHang)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(g => g.MaKhachHang)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(g => g.SoLuong)
            .IsRequired();

        // Quan hệ tham chiếu tới KhachHang
        builder.HasOne(g => g.KhachHang)
            .WithMany()
            .HasForeignKey(g => g.MaKhachHang)
            .OnDelete(DeleteBehavior.Cascade);

        // Quan hệ 1-N với ChiTietGioHang
        builder.HasMany(g => g.DanhSachSanPham)
            .WithOne(c => c.GioHang)
            .HasForeignKey(c => c.MaGioHang)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
