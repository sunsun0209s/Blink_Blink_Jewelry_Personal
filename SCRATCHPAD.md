# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Tối ưu hóa không gian hiển thị & nới rộng giao diện WinForms Admin (`BlinkBlink.Admin`):**
  - Mở rộng kích thước cửa sổ chính lên 1480x880 (MinimumSize 1280x720, mở cực đại Maximized).
  - Tái cấu trúc thanh công cụ dạng FlowLayoutPanel tự co giãn chống tràn chữ trên mọi độ phân giải.
  - Cấu hình MinimumWidth cho toàn bộ các cột DataGridView trên cả 5 phân hệ.
  - Sửa lỗi ngoại lệ `SplitContainer` an toàn bằng sự kiện `Load` & `Resize`.
- **Khắc phục triệt để lỗi co cụm DataGridView và nạp dữ liệu SQL Server:**
  - Bổ sung `dgv.Dock = DockStyle.Fill;` vào [`UITheme.ApplyGridViewStyle()`](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/Helpers/UITheme.cs), giúp tất cả DataGridView tự động bung kín 100% diện tích panel.
  - Điều chỉnh thứ tự z-order (`BringToFront()` / `SendToBack()`) trong `OrdersView` và `ReportingView` đảm bảo tiêu đề và lưới hiển thị chuẩn xác.
  - Thực thi kịch bản [`database/BlinkBlinkJewelry.sql`](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/database/BlinkBlinkJewelry.sql) trực tiếp trên máy chủ SQL Server máy trạm (`MSSQLSERVER` / `.` và `.\MSSQL`), tạo hoàn chỉnh 15 bảng CSDL `BlinkBlinkJewelryDb` và nạp toàn bộ dữ liệu mẫu cao cấp.
  - Cập nhật [`AdminServiceScope.cs`](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Services/AdminServiceScope.cs) với cơ chế tự động dò tìm máy chủ SQL Server (`.`, `localhost`, `.\MSSQL`, `(localdb)\mssqllocaldb`).
  - Toàn bộ 27/27 unit & integration tests pass 100%, solution biên dịch 0 lỗi, 0 cảnh báo.

## 2. Những gì đang làm dở & Các quyết định kỹ thuật đã chốt
- Ứng dụng WinForms Admin đã kết nối trực tiếp CSDL SQL Server thật, hiển thị đầy đủ các đơn hàng, sản phẩm kim hoàn, bảo hành, tài khoản nhân viên và khách hàng.
- Giao diện DataGridView đã bung rộng 100% không gian màn hình.
- Phân hệ kế tiếp: Xây dựng Giao diện Web Khách Hàng (`BlinkBlink.Web`) mua sắm, giỏ hàng, tra cứu bảo hành kết nối Web API theo chuẩn Clean Architecture và Taste Skill.

## 3. Hành động cụ thể tiếp theo cho phiên làm việc sau
1. Commit các thay đổi trên nhánh `bugfix/grid-dock-and-database-connection`.
2. Hợp nhất (merge) nhánh `bugfix/grid-dock-and-database-connection` vào nhánh `main`.
3. Đẩy (push) lên GitHub remote.
4. Bắt đầu phân hệ Giao diện Web Khách Hàng (Tạo nhánh `feature/customer-web-ui`, trình bày Bước 1 Đề xuất Ý tưởng & Giải pháp).
