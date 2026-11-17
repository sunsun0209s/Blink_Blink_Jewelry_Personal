# BẢNG TỔNG HỢP LỊCH SỬ ĐÓNG GÓP THÀNH VIÊN THEO TỪNG COMMIT VÀ ĐỐI CHIẾU USE CASE
**Đề tài:** Hệ thống Thuyết minh Du lịch Tự động Đa ngôn ngữ (*AutoVoiceOver*) – Khu Di tích Hoàng Thành Thăng Long  
**Môn học:** Công nghệ Phần mềm  
**Khoa:** Công nghệ Thông tin – Trường Đại học Sài Gòn (SGU)  

---

## 1. THÔNG TIN THÀNH VIÊN VÀ PHÂN CÔNG CHỦ CHỐT

| STT | Mã số sinh viên | Họ và tên | Tên tài khoản Git / Email | Vai trò chính trong dự án |
| :---: | :---: | :--- | :--- | :--- |
| **1** | **3124411104** | **Đặng Quốc Huy** | `qhy` / `imqhyy` / `hy`<br>`huydang6726@gmail.com` | **Trưởng nhóm, Kiến trúc sư hệ thống:** Khởi tạo kiến trúc 3 lớp, phát triển Core Backend (.NET 8), chuyển đổi toàn diện sang Microsoft SQL Server qua ADO.NET, bảo mật tài khoản SHA-256, WebSocket SignalR Hub, Web Audio Engine, CI/CD Pipeline và Docker. |
| **2** | **3124411305** | **Trần Ngọc Anh Thy** | `anhthy` / `TrannThy`<br>`anhthy12012006@gmail.com` | **Phân tích thiết kế & Tính năng Bản đồ:** Phụ trách trọn gói Quản lý bản đồ khu vực (UC-09), mở rộng quản lý điểm tham quan theo bản đồ (UC-06), Radar đa bản đồ, xử lý trang lỗi 404 du khách, thiết kế trọn bộ tài liệu đồ án (DFD mức 0-1-2, ERD Chen, 8 Activity Diagrams) và CD Pipeline GHCR. |
| **3** | **3124411266** | **Trương Xuân Tâm** | `sun` / `sunsun0209s` / `Xuân Tâm`<br>`sunsun0209s@gmail.com` | **Xử lý thuật toán không gian & Real-time:** Thuật toán giải quyết tranh chấp vùng nhận diện lồng nhau (Euclid Geofence), tối ưu hóa tiết lưu SignalR (Throttling), bộ chuyển đổi ngôn ngữ trên Audio Dock Toolbar, phụ trách chức năng Đổi mật khẩu Admin (UC-10), khắc phục lỗi CSDL khởi động và nâng cấp .NET 8. |

---

## 2. BẢNG MA TRẬN ĐỐI CHIẾU ĐÓNG GÓP THEO 10 USE CASE HỆ THỐNG

| Mã UC | Tên Use Case | Phân hệ | Đặng Quốc Huy (`qhy`) | Trần Ngọc Anh Thy (`anhthy`) | Trương Xuân Tâm (`sun`) |
| :---: | :--- | :---: | :---: | :---: | :---: |
| **UC-01** | **Khám phá điểm tham quan trên bản đồ** | Du khách | **Chính** (Dựng Web Client 1200x800, chạm mô phỏng) | Hỗ trợ (Màn hình 404 khi bản đồ tắt, vẽ sơ đồ AD UC-01) | Hỗ trợ (Tự động sinh mã QR Ngrok vào bản đồ) |
| **UC-02** | **Chọn ngôn ngữ thuyết minh** | Du khách | Hỗ trợ (Thiết kế DTOs đa ngữ) | Hỗ trợ (Dịch vụ dịch thuật BLL, vẽ AD UC-02) | **Chính** (Tích hợp bộ đổi ngôn ngữ trên Audio Dock Player) |
| **UC-03** | **Nghe thuyết minh tự động** | Du khách | **Chính** (Web Audio Engine, tính toán khoảng cách) | Hỗ trợ (Vẽ sơ đồ AD UC-03) | **Chính** (Giải quyết tranh chấp vùng lồng nhau gần nhất) |
| **UC-04** | **Xem phụ đề và ảnh đối chiếu** *(extend UC-03)* | Du khách | **Chính** (Sliding Bottom Sheet, nạp text/ảnh) | Hỗ trợ (Vẽ sơ đồ AD UC-04) | Hỗ trợ (Bổ sung kho ảnh tư liệu và audio thực tế) |
| **UC-05** | **Đăng nhập hệ thống quản trị** | Quản trị | **Chính** (API Auth, mã hóa SHA-256, Session Token, DAL) | **Chính** (Thiết kế UI form đăng nhập, loại bỏ quick-fill, vẽ AD UC-05) | Hỗ trợ (Bọc an toàn CSDL đăng nhập) |
| **UC-06** | **Quản lý điểm tham quan** | Quản trị | **Chính** (DAL ADO.NET CRUD, API Controller) | **Chính** (Gắn điểm theo từng bản đồ, vẽ AD UC-06) | Hỗ trợ (Đồng bộ tọa độ bản đồ) |
| **UC-07** | **Quản lý audio đa ngôn ngữ và phụ đề** | Quản trị | **Chính** (Xây dựng BLL Thuyết minh, DAL KhoNoiDung) | Hỗ trợ (Vẽ sơ đồ AD UC-07) | Hỗ trợ (Bổ sung kho file thu âm MP3/WAV chuẩn) |
| **UC-08** | **Giám sát du khách thời gian thực** | Quản trị | **Chính** (SignalR Hub `/hub/theodoi`, nhóm Admin, Radar ban đầu) | **Chính** (Chuyển đổi theo dõi giữa nhiều bản đồ, vẽ AD UC-08) | **Chính** (Sửa lỗi đồng bộ tên khách, tiết lưu Throttling) |
| **UC-09** | **Quản lý bản đồ khu vực** | Quản trị | Hỗ trợ (Cấu hình CSDL BanDoKhuVuc) | **Chủ trì trọn gói** (CRUD bản đồ, bật/tắt, xuất QR, vẽ sơ đồ AC UC-09) | Hỗ trợ (Tối ưu hóa ảnh sơ đồ bản đồ) |
| **UC-10** | **Đổi mật khẩu tài khoản quản trị** | Quản trị | Hỗ trợ (Cấu trúc bảng TaiKhoanAdmins) | Hỗ trợ (Review kiểm thử) | **Chủ trì trọn gói** (Toàn bộ UI, API, BLL SHA-256, AD UC-10, Unit Tests) |
| **DevOps** | **Hạ tầng, Docker, CI/CD** | Hệ thống | **Chính** (CI GitHub Actions, MSSQL container, Dockerfile, scripts) | **Chính** (CD Pipeline GHCR, hướng dẫn Docker) | **Chính** (Nâng cấp .NET 8, docker-compose autovoiceover) |

---

## 3. CHI TIẾT ĐÓNG GÓP TỪNG COMMIT CỦA ĐẶNG QUỐC HUY (`qhy`)

* **Tổng số commits:** 26 commits  
* **Tài khoản Git:** `imqhyy`, `hy`  
* **Email:** `huydang6726@gmail.com`  

```
Chronological Commit History - Đặng Quốc Huy
========================================================================================
```

### Commit 1: `544d47f` – Khởi tạo dự án
* **Thời gian:** `2026-10-10 14:00:06 +07:00`
* **Nội dung:** Khởi tạo repository, tạo file `README.md`.
* **Đối chiếu Use Case / Thành phần:** Nền tảng dự án.
* **Tác động tệp:** `README.md`.

### Commit 2: `8d7dd45` – Cấu hình CI Pipeline và khung kiến trúc 3 lớp
* **Thời gian:** `2026-10-10 15:33:29 +07:00`
* **Nội dung:** Thiết lập cấu trúc solution `.slnx` gồm 4 projects: `AutoVoiceOver.Api`, `AutoVoiceOver.Bll`, `AutoVoiceOver.Dal`, `AutoVoiceOver.Tests`. Cấu hình workflow tự động build và test trên GitHub Actions (`.github/workflow/ci.yml`).
* **Đối chiếu Use Case / Thành phần:** Hạ tầng DevOps / Kiến trúc 3 lớp.
* **Tác động tệp (15 files):**
  * `.github/workflow/ci.yml`, `.gitignore`, `AutoVoiceOver.slnx`
  * `src/AutoVoiceOver.Api/Program.cs`, `AutoVoiceOver.Api.csproj`, `appsettings.json`, `appsettings.Development.json`, `launchSettings.json`, `AutoVoiceOver.Api.http`
  * `src/AutoVoiceOver.Bll/AutoVoiceOver.Bll.csproj`, `Class1.cs`
  * `src/AutoVoiceOver.Dal/AutoVoiceOver.Dal.csproj`, `Class1.cs`
  * `tests/AutoVoiceOver.Tests/AutoVoiceOver.Tests.csproj`, `UnitTest1.cs`

### Commit 3: `dab1ac9` – Khởi tạo tầng truy xuất dữ liệu (DAL)
* **Thời gian:** `2026-10-12 23:37:26 +07:00`
* **Nội dung:** Thiết lập các Entities dữ liệu ban đầu (`HoSoGiongDoc`, `TrangThaiYeuCau`, `YeuCauThuyetMinh`), DbContext `NguCanhDuLieuApp` và Generic Repository Pattern (`IKhoDuLieuChung`, `KhoYeuCauThuyetMinh`).
* **Đối chiếu Use Case / Thành phần:** Tầng DAL / Dữ liệu nền tảng.
* **Tác động tệp (12 files):**
  * `src/AutoVoiceOver.Dal/Entities/HoSoGiongDoc.cs`, `TrangThaiYeuCau.cs`, `YeuCauThuyetMinh.cs`
  * `src/AutoVoiceOver.Dal/NguCanhDuLieuApp.cs`
  * `src/AutoVoiceOver.Dal/Repositories/IKhoDuLieuChung.cs`, `IKhoYeuCauThuyetMinh.cs`, `KhoYeuCauThuyetMinh.cs`

### Commits 4, 5, 6, 7: `5d7b11c`, `e3e7bf3`, `5ff3f55`, `fd16653` – Tinh chỉnh Repository & CI
* **Thời gian:** `2026-10-13 10:31` $\rightarrow$ `13:45 +07:00`
* **Nội dung:** Cập nhật `KhoDuLieuChung.cs`, sửa lỗi cấu hình YAML GitHub Actions, chuẩn hóa định dạng tập tin solution.
* **Tác động tệp:** `KhoDuLieuChung.cs`, `.github/workflows/ci.yml`, `AutoVoiceOver.sln`, `AutoVoiceOver.slnx`.

### Commit 8: `b2015fe` – Khởi tạo tầng nghiệp vụ (BLL)
* **Thời gian:** `2026-10-13 14:35:50 +07:00`
* **Nội dung:** Định nghĩa các Interface và DTOs nghiệp vụ cốt lõi: `IDichVuThuyetMinh`, `IDichVuGiongDoc`, `IDichVuDichThuat`, `IDichVuTaoGiongNoi`.
* **Đối chiếu Use Case / Thành phần:** Nền tảng BLL cho UC-02, UC-03, UC-07.
* **Tác động tệp (11 files):**
  * `src/AutoVoiceOver.Bll/Dtos/KetQuaThuyetMinhDto.cs`, `ThongTinGiongDocDto.cs`, `YeuCauTaoThuyetMinhDto.cs`
  * `src/AutoVoiceOver.Bll/Interface/IDichVuDichThuat.cs`, `IDichVuGiongDoc.cs`, `IDichVuTaoGiongNoi.cs`, `IDichVuThuyetMinh.cs`
  * `src/AutoVoiceOver.Bll/Service/DichVuDichThuat.cs`, `DichVuGiongDoc.cs`, `DichVuTaoGiongNoi.cs`, `DichVuThuyetMinh.cs`

### Commit 9, 10, 11, 12: `e471ddb`, `0f59753`, `a242021`, `0c771da` – Code BLL & Merge PRs
* **Thời gian:** `2026-10-17 01:25` $\rightarrow$ `23:21 +07:00`
* **Nội dung:** Merge PR #1 (nâng cấp .NET 8 của Tâm), PR #3 & #4 (dịch thuật của Thy); code chi tiết service `DichVuGiongDoc` và `DichVuThuyetMinh`.
* **Đối chiếu Use Case:** UC-07 (Quản lý nội dung giọng đọc và thuyết minh).
* **Tác động tệp:** Các DTOs và Services trong `AutoVoiceOver.Bll`.

### Commit 13, 14: `6ba5389`, `43bb855` – Hoàn thiện DichVuThuyetMinh & Viết Unit Test
* **Thời gian:** `2026-10-20 09:55` $\rightarrow$ `15:09 +07:00`
* **Nội dung:** Code xử lý tạo thuyết minh và viết bộ kiểm thử xUnit tự động `DichVuThuyetMinhTests.cs`.
* **Đối chiếu Use Case:** UC-03, UC-07.
* **Tác động tệp:** `DichVuThuyetMinh.cs`, `tests/AutoVoiceOver.Tests/DichVuThuyetMinhTests.cs`.

### Commit 15, 16, 17, 18: `905816f`, `b35c64b`, `8ff9a61`, `999061e` – Tái cấu trúc chuẩn hóa hệ thống
* **Thời gian:** `2026-10-20 22:07` $\rightarrow$ `2026-10-21 10:55 +07:00`
* **Nội dung:** Tách biệt rõ ràng ranh giới nghiệp vụ giữa Admin và Du khách: tạo các Controller `DuKhachController`, `QuanLyDiaDiemController`, `DieuKhienThuyetMinhController`, chuẩn hóa Entities `DiaDiemThamQuan`, `NoiDungThuyetMinh`.
* **Đối chiếu Use Case:** Khung API phục vụ toàn bộ UC-01 đến UC-08.
* **Tác động tệp (34 files):** Các Controllers, DTOs, Repositories tại Api, Bll, Dal.

### Commit 19: `46ecdd3` – CỘT MỐC CỐT LÕI: Hoàn thiện AutoVoiceOver 3 lớp, SignalR, Web Audio Engine & Docker
* **Thời gian:** `2026-10-25 02:08:25 +07:00`
* **Nội dung:** Đột phá lớn nhất của dự án (91 files):
  1. Xây dựng SignalR Hub `TheoDoiNguoiDungHub.cs` điều phối kết nối hai chiều WebSocket.
  2. Xây dựng giao diện Web Du khách Responsive (`user/index.html`, `user.js`, `user.css`) tích hợp `Web Audio Engine` vượt qua chính sách Autoplay Policy của trình duyệt di động.
  3. Cài đặt thuật toán Euclid Geofence tính khoảng cách giữa du khách và di tích.
  4. Xây dựng giao diện quản trị Admin Web Dashboard với màn hình Radar Canvas (`admin/index.html`, `admin.js`, `admin.css`).
  5. Đóng gói Dockerfile, `docker-compose.yml` và script khởi chạy `run_server.bat`.
* **Đối chiếu Use Case:** **UC-01, UC-03, UC-04, UC-06, UC-07, UC-08.**
* **Tác động tệp (91 files tiêu biểu):**
  * `src/AutoVoiceOver.Api/Hubs/TheoDoiNguoiDungHub.cs`
  * `src/AutoVoiceOver.Api/Controllers/DuKhachController.cs`, `QuanLyDiaDiemController.cs`, `DashboardController.cs`
  * `src/AutoVoiceOver.Api/wwwroot/user/` (`index.html`, `user.js`, `user.css`)
  * `src/AutoVoiceOver.Api/wwwroot/admin/` (`index.html`, `admin.js`, `admin.css`)
  * `src/AutoVoiceOver.Bll/Service/DichVuDuKhach.cs`, `DichVuQuanLyDiaDiem.cs`, `DichVuThongKe.cs`
  * `Dockerfile`, `docker-compose.yml`, `run_server.bat`, các file audio mẫu.

### Commit 20, 21: `94b5b1a`, `5fec3ed` – Cập nhật đường dẫn Web du khách
* **Thời gian:** `2026-10-25 02:09` $\rightarrow$ `02:11 +07:00`
* **Nội dung:** Cập nhật thông tin đường dẫn trang Web Du Khách trong script `run_server.bat`.

### Commit 22: `5769b5c` – CHUYỂN ĐỔI TOÀN DIỆN SANG SQL SERVER THUẦN ADO.NET
* **Thời gian:** `2026-10-25 04:22:00 +07:00`
* **Nội dung:** Chuyển đổi toàn bộ cơ sở dữ liệu từ SQLite sang Microsoft SQL Server sử dụng thuần công nghệ ADO.NET:
  1. Viết các kho ADO.NET: `KhoBanDoAdoNet.cs`, `KhoDiaDiemAdoNet.cs`, `KhoNoiDungAdoNet.cs`, `KhoTaiKhoanAdminAdoNet.cs`, `KhoiTaoCoSoDuLieuAdoNet.cs`.
  2. Hiện thực tính năng Đăng nhập Admin bảo mật: Băm SHA-256 mật khẩu, sinh Session Token có HMAC-SHA256 (`DichVuXacThucAdmin.cs`, `AuthController.cs`).
  3. Chuẩn hóa hệ tọa độ Canvas 1200x800 trên cả giao diện Admin và User.
  4. Viết các Unit Test kiểm thử ADO.NET và xác thực Admin.
* **Đối chiếu Use Case:** **UC-05 (Đăng nhập quản trị), UC-06 (Điểm tham quan), UC-07 (Audio), UC-08 (Radar).**
* **Tác động tệp (32 files):**
  * `database/schema.sql`, `seed_data.sql`, `autovoiceover.sql`
  * `src/AutoVoiceOver.Dal/Repositories/*AdoNet.cs`
  * `src/AutoVoiceOver.Bll/Service/DichVuXacThucAdmin.cs`
  * `src/AutoVoiceOver.Api/Controllers/AuthController.cs`
  * `tests/AutoVoiceOver.Tests/DichVuXacThucAdminTests.cs`, `KhoDiaDiemAdoNetTests.cs`, `KhoTaiKhoanAdminAdoNetTests.cs`

### Commit 23: `d17a1c5` – Cấu hình MSSQL Container trên GitHub Actions CI
* **Thời gian:** `2026-10-25 09:45:47 +07:00`
* **Nội dung:** Cấu hình MSSQL service container chạy ngầm trong GitHub Actions workflow (`ci.yml`), tạo helper `KiemThuCoSoDuLieuHelper.cs` giúp toàn bộ 38 Unit Test tự động chạy và pass 100% trên môi trường CI đám mây.
* **Đối chiếu Use Case:** Hạ tầng CI/CD tự động hóa.
* **Tác động tệp:** `.github/workflows/ci.yml`, `tests/AutoVoiceOver.Tests/KiemThuCoSoDuLieuHelper.cs`.

### Commit 24, 25: `a158735`, `6b0744a` – Tối ưu hóa DevOps & Scripts vận hành 1-Click
* **Thời gian:** `2026-11-17 14:57` $\rightarrow$ `15:00 +07:00`
* **Nội dung:**
  1. Viết script [stop_server.bat](file:///c:/Users/huy/Downloads/code/CongNghePhanMem/CNPM/stop_server.bat) dọn dẹp sạch sẽ Docker, Ngrok, port 5256 và build-server.
  2. Nâng cấp [run_server.bat](file:///c:/Users/huy/Downloads/code/CongNghePhanMem/CNPM/run_server.bat) tự động cấp phát domain tĩnh Ngrok (`implicit-reborn-subfloor.ngrok-free.dev`) và tự động tắt Docker khi đóng cửa sổ.
* **Đối chiếu Use Case:** DevOps & Hạ tầng vận hành demo.
* **Tác động tệp:** `run_server.bat`, `stop_server.bat`, `.vscode/settings.json`.

### Commit 26: `1a308c3` – Dọn dẹp tệp tin rác
* **Thời gian:** `2026-11-17 16:40:49 +07:00`
* **Nội dung:** Xóa bỏ các file trùng lặp và tệp tài liệu tạm thời ở thư mục gốc để làm sạch repository.
* **Tác động tệp:** `CNPM-AD UC10.drawio.xml`, `CONGVIECTIEPTHEO.md`, `Ngữ cảnh Ứng dụng.md`.

---

## 4. CHI TIẾT ĐÓNG GÓP TỪNG COMMIT CỦA TRẦN NGỌC ANH THY (`anhthy`)

* **Tổng số commits:** 18 commits  
* **Tài khoản Git:** `anhthy`, `TrannThy`, `Trần Ngọc Anh Thy`  
* **Email:** `anhthy12012006@gmail.com`  

```
Chronological Commit History - Trần Ngọc Anh Thy
========================================================================================
```

### Commit 1, 2, 3: `bfa7172`, `38d20c0`, `5e76ea5` – Khởi tạo Module Dịch thuật đa ngôn ngữ
* **Thời gian:** `2026-10-14 22:20` $\rightarrow$ `2026-10-17 22:18 +07:00`
* **Nội dung:** Xây dựng interface `IDichVuDichThuat`, class `DichVuDichThuat` và viết bộ kiểm thử xUnit `DichVuDichThuatTests.cs`.
* **Đối chiếu Use Case:** **UC-02 (Chọn ngôn ngữ thuyết minh).**
* **Tác động tệp (3 files):**
  * `src/AutoVoiceOver.Bll/Interface/IDichVuDichThuat.cs`
  * `src/AutoVoiceOver.Bll/Service/DichVuDichThuat.cs`
  * `tests/AutoVoiceOver.Tests/DichVuDichThuatTests.cs`

### Commit 4, 5: `f858bb3`, `41a9920` – Code Review & Merge PR
* **Thời gian:** `2026-10-20 15:08` $\rightarrow$ `15:15 +07:00`
* **Nội dung:** Tiến hành review và merge pull requests #5 và #6 liên quan đến service tạo thuyết minh.

### Commit 6, 7, 8: `138ecfc`, `8fcdb68`, `d31dab6` – Tinh chỉnh giao diện & Bảo mật form Đăng nhập Admin
* **Thời gian:** `2026-10-27 22:33` $\rightarrow$ `22:57 +07:00`
* **Nội dung:**
  1. Loại bỏ các nút điền sẵn tài khoản mẫu (quick-fill demo) để nâng cao tính bảo mật thực tế cho hệ thống quản trị.
  2. Căn chỉnh khoảng cách, padding, font chữ và responsive cho form đăng nhập Admin (`admin.css`, `admin/index.html`).
  3. Cải thiện logic kiểm tra hợp lệ dữ liệu đăng nhập phía client (`admin.js`).
* **Đối chiếu Use Case:** **UC-05 (Đăng nhập hệ thống quản trị).**
* **Tác động tệp (3 files):**
  * `src/AutoVoiceOver.Api/wwwroot/admin/admin.css`
  * `src/AutoVoiceOver.Api/wwwroot/admin/admin.js`
  * `src/AutoVoiceOver.Api/wwwroot/admin/index.html`

### Commit 9: `c02bf9c` – Thiết kế sơ đồ luồng dữ liệu DFD
* **Thời gian:** `2026-11-11 08:34:00 +07:00`
* **Nội dung:** Xây dựng bản thảo sơ đồ luồng dữ liệu DFD Mức 0 (Sơ đồ ngữ cảnh) và DFD Mức 1 (Phân rã chức năng) bằng mã nguồn Mermaid.
* **Đối chiếu Use Case / Thiết kế:** Mô hình hóa DFD cho toàn bộ hệ thống.
* **Tác động tệp:** `DFD_Muc_0.mmd`, `DFD_Muc_1.mmd`, `CONGVIECTIEPTHEO.md`.

### Commit 10: `58d3f25` – Cập nhật cấu hình CSDL ADO.NET
* **Thời gian:** `2026-11-11 08:51:14 +07:00`
* **Nội dung:** Tinh chỉnh repository khởi tạo database tự động qua ADO.NET `KhoiTaoCoSoDuLieuAdoNet.cs`.
* **Tác động tệp:** `KhoiTaoCoSoDuLieuAdoNet.cs`, `admin.txt`, `run_server.bat`.

### Commit 11: `7e23a8c` – CHỨC NĂNG RADAR ĐA BẢN ĐỒ
* **Thời gian:** `2026-11-11 09:38:47 +07:00`
* **Nội dung:**
  1. Nâng cấp `TheoDoiNguoiDungHub.cs` hỗ trợ tham số `maBanDo` trong việc đăng ký và phát tọa độ du khách.
  2. Cho phép Quản trị viên lựa chọn bản đồ hiển thị trên màn hình Radar Giám sát Live.
  3. Cập nhật `NguoiDungOnlineDto` và service `DichVuThongKe.cs` để phân loại khách theo từng khu vực.
* **Đối chiếu Use Case:** **UC-08 (Giám sát du khách thời gian thực), UC-09 (Quản lý bản đồ).**
* **Tác động tệp (8 files):**
  * `src/AutoVoiceOver.Api/Hubs/TheoDoiNguoiDungHub.cs`
  * `src/AutoVoiceOver.Api/wwwroot/admin/admin.js`, `admin.css`, `admin/index.html`
  * `src/AutoVoiceOver.Api/wwwroot/user/user.js`
  * `src/AutoVoiceOver.Bll/Dtos/NguoiDungOnlineDto.cs`
  * `src/AutoVoiceOver.Bll/Interface/IDichVuThongKe.cs`, `DichVuThongKe.cs`

### Commit 12: `ede84bb` – Quản lý điểm tham quan theo từng bản đồ khu vực
* **Thời gian:** `2026-11-11 10:05:54 +07:00`
* **Nội dung:**
  1. Bổ sung trường chọn bản đồ liên kết khi thêm mới hoặc chỉnh sửa điểm tham quan.
  2. Cập nhật bộ lọc danh sách điểm tham quan theo từng bản đồ trên bảng quản trị.
  3. Viết Unit Test kiểm thử quản lý điểm theo bản đồ trong `DichVuQuanLyDiaDiemTests.cs`.
* **Đối chiếu Use Case:** **UC-06 (Quản lý điểm tham quan), UC-09 (Quản lý bản đồ).**
* **Tác động tệp (5 files):**
  * `src/AutoVoiceOver.Api/wwwroot/admin/admin.js`, `admin/index.html`
  * `src/AutoVoiceOver.Bll/Service/DichVuQuanLyDiaDiem.cs`
  * `src/AutoVoiceOver.Dal/Repositories/KhoDiaDiemAdoNet.cs`
  * `tests/AutoVoiceOver.Tests/DichVuQuanLyDiaDiemTests.cs`

### Commit 13: `018bce2` – CHỦ TRÌ PHÁT TRIỂN CHỨC NĂNG QUẢN LÝ BẢN ĐỒ (UC-09)
* **Thời gian:** `2026-11-11 19:30:29 +07:00`
* **Nội dung:** Hoàn thiện toàn diện module Quản lý bản đồ khu vực:
  1. Đổi tên tab điều hướng thành "Quản lý Bản đồ".
  2. Viết API Controller và BLL hỗ trợ quản lý nhiều sơ đồ bản đồ, upload ảnh sơ đồ, thiết lập tỉ lệ mét/pixel.
  3. Tính năng bật / tắt trạng thái hoạt động của từng bản đồ (`hoatDong`).
  4. Tạo Modal xuất mã QR định danh riêng cho từng bản đồ kèm chức năng tải ảnh QR về máy.
* **Đối chiếu Use Case:** **UC-09 (Quản lý bản đồ khu vực) – Đảm nhận chính.**
* **Tác động tệp (8 files):**
  * `src/AutoVoiceOver.Api/Controllers/QuanLyDiaDiemController.cs`
  * `src/AutoVoiceOver.Api/wwwroot/admin/admin.js`, `admin.css`, `admin/index.html`
  * `src/AutoVoiceOver.Bll/Dtos/BanDoKhuVucDto.cs`
  * `src/AutoVoiceOver.Bll/Interface/IDichVuQuanLyDiaDiem.cs`, `DichVuQuanLyDiaDiem.cs`
  * `tests/AutoVoiceOver.Tests/DichVuQuanLyDiaDiemTests.cs`

### Commit 14: `6467cdb` – Xây dựng màn hình 404 thân thiện cho Du khách
* **Thời gian:** `2026-11-11 19:37:16 +07:00`
* **Nội dung:** Xử lý ngoại lệ trải nghiệm người dùng: Khi du khách quét mã QR vào một bản đồ không tồn tại hoặc đã bị quản trị viên tắt hoạt động (`hoatDong = false`), hệ thống hiển thị màn hình 404 đẹp mắt, lịch sự và hướng dẫn quay lại.
* **Đối chiếu Use Case:** **UC-01 (Khám phá điểm tham quan), UC-09 (Quản lý bản đồ).**
* **Tác động tệp (6 files):**
  * `src/AutoVoiceOver.Api/Controllers/DuKhachController.cs`
  * `src/AutoVoiceOver.Api/wwwroot/user/index.html`, `user.css`, `user.js`
  * `src/AutoVoiceOver.Bll/Service/DichVuDuKhach.cs`
  * `tests/AutoVoiceOver.Tests/DichVuDuKhachTests.cs`

### Commit 15, 16: `1a83c45`, `b2887cc` – ĐÓNG GÓP TOÀN BỘ SƠ ĐỒ THIẾT KẾ VÀ TỐI ƯU CSDL SQL SERVER
* **Thời gian:** `2026-11-15 17:01` $\rightarrow$ `17:34 +07:00`
* **Nội dung:** Siêu đóng góp tài liệu thiết kế (27 files):
  1. Vẽ trọn bộ sơ đồ DFD chuẩn phong cách Gane & Sarson: DFD Mức 0, DFD Mức 1, và 6 sơ đồ DFD Mức 2 (2.1 đến 2.6).
  2. Vẽ mô hình thực thể kết hợp ERD chuẩn ký pháp Peter Chen (`CNPM-ERD.drawio.xml`).
  3. Vẽ toàn bộ 8 Sơ đồ hoạt động Activity Diagrams từ `CNPM-AD UC01` đến `CNPM-AD UC08`, cùng sơ đồ `CNPM-AC UC09`.
  4. Tối ưu hóa các script CSDL SQL Server (`schema.sql`, `seed_data.sql`, `autovoiceover.sql`).
* **Đối chiếu Use Case:** **Thiết kế mô hình cho toàn bộ 9 Use Case (UC-01 đến UC-09).**
* **Tác động tệp (27 files tiêu biểu):**
  * Thư mục `CNPM XML/`: `CNPM-AD UC01.drawio.xml` $\rightarrow$ `UC08.drawio.xml`, `CNPM-AC UC09.drawio.xml`
  * Thư mục `DFD/`: `DFD-Muc-0.drawio.xml`, `DFD-Muc-1.drawio.xml`, các file `DFD-Muc-2-*.drawio.xml`
  * `CNPM-ERD.drawio.xml`, `database/schema.sql`, `seed_data.sql`.

### Commit 17, 18: `b9f4bad`, `dcd97f9` – THIẾT LẬP CD PIPELINE LÊN GHCR VÀ TỐI ƯU DOCKER
* **Thời gian:** `2026-11-15 20:04` $\rightarrow$ `20:48 +07:00`
* **Nội dung:**
  1. Viết pipeline triển khai liên tục CD (`.github/workflows/cd.yml`) tự động đóng gói và đẩy Docker image lên GitHub Container Registry (GHCR: `ghcr.io/imqhyy/cnpm:latest`).
  2. Tối ưu hóa file `Dockerfile`, `.dockerignore`, `docker-compose.yml`.
  3. Viết tài liệu hướng dẫn vận hành container `huongdanDocker.md`.
  4. Cập nhật luồng quét QR ở trang chủ `wwwroot/index.html`.
* **Đối chiếu Use Case:** Hạ tầng DevOps / Triển khai CD.
* **Tác động tệp (8 files):**
  * `.github/workflows/cd.yml`, `.dockerignore`, `Dockerfile`, `docker-compose.yml`
  * `huongdanDocker.md`, `README.md`, `run_server.bat`, `src/AutoVoiceOver.Api/wwwroot/index.html`.

---

## 5. CHI TIẾT ĐÓNG GÓP TỪNG COMMIT CỦA TRƯƠNG XUÂN TÂM (`sun`)

* **Tổng số commits:** 15 commits  
* **Tài khoản Git:** `sunsun0209s`, `Xuân Tâm`  
* **Email:** `sunsun0209s@gmail.com`  

```
Chronological Commit History - Trương Xuân Tâm
========================================================================================
```

### Commit 1: `5ef5d1a` – Nâng cấp toàn bộ dự án lên .NET 8 LTS
* **Thời gian:** `2026-10-17 01:07:45 +07:00`
* **Nội dung:** Nâng cấp TargetFramework của cả 4 project (Api, Bll, Dal, Tests) lên nền tảng `.NET 8.0` LTS hiện đại nhất, cập nhật `Program.cs`.
* **Đối chiếu Use Case / Thành phần:** Nâng cấp hạ tầng Core framework.
* **Tác động tệp (5 files):**
  * `src/AutoVoiceOver.Api/AutoVoiceOver.Api.csproj`, `Program.cs`
  * `src/AutoVoiceOver.Bll/AutoVoiceOver.Bll.csproj`
  * `src/AutoVoiceOver.Dal/AutoVoiceOver.Dal.csproj`
  * `tests/AutoVoiceOver.Tests/AutoVoiceOver.Tests.csproj`

### Commit 2, 3: `fa13e88`, `6a427e4` – Hoàn thiện DichVuTaoGiongNoi & Unit Test
* **Thời gian:** `2026-10-17 04:08` $\rightarrow$ `04:12 +07:00`
* **Nội dung:** Xây dựng interface `IDichVuTaoGiongNoi`, triển khai class `DichVuTaoGiongNoi` và viết bộ test xUnit `DichVuTaoGiongNoiTests.cs`.
* **Đối chiếu Use Case:** UC-07 (Tạo âm thanh thuyết minh).
* **Tác động tệp:** `IDichVuTaoGiongNoi.cs`, `DichVuTaoGiongNoi.cs`, `DichVuTaoGiongNoiTests.cs`, `appsettings.json`.

### Commit 4: `eac0922` – THUẬT TOÁN GIẢI QUYẾT TRANH CHẤP VÙNG NHẬN DIỆN LỒNG NHAU
* **Thời gian:** `2026-10-27 22:09:21 +07:00`
* **Nội dung:** Giải quyết bài toán hóc búa nhất của thuyết minh tự động:
  1. Khi 2 điểm tham quan nằm sát nhau có bán kính nhận diện giao thoa, thuật toán trong `DichVuDuKhach.cs` tự động tính toán khoảng cách Euclid và **ưu tiên kích hoạt điểm gần nhất**.
  2. Bổ sung endpoint `HeThongController.cs` và cập nhật client tự động sinh mã QR động theo URL Ngrok public.
  3. Viết Unit Test `DichVuDuKhachTests.cs` kiểm tra chính xác thuật toán.
* **Đối chiếu Use Case:** **UC-01 (Bản đồ), UC-03 (Nghe thuyết minh tự động) – Đảm nhận chính thuật toán.**
* **Tác động tệp (14 files):**
  * `src/AutoVoiceOver.Bll/Service/DichVuDuKhach.cs`
  * `src/AutoVoiceOver.Api/Controllers/HeThongController.cs`, `QuanLyDiaDiemController.cs`
  * `src/AutoVoiceOver.Api/wwwroot/user/user.js`, `admin/admin.js`, `admin/index.html`, `index.html`
  * `tests/AutoVoiceOver.Tests/DichVuDuKhachTests.cs`

### Commit 5: `d608473` – Tích hợp Ngrok vào script chạy server
* **Thời gian:** `2026-10-27 22:20:49 +07:00`
* **Nội dung:** Cập nhật script `run_server.bat` tự động phát hiện và khởi chạy tunnel Ngrok để du khách quét mã QR từ điện thoại thật.
* **Đối chiếu Use Case:** Hỗ trợ demo thực tế UC-01.
* **Tác động tệp:** `run_server.bat`, `.gitignore`.

### Commit 6: `6badf7d` – SỬA LỖI ĐỒNG BỘ REALTIME TÊN DU KHÁCH VÀ VỊ TRÍ RADAR
* **Thời gian:** `2026-10-28 01:46:47 +07:00`
* **Nội dung:**
  1. Khắc phục lỗi bất đồng bộ tên du khách trên SignalR Hub: Khi khách nhập biệt danh sau khi quét QR, `TheoDoiNguoiDungHub.cs` cập nhật ngay vào `_dichVuThongKe` và broadcast tức thời sang Dashboard.
  2. Bổ sung các tệp âm thanh thực tế vào `uploads/audios/`.
* **Đối chiếu Use Case:** **UC-08 (Giám sát du khách thời gian thực).**
* **Tác động tệp (6 files):**
  * `src/AutoVoiceOver.Api/Hubs/TheoDoiNguoiDungHub.cs`
  * `src/AutoVoiceOver.Api/wwwroot/admin/admin.js`, `user/user.js`
  * `src/AutoVoiceOver.Bll/Service/DichVuThongKe.cs`

### Commit 7: `bbf0b96` – TỐI ƯU HIỆU NĂNG REALTIME (THROTTLING SIGNALR)
* **Thời gian:** `2026-10-28 01:51:20 +07:00`
* **Nội dung:** Áp dụng kỹ thuật **Throttling (tiết lưu sự kiện)**: Khi du khách di chuyển liên tục trên điện thoại, hệ thống gom cụm và giới hạn tần suất gửi tọa độ qua WebSocket, tránh làm nghẽn băng thông server khi có hàng trăm du khách kết nối cùng lúc.
* **Đối chiếu Use Case:** **UC-01 (Mô phỏng di chuyển), UC-08 (Giám sát Live Radar).**
* **Tác động tệp (3 files):**
  * `src/AutoVoiceOver.Api/Program.cs`
  * `src/AutoVoiceOver.Api/wwwroot/admin/admin.js`
  * `src/AutoVoiceOver.Api/wwwroot/user/user.js`

### Commit 8: `b7611e6` – Tinh gọn Header & Nút ẩn hiện Sidebar Admin
* **Thời gian:** `2026-10-28 01:59:53 +07:00`
* **Nội dung:** Tinh chỉnh UI Dashboard: Bổ sung nút thu gọn thanh bên (Sidebar Toggle), giúp mở rộng tối đa không gian màn hình quan sát Canvas Radar khi giám sát du khách.
* **Đối chiếu Use Case:** UC-08 (Radar Giám sát).
* **Tác động tệp (3 files):** `admin.css`, `admin.js`, `admin/index.html`.

### Commit 9: `b3bd94b` – TÍCH HỢP BỘ CHUYỂN ĐỔI NGÔN NGỮ TRÊN AUDIO DOCK TOOLBAR
* **Thời gian:** `2026-10-28 02:59:26 +07:00`
* **Nội dung:**
  1. Thêm bộ chọn ngôn ngữ trực tiếp ngay trên thanh điều khiển Audio Dock Player phía dưới màn hình điện thoại du khách.
  2. Khi du khách đổi ngôn ngữ, hệ thống tự động đổi luồng phát âm thanh và cập nhật phụ đề ngay lập tức mà không cần mở menu lớn.
  3. Bổ sung kho dữ liệu âm thanh thu âm đa ngữ và hình ảnh minh họa di tích.
* **Đối chiếu Use Case:** **UC-02 (Chọn ngôn ngữ), UC-03 (Nghe thuyết minh), UC-04 (Xem phụ đề/ảnh).**
* **Tác động tệp (12 files):**
  * `src/AutoVoiceOver.Api/wwwroot/user/` (`index.html`, `user.css`, `user.js`)
  * Các file audio đa ngữ và hình ảnh trong `uploads/audios/` và `uploads/images/`.

### Commit 10: `ad98a51` – KHẮC PHỤC LỖI CSDL KHỞI ĐỘNG (UNIQUE CONSTRAINT 2627)
* **Thời gian:** `2026-10-28 03:09:54 +07:00`
* **Nội dung:** Khắc phục triệt để lỗi crash server khi khởi động lại do câu lệnh INSERT dữ liệu mẫu bị vi phạm ràng buộc UNIQUE 2627; bọc khối kiểm tra tồn tại an toàn trong `KhoiTaoCoSoDuLieuAdoNet.cs` và `Program.cs`.
* **Đối chiếu Use Case / Hạ tầng:** Độ tin cậy và Tính ổn định CSDL.
* **Tác động tệp:** `Program.cs`, `KhoiTaoCoSoDuLieuAdoNet.cs`.

### Commit 11: `0cbaba5` – Bổ sung tệp dữ liệu Media
* **Thời gian:** `2026-10-28 09:28:22 +07:00`
* **Nội dung:** Bổ sung thêm các tệp audio thu âm thực tế và hình ảnh di tích vào thư mục uploads.
* **Tác động tệp:** `uploads/audios/`, `uploads/images/`, `admin/index.html`.

### Commit 12, 13: `3a7d5d7`, `0f73d06` – Đặt tên chuẩn Container & Tối ưu Docker Compose
* **Thời gian:** `2026-11-17 11:32:01 +07:00`
* **Nội dung:** Đặt tên project Docker chuẩn `name: autovoiceover`, tối ưu cấu hình mapping cổng `14333:1433` cho SQL Server, cập nhật `.gitignore`.
* **Đối chiếu Use Case:** Hạ tầng triển khai Docker.
* **Tác động tệp:** `docker-compose.yml`, `.gitignore`.

### Commit 14, 15: `039218a`, `325d8f0` – CHỦ TRÌ PHÁT TRIỂN CHỨC NĂNG ĐỔI MẬT KHẨU ADMIN (UC-10)
* **Thời gian:** `2026-11-17 13:09:20 +07:00`
* **Nội dung:** Đóng góp toàn diện tính năng Đổi mật khẩu tài khoản quản trị (39 files):
  1. Triển khai phương thức `DoiMatKhauAsync` trong `DichVuXacThucAdmin.cs`: Xác thực Token, kiểm tra độ dài $\ge 6$, kiểm tra khác mật khẩu cũ, băm SHA-256 mật khẩu cũ để đối chiếu và băm SHA-256 mật khẩu mới để cập nhật vào CSDL qua ADO.NET.
  2. Viết API Controller endpoint `POST /api/admin/auth/change-password` trong `AuthController.cs`.
  3. Xây dựng giao diện Modal đổi mật khẩu trên trang Admin Dashboard (`admin.js`, `admin/index.html`).
  4. Viết bộ kiểm thử xUnit `DichVuXacThucAdminTests.cs` kiểm tra toàn bộ luồng đổi mật khẩu thành công và các ngoại lệ.
  5. Thiết kế và bổ sung Sơ đồ hoạt động `CNPM-AD UC10.drawio.xml`, cập nhật các sơ đồ DFD mức 0, mức 1, DFD mức 2 (1.0-XacThuc).
* **Đối chiếu Use Case:** **UC-10 (Đổi mật khẩu tài khoản quản trị) – Đảm nhận trọn gói.**
* **Tác động tệp (39 files tiêu biểu):**
  * `src/AutoVoiceOver.Api/Controllers/AuthController.cs`
  * `src/AutoVoiceOver.Bll/Service/DichVuXacThucAdmin.cs`, `Interface/IDichVuXacThucAdmin.cs`, `Dtos/AdminAuthDtos.cs`
  * `src/AutoVoiceOver.Api/wwwroot/admin/admin.js`, `admin/index.html`
  * `tests/AutoVoiceOver.Tests/DichVuXacThucAdminTests.cs`
  * `CNPM XML/CNPM-AD UC10.drawio.xml`, `DFD-Muc-2-1.0-XacThuc.drawio.xml`

---

## 6. DÒNG THỜI GIAN TIẾN ĐỘ DỰ ÁN (PROJECT MASTER TIMELINE)

```
[10/10/2026] ─── Huy: Khởi tạo Solution 3 lớp, CI GitHub Actions
     │
[14/10/2026] ─── Thy: Xây dựng Module Dịch thuật BLL + Unit Test
     │
[17/10/2026] ─── Tâm: Nâng cấp Core .NET 8 LTS, hoàn thiện DichVuTaoGiongNoi
     │
[20/10/2026] ─── Huy: Hoàn thiện BLL Thuyết minh, tái cấu trúc phân chia Admin & Du khách
     │
[25/10/2026] ─── Huy: ĐỘT PHÁ HỆ THỐNG: Web User Audio Engine, SignalR Hub, Docker,
     │                Chuyển đổi toàn diện sang SQL Server ADO.NET, MSSQL CI Runner
     │
[27/10/2026] ─── Thy: Tinh chỉnh giao diện bảo mật form đăng nhập Admin
     │           Tâm: Thuật toán Geofence chống lồng vùng, tự động sinh QR Ngrok
     │
[28/10/2026] ─── Tâm: Sửa lỗi realtime SignalR, Throttling tọa độ, đổi ngôn ngữ Audio Dock,
     │                sửa lỗi CSDL Unique 2627 khi server khởi động lại
     │
[11/11/2026] ─── Thy: Phát triển Radar đa bản đồ, quản lý điểm theo bản đồ,
     │                hoàn thiện trọn gói Quản lý Bản đồ (UC-09), màn hình 404 du khách
     │
[15/11/2026] ─── Thy: Siêu đóng góp tài liệu: trọn bộ DFD mức 0-1-2, ERD Chen, 9 Activity Diagrams,
     │                thiết lập CD Pipeline tự động đẩy Docker Image lên GHCR
     │
[17/11/2026] ─── Tâm: Phát triển trọn gói chức năng Đổi mật khẩu Admin (UC-10), vẽ AD UC-10,
     │                tối ưu hóa docker-compose autovoiceover
     │           Huy: Bổ sung script 1-click stop_server.bat, tối ưu domain tĩnh Ngrok,
     │                dọn dẹp sạch sẽ repository phục vụ bảo vệ đồ án
```

---

## 7. HƯỚNG DẪN TRẢ LỜI PHỎNG VẤN VẤN ĐÁP BẢO VỆ ĐỒ ÁN (VỀ ĐÓNG GÓP THÀNH VIÊN)

> **Câu hỏi của Thầy/Cô:** *"Nhóm có 3 bạn, hãy cho biết cụ thể từng bạn đảm nhận phần việc nào trong đồ án và trên Git có thể hiện đúng như vậy không?"*
>
> **Câu trả lời mẫu đại diện nhóm:**
> *"Dạ thưa Thầy/Cô, nhóm chúng em phân chia công việc rất rõ ràng theo năng lực chuyên môn và được minh chứng 100% qua lịch sử 59 commits trên Git:
> 
> 1. **Bạn Đặng Quốc Huy (Trưởng nhóm):** Đảm nhận vai trò Kiến trúc sư và Backend chính. Huy thiết lập khung kiến trúc 3 lớp, xây dựng WebSocket SignalR Hub điều phối thời gian thực, chuyển đổi toàn bộ tầng dữ liệu sang Microsoft SQL Server thuần ADO.NET, xây dựng Web Audio Engine cho du khách, cấu hình CI/CD trên GitHub Actions với MSSQL container và các script vận hành Docker.
> 
> 2. **Bạn Trần Ngọc Anh Thy:** Đảm nhận phân tích thiết kế hệ thống và nghiệp vụ bản đồ. Thy chủ trì trọn gói **Use Case 09: Quản lý bản đồ khu vực** (cho phép thêm nhiều bản đồ, bật/tắt hoạt động, xuất mã QR riêng và màn hình 404), mở rộng Radar đa bản đồ, và là người vẽ toàn bộ hệ thống tài liệu đồ án gồm sơ đồ DFD các mức 0-1-2, ERD Chen, các sơ đồ Activity Diagrams cùng việc xây dựng CD Pipeline GHCR.
> 
> 3. **Bạn Trương Xuân Tâm:** Đảm nhận các thuật toán không gian, tối ưu real-time và tính năng bảo mật. Tâm giải quyết bài toán cốt lõi về **tranh chấp vùng nhận diện lồng nhau** (ưu tiên điểm gần nhất theo khoảng cách Euclid), tối ưu tiết lưu Throttling cho SignalR, tích hợp bộ đổi ngôn ngữ tức thì trên Audio Dock Toolbar, nâng cấp .NET 8, và chủ trì phát triển trọn gói **Use Case 10: Đổi mật khẩu tài khoản quản trị** từ UI, API băm SHA-256 đến sơ đồ AD UC-10."*
