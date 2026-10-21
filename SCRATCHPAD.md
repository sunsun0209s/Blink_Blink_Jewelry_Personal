# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Đã hoàn tất trọn vẹn 100% toàn bộ 5 Phân hệ nghiệp vụ Backend theo Clean Architecture:**
  1. **Phân hệ 1: Quản lý Người dùng & Phân quyền (Identity & Access Control)**
  2. **Phân hệ 2: Quản lý Sản phẩm & Kho hàng (Catalog & Inventory)**
  3. **Phân hệ 3: Quản lý Bán hàng & Đơn hàng (Sales Management)**
  4. **Phân hệ 4: Quản lý Dịch vụ & Bảo hành (Warranty Management)**
  5. **Phân hệ 5: Báo cáo & Thống kê (Reporting & Analytics)** (Vừa hoàn thành trên nhánh `feature/reporting-module`):
     - Domain Interface: `src/BlinkBlink.Domain/Interfaces/IThongKeRepository.cs` (Truy vấn doanh thu, tồn kho, kiểm kê, sản phẩm doanh thu thấp).
     - Application Layer:
       - `DoanhThuThongKeDTO.cs`, `BaoCaoTonKhoDTO.cs`, `TopSanPhamBanChayDTO.cs`.
       - `ThongKeDoanhThuUseCase.cs`: Tổng hợp doanh thu, số đơn, lợi nhuận ước tính theo ngày.
       - `ThongKeKhoHangUseCase.cs`: Tính toán báo cáo tồn kho kèm chênh lệch kiểm kê, xếp hạng Top sản phẩm bán chạy và Top sản phẩm doanh thu thấp.
     - Infrastructure Layer:
       - `ThongKeRepository.cs`: Triển khai truy vấn tối ưu dạng chỉ đọc `AsNoTracking` trên `AppDbContext`.
     - Web RESTful API:
       - `ThongKeController.cs`: 4 endpoints chuẩn REST (`doanh-thu`, `ton-kho`, `top-ban-chay`, `doanh-thu-thap`).
       - `Program.cs`: Đăng ký DI cho `IThongKeRepository`, `ThongKeDoanhThuUseCase`, `ThongKeKhoHangUseCase`.
     - Unit Tests:
       - `ThongKeDoanhThuUseCaseTests.cs` (2 tests).
       - `ThongKeKhoHangUseCaseTests.cs` (3 tests).
       - Toàn bộ **27/27 tests pass 100%**, Solution biên dịch 0 lỗi 0 cảnh báo.

## 2. Những gì đang làm dở & Các quyết định kỹ thuật đã chốt
- Quyết định kiến trúc chuẩn:
  1. Toàn bộ nền tảng backend của 5 phân hệ đã hoàn chỉnh 100% theo Diagram và Đặc tả hệ thống trên .NET 8 LTS.
  2. Giao diện (Presentation Layer) là bước tiếp theo:
     - WinForms Admin Desktop: Thiết kế form trang nhã, số liệu căn gióng (tabular figures), sang trọng theo Taste Skill.
     - Web Khách hàng (HTML/CSS/JS): Kết nối gọi toàn bộ nghiệp vụ backend qua RESTful Web API.

## 3. Hành động cụ thể tiếp theo
1. Cam kết mã nguồn và đẩy nhánh `feature/reporting-module` lên GitHub repository.
2. Gộp nhánh `feature/reporting-module` vào `main`.
3. Bắt tay vào giai đoạn thiết kế giao diện (Presentation Layer) theo yêu cầu người dùng: WinForms Admin và Web Khách hàng.
