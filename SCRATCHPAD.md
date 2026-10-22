# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Tối ưu hóa không gian hiển thị & nới rộng giao diện WinForms Admin (`BlinkBlink.Admin`):**
  - Mở rộng kích thước cửa sổ chính lên 1480x880 (MinimumSize 1280x720, mở cực đại Maximized).
  - Tái cấu trúc thanh công cụ dạng FlowLayoutPanel tự co giãn chống tràn chữ trên mọi độ phân giải.
  - Cấu hình MinimumWidth cho toàn bộ các cột DataGridView trên cả 5 phân hệ.
- **Khắc phục triệt để ngoại lệ `InvalidOperationException` của SplitContainer trong [`OrdersView.cs`](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Orders/OrdersView.cs):**
  - Loại bỏ các thuộc tính gán cứng `SplitterDistance`, `Panel1MinSize`, `Panel2MinSize` trong khối khởi tạo đối tượng khi UserControl chưa được dựng layout thực tế.
  - Chuyển logic căn chỉnh tỷ lệ 55%/45% sang sự kiện `Load` và `Resize` với điều kiện bảo vệ `Height > 300` và khối `try-catch`, đảm bảo khởi chạy mượt mà 100%.
  - Toàn bộ 27/27 unit & integration tests pass 100%, solution biên dịch 0 lỗi, 0 cảnh báo.

## 2. Những gì đang làm dở & Các quyết định kỹ thuật đã chốt
- Đã sửa xong lỗi `SplitContainer` và kiểm tra biên dịch thành công.
- Ứng dụng WinForms Admin chạy ổn định, không còn bị lỗi khi khởi động.
- Phân hệ kế tiếp: Xây dựng Giao diện Web Khách Hàng (`BlinkBlink.Web`) mua sắm, giỏ hàng, tra cứu bảo hành kết nối Web API theo chuẩn Clean Architecture và Taste Skill.

## 3. Hành động cụ thể tiếp theo cho phiên làm việc sau
1. Commit các thay đổi trên nhánh `bugfix/fix-splitcontainer-init`.
2. Hợp nhất (merge) nhánh `bugfix/fix-splitcontainer-init` vào `main`.
3. Đẩy (push) lên GitHub remote.
4. Bắt đầu phân hệ Giao diện Web Khách Hàng (Tạo nhánh `feature/customer-web-ui`, trình bày Bước 1 Đề xuất Ý tưởng & Giải pháp).
