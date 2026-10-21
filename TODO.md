### Mục tiêu dự án (Phụ trách: XuanTam - Toàn hệ thống & Phân hệ Identity)

#### 1. Hành động đang chờ:
- [ ] Triển khai Phân hệ Báo cáo & Thống kê (Reporting) chuẩn Clean Architecture 100% Diagram:
  - Application Layer DTOs: `DoanhThuThongKeDTO`, `BaoCaoTonKhoDTO`, `TopSanPhamBanChayDTO`.
  - Application Layer Use Cases: `ThongKeDoanhThuUseCase`, `ThongKeKhoHangUseCase`.
  - Presentation Layer: RESTful Web API `ThongKeController`, đăng ký DI trong `Program.cs`.
  - Quality Assurance: Bộ Unit Tests kiểm thử thống kê doanh thu và báo cáo kho hàng.
- [ ] (Tạm hoãn làm sau) Hoàn thiện tài liệu `PRD.md` và vẽ sơ đồ Activity Diagram chi tiết bằng Mermaid.

#### 2. Kế hoạch tiếp theo:
- [ ] Thiết kế chi tiết các màn hình WinForms Admin & Web khách hàng gọi RESTful API.
