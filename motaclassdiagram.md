# TÀI LIỆU MÔ TẢ CHI TIẾT CLASS DIAGRAM HỆ THỐNG QUẢN LÝ TIỆM VÀNG / TRANG SỨC BLINK BLINK
*(Phân tích và Thiết kế Hướng đối tượng - PTTKHDT)*

---

## 1. TỔNG QUAN HỆ THỐNG VÀ DANH SÁCH CÁC CLASS

Hệ thống **Blink Blink Jewelry** được mô hình hóa theo mô hình hướng đối tượng với 17 lớp (classes/swimlanes) được thiết kế trong sơ đồ UML Class Diagram (`classdiagram và erd blinkblink pttkhdt-class diagram.drawio.xml`).

Các lớp được phân bổ theo 5 phân hệ nghiệp vụ chính:
1. **Phân hệ Người dùng & Phân quyền (Identity & Access Control):** `NguoiDung`, `KhachHang`, `NhanVien`, `VaiTro`.
2. **Phân hệ Danh mục & Sản phẩm Trang sức (Catalog & Products):** `DanhMuc`, `NhomSanPham`, `SanPhamTrangSuc`.
3. **Phân hệ Quản lý Kho & Tồn kho (Inventory & Warehouse):** `PhieuNhapKho`, `ChiTietNhapKho`, `PhieuKiemKe`, `ChiTietKiemKe`.
4. **Phân hệ Giỏ hàng & Bán lẻ / Hóa đơn (Cart & Orders):** `GioHang`, `ChiTietGioHang`, `HoaDon` *(Bản đầy đủ/Online)*, `ChiTietHoaDon`, `HoaDon` *(Bản rút gọn/POS quầy)*.
5. **Phân hệ Dịch vụ & Hậu mãi (Warranty & Services):** `PhieuBaoHanh`.

### Bảng tổng hợp danh sách các Class trên sơ đồ

| STT | Tên Lớp | Loại Lớp (Stereotype) | Phân hệ nghiệp vụ | Số thuộc tính | Số phương thức |
|:---:|:---|:---|:---|:---:|:---:|
| 1 | `NguoiDung` | `<<abstract>>` | Quản lý Người dùng | 7 | 4 |
| 2 | `KhachHang` | Kế thừa `NguoiDung` | Khách hàng | 2 *(+7 kế thừa)* | 1 *(+4 kế thừa)* |
| 3 | `NhanVien` | Kế thừa `NguoiDung` | Nhân sự | 2 *(+7 kế thừa)* | 1 *(+4 kế thừa)* |
| 4 | `VaiTro` | Entity | Phân quyền | 3 | 2 |
| 5 | `NhomSanPham` | Entity | Phân loại trang sức | 3 | 0 |
| 6 | `SanPhamTrangSuc` | Entity | Sản phẩm cốt lõi | 13 | 4 |
| 7 | `PhieuNhapKho` | Entity / Transaction | Quản lý nhập kho | 7 | 4 |
| 8 | `ChiTietNhapKho` | Composition Component | Dòng nhập kho | 5 | 2 |
| 9 | `PhieuKiemKe` | Entity / Transaction | Kiểm kê định kỳ | 8 | 4 |
| 10 | `ChiTietKiemKe` | Composition Component | Dòng kiểm kê | 5 | 3 |
| 11 | `DanhMuc` | Entity | Danh mục sản phẩm | 3 | 2 |
| 12 | `HoaDon` *(Đầy đủ)* | Entity / Transaction | Đơn hàng & Thanh toán | 12 | 3 |
| 13 | `ChiTietHoaDon` | Composition Component | Mặt hàng mua | 5 | 1 |
| 14 | `GioHang` | Entity | Giỏ hàng mua sắm | 4 | 4 |
| 15 | `ChiTietGioHang` | Composition Component | Mặt hàng trong giỏ | 3 | 2 |
| 16 | `PhieuBaoHanh` | Entity / Transaction | Bảo hành & Sửa chữa | 10 | 6 |
| 17 | `HoaDon` *(Rút gọn POS)* | Entity / Transaction | Bán lẻ tại quầy | 7 | 2 |

---

## 2. QUY ƯỚC KÝ HIỆU TRUY CẬP (ACCESS MODIFIERS)

- **`-` (Private):** Thuộc tính/Phương thức chỉ có thể truy xuất nội bộ trong chính lớp đó.
- **`#` (Protected):** Thuộc tính/Phương thức có thể truy xuất trong lớp khai báo và các lớp con kế thừa từ nó.
- **`+` (Public):** Thuộc tính/Phương thức có thể truy xuất từ bất kỳ đâu bên ngoài lớp.

---

## 3. MÔ TẢ CHI TIẾT TỪNG CLASS

### 3.1. Phân hệ Người dùng & Phân quyền (Identity & Access Control)

#### 1. Class `NguoiDung` (`<<abstract>>`)
- **Định danh Cell trên sơ đồ:** `618drPIvKTOmIINHH8TA-1`
- **Mục đích:** Là lớp trừu tượng cơ sở (Base/Abstract Class) đại diện cho tất cả các đối tượng người dùng đăng nhập và tương tác với hệ thống (bao gồm cả Khách hàng và Nhân viên), chứa các thông tin xác thực, định danh chung và trạng thái tài khoản.

##### Danh sách Thuộc tính (Attributes):
| Ký hiệu | Tên thuộc tính | Kiểu dữ liệu | Ý nghĩa nghiệp vụ |
|:---:|:---|:---|:---|
| `#` | `_maNguoiDung` | `string` | Mã định danh duy nhất của người dùng trong toàn hệ thống. |
| `#` | `_hoTen` | `string` | Họ và tên đầy đủ của người dùng. |
| `#` | `_soDienThoai` | `string` | Số điện thoại liên hệ cá nhân. |
| `#` | `_tenDangNhap` | `string` | Tên tài khoản dùng để đăng nhập hệ thống. |
| `#` | `_matKhau` | `string` | Mật khẩu tài khoản (đã mã hóa hash bảo mật). |
| `#` | `_maVaiTro` | `string` | Mã vai trò được gán (khóa ngoại liên kết đến `VaiTro`). |
| `#` | `_trangThai` | `bool` | Trạng thái kích hoạt của tài khoản (`true`: Đang hoạt động, `false`: Đã bị khóa). |

##### Danh sách Phương thức (Methods):
| Ký hiệu | Tên phương thức & Tham số | Kiểu trả về | Mô tả chức năng |
|:---:|:---|:---|:---|
| `+` | `DangNhap(tenDN: string, mk: string)` | `bool` | Xác thực người dùng qua tài khoản và mật khẩu; trả về `true` nếu hợp lệ. |
| `+` | `DoiMatKhau(mkMoi: string)` | `bool` | Cho phép người dùng cập nhật mật khẩu mới sau khi đã xác thực. |
| `+` | `CapNhatThongTin(hoTen: string, sdt: string)` | `bool` | Cập nhật thông tin họ tên và số điện thoại người dùng. |
| `+` | `KhoaTaiKhoan()` | `bool` | Khóa tài khoản của người dùng (chuyển `_trangThai` sang `false`). |

---

#### 2. Class `KhachHang`
- **Định danh Cell trên sơ đồ:** `618drPIvKTOmIINHH8TA-14`
- **Quan hệ:** Kế thừa (`Extends`) từ lớp cha trừu tượng `NguoiDung`.
- **Mục đích:** Đại diện cho đối tượng khách hàng mua sắm trang sức, có thêm ngày sinh để phục vụ chính sách ưu đãi sinh nhật và lịch sử mua sắm/bảo hành.

##### Danh sách Thuộc tính (Attributes):
| Ký hiệu | Tên thuộc tính | Kiểu dữ liệu | Ý nghĩa nghiệp vụ |
|:---:|:---|:---|:---|
| `-` | `_maKhachHang` | `string` | Mã định danh khách hàng riêng biệt. |
| `-` | `_ngaySinh` | `DateTime` | Ngày tháng năm sinh (phục vụ chương trình tri ân, khuyến mãi sinh nhật). |
| *kế thừa* | *(Các trường từ `NguoiDung`)* | | `_maNguoiDung`, `_hoTen`, `_soDienThoai`, `_tenDangNhap`, `_matKhau`, `_maVaiTro`, `_trangThai`. |

##### Danh sách Phương thức (Methods):
| Ký hiệu | Tên phương thức & Tham số | Kiểu trả về | Mô tả chức năng |
|:---:|:---|:---|:---|
| `+` | `ToString()` | `string (override)` | Ghi đè phương thức chuỗi để định dạng hiển thị thông tin tóm tắt của khách hàng. |
| *kế thừa* | *(Các hàm từ `NguoiDung`)* | | `DangNhap()`, `DoiMatKhau()`, `CapNhatThongTin()`, `KhoaTaiKhoan()`. |

---

#### 3. Class `NhanVien`
- **Định danh Cell trên sơ đồ:** `618drPIvKTOmIINHH8TA-20`
- **Quan hệ:** Kế thừa (`Extends`) từ lớp cha trừu tượng `NguoiDung`.
- **Mục đích:** Đại diện cho nhân viên làm việc tại tiệm vàng (thu ngân, nhân viên kho, thợ kim hoàn, nhân viên bán hàng).

##### Danh sách Thuộc tính (Attributes):
| Ký hiệu | Tên thuộc tính | Kiểu dữ liệu | Ý nghĩa nghiệp vụ |
|:---:|:---|:---|:---|
| `-` | `_maNhanVien` | `string` | Mã định danh hồ sơ nhân viên trong tiệm. |
| `-` | `_luongCoBan` | `decimal` | Mức lương cơ bản hàng tháng của nhân viên. |
| *kế thừa* | *(Các trường từ `NguoiDung`)* | | `_maNguoiDung`, `_hoTen`, `_soDienThoai`, `_tenDangNhap`, `_matKhau`, `_maVaiTro`, `_trangThai`. |

##### Danh sách Phương thức (Methods):
| Ký hiệu | Tên phương thức & Tham số | Kiểu trả về | Mô tả chức năng |
|:---:|:---|:---|:---|
| `+` | `get set co ban` | `void` | Các phương thức getter/setter đóng gói truy xuất các thuộc tính nội bộ. |
| *kế thừa* | *(Các hàm từ `NguoiDung`)* | | `DangNhap()`, `DoiMatKhau()`, `CapNhatThongTin()`, `KhoaTaiKhoan()`. |

---

#### 4. Class `VaiTro`
- **Định danh Cell trên sơ đồ:** `618drPIvKTOmIINHH8TA-26`
- **Quan hệ:** Thu nạp (`Aggregation`) với `NguoiDung` (1 Vai trò gắn với 0..* Người dùng).
- **Mục đích:** Quản lý nhóm vai trò phân quyền người dùng (ví dụ: Quản trị viên, Quản lý kho, Thu ngân, Khách hàng).

##### Danh sách Thuộc tính (Attributes):
| Ký hiệu | Tên thuộc tính | Kiểu dữ liệu | Ý nghĩa nghiệp vụ |
|:---:|:---|:---|:---|
| `-` | `_maVaiTro` | `string` | Mã định danh vai trò (Role ID). |
| `-` | `_tenVaiTro` | `string` | Tên gọi của vai trò (vd: Admin, StockKeeper, Cashier, Customer). |
| `-` | `_moTa` | `string` | Diễn giải chi tiết trách nhiệm và phạm vi quyền hạn của vai trò. |

##### Danh sách Phương thức (Methods):
| Ký hiệu | Tên phương thức & Tham số | Kiểu trả về | Mô tả chức năng |
|:---:|:---|:---|:---|
| `+` | `ThemQuyen(maQuyen: string)` | `void` | Thêm một mã quyền chức năng (Permission ID) vào vai trò này. |
| `+` | `XoaQuyen(maQuyen: string)` | `void` | Thu hồi một mã quyền chức năng khỏi vai trò. |

---

### 3.2. Phân hệ Danh mục & Sản phẩm Trang sức (Catalog & Products)

#### 5. Class `NhomSanPham`
- **Định danh Cell trên sơ đồ:** `uenGJVKW0tGAkK0qaeP1-95`
- **Quan hệ:** Thu nạp (`Aggregation`) với `SanPhamTrangSuc` (1 Nhóm sản phẩm gom 0..* Sản phẩm).
- **Mục đích:** Phân loại trang sức theo nhóm chế tác (ví dụ: Nhẫn kim cương, Dây chuyền vàng, Lắc tay bạc, Bông tai ngọc trai...).

##### Danh sách Thuộc tính (Attributes):
| Ký hiệu | Tên thuộc tính | Kiểu dữ liệu | Ý nghĩa nghiệp vụ |
|:---:|:---|:---|:---|
| `-` | `_maNhom` | `string` | Mã nhóm sản phẩm trang sức. |
| `-` | `_tenNhom` | `string` | Tên của nhóm sản phẩm. |
| `-` | `_moTa` | `string` | Mô tả đặc điểm, quy chuẩn của nhóm sản phẩm. |

##### Danh sách Phương thức (Methods):
- Lớp này trên sơ đồ đóng vai trò lớp thực thể chứa dữ liệu danh mục tĩnh (Entity Data), sử dụng các hành vi CRUD và thuộc tính cơ bản.

---

#### 6. Class `DanhMuc`
- **Định danh Cell trên sơ đồ:** `uenGJVKW0tGAkK0qaeP1-170`
- **Quan hệ:** Thu nạp (`Aggregation`) với `SanPhamTrangSuc` (1 Danh mục chứa 0..* Sản phẩm).
- **Mục đích:** Danh mục hiển thị và quản lý theo bộ sưu tập / phong cách (ví dụ: Trang sức mùa cưới, Quà tặng Valentine, Trang sức phong thủy...).

##### Danh sách Thuộc tính (Attributes):
| Ký hiệu | Tên thuộc tính | Kiểu dữ liệu | Ý nghĩa nghiệp vụ |
|:---:|:---|:---|:---|
| `-` | `_maDanhMuc` | `string` | Mã danh mục sản phẩm. |
| `-` | `_tenDanhMuc` | `string` | Tên danh mục hiển thị. |
| `-` | `_moTa` | `string` | Mô tả về danh mục bộ sưu tập. |

##### Danh sách Phương thức (Methods):
| Ký hiệu | Tên phương thức & Tham số | Kiểu trả về | Mô tả chức năng |
|:---:|:---|:---|:---|
| `+` | `LayDanhSachSanPham()` | `List<SanPhamTrangSuc>` | Lấy danh sách tất cả các sản phẩm trang sức thuộc danh mục này. |
| `+` | `CapNhatDanhMuc()` | `bool` | Cập nhật thông tin tên, mô tả danh mục; trả về kết quả thành công/thất bại. |

---

#### 7. Class `SanPhamTrangSuc`
- **Định danh Cell trên sơ đồ:** `uenGJVKW0tGAkK0qaeP1-99`
- **Quan hệ:**
  - Liên kết với `DanhMuc` (0..* - 1)
  - Liên kết với `NhomSanPham` (0..* - 1)
  - Được tham chiếu bởi `ChiTietNhapKho`, `ChiTietKiemKe`, `ChiTietHoaDon`, `ChiTietGioHang`, `PhieuBaoHanh`.
- **Mục đích:** Là thực thể trung tâm của toàn hệ thống, quản lý chi tiết từng mẫu trang sức bao gồm chất liệu vàng/bạc/đá quý, kích cỡ (ni), định giá vốn, tỷ lệ lợi nhuận, giá niêm yết và lượng hàng tồn kho.

##### Danh sách Thuộc tính (Attributes):
| Ký hiệu | Tên thuộc tính | Kiểu dữ liệu | Ý nghĩa nghiệp vụ |
|:---:|:---|:---|:---|
| `-` | `_maSanPham` | `string` | Mã định danh sản phẩm trang sức (Barcode/SKU). |
| `-` | `_tenSanPham` | `string` | Tên thương mại của trang sức. |
| `-` | `_maDanhMuc` | `string` | Khóa ngoại trỏ đến `DanhMuc`. |
| `-` | `_maNhom` | `string` | Khóa ngoại trỏ đến `NhomSanPham`. |
| `-` | `_chatLieu` | `string` | Chất liệu chế tác (vd: Vàng 18K, Vàng Ý 750, Bạc 925, Bạch kim Platin, Đính Kim cương...). |
| `-` | `_size` | `string` | Kích thước / ni tay / chiều dài dây chuyền (vd: Ni 12, Size 45cm...). |
| `-` | `_trangThai` | `string` | Tình trạng kinh doanh (vd: "Đang bán", "Hết hàng", "Ngừng kinh doanh"). |
| `-` | `_giaNhap` | `decimal` | Giá vốn nhập hàng từ nhà cung cấp / xưởng chế tác. |
| `-` | `_tyLeLoiNhuan` | `decimal` | Tỷ lệ phần trăm lợi nhuận biên mục tiêu (markup). |
| `-` | `_giaBan` | `decimal` | Giá bán lẻ niêm yết chính thức đến khách hàng. |
| `-` | `_hinhAnh` | `string` | Đường dẫn lưu trữ hình ảnh hoặc URL của sản phẩm. |
| `-` | `_phanTramGiamGia` | `int` | Phần trăm giảm giá khuyến mãi hiện thời (0 - 100%). |
| `-` | `_soLuongTon` | `int` | Số lượng tồn kho thực tế trong cửa hàng. |

##### Danh sách Phương thức (Methods):
| Ký hiệu | Tên phương thức & Tham số | Kiểu trả về | Mô tả chức năng |
|:---:|:---|:---|:---|
| `-` | `KiemTraTonKho()` | `bool` | Kiểm tra nội bộ xem sản phẩm còn hàng (`_soLuongTon > 0`) hay đã hết hàng. |
| `-` | `CapNhatTrangThai(tt)` | `bool` | Cập nhật trạng thái kinh doanh của sản phẩm dựa trên tình trạng tồn kho hoặc quyết định kinh doanh. |
| `+` | `TinhGiaBanTheoLoiNhuan()` | `decimal` | Tự động tính toán giá bán theo công thức nghiệp vụ: `_giaBan = _giaNhap * (1 + _tyLeLoiNhuan)`. |
| `-` | `ToString()` | `string` | Trả về chuỗi mô tả ngắn gọn thông tin chi tiết sản phẩm. |

---

### 3.3. Phân hệ Quản lý Kho & Tồn kho (Inventory & Warehouse)

#### 8. Class `PhieuNhapKho`
- **Định danh Cell trên sơ đồ:** `uenGJVKW0tGAkK0qaeP1-118`
- **Quan hệ:** Hợp thành (`Composition`) với `ChiTietNhapKho` (1 Phiếu nhập gồm 1..* Dòng chi tiết nhập).
- **Mục đích:** Quản lý giao dịch nhập hàng từ nhà cung cấp hoặc xưởng chế tác kim hoàn vào kho tiệm vàng.

##### Danh sách Thuộc tính (Attributes):
| Ký hiệu | Tên thuộc tính | Kiểu dữ liệu | Ý nghĩa nghiệp vụ |
|:---:|:---|:---|:---|
| `-` | `_maPhieuNhap` | `string` | Mã số phiếu nhập kho duy nhất. |
| `-` | `_ngayNhap` | `date` | Ngày thực hiện tiếp nhận và nhập kho. |
| `-` | `_nhaCungCap` | `string` | Tên đơn vị / đối tác cung cấp trang sức hoặc vàng thô. |
| `-` | `_maNhanVien` | `string` | Mã nhân viên thủ kho chịu trách nhiệm lập phiếu nhập. |
| `-` | `_tongSoSanPham` | `int` | Tổng số lượng đơn vị sản phẩm được nhập trong phiếu. |
| `-` | `_tongGiaTri` | `decimal` | Tổng giá trị tiền thanh toán cho toàn bộ phiếu nhập kho. |
| `-` | `_trangThai` | `string` | Trạng thái phiếu nhập (vd: "Chờ duyệt", "Đã nhập kho", "Đã hủy"). |

##### Danh sách Phương thức (Methods):
| Ký hiệu | Tên phương thức & Tham số | Kiểu trả về | Mô tả chức năng |
|:---:|:---|:---|:---|
| `+` | `ThemChiTiet(sp)` | `void` | Thêm một dòng sản phẩm và số lượng vào chi tiết phiếu nhập. |
| `+` | `XacNhanNhapKho()` | `bool` | Xác nhận hàng đã được kiểm tra thực tế nhập vào kho và tăng số lượng tồn của sản phẩm tương ứng. |
| `+` | `DuyetPhieu()` | `void` | Cấp quản lý phê duyệt phiếu nhập hợp lệ. |
| `+` | `HuyPhieu()` | `bool` | Hủy phiếu nhập kho trong trường hợp sai thông tin hoặc hàng bị hoàn trả. |

---

#### 9. Class `ChiTietNhapKho`
- **Định danh Cell trên sơ đồ:** `uenGJVKW0tGAkK0qaeP1-131`
- **Quan hệ:**
  - Thành phần hợp thành của `PhieuNhapKho` (1..* thuộc về 1 Phiếu).
  - Liên kết với `SanPhamTrangSuc` (0..* - 1).
- **Mục đích:** Lưu trữ từng dòng mặt hàng cụ thể được nhập kho trong một phiếu nhập.

##### Danh sách Thuộc tính (Attributes):
| Ký hiệu | Tên thuộc tính | Kiểu dữ liệu | Ý nghĩa nghiệp vụ |
|:---:|:---|:---|:---|
| `-` | `_maPhieuNhap` | `string` | Mã phiếu nhập kho cha (Foreign Key). |
| `-` | `_maSanPham` | `string` | Mã sản phẩm trang sức được nhập (Foreign Key). |
| `-` | `_soLuong` | `int` | Số lượng sản phẩm nhập của dòng này. |
| `-` | `_giaVonNhap` | `decimal` | Đơn giá vốn nhập cho mỗi sản phẩm. |
| `-` | `_soChungTu` | `string` | Số hóa đơn, chứng từ đi kèm đối chiếu từ nhà cung cấp. |

##### Danh sách Phương thức (Methods):
| Ký hiệu | Tên phương thức & Tham số | Kiểu trả về | Mô tả chức năng |
|:---:|:---|:---|:---|
| `-` | `TinhGiaTri()` | `decimal` | Tính tổng giá trị dòng nhập: `_soLuong * _giaVonNhap`. |
| `+` | `CapNhatGiaVon()` | `bool` | Cập nhật lại giá vốn (`_giaNhap`) vào bảng sản phẩm tương ứng. |

---

#### 10. Class `PhieuKiemKe`
- **Định danh Cell trên sơ đồ:** `uenGJVKW0tGAkK0qaeP1-143`
- **Quan hệ:**
  - Hợp thành (`Composition`) với `ChiTietKiemKe` (1 Phiếu kiểm kê gồm 1..* Dòng chi tiết).
  - Liên kết với `NhanVien` (0..* Phiếu được phụ trách bởi 1 Nhân viên kiểm kê).
- **Mục đích:** Quản lý các đợt kiểm kê vàng bạc trang sức định kỳ tại từng quầy hoặc kho, phát hiện hao hụt, thừa thiếu và lập biên bản xử lý.

##### Danh sách Thuộc tính (Attributes):
| Ký hiệu | Tên thuộc tính | Kiểu dữ liệu | Ý nghĩa nghiệp vụ |
|:---:|:---|:---|:---|
| `-` | `_maPhieuKiemKe` | `string` | Mã số phiếu kiểm kê kho. |
| `-` | `_ngayKiemKe` | `DateTime` | Ngày giờ thực hiện kiểm đếm sản phẩm. |
| `-` | `_viTriKiemKe` | `string` | Khu vực kho hoặc quầy tủ trưng bày được kiểm tra (vd: Tủ vàng 24K Quầy 1). |
| `-` | `_maNhanVienKho` | `string` | Mã nhân viên kho chịu trách nhiệm kiểm đếm. |
| `-` | `_trangThai` | `string` | Tình trạng kiểm kê (vd: "Đang kiểm", "Hoàn thành", "Đã đối chiếu"). |
| `-` | `_trangThaiDuyet` | `bool` | Xác nhận phê duyệt từ cấp quản lý (`true`/`false`). |
| `-` | `_ghiChu` | `string` | Ghi chú thêm về điều kiện kiểm kê hoặc sự cố phát sinh. |
| `-` | `_ngayDuyet` | `DateTime?` | Ngày giờ người quản lý ký duyệt biên bản (có thể null). |

##### Danh sách Phương thức (Methods):
| Ký hiệu | Tên phương thức & Tham số | Kiểu trả về | Mô tả chức năng |
|:---:|:---|:---|:---|
| `+` | `BatDauKiemKe()` | `void` | Khởi tạo phiên kiểm kê, chốt số liệu tồn sổ sách tại thời điểm kiểm tra. |
| `+` | `KetThucKiemKe()` | `void` | Kết thúc việc đếm hàng thực tế và tổng hợp chênh lệch. |
| `+` | `LapBienBanChenhLech()` | `void` | Tự động xuất biên bản nếu phát hiện hàng hóa bị thiếu, thừa hoặc sai mã. |
| `+` | `VoHieuHoa()` | `void` | Hủy bỏ hoặc vô hiệu hóa kết quả phiếu kiểm kê nếu quy trình có vi phạm. |

---

#### 11. Class `ChiTietKiemKe`
- **Định danh Cell trên sơ đồ:** `uenGJVKW0tGAkK0qaeP1-157`
- **Quan hệ:**
  - Thành phần hợp thành của `PhieuKiemKe` (1..* thuộc về 1 Phiếu).
  - Liên kết với `SanPhamTrangSuc` (0..* - 1).
- **Mục đích:** Ghi nhận số liệu so sánh giữa sổ sách máy tính và số lượng thực đếm cho từng mã trang sức.

##### Danh sách Thuộc tính (Attributes):
| Ký hiệu | Tên thuộc tính | Kiểu dữ liệu | Ý nghĩa nghiệp vụ |
|:---:|:---|:---|:---|
| `-` | `_maPhieuKiemKe` | `string` | Mã phiếu kiểm kê liên kết (Foreign Key). |
| `-` | `_maSanPham` | `string` | Mã sản phẩm trang sức được kiểm đếm (Foreign Key). |
| `-` | `_tonSoSach` | `int` | Số lượng tồn kho ghi nhận trên hệ thống phần mềm tại thời điểm kiểm. |
| `-` | `_tonThucTe` | `int` | Số lượng đếm thực tế có mặt tại quầy/kho. |
| `-` | `_chenhLech` | `int` | Số lượng chênh lệch: `_tonThucTe - _tonSoSach` (<0: Thiếu, >0: Thừa, 0: Khớp). |

##### Danh sách Phương thức (Methods):
| Ký hiệu | Tên phương thức & Tham số | Kiểu trả về | Mô tả chức năng |
|:---:|:---|:---|:---|
| `+` | `XacDinhTrangThai()` | `string` | Đánh giá trạng thái dòng kiểm kê: "Khớp số lượng", "Thừa hàng", hoặc "Thất thoát/Thiếu". |
| `-` | `GhiNhanLyDo(lyDo)` | `void` | Ghi chú giải trình lý do gây nên chênh lệch (rơi gãy, đưa đi đánh bóng, thất lạc...). |
| `-` | `TinhTongHaoHut()` | `decimal` | Quy đổi số lượng chênh lệch thiếu ra giá trị tiền tệ tương ứng. |

---

### 3.4. Phân hệ Giỏ hàng & Bán lẻ / Hóa đơn (Cart & Orders)

#### 12. Class `GioHang`
- **Định danh Cell trên sơ đồ:** `Ldr6YsZa0aAMMqHu3qul-29`
- **Quan hệ:**
  - Hợp thành (`Composition`) với `ChiTietGioHang` (1 Giỏ hàng chứa 0..* Dòng sản phẩm).
  - Thuộc sở hữu của 1 `KhachHang`.
- **Mục đích:** Quản lý giỏ hàng của khách hàng khi lựa chọn mua sắm trang sức trực tuyến hoặc lưu tạm đơn hàng trước khi thanh toán.

##### Danh sách Thuộc tính (Attributes):
| Ký hiệu | Tên thuộc tính | Kiểu dữ liệu | Ý nghĩa nghiệp vụ |
|:---:|:---|:---|:---|
| `-` | `_maGioHang` | `string` | Mã định danh duy nhất của giỏ hàng. |
| `-` | `_maKhachHang` | `string` | Mã khách hàng sở hữu giỏ hàng này (Foreign Key). |
| `-` | `_soLuong` | `int` | Tổng số lượng mặt hàng đang có trong giỏ hàng. |
| `-` | `_danhSachSanPham` | `List<ChiTietGioHang>` | Danh sách các đối tượng chi tiết mặt hàng trong giỏ. |

##### Danh sách Phương thức (Methods):
| Ký hiệu | Tên phương thức & Tham số | Kiểu trả về | Mô tả chức năng |
|:---:|:---|:---|:---|
| `+` | `DonDepGioHang()` | `void` | Xóa sạch toàn bộ sản phẩm trong giỏ hàng (Clear Cart). |
| `+` | `DatHang()` | `void` | Chuyển đổi các mặt hàng trong giỏ sang quy trình đặt hàng / tạo hóa đơn. |
| `+` | `ThemSanPham()` | `void` | Thêm mới một sản phẩm vào danh sách giỏ hàng. |
| `+` | `XoaSanPham()` | `void` | Xóa một mặt hàng đã chọn khỏi giỏ hàng. |

---

#### 13. Class `ChiTietGioHang`
- **Định danh Cell trên sơ đồ:** `Ldr6YsZa0aAMMqHu3qul-39`
- **Quan hệ:**
  - Là thành phần hợp thành của `GioHang` (0..* dòng thuộc về 1 Giỏ hàng).
  - Liên kết với `SanPhamTrangSuc` (0..* - 1).
- **Mục đích:** Lưu trữ thông tin từng món trang sức được chọn và số lượng tương ứng trong giỏ hàng.

##### Danh sách Thuộc tính (Attributes):
| Ký hiệu | Tên thuộc tính | Kiểu dữ liệu | Ý nghĩa nghiệp vụ |
|:---:|:---|:---|:---|
| `-` | `_maGioHang` | `string` | Mã giỏ hàng chứa dòng sản phẩm này. |
| `-` | `_maSanPham` | `string` | Mã trang sức được chọn vào giỏ. |
| `-` | `_soLuongSanPham` | `int` | Số lượng chọn mua của món trang sức này. |

##### Danh sách Phương thức (Methods):
| Ký hiệu | Tên phương thức & Tham số | Kiểu trả về | Mô tả chức năng |
|:---:|:---|:---|:---|
| `+` | `TangSoLuong()` | `void` | Tăng số lượng mua của sản phẩm thêm 1 đơn vị. |
| `+` | `GiamSoLuong()` | `void` | Giảm số lượng mua của sản phẩm đi 1 đơn vị (nếu về 0 sẽ tự động xóa). |

---

#### 14. Class `HoaDon` *(Phiên bản đầy đủ / Đơn hàng đa kênh)*
- **Định danh Cell trên sơ đồ:** `Ldr6YsZa0aAMMqHu3qul-1`
- **Quan hệ:**
  - Liên kết với `KhachHang` (1 Khách hàng có 0..* Hóa đơn).
  - Hợp thành (`Composition`) với `ChiTietHoaDon` (1 Hóa đơn gồm 1..* Dòng chi tiết hóa đơn).
- **Mục đích:** Quản lý toàn diện quy trình bán hàng đa kênh (bán tại quầy hoặc bán trực tuyến giao tận nơi), lưu giữ thông tin giao nhận, thanh toán, trạng thái luân chuyển và các khoản tiền.

##### Danh sách Thuộc tính (Attributes):
| Ký hiệu | Tên thuộc tính | Kiểu dữ liệu | Ý nghĩa nghiệp vụ |
|:---:|:---|:---|:---|
| `-` | `_maHoaDon` | `string` | Mã đơn hàng / mã hóa đơn duy nhất. |
| `-` | `_ngayLap` | `DateTime` | Ngày và giờ lập đơn hàng/hóa đơn. |
| `-` | `_maNhanVien` | `string` | Mã nhân viên phụ trách bán hàng / xuất hóa đơn. |
| `-` | `_maKhachHang` | `string` | Mã khách hàng mua hàng (Foreign Key). |
| `-` | `_diaChiGiaoHang` | `string` | Địa chỉ nhận trang sức khi đặt hàng giao tận nhà. |
| `-` | `_soDienThoaiNhan` | `string` | Số điện thoại của người nhận hàng. |
| `-` | `_phuongThucThanhToan` | `string` | Hình thức thanh toán (Tiền mặt, Chuyển khoản ngân hàng, Thẻ tín dụng, VNPAY, COD). |
| `-` | `_trangThaiDonHang` | `string` | Trạng thái vòng đời đơn hàng ("Chờ xác nhận", "Đang đóng gói", "Đang giao", "Đã giao thành công", "Đã hủy"). |
| `-` | `_tongTienHang` | `decimal` | Tổng giá trị hàng hóa trước giảm trừ. |
| `-` | `_tienGiamGia` | `decimal` | Tổng tiền được khấu trừ từ khuyến mãi / mã voucher / ưu đãi thành viên. |
| `-` | `_tongThanhToan` | `decimal` | Số tiền thực tế người mua phải trả (`_tongTienHang - _tienGiamGia`). |
| `-` | `_ghiChu` | `string` | Lời nhắn hoặc chỉ dẫn giao hàng đặc biệt (vd: Giao sau 18h, bọc hộp quà cưới...). |

##### Danh sách Phương thức (Methods):
| Ký hiệu | Tên phương thức & Tham số | Kiểu trả về | Mô tả chức năng |
|:---:|:---|:---|:---|
| `+` | `TinhTongTien()` | `decimal` | Tổng hợp giá trị các dòng chi tiết hóa đơn, trừ đi giảm giá để ra tổng thanh toán. |
| `+` | `CapNhatTrangThai(trangThai: string, maNV: string)` | `bool` | Chuyển đổi trạng thái đơn hàng (kèm mã nhân viên ghi nhận thao tác kiểm soát). |
| `+` | `HuyHoaDon()` | `bool` | Hủy hóa đơn / đơn hàng và hoàn lại số lượng tồn kho cho các sản phẩm. |

---

#### 15. Class `ChiTietHoaDon`
- **Định danh Cell trên sơ đồ:** `Ldr6YsZa0aAMMqHu3qul-18`
- **Quan hệ:**
  - Thành phần hợp thành của `HoaDon` (1..* dòng thuộc về 1 Hóa đơn).
  - Liên kết với `SanPhamTrangSuc` (0..* - 1).
- **Mục đích:** Lưu trữ chi tiết từng món trang sức được bán trong hóa đơn, bao gồm đơn giá thời điểm bán và tỷ lệ giảm giá sản phẩm.

##### Danh sách Thuộc tính (Attributes):
| Ký hiệu | Tên thuộc tính | Kiểu dữ liệu | Ý nghĩa nghiệp vụ |
|:---:|:---|:---|:---|
| `-` | `_soHoaDon` | `string` | Mã hóa đơn cha chứa mặt hàng này. |
| `-` | `_maSanPham` | `string` | Mã sản phẩm trang sức được mua. |
| `-` | `_donGia` | `decimal` | Giá bán lẻ của sản phẩm tại thời điểm giao dịch. |
| `-` | `_soLuong` | `int` | Số lượng chiếc/món trang sức mua. |
| `-` | `_phanTramGiamGia` | `int` | Tỷ lệ phần trăm giảm giá riêng áp dụng cho sản phẩm này. |

##### Danh sách Phương thức (Methods):
| Ký hiệu | Tên phương thức & Tham số | Kiểu trả về | Mô tả chức năng |
|:---:|:---|:---|:---|
| `+` | `TinhThanhTien()` | `decimal` | Tính thành tiền của dòng: `_donGia * _soLuong * (1 - _phanTramGiamGia / 100)`. |

---

#### 16. Class `HoaDon` *(Phiên bản rút gọn / POS bán lẻ tại quầy)*
- **Định danh Cell trên sơ đồ:** `j9QlCu0685t5N0Pw1BH7-14`
- **Quan hệ:**
  - Liên kết với `SanPhamTrangSuc` (0..* - 0..*).
- **Mục đích:** Là phiên bản tinh giản của hóa đơn, phù hợp với nghiệp vụ bán lẻ nhanh tại quầy thu ngân (POS) không cần xử lý thông tin vận chuyển, giao nhận phức tạp.

##### Danh sách Thuộc tính (Attributes):
| Ký hiệu | Tên thuộc tính | Kiểu dữ liệu | Ý nghĩa nghiệp vụ |
|:---:|:---|:---|:---|
| `-` | `_maHoaDon` | `string` | Mã hóa đơn bán lẻ tại quầy. |
| `-` | `_ngayLap` | `DateTime` | Ngày giờ lập hóa đơn xuất cho khách. |
| `-` | `_maNhanVien` | `string` | Mã nhân viên thu ngân lập phiếu. |
| `-` | `_maKhachHang` | `string` | Mã khách hàng (hoặc mã khách lẻ vãng lai). |
| `-` | `_tongTienHang` | `decimal` | Tổng tiền hàng bán ra. |
| `-` | `_tienGiamGia` | `decimal` | Tiền chiết khấu trực tiếp tại quầy. |
| `-` | `_tongThanhToan` | `decimal` | Tổng tiền mặt hoặc chuyển khoản khách đã thanh toán. |

##### Danh sách Phương thức (Methods):
| Ký hiệu | Tên phương thức & Tham số | Kiểu trả về | Mô tả chức năng |
|:---:|:---|:---|:---|
| `+` | `TinhTongTien()` | `decimal` | Tính toán số tiền thanh toán cuối cùng của hóa đơn. |
| `+` | `HuyHoaDon()` | `bool` | Hủy phiếu thu / hóa đơn bán lẻ khi có sai sót lúc thanh toán. |

---

### 3.5. Phân hệ Dịch vụ & Hậu mãi (Warranty & Services)

#### 17. Class `PhieuBaoHanh`
- **Định danh Cell trên sơ đồ:** `_d1MvcGUq7aNh32TfMgG-1`
- **Quan hệ:**
  - Liên kết với `KhachHang` (1 Khách hàng có 0..* Phiếu bảo hành).
  - Liên kết với `SanPhamTrangSuc` (1 Sản phẩm có 0..* Lần tiếp nhận bảo hành).
- **Mục đích:** Quản lý dịch vụ sau bán hàng - bảo hành, bảo dưỡng, làm sáng, đánh bóng, gắn đá rơi, nới/bóp ni nhẫn cho khách hàng tiệm vàng.

##### Danh sách Thuộc tính (Attributes):
| Ký hiệu | Tên thuộc tính | Kiểu dữ liệu | Ý nghĩa nghiệp vụ |
|:---:|:---|:---|:---|
| `-` | `_soPhieuBH` | `string` | Mã số phiếu bảo hành duy nhất. |
| `-` | `_maKhachHang` | `string` | Mã khách hàng mang trang sức đến làm dịch vụ. |
| `-` | `_soDienThoai` | `string` | Số điện thoại liên hệ thông báo cho khách khi hoàn thành. |
| `-` | `_maSanPham` | `string` | Mã món trang sức cần sửa chữa/bảo hành. |
| `-` | `_lyDoBaoHanh` | `string` | Lý do bảo hành (đánh bóng, đứt mắt xích, rớt hột đá kim cương, chỉnh size nhẫn...). |
| `-` | `_tinhTrangTiepNhan` | `string` | Hiện trạng thực tế của món trang sức khi nhân viên tiếp nhận kiểm tra ban đầu. |
| `-` | `_ghiChu` | `string` | Yêu cầu kỹ thuật đặc biệt của khách hàng cho thợ kim hoàn. |
| `-` | `_chiPhiUocTinh` | `decimal` | Chi phí dự kiến báo trước cho khách hàng (nếu ngoài phạm vi miễn phí). |
| `-` | `_chiPhiThucTe` | `decimal` | Chi phí phát sinh thực tế sau khi thợ đã sửa xong (tiền công + bù thêm vàng/đá). |
| `-` | `_trangThaiXuLy` | `string` | Tiến độ xử lý (vd: "Tiếp nhận", "Đang xử lý tại xưởng", "Chờ bàn giao", "Đã hoàn tất", "Đã hủy"). |

##### Danh sách Phương thức (Methods):
| Ký hiệu | Tên phương thức & Tham số | Kiểu trả về | Mô tả chức năng |
|:---:|:---|:---|:---|
| `+` | `CapNhatTrangThai(tt: string)` | `bool` | Cập nhật tiến độ của phiếu bảo hành (ví dụ: chuyển từ tiếp nhận sang đã sửa xong). |
| `+` | `ChuyenSangDangXuLy()` | `bool` | Xác nhận chuyển món trang sức sang cho thợ kim hoàn tiến hành chế tác/bảo dưỡng. |
| `+` | `QuyetToan(chiPhi: decimal)` | `bool` | Nhập chi phí phát sinh thực tế và tiến hành quyết toán thanh toán với khách. |
| `+` | `HoanTatBaoHanh()` | `bool` | Đánh dấu hoàn thành toàn bộ quy trình dịch vụ và bàn giao trang sức cho khách. |
| `+` | `HuyPhieu()` | `bool` | Hủy phiếu bảo hành nếu khách hàng đổi ý không muốn sửa chữa nữa. |
| `+` | `TinhPhiBaoHanh()` | `decimal` | Tính toán số tiền phí bảo hành khách cần thanh toán (căn cứ vào chi phí thực tế và chính sách miễn phí). |

---

## 4. BẢNG TỔNG HỢP QUAN HỆ GIỮA CÁC CLASS (RELATIONSHIPS & MULTIPLICITY)

Dưới đây là chi tiết toàn bộ **18 đường liên kết** được trích xuất từ file thiết kế:

| STT | Lớp Nguồn (Source) | Loại quan hệ (Relationship Type) | Lớp Đích (Target) | Độ bội (Multiplicity) | Diễn giải ý nghĩa nghiệp vụ |
|:---:|:---|:---:|:---|:---:|:---|
| 1 | `KhachHang` | **Kế thừa (Inheritance)** | `NguoiDung` | - | Khách hàng là một loại Người dùng, thừa hưởng đầy đủ thuộc tính và phương thức đăng nhập, đổi mật khẩu. |
| 2 | `NhanVien` | **Kế thừa (Inheritance)** | `NguoiDung` | - | Nhân viên là một loại Người dùng làm việc trong nội bộ tiệm, có thêm lương cơ bản. |
| 3 | `VaiTro` | **Thu nạp (Aggregation)** | `NguoiDung` | `1` - `0..*` | Mỗi vai trò (Admin, Nhân viên, Khách hàng) có thể được phân bổ cho 0 hoặc nhiều Người dùng. |
| 4 | `DanhMuc` | **Thu nạp (Aggregation)** | `SanPhamTrangSuc` | `1` - `0..*` | Một danh mục chứa nhiều sản phẩm trang sức; khi xóa danh mục, sản phẩm vẫn tồn tại độc lập. |
| 5 | `NhomSanPham` | **Thu nạp (Aggregation)** | `SanPhamTrangSuc` | `1` - `0..*` | Một nhóm sản phẩm chứa nhiều mặt hàng trang sức; đây là quan hệ thu nạp lỏng (Aggregation). |
| 6 | `ChiTietNhapKho` | **Hợp thành (Composition)** | `PhieuNhapKho` | `1..*` - `1` | Mỗi phiếu nhập kho bao gồm ít nhất 1 chi tiết nhập; nếu hủy/xóa phiếu nhập thì các chi tiết này bị xóa theo. |
| 7 | `ChiTietNhapKho` | **Liên kết (Association)** | `SanPhamTrangSuc` | `0..*` - `1` | Mỗi dòng nhập kho tham chiếu đến chính xác 1 món trang sức cụ thể. |
| 8 | `ChiTietKiemKe` | **Hợp thành (Composition)** | `PhieuKiemKe` | `1..*` - `1` | Mỗi phiếu kiểm kê bao gồm ít nhất 1 chi tiết kiểm đếm mặt hàng; sống chết phụ thuộc vào phiếu kiểm kê. |
| 9 | `ChiTietKiemKe` | **Liên kết (Association)** | `SanPhamTrangSuc` | `0..*` - `1` | Mỗi dòng kiểm kê đối chiếu số lượng của 1 món trang sức nhất định. |
| 10 | `NhanVien` | **Liên kết (Association)** | `PhieuKiemKe` | `1` - `0..*` | Mỗi nhân viên kho có thể thực hiện lập 0 hoặc nhiều đợt phiếu kiểm kê. |
| 11 | `ChiTietGioHang` | **Hợp thành (Composition)** | `GioHang` | `0..*` - `1` | Các sản phẩm trong giỏ hàng gắn liền với phiên giỏ hàng của khách hàng. |
| 12 | `SanPhamTrangSuc` | **Liên kết (Association)** | `ChiTietGioHang` | `1` - `0..*` | Một sản phẩm có thể được nhiều khách hàng thêm vào các chi tiết giỏ hàng của họ. |
| 13 | `ChiTietHoaDon` | **Hợp thành (Composition)** | `HoaDon` *(Đầy đủ)* | `1..*` - `1` | Một hóa đơn hoàn chỉnh bao gồm ít nhất 1 chi tiết hóa đơn (mặt hàng bán). |
| 14 | `SanPhamTrangSuc` | **Liên kết (Association)** | `ChiTietHoaDon` | `1` - `0..*` | Một món trang sức có thể xuất hiện trong nhiều chi tiết hóa đơn bán ra. |
| 15 | `HoaDon` *(Đầy đủ)* | **Liên kết (Association)** | `KhachHang` | `0..*` - `1` | Một khách hàng có thể sở hữu 0 hoặc nhiều hóa đơn mua sắm trang sức. |
| 16 | `KhachHang` | **Liên kết (Association)** | `PhieuBaoHanh` | `1` - `0..*` | Một khách hàng có thể mang trang sức đến lập 0 hoặc nhiều phiếu bảo hành. |
| 17 | `PhieuBaoHanh` | **Liên kết (Association)** | `SanPhamTrangSuc` | `0..*` - `1` | Mỗi phiếu bảo hành ứng với việc bảo dưỡng/sửa chữa cho 1 món trang sức cụ thể. |
| 18 | `HoaDon` *(Rút gọn POS)* | **Liên kết (Association)** | `SanPhamTrangSuc` | `0..*` - `0..*` | Sơ đồ bán lẻ nhanh tại quầy liên kết nhiều - nhiều trực tiếp giữa hóa đơn và sản phẩm. |

---

## 5. NHẬN XÉT VÀ ĐÁNH GIÁ THIẾT KẾ HƯỚNG ĐỐI TƯỢNG (OOP EVALUATION)

1. **Tính kế thừa (Inheritance):**
   - Lớp `NguoiDung` được thiết kế dưới dạng abstract class `<<abstract>>`, thể hiện chuẩn mực thiết kế OOP: không khởi tạo trực tiếp `NguoiDung` mà mở rộng thành `KhachHang` và `NhanVien`.
   - Giúp tái sử dụng tối đa logic xác thực mật khẩu, tài khoản và khóa người dùng.

2. **Tính đóng gói (Encapsulation):**
   - Hầu hết các trường dữ liệu đều được đặt ở mức độ truy cập `private` (`-`) hoặc `protected` (`#`), đảm bảo tính che giấu dữ liệu và toàn vẹn của nghiệp vụ tài chính, giá vốn và mật khẩu tài khoản.
   - Các hành vi biến đổi trạng thái đều được điều khiển thông qua các phương thức nghiệp vụ rõ ràng như `XacNhanNhapKho()`, `CapNhatTrangThai()`, `DuyetPhieu()`, `QuyetToan()`.

3. **Tính hợp thành và phân tách trách nhiệm (Separation of Concerns):**
   - Thiết kế rành mạch giữa các phiếu chứng từ (Transaction Header) và các dòng chi tiết (Transaction Detail): `PhieuNhapKho` - `ChiTietNhapKho`, `PhieuKiemKe` - `ChiTietKiemKe`, `HoaDon` - `ChiTietHoaDon`, `GioHang` - `ChiTietGioHang` theo đúng quan hệ Composition trong UML.
   - Thể hiện đầy đủ đặc thù ngành kim hoàn: định giá bán theo markup từ giá vốn (`TinhGiaBanTheoLoiNhuan`), theo dõi ni/size, chất liệu vàng bạc, kiểm kê thất thoát định kỳ và bảo hành đánh bóng trang sức.
