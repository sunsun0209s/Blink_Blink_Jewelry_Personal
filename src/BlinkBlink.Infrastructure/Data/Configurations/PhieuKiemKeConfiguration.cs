using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlinkBlink.Domain.Entities.Inventory;

namespace BlinkBlink.Infrastructure.Data.Configurations;

/// <summary>
/// Cấu hình ánh xạ Fluent API cho thực thể PhieuKiemKe.
/// </summary>
public class PhieuKiemKeConfiguration : IEntityTypeConfiguration<PhieuKiemKe>
{
    public void Configure(EntityTypeBuilder<PhieuKiemKe> builder)
    {
        builder.ToTable("PhieuKiemKe");

        builder.HasKey(p => p.MaPhieuKiemKe);
        builder.Property(p => p.MaPhieuKiemKe)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(p => p.NgayKiemKe)
            .IsRequired();

        builder.Property(p => p.ViTriKiemKe)
            .HasMaxLength(150);

        builder.Property(p => p.MaNhanVienKho)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(p => p.TrangThaiDuyet)
            .HasDefaultValue(false);

        // Quan hệ Composition: Xóa phiếu kiểm kê sẽ xóa kèm các dòng chi tiết kiểm kê
        builder.HasMany(p => p.ChiTietKiemKes)
            .WithOne(c => c.PhieuKiemKe)
            .HasForeignKey(c => c.MaPhieuKiemKe)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
