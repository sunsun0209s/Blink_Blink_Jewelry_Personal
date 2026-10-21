using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlinkBlink.Domain.Entities.Catalog;

namespace BlinkBlink.Infrastructure.Data.Configurations;

/// <summary>
/// Cấu hình ánh xạ Fluent API cho thực thể DanhMuc.
/// </summary>
public class DanhMucConfiguration : IEntityTypeConfiguration<DanhMuc>
{
    public void Configure(EntityTypeBuilder<DanhMuc> builder)
    {
        builder.ToTable("DanhMuc");

        builder.HasKey(d => d.MaDanhMuc);
        builder.Property(d => d.MaDanhMuc)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(d => d.TenDanhMuc)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(d => d.MoTa)
            .HasMaxLength(500);

        // Quan hệ 1-N: 1 Danh mục có nhiều sản phẩm trang sức
        builder.HasMany(d => d.SanPhamTrangSucs)
            .WithOne(s => s.DanhMuc)
            .HasForeignKey(s => s.MaDanhMuc)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
