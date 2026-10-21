# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Chặng 1 (Domain Layer):** 4 thực thể (`VaiTro`, `NguoiDung`, `NhanVien`, `KhachHang`) chuẩn 100% Diagram.
- **Chặng 2 (Application Layer):** 4 Repository Interfaces, 3 DTOs, 3 Use Cases.
- **Chặng 3 (Infrastructure Layer):** Cài đặt EF Core 8.0.13, `AppDbContext`, 4 cấu hình Fluent API (TPT) và 4 Repositories.
- **Đã tạo nhánh và đẩy commit Revert lên GitHub:**
  - Nhánh: `refactor/revert-identity-presentation`.
  - Đã xóa sạch Controllers, Views, WPF UserControl và cấu hình CSDL tạm thời.
  - Toàn bộ Solution biên dịch thành công 0 lỗi, 0 cảnh báo.
  - Link tạo PR: https://github.com/sunsun0209s/Blink_Blink_Jewelry/pull/new/refactor/revert-identity-presentation

## 2. Những gì đang làm dở & Các quyết định kỹ thuật đã chốt
- Do kho GitHub có cấu hình Rule bảo vệ nhánh `main` ("Changes must be made through a pull request"), mọi thay đổi vào `main` bắt buộc phải qua PR (rất chuẩn theo quy trình nhóm).
- Nhánh `main` sẽ sạch sẽ và chỉ chứa Chặng 1, 2, 3 sau khi bạn bấm Merge PR trên GitHub.

## 3. Hành động cụ thể tiếp theo
1. Bạn XuanTam mở link GitHub PR và bấm Merge vào `main`.
2. Chạy `git checkout main` và `git pull origin main`.
3. Chờ ý kiến thống nhất từ nhóm để bắt đầu Chặng 4 trên nhánh mới.
