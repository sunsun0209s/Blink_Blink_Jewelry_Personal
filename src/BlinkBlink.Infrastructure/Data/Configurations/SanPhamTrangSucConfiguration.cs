using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlinkBlink.Domain.Entities.Catalog;

namespace BlinkBlink.Infrastructure.Data.Configurations;

/// <summary>
/// Cấu hình ánh xạ Fluent API cho thực thể SanPhamTrangSuc vào cơ sở dữ liệu SQL Server.
/// </summary>
public class SanPhamTrangSucConfiguration : IEntityTypeConfiguration<SanPhamTrangSuc>
{
    public void Configure(EntityTypeBuilder<SanPhamTrangSuc> builder)
    {
        builder.ToTable("SanPhamTrangSuc");

        builder.HasKey(s => s.MaSanPham);
        builder.Property(s => s.MaSanPham)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(s => s.TenSanPham)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(s => s.MaDanhMuc)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(s => s.ChatLieu)
            .HasMaxLength(100);

        builder.Property(s => s.Size)
            .HasMaxLength(20);

        builder.Property(s => s.TrangThai)
            .HasMaxLength(50)
            .HasDefaultValue("SanSang");

        builder.Property(s => s.GiaNhap)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(s => s.GiaBan)
            .HasPrecision(18, 2)
            .IsRequired();

        // Cấu hình quan hệ với DanhMuc
        builder.HasOne(s => s.DanhMuc)
            .WithMany(d => d.SanPhamTrangSucs)
            .HasForeignKey(s => s.MaDanhMuc)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
