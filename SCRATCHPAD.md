# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Chuyển đổi toàn bộ CSDL và tầng Data Access sang ADO.NET thuần (không dùng EF Core Migrations):**
  - [`database/BlinkBlinkJewelry.sql`](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/database/BlinkBlinkJewelry.sql): Kịch bản SQL Server độc lập chứa đầy đủ DDL 15 bảng chuẩn 3NF và DML nạp dữ liệu mẫu kim hoàn thực tế cao cấp.
  - [`DatabaseHelper.cs`](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Infrastructure/Data/DatabaseHelper.cs): Lớp tiện ích ADO.NET quản lý `SqlConnection`, thực thi câu lệnh SQL với `SqlParameter` an toàn tuyệt đối.
  - Viết lại toàn bộ 12 Repositories trong [`src/BlinkBlink.Infrastructure/Repositories/`](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Infrastructure/Repositories/) bằng ADO.NET thuần.
  - Xóa bỏ `AppDbContext.cs` và thư mục `Configurations/` trong Infrastructure; xóa bỏ hoàn toàn dữ liệu mồi tĩnh trong mã nguồn C# ([`InMemoryDataContext.cs`](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Services/InMemoryDataContext.cs), [`AdminServiceScope.cs`](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Services/AdminServiceScope.cs)).
  - Cập nhật đồng bộ các sơ đồ kiến trúc XML ([`XML/Class Diagram.xml`](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/XML/Class%20Diagram.xml) và 5 file sơ đồ phân hệ XML) thay thế `AppDbContext` bằng `DatabaseHelper`.
  - Cập nhật tài liệu [`CONG_NGHE_VA_QUY_TRINH.md`](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/CONG_NGHE_VA_QUY_TRINH.md): Ghi nhận vai trò Admin & Nhân viên, WinForms Admin Desktop, Web Khách hàng HTML/CSS/JS/PHP và ADO.NET thuần.
  - Toàn bộ 27/27 tests (Unit & Integration) pass 100%, Solution biên dịch 0 lỗi, 0 cảnh báo.

## 2. Những gì đang làm dở & Các quyết định kỹ thuật đã chốt
- Đã hoàn tất 100% việc chuyển đổi sang ADO.NET thuần và khởi tạo kịch bản SQL Server.
- CSDL được quản lý độc lập qua file `.sql`, sẵn sàng chạy trực tiếp trên SSMS.
- Kiến trúc cho phân hệ tiếp theo: **Giao diện Web Khách hàng (`BlinkBlink.Web`)** (HTML, CSS, JS, PHP nếu có) giao tiếp với backend qua RESTful Web API Controller.

## 3. Hành động cụ thể tiếp theo cho phiên làm việc sau
1. Gộp nhánh `refactor/adonet-database` vào `main`.
2. Tạo nhánh mới `feature/customer-web-ui` từ `main`.
3. Trình bày **Bước 1 (Idea & Proposal)** thiết kế giao diện Web Khách hàng mua sắm, giỏ hàng và tra cứu bảo hành kết nối RESTful API.
