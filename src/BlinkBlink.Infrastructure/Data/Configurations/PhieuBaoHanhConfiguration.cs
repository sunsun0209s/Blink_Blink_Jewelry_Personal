using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlinkBlink.Domain.Entities.Warranty;

namespace BlinkBlink.Infrastructure.Data.Configurations;

/// <summary>
/// Cấu hình ánh xạ Fluent API cho thực thể PhieuBaoHanh.
/// Khớp 100% với Class Diagram kỹ thuật và cấu trúc bảng CSDL SQL Server.
/// </summary>
public class PhieuBaoHanhConfiguration : IEntityTypeConfiguration<PhieuBaoHanh>
{
    public void Configure(EntityTypeBuilder<PhieuBaoHanh> builder)
    {
        builder.ToTable("PhieuBaoHanh");

        builder.HasKey(p => p.SoPhieuBH);

        builder.Property(p => p.SoPhieuBH)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(p => p.MaKhachHang)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(p => p.MaSanPham)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(p => p.LyDoBaoHanh)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(p => p.ChiPhiBaoHanh)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(p => p.TrangThaiXuLy)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.NgayTiepNhan)
            .IsRequired();

        builder.Property(p => p.NgayHenTra)
            .IsRequired(false);

        // Quan hệ tham chiếu tới KhachHang
        builder.HasOne(p => p.KhachHang)
            .WithMany()
            .HasForeignKey(p => p.MaKhachHang)
            .OnDelete(DeleteBehavior.Restrict);

        // Quan hệ tham chiếu tới SanPhamTrangSuc
        builder.HasOne(p => p.SanPham)
            .WithMany()
            .HasForeignKey(p => p.MaSanPham)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
