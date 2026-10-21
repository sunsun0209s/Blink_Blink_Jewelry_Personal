# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Chặng 1 (Domain Layer):** 4 thực thể (`VaiTro`, `NguoiDung`, `NhanVien`, `KhachHang`) chuẩn 100% Diagram.
- **Chặng 2 (Application Layer):** 4 Repository Interfaces, 3 DTOs, 3 Use Cases.
- **Chặng 3 (Infrastructure Layer):** Cài đặt EF Core 8.0.13, `AppDbContext`, 4 cấu hình Fluent API (TPT) và 4 Repositories.
- **Đã merge PR #5 dọn dẹp Presentation Layer:** Nhánh `main` sạch sẽ, đồng bộ.
- **Rà soát & Đồng bộ 100% Sơ đồ XML với `Đặc tả cơ bản cho hệ thống.md`:**
  - `XML/Phân hệ quản lý dịch vụ và bảo hành.xml`: Đổi `_dichVuYeuCau` -> `_lyDoBaoHanh`, `_chiPhiDuKien` / `_chiPhiSuaChua` -> `_chiPhiBaoHanh`, `HoanTatBaoHanh()`, `TinhPhiBaoHanh()`.
  - `XML/Phân hệ quản lý bán hàng.xml`: `HoaDon` bổ sung thông tin nhân viên xử lý `_maNhanVien`, địa chỉ, sđt, pttt, trạng thái đơn hàng; bổ sung `XuLyDonHangUseCase`, `CapNhatDonHangDTO`, mở rộng `IHoaDonRepository` và `BanHangController`.
  - `XML/Phân hệ quản lý người dùng và phân quyền.xml`: Bổ sung `ResetMatKhau` cho nhân viên.
  - `XML/Phân hệ quản lý sản phẩm và kho hàng.xml`: Bổ sung `CapNhatSanPhamDTO` & method `CapNhatThongTin`.
  - `XML/Phân hệ báo cáo và thống kê.xml`: Bổ sung truy vấn sản phẩm doanh thu thấp.
  - `XML/Class Diagram.xml`: Đồng bộ hoàn chỉnh toàn bộ 7 tabs (Tab 1 quan hệ tổng thể + Tab 2 "3 Layers" + 5 Tabs phân hệ chi tiết).
  - Solution build & test thành công trên .NET 8 LTS (0 warning, 0 error).

## 2. Những gì đang làm dở & Các quyết định kỹ thuật đã chốt
- Quyết định chuẩn bị đầy đủ PRD (Product Requirements Document) và sơ đồ Activity Diagram trước khi bắt tay viết mã tầng Presentation.
- Các sơ đồ XML đã được chốt và đồng bộ hoàn toàn với bản đặc tả của XuanTam.
- Nền tảng thống nhất: .NET 8 LTS theo Clean Architecture, áp dụng quy chuẩn Taste Skill Luxury/Editorial cho giao diện Blink Blink Jewelry.

## 3. Hành động cụ thể tiếp theo
1. Thống nhất với XuanTam về nội dung tài liệu `PRD.md` tổng hợp từ bản đặc tả và các sơ đồ đã chốt.
2. Vẽ các sơ đồ Activity Diagram (bằng cú pháp Mermaid) cho các luồng nghiệp vụ chính của từng phân hệ (đặc biệt: Luồng Đặt hàng & Xử lý duyệt đơn hàng, Luồng Tiếp nhận & Hoàn tất bảo hành).
3. Chuẩn bị tạo nhánh tính năng tiếp theo để bước vào giai đoạn code theo quy trình 2 bước.
