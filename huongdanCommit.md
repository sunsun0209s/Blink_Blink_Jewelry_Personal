# HƯỚNG DẪN ĐỒNG BỘ & COMMIT MÃ NGUỒN CHO THÀNH VIÊN NHÓM QUA ANTIGRAVITY
**Dự án:** Blink Blink Jewelry  
**Dành cho các thành viên:** `qhy`, `sun`, `anhthy`, `tlinh`  
**Mốc đồng bộ Đợt 1 (Phase 1 Baseline):** `7993495` *(ĐÃ HOÀN TẤT)*  
**Mốc đồng bộ Đợt 2 (Phase 2 Target):** `4ea7311` *(ĐANG THỰC HIỆN)*  
**Tài liệu tham chiếu:** [scheduleCommit.md](scheduleCommit.md)

---

## 1. MỤC ĐÍCH & Ý NGHĨA

Tài liệu này hướng dẫn từng thành viên trong nhóm đưa các tệp mã nguồn sạch thuộc phân hệ của mình từ kho cá nhân (**Personal**) sang kho dự án chính của nhóm (**Team Repository**) **chỉ trong 1 lần thực hiện duy nhất cho mỗi đợt**.

> [!TIP]
> **Ưu điểm khi dùng Antigravity hỗ trợ đồng bộ theo kế hoạch:**
> - **Nhanh gọn tuyệt đối:** Mỗi người chỉ cần tạo 1 nhánh `feature/...` duy nhất cho toàn bộ phân hệ của mình, chạy 1 lượt là xong 100%.
> - **Chính xác 100%:** Tự động đối chiếu và lấy đúng các file `.cs`, `.csproj` đã được phân công theo mốc baseline `4ea7311`.
> - **Chống xung đột (Zero Merge Conflict):** Mỗi thành viên phụ trách một module riêng biệt nên không lo bị trùng lặp file.
> - **An toàn tuyệt đối:** Loại bỏ hoàn toàn nguy cơ commit nhầm file rác / Build Artifacts (`bin/`, `obj/`, `.vs/`).

---

## 2. CHUẨN BỊ TRƯỚC KHI THỰC HIỆN ĐỢT 2
[qhy]
1. **Kho mã nguồn Personal:** Đảm bảo bạn đã clone hoặc pull nhánh `main` mới nhất (chứa commit `4ea7311`) về máy:
   - Thư mục mẫu: `D:\Học tập\Năm 3\Học kì 1\Blink_Blink_Jewelry_Personal`
2. **Kho mã nguồn Dự án chính của nhóm (Team Repo):** Mở thư mục này trong IDE có tích hợp trợ lý Antigravity:
   - Thư mục mẫu: `D:\Học tập\Năm 3\Học kì 1\Blink_Blink_Jewelry`
3. Đảm bảo kho dự án chính đã được cập nhật mới nhất từ `main`:
   ```bash
   git checkout main
   git pull origin main
   ```
---
[sun]
1. **Kho mã nguồn Personal:** Đảm bảo bạn đã clone hoặc pull nhánh `main` mới nhất (chứa commit `4ea7311`) về máy:
   - Thư mục mẫu: `D:\Học tập\Năm 3\Học kì 1\Blink_Blink_Jewelry_Personal`
2. **Kho mã nguồn Dự án chính của nhóm (Team Repo):** Mở thư mục này trong IDE có tích hợp trợ lý Antigravity:
   - Thư mục mẫu: `D:\Học tập\Năm 3\Học kì 1\Blink_Blink_Jewelry`
3. Đảm bảo kho dự án chính đã được cập nhật mới nhất từ `main`:
   ```bash
   git checkout main
   git pull origin main
   ```
---
[anhthy]
1. **Kho mã nguồn Personal:** Đảm bảo bạn đã clone hoặc pull nhánh `main` mới nhất (chứa commit `4ea7311`) về máy:
   - Thư mục mẫu: `E:\NNLT CS\DoAn_CSharp_Personal`
2. **Kho mã nguồn Dự án chính của nhóm (Team Repo):** Mở thư mục này trong IDE có tích hợp trợ lý Antigravity:
   - Thư mục mẫu: `E:\NNLT CS\DoAn_CSharp\Blink_Blink_Jewelry`
3. Đảm bảo kho dự án chính đã được cập nhật mới nhất từ `main`:
   ```bash
   git checkout main
   git pull origin main
   ```
---
[tlinh]
1. **Kho mã nguồn Personal:** Đảm bảo bạn đã clone hoặc pull nhánh `main` mới nhất (chứa commit `4ea7311`) về máy:
   - Thư mục mẫu: `C:\Users\Asus\OneDrive - Saigon University\Desktop\C#\Blink_Blink_Jewelry_Personal`
2. **Kho mã nguồn Dự án chính của nhóm (Team Repo):** Mở thư mục này trong IDE có tích hợp trợ lý Antigravity:
   - Thư mục mẫu: `C:\Users\Asus\OneDrive - Saigon University\Desktop\C#\Blink_Blink_Jewelry`
3. Đảm bảo kho dự án chính đã được cập nhật mới nhất từ `main`:
   ```bash
   git checkout main
   git pull origin main
   ```

---

## 3. CÁC CÁCH RA LỆNH CHO ANTIGRAVITY CHO ĐỢT 2 (PROMPT TEMPLATES)

### Cách 1: Tự Động Hóa Hoàn Toàn (Khuyên Dùng - Nhanh & Chính Xác)
> **Dán đoạn prompt sau vào khung chat của Antigravity (thay đổi thông tin trong dấu ngoặc vuông):**
> 
> *"Chào Antigravity, tôi là **[Điền tên: qhy / sun / anhthy / tlinh]**.*  
> *Hôm nay tôi cần đưa toàn bộ mã nguồn phân hệ của mình từ kho Personal sang dự án chính cho **Đợt 2** theo tài liệu `huongdanCommit.md` và `scheduleCommit.md` (mốc commit baseline `4ea7311`).*  
> *- Thư mục chứa code Personal của tôi là: `[Đường dẫn thư mục Personal, ví dụ: C:\...\Blink_Blink_Jewelry_Personal]`*  
> *- Workspace hiện tại là thư mục dự án chung của nhóm.*  
> *Hãy đọc tài liệu `huongdanCommit.md`, tự động tạo nhánh `feature/phase2-...` tương ứng của tôi ở Đợt 2, copy toàn bộ các file Domain, Application, Infrastructure thuộc phân hệ của tôi từ Personal sang dự án chính, `git add` an toàn từng file và commit với thông điệp chuẩn Conventional Commit."*

**Antigravity sẽ tự động thực hiện:**
1. Đọc danh sách file được phân công cho bạn ở Mục 4.1 bên dưới.
2. Tạo nhánh làm việc: `git checkout -b feature/phase2-<tên_nhánh>`.
3. Sao chép nội dung chính xác từ thư mục Personal sang dự án nhóm.
4. `git add` chính xác từng file mã nguồn (không add file rác).
5. Thực hiện commit với thông điệp chuẩn và hướng dẫn bạn lệnh `git push` để mở Pull Request.

---

### Cách 2: Tự Thực Hiện Bằng Dòng Lệnh (Manual Git Commands)
Nếu bạn muốn tự tay gõ lệnh, hãy mở Terminal tại thư mục dự án nhóm và thực hiện:
```bash
git checkout main
git pull origin main
git checkout -b feature/phase2-<tên_nhánh_của_bạn>
```
Sau đó copy file và chạy lệnh `git add` + `git commit` + `git push` theo đúng khối lệnh ở Mục 4.1 dưới đây.

---

## 4. CHI TIẾT PHÂN CÔNG TỆP MÃ NGUỒN

### 4.1. PHÂN CÔNG ĐỢT 2 (PHASE 2 - HIỆN TẠI ĐANG THỰC HIỆN)

#### 1. Thành viên `qhy` (Đợt 2)
- **Phân hệ:** **Identity & Khách hàng** (Tìm kiếm SĐT & Xóa có ràng buộc chứng từ)
- **Tên nhánh:** `feature/phase2-identity-module`
- **Số lượng:** 7 tệp
- **Lệnh thực hiện:**
  ```bash
  git checkout -b feature/phase2-identity-module
  git add src/BlinkBlink.Domain/Interfaces/IKhachHangRepository.cs src/BlinkBlink.Domain/Interfaces/INhanVienRepository.cs src/BlinkBlink.Application/Services/Identity/DangKyKhachHangUseCase.cs src/BlinkBlink.Application/Services/Identity/QuanLyKhachHangUseCase.cs src/BlinkBlink.Application/Services/Identity/QuanLyNhanVienUseCase.cs src/BlinkBlink.Infrastructure/Repositories/KhachHangRepository.cs src/BlinkBlink.Infrastructure/Repositories/NhanVienRepository.cs
  git commit -m "feat(identity): cap nhat tim kiem theo sdt va xoa tai khoan kiem tra rang buoc chung tu"
  git push origin feature/phase2-identity-module
  ```

#### 2. Thành viên `sun` (Đợt 2)
- **Phân hệ:** **Catalog & Tồn kho sản phẩm** (Bổ sung SoLuongTon & Tính tồn kho tự động)
- **Tên nhánh:** `feature/phase2-catalog-module`
- **Số lượng:** 2 tệp
- **Lệnh thực hiện:**
  ```bash
  git checkout -b feature/phase2-catalog-module
  git add src/BlinkBlink.Domain/Entities/Catalog/SanPhamTrangSuc.cs src/BlinkBlink.Infrastructure/Repositories/SanPhamRepository.cs
  git commit -m "feat(catalog): bo sung thuoc tinh SoLuongTon va truy van ton kho tu dong thoi gian thuc"
  git push origin feature/phase2-catalog-module
  ```

#### 3. Thành viên `anhthy` (Đợt 2)
- **Phân hệ:** **Giỏ hàng, Đơn hàng & Nền tảng CSDL** (Bán hàng POS Offline & Tự khởi tạo CSDL)
- **Tên nhánh:** `feature/phase2-cart-orders-foundation-module`
- **Số lượng:** 8 tệp
- **Lệnh thực hiện:**
  ```bash
  git checkout -b feature/phase2-cart-orders-foundation-module
  git add src/BlinkBlink.Domain/Entities/Orders/ChiTietHoaDon.cs src/BlinkBlink.Domain/Interfaces/IHoaDonRepository.cs src/BlinkBlink.Application/DTOs/Orders/TaoHoaDonOfflineDTO.cs src/BlinkBlink.Application/Services/Orders/BanHangOfflineUseCase.cs src/BlinkBlink.Application/Services/Orders/DatHangUseCase.cs src/BlinkBlink.Infrastructure/BlinkBlink.Infrastructure.csproj src/BlinkBlink.Infrastructure/Data/DatabaseHelper.cs src/BlinkBlink.Infrastructure/Repositories/HoaDonRepository.cs
  git commit -m "feat(orders-data): tich hop UseCase ban hang POS offline va co che tu dong khoi tao CSDL"
  git push origin feature/phase2-cart-orders-foundation-module
  ```

#### 4. Thành viên `tlinh` (Đợt 2)
- **Phân hệ:** **Kho hàng & Bảo hành** (Phân định chi phí ước tính và chi phí thực tế)
- **Tên nhánh:** `feature/phase2-warranty-module`
- **Số lượng:** 4 tệp
- **Lệnh thực hiện:**
  ```bash
  git checkout -b feature/phase2-warranty-module
  git add src/BlinkBlink.Domain/Entities/Warranty/PhieuBaoHanh.cs src/BlinkBlink.Application/Services/Warranty/TiepNhanBaoHanhUseCase.cs src/BlinkBlink.Application/Services/Warranty/XuLyBaoHanhUseCase.cs src/BlinkBlink.Infrastructure/Repositories/BaoHanhRepository.cs
  git commit -m "feat(warranty): phan dinh chi phi uoc tinh va chi phi thuc te cho quy trinh bao hanh"
  git push origin feature/phase2-warranty-module
  ```

---

### 4.2. PHÂN CÔNG ĐỢT 1 (PHASE 1 - LỊCH SỬ ĐÃ HOÀN TẤT)

> [!NOTE]
> Thông tin dưới đây được bảo lưu nguyên vẹn để đối chiếu và tra cứu khi cần thiết.

- **`qhy` (Nhánh: `feature/identity-management-module` - 22 tệp):** `feat(identity): tich hop toan bo module NguoiDung, NhanVien, VaiTro va KhachHang` *(ĐÃ XONG)*
- **`sun` (Nhánh: `feature/catalog-reporting-module` - 17 tệp):** `feat(catalog-reporting): tich hop toan bo module SanPham, DanhMuc va BaoCaoThongKe` *(ĐÃ XONG)*
- **`anhthy` (Nhánh: `feature/cart-orders-foundation-module` - 14 tệp):** `feat(cart-orders): tich hop toan bo module GioHang, HoaDon va DatabaseHelper` *(ĐÃ XONG)*
- **`tlinh` (Nhánh: `feature/inventory-warranty-module` - 18 tệp):** `feat(inventory-warranty): tich hop toan bo module NhapKho, KiemKe va BaoHanh` *(ĐÃ XONG)*

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
>    git push origin feature/phase2-<tên_nhánh_của_bạn>
>    ```
> 2. Vào giao diện GitHub của nhóm, tạo Pull Request để gộp từ nhánh `feature/phase2-...` của bạn vào `main`.
> 3. Nhờ 1 thành viên khác trong nhóm kiểm tra (Review & Approve) trước khi Merge vào `main`.
> 4. **Cập nhật tiến độ:** Sau khi hoàn thành xong và PR đã được merge, mở file [scheduleCommit.md](scheduleCommit.md), chuyển tên của bạn từ mục `thành viên chưa thực hiện` sang mục `thành viên đã thực hiện` ở **Lần 2**.
