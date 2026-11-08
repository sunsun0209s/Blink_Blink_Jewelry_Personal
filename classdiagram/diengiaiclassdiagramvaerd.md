# TÀI LIỆU DIỄN GIẢI CHI TIẾT SƠ ĐỒ CLASS DIAGRAM VÀ ERD
## HỆ THỐNG QUẢN LÝ BÁN VÀ KINH DOANH TRANG SỨC BLINK BLINK JEWELRY

---

## LỜI NÓI ĐẦU VÀ MỤC ĐÍCH TÀI LIỆU
Tài liệu này được biên soạn nhằm cung cấp phần thuyết minh và diễn giải chi tiết cho các hình ảnh sơ đồ thiết kế trong tiểu luận/khóa luận, bao gồm:
1. **Sơ đồ thực thể quan hệ (Entity Relationship Diagram - ERD)** mô hình hóa cơ sở dữ liệu quan hệ của hệ thống.
2. **Sơ đồ lớp chi tiết (Class Diagram)** được xây dựng theo kiến trúc đa tầng sạch (Clean Architecture) cho từng phân hệ nghiệp vụ chính của hệ thống cửa hàng trang sức Blink Blink.

Mục tiêu của tài liệu là làm rõ:
- **Vai trò và trách nhiệm** của từng thực thể, từng lớp đối tượng trong hệ thống.
- **Bản chất các đường nối quan hệ** (Kế thừa, Hiện thực hóa giao diện, Chứa đựng/Hợp thành, Phụ thuộc, Liên kết).
- **Lý do kỹ thuật và nghiệp vụ** đằng sau các quyết định thiết kế (Tại sao cần tách bảng, tại sao sử dụng mô hình kế thừa Table-Per-Type, tại sao cần tầng Use Case trung gian,...).

---

# PHẦN 1: DIỄN GIẢI SƠ ĐỒ THỰC THỂ QUAN HỆ (ERD)

## 1.1. Triết lý thiết kế và Chiến lược lưu trữ CSDL
Cơ sở dữ liệu của hệ thống Blink Blink Jewelry được thiết kế trên hệ quản trị cơ sở dữ liệu quan hệ **SQL Server**, tuân thủ nghiêm ngặt các nguyên tắc chuẩn hóa dữ liệu (Chuẩn 3NF), đảm bảo tính toàn vẹn tham chiếu và tối ưu hóa cho ngành bán lẻ vàng bạc đá quý đặc thù:
- **Chiến lược Table-Per-Type (TPT):** Phân rã dữ liệu người dùng thành bảng cha `NguoiDung` và hai bảng con `NhanVien`, `KhachHang`.
- **Snapshot dữ liệu đơn hàng:** Lưu giữ đơn giá và thông tin bán hàng tại thời điểm phát sinh giao dịch, ngăn chặn sự biến động giá sản phẩm làm sai lệch doanh thu lịch sử.
- **Ràng buộc khóa chính phức hợp (Composite Primary Key):** Áp dụng tại tất cả các bảng chi tiết (`ChiTietHoaDon`, `ChiTietGioHang`, `ChiTietNhapKho`, `ChiTietKiemKe`) nhằm triệt tiêu dữ liệu trùng lặp và thể hiện chính xác mối quan hệ nhiều-nhiều.

---

## 1.2. Diễn giải chi tiết từng Thực thể trong ERD

### 1. Phân hệ Kiểm soát truy cập & Phân quyền (TPT Pattern)
* **`dbo.VaiTro`:**
  - *Vai trò:* Định nghĩa các nhóm quyền hạn trong hệ thống (như Admin quản trị, Quản lý kho, Thu ngân bán hàng, Khách hàng thành viên).
  - *Thuộc tính chính:* `MaVaiTro` (PK), `TenVaiTro`, `MoTa`.
  - *Tại sao cần:* Giúp hệ thống phân quyền mềm dẻo. Khi thêm một chức vụ mới, chỉ cần khai báo một dòng trong bảng này mà không cần sửa cấu trúc bảng cơ sở dữ liệu.
* **`dbo.NguoiDung` (Bảng cha trong mô hình TPT):**
  - *Vai trò:* Lưu trữ các thông tin định danh và xác thực cơ bản nhất của mọi chủ thể truy cập hệ thống.
  - *Thuộc tính chính:* `MaNguoiDung` (PK), `HoTen`, `SoDienThoai`, `TenDangNhap` (UNIQUE), `MatKhau`, `MaVaiTro` (FK), `TrangThai`.
  - *Tại sao cần:* Là bảng trừu tượng gom các thuộc tính dùng chung. Mọi người dùng dù là giám đốc, thủ kho hay khách mua hàng online đều cần tài khoản đăng nhập và họ tên liên hệ.
* **`dbo.NhanVien` (Bảng con TPT):**
  - *Vai trò:* Lưu thông tin chuyên biệt dành riêng cho nội bộ nhân sự làm việc tại chuỗi cửa hàng.
  - *Thuộc tính chính:* `MaNguoiDung` (PK, FK liên kết 1-1 tới `NguoiDung`), `MaNhanVien` (UNIQUE), `LuongCoBan`.
  - *Tại sao cần thiết kế TPT:* Tránh việc đưa cột nhạy cảm như `LuongCoBan` vào bảng chung `NguoiDung` (nơi chứa cả tài khoản khách hàng). Khóa chính của bảng con trùng với khóa ngoại tham chiếu đến bảng cha, đảm bảo quan hệ 1-1 tuyệt đối.
* **`dbo.KhachHang` (Bảng con TPT):**
  - *Vai trò:* Lưu thông tin dành riêng cho khách hàng mua sắm, tích điểm thành viên.
  - *Thuộc tính chính:* `MaNguoiDung` (PK, FK liên kết 1-1 tới `NguoiDung`), `MaKhachHang` (UNIQUE), `NgaySinh`.
  - *Tại sao cần:* Trường `NgaySinh` phục vụ cho các chính sách chăm sóc khách hàng đặc thù của ngành trang sức (tặng voucher giảm giá ngày sinh nhật).

---

### 2. Phân hệ Danh mục & Sản phẩm trang sức
* **`dbo.DanhMuc`:**
  - *Vai trò:* Phân loại dòng trang sức theo cấu trúc phân cấp công năng (Ví dụ: Nhẫn kim cương, Dây chuyền bạc, Bông tai vàng, Vòng tay phong thủy).
  - *Thuộc tính chính:* `MaDanhMuc` (PK), `TenDanhMuc`, `MoTa`.
  - *Tại sao cần:* Giúp khách hàng và nhân viên lọc, tra cứu sản phẩm nhanh chóng; phục vụ việc hiển thị cây danh mục trên website.
* **`dbo.NhomSanPham`:**
  - *Vai trò:* Gom các sản phẩm thành từng bộ sưu tập hoặc chiến dịch tiếp thị (Ví dụ: Bộ sưu tập Mùa cưới 2026, Trang sức phong thủy, Bộ sưu tập Valentine).
  - *Thuộc tính chính:* `MaNhom` (PK), `TenNhom`, `MoTa`.
  - *Tại sao cần:* Một sản phẩm thuộc danh mục "Nhẫn", nhưng có thể thuộc bộ sưu tập "Mùa Cưới". Việc tách bảng nhóm giúp linh hoạt triển khai các chiến dịch Marketing theo mùa mà không làm xáo trộn danh mục chủng loại gốc.
* **`dbo.SanPhamTrangSuc`:**
  - *Vai trò:* Thực thể trung tâm lưu trữ thông tin sản phẩm trang sức đang kinh doanh.
  - *Thuộc tính chính:* `MaSanPham` (PK), `TenSanPham`, `MaDanhMuc` (FK), `MaNhom` (FK), `ChatLieu` (Vàng 18K, Bạc 925, Bạch kim,...), `Size` (Ni tay: 10, 12, 14), `GiaNhap`, `GiaBan`, `TrangThai`, `HinhAnh`, `PhanTramGiamGia`, `SoLuongTon`.
  - *Quyết định thiết kế cốt lõi:* Thuộc tính chất liệu được lưu dạng chuỗi trực tiếp (`NVARCHAR`) theo quy ước nghiệp vụ của hệ thống, không lưu bảng lịch sử biến động giá vàng phức tạp nhằm tinh giản kiến trúc nhưng vẫn đáp ứng hoàn hảo yêu cầu bán buôn, bán lẻ.

---

### 3. Phân hệ Giỏ hàng trực tuyến (E-Commerce Shopping Cart)
* **`dbo.GioHang`:**
  - *Vai trò:* Đại diện cho phiên mua sắm trực tuyến của từng khách hàng.
  - *Thuộc tính chính:* `MaGioHang` (PK), `MaKhachHang` (FK, UNIQUE), `SoLuong`.
  - *Tại sao cần:* Ràng buộc `UNIQUE` trên cột `MaKhachHang` đảm bảo mỗi khách hàng tại một thời điểm chỉ sở hữu duy nhất 1 giỏ hàng hoạt động.
* **`dbo.ChiTietGioHang`:**
  - *Vai trò:* Bảng liên kết trung gian lưu từng món hàng cụ thể được thêm vào giỏ.
  - *Thuộc tính chính:* `MaGioHang` (PK, FK), `MaSanPham` (PK, FK), `SoLuongSanPham`.
  - *Tại sao cần:* Cặp khóa chính phức hợp `(MaGioHang, MaSanPham)` đảm bảo một sản phẩm chỉ xuất hiện một dòng trong giỏ, nếu khách ấn thêm thì cập nhật trường `SoLuongSanPham` thay vì tạo dòng mới.

---

### 4. Phân hệ Đặt hàng & Bán hàng (Omni-Channel Sales)
* **`dbo.HoaDon`:**
  - *Vai trò:* Ghi nhận thông tin chung của giao dịch thanh toán (hỗ trợ cả đơn đặt hàng trực tuyến từ Web và hóa đơn bán trực tiếp tại quầy POS của nhân viên thu ngân).
  - *Thuộc tính chính:* `MaHoaDon` (PK), `NgayLap`, `MaKhachHang` (FK), `MaNhanVien` (FK, NULLable), `DiaChiGiaoHang`, `SoDienThoaiNhan`, `PhuongThucThanhToan`, `TrangThaiDonHang`, `TongTienHang`, `TienGiamGia`, `TongThanhToan`, `GhiChu`.
  - *Tại sao cần cột `MaNhanVien` cho phép NULL:* Khi khách hàng tự đặt hàng trên website, đơn hàng chưa có nhân viên phụ trách trực tiếp (`MaNhanVien` mang giá trị NULL). Khi nhân viên tại cửa hàng lập hóa đơn trên màn hình WinForms POS, `MaNhanVien` được gán chính xác mã của thu ngân lập hóa đơn.
* **`dbo.ChiTietHoaDon`:**
  - *Vai trò:* Lưu chi tiết từng mã sản phẩm, số lượng và giá bán trong hóa đơn.
  - *Thuộc tính chính:* `SoHoaDon` (PK, FK), `MaSanPham` (PK, FK), `DonGia`, `SoLuong`.
  - *Tại sao cần trường `DonGia`:* Đây là kỹ thuật snapshot giá. Giá bán tại bảng `SanPhamTrangSuc` có thể tăng giảm theo thời gian, nhưng đơn giá đã bán trong hóa đơn phải được cố định vĩnh viễn để bảo đảm doanh thu kế toán trong quá khứ không bị thay đổi.

---

### 5. Phân hệ Quản lý kho hàng & Kiểm kê
* **`dbo.PhieuNhapKho` & `dbo.ChiTietNhapKho`:**
  - *Vai trò:* Quản lý các đợt nhập hàng từ nhà cung cấp kim hoàn vào kho của cửa hàng.
  - *Thuộc tính chính:* `PhieuNhapKho` (`MaPhieuNhap`, `NgayNhap`, `NhaCungCap`, `MaNhanVien`, `TongSoSanPham`, `TongGiaTri`), `ChiTietNhapKho` (`MaPhieuNhap`, `MaSanPham`, `SoLuong`, `GiaVonNhap`, `SoChungTu`).
  - *Tại sao cần:* Cho phép theo dõi nguồn gốc xuất xứ, chứng từ nhập kho và ghi nhận giá vốn thực tế của từng lô trang sức làm căn cứ tính lợi nhuận.
* **`dbo.PhieuKiemKe` & `dbo.ChiTietKiemKe`:**
  - *Vai trò:* Quản lý quy trình kiểm tra số lượng thực tế tại tủ/két so với phần mềm kế toán.
  - *Thuộc tính chính:* `PhieuKiemKe` (`MaPhieuKiemKe`, `NgayKiemKe`, `ViTriKiemKe`, `TrangThai`, `NguoiTao`, `NguoiDuyet`, `NgayDuyet`, `GhiChu`), `ChiTietKiemKe` (`MaPhieuKiemKe`, `MaSanPham`, `TonHeThong`, `TonThucTe`, `ChenhLech`, `LyDoChenhLech`).
  - *Tại sao cần:* Trang sức là mặt hàng giá trị rất cao. Bảng kiểm kê bắt buộc phải ghi rõ độ lệch (`ChenhLech`) và diễn giải nguyên nhân (`LyDoChenhLech`) như hao hụt, rơi rớt hay thất thoát để trình quản lý phê duyệt (`NguoiDuyet`).

---

### 6. Phân hệ Dịch vụ & Bảo hành trang sức
* **`dbo.PhieuBaoHanh`:**
  - *Vai trò:* Tiếp nhận, theo dõi tiến độ và thanh toán chi phí cho các dịch vụ hậu mãi (làm mới, gắn đá, thâu/nới ni nhẫn, hàn mối nối).
  - *Thuộc tính chính:* `SoPhieuBH` (PK), `MaKhachHang` (FK), `MaSanPham` (FK), `LyDoBaoHanh`, `TinhTrangTiepNhan`, `GhiChu`, `ChiPhiUocTinh`, `ChiPhiThucTe`, `ChiPhiBaoHanh`, `TrangThaiXuLy`, `SoDienThoai`, `NgayTiepNhan`, `NgayHenTra`.
  - *Tại sao cần:* Giúp khách hàng kiểm tra tình trạng sửa chữa sản phẩm và cửa hàng kiểm soát quy trình xử lý của thợ kim hoàn cũng như ghi nhận chi phí phát sinh.

---

## 1.3. Bảng tổng hợp các Đường nối Quan hệ trong ERD

| Bảng nguồn (Parent / 1) | Bảng đích (Child / N) | Quan hệ & Lực lượng | Khóa ngoại (FK) | Lý do nghiệp vụ cần quan hệ này |
| :--- | :--- | :---: | :--- | :--- |
| `dbo.DanhMuc` | `dbo.SanPhamTrangSuc` | 1..1 --- 0..* | `MaDanhMuc` | Một danh mục chứa nhiều sản phẩm; một sản phẩm bắt buộc thuộc một danh mục cụ thể. |
| `dbo.NhomSanPham` | `dbo.SanPhamTrangSuc` | 0..1 --- 0..* | `MaNhom` | Một nhóm/bộ sưu tập có nhiều sản phẩm; sản phẩm có thể chưa thuộc bộ sưu tập nào (cho phép NULL). |
| `dbo.VaiTro` | `dbo.NguoiDung` | 1..1 --- 0..* | `MaVaiTro` | Một vai trò được cấp cho nhiều tài khoản người dùng; mỗi tài khoản có đúng 1 vai trò chủ đạo. |
| `dbo.NguoiDung` | `dbo.NhanVien` | 1..1 --- 1..1 | `MaNguoiDung` (PK=FK) | Kế thừa TPT: Nhân viên chính là một người dùng, chia sẻ cùng khóa chính và tài khoản đăng nhập. |
| `dbo.NguoiDung` | `dbo.KhachHang` | 1..1 --- 1..1 | `MaNguoiDung` (PK=FK) | Kế thừa TPT: Khách hàng là người dùng hệ thống, sở hữu ngày sinh và mã tích điểm thành viên. |
| `dbo.KhachHang` | `dbo.GioHang` | 1..1 --- 0..1 | `MaKhachHang` (UNIQUE) | Mỗi khách hàng có tối đa 1 giỏ hàng trực tuyến đang hoạt động. |
| `dbo.GioHang` | `dbo.ChiTietGioHang` | 1..1 --- 0..* | `MaGioHang` | Giỏ hàng chứa nhiều sản phẩm đang chọn; xóa giỏ hàng sẽ xóa các chi tiết giỏ hàng tương ứng. |
| `dbo.SanPhamTrangSuc`| `dbo.ChiTietGioHang` | 1..1 --- 0..* | `MaSanPham` | Một sản phẩm có thể được chọn trong giỏ của nhiều khách hàng khác nhau. |
| `dbo.KhachHang` | `dbo.HoaDon` | 1..1 --- 0..* | `MaKhachHang` | Khách hàng thực hiện các giao dịch mua sắm; một khách hàng có thể có nhiều hóa đơn. |
| `dbo.NhanVien` | `dbo.HoaDon` | 0..1 --- 0..* | `MaNhanVien` | Đơn POS quầy bắt buộc có nhân viên thu ngân lập; đơn Online thì nhân viên là NULL trước khi duyệt. |
| `dbo.HoaDon` | `dbo.ChiTietHoaDon` | 1..1 --- 1..* | `SoHoaDon` | Một hóa đơn bắt buộc phải có ít nhất 1 dòng chi tiết sản phẩm được bán ra. |
| `dbo.SanPhamTrangSuc`| `dbo.ChiTietHoaDon` | 1..1 --- 0..* | `MaSanPham` | Sản phẩm xuất hiện trên các hóa đơn đã bán cho khách. |
| `dbo.NhanVien` | `dbo.PhieuNhapKho` | 1..1 --- 0..* | `MaNhanVien` | Thủ kho chịu trách nhiệm ký nhận và lập phiếu nhập kho. |
| `dbo.PhieuNhapKho` | `dbo.ChiTietNhapKho` | 1..1 --- 1..* | `MaPhieuNhap` | Phiếu nhập kho gồm nhiều sản phẩm trang sức được bàn giao từ nhà cung cấp. |
| `dbo.SanPhamTrangSuc`| `dbo.ChiTietNhapKho` | 1..1 --- 0..* | `MaSanPham` | Sản phẩm được nhập bổ sung số lượng tồn kho. |
| `dbo.NhanVien` / `NguoiDung`| `dbo.PhieuKiemKe` | 1..1 --- 0..* | `NguoiTao`, `NguoiDuyet` | Thủ kho tạo phiếu kiểm kê và Quản lý/Ban giám đốc ký duyệt biên bản. |
| `dbo.PhieuKiemKe` | `dbo.ChiTietKiemKe` | 1..1 --- 1..* | `MaPhieuKiemKe` | Một đợt kiểm kê rà soát danh sách nhiều sản phẩm có trong khu vực tủ/két. |
| `dbo.SanPhamTrangSuc`| `dbo.ChiTietKiemKe` | 1..1 --- 0..* | `MaSanPham` | Mỗi sản phẩm trong đợt kiểm kê được đối chiếu giữa số lượng sổ sách và thực tế. |
| `dbo.KhachHang` | `dbo.PhieuBaoHanh` | 1..1 --- 0..* | `MaKhachHang` | Khách hàng mang trang sức đã mua đến yêu cầu bảo dưỡng, sửa chữa. |
| `dbo.SanPhamTrangSuc`| `dbo.PhieuBaoHanh` | 1..1 --- 0..* | `MaSanPham` | Sản phẩm cụ thể cần được thực hiện dịch vụ kim hoàn. |

---

# PHẦN 2: TỔNG QUAN KIẾN TRÚC CLASS DIAGRAM THEO CLEAN ARCHITECTURE

Các sơ đồ lớp của hệ thống Blink Blink Jewelry được tổ chức theo tiêu chuẩn **Clean Architecture** của Robert C. Martin (Uncle Bob), chia thành 4 lớp rõ rệt từ trong ra ngoài:

```
[Frameworks & Drivers (Infrastructure)]
        |
        v
[Interface Adapters (Controllers, Presenters, DTOs)]
        |
        v
[Application (Use Cases & Repository Interfaces)]
        |
        v
[Domain Entities (Thực thể lõi)]
```

### Ý nghĩa của từng tầng:
1. **Domain Entities:** Chứa các đối tượng nghiệp vụ thuần túy của ngành trang sức (`SanPhamTrangSuc`, `HoaDon`, `PhieuBaoHanh`,...). Tầng này độc lập hoàn toàn, không phụ thuộc vào bất kỳ framework, UI hay CSDL nào.
2. **Application (Use Cases & Interfaces):** Chứa các ca sử dụng đặc thù của phần mềm (như `DatHangUseCase`, `XacNhanNhapKhoUseCase`,...). Tầng này định nghĩa các cổng giao tiếp trừu tượng (`IHoaDonRepository`, `ISanPhamRepository`) tuân thủ nguyên lý đảo ngược phụ thuộc (Dependency Inversion Principle - DIP).
3. **Interface Adapters (Presentation):** Bao gồm các bộ điều khiển API (`Controllers`), các đối tượng truyền dữ liệu (`DTOs`) đóng vai trò trung gian chuyển đổi dữ liệu giữa màn hình giao diện (WinForms, Web) và các Use Cases.
4. **Frameworks & Drivers (Infrastructure):** Nơi hiện thực hóa cụ thể việc giao tiếp với cơ sở dữ liệu (`Repository Implementation`), sử dụng ADO.NET hoặc Entity Framework Core thông qua lớp tiện ích `DatabaseHelper`.

### Các quy ước ký hiệu đường nối trong Class Diagram:
- **Đường nét đứt có mũi tên tam giác rỗng (`Realization / Implementation`):** Lớp cụ thể ở tầng Infrastructure hiện thực hóa một Interface ở tầng Application (Ví dụ: `HoaDonRepository` hiện thực hóa `IHoaDonRepository`).
- **Đường nét liền có hình thoi rỗng hoặc đặc (`Aggregation / Composition`):** Thể hiện quan hệ sở hữu.
  - Giữa các Entity: `HoaDon` chứa danh sách `ChiTietHoaDon`.
  - Giữa Controller và Use Case: Controller giữ instance của Use Case để điều phối luồng.
  - Giữa Use Case và Interface Repository: Use Case giữ thuộc tính tham chiếu tới Repository Interface để gọi truy vấn dữ liệu.
- **Đường nét đứt có mũi tên hở (`Dependency`):** Thể hiện lớp này sử dụng lớp kia làm tham số đầu vào hoặc kiểu dữ liệu trả về (Ví dụ: Use Case nhận tham số là `DatHangDTO`).
- **Đường nét liền có mũi tên tam giác rỗng kín (`Generalization / Inheritance`):** Thể hiện sự kế thừa giữa lớp con và lớp cha (Ví dụ: `KhachHang` kế thừa từ lớp trừu tượng `NguoiDung`).

---

# PHẦN 3: DIỄN GIẢI CHI TIẾT CÁC PHÂN HỆ CLASS DIAGRAM

## 3.1. Phân hệ Quản lý Người dùng và Phân quyền

### 1. Danh sách các Lớp và Thành phần
* **Tầng Domain:**
  - `<<abstract>> NguoiDung`: Lớp cơ sở trừu tượng chứa các trường chung (`_maNguoiDung`, `_hoTen`, `_soDienThoai`, `_tenDangNhap`, `_matKhau`, `_maVaiTro`, `_trangThai`) và các phương thức xử lý tài khoản (`DangNhap`, `DoiMatKhau`, `CapNhatThongTin`, `KhoaTaiKhoan`).
  - `KhachHang`: Lớp dẫn xuất mở rộng thêm thuộc tính `_maKhachHang`, `_ngaySinh`.
  - `NhanVien`: Lớp dẫn xuất mở rộng thêm thuộc tính `_maNhanVien`, `_luongCoBan`.
  - `VaiTro`: Thực thể quyền hạn với các phương thức quản lý đặc quyền (`ThemQuyen`, `XoaQuyen`).
* **Tầng Application:**
  - `DangNhapUseCase`: Xử lý xác thực tài khoản, kiểm tra mật khẩu đã mã hóa và trạng thái hoạt động của tài khoản.
  - `DangKyKhachHangUseCase`: Xử lý nghiệp vụ tạo tài khoản khách hàng mới trên website, đồng thời hỗ trợ liên kết các hóa đơn khách vãng lai đã mua trước đó thông qua số điện thoại.
  - `QuanLyKhachHangUseCase`: Thực thi cập nhật hồ sơ khách hàng, khóa tài khoản hoặc xóa khách hàng (kèm kiểm tra ràng buộc lịch sử mua sắm).
  - `QuanLyNhanVienUseCase`: Nghiệp vụ dành cho ban quản trị tạo hồ sơ nhân viên mới, gán vai trò và cập nhật bảng lương.
  - Các Repository Interfaces: `INguoiDungRepository`, `IKhachHangRepository`, `INhanVienRepository`, `IVaiTroRepository`.
* **Tầng Presentation & Infrastructure:**
  - Controllers: `XacThucController`, `KhachHangController`, `NhanVienController`.
  - DTOs: `DangNhapDTO`, `DangKyKhachHangDTO`, `TaoNhanVienDTO`, `CapNhatKhachHangDTO`.
  - Repositories: `NguoiDungRepository`, `NhanVienRepository`, `KhachHangRepository`, `VaiTroRepository`.

### 2. Phân tích các Đường nối Quan hệ và Lý do thiết kế
1. **`KhachHang` và `NhanVien` kế thừa (`Extends`) `NguoiDung`:**
   - *Bản chất:* Kế thừa trong lập trình hướng đối tượng (OOP Inheritance).
   - *Lý do:* Tránh lặp lại mã nguồn cho các thuộc tính đăng nhập, họ tên, số điện thoại. Đảm bảo tính đa hình (Polymorphism): tầng xác thực chỉ cần thao tác với đối tượng trừu tượng `NguoiDung` mà không cần quan tâm người đang đăng nhập là nhân viên thu ngân hay khách hàng.
2. **`VaiTro` liên kết với `NguoiDung`:**
   - *Bản chất:* Quan hệ kết tập (Aggregation).
   - *Lý do:* Mỗi đối tượng người dùng bắt buộc phải gắn liền với một vai trò cụ thể để hệ thống thực hiện phân quyền truy cập chức năng.
3. **Các Controller liên kết Use Cases (`Composition`):**
   - *Bản chất:* Bộ điều khiển chứa Use Case.
   - *Lý do:* Controller nhận request từ Web API hoặc sự kiện từ WinForms, đóng gói tham số vào DTO và ủy quyền việc xử lý nghiệp vụ cho Use Case tương ứng.
4. **Các Use Cases liên kết Repository Interfaces (`Dependency Injection`):**
   - *Bản chất:* Use Case giữ biến tham chiếu đến Interface.
   - *Lý do:* Tuân thủ nguyên lý Đảo ngược phụ thuộc (DIP). Use Case không phụ thuộc vào `NguoiDungRepository` cụ thể của SQL Server, giúp dễ dàng viết mã kiểm thử (Mocking Unit Test) mà không cần kết nối cơ sở dữ liệu thật.
5. **Các Repository cụ thể hiện thực hóa Interface (`Implements`):**
   - *Bản chất:* Realization (nét đứt tam giác rỗng).
   - *Lý do:* Tầng Infrastructure cam kết thực hiện đầy đủ các hợp đồng phương thức do tầng Application đưa ra bằng các câu lệnh truy vấn SQL Server thông qua `DatabaseHelper`.

---

## 3.2. Phân hệ Quản lý Sản phẩm và Kho hàng

### 1. Danh sách các Lớp và Thành phần
* **Tầng Domain:**
  - `SanPhamTrangSuc`: Thực thể chứa đầy đủ quy tắc nghiệp vụ của trang sức (kiểm tra tồn kho `KiemTraTonKho()`, cập nhật trạng thái kinh doanh `CapNhatTrangThai()`,...).
  - `DanhMuc` & `NhomSanPham`: Các thực thể phân nhóm trang sức.
  - `PhieuNhapKho` & `ChiTietNhapKho`: Quản lý quy trình nhập hàng, tính giá trị lô hàng `ThemChiTiet()`, xác nhận nhập kho và hủy phiếu.
  - `PhieuKiemKe` & `ChiTietKiemKe`: Quản lý biên bản kiểm kê, tính toán chênh lệch tồn sổ sách so với thực tế và ghi nhận lý do thất thoát.
* **Tầng Application:**
  - `QuanLySanPhamUseCase`: Thêm sản phẩm mới, cập nhật giá bán, sửa đổi kích thước/hình ảnh.
  - `XacNhanNhapKhoUseCase`: Thực hiện giao dịch nhập kho: vừa ghi nhận phiếu nhập vừa cộng dồn số lượng tồn vào thực thể sản phẩm trong kho.
  - Interfaces: `ISanPhamRepository`, `IDanhMucRepository`, `IPhieuNhapKhoRepository`, `IPhieuKiemKeRepository`.
* **Tầng Presentation & Infrastructure:**
  - Controllers: `SanPhamController`, `PhieuNhapKhoController`.
  - DTOs: `SanPhamDTO`, `DoiGiaDTO`, `CapNhatSanPhamDTO`, `NhapKhoDTO`, `ChiTietNhapKhoDTO`.
  - Repositories: `SanPhamRepository`, `DanhMucRepository`, `PhieuNhapKhoRepositoryImpl`, `PhieuKiemKeRepository`.

### 2. Phân tích các Đường nối Quan hệ và Lý do thiết kế
1. **`ChiTietNhapKho` liên kết `PhieuNhapKho` & `ChiTietKiemKe` liên kết `PhieuKiemKe` (`Composition - 1..*`):**
   - *Bản chất:* Quan hệ hợp thành sống còn (Composition).
   - *Lý do:* Chi tiết dòng hàng không thể tồn tại độc lập nếu thiếu phiếu nhập hoặc phiếu kiểm kê. Nếu một phiếu bị xóa bỏ, toàn bộ các chi tiết thuộc phiếu đó phải bị hủy đồng thời để tránh dữ liệu mồ côi.
2. **`ChiTietNhapKho` và `ChiTietKiemKe` liên kết `SanPhamTrangSuc` (`Association - N:1`):**
   - *Bản chất:* Liên kết chỉ mục.
   - *Lý do:* Mỗi dòng chi tiết nhập hoặc kiểm kê cần chỉ định rõ sản phẩm trang sức nào đang được thao tác để cập nhật đúng trường tồn kho `SoLuongTon`.
3. **`DanhMuc` và `NhomSanPham` liên kết `SanPhamTrangSuc` (`1..*`):**
   - *Bản chất:* Kết hợp phân loại.
   - *Lý do:* Cho phép một danh mục chứa danh sách tập hợp các sản phẩm trang sức (`LayDanhSachSanPham()`).
4. **`XacNhanNhapKhoUseCase` phụ thuộc cả `IPhieuNhapKhoRepository` và `ISanPhamRepository`:**
   - *Bản chất:* Điều phối giao dịch đa nghiệp vụ.
   - *Lý do:* Việc nhập kho không chỉ đơn thuần là lưu phiếu, mà phải thực hiện một giao dịch kép (Transaction): Lưu phiếu nhập kho và đồng thời tăng số lượng tồn của các sản phẩm tương ứng trong bảng sản phẩm.

---

## 3.3. Phân hệ Quản lý Bán hàng (POS & Online E-Commerce)

### 1. Danh sách các Lớp và Thành phần
* **Tầng Domain:**
  - `HoaDon`: Thực thể trung tâm quản lý tổng tiền, tiền giảm giá, trạng thái đơn hàng và các hành vi nghiệp vụ như tính tổng giá trị (`TinhTongTien()`), cập nhật trạng thái giao dịch (`CapNhatTrangThai()`), hủy hóa đơn (`HuyHoaDon()`).
  - `ChiTietHoaDon`: Chứa thông tin số lượng, đơn giá xuất bán và phương thức tính thành tiền của từng món trang sức (`TinhThanhTien()`).
  - `GioHang`: Thực thể đại diện giỏ mua sắm trên web, có các phương thức dọn dẹp (`DonDepGioHang()`), thêm/bớt món hàng (`ThemSanPham()`, `XoaSanPham()`).
  - `ChiTietGioHang`: Dòng mặt hàng chọn mua kèm số lượng (`TangSoLuong()`, `GiamSoLuong()`).
* **Tầng Application:**
  - `DatHangUseCase`: Tiếp nhận giỏ hàng từ khách hàng trực tuyến, kiểm tra số lượng tồn kho của từng món hàng, tạo hóa đơn mới ở trạng thái chờ duyệt và làm sạch giỏ hàng.
  - `XuLyDonHangUseCase`: Dành cho nhân viên cửa hàng duyệt đơn, chuyển trạng thái đóng gói, giao hàng, hoặc hủy đơn hàng hoàn lại tồn kho.
  - `BanHangOfflineUseCase`: Nghiệp vụ chuyên biệt phục vụ màn hình bán hàng WinForms POS tại quầy. Lập hóa đơn tức thì, nhận tiền mặt/chuyển khoản và trừ kho ngay lập tức.
  - Interfaces: `IHoaDonRepository`, `IGioHangRepository`.
* **Tầng Presentation & Infrastructure:**
  - `BanHangController`: Cung cấp các Endpoint tiếp nhận đơn đặt hàng trực tuyến hoặc gọi nội bộ từ giao diện quầy.
  - DTOs: `DatHangDTO`, `CapNhatDonHangDTO`, `TaoHoaDonOfflineDTO`.
  - Repositories: `HoaDonRepository`, `GioHangRepository`.

### 2. Phân tích các Đường nối Quan hệ và Lý do thiết kế
1. **`ChiTietHoaDon` hợp thành `HoaDon` (`Composition - 1..*`):**
   - *Lý do:* Hóa đơn là một Aggregate Root (theo thiết kế DDD). Mọi thao tác tính tiền, chiết khấu đều phải đi qua thực thể cha `HoaDon`. Khi thanh toán thành công, hóa đơn là căn cứ pháp lý và bảo hành.
2. **`ChiTietGioHang` hợp thành `GioHang` (`Composition - 0..*`):**
   - *Lý do:* Quản lý vòng đời giỏ hàng. Khi khách hàng bấm đặt hàng thành công (`Execute(DatHangDTO)`), phương thức `DonDepGioHang()` sẽ xóa trắng các dòng trong chi tiết giỏ hàng này.
3. **Sự tách biệt giữa `DatHangUseCase` (Online) và `BanHangOfflineUseCase` (POS Quầy):**
   - *Lý do:* Khách mua online phải trải qua quy trình đặt hàng -> chờ xác nhận -> giao hàng (cần thông tin địa chỉ, số điện thoại người nhận). Trong khi đó, bán hàng tại quầy là quy trình tức thì: khách chọn trang sức, thu ngân quét mã, in hóa đơn và nhận tiền ngay lập tức. Tách riêng hai Use Case giúp code độc lập, không làm phát sinh lỗi chéo khi sửa đổi quy trình mua sắm của một bên.

---

## 3.4. Phân hệ Quản lý Dịch vụ và Bảo hành

### 1. Danh sách các Lớp và Thành phần
* **Tầng Domain:**
  - `PhieuBaoHanh`: Quản lý toàn bộ vòng đời dịch vụ chăm sóc trang sức. Sở hữu các phương thức kiểm soát trạng thái: `ChuyenSangDangXuLy()`, `QuyetToan(chiPhi)`, `HoanTatBaoHanh()`, `HuyPhieu()`, `TinhPhiBaoHanh()`.
  - Tham chiếu `KhachHang` và `SanPhamTrangSuc`.
* **Tầng Application:**
  - `TiepNhanBaoHanhUseCase`: Tiếp nhận sản phẩm hư hỏng, ghi nhận tình trạng trầy xước, đứt gãy, rơi đá và ước tính chi phí sơ bộ.
  - `XuLyBaoHanhUseCase`: Cập nhật tiến độ của thợ kim hoàn, ghi nhận chi phí vật tư thực tế và xuất hóa đơn dịch vụ khi hoàn trả trang sức cho khách.
  - Interface: `IBaoHanhRepository`.
* **Tầng Presentation & Infrastructure:**
  - `BaoHanhController`: Tiếp nhận các yêu cầu tra cứu và cập nhật phiếu bảo hành.
  - DTOs: `TaoBaoHanhDTO`, `CapNhatTrangThaiDTO`.
  - Repository: `BaoHanhRepository`.

### 2. Phân tích các Đường nối Quan hệ và Lý do thiết kế
1. **`KhachHang` và `SanPhamTrangSuc` liên kết tới `PhieuBaoHanh` (`Association`):**
   - *Lý do:* Phiếu bảo hành đóng vai trò biên bản cam kết giữa 3 bên: Khách hàng sở hữu, món trang sức cần sửa chữa và cửa hàng Blink Blink Jewelry. Đường nối này giúp hệ thống truy xuất được lịch sử: sản phẩm này đã được bảo hành bao nhiêu lần và khách hàng có thường xuyên bảo dưỡng định kỳ hay không.
2. **Luồng State Machine trong phương thức của `PhieuBaoHanh`:**
   - *Lý do:* Các phương thức `ChuyenSangDangXuLy()`, `QuyetToan()` hay `HoanTatBaoHanh()` đóng gói quy tắc nghiệp vụ chặt chẽ: Không được phép hoàn tất nếu chưa quyết toán chi phí; không được phép quyết toán nếu phiếu đang ở trạng thái đã hủy.

---

## 3.5. Phân hệ Báo cáo và Thống kê

### 1. Danh sách các Lớp và Thành phần
* **Tầng Domain:**
  - Sử dụng lại các Entity lõi: `HoaDon`, `SanPhamTrangSuc`, `ChiTietKiemKe`. Tầng này chỉ đóng vai trò cung cấp cấu trúc dữ liệu nguồn, không chứa nghiệp vụ ghi chép.
* **Tầng Application:**
  - `ThongKeDoanhThuUseCase`: Tổng hợp doanh số, số lượng đơn hàng, chiết khấu và tính toán lợi nhuận ước tính theo khoảng thời gian (từ ngày đến ngày).
  - `ThongKeKhoHangUseCase`: Phân tích tình hình kho: lập báo cáo giá trị tồn kho, danh sách các sản phẩm bán chạy nhất (`LayTopBanChay`), và các sản phẩm bán chậm/doanh thu thấp (`LaySanPhamDoanhThuThap`) để hỗ trợ đưa ra quyết định nhập hàng hoặc xả hàng.
  - Interface: `IThongKeRepository`.
* **Tầng Presentation & Infrastructure:**
  - `ThongKeController`: Cung cấp các hàm API HttpGet (`GetDoanhThu`, `GetBaoCaoTonKho`, `GetTopBanChay`, `GetSanPhamDoanhThuThap`) phục vụ hiển thị biểu đồ và bảng dữ liệu lên giao diện báo cáo của Admin.
  - DTOs: `DoanhThuThongKeDTO`, `BaoCaoTonKhoDTO`, `TopSanPhamBanChayDTO`.
  - Repository: `ThongKeRepository`.

### 2. Phân tích các Đường nối Quan hệ và Lý do thiết kế
1. **Kiến trúc chỉ đọc (Read-Only Separation):**
   - *Lý do:* Phân hệ báo cáo thống kê sử dụng riêng một `IThongKeRepository` thay vì gọi lại repository của từng phân hệ đơn lẻ. Các câu lệnh truy vấn ở đây thường là các phép gom nhóm dữ liệu phức tạp (`GROUP BY`, `SUM`, `COUNT`, `JOIN` nhiều bảng). Việc tách riêng giúp tối ưu hóa hiệu năng, có thể viết các câu lệnh SQL tối ưu riêng mà không làm xáo trộn hoặc làm chậm các luồng nghiệp vụ giao dịch thường ngày (Bán hàng, Đặt hàng, Nhập kho).
2. **Sử dụng các DTO chuyên biệt thay vì trả về toàn bộ Entity:**
   - *Lý do:* Các lớp `DoanhThuThongKeDTO` hay `TopSanPhamBanChayDTO` chỉ chứa đúng các cột cần vẽ biểu đồ (Doanh thu, Lợi nhuận, Tên sản phẩm, Số lượng đã bán). Việc này giúp giảm tải băng thông mạng và tránh làm lộ các thông tin nhạy cảm của hệ thống.

---

# PHẦN 4: ĐÁNH GIÁ VÀ GIÁ TRỊ CỦA THIẾT KẾ ĐỐI VỚI BÀI TIỂU LUẬN

Nhìn một cách tổng thể, toàn bộ thiết kế cơ sở dữ liệu (ERD) và các sơ đồ lớp (Class Diagram) của hệ thống **Blink Blink Jewelry** mang lại các giá trị học thuật và ứng dụng thực tiễn cao:
1. **Tuân thủ chặt chẽ các nguyên lý thiết kế phần mềm hiện đại:**
   - Triệt để áp dụng nguyên lý **SOLID**: Trách nhiệm đơn lẻ (Single Responsibility), Mở/Đóng (Open/Closed), Phân tách giao diện (Interface Segregation), và Đảo ngược phụ thuộc (Dependency Inversion).
   - Ranh giới giữa các tầng kiến trúc được phân định minh bạch. Tầng giao diện (WinForms cho Admin, Web cho Khách hàng) hoàn toàn có thể thay đổi mà không ảnh hưởng đến logic nghiệp vụ cốt lõi ở tầng Use Case và Domain.
2. **Phù hợp đặc thù nghiệp vụ trang sức:**
   - Xử lý mượt mà bài toán bán hàng đa kênh (Omni-channel): Hỗ trợ cả khách vãng lai mua nhanh tại quầy POS và khách hàng đăng ký mua sắm trực tuyến.
   - Quản lý nghiêm ngặt tồn kho và kiểm kê hao hụt giá trị cao; hỗ trợ dịch vụ bảo dưỡng/sửa chữa chuyên nghiệp.
3. **Tính sẵn sàng kiểm thử (Testability) và Bảo trì cao:**
   - Nhờ việc sử dụng Repository Interface và DTO tại mọi điểm kết nối, hệ thống cho phép viết các bài kiểm thử tự động (Unit Test) một cách nhanh chóng và tin cậy.

