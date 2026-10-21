# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Triển khai Phân hệ Quản lý Bán hàng (Sales Subsystem) khớp 100% Diagram:**
  - **Domain Entities:**
    - `src/BlinkBlink.Domain/Entities/Orders/HoaDon.cs`: Các trường định danh, thông tin thanh toán, phương thức `TinhTongTien()`, `CapNhatTrangThai()`, `HuyHoaDon()`.
    - `src/BlinkBlink.Domain/Entities/Orders/ChiTietHoaDon.cs`: Khóa kết hợp SoHoaDon + MaSanPham, phương thức `TinhThanhTien()`.
    - `src/BlinkBlink.Domain/Entities/Cart/GioHang.cs`: Quản lý danh sách món chọn, `DonDepGioHang()`, `DatHang()`, `ThemSanPham()`, `XoaSanPham()`.
    - `src/BlinkBlink.Domain/Entities/Cart/ChiTietGioHang.cs`: Khóa kết hợp MaGioHang + MaSanPham, `TangSoLuong()`, `GiamSoLuong()`.
  - **Domain Interfaces (Repositories):**
    - `src/BlinkBlink.Domain/Interfaces/IHoaDonRepository.cs`
    - `src/BlinkBlink.Domain/Interfaces/IGioHangRepository.cs`
  - **Application Layer (DTOs & Use Cases):**
    - `src/BlinkBlink.Application/DTOs/Orders/DatHangDTO.cs`
    - `src/BlinkBlink.Application/DTOs/Orders/CapNhatDonHangDTO.cs`
    - `src/BlinkBlink.Application/Services/Orders/DatHangUseCase.cs`: Logic chuyển đổi giỏ hàng sang hóa đơn, kiểm tra tồn kho, làm sạch giỏ hàng.
    - `src/BlinkBlink.Application/Services/Orders/XuLyDonHangUseCase.cs`: Nghiệp vụ duyệt đơn hàng, trừ trạng thái tồn kho, cập nhật tiến độ giao vận và hoàn kho khi hủy đơn.
  - **Infrastructure Layer:**
    - `HoaDonConfiguration.cs`, `ChiTietHoaDonConfiguration.cs`, `GioHangConfiguration.cs`, `ChiTietGioHangConfiguration.cs`.
    - Cập nhật `AppDbContext.cs`: Khai báo 4 `DbSet` mới cho Hóa đơn và Giỏ hàng.
    - `HoaDonRepository.cs`, `GioHangRepository.cs` triển khai đầy đủ các phương thức truy vấn đồng bộ và bất đồng bộ.
  - **Presentation Layer (Web RESTful API):**
    - `src/BlinkBlink.Web/Controllers/BanHangController.cs`: Cung cấp 5 API endpoints chuẩn REST (`dat-hang`, `khach-hang/{maKH}`, `danh-sach`, `trang-thai`, `{maHoaDon}/huy`).
    - Cập nhật `src/BlinkBlink.Web/Program.cs`: Đăng ký DI cho các repository và use case mới.
  - **Unit Testing:**
    - `tests/BlinkBlink.UnitTests/Services/Orders/DatHangUseCaseTests.cs` (3 tests).
    - `tests/BlinkBlink.UnitTests/Services/Orders/XuLyDonHangUseCaseTests.cs` (4 tests).
    - Toàn bộ 16/16 tests pass 100%, Solution build 0 lỗi 0 cảnh báo.

## 2. Những gì đang làm dở & Các quyết định kỹ thuật đã chốt
- Quyết định kiến trúc chuẩn:
  1. Toàn bộ mã nguồn tuân thủ Clean Architecture trên nền tảng duy nhất .NET 8 LTS.
  2. WinForms Admin và Web Khách hàng đều gọi đến cùng các nghiệp vụ Application/Infrastructure hoặc qua RESTful Web API.
  3. Giao diện (UI Forms & Web Views) được để lại làm ở giai đoạn cuối theo phong cách sang trọng (Taste Skill).
- Phân hệ Bán hàng đã hoàn thành và sẵn sàng tích hợp hoặc tạo PR.

## 3. Hành động cụ thể tiếp theo
1. Cam kết mã nguồn và đẩy nhánh `feature/sales-module` lên GitHub repository.
2. Lựa chọn phân hệ tiếp theo cần triển khai: **Phân hệ Quản lý Dịch vụ & Bảo hành (Warranty)** hoặc **Phân hệ Báo cáo & Thống kê (Reporting)**.
