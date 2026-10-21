# BẢNG GHI NHỚ TIẾN ĐỘ & NGỮ CẢNH (SCRATCHPAD)

## 1. Những gì đã hoàn thành
- **Cập nhật Diagram:**
  - File: `c:\Users\huy\Downloads\class diagram bán trang sức\Class Diagram bán trang sức-Phân hệ quản lý sản phẩm & kho.drawio.xml` và `XML/Phân hệ quản lý sản phẩm và kho hàng.xml`.
  - Đã thêm các khối: `DoiGiaDTO`, `ChiTietNhapKhoDTO`, `IPhieuKiemKeRepository`, `PhieuKiemKeRepository`, mở rộng khung frame tương ứng.
- **Tạo nhánh:** `feature/product-inventory-management`.
- **Domain Layer:**
  - `src/BlinkBlink.Domain/Entities/Catalog/SanPhamTrangSuc.cs` (đồng bộ `Size` kiểu string, methods `KiemTraTonKho`, `CapNhatTrangThai`, `ToString`).
  - `src/BlinkBlink.Domain/Entities/Catalog/DanhMuc.cs` (methods `LayDanhSachSanPham`, `CapNhatDanhMuc`).
  - `src/BlinkBlink.Domain/Entities/Inventory/PhieuNhapKho.cs`
  - `src/BlinkBlink.Domain/Entities/Inventory/ChiTietNhapKho.cs`
  - `src/BlinkBlink.Domain/Entities/Inventory/PhieuKiemKe.cs`
  - `src/BlinkBlink.Domain/Entities/Inventory/ChiTietKiemKe.cs`
  - `src/BlinkBlink.Domain/Interfaces/ISanPhamRepository.cs`
  - `src/BlinkBlink.Domain/Interfaces/IDanhMucRepository.cs`
  - `src/BlinkBlink.Domain/Interfaces/IPhieuNhapKhoRepository.cs`
  - `src/BlinkBlink.Domain/Interfaces/IPhieuKiemKeRepository.cs`
- **Application Layer:**
  - `src/BlinkBlink.Application/DTOs/Catalog/SanPhamDTO.cs`
  - `src/BlinkBlink.Application/DTOs/Catalog/DoiGiaDTO.cs`
  - `src/BlinkBlink.Application/DTOs/Inventory/NhapKhoDTO.cs`
  - `src/BlinkBlink.Application/DTOs/Inventory/ChiTietNhapKhoDTO.cs`
  - `src/BlinkBlink.Application/Services/Catalog/QuanLySanPhamUseCase.cs`
  - `src/BlinkBlink.Application/Services/Inventory/XacNhanNhapKhoUseCase.cs`
- **Infrastructure Layer:**
  - `src/BlinkBlink.Infrastructure/Data/Configurations/SanPhamTrangSucConfiguration.cs`
  - `src/BlinkBlink.Infrastructure/Data/Configurations/DanhMucConfiguration.cs`
  - `src/BlinkBlink.Infrastructure/Data/Configurations/PhieuNhapKhoConfiguration.cs`
  - `src/BlinkBlink.Infrastructure/Data/Configurations/ChiTietNhapKhoConfiguration.cs`
  - `src/BlinkBlink.Infrastructure/Data/Configurations/PhieuKiemKeConfiguration.cs`
  - `src/BlinkBlink.Infrastructure/Data/Configurations/ChiTietKiemKeConfiguration.cs`
  - `src/BlinkBlink.Infrastructure/Data/AppDbContext.cs` (Khai báo đủ 6 `DbSet`).
  - `src/BlinkBlink.Infrastructure/Repositories/SanPhamRepository.cs`
  - `src/BlinkBlink.Infrastructure/Repositories/DanhMucRepository.cs`
  - `src/BlinkBlink.Infrastructure/Repositories/PhieuNhapKhoRepositoryImpl.cs`
  - `src/BlinkBlink.Infrastructure/Repositories/PhieuKiemKeRepository.cs`
- **Presentation Layer:**
  - `src/BlinkBlink.Web/Controllers/SanPhamController.cs`
  - `src/BlinkBlink.Web/Controllers/PhieuNhapKhoController.cs`
- **Kiểm thử biên dịch:** Toàn bộ Solution (.NET 8 LTS) build thành công 0 lỗi, 0 cảnh báo; toàn bộ Unit Tests & Integration Tests Passed.

## 2. Những gì đang làm dở & Các quyết định kỹ thuật đã chốt
- Đã chốt kiến trúc Clean Architecture chuẩn .NET 8 LTS cho Phân hệ Quản lý Sản phẩm & Kho hàng.
- Tất cả các lớp, thuộc tính, kiểu dữ liệu và phương thức khớp 100% với Class Diagram.

## 3. Hành động cụ thể tiếp theo
1. Commit và push nhánh `feature/product-inventory-management` lên remote repository.
2. Tạo Pull Request trên GitHub để review và gộp vào `main`.
3. Chuyển sang phân hệ tiếp theo theo kế hoạch của nhóm.
