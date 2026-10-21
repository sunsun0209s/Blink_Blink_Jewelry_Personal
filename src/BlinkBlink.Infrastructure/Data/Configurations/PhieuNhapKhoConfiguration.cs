using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlinkBlink.Domain.Entities.Inventory;

namespace BlinkBlink.Infrastructure.Data.Configurations;

/// <summary>
/// Cấu hình ánh xạ Fluent API cho thực thể PhieuNhapKho.
/// </summary>
public class PhieuNhapKhoConfiguration : IEntityTypeConfiguration<PhieuNhapKho>
{
    public void Configure(EntityTypeBuilder<PhieuNhapKho> builder)
    {
        builder.ToTable("PhieuNhapKho");

        builder.HasKey(p => p.MaPhieuNhap);
        builder.Property(p => p.MaPhieuNhap)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(p => p.NgayNhap)
            .IsRequired();

        builder.Property(p => p.NhaCungCap)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.MaNhanVien)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(p => p.TongSoSanPham)
            .HasDefaultValue(0);

        builder.Property(p => p.TongGiaTri)
            .HasPrecision(18, 2)
            .HasDefaultValue(0m);

        // Quan hệ Composition: Xóa phiếu nhập kho sẽ xóa kèm các chi tiết dòng nhập
        builder.HasMany(p => p.ChiTietNhapKhos)
            .WithOne(c => c.PhieuNhapKho)
            .HasForeignKey(c => c.MaPhieuNhap)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
