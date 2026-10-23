# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành (Tổng kết phiên)
- **Tính năng tìm kiếm & bộ lọc nâng cao kèm gợi ý kiệt tác liên quan cho khách hàng (Storefront Web):**
  - Đã cập nhật [Index.cshtml](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Web/Views/Home/Index.cshtml): Mở rộng thanh công cụ Catalog Toolbar với ô tìm kiếm từ khóa, nút xóa nhanh (`catalog-search-clear`), bộ lọc mức giá, chất liệu, sắp xếp và chỉ báo số lượng kết quả.
  - Đã cập nhật [storefront.js](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Web/wwwroot/js/storefront.js): Tách hàm `createProductCardHtml`, xây dựng hàm chuẩn hóa tiếng Việt `normalizeSearchText`, logic lọc đa tiêu chí thời gian thực `applyProductFilters` (từ khóa, danh mục, giá bán, chất liệu), giao diện rỗng `renderNoResults` hiển thị thông báo "Không tìm thấy sản phẩm phù hợp" kèm khối kiệt tác gợi ý liên quan và nút `resetAllFilters`.
  - Đã cập nhật [storefront.css](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Web/wwwroot/css/storefront.css): Nâng cấp giao diện toolbar, dropdown phong cách Haute Joaillerie, định dạng empty state sang trọng với ánh kim và card kiệt tác gợi ý.
  - Đã kiểm thử tự động đạt 10/10 checks PASS và toàn bộ 31/31 unit & integration tests đều PASSED 100%.

## 2. Những gì đang làm dở & Quyết định kỹ thuật đã chốt
- **Trạng thái công việc dở dang:** Toàn bộ tính năng Storefront Web khách hàng, Đồng bộ 2 chiều CSDL SQL Server và Quản lý khách hàng trên Admin WinForms đã hoàn thành 100%, không còn task dở dang.
- **Quyết định kỹ thuật đã chốt:**
  - Duy trì duy nhất chuẩn Clean Architecture trên .NET 8 LTS cho toàn solution (không nâng .NET 9/10).
  - SQL Server `BlinkBlinkJewelryDb` là nguồn chân lý duy nhất (Single Source of Truth), truy xuất trực tiếp qua RESTful API và ADO.NET thuần.
  - Không sử dụng bất kỳ mảng mock tĩnh (`sampleOrders`, `sampleProducts`) nào trong luồng thương mại thực tế của Web Storefront.
  - Tuân thủ nghiêm ngặt Taste Skill: hình ảnh kim hoàn thật, không dùng icon hay placeholder méo mó.

## 3. Hành động cụ thể tiếp theo cho phiên sau
1. Hoàn thiện tài liệu `PRD.md` và các sơ đồ phân tích nghiệp vụ nâng cao theo danh sách [TODO.md](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/TODO.md).
2. Đóng gói và phát hành tài liệu hướng dẫn vận hành hệ thống.
3. Rà soát nghiệm thu tổng thể hệ thống trước khi bàn giao.
