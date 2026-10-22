# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Tinh chỉnh Nghiệp vụ & Giao diện Phân hệ Sản phẩm & Kho hàng:**
  - **Bỏ nhập Giá vốn & Đơn giá bán khi tạo mới sản phẩm:** Sản phẩm vừa tạo mặc định giá vốn = 0, giá bán = 0, % giảm giá = 0, trạng thái 'HetHang' (tồn kho = 0).
  - **Công thức Giá nhập Bình quân Gia quyền khi Nhập kho:** Tích hợp trong [PhieuNhapKhoRepositoryImpl.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Infrastructure/Repositories/PhieuNhapKhoRepositoryImpl.cs) theo đúng công thức:
    $$\text{GiaNhapMoi} = \frac{(\text{SoLuongTon} \times \text{GiaNhapHienTai}) + (\text{SoLuongNhap} \times \text{GiaVonNhapMoi})}{\text{SoLuongTon} + \text{SoLuongNhap}}$$
  - **Chỉnh sửa giá bán & % Giảm giá trong Modal Sửa sản phẩm ([ProductEditDialog.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Catalog/ProductEditDialog.cs)):** Cho phép chỉnh sửa đơn giá bán và % giảm giá (NumericUpDown 0 - 100%, mặc định 0%); hiển thị cột `% GIẢM GIÁ` nổi bật trên bảng sản phẩm ([ProductsView.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/UserControls/Catalog/ProductsView.cs)).
  - **Bật Cuộn ngang (Horizontal Scrolling) cho DataGridView:** Cấu hình `AutoSizeColumnsMode = None`, `ScrollBars = ScrollBars.Both`, độ rộng cột tối ưu tổng 1345px giúp bảng cuộn ngang mượt mà trên mọi kích thước màn hình mà không bị co giật cột.
  - **Tự động giãn chiều cao dòng và tiêu đề theo chữ:** Cấu hình trong [UITheme.cs](file:///c:/Users/huy/Downloads/code/laptrinhCSharp/Clone_Blink_Blink/Blink_Blink_Jewelry_Personal/src/BlinkBlink.Admin/Views/Helpers/UITheme.cs) với `AutoSizeRowsMode = AllCells`, `WrapMode = TriState.True`, chiều cao tiêu đề 52px chống hoàn toàn hiện tượng mất chữ hoặc rớt dòng.
  - **Nới rộng toàn diện các Hộp thoại Modal:**
    - `ProductEditDialog`: Mở rộng lên 880x700px.
    - `CategoryView` Modal: Mở rộng lên 680x520px.
    - `InventoryInwardView` Modal: Mở rộng lên 1000x740px.
  - **Cơ sở dữ liệu & Kiểm thử:** Bổ sung cột `PhanTramGiamGia INT NOT NULL DEFAULT 0` vào bảng `dbo.SanPhamTrangSuc` trên SQL Server cục bộ và các tệp SQL kịch bản; toàn bộ 31/31 unit & integration tests pass 100%, biên dịch 0 lỗi, 0 cảnh báo.

## 2. Những gì đang làm dở & Các quyết định kỹ thuật đã chốt
- Hoàn tất 100% tất cả các yêu cầu về nghiệp vụ giá nhập bình quân, % giảm giá, cuộn ngang và nới rộng layout cho desktop admin.
- Nhánh hiện tại: `feature/catalog-inventory-refactor`.

## 3. Hành động cụ thể tiếp theo cho phiên làm việc sau
1. Merge pull request nhánh `feature/catalog-inventory-refactor` vào `main`.
2. Bắt đầu phân hệ Giao diện Web Khách Hàng (`BlinkBlink.Web`) mua sắm, giỏ hàng, tra cứu bảo hành kết nối Web API theo chuẩn Clean Architecture và Taste Skill.
