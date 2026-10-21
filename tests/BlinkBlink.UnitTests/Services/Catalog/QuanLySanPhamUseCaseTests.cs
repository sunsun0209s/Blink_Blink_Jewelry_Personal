using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlinkBlink.Application.DTOs.Catalog;
using BlinkBlink.Application.Services.Catalog;
using BlinkBlink.Domain.Entities.Catalog;
using BlinkBlink.Domain.Interfaces;
using Xunit;

namespace BlinkBlink.UnitTests.Services.Catalog;

public class QuanLySanPhamUseCaseTests
{
    private class FakeSanPhamRepository : ISanPhamRepository
    {
        public readonly Dictionary<string, SanPhamTrangSuc> Data = new();

        public void LuuSanPham(SanPhamTrangSuc sp)
        {
            Data[sp.MaSanPham] = sp;
        }

        public Task LuuSanPhamAsync(SanPhamTrangSuc sp, CancellationToken cancellationToken = default)
        {
            Data[sp.MaSanPham] = sp;
            return Task.CompletedTask;
        }

        public SanPhamTrangSuc? TimTheoId(string id)
        {
            return Data.TryGetValue(id, out var sp) ? sp : null;
        }

        public Task<SanPhamTrangSuc?> TimTheoIdAsync(string id, CancellationToken cancellationToken = default)
        {
            Data.TryGetValue(id, out var sp);
            return Task.FromResult(sp);
        }
    }

    private class FakeDanhMucRepository : IDanhMucRepository
    {
        public readonly Dictionary<string, DanhMuc> Data = new();

        public void LuuDanhMuc(DanhMuc dm)
        {
            Data[dm.MaDanhMuc] = dm;
        }

        public Task LuuDanhMucAsync(DanhMuc dm, CancellationToken cancellationToken = default)
        {
            Data[dm.MaDanhMuc] = dm;
            return Task.CompletedTask;
        }

        public DanhMuc? TimTheoId(string id)
        {
            return Data.TryGetValue(id, out var dm) ? dm : null;
        }

        public Task<DanhMuc?> TimTheoIdAsync(string id, CancellationToken cancellationToken = default)
        {
            Data.TryGetValue(id, out var dm);
            return Task.FromResult(dm);
        }
    }

    [Fact]
    public void CapNhatThongTin_ValidRequest_UpdatesProductInformation()
    {
        // Arrange
        var fakeSpRepo = new FakeSanPhamRepository();
        var fakeDmRepo = new FakeDanhMucRepository();
        var product = new SanPhamTrangSuc(
            maSanPham: "SP_01",
            tenSanPham: "Nhan Kim Cuong 1",
            maDanhMuc: "DM_NHAN",
            chatLieu: "Vang 18K",
            size: "12",
            giaNhap: 5000000,
            giaBan: 8000000
        );
        fakeSpRepo.LuuSanPham(product);

        var useCase = new QuanLySanPhamUseCase(fakeSpRepo, fakeDmRepo);
        var request = new CapNhatSanPhamDTO
        {
            MaSanPham = "SP_01",
            TenSanPham = "Nhan Kim Cuong Solitaire",
            MaDanhMuc = "DM_NHAN_CUOI",
            ChatLieu = "Bach Kim PT950",
            Size = "14"
        };

        // Act
        var result = useCase.CapNhatThongTin(request);

        // Assert
        Assert.True(result);
        var updated = fakeSpRepo.TimTheoId("SP_01");
        Assert.NotNull(updated);
        Assert.Equal("Nhan Kim Cuong Solitaire", updated.TenSanPham);
        Assert.Equal("DM_NHAN_CUOI", updated.MaDanhMuc);
        Assert.Equal("Bach Kim PT950", updated.ChatLieu);
        Assert.Equal("14", updated.Size);
    }

    [Fact]
    public void CapNhatThongTin_ProductNotFound_ReturnsFalse()
    {
        // Arrange
        var fakeSpRepo = new FakeSanPhamRepository();
        var fakeDmRepo = new FakeDanhMucRepository();
        var useCase = new QuanLySanPhamUseCase(fakeSpRepo, fakeDmRepo);
        var request = new CapNhatSanPhamDTO
        {
            MaSanPham = "SP_NONEXIST",
            TenSanPham = "Test",
            MaDanhMuc = "DM_01"
        };

        // Act
        var result = useCase.CapNhatThongTin(request);

        // Assert
        Assert.False(result);
    }
}
