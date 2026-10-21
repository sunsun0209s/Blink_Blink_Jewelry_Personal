### Đây là nơi lưu những thứ đã hoàn thành

- [x] [2026-10-18] [Infrastructure EF Core 8 & Repositories Phân hệ Identity]: Cài đặt Microsoft.EntityFrameworkCore.SqlServer 8.0.13, tạo AppDbContext với cơ chế ApplyConfigurationsFromAssembly, cấu hình Fluent API (TPT) cho 4 bảng (VaiTro, NguoiDung, NhanVien, KhachHang) và triển khai 4 Repository hoàn chỉnh (NguoiDungRepository, NhanVienRepository, KhachHangRepository, VaiTroRepository).
- [x] [2026-10-18] [Interfaces, DTOs & Use Cases Phân hệ Identity]: Triển khai 4 Repository Interface (INguoiDungRepository, INhanVienRepository, IKhachHangRepository, IVaiTroRepository), 3 DTO (DangNhapDTO, DangKyKhachHangDTO, TaoNhanVienDTO) và 3 Use Case (DangNhapUseCase, DangKyKhachHangUseCase, QuanLyNhanVienUseCase) theo đúng 100% Class Diagram.
- [x] [2026-10-18] [Thực thể Domain Phân hệ Người dùng & Phân quyền]: Triển khai 4 thực thể NguoiDung (abstract), NhanVien, KhachHang, VaiTro theo đúng 100% bản vẽ Diagram trong nhánh feature/identity-entities.
- [x] [2026-10-16] [Chuyển Framework về .NET 8 LTS]: Chuyển TargetFramework của toàn bộ Solution về net8.0 LTS (Domain, Application, Infrastructure, Web, Admin, Tests).
- [x] [2026-10-14] [Thực thể DanhMuc & SanPhamTrangSuc]: Triển khai 2 thực thể thuộc phân hệ Catalog theo đúng bản vẽ Class Diagram.
