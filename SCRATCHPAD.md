# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành (Tổng kết phiên)
- **Rà soát & Đồng bộ Toàn diện 5 Sơ đồ Kiến trúc XML Khớp 100% với C# Code:**
  - `XML/Phân hệ quản lý sản phẩm và kho hàng.xml`: Bổ sung `_hinhAnh`, `_phanTramGiamGia`, `_soLuongTon` cho `SanPhamTrangSuc`.
  - `XML/Phân hệ quản lý bán hàng.xml`: Bổ sung `CapNhatKhachHangTheoSoDienThoai` cho `IHoaDonRepository`/`HoaDonRepository`, use case `BanHangOfflineUseCase`, DTO `TaoHoaDonOfflineDTO`, `SanPhamMuaOfflineDTO`.
  - `XML/Phân hệ quản lý người dùng và phân quyền.xml`: Bổ sung `TimTheoSoDienThoai`, `LayTatCa`, `KiemTraCoTheXoa`, `XoaKhachHang` cho `IKhachHangRepository`/`KhachHangRepository`; `LayTatCa`, `CapNhatNhanVien`, `KiemTraCoTheXoa`, `XoaNhanVien` cho `INhanVienRepository`/`NhanVienRepository`; bổ sung phương thức xóa có điều kiện chứng từ cho `QuanLyNhanVienUseCase` và `QuanLyKhachHangUseCase`.
  - `XML/3 Layers.xml`: Loại bỏ hoàn toàn 15 vị trí chứa `AppDbContext` và `- _context: AppDbContext` thay bằng `DatabaseHelper` và `- _dbHelper: DatabaseHelper`; bổ sung thuộc tính cho `SanPhamTrangSuc` và chuẩn hóa chi phí bảo hành ước tính / thực tế cho `PhieuBaoHanh`.
  - `XML/Class Diagram.xml`: Cập nhật `c_SanPhamTrangSuc` tại Trang 1, ráp nối đồng bộ chuẩn xác toàn bộ 4 phân hệ mới vào các trang draw.io trong file mẹ, bảo toàn trọn vẹn 7 trang.
  - Solution biên dịch 0 error, 0 warning (.NET 8 LTS); 35/35 test cases PASS 100%.
- **Cơ chế Tự động Khởi tạo CSDL (Auto-Database Provisioning) & Đồng bộ Admin Mặc định:**
  - Nâng cấp [DatabaseHelper.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Infrastructure/Data/DatabaseHelper.cs) với `TryProvisionDatabase`: Khi clone dự án về máy mới chưa có `BlinkBlinkJewelryDb`, tự động kết nối qua `master` tạo database, phân tách khối lệnh `GO` và chạy toàn bộ script DDL/DML.
  - Nhúng [BlinkBlinkJewelry.sql](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/BlinkBlinkJewelry.sql) làm `EmbeddedResource` trong DLL `BlinkBlink.Infrastructure`, tự động dự phòng đọc file vật lý hoặc tài nguyên nhúng.
  - Tích hợp tự động khởi tạo CSDL vào [AdminServiceScope.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Services/AdminServiceScope.cs) (WinForms) và [Program.cs (Web)](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Web/Program.cs).
  - Bổ sung dữ liệu và tài khoản quản trị `admin` (`123456789`) dự phòng trong [InMemoryDataContext.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Services/InMemoryDataContext.cs).
  - Đồng bộ mật khẩu `admin` = `123456789` trong script [BlinkBlinkJewelry.sql](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/BlinkBlinkJewelry.sql) và [database/BlinkBlinkJewelry.sql](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/database/BlinkBlinkJewelry.sql).
  - Tạo mới bài test [DatabaseHelperTests.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/tests/BlinkBlink.IntegrationTests/DatabaseHelperTests.cs); toàn bộ 35/35 tests PASSED 100%.
- **Thực hiện Commit Tự Động cho thành viên `qhy` sang Team Repo:**
  - Nhánh `feature/identity-management-module` tại `C:\Users\huy\Downloads\code\laptrinhCSharp\Blink_Blink_Jewelry`.
  - Commit `ab62b2a`: Đã đưa toàn bộ phân hệ Identity & Khách hàng (22 tệp mã nguồn sạch sẽ, không file rác).
  - Cập nhật [scheduleCommit.md](scheduleCommit.md): `qhy` đã chuyển sang mục *thành viên đã thực hiện*.
- **Thực hiện Commit Tự Động cho thành viên `anhthy` sang Team Repo:**
  - Nhánh `feature/cart-orders-foundation-module` tại `E:\NNLT CS\DoAn_CSharp\Blink_Blink_Jewelry`.
  - Commit `af335b0`: Đã đưa toàn bộ phân hệ Giỏ hàng, Đơn hàng & Nền tảng Kết nối CSDL (14 tệp mã nguồn sạch sẽ, không file rác).
  - Đã push thành công lên GitHub và tạo nhánh sẵn sàng mở Pull Request.
  - Cập nhật [scheduleCommit.md](scheduleCommit.md): `anhthy` đã chuyển sang mục *thành viên đã thực hiện*.
- **Thực hiện Commit Tự Động cho thành viên `sun` sang Team Repo:**
  - Nhánh `feature/catalog-reporting-module` tại `D:\Học tập\Năm 3\Học kì 1\Blink_Blink_Jewelry`.
  - Commit `ec45874`: Đã đưa toàn bộ phân hệ Catalog & Báo cáo Thống kê (17 tệp mã nguồn sạch sẽ, không file rác).
  - Đã push thành công lên GitHub (`origin/feature/catalog-reporting-module`) và sẵn sàng mở Pull Request.
  - Cập nhật [scheduleCommit.md](scheduleCommit.md): `sun` đã chuyển sang mục *thành viên đã thực hiện*.
- **Đồng bộ toàn diện Bộ đôi Tài liệu Lộ trình & Hướng dẫn ([scheduleCommit.md](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/scheduleCommit.md), [huongdanCommit.md](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/huongdanCommit.md)):**
  - Chuyển đổi 100% cả 2 tài liệu từ lộ trình 3 ngày sang quy trình 1 lần duy nhất hoàn thành cho cả 4 thành viên (`qhy`, `sun`, `anhthy`, `tlinh`).
  - Phân bổ 4 nhánh tính năng duy nhất tương ứng với 4 module: Identity, Catalog/Báo cáo, Giỏ hàng/Hóa đơn/CSDL, Kho/Bảo hành.
  - Bảo toàn trọn vẹn mốc commit baseline `7993495` và Bảng Audit Trail 68 tệp gốc.
- **Quản lý sản phẩm & Kho hàng ([ProductsView.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Catalog/ProductsView.cs), [SanPhamTrangSuc.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Domain/Entities/Catalog/SanPhamTrangSuc.cs), [SanPhamRepository.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Infrastructure/Repositories/SanPhamRepository.cs)):**
  - Thêm thuộc tính `SoLuongTon` vào thực thể `SanPhamTrangSuc`.
  - Tích hợp subquery tính tồn kho tự động trong CSDL SQL Server: `Tổng nhập (ChiTietNhapKho) - Tổng xuất (ChiTietHoaDon không bị hủy)`.
  - Thêm cột `TỒN KHO` trên DataGridView căn giữa với font số tabular: màu xanh lục ngọc khi tồn > 0, màu đỏ cảnh báo khi tồn = 0.
- **Sửa lỗi KPI Quản trị viên = 0 ([UsersView.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Identity/UsersView.cs)):**
  - Khắc phục lỗi so sánh cứng `n.MaVaiTro == "VT01"` sang kiểm tra đồng thời cả `ADMIN` và `VT01`. KPI hiển thị đúng số lượng quản trị viên.
- **Màn hình Đăng Nhập khởi động ([LoginForm.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Forms/LoginForm.cs), [Program.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Program.cs)):**
  - Khởi chạy form đăng nhập thẩm mỹ cao chuẩn Taste Skill khi bật ứng dụng, xác thực qua `INguoiDungRepository`.
  - Loại bỏ hoàn toàn khối tài khoản mẫu, điều chỉnh kích thước form gọn gàng 440px chuẩn bảo mật.
  - Vòng lặp `while (true)` trong `Program.cs` duy trì đăng nhập / đăng xuất mượt mà.
- **Tái cấu trúc Header [MainForm.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Forms/MainForm.cs):**
  - Thay nút đổi vai trò thành 2 nút: `"🔑 Đổi Mật Khẩu"` (mở [ChangePasswordDialog.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Forms/ChangePasswordDialog.cs)) và `"🚪 Đăng Xuất"`.
  - Chuẩn hóa hiển thị dòng tài khoản: `Tài khoản: tên đăng nhập [vai trò]` (ví dụ: `Tài khoản: admin [admin]`, `Tài khoản: thao.le [sale]`, `Tài khoản: huy.nguyen [warehouse]`).
- **Kiểm thử tự động:**
  - Bổ sung 2 test cases mới trong `QuanLySanPhamUseCaseTests.cs` và `QuanLyNhanVienUseCaseTests.cs`.
  - Giải pháp biên dịch 0 error, 0 warning (.NET 8 LTS); toàn bộ 35/35 test cases PASSED 100%.

## 2. Những gì đang làm dở & Quyết định kỹ thuật đã chốt
- **Trạng thái:** Toàn bộ 5 yêu cầu mới về Quản lý kho tồn, KPI Quản trị viên, Đăng nhập khởi động, Đăng xuất, Đổi mật khẩu và định dạng Header đã hoàn tất 100%.
- **Quyết định kỹ thuật đã chốt:**
  - Thống nhất duy nhất .NET 8 LTS cho toàn bộ solution.
  - Tồn kho hiển thị được tính toán theo dữ liệu luân chuyển thực tế giữa nhập kho và hóa đơn xuất bán.
  - Vòng lặp WinForms chạy tuần tự giữa `LoginForm` và `MainForm`.

## 3. Hành động cụ thể tiếp theo cho phiên sau
1. Hoàn thiện tài liệu `PRD.md` và các sơ đồ phân tích nghiệp vụ nâng cao nếu có yêu cầu mới.
2. Đóng gói và phát hành tài liệu hướng dẫn vận hành hệ thống.
3. Rà soát nghiệm thu tổng thể hệ thống và chuẩn bị thuyết trình đồ án.
