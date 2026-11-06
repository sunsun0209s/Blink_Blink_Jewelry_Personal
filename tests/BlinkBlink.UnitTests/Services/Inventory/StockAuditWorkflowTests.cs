using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using BlinkBlink.Application.DTOs.Inventory;
using BlinkBlink.Application.Services.Inventory;
using BlinkBlink.Domain.Entities.Catalog;
using BlinkBlink.Domain.Entities.Inventory;
using BlinkBlink.Domain.Interfaces;

namespace BlinkBlink.UnitTests.Services.Inventory;

/// <summary>
/// Kiểm thử toàn diện luồng nghiệp vụ Kiểm kê kho hàng (QuanLyKiemKeUseCase)
/// và Vô hiệu hóa phiếu nhập kho (XacNhanNhapKhoUseCase).
/// Kiểm chứng các quy tắc chống chồng chéo (Anti-Conflict) và hoàn tác tồn kho theo Delta.
/// </summary>
public class StockAuditWorkflowTests
{
    private class FakeSanPhamRepository : ISanPhamRepository
    {
        public readonly Dictionary<string, SanPhamTrangSuc> Data = new();

        public void LuuSanPham(SanPhamTrangSuc sp) => Data[sp.MaSanPham] = sp;
        public SanPhamTrangSuc? TimTheoId(string maSanPham) => Data.TryGetValue(maSanPham, out var sp) ? sp : null;
        public List<SanPhamTrangSuc> LayTatCa() => Data.Values.ToList();
        public List<SanPhamTrangSuc> LayTheoNhom(string maNhom) => Data.Values.Where(s => s.MaNhom == maNhom).ToList();

        public Task LuuSanPhamAsync(SanPhamTrangSuc sp, CancellationToken ct = default) { LuuSanPham(sp); return Task.CompletedTask; }
        public Task<SanPhamTrangSuc?> TimTheoIdAsync(string maSanPham, CancellationToken ct = default) => Task.FromResult(TimTheoId(maSanPham));
        public Task<List<SanPhamTrangSuc>> LayTatCaAsync(CancellationToken ct = default) => Task.FromResult(LayTatCa());
        public Task<List<SanPhamTrangSuc>> LayTheoNhomAsync(string maNhom, CancellationToken ct = default) => Task.FromResult(LayTheoNhom(maNhom));
    }

    private class FakePhieuKiemKeRepository : IPhieuKiemKeRepository
    {
        public readonly Dictionary<string, PhieuKiemKe> Data = new();

        public void LuuPhieuKiemKe(PhieuKiemKe phieu) => Data[phieu.MaPhieuKiemKe] = phieu;
        public PhieuKiemKe? TimTheoId(string maPhieu) => Data.TryGetValue(maPhieu, out var p) ? p : null;
        public List<PhieuKiemKe> LayTatCa() => Data.Values.OrderByDescending(p => p.NgayKiemKe).ToList();

        public void XoaPhieuKiemKe(string maPhieu)
        {
            if (Data.TryGetValue(maPhieu, out var p) && string.Equals(p.TrangThai, "ChoDuyet", StringComparison.OrdinalIgnoreCase))
            {
                Data.Remove(maPhieu);
            }
        }

        public void CapNhatTrangThai(string maPhieu, string trangThaiMoi, DateTime? ngayDuyet = null)
        {
            if (Data.TryGetValue(maPhieu, out var p))
            {
                p.TrangThai = trangThaiMoi;
                p.TrangThaiDuyet = string.Equals(trangThaiMoi, "DaDuyet", StringComparison.OrdinalIgnoreCase);
                if (ngayDuyet.HasValue) p.NgayDuyet = ngayDuyet.Value;
            }
        }

        public bool KiemTraCoPhieuKiemKeMoiHon(string maPhieu, string maSanPham)
        {
            if (!Data.TryGetValue(maPhieu, out var target)) return false;
            var targetTime = target.NgayDuyet ?? target.NgayKiemKe;

            return Data.Values.Any(p =>
                p.MaPhieuKiemKe != maPhieu &&
                string.Equals(p.TrangThai, "DaDuyet", StringComparison.OrdinalIgnoreCase) &&
                (p.NgayDuyet ?? p.NgayKiemKe) > targetTime &&
                p.ChiTietKiemKes.Any(ct => ct.MaSanPham == maSanPham)
            );
        }

        public Task LuuPhieuKiemKeAsync(PhieuKiemKe phieu, CancellationToken ct = default) { LuuPhieuKiemKe(phieu); return Task.CompletedTask; }
        public Task<PhieuKiemKe?> TimTheoIdAsync(string maPhieu, CancellationToken ct = default) => Task.FromResult(TimTheoId(maPhieu));
    }

    private class FakePhieuNhapKhoRepository : IPhieuNhapKhoRepository
    {
        public readonly Dictionary<string, PhieuNhapKho> Data = new();

        public void LuuPhieuNhap(PhieuNhapKho phieu) => Data[phieu.MaPhieuNhap] = phieu;
        public PhieuNhapKho? TimTheoId(string maPhieu) => Data.TryGetValue(maPhieu, out var p) ? p : null;
        public List<PhieuNhapKho> LayTatCa() => Data.Values.OrderByDescending(p => p.NgayNhap).ToList();
        public void VoHieuHoaPhieuNhap(string maPhieuNhap)
        {
            if (Data.TryGetValue(maPhieuNhap, out var p))
            {
                p.TrangThai = false;
            }
        }

        public Task LuuPhieuNhapAsync(PhieuNhapKho phieu, CancellationToken ct = default) { LuuPhieuNhap(phieu); return Task.CompletedTask; }
        public Task<PhieuNhapKho?> TimTheoIdAsync(string maPhieu, CancellationToken ct = default) => Task.FromResult(TimTheoId(maPhieu));
        public Task<List<PhieuNhapKho>> LayTatCaAsync(CancellationToken ct = default) => Task.FromResult(LayTatCa());
    }

    [Fact]
    public void LapPhieuKiemKe_ThanhCong_TrangThaiChoDuyet_TonKhoChuaThayDoi()
    {
        var spRepo = new FakeSanPhamRepository();
        var kiemKeRepo = new FakePhieuKiemKeRepository();
        var useCase = new QuanLyKiemKeUseCase(kiemKeRepo, spRepo);

        var sp = new SanPhamTrangSuc("SP001", "Nhẫn Kim Cương", "DM01", "Vàng 18K", "Size 14", 10000000, 15000000, "SanSang", "", 10) { SoLuongTon = 10 };
        spRepo.LuuSanPham(sp);

        var dto = new TaoPhieuKiemKeDTO
        {
            MaPhieuKiemKe = "PKK001",
            ViTriKiemKe = "Kho Tổng",
            MaNhanVienKho = "NV_KHO",
            GhiChu = "Kiểm kê định kỳ tháng",
            DanhSachChiTiet = new List<ChiTietKiemKeItemDTO>
            {
                new() { MaSanPham = "SP001", TenSanPham = "Nhẫn Kim Cương", TonSoSach = 10, TonThucTe = 12 }
            }
        };

        var result = useCase.LapPhieuKiemKe(dto, out var error);

        Assert.True(result);
        Assert.Empty(error);

        var phieu = kiemKeRepo.TimTheoId("PKK001");
        Assert.NotNull(phieu);
        Assert.Equal("ChoDuyet", phieu.TrangThai);
        Assert.False(phieu.TrangThaiDuyet);

        // Tồn kho chưa bị thay đổi trước khi duyệt
        var spCheck = spRepo.TimTheoId("SP001");
        Assert.Equal(10, spCheck!.SoLuongTon);
    }

    [Fact]
    public void CapNhatPhieuKiemKe_ThanhCong_KhiOTrangThaiChoDuyet()
    {
        var spRepo = new FakeSanPhamRepository();
        var kiemKeRepo = new FakePhieuKiemKeRepository();
        var useCase = new QuanLyKiemKeUseCase(kiemKeRepo, spRepo);

        var phieu = new PhieuKiemKe("PKK002", "Kho Tổng", "NV_KHO", DateTime.UtcNow, "ChoDuyet", "Ghi chú cũ");
        phieu.ChiTietKiemKes.Add(new ChiTietKiemKe("PKK002", "SP001", 10, 8));
        kiemKeRepo.LuuPhieuKiemKe(phieu);

        var updateDto = new CapNhatPhieuKiemKeDTO
        {
            MaPhieuKiemKe = "PKK002",
            ViTriKiemKe = "Kho Quầy 1",
            GhiChu = "Đã cập nhật sau khi đếm lại",
            DanhSachChiTiet = new List<ChiTietKiemKeItemDTO>
            {
                new() { MaSanPham = "SP001", TonSoSach = 10, TonThucTe = 9, LyDoChenhLech = "Nhầm lẫn đợt 1" }
            }
        };

        var ok = useCase.CapNhatPhieuKiemKe(updateDto, out var err);

        Assert.True(ok);
        Assert.Empty(err);

        var phieuSau = kiemKeRepo.TimTheoId("PKK002");
        Assert.Equal("Đã cập nhật sau khi đếm lại", phieuSau!.GhiChu);
        Assert.Equal(9, phieuSau.ChiTietKiemKes.First().TonThucTe);
    }

    [Fact]
    public void XoaPhieuKiemKe_ThanhCong_KhiChoDuyet_VaChanKhiDaDuyet()
    {
        var spRepo = new FakeSanPhamRepository();
        var kiemKeRepo = new FakePhieuKiemKeRepository();
        var useCase = new QuanLyKiemKeUseCase(kiemKeRepo, spRepo);

        var phieu1 = new PhieuKiemKe("PKK_DEL_1", "Kho", "NV", DateTime.UtcNow, "ChoDuyet");
        var phieu2 = new PhieuKiemKe("PKK_DEL_2", "Kho", "NV", DateTime.UtcNow, "DaDuyet");
        kiemKeRepo.LuuPhieuKiemKe(phieu1);
        kiemKeRepo.LuuPhieuKiemKe(phieu2);

        // Xóa phiếu chưa duyệt -> OK
        var ok1 = useCase.XoaPhieuKiemKe("PKK_DEL_1", out var err1);
        Assert.True(ok1);
        Assert.Null(kiemKeRepo.TimTheoId("PKK_DEL_1"));

        // Xóa phiếu đã duyệt -> Chặn
        var ok2 = useCase.XoaPhieuKiemKe("PKK_DEL_2", out var err2);
        Assert.False(ok2);
        Assert.Contains("trạng thái Chờ duyệt", err2);
        Assert.NotNull(kiemKeRepo.TimTheoId("PKK_DEL_2"));
    }

    [Fact]
    public void DuyetPhieuKiemKe_ThanhCong_CapNhatTonKhoTheoDelta()
    {
        var spRepo = new FakeSanPhamRepository();
        var kiemKeRepo = new FakePhieuKiemKeRepository();
        var useCase = new QuanLyKiemKeUseCase(kiemKeRepo, spRepo);

        var sp = new SanPhamTrangSuc("SP001", "Dây Chuyền Ruby", "DM02", "Vàng 18K", "45cm", 5000000, 8000000, "SanSang", "", 10) { SoLuongTon = 10 };
        spRepo.LuuSanPham(sp);

        var phieu = new PhieuKiemKe("PKK_APPROVE", "Kho", "NV", DateTime.UtcNow, "ChoDuyet");
        phieu.ChiTietKiemKes.Add(new ChiTietKiemKe("PKK_APPROVE", "SP001", 10, 15)); // delta = +5
        kiemKeRepo.LuuPhieuKiemKe(phieu);

        var ok = useCase.DuyetPhieuKiemKe("PKK_APPROVE", "ADMIN", out var error);

        Assert.True(ok);
        Assert.Empty(error);

        var spCheck = spRepo.TimTheoId("SP001");
        Assert.Equal(15, spCheck!.SoLuongTon); // 10 + 5 = 15

        var phieuCheck = kiemKeRepo.TimTheoId("PKK_APPROVE");
        Assert.Equal("DaDuyet", phieuCheck!.TrangThai);
        Assert.True(phieuCheck.TrangThaiDuyet);
    }

    [Fact]
    public void DuyetPhieuKiemKe_TuChoiDuyet_KhiTonKhoThucTeLechSoSachGhiTrongPhieu()
    {
        // Ràng buộc Anti-conflict: Khi duyệt, kiểm tra tồn kho hiện tại còn khớp TonSoSach không.
        // Giả sử sau khi lập phiếu, khách mua 2 cái nên tồn kho hệ thống giảm từ 10 xuống 8.
        var spRepo = new FakeSanPhamRepository();
        var kiemKeRepo = new FakePhieuKiemKeRepository();
        var useCase = new QuanLyKiemKeUseCase(kiemKeRepo, spRepo);

        var sp = new SanPhamTrangSuc("SP001", "Dây Chuyền Ruby", "DM02", "Vàng 18K", "45cm", 5000000, 8000000, "SanSang", "", 8) { SoLuongTon = 8 };
        spRepo.LuuSanPham(sp);

        var phieu = new PhieuKiemKe("PKK_CONFLICT", "Kho", "NV", DateTime.UtcNow, "ChoDuyet");
        phieu.ChiTietKiemKes.Add(new ChiTietKiemKe("PKK_CONFLICT", "SP001", 10, 12)); // Ghi nhận lúc tồn là 10
        kiemKeRepo.LuuPhieuKiemKe(phieu);

        var ok = useCase.DuyetPhieuKiemKe("PKK_CONFLICT", "ADMIN", out var error);

        Assert.False(ok);
        Assert.Contains("không chính xác so với hệ thống hiện tại", error);

        // Tồn kho không bị thay đổi sai lệch
        var spCheck = spRepo.TimTheoId("SP001");
        Assert.Equal(8, spCheck!.SoLuongTon);
    }

    [Fact]
    public void VoHieuHoaPhieuKiemKe_ThanhCong_HoanTacTonKhoTheoDelta_BaoToanGiaoDichBanHang()
    {
        // Kịch bản từ CONGVIECTIEPTHEO.md:
        // Hệ thống ghi 105 cái, thực tế 100 cái (delta = -5).
        // Duyệt phiếu -> tồn kho thành 100.
        // Khách mua 2 cái -> tồn kho thành 98.
        // Vô hiệu hóa phiếu -> Hoàn tác delta: 98 - (-5) = 103 cái (không phải 105).
        var spRepo = new FakeSanPhamRepository();
        var kiemKeRepo = new FakePhieuKiemKeRepository();
        var useCase = new QuanLyKiemKeUseCase(kiemKeRepo, spRepo);

        var sp = new SanPhamTrangSuc("SP100", "Lắc Tay Kim Cương", "DM04", "Bạch Kim", "17cm", 20000000, 30000000, "SanSang", "", 105) { SoLuongTon = 105 };
        spRepo.LuuSanPham(sp);

        var phieu = new PhieuKiemKe("PKK_SPEC", "Kho", "NV", DateTime.UtcNow.AddHours(-2), "ChoDuyet");
        phieu.ChiTietKiemKes.Add(new ChiTietKiemKe("PKK_SPEC", "SP100", 105, 100)); // TonSoSach = 105, TonThucTe = 100
        kiemKeRepo.LuuPhieuKiemKe(phieu);

        // 1. Duyệt phiếu
        var okDuyet = useCase.DuyetPhieuKiemKe("PKK_SPEC", "ADMIN", out _);
        Assert.True(okDuyet);
        Assert.Equal(100, spRepo.TimTheoId("SP100")!.SoLuongTon);

        // 2. Khách mua 2 cái sau khi duyệt
        var spSauBan = spRepo.TimTheoId("SP100")!;
        spSauBan.SoLuongTon -= 2; // Còn 98
        spRepo.LuuSanPham(spSauBan);
        Assert.Equal(98, spRepo.TimTheoId("SP100")!.SoLuongTon);

        // 3. Admin vô hiệu hóa phiếu kiểm kê
        var okVoHieuHoa = useCase.VoHieuHoaPhieuKiemKe("PKK_SPEC", "ADMIN", out var errVoHieuHoa);
        Assert.True(okVoHieuHoa);
        Assert.Empty(errVoHieuHoa);

        // Tồn kho phải là 98 - (100 - 105) = 98 - (-5) = 103!
        Assert.Equal(103, spRepo.TimTheoId("SP100")!.SoLuongTon);
    }

    [Fact]
    public void VoHieuHoaPhieuKiemKe_TuChoi_KhiCoPhieuKiemKeMoiHonCungSanPham()
    {
        // Ràng buộc chuỗi kiểm kê (Audit Chain):
        // Phiếu 1 duyệt hôm qua, Phiếu 2 duyệt hôm nay cho cùng sản phẩm.
        // Cấm vô hiệu hóa Phiếu 1.
        var spRepo = new FakeSanPhamRepository();
        var kiemKeRepo = new FakePhieuKiemKeRepository();
        var useCase = new QuanLyKiemKeUseCase(kiemKeRepo, spRepo);

        var sp = new SanPhamTrangSuc("SP001", "Bông Tai", "DM03", "Bạch Kim", "Tiêu chuẩn", 1000000, 2000000, "SanSang", "", 10) { SoLuongTon = 10 };
        spRepo.LuuSanPham(sp);

        var phieu1 = new PhieuKiemKe("PKK_CHAIN_1", "Kho", "NV", DateTime.UtcNow.AddDays(-2), "DaDuyet", "") { NgayDuyet = DateTime.UtcNow.AddDays(-2) };
        phieu1.ChiTietKiemKes.Add(new ChiTietKiemKe("PKK_CHAIN_1", "SP001", 10, 12));
        kiemKeRepo.LuuPhieuKiemKe(phieu1);

        var phieu2 = new PhieuKiemKe("PKK_CHAIN_2", "Kho", "NV", DateTime.UtcNow.AddDays(-1), "DaDuyet", "") { NgayDuyet = DateTime.UtcNow.AddDays(-1) };
        phieu2.ChiTietKiemKes.Add(new ChiTietKiemKe("PKK_CHAIN_2", "SP001", 12, 11));
        kiemKeRepo.LuuPhieuKiemKe(phieu2);

        // Thử vô hiệu hóa phiếu cũ PKK_CHAIN_1 -> Phải bị từ chối!
        var ok = useCase.VoHieuHoaPhieuKiemKe("PKK_CHAIN_1", "ADMIN", out var error);

        Assert.False(ok);
        Assert.Contains("Chỉ cho phép vô hiệu hóa phiếu có chỉnh sửa mới nhất đối với sản phẩm", error);
    }

    [Fact]
    public void VoHieuHoaPhieuNhap_ThanhCong_CapNhatTrangThaiVaTruTonKho()
    {
        var spRepo = new FakeSanPhamRepository();
        var nhapKhoRepo = new FakePhieuNhapKhoRepository();
        var useCase = new XacNhanNhapKhoUseCase(nhapKhoRepo, spRepo);

        var sp = new SanPhamTrangSuc("SP_INWARD", "Nhẫn Solitaire", "DM01", "Vàng 18K", "Size 14", 10000000, 16000000, "SanSang", "", 20) { SoLuongTon = 20 };
        spRepo.LuuSanPham(sp);

        var phieuNhap = new PhieuNhapKho("PNK_TEST", "NCC PNJ", "NV_ADMIN", DateTime.UtcNow, true)
        {
            TongSoSanPham = 5,
            TongGiaTri = 50000000
        };
        phieuNhap.ChiTietNhapKhos.Add(new ChiTietNhapKho("PNK_TEST", "SP_INWARD", 5, 10000000, "CT01"));
        nhapKhoRepo.LuuPhieuNhap(phieuNhap);

        var ok = useCase.VoHieuHoaPhieuNhap("PNK_TEST", "ADMIN", out var error);

        Assert.True(ok);
        Assert.Empty(error);

        // Phiếu nhập bị đánh dấu TrangThai = false
        var phieuCheck = nhapKhoRepo.TimTheoId("PNK_TEST");
        Assert.False(phieuCheck!.TrangThai);

        // Tồn kho bị trừ 5 món (20 - 5 = 15)
        var spCheck = spRepo.TimTheoId("SP_INWARD");
        Assert.Equal(15, spCheck!.SoLuongTon);
    }
}
