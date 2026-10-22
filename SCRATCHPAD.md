# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Khắc phục triệt để các lỗi giao diện theo phản hồi người dùng:**
  - **Triệt tiêu hiện tượng căn lề so le (18.000.000 vs 12.000.000):** Xóa bỏ ghi đè `Alignment = MiddleLeft` trong `AlternatingRowsDefaultCellStyle` tại [UITheme.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/Helpers/UITheme.cs). Mọi dòng trong DataGridView kế thừa 100% căn lề của cột (`MiddleRight` cho tiền tệ, `MiddleCenter` cho số lượng/% giảm giá, `MiddleLeft` cho văn bản).
  - **Chống đè thẻ KPI "Tổng Mẫu":** Tái cấu trúc `topHeaderPanel` tại [ProductsView.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Catalog/ProductsView.cs), [InventoryInwardView.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Inventory/InventoryInwardView.cs), [CategoryView.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Catalog/CategoryView.cs) và [OrdersView.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Orders/OrdersView.cs) dùng `pnlTitleContainer` neo `Dock = Fill` kết hợp `pnlKpis.SendToBack()`, ngăn chặn tuyệt đối dòng giới thiệu (subtitle) đè qua mép thẻ KPI.
  - **Chống đè nhãn và ô nhập trong Modal Nhập kho:** Chuyển đổi toàn bộ khu vực `pnlInfo` và `pnlAddItem` trong [InventoryInwardView.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Inventory/InventoryInwardView.cs) sang `FlowLayoutPanel` 2 hàng tuần tự với khoảng đệm `Margin` chuẩn xác. Loại bỏ 100% nguy cơ nhãn "Số Lượng (*):" và "Đơn Giá Vốn (VND) (*):" đè lên các ô nhập liệu `nudSl` và `txtGiaVon`.
- **Cơ sở dữ liệu & Kiểm thử:** Toàn bộ 31/31 unit & integration tests pass 100%, solution biên dịch 0 lỗi, 0 cảnh báo.

## 2. Những gì đang làm dở & Các quyết định kỹ thuật đã chốt
- Toàn bộ giao diện Admin WinForms và các phân hệ Sản phẩm / Danh mục / Nhập kho / Đơn hàng đã được tối ưu hoàn thiện 100% theo phản hồi thực tế của người dùng.
- Nhánh hiện tại: `feature/catalog-inventory-refactor`.

## 3. Hành động cụ thể tiếp theo cho phiên làm việc sau
1. Merge nhánh `feature/catalog-inventory-refactor` vào `main`.
2. Bắt đầu phân hệ Giao diện Web Khách Hàng (`BlinkBlink.Web`) mua sắm, giỏ hàng, tra cứu bảo hành kết nối Web API theo chuẩn Clean Architecture và Taste Skill.
