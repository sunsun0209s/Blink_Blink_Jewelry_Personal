# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Đã gộp nhánh `feature/sales-module` vào nhánh `main` và đẩy lên GitHub:**
  - Hoàn thiện toàn bộ Phân hệ Quản lý Bán hàng (Sales Subsystem) khớp 100% Diagram.
- **Triển khai Phân hệ Quản lý Dịch vụ & Bảo hành (Warranty Subsystem) trên nhánh `feature/warranty-module`:**
  - **Domain Entities:**
    - `src/BlinkBlink.Domain/Entities/Warranty/PhieuBaoHanh.cs`: Quản lý các thuộc tính `SoPhieuBH`, `MaKhachHang`, `MaSanPham`, `LyDoBaoHanh`, `ChiPhiBaoHanh`, `TrangThaiXuLy`, `NgayTiepNhan`, `NgayHenTra` và các hàm nghiệp vụ `CapNhatTrangThai()`, `HoanTatBaoHanh()`, `TinhPhiBaoHanh()`.
    - `src/BlinkBlink.Domain/Interfaces/IBaoHanhRepository.cs`: Giao diện Repository quản lý phiếu bảo hành kèm các mở rộng Task async.
  - **Application Layer (DTOs & Use Cases):**
    - `src/BlinkBlink.Application/DTOs/Warranty/TaoBaoHanhDTO.cs`
    - `src/BlinkBlink.Application/DTOs/Warranty/CapNhatTrangThaiDTO.cs`
    - `src/BlinkBlink.Application/Services/Warranty/TiepNhanBaoHanhUseCase.cs`: Tạo phiếu bảo hành và cập nhật trạng thái sản phẩm sang `DangBaoHanh`.
    - `src/BlinkBlink.Application/Services/Warranty/XuLyBaoHanhUseCase.cs`: Cập nhật tiến độ `CapNhatTienDo()`, hoàn tất quyết toán chi phí và hoàn trạng thái trang sức `HoanTatBaoHanh()`.
  - **Infrastructure Layer:**
    - `src/BlinkBlink.Infrastructure/Data/Configurations/PhieuBaoHanhConfiguration.cs`: Cấu hình Fluent API bảng `PhieuBaoHanh`.
    - Cập nhật `src/BlinkBlink.Infrastructure/Data/AppDbContext.cs`: Khai báo `DbSet<PhieuBaoHanh> PhieuBaoHanhs`.
    - `src/BlinkBlink.Infrastructure/Repositories/BaoHanhRepository.cs`: Triển khai đầy đủ các phương thức truy vấn đồng bộ và bất đồng bộ.
  - **Presentation Layer (Web RESTful API):**
    - `src/BlinkBlink.Web/Controllers/BaoHanhController.cs`: 5 API endpoints chuẩn REST (`tiep-nhan`, `{soPhieu}/trang-thai`, `khach-hang/{maKH}`, `{soPhieu}`, `danh-sach`).
    - Cập nhật `src/BlinkBlink.Web/Program.cs`: Đăng ký DI cho `IBaoHanhRepository`, `TiepNhanBaoHanhUseCase`, `XuLyBaoHanhUseCase`.
  - **Unit Testing:**
    - `tests/BlinkBlink.UnitTests/Services/Warranty/TiepNhanBaoHanhUseCaseTests.cs` (3 tests).
    - `tests/BlinkBlink.UnitTests/Services/Warranty/XuLyBaoHanhUseCaseTests.cs` (3 tests).
    - Toàn bộ 22/22 tests pass 100%, Solution build 0 lỗi 0 cảnh báo.

## 2. Những gì đang làm dở & Các quyết định kỹ thuật đã chốt
- Quyết định kiến trúc chuẩn:
  1. Toàn bộ mã nguồn tuân thủ Clean Architecture trên nền tảng duy nhất .NET 8 LTS.
  2. Giao diện (UI Forms Admin & Web Views Khách hàng) được để lại làm ở giai đoạn cuối theo phong cách sang trọng (Taste Skill).
  3. Đã hoàn thành 4/5 phân hệ cốt lõi: Identity, Catalog & Kho, Bán hàng, Dịch vụ & Bảo hành.
- Phân hệ cuối cùng về backend là **Phân hệ 5: Báo cáo & Thống kê (Reporting & Analytics)**.

## 3. Hành động cụ thể tiếp theo
1. Cam kết mã nguồn và đẩy nhánh `feature/warranty-module` lên GitHub repository.
2. Gộp nhánh `feature/warranty-module` vào `main` (hoặc tạo PR).
3. Triển khai phân hệ backend cuối cùng: **Phân hệ Báo cáo & Thống kê (Reporting)**.
