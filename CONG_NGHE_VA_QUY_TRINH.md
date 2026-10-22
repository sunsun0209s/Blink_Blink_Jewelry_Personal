# CÔNG NGHỆ SỬ DỤNG VÀ QUY TRÌNH THIẾT KẾ DỰ ÁN

Tài liệu này trình bày toàn bộ công nghệ được lựa chọn cho dự án Blink Blink Jewelry, mô hình kiến trúc C4 và quy trình từng bước xây dựng hệ thống từ cốt lõi ra bên ngoài theo đúng sơ đồ thiết kế kiến trúc lớp (Class Diagram) và kiến trúc sạch (Clean Architecture).

---

## 1. CÔNG NGHỆ SỬ DỤNG

Hệ thống được xây dựng trên nền tảng .NET 8 (LTS) kết hợp kiến trúc Clean Architecture:

| Thành phần | Công nghệ lựa chọn | Vai trò trong hệ thống |
| :--- | :--- | :--- |
| **Nền tảng & Ngôn ngữ** | C# trên nền .NET 8 (LTS) | Ngôn ngữ lập trình chính cho toàn bộ backend, an toàn về kiểu dữ liệu và tối ưu hiệu năng dài hạn. |
| **Giao diện Quản trị (Desktop)** | Windows Forms (WinForms .NET 8) | Ứng dụng chạy trên máy trạm Windows dành cho **Admin** và **Nhân viên** (xử lý đơn hàng, quản lý danh mục sản phẩm, nhập kho, kiểm kê và tiếp nhận bảo hành). Thiết kế panel sang trọng, số liệu căn gióng chính xác theo chuẩn kim hoàn. |
| **Giao diện Khách hàng (Web)** | HTML5, CSS3, JavaScript (hoặc PHP nếu có) | Trang web mua sắm trực tuyến dành cho khách hàng tra cứu mẫu mã, giỏ hàng, đặt hàng và tra cứu phiếu bảo hành; giao tiếp với phần nghiệp vụ thông qua RESTful Web API. |
| **Hệ quản trị Cơ sở dữ liệu** | Microsoft SQL Server | Nơi lưu trữ toàn bộ dữ liệu kinh doanh của cửa hàng (sản phẩm, hóa đơn, khách hàng, phiếu nhập kho, phiếu kiểm kê, bảo hành), bảo đảm chuẩn hóa quan hệ 3NF. |
| **Tương tác CSDL** | ADO.NET thuần (`Microsoft.Data.SqlClient`) | Quản lý kết nối qua `DatabaseHelper`, thực thi câu lệnh SQL trực tiếp với tham số hóa `SqlParameter` an toàn tuyệt đối chống SQL Injection, hiệu năng cao và độc lập với EF Core Migrations. |
| **Triển khai Cơ sở dữ liệu** | Kịch bản SQL (`database/BlinkBlinkJewelry.sql`) | Khởi tạo toàn bộ database `BlinkBlinkJewelryDb`, 15 bảng quan hệ và nạp sẵn dữ liệu mẫu kim hoàn cao cấp thực tế trên SSMS (SQL Server Management Studio). |
| **Cơ chế quản lý phụ thuộc** | Dependency Injection (DI) | Cơ chế tiêm phụ thuộc có sẵn của .NET giúp kết nối các tầng với nhau thông qua hợp đồng (Interface), đảm bảo tính linh hoạt và dễ kiểm thử. |
| **Kiểm thử tự động** | xUnit | Công cụ viết mã kiểm thử tự động kiểm tra logic tính tiền hóa đơn, xét điều kiện và quy tắc nghiệp vụ. |

---

## 2. MÔ HÌNH KIẾN TRÚC C4 TRONG THIẾT KẾ HỆ THỐNG

Theo yêu cầu chuẩn hóa, hệ thống được phân rã và tài liệu hóa theo **Mô hình C4 (Context, Container, Component, Code)**:

1. **Mức 1 - Bối cảnh hệ thống (System Context):**
   - Chỉ ra ranh giới giữa Hệ thống Blink Blink Jewelry và các đối tượng bên ngoài:
     - **Admin & Nhân viên:** Tương tác trực tiếp qua Ứng dụng Desktop Windows Forms.
     - **Khách hàng:** Tương tác từ xa qua Cổng Web Khách hàng (HTML/CSS/JS) gọi RESTful Web API.
2. **Mức 2 - Vỏ chứa (Container Diagram):**
   - Phân rã hệ thống thành các khối ứng dụng:
     - **Desktop Admin App (WinForms .NET 8):** Chạy trên máy trạm cửa hàng.
     - **Customer Web Storefront (HTML/CSS/JS/PHP):** Chạy trên trình duyệt internet của khách hàng.
     - **Backend RESTful Web API (`BlinkBlink.Web`):** Cung cấp các endpoints cho Web Khách hàng.
     - **Shared Core Libraries (`Domain`, `Application`, `Infrastructure`):** Khối thư viện logic dùng chung.
     - **Database (SQL Server):** Lưu trữ dữ liệu tập trung.
3. **Mức 3 - Thành phần (Component Diagram):**
   - Bên trong ứng dụng Desktop WinForms: Gồm các UserControl nghiệp vụ (`OrdersView`, `ProductsView`, `WarrantyView`, `UsersView`, `ReportingView`) kết nối với các Use Case trong tầng Application.
   - Bên trong ứng dụng Web API: Gồm các RESTful Controller (`SanPhamController`, `BanHangController`, `BaoHanhController`, `PhieuNhapKhoController`, `ThongKeController`).
   - Bên trong tầng Application: Gồm các Use Case độc lập theo nguyên lý đơn trách nhiệm (Single Responsibility Principle).
4. **Mức 4 - Mã nguồn (Code / Class Diagram):**
   - Thể hiện chi tiết cấu trúc các lớp đối tượng, các thuộc tính, phương thức và mối quan hệ (1-n, kế thừa, hiện thực hóa interface) đã được định nghĩa trong sơ đồ kiến trúc lớp.

---

## 3. QUY TRÌNH THIẾT KẾ: TỪ ĐÂU ĐẾN ĐÂU?

Hệ thống được xây dựng theo phương pháp luận **Thiết kế hướng đối tượng từ trong ra ngoài (Domain-First / Inside-Out)** theo chuẩn kiến trúc sạch (Clean Architecture):

```
[Giai đoạn 1: Tầng Lõi]        Xây dựng Thực thể (Entities) & Quy tắc nghiệp vụ từ Class Diagram
           │
           ▼
[Giai đoạn 2: Tầng Cơ sở dữ liệu] Viết Script SQL (database/BlinkBlinkJewelry.sql) & DatabaseHelper ADO.NET
           │
           ▼
[Giai đoạn 3: Tầng Ứng dụng]    Hiện thực hóa các Ca Sử Dụng (Use Cases) độc lập
           │
           ▼
[Giai đoạn 4: Tầng Giao diện]   Ghép WinForms Admin (Desktop) & Web Khách hàng (HTML/CSS/JS) qua RESTful API
           │
           ▼
[Giai đoạn 5: Tầng Kiểm thử]    Viết mã kiểm thử xUnit bảo đảm tính đúng đắn và an toàn giao dịch
```

---

#### Giai đoạn 1: Xây dựng Tầng Lõi Nghiệp vụ (`BlinkBlink.Domain`)
Hiện thực hóa các lớp thực thể từ sơ đồ kiến trúc lớp (Class Diagram) thành mã C#:
1. **Lớp trừu tượng cơ sở (`NguoiDung.cs`):** Định nghĩa thuộc tính tài khoản chung (`MaNguoiDung`, `HoTen`, `SoDienThoai`, `TenDangNhap`, `MatKhau`, `MaVaiTro`, `TrangThai`) và các nghiệp vụ tự thân (`DangNhap`, `DoiMatKhau`, `CapNhatThongTin`, `KhoaTaiKhoan`).
2. **Các phân hệ thực thể chuẩn hóa:**
   - `Identity/`:
     - `VaiTro.cs`: Quản lý vai trò (ADMIN, SALE, WAREHOUSE, KHACHHANG).
     - `NhanVien.cs`: Kế thừa `NguoiDung`, quản lý nhân sự cửa hàng (`MaNhanVien`, `LuongCoBan`).
   - `Catalog/`: 
     - `DanhMuc.cs`: Quản lý phân loại trang sức (`MaDanhMuc`, `TenDanhMuc`, `MoTa`).
     - `SanPhamTrangSuc.cs`: Quản lý sản phẩm độc bản (`MaSanPham`, `TenSanPham`, `MaDanhMuc`, `ChatLieu`, `Size`, `TrangThai`, `GiaNhap`, `GiaBan`). Thông tin chất liệu lưu chuỗi string, không cần lưu lịch sử giá theo quy định nghiệp vụ.
   - `Orders/`: `HoaDon.cs`, `ChiTietHoaDon.cs` (tính tiền hàng, giảm giá, tổng thanh toán).
   - `Cart/`: `GioHang.cs`, `ChiTietGioHang.cs` (giỏ hàng trực tuyến của khách hàng).
   - `Customers/`: `KhachHang.cs` (kế thừa `NguoiDung`, quản lý khách hàng mua sắm: `MaKhachHang`, `NgaySinh`).
   - `Inventory/`: `PhieuNhapKho.cs`, `ChiTietNhapKho.cs`, `PhieuKiemKe.cs`, `ChiTietKiemKe.cs` (nhập kho và đối soát tồn thực tế vs sổ sách).
   - `Warranty/`: `PhieuBaoHanh.cs` (tiếp nhận sửa chữa, đánh bóng và chi phí bảo hành).

---

#### Giai đoạn 2: Hiện thực hóa Cơ sở dữ liệu (`BlinkBlink.Infrastructure`)
Triển khai cơ sở dữ liệu thuần SQL và tầng truy xuất dữ liệu bằng ADO.NET thuần:
1. **Kịch bản cơ sở dữ liệu `database/BlinkBlinkJewelry.sql`**:
   - Chứa câu lệnh DDL khởi tạo database `BlinkBlinkJewelryDb`, 15 bảng quan hệ, khóa chính, khóa ngoại, ràng buộc duy nhất.
   - Chứa câu lệnh DML nạp sẵn dữ liệu mẫu thực tế của ngành kim hoàn cao cấp (vai trò, nhân viên, khách hàng, 4 danh mục trang sức, 5 sản phẩm trang sức độc bản, hóa đơn, phiếu nhập kho, phiếu bảo hành).
   - Người dùng chỉ cần mở file trong SSMS (SQL Server Management Studio) và nhấn F5 (Execute) để triển khai.
2. **Lớp tiện ích `DatabaseHelper.cs`**:
   - Quản lý mở kết nối `SqlConnection` và cung cấp các hàm thực thi an toàn (`ExecuteNonQuery`, `ExecuteReader`, `ExecuteScalar`, `ExecuteDataTable`).
   - Tham số hóa 100% bằng `SqlParameter`, ngăn chặn triệt để SQL Injection.
3. **Các Repositories ADO.NET thuần**:
   - Triển khai toàn bộ 12 Repositories sử dụng `SqlCommand` và `SqlDataReader`, loại bỏ hoàn toàn phụ thuộc vào Entity Framework Core Migrations.
4. **Cấu hình chuỗi kết nối trong `src/BlinkBlink.Web/appsettings.json`**:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BlinkBlinkJewelryDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
   }
   ```

---

#### Giai đoạn 3: Hiện thực hóa Ca Sử Dụng Nghiệp Vụ (`BlinkBlink.Application`)
Đóng vai trò người điều phối nghiệp vụ (Use Cases), tiếp nhận DTO từ Presentation, gọi Domain Entity và Repository để xử lý:
1. **Phân hệ Người dùng & Phân quyền:**
   - `DangNhapUseCase`: Xác thực tài khoản người dùng, kiểm tra trạng thái hoạt động.
   - `DangKyKhachHangUseCase`: Tạo tài khoản khách hàng mới, kiểm tra trùng lặp.
   - `QuanLyNhanVienUseCase`: Thêm mới nhân viên, cập nhật mức lương cơ bản, phân vai trò.
2. **Phân hệ Bán hàng & Đơn hàng:**
   - `DatHangUseCase`: Tạo đơn đặt hàng, kiểm tra giỏ hàng, trừ kho sản phẩm.
   - `XuLyDonHangUseCase`: Duyệt đơn, cập nhật trạng thái đơn hàng (Đang giao, Đã giao, Hủy).
3. **Phân hệ Quản lý Sản phẩm & Tồn kho:**
   - `QuanLySanPhamUseCase`: Thêm mới, cập nhật giá bán niêm yết và thông tin sản phẩm.
   - `XacNhanNhapKhoUseCase`: Lập phiếu nhập kho từ nhà chế tác, cập nhật số lượng tồn kho.
4. **Phân hệ Bảo hành & Dịch vụ:**
   - `TiepNhanBaoHanhUseCase`: Lập phiếu tiếp nhận bảo hành trang sức, ghi nhận tình trạng hư hỏng.
   - `XuLyBaoHanhUseCase`: Cập nhật tiến độ xử lý bảo hành, chi phí sửa chữa và ngày bàn giao.
5. **Phân hệ Báo cáo & Thống kê:**
   - `ThongKeDoanhThuUseCase`: Tổng hợp doanh số bán hàng theo mốc thời gian.
   - `ThongKeKhoHangUseCase`: Báo cáo hàng tồn, chênh lệch kiểm kê và sản phẩm bán chạy.

---

#### Giai đoạn 4: Xây dựng Giao diện Người dùng (Presentation)
1. **Ứng dụng Desktop Quản trị WinForms (`BlinkBlink.Admin`):**
   - Dành cho **Admin** và **Nhân viên**.
   - `MainForm.cs`: Vỏ điều hướng trung tâm chuyển đổi các phân hệ trên `panelContent`.
   - Các UserControls nghiệp vụ độc lập:
     - `OrdersView.cs`: Màn hình quản lý đơn hàng và hóa đơn bán hàng.
     - `ProductsView.cs`: Màn hình quản lý danh mục sản phẩm trang sức và giá bán.
     - `WarrantyView.cs`: Màn hình tiếp nhận và bàn giao phiếu bảo hành dịch vụ.
     - `UsersView.cs`: Màn hình quản lý nhân viên và khách hàng.
     - `ReportingView.cs`: Màn hình báo cáo doanh thu và tình trạng tồn kho kim hoàn.
2. **Cổng Web Khách hàng (`BlinkBlink.Web`):**
   - Dành cho khách hàng mua sắm, giao tiếp với backend qua RESTful Web API Controllers:
     - `SanPhamController.cs`: Cung cấp API duyệt bộ sưu tập trang sức, lọc theo danh mục, chất liệu, size.
     - `BanHangController.cs`: Cung cấp API đặt hàng online, tra cứu lịch sử mua sắm.
     - `BaoHanhController.cs`: Cung cấp API tra cứu tiến độ bảo hành theo mã số phiếu.
     - Giao diện người dùng xây dựng bằng HTML5, CSS3, JavaScript (hoặc PHP nếu có).

---

#### Giai đoạn 5: Kiểm thử Tự động & Tinh chỉnh (`tests/`)
1. **Kiểm thử đơn vị (`BlinkBlink.UnitTests`):** Kiểm tra tính toán tổng tiền đơn hàng, chiết khấu, nghiệp vụ tiếp nhận bảo hành và đối soát tồn kho qua Mock Repository độc lập.
2. **Kiểm thử tích hợp (`BlinkBlink.IntegrationTests`):** Kiểm tra tích hợp luồng nghiệp vụ trên hệ thống.
