# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Đồng bộ nhánh & Cập nhật mã nguồn mới nhất từ `origin/main`:**
  - Kéo và gộp thành công toàn bộ các cập nhật mới nhất từ `origin/main` vào nhánh làm việc `feature/customer-web-ui`.
  - Khắc phục triệt để các lỗi giao diện WinForms Admin: triệt tiêu hiện tượng căn lề so le trong DataGridView, chống tràn/đè thẻ KPI "Tổng Mẫu", cấu trúc `FlowLayoutPanel` cho modal Nhập kho, tính giá nhập bình quân gia quyền và bổ sung % giảm giá sản phẩm.
- **Phân hệ Web Khách Hàng (`BlinkBlink.Web`):**
  - **Bộ lưu trữ giỏ hàng độc lập & Bền vững dữ liệu (`src/BlinkBlink.Web/wwwroot/js/storefront.js`):** Sử dụng khóa `localStorage` riêng biệt `blink_jewelry_cart` độc lập hoàn toàn với phiên đăng nhập. Khi đăng xuất tài khoản (`logoutUser`), chỉ xóa phiên `blink_currentUser`, giỏ hàng và danh sách sản phẩm đã chọn giữ nguyên 100%. Khi tải lại trang (F5), giỏ hàng tự động nạp lại chính xác từ bộ nhớ trình duyệt.
  - **Khắc phục lỗi Menu Dropdown & Đăng xuất:** `storefront.css` loại bỏ hover tự động, menu chỉ hiển thị khi có class `.logged-in` và đang mở `.open`. Hàm `logoutUser()` reset ngay lập tức họ tên về "Khách Hàng", mã về "Thành viên Blink Blink", đóng dropdown tức thì mà không cần F5.
  - **Loại bỏ dữ liệu mồi & Khối đăng nhập 1-click (`_Layout.cshtml`, `storefront.js`):** Xóa sạch mồi dữ liệu, giữ nguyên 100% CSDL chuẩn trong SQL Server.
  - **Xử lý ngoại lệ CSDL trong API Backend (`KhachHangController.cs`):** Bắt lỗi `SqlException` 2601/2627 khi trùng tên đăng nhập, trả về thông báo tiếng Việt thân thiện thay vì lỗi 500.
  - **Bộ Kiểm Thử Tự Động Toàn Diện:** Kịch bản API & Database hoàn thành 10/10 test cases PASS 100%; kịch bản E2E trên trình duyệt 10/10 bước PASS 100%.
- **Toàn bộ Unit Tests Solution:** 31/31 unit tests pass 100%, solution biên dịch 0 lỗi, 0 cảnh báo.

## 2. Quyết định kỹ thuật đã chốt
- Web Khách Hàng giao tiếp hoàn toàn với Backend qua RESTful API, tuân thủ Clean Architecture .NET 8 LTS.
- Dữ liệu CSDL SQL Server `BlinkBlinkJewelryDb` trên `Server=.` được giữ nguyên vẹn cấu trúc và dữ liệu.
- Phân hệ Web và WinForms hoàn toàn độc lập ở tầng Presentation, dùng chung tầng Domain & Application và CSDL SQL Server.

## 3. Hành động cụ thể tiếp theo
1. Kiểm thử liên thông: Đặt hàng trên Web Khách Hàng -> Đơn hàng tức thì xuất hiện trên WinForms Admin và tiến hành Duyệt đơn / Cập nhật trạng thái.
2. Hoàn thiện tài liệu `PRD.md` và các sơ đồ phân tích nghiệp vụ nâng cao nếu có yêu cầu bổ sung.
3. Tạo Pull Request đưa `feature/customer-web-ui` vào `main`.
