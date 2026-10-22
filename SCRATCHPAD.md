# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành (Tổng kết phiên)
- **Đồng bộ ảnh sản phẩm đơn hàng từ CSDL SQL Server thật & Triệt tiêu lỗi vỡ ảnh (Taste Skill):**
  - Đã khắc phục [HoaDonRepository.cs](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Infrastructure/Repositories/HoaDonRepository.cs): `LayChiTietHoaDon` nạp đầy đủ `s.HinhAnh` và `s.PhanTramGiamGia` vào đối tượng `SanPhamTrangSuc` của từng `ChiTietHoaDon`.
  - Đã cập nhật [storefront.js](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Web/wwwroot/js/storefront.js): `renderCustomerOrders` và `renderCheckoutSummary` nhận diện an toàn cả camelCase lẫn PascalCase, fallback về ảnh kim hoàn chuẩn `/images/hero-jewelry.jpg` kèm `onerror=null` và `loading="lazy"`.
  - Đã tạo tệp ảnh tĩnh dự phòng hợp lệ tại `src/BlinkBlink.Web/wwwroot/images/products/no-image.png`.
  - Hoàn tất chuỗi 12 vòng kiểm thử thương mại (`run_12_image_order_tests.ps1`) đạt 12/12 PASS 100%. Xác nhận đơn hàng `HD20261022210438990` nạp chính xác ảnh thật 100% không còn vỡ ảnh.
- **Quy trình Git & Đồng bộ kho mã nguồn:**
  - Đã commit toàn bộ thay đổi vào nhánh `feature/customer-web-ui` và đẩy lên remote (`git push origin feature/customer-web-ui`).
  - Đã merge thành công nhánh `feature/customer-web-ui` vào nhánh chính `main` (`merge --no-ff`), chạy kiểm thử 31/31 tests pass 100%.
  - Đã đẩy hoàn tất lên `origin/main` (`git push origin main`), working tree sạch sẽ.
  - Đã ghi nhận lịch sử vào [COMPLETED.md](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/COMPLETED.md) và cập nhật [TODO.md](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/TODO.md).

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
