# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Đồng bộ ảnh sản phẩm đơn hàng từ CSDL SQL Server thật & Triệt tiêu lỗi vỡ ảnh (Taste Skill):**
  - Đã khắc phục [HoaDonRepository.cs](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Infrastructure/Repositories/HoaDonRepository.cs): `LayChiTietHoaDon` nạp đầy đủ `s.HinhAnh` và `s.PhanTramGiamGia` vào đối tượng `SanPhamTrangSuc` của từng `ChiTietHoaDon`.
  - Đã cập nhật [storefront.js](file:///d:/Học%20tập/Năm%203/Học%20kì%201/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Web/wwwroot/js/storefront.js): `renderCustomerOrders` và `renderCheckoutSummary` nhận diện an toàn cả camelCase lẫn PascalCase, fallback về ảnh kim hoàn chuẩn `/images/hero-jewelry.jpg` kèm `onerror=null` và `loading="lazy"`.
  - Đã tạo tệp ảnh tĩnh dự phòng hợp lệ tại `src/BlinkBlink.Web/wwwroot/images/products/no-image.png`.
  - Hoàn tất chuỗi 12 vòng kiểm thử thương mại (`run_12_image_order_tests.ps1`) đạt 12/12 PASS 100%. Xác nhận đơn hàng `HD20261022210438990` từ screenshot của khách hàng nạp chính xác ảnh thật 100% không còn vỡ ảnh.

## 2. Quyết định kỹ thuật đã chốt
- Đơn hàng online lưu trực tiếp vào CSDL SQL Server thật (`dbo.HoaDon`, `dbo.ChiTietHoaDon`).
- Phía Web Storefront và WinForms Admin cùng chia sẻ một nguồn chân lý dữ liệu (Single Source of Truth) từ SQL Server thông qua RESTful API và ADO.NET thuần.
- Không sử dụng bất kỳ mảng dữ liệu giả lập (mock/sample) nào trong luồng thương mại thực tế của Web Storefront.
- Mọi hình ảnh fallback đều trỏ về ảnh kim hoàn Haute Joaillerie thật, tuân thủ tuyệt đối quy định Anti-Slop Bans trong `GEMINI.md`.

## 3. Hành động cụ thể tiếp theo
1. Hoàn thiện tài liệu `PRD.md` và các sơ đồ phân tích nghiệp vụ nâng cao theo danh sách `TODO.md`.
2. Đóng gói và phát hành tài liệu hướng dẫn vận hành hệ thống.
3. Mở Pull Request đưa nhánh `feature/customer-web-ui` vào `main`.
