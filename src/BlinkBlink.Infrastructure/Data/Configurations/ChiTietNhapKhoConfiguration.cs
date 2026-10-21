using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlinkBlink.Domain.Entities.Inventory;

namespace BlinkBlink.Infrastructure.Data.Configurations;

/// <summary>
/// Cấu hình ánh xạ Fluent API cho thực thể ChiTietNhapKho.
/// Sử dụng khóa chính kết hợp (Composite Key): MaPhieuNhap + MaSanPham.
/// </summary>
public class ChiTietNhapKhoConfiguration : IEntityTypeConfiguration<ChiTietNhapKho>
{
    public void Configure(EntityTypeBuilder<ChiTietNhapKho> builder)
    {
        builder.ToTable("ChiTietNhapKho");

        // Khóa chính kết hợp
        builder.HasKey(c => new { c.MaPhieuNhap, c.MaSanPham });

        builder.Property(c => c.MaPhieuNhap)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(c => c.MaSanPham)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(c => c.SoLuong)
            .IsRequired();

        builder.Property(c => c.GiaVonNhap)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(c => c.SoChungTu)
            .HasMaxLength(100);

        builder.HasOne(c => c.SanPhamTrangSuc)
            .WithMany()
            .HasForeignKey(c => c.MaSanPham)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
