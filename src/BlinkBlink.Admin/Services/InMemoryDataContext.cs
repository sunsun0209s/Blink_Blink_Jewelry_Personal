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
    public INguoiDungRepository NguoiDungRepo { get; }

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
        NguoiDungRepo = new MemoryNguoiDungRepository(this);
    }

    public void SeedInitialLuxuryData()
    {
        // 1. Vai trò hệ thống
        VaiTros.Clear();
        VaiTros.Add(new VaiTro("ADMIN", "Quản trị viên", "Quản trị toàn quyền hệ thống cửa hàng kim hoàn"));
        VaiTros.Add(new VaiTro("SALE", "Nhân viên bán hàng", "Thu ngân và tư vấn bán hàng tại quầy POS"));
        VaiTros.Add(new VaiTro("WAREHOUSE", "Thủ kho", "Quản lý nhập kho và lập biên bản kiểm kê trang sức"));
        VaiTros.Add(new VaiTro("KHACHHANG", "Khách hàng", "Khách hàng mua sắm trực tuyến và tra cứu bảo hành"));

        // 2. Tài khoản nhân sự mặc định (Đảm bảo luôn đăng nhập được với admin / 123456789)
        NhanViens.Clear();
        NhanViens.Add(new NhanVien("USER_ADMIN01", "Trần Minh Hoàng", "0901234567", "admin", "123456789", "ADMIN", "NV_ADMIN", 25000000, true));
        NhanViens.Add(new NhanVien("USER_NV001", "Lê Phương Thảo", "0912345678", "thao.le", "123456789", "SALE", "NV001", 12000000, true));
        NhanViens.Add(new NhanVien("USER_NV002", "Nguyễn Quốc Huy", "0933445566", "huy.nguyen", "123456789", "WAREHOUSE", "NV002", 14000000, true));

        // 3. Danh mục sản phẩm kim hoàn
        DanhMucs.Clear();
        DanhMucs.Add(new DanhMuc("DM01", "Nhẫn Kim Cương", "Bộ sưu tập nhẫn đính hôn và nhẫn cưới"));
        DanhMucs.Add(new DanhMuc("DM02", "Dây Chuyền & Mặt Dây", "Thiết kế vàng 18K và đá quý thiên nhiên"));
        DanhMucs.Add(new DanhMuc("DM03", "Bông Tai Quý Phái", "Bông tai bạch kim và sapphire"));
        DanhMucs.Add(new DanhMuc("DM04", "Lắc Tay & Vòng Tay", "Vòng tay tennis kim cương cao cấp"));

        // 4. Sản phẩm trang sức mẫu
        SanPhams.Clear();
        SanPhams.Add(new SanPhamTrangSuc("SP001", "Nhẫn Solitaire Diamond 18K", "DM01", "Vàng trắng 18K đính kim cương chủ 5.4 ly", "Size 14", 18000000, 28500000, "SanSang", "https://images.unsplash.com/photo-1605100804763-247f67b3557e?auto=format&fit=crop&w=800&q=80", 10) { SoLuongTon = 10 });
        SanPhams.Add(new SanPhamTrangSuc("SP002", "Dây Chuyền Eternal Heart Ruby", "DM02", "Vàng hồng 18K đính đá Ruby huyết bồ câu", "Size 45cm", 12000000, 19200000, "SanSang", "https://images.unsplash.com/photo-1599643478518-a784e5dc4c8f?auto=format&fit=crop&w=800&q=80", 0) { SoLuongTon = 8 });
        SanPhams.Add(new SanPhamTrangSuc("SP003", "Bông Tai Royal Blue Sapphire", "DM03", "Bạch kim Platin 950 đính Sapphire Hoàng gia", "Tiêu chuẩn", 22000000, 34000000, "SanSang", "https://images.unsplash.com/photo-1630019852942-f89202989a59?auto=format&fit=crop&w=800&q=80", 15) { SoLuongTon = 5 });
        SanPhams.Add(new SanPhamTrangSuc("SP004", "Lắc Tay Tennis Diamond Bracelet", "DM04", "Vàng trắng 18K đính 45 viên kim cương tự nhiên", "Size 17cm", 48000000, 72000000, "SanSang", "https://images.unsplash.com/photo-1611591475152-47354900a359?auto=format&fit=crop&w=800&q=80", 20) { SoLuongTon = 4 });
        SanPhams.Add(new SanPhamTrangSuc("SP005", "Nhẫn Cưới Harmony Eternity", "DM01", "Vàng Ý 750 tinh tế đính kim cương tấm viền", "Size 16", 11000000, 16800000, "SanSang", "https://images.unsplash.com/photo-1603561591411-07134e71a2a9?auto=format&fit=crop&w=800&q=80", 5) { SoLuongTon = 12 });
        SanPhams.Add(new SanPhamTrangSuc("SP006", "Mặt Dây Chuyền Ngọc Trai South Sea", "DM02", "Vàng vàng 18K đính ngọc trai biển và kim cương", "Size 42cm", 28000000, 42500000, "SanSang", "https://images.unsplash.com/photo-1515562141207-7a88fb7ce338?auto=format&fit=crop&w=800&q=80", 0) { SoLuongTon = 6 });
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
        public List<SanPhamTrangSuc> LayTatCa() => _ctx.SanPhams.ToList();
        public Task LuuSanPhamAsync(SanPhamTrangSuc sp, CancellationToken ct = default) { LuuSanPham(sp); return Task.CompletedTask; }
        public Task<SanPhamTrangSuc?> TimTheoIdAsync(string id, CancellationToken ct = default) => Task.FromResult(TimTheoId(id));
        public Task<List<SanPhamTrangSuc>> LayTatCaAsync(CancellationToken ct = default) => Task.FromResult(LayTatCa());
    }

    private class MemoryDanhMucRepository : IDanhMucRepository
    {
        private readonly InMemoryDataContext _ctx;
        public MemoryDanhMucRepository(InMemoryDataContext ctx) => _ctx = ctx;
        public void LuuDanhMuc(DanhMuc dm) { _ctx.DanhMucs.Add(dm); }
        public Task LuuDanhMucAsync(DanhMuc dm, CancellationToken ct = default) { LuuDanhMuc(dm); return Task.CompletedTask; }
        public DanhMuc? TimTheoId(string id) => _ctx.DanhMucs.FirstOrDefault(d => d.MaDanhMuc == id);
        public Task<DanhMuc?> TimTheoIdAsync(string id, CancellationToken ct = default) => Task.FromResult(TimTheoId(id));
        public List<DanhMuc> LayTatCa() => _ctx.DanhMucs.ToList();
        public Task<List<DanhMuc>> LayTatCaAsync(CancellationToken ct = default) => Task.FromResult(LayTatCa());
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

        public void CapNhatKhachHangTheoSoDienThoai(string soDienThoai, string maKhachHangMoi)
        {
            if (string.IsNullOrWhiteSpace(soDienThoai) || string.IsNullOrWhiteSpace(maKhachHangMoi)) return;
            foreach (var hd in _ctx.HoaDons.Where(h => h.SoDienThoaiNhan == soDienThoai && (h.MaKhachHang == "0" || h.MaKhachHang == "KH_0")))
            {
                hd.MaKhachHang = maKhachHangMoi;
            }
        }
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
        public void LuuNhanVien(NhanVien nv)
        {
            var existing = _ctx.NhanViens.FirstOrDefault(n => n.MaNhanVien == nv.MaNhanVien || n.MaNguoiDung == nv.MaNguoiDung);
            if (existing != null) _ctx.NhanViens.Remove(existing);
            _ctx.NhanViens.Add(nv);
        }
        public void CapNhatNhanVien(NhanVien nv) => LuuNhanVien(nv);
        public NhanVien? TimTheoMa(string maNV) => _ctx.NhanViens.FirstOrDefault(n => n.MaNhanVien == maNV || n.MaNguoiDung == maNV);
        public List<NhanVien> LayTatCa() => _ctx.NhanViens.ToList();
        public bool KiemTraCoTheXoa(string maNV)
        {
            var hasNhapKho = _ctx.PhieuNhapKhos.Any(p => p.MaNhanVien == maNV);
            var hasHoaDon = _ctx.HoaDons.Any(h => h.MaNhanVien == maNV);
            var hasKiemKe = _ctx.PhieuKiemKes.Any(k => k.MaNhanVienKho == maNV);
            return !hasNhapKho && !hasHoaDon && !hasKiemKe;
        }
        public bool XoaNhanVien(string maNV)
        {
            if (!KiemTraCoTheXoa(maNV)) return false;
            var item = TimTheoMa(maNV);
            if (item != null) return _ctx.NhanViens.Remove(item);
            return false;
        }
    }

    private class MemoryKhachHangRepository : IKhachHangRepository
    {
        private readonly InMemoryDataContext _ctx;
        public MemoryKhachHangRepository(InMemoryDataContext ctx) => _ctx = ctx;
        public void LuuKhachHang(KhachHang kh)
        {
            var existing = _ctx.KhachHangs.FirstOrDefault(k => k.MaKhachHang == kh.MaKhachHang || k.MaNguoiDung == kh.MaNguoiDung);
            if (existing != null) _ctx.KhachHangs.Remove(existing);
            _ctx.KhachHangs.Add(kh);
        }
        public KhachHang? TimTheoMa(string maKH) => _ctx.KhachHangs.FirstOrDefault(k => k.MaKhachHang == maKH || k.MaNguoiDung == maKH);
        public KhachHang? TimTheoSoDienThoai(string soDienThoai) => _ctx.KhachHangs.FirstOrDefault(k => k.SoDienThoai == soDienThoai);
        public List<KhachHang> LayTatCa() => _ctx.KhachHangs.ToList();
        public bool KiemTraCoTheXoa(string maKH)
        {
            var hasDon = _ctx.HoaDons.Any(h => h.MaKhachHang == maKH);
            var hasBaoHanh = _ctx.PhieuBaoHanhs.Any(b => b.MaKhachHang == maKH);
            return !hasDon && !hasBaoHanh;
        }
        public bool XoaKhachHang(string maKH)
        {
            if (!KiemTraCoTheXoa(maKH)) return false;
            var item = TimTheoMa(maKH);
            if (item != null) return _ctx.KhachHangs.Remove(item);
            return false;
        }
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
        public List<PhieuNhapKho> LayTatCa() => _ctx.PhieuNhapKhos.ToList();
        public Task<List<PhieuNhapKho>> LayTatCaAsync(CancellationToken ct = default) => Task.FromResult(LayTatCa());
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

    private class MemoryNguoiDungRepository : INguoiDungRepository
    {
        private readonly InMemoryDataContext _ctx;
        public MemoryNguoiDungRepository(InMemoryDataContext ctx) => _ctx = ctx;
        public NguoiDung? TimTheoTenDangNhap(string tenDN)
        {
            var nv = _ctx.NhanViens.FirstOrDefault(n => string.Equals(n.TenDangNhap, tenDN, StringComparison.OrdinalIgnoreCase));
            if (nv != null) return nv;
            return _ctx.KhachHangs.FirstOrDefault(k => string.Equals(k.TenDangNhap, tenDN, StringComparison.OrdinalIgnoreCase));
        }
        public void CapNhat(NguoiDung nd)
        {
            if (nd is NhanVien nv)
            {
                var idx = _ctx.NhanViens.FindIndex(n => n.MaNguoiDung == nd.MaNguoiDung);
                if (idx >= 0) _ctx.NhanViens[idx] = nv;
            }
            else if (nd is KhachHang kh)
            {
                var idx = _ctx.KhachHangs.FindIndex(k => k.MaNguoiDung == nd.MaNguoiDung);
                if (idx >= 0) _ctx.KhachHangs[idx] = kh;
            }
        }
    }
}
