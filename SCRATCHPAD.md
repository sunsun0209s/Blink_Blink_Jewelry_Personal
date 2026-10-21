# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Chặng 1 (Domain Layer):** 4 thực thể (`VaiTro`, `NguoiDung`, `NhanVien`, `KhachHang`) chuẩn 100% Diagram.
- **Chặng 2 (Application Layer):** 4 Repository Interfaces, 3 DTOs, 3 Use Cases.
- **Chặng 3 (Infrastructure Layer):** Cài đặt EF Core 8.0.13, `AppDbContext`, 4 cấu hình Fluent API (TPT) và 4 Repositories.
- **Đã merge PR #5 dọn dẹp Presentation Layer:** Nhánh `main` sạch sẽ, đồng bộ.
- **Rà soát & Đồng bộ 100% Sơ đồ XML với `Đặc tả cơ bản cho hệ thống.md`:** Cập nhật 5 sơ đồ phân hệ XML và `Class Diagram.xml`.
- **Đồng bộ mã nguồn C# khớp 100% với Diagram cho Identity & Catalog trên nhánh `refactor/sync-identity-catalog`:**
  - `CapNhatKhachHangDTO.cs`: `MaKhachHang`, `HoTen`, `SoDienThoai`, `NgaySinh`.
  - `QuanLyKhachHangUseCase.cs`: `CapNhatThongTin(...)`, `KhoaTaiKhoanKhachHang(...)`.
  - `QuanLyNhanVienUseCase.cs`: bổ sung `ResetMatKhau(string maNv)`.
  - `CapNhatSanPhamDTO.cs`: `MaSanPham`, `TenSanPham`, `MaDanhMuc`, `ChatLieu`, `Size`.
  - `QuanLySanPhamUseCase.cs`: bổ sung `CapNhatThongTin(CapNhatSanPhamDTO request)`.
  - `SanPhamController.cs`: bổ sung endpoint `CapNhatThongTin(...)`.
  - Viết 3 bộ Unit Tests mới, toàn bộ 8 Unit Tests và 1 Integration Test pass 100%.

## 2. Những gì đang làm dở & Các quyết định kỹ thuật đã chốt
- Đã hoàn tất đồng bộ mã nguồn cho các phân hệ hiện có (Identity và Catalog).
- Quyết định chuẩn bị tài liệu `PRD.md` và sơ đồ Activity Diagram cho các phân hệ còn lại trước khi tiến hành code các phân hệ mới (Bán hàng, Bảo hành, Thống kê).
- Nền tảng thống nhất: .NET 8 LTS theo Clean Architecture, áp dụng quy chuẩn Taste Skill Luxury/Editorial cho giao diện Blink Blink Jewelry.

## 3. Hành động cụ thể tiếp theo
1. Merge nhánh `refactor/sync-identity-catalog` vào `main` (hoặc mở PR theo quy trình).
2. Soạn thảo tài liệu `PRD.md` kèm theo các sơ đồ Activity Diagram (bằng cú pháp Mermaid) cho 5 phân hệ.
3. Lên kế hoạch triển khai phân hệ Bán hàng (`HoaDon`, `GioHang`, `DatHangUseCase`, `XuLyDonHangUseCase`) theo đúng quy trình 2 bước.
