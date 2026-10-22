using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlinkBlink.Domain.Entities.Cart;
using BlinkBlink.Domain.Entities.Catalog;
using BlinkBlink.Domain.Entities.Customers;
using BlinkBlink.Domain.Entities.Identity;
using BlinkBlink.Domain.Entities.Inventory;
using BlinkBlink.Domain.Entities.Orders;
using BlinkBlink.Domain.Entities.Warranty;
using BlinkBlink.Domain.Interfaces;

namespace BlinkBlink.Admin.Services;

/// <summary>
/// Kho dữ liệu mẫu lưu trữ trong bộ nhớ phục vụ hiển thị trực quan và kiểm thử trên WinForms Admin.
/// Tự động kích hoạt khi môi trường máy trạm chưa khởi chạy máy chủ SQL Server.
/// </summary>
public class InMemoryDataContext
{
    public List<SanPhamTrangSuc> SanPhams { get; } = new();
    public List<DanhMuc> DanhMucs { get; } = new();
    public List<HoaDon> HoaDons { get; } = new();
    public List<PhieuBaoHanh> PhieuBaoHanhs { get; } = new();
    public List<NhanVien> NhanViens { get; } = new();
    public List<KhachHang> KhachHangs { get; } = new();
    public List<VaiTro> VaiTros { get; } = new();
    public List<PhieuNhapKho> PhieuNhapKhos { get; } = new();
    public List<PhieuKiemKe> PhieuKiemKes { get; } = new();

    public ISanPhamRepository SanPhamRepo { get; }
    public IDanhMucRepository DanhMucRepo { get; }
    public IHoaDonRepository HoaDonRepo { get; }
    public IGioHangRepository GioHangRepo { get; }
    public IBaoHanhRepository BaoHanhRepo { get; }
    public INhanVienRepository NhanVienRepo { get; }
    public IKhachHangRepository KhachHangRepo { get; }
    public IVaiTroRepository VaiTroRepo { get; }
    public IThongKeRepository ThongKeRepo { get; }
    public IPhieuNhapKhoRepository PhieuNhapKhoRepo { get; }
    public IPhieuKiemKeRepository PhieuKiemKeRepo { get; }

    public InMemoryDataContext()
    {
        SanPhamRepo = new MemorySanPhamRepository(this);
        DanhMucRepo = new MemoryDanhMucRepository(this);
        HoaDonRepo = new MemoryHoaDonRepository(this);
        GioHangRepo = new MemoryGioHangRepository();
        BaoHanhRepo = new MemoryBaoHanhRepository(this);
        NhanVienRepo = new MemoryNhanVienRepository(this);
        KhachHangRepo = new MemoryKhachHangRepository(this);
        VaiTroRepo = new MemoryVaiTroRepository(this);
        ThongKeRepo = new MemoryThongKeRepository(this);
        PhieuNhapKhoRepo = new MemoryPhieuNhapKhoRepository(this);
        PhieuKiemKeRepo = new MemoryPhieuKiemKeRepository(this);
    }

    public void SeedInitialLuxuryData()
    {
        // Da loai bo du lieu mau trong C# theo yeu cau.
    }

    private class MemorySanPhamRepository : ISanPhamRepository
    {
        private readonly InMemoryDataContext _ctx;
        public MemorySanPhamRepository(InMemoryDataContext ctx) => _ctx = ctx;

        public void LuuSanPham(SanPhamTrangSuc sp)
        {
            var existing = _ctx.SanPhams.FirstOrDefault(s => s.MaSanPham == sp.MaSanPham);
            if (existing != null) _ctx.SanPhams.Remove(existing);
            _ctx.SanPhams.Add(sp);
        }
        public SanPhamTrangSuc? TimTheoId(string id) => _ctx.SanPhams.FirstOrDefault(s => s.MaSanPham == id);
        public Task LuuSanPhamAsync(SanPhamTrangSuc sp, CancellationToken ct = default) { LuuSanPham(sp); return Task.CompletedTask; }
        public Task<SanPhamTrangSuc?> TimTheoIdAsync(string id, CancellationToken ct = default) => Task.FromResult(TimTheoId(id));
    }

    private class MemoryDanhMucRepository : IDanhMucRepository
    {
        private readonly InMemoryDataContext _ctx;
        public MemoryDanhMucRepository(InMemoryDataContext ctx) => _ctx = ctx;
        public void LuuDanhMuc(DanhMuc dm) { _ctx.DanhMucs.Add(dm); }
        public Task LuuDanhMucAsync(DanhMuc dm, CancellationToken ct = default) { LuuDanhMuc(dm); return Task.CompletedTask; }
        public DanhMuc? TimTheoId(string id) => _ctx.DanhMucs.FirstOrDefault(d => d.MaDanhMuc == id);
        public Task<DanhMuc?> TimTheoIdAsync(string id, CancellationToken ct = default) => Task.FromResult(TimTheoId(id));
    }

    private class MemoryHoaDonRepository : IHoaDonRepository
    {
        private readonly InMemoryDataContext _ctx;
        public MemoryHoaDonRepository(InMemoryDataContext ctx) => _ctx = ctx;

        public void LuuHoaDon(HoaDon hd) => _ctx.HoaDons.Add(hd);
        public Task LuuHoaDonAsync(HoaDon hd, CancellationToken ct = default) { LuuHoaDon(hd); return Task.CompletedTask; }
        public HoaDon? TimTheoMa(string ma) => _ctx.HoaDons.FirstOrDefault(h => h.MaHoaDon == ma);
        public Task<HoaDon?> TimTheoMaAsync(string ma, CancellationToken ct = default) => Task.FromResult(TimTheoMa(ma));
        public List<HoaDon> LayTheoKhachHang(string maKH) => _ctx.HoaDons.Where(h => h.MaKhachHang == maKH).ToList();
        public Task<List<HoaDon>> LayTheoKhachHangAsync(string maKH, CancellationToken ct = default) => Task.FromResult(LayTheoKhachHang(maKH));
        public List<HoaDon> LayDanhSachDonHang() => _ctx.HoaDons.OrderByDescending(h => h.NgayLap).ToList();
        public Task<List<HoaDon>> LayDanhSachDonHangAsync(CancellationToken ct = default) => Task.FromResult(LayDanhSachDonHang());
        public void CapNhatHoaDon(HoaDon hd)
        {
            var idx = _ctx.HoaDons.FindIndex(h => h.MaHoaDon == hd.MaHoaDon);
            if (idx >= 0) _ctx.HoaDons[idx] = hd;
        }
        public Task CapNhatHoaDonAsync(HoaDon hd, CancellationToken ct = default) { CapNhatHoaDon(hd); return Task.CompletedTask; }
    }

    private class MemoryGioHangRepository : IGioHangRepository
    {
        private readonly Dictionary<string, GioHang> _carts = new();
        public GioHang? LayGioHang(string maKH) => _carts.TryGetValue(maKH, out var gh) ? gh : null;
        public Task<GioHang?> LayGioHangAsync(string maKH, CancellationToken ct = default) => Task.FromResult(LayGioHang(maKH));
        public void CapNhatGioHang(GioHang gh) => _carts[gh.MaKhachHang] = gh;
        public Task CapNhatGioHangAsync(GioHang gh, CancellationToken ct = default) { CapNhatGioHang(gh); return Task.CompletedTask; }
        public Task LuuGioHangAsync(GioHang gh, CancellationToken ct = default) { CapNhatGioHang(gh); return Task.CompletedTask; }
    }

    private class MemoryBaoHanhRepository : IBaoHanhRepository
    {
        private readonly InMemoryDataContext _ctx;
        public MemoryBaoHanhRepository(InMemoryDataContext ctx) => _ctx = ctx;

        public void LuuPhieu(PhieuBaoHanh pbh) => _ctx.PhieuBaoHanhs.Add(pbh);
        public Task LuuPhieuAsync(PhieuBaoHanh pbh, CancellationToken ct = default) { LuuPhieu(pbh); return Task.CompletedTask; }
        public PhieuBaoHanh? TimTheoSoPhieu(string soPhieu) => _ctx.PhieuBaoHanhs.FirstOrDefault(p => p.SoPhieuBH == soPhieu);
        public Task<PhieuBaoHanh?> TimTheoSoPhieuAsync(string soPhieu, CancellationToken ct = default) => Task.FromResult(TimTheoSoPhieu(soPhieu));
        public List<PhieuBaoHanh> LayTheoKhachHang(string maKH) => _ctx.PhieuBaoHanhs.Where(p => p.MaKhachHang == maKH).ToList();
        public Task<List<PhieuBaoHanh>> LayTheoKhachHangAsync(string maKH, CancellationToken ct = default) => Task.FromResult(LayTheoKhachHang(maKH));
        public List<PhieuBaoHanh> LayDanhSachPhieu() => _ctx.PhieuBaoHanhs.OrderByDescending(p => p.NgayTiepNhan).ToList();
        public Task<List<PhieuBaoHanh>> LayDanhSachPhieuAsync(CancellationToken ct = default) => Task.FromResult(LayDanhSachPhieu());
        public void CapNhatPhieu(PhieuBaoHanh pbh)
        {
            var idx = _ctx.PhieuBaoHanhs.FindIndex(p => p.SoPhieuBH == pbh.SoPhieuBH);
            if (idx >= 0) _ctx.PhieuBaoHanhs[idx] = pbh;
        }
        public Task CapNhatPhieuAsync(PhieuBaoHanh pbh, CancellationToken ct = default) { CapNhatPhieu(pbh); return Task.CompletedTask; }
    }

    private class MemoryNhanVienRepository : INhanVienRepository
    {
        private readonly InMemoryDataContext _ctx;
        public MemoryNhanVienRepository(InMemoryDataContext ctx) => _ctx = ctx;
        public void LuuNhanVien(NhanVien nv) { _ctx.NhanViens.Add(nv); }
        public NhanVien? TimTheoMa(string maNV) => _ctx.NhanViens.FirstOrDefault(n => n.MaNhanVien == maNV || n.MaNguoiDung == maNV);
    }

    private class MemoryKhachHangRepository : IKhachHangRepository
    {
        private readonly InMemoryDataContext _ctx;
        public MemoryKhachHangRepository(InMemoryDataContext ctx) => _ctx = ctx;
        public void LuuKhachHang(KhachHang kh) { _ctx.KhachHangs.Add(kh); }
        public KhachHang? TimTheoMa(string maKH) => _ctx.KhachHangs.FirstOrDefault(k => k.MaKhachHang == maKH || k.MaNguoiDung == maKH);
    }

    private class MemoryVaiTroRepository : IVaiTroRepository
    {
        private readonly InMemoryDataContext _ctx;
        public MemoryVaiTroRepository(InMemoryDataContext ctx) => _ctx = ctx;
        public VaiTro? TimTheoMa(string maVaiTro) => _ctx.VaiTros.FirstOrDefault(v => v.MaVaiTro == maVaiTro);
        public List<VaiTro> LayTatCa() => _ctx.VaiTros.ToList();
        public void LuuVaiTro(VaiTro vt) => _ctx.VaiTros.Add(vt);
    }

    private class MemoryThongKeRepository : IThongKeRepository
    {
        private readonly InMemoryDataContext _ctx;
        public MemoryThongKeRepository(InMemoryDataContext ctx) => _ctx = ctx;

        public List<HoaDon> TruyVanDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            return _ctx.HoaDons
                .Where(h => h.NgayLap.Date >= tuNgay.Date && h.NgayLap.Date <= denNgay.Date && h.TrangThaiDonHang != "DaHuy")
                .ToList();
        }
        public Task<List<HoaDon>> TruyVanDoanhThuAsync(DateTime tu, DateTime den, CancellationToken ct = default) => Task.FromResult(TruyVanDoanhThu(tu, den));

        public List<SanPhamTrangSuc> TruyVanSanPhamTonKho() => _ctx.SanPhams.ToList();
        public Task<List<SanPhamTrangSuc>> TruyVanSanPhamTonKhoAsync(CancellationToken ct = default) => Task.FromResult(TruyVanSanPhamTonKho());

        public List<ChiTietKiemKe> TruyVanChenhLechKiemKe() => new();
        public Task<List<ChiTietKiemKe>> TruyVanChenhLechKiemKeAsync(CancellationToken ct = default) => Task.FromResult(TruyVanChenhLechKiemKe());

        public List<SanPhamTrangSuc> TruyVanSanPhamDoanhThuThap() => _ctx.SanPhams.Take(2).ToList();
        public Task<List<SanPhamTrangSuc>> TruyVanSanPhamDoanhThuThapAsync(CancellationToken ct = default) => Task.FromResult(TruyVanSanPhamDoanhThuThap());
    }

    private class MemoryPhieuNhapKhoRepository : IPhieuNhapKhoRepository
    {
        private readonly InMemoryDataContext _ctx;
        public MemoryPhieuNhapKhoRepository(InMemoryDataContext ctx) => _ctx = ctx;
        public void LuuPhieuNhap(PhieuNhapKho phieu) => _ctx.PhieuNhapKhos.Add(phieu);
        public Task LuuPhieuNhapAsync(PhieuNhapKho phieu, CancellationToken ct = default) { LuuPhieuNhap(phieu); return Task.CompletedTask; }
        public Task<PhieuNhapKho?> TimTheoIdAsync(string id, CancellationToken ct = default) => Task.FromResult(_ctx.PhieuNhapKhos.FirstOrDefault(p => p.MaPhieuNhap == id));
    }

    private class MemoryPhieuKiemKeRepository : IPhieuKiemKeRepository
    {
        private readonly InMemoryDataContext _ctx;
        public MemoryPhieuKiemKeRepository(InMemoryDataContext ctx) => _ctx = ctx;
        public void LuuPhieuKiemKe(PhieuKiemKe phieu) => _ctx.PhieuKiemKes.Add(phieu);
        public Task LuuPhieuKiemKeAsync(PhieuKiemKe phieu, CancellationToken ct = default) { LuuPhieuKiemKe(phieu); return Task.CompletedTask; }
        public PhieuKiemKe? TimTheoId(string id) => _ctx.PhieuKiemKes.FirstOrDefault(p => p.MaPhieuKiemKe == id);
        public Task<PhieuKiemKe?> TimTheoIdAsync(string id, CancellationToken ct = default) => Task.FromResult(TimTheoId(id));
    }
}
