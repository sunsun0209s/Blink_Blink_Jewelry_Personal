# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Bộ lưu trữ giỏ hàng độc lập & Bền vững dữ liệu (`src/BlinkBlink.Web/wwwroot/js/storefront.js`):**
  - Sử dụng khóa `localStorage` riêng biệt `blink_jewelry_cart` độc lập hoàn toàn với phiên đăng nhập.
  - Khi đăng xuất tài khoản (`logoutUser`), chỉ xóa phiên `blink_currentUser`, giỏ hàng và danh sách sản phẩm đã chọn giữ nguyên 100%.
  - Khi tải lại trang (F5), giỏ hàng tự động nạp lại chính xác từ bộ nhớ trình duyệt.
- **Khắc phục lỗi Menu Dropdown & Đăng xuất:**
  - `storefront.css`: Loại bỏ hover tự động, menu chỉ hiển thị khi có class `.logged-in` và đang mở `.open`.
  - `storefront.js`: Hàm `logoutUser()` reset ngay lập tức họ tên về "Khách Hàng", mã về "Thành viên Blink Blink", đóng dropdown tức thì mà không cần F5.
- **Loại bỏ dữ liệu mồi & Khối đăng nhập 1-click (`_Layout.cshtml`, `storefront.js`):**
  - Xóa sạch mồi dữ liệu, giữ nguyên 100% CSDL chuẩn trong SQL Server.
- **Xử lý ngoại lệ CSDL trong API Backend (`KhachHangController.cs`):**
  - Bắt lỗi `SqlException` 2601/2627 khi trùng tên đăng nhập, trả về thông báo tiếng Việt thân thiện thay vì lỗi 500.
- **Bộ Kiểm Thử Tự Động Toàn Diện:**
  - Kịch bản API & Database: Hoàn thành 10/10 test cases PASS 100% (kiểm tra trang chủ, nạp tài nguyên tĩnh, xác thực tài khoản thật `lan.tran`/`lan123`, chặn mật khẩu sai, đăng ký tài khoản mới lưu SQL Server, bắt lỗi trùng tên, tra cứu hồ sơ khách hàng, 27/27 Unit Tests Clean Architecture).
  - Kịch bản E2E trên trình duyệt: 10/10 bước kiểm thử đều PASS 100%.

## 2. Quyết định kỹ thuật đã chốt
- Web Khách Hàng giao tiếp hoàn toàn với Backend qua RESTful API, tuân thủ Clean Architecture .NET 8 LTS.
- Dữ liệu CSDL SQL Server `BlinkBlinkJewelryDb` trên `Server=.` được giữ nguyên vẹn cấu trúc và dữ liệu.
- Phân hệ Web và WinForms hoàn toàn độc lập, dùng chung tầng Domain & Application.

## 3. Hành động cụ thể tiếp theo
1. Chuẩn bị tài liệu thuyết trình / demo báo cáo bài tập lớn ngày mai.
2. Hoàn thiện tài liệu PRD hoặc hướng dẫn vận hành nếu có yêu cầu bổ sung.
3. Tạo commit và sẵn sàng mở Pull Request đưa `feature/customer-web-ui` vào `main`.

