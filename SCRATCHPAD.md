# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Tái cấu trúc toàn diện Phân hệ Sản phẩm & Kho hàng (WinForms Admin):**
  - **Tách 3 UserControl độc lập:**
    - [ProductsView.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Catalog/ProductsView.cs): Quản lý danh sách sản phẩm, lọc danh mục động từ CSDL, tìm kiếm, xem trạng thái kho ("SanSang", "HetHang", "DaBan"), nút Thêm & Sửa sản phẩm.
    - [CategoryView.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Catalog/CategoryView.cs): Quản lý danh mục sản phẩm kim hoàn, thêm mới và hiệu chỉnh danh mục, thống kê số lượng mẫu liên kết.
    - [InventoryInwardView.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Inventory/InventoryInwardView.cs): Quản lý lịch sử nhập kho và lập phiếu nhập kho mới, tự động cập nhật sản phẩm sang trạng thái "SanSang" khi nhập hàng.
  - **Container Sub-navigation Tabs ([ProductsAndInventoryContainerView.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Catalog/ProductsAndInventoryContainerView.cs)):** Điều hướng mượt mà 3 UserControl, tự động đồng bộ dữ liệu chéo khi chuyển tab.
  - **Modal Thêm/Sửa Sản phẩm Chống Đè Nút ([ProductEditDialog.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Catalog/ProductEditDialog.cs)):**
    - Hỗ trợ chọn tệp ảnh thực tế và xem trước (preview) trên khung ảnh kim hoàn.
    - Chống đè nút 100% trên màn hình High DPI (125% - 150%) nhờ FlowLayoutPanel.
    - Khóa mã sản phẩm (ReadOnly) khi chỉnh sửa, cho phép sửa mọi trường còn lại.
  - **Chuẩn hóa Logic Tồn Kho:** Sản phẩm vừa tạo mới có trạng thái ban đầu là "HetHang" (tồn kho = 0); chuyển sang "SanSang" khi có phiếu nhập kho.
  - **Cơ sở dữ liệu & Clean Architecture:** Thêm cột `HinhAnh NVARCHAR(500) NULL` vào bảng `dbo.SanPhamTrangSuc`; bổ sung các phương thức `LayTatCa` trên Repository; cập nhật Use Cases và Entities tương thích 100% Class Diagram.
- Toàn bộ 28/28 unit & integration tests pass 100%, solution biên dịch 0 lỗi, 0 cảnh báo.

## 2. Những gì đang làm dở & Các quyết định kỹ thuật đã chốt
- Hoàn tất 100% phân hệ Sản Phẩm & Kho hàng trên Desktop Admin WinForms theo Hướng tiếp cận 1.
- Nhánh hiện tại: `feature/catalog-inventory-refactor`.

## 3. Hành động cụ thể tiếp theo cho phiên làm việc sau
1. Hợp nhất nhánh `feature/catalog-inventory-refactor` vào `main`.
2. Bắt đầu phân hệ Giao diện Web Khách Hàng (`BlinkBlink.Web`) mua sắm, giỏ hàng, tra cứu bảo hành kết nối Web API theo chuẩn Clean Architecture và Taste Skill.
