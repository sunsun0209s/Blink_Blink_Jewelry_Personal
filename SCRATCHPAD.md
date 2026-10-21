# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Đã gộp toàn bộ 5 Phân hệ nghiệp vụ Backend vào nhánh `main` và đẩy lên GitHub (`origin/main`):**
  1. **Phân hệ 1: Quản lý Người dùng & Phân quyền (Identity & Access Control)**: Đăng nhập, đăng ký khách hàng, quản lý nhân viên, đổi mật khẩu, phân quyền vai trò.
  2. **Phân hệ 2: Quản lý Sản phẩm & Kho hàng (Catalog & Inventory)**: Danh mục, sản phẩm trang sức, phiếu nhập kho, phiếu kiểm kê, cập nhật giá và tồn kho.
  3. **Phân hệ 3: Quản lý Bán hàng & Đơn hàng (Sales Management)**: Giỏ hàng, tạo đơn hàng online, phê duyệt đơn, cập nhật tiến độ giao hàng, hủy đơn và hoàn kho.
  4. **Phân hệ 4: Quản lý Dịch vụ & Bảo hành (Warranty Management)**: Tiếp nhận bảo hành, quản lý tiến độ xử lý trang sức, hoàn tất quyết toán chi phí.
  5. **Phân hệ 5: Báo cáo & Thống kê (Reporting & Analytics)**: Thống kê doanh thu, lợi nhuận ước tính theo mốc thời gian, báo cáo tồn kho & chênh lệch kiểm kê, Top bán chạy và sản phẩm doanh thu thấp.
- **Trạng thái kiểm thử:** 27/27 tests passed 100%, Solution biên dịch 0 lỗi, 0 cảnh báo trên .NET 8 LTS.
- **Trạng thái Git:** Nhánh `main` sạch sẽ, đồng bộ mới nhất với GitHub remote.

## 2. Những gì đang làm dở & Các quyết định kỹ thuật đã chốt
- Nền tảng Backend Clean Architecture của 5 phân hệ đã hoàn chỉnh 100% theo Diagram và Đặc tả hệ thống.
- Chuyển sang giai đoạn **Presentation Layer (Giao diện người dùng)**:
  1. **WinForms Admin (`BlinkBlink.Admin`):** Thiết kế giao diện WinForms quản trị POS/Desktop cho Nhân viên và Quản trị viên (form trang nhã, căn gióng số liệu tabular figures, màu sắc sang trọng theo quy chuẩn Taste Skill của Blink Blink Jewelry).
  2. **Web Khách hàng (`BlinkBlink.Web`):** Giao diện Web storefront mua sắm trực tuyến (HTML5, Modern CSS, JavaScript Vanilla/Fetch) gọi các RESTful Web API Controller đã dựng sẵn.

## 3. Hành động cụ thể tiếp theo
1. Thống nhất với người dùng triển khai giao diện nào trước: **WinForms Admin** hay **Web Storefront Khách hàng**.
2. Tạo nhánh tính năng tương ứng (`feature/admin-ui` hoặc `feature/customer-web-ui`) và triển khai.
