# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành (Tổng kết phiên)
- **Khắc phục triệt để hiện tượng giật rung (hover flicker) ở mép rìa search bar Header:**
  - Bọc `#header-search-container` trong `.header-search-wrap` (`padding: 6px; margin: -6px 0;`) tạo vùng đệm cảm ứng an toàn mở rộng 12px không lệch layout.
  - Tối ưu kích thước nút tròn lên `40x40px`, ngắt `pointer-events: none` cho input khi thu nhỏ và kích hoạt lại khi mở rộng.
  - Tích hợp cơ chế Hover Intent & Debounce Delay `280ms` trong `storefront.js`: Chuột vào mở ngay, chuột rời mép duy trì 280ms chống trượt viền gây giật.
  - Kiểm thử tự động 6/6 tests PASS; 31/31 unit & integration tests pass 100%.

- **Thanh tìm kiếm Header co giãn, Trang tìm kiếm riêng biệt & Sửa lỗi gạch chân menu active:**
  - Đã cập nhật [HomeController.cs](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Web/Controllers/HomeController.cs): Bổ sung action `TimKiem` với route `/TimKiem` và `/tim-kiem`.
  - Đã tạo mới [Views/Home/TimKiem.cshtml](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Web/Views/Home/TimKiem.cshtml): Giao diện tìm kiếm kiệt tác chuyên biệt gồm Breadcrumb, Tiêu đề Editorial, Thanh công cụ lọc đa chiều (từ khóa, danh mục, giá, chất liệu, sort), lưới sản phẩm, empty state và gợi ý liên quan.
  - Đã tinh gọn [Views/Home/Index.cshtml](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Web/Views/Home/Index.cshtml): Dọn dẹp thanh tìm kiếm khỏi section `#catalog` của trang chủ, giữ lại các pill danh mục nhanh và nút `.btn-goto-advanced-search` dẫn sang `/TimKiem`.
  - Đã cập nhật [Views/Shared/_Layout.cshtml](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Web/Views/Shared/_Layout.cshtml): Tích hợp widget `#header-search-container` hình tròn 38x38px, co giãn thành ô nhập từ khóa 280px khi hover/focus; cập nhật liên kết `.nav-links` đa trang.
  - Đã cập nhật [storefront.css](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Web/wwwroot/css/storefront.css): CSS cho thanh search header co giãn mượt mà, layout trang tìm kiếm và nút tìm kiếm nâng cao.
  - Đã cập nhật [storefront.js](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Web/wwwroot/js/storefront.js): `initHeaderSearch` (tìm kiếm bằng phím Enter hoặc nút kính lúp, xóa nhanh, click outside), `initNavigationAndScrollSpy` (click chuyển tab active, IntersectionObserver scroll-spy tự lướt gạch chân vàng theo section, tự active tab "Tìm Kiếm" khi ở `/TimKiem`), tự động đọc query URL `?tuKhoa=` để lọc tức thời.
  - Đã kiểm thử tự động đạt 6/6 tests PASS; 31/31 unit & integration tests pass 100%.

- **Gỡ bỏ hoàn toàn Đặc Tả Use Case ra khỏi trang Web Storefront:**
  - Đã cập nhật [src/BlinkBlink.Web/Views/Shared/_Layout.cshtml](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Web/Views/Shared/_Layout.cshtml): Gỡ bỏ 2 liên kết `Đặc Tả Use Case` tại thanh điều hướng chính Header và chân trang Footer (cột Đặc Quyền Khách Hàng).
  - Đã xóa tệp tĩnh `src/BlinkBlink.Web/wwwroot/docs/DacTaUseCase_KhachHang.html` và thư mục `wwwroot/docs/`.
  - Giữ lại tệp tài liệu nghiên cứu gốc [DacTaUseCase_KhachHang.html](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/DacTaUseCase_KhachHang.html) tại thư mục gốc dự án phục vụ báo cáo học thuật.
  - Toàn bộ solution biên dịch thành công 0 lỗi (.NET 8 LTS); 31/31 unit & integration tests pass 100%.

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
