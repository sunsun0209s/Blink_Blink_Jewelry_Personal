# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Chặng 1 (Domain Layer):** 4 thực thể (`VaiTro`, `NguoiDung`, `NhanVien`, `KhachHang`) chuẩn 100% Diagram.
- **Chặng 2 (Application Layer):** 4 Repository Interfaces, 3 DTOs, 3 Use Cases.
- **Chặng 3 (Infrastructure Layer):** Cài đặt EF Core 8.0.13, `AppDbContext`, 4 cấu hình Fluent API (TPT) và 4 Repositories.
- **Đã merge PR #5 dọn dẹp Presentation Layer:** Nhánh `main` sạch sẽ, đồng bộ.
- **Rà soát & Đồng bộ 100% Sơ đồ XML với `Đặc tả cơ bản cho hệ thống.md`:** Cập nhật 5 sơ đồ phân hệ XML và `Class Diagram.xml`.
- **Đồng bộ mã nguồn C# khớp 100% với Diagram cho Identity & Catalog:**
  - `CapNhatKhachHangDTO.cs`, `QuanLyKhachHangUseCase.cs`, `ResetMatKhau` trong `QuanLyNhanVienUseCase.cs`.
  - `CapNhatSanPhamDTO.cs`, `CapNhatThongTin` trong `QuanLySanPhamUseCase.cs` & `SanPhamController.cs`.
  - Viết 3 bộ Unit Tests mới, toàn bộ 9/9 tests pass 100%. Đã merge vào `main`.
- **Chuyển đổi hoàn tất Presentation Layer:**
  - Project `BlinkBlink.Admin` chuyển từ WPF sang **Windows Forms (WinForms)** trên nền .NET 8 LTS (`net8.0-windows`).
  - Dọn dẹp toàn bộ file XAML; tạo điểm khởi chạy `Program.cs` và `MainForm.cs` theo phong cách luxury kim hoàn.
  - Project `BlinkBlink.Web` được chuẩn hóa thành **RESTful API Server** (Swagger OpenAPI, CORS, dependency injection cho tất cả Repositories và Use Cases).

## 2. Những gì đang làm dở & Các quyết định kỹ thuật đã chốt
- Quyết định kiến trúc chuẩn:
  1. Admin/POS Desktop viết bằng **C# Windows Forms (.NET 8 LTS)**.
  2. Trang mua sắm khách hàng viết bằng **Web (HTML, CSS, JS)** kết nối tới backend Clean Architecture thông qua **RESTful Web API**.
- Bước tiếp theo: Tiến hành triển khai trọn vẹn **Phân hệ Quản lý Bán hàng (Sales Module)**.

## 3. Hành động cụ thể tiếp theo
1. Merge nhánh `refactor/convert-admin-to-winforms` vào `main`.
2. Tạo nhánh mới `feature/sales-module` và triển khai mã nguồn Phân hệ Bán hàng theo quy trình 2 bước đã duyệt.
