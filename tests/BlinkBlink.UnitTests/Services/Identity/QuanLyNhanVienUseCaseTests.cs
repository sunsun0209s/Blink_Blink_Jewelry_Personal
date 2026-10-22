using System;
using System.Collections.Generic;
using BlinkBlink.Application.Services.Identity;
using BlinkBlink.Domain.Entities.Identity;
using BlinkBlink.Domain.Interfaces;
using Xunit;

namespace BlinkBlink.UnitTests.Services.Identity;

public class QuanLyNhanVienUseCaseTests
{
    private class FakeNhanVienRepository : INhanVienRepository
    {
        public readonly Dictionary<string, NhanVien> Data = new();

        public void LuuNhanVien(NhanVien nv)
        {
            Data[nv.MaNhanVien] = nv;
        }

        public NhanVien? TimTheoMa(string maNV)
        {
            return Data.TryGetValue(maNV, out var nv) ? nv : null;
        }

        public List<NhanVien> LayTatCa()
        {
            return new List<NhanVien>(Data.Values);
        }
    }

    private class FakeVaiTroRepository : IVaiTroRepository
    {
        public readonly Dictionary<string, VaiTro> Data = new();

        public void LuuVaiTro(VaiTro vt)
        {
            Data[vt.MaVaiTro] = vt;
        }

        public VaiTro? TimTheoMa(string maVT)
        {
            return Data.TryGetValue(maVT, out var vt) ? vt : null;
        }

        public List<VaiTro> LayTatCa()
        {
            return new List<VaiTro>(Data.Values);
        }
    }

    [Fact]
    public void ResetMatKhau_ValidEmployee_ResetsPasswordToDefault()
    {
        // Arrange
        var fakeNvRepo = new FakeNhanVienRepository();
        var fakeVtRepo = new FakeVaiTroRepository();
        var employee = new NhanVien(
            maNguoiDung: "ND_NV01",
            hoTen: "Nhan Vien C",
            soDienThoai: "0933112233",
            tenDangNhap: "nhanvienc",
            matKhau: "OldPasswordSecret",
            maVaiTro: "Staff",
            maNhanVien: "NV_01",
            luongCoBan: 10000000
        );
        fakeNvRepo.LuuNhanVien(employee);

        var useCase = new QuanLyNhanVienUseCase(fakeNvRepo, fakeVtRepo);

        // Act
        var result = useCase.ResetMatKhau("NV_01");

        // Assert
        Assert.True(result);
        var updated = fakeNvRepo.TimTheoMa("NV_01");
        Assert.NotNull(updated);
        Assert.Equal("123456", updated.MatKhau);
    }

    [Fact]
    public void ResetMatKhau_NonExistingEmployee_ReturnsFalse()
    {
        // Arrange
        var fakeNvRepo = new FakeNhanVienRepository();
        var fakeVtRepo = new FakeVaiTroRepository();
        var useCase = new QuanLyNhanVienUseCase(fakeNvRepo, fakeVtRepo);

        // Act
        var result = useCase.ResetMatKhau("NV_NONEXIST");

        // Assert
        Assert.False(result);
    }
}
