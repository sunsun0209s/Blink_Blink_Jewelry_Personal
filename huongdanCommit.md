# HƯỚNG DẪN ĐỒNG BỘ & COMMIT MÃ NGUỒN CHO THÀNH VIÊN NHÓM QUA ANTIGRAVITY
**Dự án:** Blink Blink Jewelry  
**Dành cho các thành viên:** `qhy`, `sun`, `anhthy`, `tlinh`  
**Tài liệu lộ trình gốc:** [scheduleCommit.md](scheduleCommit.md)

---

## 1. MỤC ĐÍCH & Ý NGHĨA

Tài liệu này hướng dẫn các thành viên trong nhóm cách sử dụng trợ lý lập trình **Antigravity** để đưa mã nguồn từ kho cá nhân (**Personal**) sang kho dự án chính của nhóm (**Team Repository**) theo đúng lộ trình 3 ngày đã được hoạch định trong [scheduleCommit.md](scheduleCommit.md).

> [!TIP]
> **Ưu điểm khi dùng Antigravity hỗ trợ:**
> - **Chính xác 100%:** Tự động đối chiếu đúng file `.cs`, `.csproj` thuộc phân hệ của từng người.
> - **Chống xung đột (Zero Merge Conflict):** Mỗi người làm một phân hệ riêng biệt theo nhánh `feature/...`.
> - **An toàn tuyệt đối:** Loại bỏ hoàn toàn nguy cơ commit nhầm file rác / Build Artifacts (`bin/`, `obj/`, `.vs/`).

---

## 2. CHUẨN BỊ TRƯỚC KHI THỰC HIỆN

1. **Kho mã nguồn Personal:** Đảm bảo bạn đã clone nhánh `main` mới nhất của dự án Personal về máy:
   - Thư mục mẫu: `D:\Code\Blink_Blink_Jewelry_Personal`
2. **Kho mã nguồn Dự án chính của nhóm (Team Repo):** Mở thư mục này trong IDE có tích hợp Antigravity:
   - Thư mục mẫu: `D:\Code\BlinkBlink_Team_Project`
3. Đảm bảo kho dự án chính đã được kéo mới nhất từ `main`:
   ```bash
   git checkout main
   git pull origin main
   ```

---

## 3. CÁC CÁCH RA LỆNH CHO ANTIGRAVITY (PROMPT TEMPLATES)

Tùy vào việc bạn muốn Antigravity tự động thực hiện hay hướng dẫn bạn từng bước, hãy copy một trong hai mẫu prompt bên dưới và gửi cho Antigravity:

### Cách 1: Tự Động Hóa Hoàn Toàn (Khuyên Dùng - Nhanh & Chính Xác)
> **Dán đoạn sau vào khung chat của Antigravity (thay đổi thông tin trong dấu ngoặc vuông):**
> 
> *"Chào Antigravity, tôi là **[Điền tên: qhy / sun / anhthy / tlinh]**.*  
> *Hôm nay tôi cần thực hiện các commit cho **[Điền ngày: Ngày 1 / Ngày 2 / Ngày 3 - Sáng hoặc Chiều]** theo file `scheduleCommit.md`.*  
> *- Thư mục chứa code Personal của tôi là: `[Đường dẫn thư mục Personal, ví dụ: D:\Code\Blink_Blink_Jewelry_Personal]`*  
> *- Workspace hiện tại là thư mục dự án chung của nhóm.*  
> *Hãy đọc file `scheduleCommit.md`, tự động copy các file mã nguồn thuộc lượt của tôi từ Personal sang dự án chính, tạo đúng nhánh `feature/...`, `git add` an toàn và commit đúng thông điệp quy định."*

**Antigravity sẽ tự động thực hiện:**
1. Đọc [scheduleCommit.md](scheduleCommit.md) để lấy đúng danh sách file `.cs` cần commit.
2. Sao chép nội dung chính xác từ thư mục Personal sang dự án nhóm.
3. Tạo nhánh mới: `git checkout -b feature/<tên_nhánh>`.
4. Add chính xác từng file và commit với thông điệp chuẩn (Conventional Commit).

---

### Cách 2: Hướng Dẫn Từng Bước (Dành Cho Ai Muốn Tự Gõ Lệnh)
> **Dán đoạn sau vào khung chat của Antigravity:**
> 
> *"Chào Antigravity, tôi là **[Điền tên: qhy / sun / anhthy / tlinh]**.*  
> *Hôm nay tôi cần đưa code của **[Điền ngày: Ngày 1 / Ngày 2 / Ngày 3]** từ Personal sang dự án chính.*  
> *Hãy đọc file `scheduleCommit.md` và hướng dẫn chi tiết từng bước cho tôi: cần copy những file nào, và cung cấp sẵn các câu lệnh Git (checkout branch, git add, git commit) để tôi chỉ cần copy-paste chạy."*

**Antigravity sẽ phản hồi:**
- Danh sách rõ ràng từng file `.cs` bạn cần lấy.
- Từng câu lệnh Git viết sẵn để bạn copy chạy trực tiếp trên Terminal.

---

## 4. TÓM TẮT PHÂN CÔNG THEO THÀNH VIÊN

| Thành viên | Phân Hệ Nghiệp Vụ Chính | Nhánh Git Mẫu |
| :--- | :--- | :--- |
| **`qhy`** | Identity & Khách hàng (`NguoiDung`, `NhanVien`, `VaiTro`, `KhachHang`) | `feature/identity-domain-entities`<br>`feature/identity-application-usecase`<br>`feature/identity-management-completion` |
| **`sun`** | Catalog & Thống kê (`DanhMuc`, `SanPhamTrangSuc`, `ThongKe`) | `feature/catalog-domain-entities`<br>`feature/catalog-application-usecase`<br>`feature/reporting-application-infra` |
| **`anhthy`** | Giỏ hàng, Đơn hàng & Hạ tầng DB (`GioHang`, `HoaDon`, `DatabaseHelper`) | `feature/cart-orders-domain-infra-base`<br>`feature/orders-application-usecase`<br>`feature/orders-processing-infra-completion` |
| **`tlinh`** | Nhập kho, Kiểm kê kho & Bảo hành (`PhieuNhapKho`, `PhieuKiemKe`, `PhieuBaoHanh`) | `feature/inventory-warranty-domain`<br>`feature/inventory-warranty-application`<br>`feature/inventory-warranty-completion` |

---

## 5. NGUYÊN TẮC BẮT BUỘC ĐỂ KHÔNG BỊ LỖI

> [!WARNING]
> **1. TUYỆT ĐỐI KHÔNG COMMIT BUILD ARTIFACTS**
> - Không bao giờ gõ `git add .` bừa bãi.
> - Tuyệt đối không commit các thư mục `bin/`, `obj/`, `.vs/`, `.vscode/` hoặc tệp `*.user`.
> - Luôn chạy lệnh `git status` để kiểm tra các file đang ở vùng chờ (staged) trước khi commit.

> [!IMPORTANT]
> **2. QUY TRÌNH PULL REQUEST (PR)**
> - Sau khi hoàn thành commit trên nhánh `feature/...` của mình, đẩy nhánh lên GitHub:
>   ```bash
>   git push origin feature/<tên_nhánh>
>   ```
> - Lên giao diện GitHub của nhóm, tạo Pull Request để gộp từ `feature/<tên_nhánh>` vào `main`.
> - Nhờ 1 thành viên khác trong nhóm duyệt (Review & Approve) trước khi Merge vào `main`.
