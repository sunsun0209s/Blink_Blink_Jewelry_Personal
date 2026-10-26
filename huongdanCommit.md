# HƯỚNG DẪN ĐỒNG BỘ & COMMIT MÃ NGUỒN CHO THÀNH VIÊN NHÓM QUA ANTIGRAVITY (1 LẦN HOÀN THÀNH)
**Dự án:** Blink Blink Jewelry  
**Dành cho các thành viên:** `qhy`, `sun`, `anhthy`, `tlinh`  
**Mốc đồng bộ Baseline (Personal):** `7993495`  
**Tài liệu tham chiếu:** [scheduleCommit.md](scheduleCommit.md)

---

## 1. MỤC ĐÍCH & Ý NGHĨA

Tài liệu này hướng dẫn từng thành viên trong nhóm đưa toàn bộ mã nguồn thuộc phân hệ của mình từ kho cá nhân (**Personal**) sang kho dự án chính của nhóm (**Team Repository**) **chỉ trong 1 lần thực hiện duy nhất**, thay vì phải chia nhỏ làm nhiều ngày.

> [!TIP]
> **Ưu điểm khi dùng Antigravity hỗ trợ đồng bộ 1 lần:**
> - **Nhanh gọn tuyệt đối:** Mỗi người chỉ cần tạo 1 nhánh `feature/...` duy nhất cho toàn bộ phân hệ của mình, chạy 1 lượt là xong 100%.
> - **Chính xác 100%:** Tự động đối chiếu và lấy đúng các file `.cs`, `.csproj` đã được phân công theo mốc baseline `7993495`.
> - **Chống xung đột (Zero Merge Conflict):** Mỗi thành viên phụ trách một module riêng biệt nên không lo bị trùng lặp file.
> - **An toàn tuyệt đối:** Loại bỏ hoàn toàn nguy cơ commit nhầm file rác / Build Artifacts (`bin/`, `obj/`, `.vs/`).

---

## 2. CHUẨN BỊ TRƯỚC KHI THỰC HIỆN

1. **Kho mã nguồn Personal:** Đảm bảo bạn đã clone hoặc pull nhánh `main` mới nhất (chứa commit `7993495`) về máy:
   - Thư mục mẫu: `C:\Users\huy\Downloads\code\laptrinhCSharp\Clone_Blink_Blink\Blink_Blink_Jewelry_Personal`
2. **Kho mã nguồn Dự án chính của nhóm (Team Repo):** Mở thư mục này trong IDE có tích hợp trợ lý Antigravity:
   - Thư mục mẫu: `C:\Users\huy\Downloads\code\laptrinhCSharp\Blink_Blink_Jewelry`
3. Đảm bảo kho dự án chính đã được cập nhật mới nhất từ `main`:
   ```bash
   git checkout main
   git pull origin main
   ```

---

## 3. CÁC CÁCH RA LỆNH CHO ANTIGRAVITY (PROMPT TEMPLATES)

### Cách 1: Tự Động Hóa Hoàn Toàn (Khuyên Dùng - Nhanh & Chính Xác)
> **Dán đoạn prompt sau vào khung chat của Antigravity (thay đổi thông tin trong dấu ngoặc vuông):**
> 
> *"Chào Antigravity, tôi là **[Điền tên: qhy / sun / anhthy / tlinh]**.*  
> *Hôm nay tôi cần đưa toàn bộ mã nguồn phân hệ của mình từ kho Personal sang dự án chính trong 1 lần duy nhất theo tài liệu `huongdanCommit.md` (mốc commit baseline `7993495`).*  
> *- Thư mục chứa code Personal của tôi là: `[Đường dẫn thư mục Personal, ví dụ: C:\...\Blink_Blink_Jewelry_Personal]`*  
> *- Workspace hiện tại là thư mục dự án chung của nhóm.*  
> *Hãy đọc tài liệu `huongdanCommit.md`, tự động tạo nhánh `feature/...` tương ứng của tôi, copy toàn bộ các file Domain, Application, Infrastructure thuộc phân hệ của tôi từ Personal sang dự án chính, `git add` an toàn từng file và commit với thông điệp chuẩn Conventional Commit."*

**Antigravity sẽ tự động thực hiện:**
1. Đọc danh sách file được phân công cho bạn ở Mục 4 bên dưới.
2. Tạo nhánh làm việc: `git checkout -b feature/<tên_nhánh>`.
3. Sao chép nội dung chính xác từ thư mục Personal sang dự án nhóm.
4. `git add` chính xác từng file mã nguồn (không add file rác).
5. Thực hiện commit với thông điệp chuẩn và hướng dẫn bạn lệnh `git push` để mở Pull Request.

---

### Cách 2: Tự Thực Hiện Bằng Dòng Lệnh (Manual Git Commands)
Nếu bạn muốn tự tay gõ lệnh, hãy mở Terminal tại thư mục dự án nhóm và thực hiện theo 5 bước:

1. **Kéo code mới nhất từ main:**
   ```bash
   git checkout main
   git pull origin main
   ```
2. **Tạo nhánh tính năng:**
   ```bash
   git checkout -b feature/<tên_nhánh_của_bạn>
   ```
3. **Sao chép các file thuộc phân hệ của bạn** từ thư mục `Personal` sang đúng vị trí trong dự án chính (tra cứu bảng ở Mục 4).
4. **Add file và Commit:**
   ```bash
   git add <danh_sách_các_file_của_bạn>
   git commit -m "feat(<phân_hệ>): tich hop toan bo module vao he thong Clean Architecture"
   ```
5. **Đẩy lên GitHub và tạo Pull Request (PR):**
   ```bash
   git push origin feature/<tên_nhánh_của_bạn>
   ```

---

## 4. BẢNG PHÂN CÔNG & DANH MỤC TỆP ĐỒNG BỘ THEO THÀNH VIÊN

### 1. Thành viên `qhy`
- **Phân hệ phụ trách:** **Identity & Khách hàng** (Người dùng, Nhân viên, Vai trò, Khách hàng)
- **Tên nhánh duy nhất:** `feature/identity-management-module`
- **Thông điệp Commit đề xuất:** `feat(identity): tich hop toan bo module NguoiDung, NhanVien, VaiTro va KhachHang`
- **Danh sách toàn bộ tệp cần đồng bộ (22 tệp):**
  - **Domain (9 tệp):**
    - `src/BlinkBlink.Domain/BlinkBlink.Domain.csproj`
    - `src/BlinkBlink.Domain/Entities/Identity/NguoiDung.cs`
    - `src/BlinkBlink.Domain/Entities/Identity/VaiTro.cs`
    - `src/BlinkBlink.Domain/Entities/Identity/NhanVien.cs`
    - `src/BlinkBlink.Domain/Entities/Customers/KhachHang.cs`
    - `src/BlinkBlink.Domain/Interfaces/INguoiDungRepository.cs`
    - `src/BlinkBlink.Domain/Interfaces/IVaiTroRepository.cs`
    - `src/BlinkBlink.Domain/Interfaces/IKhachHangRepository.cs`
    - `src/BlinkBlink.Domain/Interfaces/INhanVienRepository.cs`
  - **Application (9 tệp):**
    - `src/BlinkBlink.Application/BlinkBlink.Application.csproj`
    - `src/BlinkBlink.Application/DTOs/Identity/DangNhapDTO.cs`
    - `src/BlinkBlink.Application/DTOs/Identity/DangKyKhachHangDTO.cs`
    - `src/BlinkBlink.Application/DTOs/Identity/TaoNhanVienDTO.cs`
    - `src/BlinkBlink.Application/DTOs/Identity/CapNhatKhachHangDTO.cs`
    - `src/BlinkBlink.Application/Services/Identity/DangNhapUseCase.cs`
    - `src/BlinkBlink.Application/Services/Identity/DangKyKhachHangUseCase.cs`
    - `src/BlinkBlink.Application/Services/Identity/QuanLyKhachHangUseCase.cs`
    - `src/BlinkBlink.Application/Services/Identity/QuanLyNhanVienUseCase.cs`
  - **Infrastructure (4 tệp):**
    - `src/BlinkBlink.Infrastructure/Repositories/KhachHangRepository.cs`
    - `src/BlinkBlink.Infrastructure/Repositories/NguoiDungRepository.cs`
    - `src/BlinkBlink.Infrastructure/Repositories/VaiTroRepository.cs`
    - `src/BlinkBlink.Infrastructure/Repositories/NhanVienRepository.cs`

---

### 2. Thành viên `sun`
- **Phân hệ phụ trách:** **Catalog & Báo cáo Thống kê** (Danh mục, Sản phẩm trang sức, Thống kê doanh thu / kho)
- **Tên nhánh duy nhất:** `feature/catalog-reporting-module`
- **Thông điệp Commit đề xuất:** `feat(catalog-reporting): tich hop toan bo module SanPham, DanhMuc va BaoCaoThongKe`
- **Danh sách toàn bộ tệp cần đồng bộ (17 tệp):**
  - **Domain (5 tệp):**
    - `src/BlinkBlink.Domain/Entities/Catalog/DanhMuc.cs`
    - `src/BlinkBlink.Domain/Entities/Catalog/SanPhamTrangSuc.cs`
    - `src/BlinkBlink.Domain/Interfaces/IDanhMucRepository.cs`
    - `src/BlinkBlink.Domain/Interfaces/ISanPhamRepository.cs`
    - `src/BlinkBlink.Domain/Interfaces/IThongKeRepository.cs`
  - **Application (9 tệp):**
    - `src/BlinkBlink.Application/DTOs/Catalog/SanPhamDTO.cs`
    - `src/BlinkBlink.Application/DTOs/Catalog/CapNhatSanPhamDTO.cs`
    - `src/BlinkBlink.Application/DTOs/Catalog/DoiGiaDTO.cs`
    - `src/BlinkBlink.Application/DTOs/Reporting/BaoCaoTonKhoDTO.cs`
    - `src/BlinkBlink.Application/DTOs/Reporting/DoanhThuThongKeDTO.cs`
    - `src/BlinkBlink.Application/DTOs/Reporting/TopSanPhamBanChayDTO.cs`
    - `src/BlinkBlink.Application/Services/Catalog/QuanLySanPhamUseCase.cs`
    - `src/BlinkBlink.Application/Services/Reporting/ThongKeDoanhThuUseCase.cs`
    - `src/BlinkBlink.Application/Services/Reporting/ThongKeKhoHangUseCase.cs`
  - **Infrastructure (3 tệp):**
    - `src/BlinkBlink.Infrastructure/Repositories/DanhMucRepository.cs`
    - `src/BlinkBlink.Infrastructure/Repositories/SanPhamRepository.cs`
    - `src/BlinkBlink.Infrastructure/Repositories/ThongKeRepository.cs`

---

### 3. Thành viên `anhthy`
- **Phân hệ phụ trách:** **Giỏ hàng, Đơn hàng & Nền tảng Kết nối CSDL** (Giỏ hàng, Hóa đơn, DatabaseHelper)
- **Tên nhánh duy nhất:** `feature/cart-orders-foundation-module`
- **Thông điệp Commit đề xuất:** `feat(cart-orders): tich hop toan bo module GioHang, HoaDon va DatabaseHelper`
- **Danh sách toàn bộ tệp cần đồng bộ (14 tệp):**
  - **Domain (6 tệp):**
    - `src/BlinkBlink.Domain/Entities/Cart/GioHang.cs`
    - `src/BlinkBlink.Domain/Entities/Cart/ChiTietGioHang.cs`
    - `src/BlinkBlink.Domain/Entities/Orders/HoaDon.cs`
    - `src/BlinkBlink.Domain/Entities/Orders/ChiTietHoaDon.cs`
    - `src/BlinkBlink.Domain/Interfaces/IGioHangRepository.cs`
    - `src/BlinkBlink.Domain/Interfaces/IHoaDonRepository.cs`
  - **Application (4 tệp):**
    - `src/BlinkBlink.Application/DTOs/Orders/DatHangDTO.cs`
    - `src/BlinkBlink.Application/DTOs/Orders/CapNhatDonHangDTO.cs`
    - `src/BlinkBlink.Application/Services/Orders/DatHangUseCase.cs`
    - `src/BlinkBlink.Application/Services/Orders/XuLyDonHangUseCase.cs`
  - **Infrastructure (4 tệp):**
    - `src/BlinkBlink.Infrastructure/BlinkBlink.Infrastructure.csproj`
    - `src/BlinkBlink.Infrastructure/Data/DatabaseHelper.cs`
    - `src/BlinkBlink.Infrastructure/Repositories/GioHangRepository.cs`
    - `src/BlinkBlink.Infrastructure/Repositories/HoaDonRepository.cs`

---

### 4. Thành viên `tlinh`
- **Phân hệ phụ trách:** **Kho hàng & Bảo hành** (Phiếu nhập kho, Phiếu kiểm kê, Sổ bảo hành)
- **Tên nhánh duy nhất:** `feature/inventory-warranty-module`
- **Thông điệp Commit đề xuất:** `feat(inventory-warranty): tich hop toan bo module NhapKho, KiemKe va BaoHanh`
- **Danh sách toàn bộ tệp cần đồng bộ (18 tệp):**
  - **Domain (8 tệp):**
    - `src/BlinkBlink.Domain/Entities/Inventory/PhieuNhapKho.cs`
    - `src/BlinkBlink.Domain/Entities/Inventory/ChiTietNhapKho.cs`
    - `src/BlinkBlink.Domain/Entities/Inventory/PhieuKiemKe.cs`
    - `src/BlinkBlink.Domain/Entities/Inventory/ChiTietKiemKe.cs`
    - `src/BlinkBlink.Domain/Entities/Warranty/PhieuBaoHanh.cs`
    - `src/BlinkBlink.Domain/Interfaces/IPhieuNhapKhoRepository.cs`
    - `src/BlinkBlink.Domain/Interfaces/IPhieuKiemKeRepository.cs`
    - `src/BlinkBlink.Domain/Interfaces/IBaoHanhRepository.cs`
  - **Application (7 tệp):**
    - `src/BlinkBlink.Application/DTOs/Inventory/NhapKhoDTO.cs`
    - `src/BlinkBlink.Application/DTOs/Inventory/ChiTietNhapKhoDTO.cs`
    - `src/BlinkBlink.Application/DTOs/Warranty/TaoBaoHanhDTO.cs`
    - `src/BlinkBlink.Application/DTOs/Warranty/CapNhatTrangThaiDTO.cs`
    - `src/BlinkBlink.Application/Services/Inventory/XacNhanNhapKhoUseCase.cs`
    - `src/BlinkBlink.Application/Services/Warranty/TiepNhanBaoHanhUseCase.cs`
    - `src/BlinkBlink.Application/Services/Warranty/XuLyBaoHanhUseCase.cs`
  - **Infrastructure (3 tệp):**
    - `src/BlinkBlink.Infrastructure/Repositories/BaoHanhRepository.cs`
    - `src/BlinkBlink.Infrastructure/Repositories/PhieuNhapKhoRepositoryImpl.cs`
    - `src/BlinkBlink.Infrastructure/Repositories/PhieuKiemKeRepository.cs`

---

## 5. NGUYÊN TẮC BẮT BUỘC ĐỂ KHÔNG BỊ LỖI

> [!WARNING]
> **1. TUYỆT ĐỐI KHÔNG COMMIT BUILD ARTIFACTS / TỆP RÁC**
> - Không bao giờ gõ `git add .` hoặc `git add -A` bừa bãi.
> - Tuyệt đối không commit các thư mục: `bin/`, `obj/`, `.vs/`, `.vscode/`, `TestResults/` hoặc tệp `*.user`.
> - Luôn chạy lệnh `git status` để kiểm tra danh sách file trước khi commit.

> [!IMPORTANT]
> **2. QUY TRÌNH PULL REQUEST (PR) & CẬP NHẬT TRẠNG THÁI**
> 1. Sau khi commit, đẩy nhánh lên GitHub:
>    ```bash
>    git push origin feature/<tên_nhánh_của_bạn>
>    ```
> 2. Vào giao diện GitHub của nhóm, tạo Pull Request để gộp từ nhánh `feature/...` của bạn vào `main`.
> 3. Nhờ 1 thành viên khác trong nhóm kiểm tra (Review & Approve) trước khi Merge vào `main`.
> 4. **Cập nhật tiến độ:** Sau khi hoàn thành xong và PR đã được merge, mở file [scheduleCommit.md](scheduleCommit.md), chuyển tên của bạn từ mục `thành viên chưa thực hiện` sang mục `thành viên đã thực hiện`.
