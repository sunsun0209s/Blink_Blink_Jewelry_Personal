using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlinkBlink.Application.DTOs.Warranty;
using BlinkBlink.Application.Services.Warranty;
using BlinkBlink.Domain.Entities.Catalog;
using BlinkBlink.Domain.Entities.Warranty;
using BlinkBlink.Domain.Interfaces;
using Xunit;

namespace BlinkBlink.UnitTests.Services.Warranty;

public class TiepNhanBaoHanhUseCaseTests
{
    private class FakeBaoHanhRepository : IBaoHanhRepository
    {
        public readonly Dictionary<string, PhieuBaoHanh> Data = new();

        public void LuuPhieu(PhieuBaoHanh pbh)
        {
            Data[pbh.SoPhieuBH] = pbh;
        }

        public Task LuuPhieuAsync(PhieuBaoHanh pbh, CancellationToken cancellationToken = default)
        {
            Data[pbh.SoPhieuBH] = pbh;
            return Task.CompletedTask;
        }

        public PhieuBaoHanh? TimTheoSoPhieu(string soPhieu)
        {
            return Data.TryGetValue(soPhieu, out var pbh) ? pbh : null;
        }

        public Task<PhieuBaoHanh?> TimTheoSoPhieuAsync(string soPhieu, CancellationToken cancellationToken = default)
        {
            Data.TryGetValue(soPhieu, out var pbh);
            return Task.FromResult(pbh);
        }

        public List<PhieuBaoHanh> LayTheoKhachHang(string maKH)
        {
            return Data.Values.Where(p => p.MaKhachHang == maKH).ToList();
        }

        public Task<List<PhieuBaoHanh>> LayTheoKhachHangAsync(string maKH, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Data.Values.Where(p => p.MaKhachHang == maKH).ToList());
        }

        public List<PhieuBaoHanh> LayDanhSachPhieu()
        {
            return Data.Values.ToList();
        }

        public Task<List<PhieuBaoHanh>> LayDanhSachPhieuAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Data.Values.ToList());
        }

        public void CapNhatPhieu(PhieuBaoHanh pbh)
        {
            Data[pbh.SoPhieuBH] = pbh;
        }

        public Task CapNhatPhieuAsync(PhieuBaoHanh pbh, CancellationToken cancellationToken = default)
        {
            Data[pbh.SoPhieuBH] = pbh;
            return Task.CompletedTask;
        }
    }

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

        public SanPhamTrangSuc? TimTheoId(string maSanPham)
        {
            return Data.TryGetValue(maSanPham, out var sp) ? sp : null;
        }

        public Task<SanPhamTrangSuc?> TimTheoIdAsync(string maSanPham, CancellationToken cancellationToken = default)
        {
            Data.TryGetValue(maSanPham, out var sp);
            return Task.FromResult(sp);
        }

        public List<SanPhamTrangSuc> LayTatCa() => new List<SanPhamTrangSuc>(Data.Values);
        public Task<List<SanPhamTrangSuc>> LayTatCaAsync(CancellationToken cancellationToken = default) => Task.FromResult(LayTatCa());
    }

    [Fact]
    public async Task TiepNhanBaoHanh_ThanhCong_TaoPhieuVaChuyenTrangThaiSanPham()
    {
        // Sắp đặt
        var baoHanhRepo = new FakeBaoHanhRepository();
        var sanPhamRepo = new FakeSanPhamRepository();

        var sp = new SanPhamTrangSuc("SP001", "Nhẫn kim cương Solitaire", "DM01", "Vàng trắng 18K", "Size 10", 15000000m, 25000000m, "DaBan");
        sanPhamRepo.Data[sp.MaSanPham] = sp;

        var useCase = new TiepNhanBaoHanhUseCase(baoHanhRepo, sanPhamRepo);
        var request = new TaoBaoHanhDTO
        {
            MaKhachHang = "KH001",
            MaSanPham = "SP001",
            LyDoBaoHanh = "Gắn lại chấu đính kim cương và đánh bóng",
            ChiPhiBaoHanh = 0m,
            NgayHenTra = DateTime.UtcNow.AddDays(3)
        };

        // Thực thi
        var result = await useCase.ExecuteAsync(request);

        // Kiểm tra
        Assert.True(result);
        Assert.Single(baoHanhRepo.Data);
        var phieu = baoHanhRepo.Data.Values.First();
        Assert.Equal("KH001", phieu.MaKhachHang);
        Assert.Equal("SP001", phieu.MaSanPham);
        Assert.Equal("DangBaoHanh", phieu.TrangThaiXuLy);
        Assert.Equal(0m, phieu.ChiPhiBaoHanh);
        // Sản phẩm phải được chuyển sang DangBaoHanh
        Assert.Equal("DangBaoHanh", sp.TrangThai);
    }

    [Fact]
    public async Task TiepNhanBaoHanh_ThatBai_KhiSanPhamKhongTonTai()
    {
        // Sắp đặt
        var baoHanhRepo = new FakeBaoHanhRepository();
        var sanPhamRepo = new FakeSanPhamRepository();

        var useCase = new TiepNhanBaoHanhUseCase(baoHanhRepo, sanPhamRepo);
        var request = new TaoBaoHanhDTO
        {
            MaKhachHang = "KH001",
            MaSanPham = "SP_KHONG_TON_TAI",
            LyDoBaoHanh = "Chỉnh size"
        };

        // Thực thi
        var result = await useCase.ExecuteAsync(request);

        // Kiểm tra
        Assert.False(result);
        Assert.Empty(baoHanhRepo.Data);
    }

    [Fact]
    public async Task TiepNhanBaoHanh_ThatBai_KhiThieuLyDoBaoHanh()
    {
        // Sắp đặt
        var baoHanhRepo = new FakeBaoHanhRepository();
        var sanPhamRepo = new FakeSanPhamRepository();

        var sp = new SanPhamTrangSuc("SP001", "Dây chuyền vàng Ý", "DM02", "Vàng 24K", "Size 45cm", 8000000m, 12000000m, "DaBan");
        sanPhamRepo.Data[sp.MaSanPham] = sp;

        var useCase = new TiepNhanBaoHanhUseCase(baoHanhRepo, sanPhamRepo);
        var request = new TaoBaoHanhDTO
        {
            MaKhachHang = "KH001",
            MaSanPham = "SP001",
            LyDoBaoHanh = "   " // Rỗng
        };

        // Thực thi
        var result = await useCase.ExecuteAsync(request);

        // Kiểm tra
        Assert.False(result);
        Assert.Empty(baoHanhRepo.Data);
    }
}
