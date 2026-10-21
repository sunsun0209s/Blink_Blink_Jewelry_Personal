# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Hoàn thiện phân hệ WinForms Admin (`BlinkBlink.Admin`) trên nhánh `feature/admin-winforms` và đã push lên remote:**
  - `UITheme.cs`: Bảng màu kim hoàn cao cấp (Champagne Gold `#D4AF37`, Dark Slate `#181C24` / `#202630`, Light Neutral canvas `#F5F7FA`), font chữ chuẩn tabular figures, thiết kế phẳng chống AI-slop.
  - `AdminServiceScope.cs` & `InMemoryDataContext.cs`: Quản lý phạm vi dịch vụ, tự động kết nối SQL Server và có cơ chế dữ liệu giả lập chất lượng cao tự kích hoạt khi máy chủ CSDL chưa bật.
  - `OrdersView.cs`: Quản lý đơn hàng, duyệt đơn, chuyển trạng thái giao vận, hủy đơn và hoàn kho kèm bảng chi tiết sản phẩm.
  - `ProductsView.cs`: Danh mục sản phẩm trang sức, điều chỉnh giá bán niêm yết, hộp thoại tạo mới sản phẩm kim hoàn.
  - `WarrantyView.cs`: Quản lý phiếu bảo hành, tiếp nhận bảo hành mới, cập nhật tiến độ xử lý và hoàn tất quyết toán chi phí.
  - `UsersView.cs`: Quản lý nhân viên, phân quyền chức vụ, danh sách khách hàng và quản lý khóa tài khoản.
  - `ReportingView.cs`: Khớp 100% bản vẽ `FormBaoCaoThongKe` trong `XML/3 Layers.xml` (bộ lọc ngày, xem doanh thu, xem tồn kho, xem top bán chạy và xuất file CSV mở bằng Excel có UTF-8 BOM).
  - `MainForm.cs`: Điều hướng mượt mà giữa 5 phân hệ với hiệu ứng đổi màu nút sidebar và cập nhật thanh trạng thái kết nối.
- **Trạng thái kiểm thử:** 27/27 tests passed (26 Unit Tests + 1 Integration Test), biên dịch 0 lỗi, 0 cảnh báo trên toàn solution .NET 8 LTS.

## 2. Những gì đang làm dở & Các quyết định kỹ thuật đã chốt
- Đã hoàn tất và đẩy nhánh `feature/admin-winforms` lên GitHub. Sẵn sàng tạo Pull Request để gộp vào nhánh `main` khi có yêu cầu.
- Toàn bộ backend Clean Architecture và giao diện WinForms Admin đã hoàn tất 100%.
- Quyết định kế tiếp: Chuyển sang xây dựng **Giao diện Web Khách hàng (`BlinkBlink.Web`)** theo chuẩn Taste Skill (HTML5, CSS3 hiện đại, JavaScript fetch API kết nối các RESTful Controller đã dựng sẵn).

## 3. Hành động cụ thể tiếp theo
1. Hỗ trợ người dùng gộp nhánh `feature/admin-winforms` vào `main` nếu người dùng yêu cầu.
2. Lên ý tưởng và giải pháp cho **Giao diện Web Storefront Khách hàng** (Trang chủ trưng bày trang sức, danh mục sản phẩm, giỏ hàng, đặt hàng online, tra cứu trạng thái đơn và bảo hành).
