using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlinkBlink.Application.Services.Catalog;
using BlinkBlink.Application.Services.Identity;
using BlinkBlink.Application.Services.Inventory;
using BlinkBlink.Application.Services.Orders;
using BlinkBlink.Application.Services.Reporting;
using BlinkBlink.Application.Services.Warranty;
using BlinkBlink.Domain.Entities.Cart;
using BlinkBlink.Domain.Entities.Catalog;
using BlinkBlink.Domain.Entities.Customers;
using BlinkBlink.Domain.Entities.Identity;
using BlinkBlink.Domain.Entities.Inventory;
using BlinkBlink.Domain.Entities.Orders;
using BlinkBlink.Domain.Entities.Warranty;
using BlinkBlink.Domain.Interfaces;
using BlinkBlink.Infrastructure.Data;
using BlinkBlink.Infrastructure.Repositories;

namespace BlinkBlink.Admin.Services;

/// <summary>
/// Quản lý vòng đời dịch vụ và cung cấp điểm truy cập Use Cases trung tâm cho WinForms Admin.
/// Tự động kết nối cơ sở dữ liệu và nạp dữ liệu mẫu trang sức cao cấp ban đầu nếu cần thiết.
/// </summary>
public class AdminServiceScope
{
    private static AdminServiceScope? _instance;
    private static readonly object _lock = new();

    public static AdminServiceScope Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance ??= new AdminServiceScope();
                }
            }
            return _instance;
        }
    }

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

    public QuanLySanPhamUseCase QuanLySanPham { get; }
    public XacNhanNhapKhoUseCase XacNhanNhapKho { get; }
    public XuLyDonHangUseCase XuLyDonHang { get; }
    public DatHangUseCase DatHang { get; }
    public TiepNhanBaoHanhUseCase TiepNhanBaoHanh { get; }
    public XuLyBaoHanhUseCase XuLyBaoHanh { get; }
    public QuanLyNhanVienUseCase QuanLyNhanVien { get; }
    public QuanLyKhachHangUseCase QuanLyKhachHang { get; }
    public ThongKeDoanhThuUseCase ThongKeDoanhThu { get; }
    public ThongKeKhoHangUseCase ThongKeKhoHang { get; }

    public bool IsDatabaseConnected { get; private set; }

    private AdminServiceScope()
    {
        // Khởi tạo DbContext kết nối SQL Server LocalDb / Express
        var connStr = "Server=(localdb)\\mssqllocaldb;Database=BlinkBlinkJewelry;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;Connect Timeout=4;";
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(connStr);

        AppDbContext? db = null;
        try
        {
            db = new AppDbContext(optionsBuilder.Options);
            if (db.Database.CanConnect())
            {
                db.Database.EnsureCreated();
                IsDatabaseConnected = true;
            }
            else
            {
                IsDatabaseConnected = false;
            }
        }
        catch
        {
            IsDatabaseConnected = false;
        }

        if (IsDatabaseConnected && db != null)
        {
            SanPhamRepo = new SanPhamRepository(db);
            DanhMucRepo = new DanhMucRepository(db);
            HoaDonRepo = new HoaDonRepository(db);
            GioHangRepo = new GioHangRepository(db);
            BaoHanhRepo = new BaoHanhRepository(db);
            NhanVienRepo = new NhanVienRepository(db);
            KhachHangRepo = new KhachHangRepository(db);
            VaiTroRepo = new VaiTroRepository(db);
            ThongKeRepo = new ThongKeRepository(db);
            PhieuNhapKhoRepo = new PhieuNhapKhoRepositoryImpl(db);
            PhieuKiemKeRepo = new PhieuKiemKeRepository(db);
        }
        else
        {
            // Dự phòng kho dữ liệu nội bộ trong bộ nhớ khi chưa cấu hình máy chủ SQL Server
            var mock = new InMemoryDataContext();
            mock.SeedInitialLuxuryData();

            SanPhamRepo = mock.SanPhamRepo;
            DanhMucRepo = mock.DanhMucRepo;
            HoaDonRepo = mock.HoaDonRepo;
            GioHangRepo = mock.GioHangRepo;
            BaoHanhRepo = mock.BaoHanhRepo;
            NhanVienRepo = mock.NhanVienRepo;
            KhachHangRepo = mock.KhachHangRepo;
            VaiTroRepo = mock.VaiTroRepo;
            ThongKeRepo = mock.ThongKeRepo;
            PhieuNhapKhoRepo = mock.PhieuNhapKhoRepo;
            PhieuKiemKeRepo = mock.PhieuKiemKeRepo;
        }

        // Khởi tạo tất cả Use Cases theo Clean Architecture
        QuanLySanPham = new QuanLySanPhamUseCase(SanPhamRepo, DanhMucRepo);
        XacNhanNhapKho = new XacNhanNhapKhoUseCase(PhieuNhapKhoRepo, SanPhamRepo);
        XuLyDonHang = new XuLyDonHangUseCase(HoaDonRepo, SanPhamRepo);
        DatHang = new DatHangUseCase(HoaDonRepo, GioHangRepo, SanPhamRepo);
        TiepNhanBaoHanh = new TiepNhanBaoHanhUseCase(BaoHanhRepo, SanPhamRepo);
        XuLyBaoHanh = new XuLyBaoHanhUseCase(BaoHanhRepo, SanPhamRepo);
        QuanLyNhanVien = new QuanLyNhanVienUseCase(NhanVienRepo, VaiTroRepo);
        QuanLyKhachHang = new QuanLyKhachHangUseCase(KhachHangRepo);
        ThongKeDoanhThu = new ThongKeDoanhThuUseCase(ThongKeRepo);
        ThongKeKhoHang = new ThongKeKhoHangUseCase(ThongKeRepo);
    }
}
