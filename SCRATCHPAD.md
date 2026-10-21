# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Đã gộp hoàn tất toàn bộ phân hệ WinForms Admin (`BlinkBlink.Admin`) vào nhánh `main` và đẩy lên GitHub remote (`origin/main` - commit `ce13154`):**
  - [`UITheme.cs`](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/Helpers/UITheme.cs): Bảng màu kim hoàn sang trọng theo Taste Skill (Champagne Gold `#D4AF37`, Dark Slate `#181C24` / `#202630`, Light Neutral canvas `#F5F7FA`), định dạng số liệu tabular figures căn lề phải.
  - [`AdminServiceScope.cs`](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Services/AdminServiceScope.cs) & [`InMemoryDataContext.cs`](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Services/InMemoryDataContext.cs): Cơ chế cơ sở dữ liệu kép (Dual-Mode Database), tự động kết nối SQL Server và có fallback In-Memory Studio Mode nạp sẵn dữ liệu trang sức cao cấp.
  - 5 UserControls phân hệ: [`OrdersView.cs`](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Orders/OrdersView.cs), [`ProductsView.cs`](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Catalog/ProductsView.cs), [`WarrantyView.cs`](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Warranty/WarrantyView.cs), [`UsersView.cs`](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Identity/UsersView.cs), [`ReportingView.cs`](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Reporting/ReportingView.cs) (khớp 100% `FormBaoCaoThongKe` trong `XML/3 Layers.xml`).
  - [`MainForm.cs`](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Forms/MainForm.cs): Vỏ điều hướng mượt mà, chuyển đổi các phân hệ trên `panelContent`.
- **Trạng thái kiểm thử & Git:** 27/27 tests passed (26 Unit Tests + 1 Integration Test), Solution biên dịch 0 lỗi, 0 cảnh báo. Nhánh `main` sạch sẽ và đồng bộ mới nhất với GitHub remote.

## 2. Những gì đang làm dở & Các quyết định kỹ thuật đã chốt
- Toàn bộ 5 phân hệ Backend và ứng dụng Desktop WinForms Admin đã hoàn tất 100%.
- Đã chốt kiến trúc cho phân hệ tiếp theo: **Giao diện Web Khách hàng (`BlinkBlink.Web`)** giao tiếp với backend thông qua RESTful Web API Controller đã dựng sẵn (CORS, Swagger đã cấu hình).

## 3. Hành động cụ thể tiếp theo cho phiên làm việc sau
1. Tạo nhánh mới `feature/customer-web-ui` từ `main`.
2. Trình bày **Bước 1 (Idea & Proposal)** thiết kế giao diện Web Storefront Khách hàng theo Taste Skill (HTML5, Modern CSS, Vanilla JS Fetch API):
   - Trang chủ giới thiệu thương hiệu và showcase bộ sưu tập trang sức (Hero banner, New Arrivals, Featured Collections).
   - Danh mục sản phẩm trang sức có bộ lọc theo danh mục, chất liệu, size, giá tiền (`/api/SanPham`).
   - Giỏ hàng & Trang thanh toán / Đặt hàng online (`/api/BanHang/dat-hang`).
   - Trang tra cứu tiến độ bảo hành trang sức trực tuyến (`/api/BaoHanh/tra-cuu/{soPhieu}`).
