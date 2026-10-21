using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlinkBlink.Domain.Entities.Inventory;

namespace BlinkBlink.Infrastructure.Data.Configurations;

/// <summary>
/// Cấu hình ánh xạ Fluent API cho thực thể ChiTietKiemKe.
/// Sử dụng khóa chính kết hợp (Composite Key): MaPhieuKiemKe + MaSanPham.
/// </summary>
public class ChiTietKiemKeConfiguration : IEntityTypeConfiguration<ChiTietKiemKe>
{
    public void Configure(EntityTypeBuilder<ChiTietKiemKe> builder)
    {
        builder.ToTable("ChiTietKiemKe");

        // Khóa chính kết hợp
        builder.HasKey(c => new { c.MaPhieuKiemKe, c.MaSanPham });

        builder.Property(c => c.MaPhieuKiemKe)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(c => c.MaSanPham)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(c => c.TonSoSach)
            .IsRequired();

        builder.Property(c => c.TonThucTe)
            .IsRequired();

        builder.Property(c => c.ChenhLech)
            .IsRequired();

        builder.Property(c => c.LyDoChenhLech)
            .HasMaxLength(300);

        builder.HasOne(c => c.SanPhamTrangSuc)
            .WithMany()
            .HasForeignKey(c => c.MaSanPham)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
