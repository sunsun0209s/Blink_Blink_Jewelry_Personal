using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlinkBlink.Domain.Entities.Cart;

namespace BlinkBlink.Infrastructure.Data.Configurations;

/// <summary>
/// Cấu hình ánh xạ Fluent API cho thực thể ChiTietGioHang.
/// Sử dụng khóa chính kết hợp (Composite Key): MaGioHang + MaSanPham.
/// Khớp 100% với Class Diagram kỹ thuật.
/// </summary>
public class ChiTietGioHangConfiguration : IEntityTypeConfiguration<ChiTietGioHang>
{
    public void Configure(EntityTypeBuilder<ChiTietGioHang> builder)
    {
        builder.ToTable("ChiTietGioHang");

        builder.HasKey(c => new { c.MaGioHang, c.MaSanPham });

        builder.Property(c => c.MaGioHang)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(c => c.MaSanPham)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(c => c.SoLuongSanPham)
            .IsRequired();

        // Quan hệ tham chiếu tới GioHang
        builder.HasOne(c => c.GioHang)
            .WithMany(g => g.DanhSachSanPham)
            .HasForeignKey(c => c.MaGioHang)
            .OnDelete(DeleteBehavior.Cascade);

        // Quan hệ tham chiếu tới SanPhamTrangSuc
        builder.HasOne(c => c.SanPham)
            .WithMany()
            .HasForeignKey(c => c.MaSanPham)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
