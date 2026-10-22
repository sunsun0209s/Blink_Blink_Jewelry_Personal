# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Tối ưu hóa hiển thị Sidebar Menu & Tự động xuống dòng ([MainForm.Designer.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Forms/MainForm.Designer.cs)):**
  - Mở rộng thanh Sidebar từ 240px lên 260px, nâng chiều cao nút lên 54px và cấu hình `Padding = new Padding(16, 4, 10, 4)`.
  - Tắt `AutoEllipsis` để chuỗi text dài tự động ngắt dòng tự nhiên mà không bị che khuất hay đè lên nhau trên các màn hình có tỉ lệ phóng to High DPI (125% - 150%).
- **Khắc phục triệt để lỗi khóa ngoại `FK_HoaDon_NhanVien` khi Duyệt Đơn / Cập nhật trạng thái:**
  - Trong [OrdersView.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Orders/OrdersView.cs), chuyển mã nhân viên gán duyệt đơn từ `"NV001"` sang mã tài khoản `"USER_NV001"` phù hợp với bảng cha `dbo.NguoiDung`.
  - Trong [HoaDonRepository.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Infrastructure/Repositories/HoaDonRepository.cs), bổ sung ánh xạ tự động `MaNhanVien` -> `MaNguoiDung` thông qua biểu thức `CASE WHEN` trực tiếp trong SQL Server, ngăn ngừa 100% lỗi xung đột khóa ngoại.
  - Bổ sung khối `try-catch` an toàn trên toàn bộ các sự kiện duyệt/đổi trạng thái/hủy đơn trong `OrdersView`.
  - Đồng bộ và khôi phục đơn hàng `HD1003` trên CSDL SQL Server về lại trạng thái ban đầu `ChoDuyet` (khớp với kịch bản [BlinkBlinkJewelry.sql](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/BlinkBlinkJewelry.sql)).
- Toàn bộ 27/27 unit & integration tests pass 100%, solution biên dịch 0 lỗi, 0 cảnh báo.

## 2. Những gì đang làm dở & Các quyết định kỹ thuật đã chốt
- WinForms Admin đã xử lý xong toàn diện lỗi giao diện Sidebar và lỗi khóa ngoại hóa đơn.
- Nhánh hiện tại: `bugfix/sidebar-text-wrap-and-order-status`.
- Phân hệ kế tiếp: Xây dựng Giao diện Web Khách Hàng (`BlinkBlink.Web`) mua sắm, giỏ hàng, tra cứu bảo hành kết nối Web API theo chuẩn Clean Architecture và Taste Skill.

## 3. Hành động cụ thể tiếp theo cho phiên làm việc sau
1. Commit các thay đổi trên nhánh `bugfix/sidebar-text-wrap-and-order-status`.
2. Hợp nhất (merge) nhánh `bugfix/sidebar-text-wrap-and-order-status` vào nhánh `main`.
3. Đẩy (push) lên GitHub remote.
4. Bắt đầu phân hệ Giao diện Web Khách Hàng (Tạo nhánh `feature/customer-web-ui`, trình bày Bước 1 Đề xuất Ý tưởng & Giải pháp).
