# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Tối ưu hóa không gian hiển thị & nới rộng giao diện WinForms Admin (`BlinkBlink.Admin`):**
  - Mở rộng kích thước cửa sổ chính lên 1480x880 (MinimumSize 1280x720, mở cực đại Maximized).
  - Tái cấu trúc thanh công cụ dạng FlowLayoutPanel tự co giãn chống tràn chữ trên mọi độ phân giải.
  - Cấu hình MinimumWidth cho toàn bộ các cột DataGridView trên cả 5 phân hệ.
  - Sửa lỗi ngoại lệ `SplitContainer` an toàn bằng sự kiện `Load` & `Resize`.
  - Cấu hình `dgv.Dock = DockStyle.Fill;` trong `UITheme.ApplyGridViewStyle` giúp bảng dữ liệu lấp đầy 100% diện tích.
  - Tự động dò tìm và kết nối SQL Server cục bộ (`.`, `localhost`, `.\MSSQL`, `(localdb)\mssqllocaldb`).
- **Khắc phục toàn diện lỗi phông chữ tiếng Việt & bảng mã CSDL:**
  - Nạp lại CSDL `BlinkBlinkJewelryDb` qua luồng UTF-8 ADO.NET trực tiếp, loại bỏ hoàn toàn hiện tượng lỗi mã ký tự (mojibake) ở các cột `DiaChiGiaoHang`, `TenSanPham`, `TenDanhMuc`, `GhiChu` (hiển thị chuẩn xác tiếng Việt có dấu: Lê Lợi, Hai Bà Trưng, Bến Nghé...).
  - Nâng cấp toàn bộ hệ thống phông chữ từ `GenericSansSerif` sang phông cao cấp **`Segoe UI`** trên toàn bộ WinForms Admin (tiêu đề, nút bấm, nhãn, bảng dữ liệu, tab control).
  - Cấu hình `AutoSize = true` cho `lblHeaderTitle` và `lblUserRole` trên thanh Header, loại bỏ lỗi gãy rớt dòng (`Quản / Lý` và `Tài / khoản:`).
  - Thiết lập `UseMnemonic = false` cho thanh Header, Sidebar menu và các nhãn tiêu đề, đảm bảo hiển thị trọn vẹn ký tự `&` trong các chuỗi chữ kim hoàn.
  - Toàn bộ 27/27 unit & integration tests pass 100%, solution biên dịch 0 lỗi, 0 cảnh báo.

## 2. Những gì đang làm dở & Các quyết định kỹ thuật đã chốt
- WinForms Admin đã hoàn thiện trọn vẹn về mặt mỹ thuật kim hoàn, bố cục responsive, phông chữ Segoe UI và kết nối CSDL SQL Server thật.
- Phân hệ kế tiếp: Xây dựng Giao diện Web Khách Hàng (`BlinkBlink.Web`) mua sắm, giỏ hàng, tra cứu bảo hành kết nối Web API theo chuẩn Clean Architecture và Taste Skill.

## 3. Hành động cụ thể tiếp theo cho phiên làm việc sau
1. Commit các thay đổi trên nhánh `bugfix/vietnamese-font-and-utf8-data`.
2. Hợp nhất (merge) nhánh `bugfix/vietnamese-font-and-utf8-data` vào nhánh `main`.
3. Đẩy (push) lên GitHub remote.
4. Bắt đầu phân hệ Giao diện Web Khách Hàng (Tạo nhánh `feature/customer-web-ui`, trình bày Bước 1 Đề xuất Ý tưởng & Giải pháp).
