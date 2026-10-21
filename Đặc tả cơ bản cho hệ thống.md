# **Đặc tả chung của hệ thống**

Hệ thống bao gồm kiến trúc clean architecture được viết bằng .Net, đối với trang dành cho admin sẽ được thiết kế bằng winform, còn trang dành cho người dùng mua sắm sẽ thiết kế bằng web. Web khách hàng sẽ giao tiếp với phần nghiệp vụ của clean architecture thông qua Restful API.  
Ở đồ án này không cần quan tâm đến giá vàng hay loại vàng dùng cho sản phẩm này là gì, thông tin về cái đó chỉ cần lưu trong chuỗi string chất liệu là được, không cần ghi nhận lại lịch sử giá của sản phẩm đó.  
Hệ thống hiện tại chỉ bán online

# **Triển khai hệ thống sau này**

Mô tả bằng lời về triển khai dự án chạy thử: Sau này khi đồ án được code xong, vì là đồ án sinh viên, nên tôi sẽ triển khai theo cách để một máy cá nhân làm host, rồi dùng một máy khác, có thể không cùng một mạng để truy cập vào trang web mua hàng, một máy khác chứa ứng dụng được viết bằng winform để quản lý.  
\# SYSTEM DEPLOYMENT & NETWORK PROTOCOL SPECIFICATION

\#\# 1\. TỔNG QUAN MÔ HÌNH HẠ TẦNG (INFRASTRUCTURE OVERVIEW)  
\- \*\*Mô hình kết nối:\*\* Client \- Server qua môi trường Internet tự do.  
\- \*\*Thành phần tham gia hệ thống:\*\*  
  1\. \*\*Máy Host Server:\*\* Chạy Backend .NET Web API (Port \`5000\`), Cơ sở dữ liệu SQL Server (Port \`1433\`) và dịch vụ Ngrok Tunnel.  
  2\. \*\*Máy Client Web (Khách hàng):\*\* Trình duyệt Web truy cập giao diện bán hàng (Host trên Vercel/Netlify hoặc chạy cục bộ).  
  3\. \*\*Máy Client WinForms (Admin):\*\* Ứng dụng Desktop chạy file \`.exe\` độc lập trên hệ điều hành Windows của người quản lý.  
\- \*\*Cơ chế giao tiếp:\*\* Toàn bộ giao tiếp giữa Client (Web & WinForms) với Server đều thông qua giao thức \*\*HTTP/HTTPS (RESTful API)\*\*. WinForms và Web tuyệt đối không kết nối trực tiếp tới SQL Server (Port 1433).

\---

\#\# 2\. LƯU LƯỢNG VÀ LUỒNG DỮ LIỆU MẠNG (NETWORK DATA FLOW)

\`\`\`text  
\[ Máy Khách Hàng (Web) \]       ──(HTTPS Request)──┐  
                                                  			├──\> \[ Ngrok Public HTTPS Cloud \] ──(Local Forward)──\> \[ Máy Host Server \]  
\[ Máy Admin (WinForms .exe) \]   ──(HTTPS Request)──┘                                                          				│  
> > > > > >                                                                                                   ( Backend .NET API:5000 )  
> > > > > >                                                                                                               │  
> > > > > >                                                                                                   ( Local SQL Server:1433 )

# 

# PHÂN HỆ 1: Quản lý người dùng & Phân quyền (Identity & RBAC)

# *(Căn cứ: `NguoiDung`, `KhachHang`, `NhanVien`, `VaiTro`, `DangNhapUseCase`, `DangKyKhachHangUseCase`, `QuanLyNhanVienUseCase`)*

* # Dành cho Người dùng (Khách hàng \- Web): 

  1. # Đăng ký tài khoản (`DangKyKhachHangDTO`): Nhập họ tên, số điện thoại, ngày sinh, tên đăng nhập, mật khẩu để tạo tài khoản tích điểm/hội viên. \[Note: Người dùng không cần tính năng về tích điểm và hội viên, chỉ cần xem lại những đơn hàng đã đặt, theo dõi đơn hàng là được\]

  2. # Đăng nhập & Đăng xuất: Xác thực tài khoản, lưu phiên làm việc an toàn.

  3. # Quản lý hồ sơ cá nhân: Xem và cập nhật thông tin cá nhân (họ tên, số điện thoại, ngày sinh), đổi mật khẩu.

* # Dành cho Admin / Nội bộ (WPF Admin): 

  1. # Đăng nhập phân quyền (`DangNhapDTO`): Hệ thống nhận diện `VaiTro` để hiển thị menu chức năng tương ứng (Admin, Nhân viên). \[Note: nhân viên sẽ xử lý các đơn đặt hàng online, có đầy đủ tất cả tính năng ngoại trừ reset mật khẩu của nhân viên khác, điều chỉnh giá bán của sản phẩm(nhưng vẫncó thể điều chỉnh ảnh hoặc mô tả của sản phẩm)\]

  2. # Quản lý nhân viên (`TaoNhanVienDTO`, `QuanLyNhanVienUseCase`): Thêm mới nhân viên, reset mật khẩu nhân viên(nếu là admin) gán mã nhân viên, chức vụ (có 2 chức vụ là admin hoặc nhân viên), ca làm việc, lương cơ bản, khóa/mở khóa tài khoản nhân viên.

  3. # Quản lý danh sách khách hàng: Tra cứu hồ sơ khách hàng, xem lịch sử mua sắm và lịch sử bảo hành.

  4. # Phân quyền vai trò (Role-Based Access Control): Cấu hình quyền hạn truy cập theo từng nhóm chức năng \[Note: bỏ\].

# ---

# PHÂN HỆ 2: Quản lý sản phẩm & Kho hàng (Catalog & Inventory)

# *(Căn cứ: `DanhMuc`, `SanPhamTrangSuc`, `PhieuNhapKho`, `ChiTietNhapKho`, `PhieuKiemKe`, `ChiTietKiemKe`, `QuanLySanPhamUseCase`, `XacNhanNhapKhoUseCase`)*

* # Dành cho Người dùng (Khách hàng \- Web): 

  1. # Duyệt danh mục trang sức (`DanhMuc`): Xem sản phẩm theo cây danh mục (Nhẫn kim cương, Dây chuyền vàng, Lắc tay, Bông tai,...).

  2. # Tìm kiếm & Bộ lọc nâng cao: Tìm kiếm theo từ khóa; lọc theo chất liệu (Vàng 18K, Vàng 24K, Kim cương, Bạch kim), kích cỡ (Size ni tay), khoảng giá bán. \[Note: không cần phải có cơ sở dữ liệu riêng cho chất liệu, khi lọc chỉ cần trong chuỗi string chất liệu có chất liệu đó là được\]

  3. # Xem chi tiết trang sức: Xem hình ảnh, mô tả, chất liệu, size, giá bán, tình trạng sẵn hàng.

* # Dành cho Admin / Nhân viên: 

  1. # Quản lý danh mục (CRUD Danh mục): Thêm, sửa, ẩn/hiện danh mục sản phẩm. (Nhân viên chỉ dc xem, chỉ có admin mới được toàn quyền)

  2. # Quản lý trang sức (`SanPhamDTO`, `QuanLySanPhamUseCase`): Thêm mới sản phẩm, cập nhật mô tả, hình ảnh, điều chỉnh giá bán (`DoiGiaDTO`). (Nhân viên có thể cập nhật mô tả, hình ảnh \- cấm tuyệt đối nhân viên điều chỉnh giá bán)

  3. # Nhập kho trang sức (`NhapKhoDTO`, `XacNhanNhapKhoUseCase`): Lập phiếu nhập kho từ xưởng chế tác/nhà cung cấp, ghi nhận giá nhập (`GiaNhap`), số lượng, tự động cập nhật tồn kho. 

  4. # Kiểm kê định kỳ kho hàng (`PhieuKiemKe`, `ChiTietKiemKe`): Đối soát số lượng thực tế với phần mềm, ghi nhận hao hụt/chênh lệch và lý do xử lý. 

  5. # Quản lý trạng thái vòng đời sản phẩm: Giám sát trạng thái trang sức (`SanSang`, `DaBan`, `DangBaoHanh`).\[Note: bỏ\]

# ---

# PHÂN HỆ 3: Quản lý bán hàng (Sales Management & Orders)

# *(Căn cứ: `HoaDon`, `ChiTietHoaDon`, `GioHang`, `ChiTietGioHang`, `DatHangUseCase`, `DatHangDTO`)*

* # Dành cho Người dùng (Khách hàng \- Web): 

  1. # Quản lý giỏ hàng (`GioHang`, `ChiTietGioHang`): Thêm món trang sức vào giỏ, chọn size, điều chỉnh số lượng, xem tạm tính tổng tiền.

  2. # Đặt hàng trực tuyến (`DatHangDTO`, `DatHangUseCase`): Nhập địa chỉ nhận hàng, số điện thoại, ghi chú giao hàng, chọn phương thức thanh toán (COD hoặc chuyển khoản).

  3. # Theo dõi trạng thái đơn hàng: Xem tiến độ đơn online (Chờ duyệt, Đang đóng gói, Đang giao, Đã giao, Đã hủy).

  4. # Lịch sử mua hàng: Tra cứu hóa đơn điện tử của các đơn hàng đã đặt.

* # Dành cho Admin / Nhân viên thu ngân: \[note: bỏ nhân viên thu ngân\] 

  1. # Bán hàng tại quầy POS: Quét mã/chọn sản phẩm nhanh, kiểm tra size sẵn có, tính tổng tiền tức thì. \[Note: hiện tại hệ thống sẽ ko phục vụ tại quầy, chỉ bán online\] 

  2. # Lập & In hóa đơn bán lẻ lẻ (`HoaDon`, `ChiTietHoaDon`): Gán khách hàng thân thiết (để tích điểm), áp dụng chiết khấu/ưu đãi, ghi nhận phương thức thanh toán (tiền mặt/thẻ/chuyển khoản), in hóa đơn chuyên nghiệp. \[Note: bỏ chức năng in hoá đơn, chỉ dùng để hiển thị hoá đơn mua hàng ra màn hình của khách hàng khi mua hàng, bỏ luôn thanh toán bằng thẻ \- tính năng thanh toán bằng ngân hàng sẽ chỉ hiển thị ra UI nhưng không hoạt động với lý do Ngân hàng đang có vấn đề, bỏ luôn tính năng tích lũy điểm cho khách hàng\]

  3. # Tiếp nhận & Xử lý đơn online: Duyệt đơn đặt hàng, trừ tồn kho tự động, chuyển trạng thái giao hàng.

  4. # Hủy đơn & Đổi trả: Xử lý yêu cầu hoàn trả hoặc hủy đơn theo chính sách đổi trả trang sức của cửa hàng. \[Note: bán rồi thì k có vụ trả đâu, tính năng hủy đơn vẫn giữ lại\]

# ---

# PHÂN HỆ 4: Quản lý dịch vụ & Bảo hành (Warranty & Repair Service)

# *(Căn cứ: `PhieuBaoHanh`, `TiepNhanBaoHanhUseCase`, `XuLyBaoHanhUseCase`, `TaoBaoHanhDTO`, `CapNhatTrangThaiDTO`)*

* # Dành cho Người dùng (Khách hàng \- Web): 

  1. # Tra cứu bảo hành online: Nhập số phiếu bảo hành (`SoPhieuBH`) hoặc số điện thoại để tra cứu tình trạng xử lý món trang sức (Đang kiểm định, Đang đánh bóng/chỉnh size/đính đá, Đã hoàn tất).

  2. # Xem sổ bảo hành điện tử: Xem danh sách phiếu bảo hành liên kết với tài khoản khách hàng, thời hạn bảo hành của từng món trang sức đã mua.

  3. # Xem bảng giá dịch vụ: Tra cứu bảng giá làm mới, xi mạ, chỉnh size, đính đá.\[Note: bỏ, phiếu bảo hành sẽ có phần tiền cần thanh toán, lý do sẽ do nhân viên tự nhập\]

* # Dành cho Admin / Kỹ thuật viên\[Note: bỏ kỹ thuật viên đi\]:

  Mô tả: Lúc đầu khi nhân viên lập phiếu bảo hành, thì phiếu bảo hành đó sẽ ở trạng thái đang bảo hành, có ghi nhận lý do để biết cần bảo hành cái gì, không cần phải ghi số tiền phải thanh toán, nhưng khi bảo hành xong rồi thì nhân viên sẽ chuyển trạng thái phiếu bảo hành đó sang thành công, lúc này nhân viên phải nhập số tiền cần thanh toán cho dịch vụ bảo hành này, hoặc có thể là miễn phí.

  1. # Tiếp nhận sản phẩm (`TaoBaoHanhDTO`, `TiepNhanBaoHanhUseCase`): Nhập thông tin khách hàng, món trang sức, nội dung dịch vụ (đánh bóng, thu/nới size, gắn đá rơi), ghi nhận chi phí dự kiến nếu ngoài phạm vi miễn phí.\[Note: những cái này nhân viên sẽ tự nhập\]

  2. # Cập nhật tiến độ kỹ thuật (`XuLyBaoHanhUseCase - CapNhatTienDo`): Thợ kim hoàn cập nhật trạng thái xử lý từng công đoạn. \[Note: bỏ thợ kim hoàn đi, trạng thái bảo hành sẽ do nhân viên cập nhật\]

  3. # Hoàn tất dịch vụ & Quyết toán (`XuLyBaoHanhUseCase - HoanTatDichVu`): Nhập chi phí thực tế (`ChiPhiThucTe`), chuyển trạng thái sản phẩm từ `DangBaoHanh` về trạng thái ban đầu, in biên nhận bàn giao trả trang sức cho khách.

  4. # Danh sách phiếu bảo hành: Quản lý, tìm kiếm và lọc danh sách phiếu bảo hành theo trạng thái và hẹn ngày trả.

# ---

# PHÂN HỆ 5: Báo cáo & Thống kê (Reporting & Analytics)

# *(Căn cứ: `DoanhThuThongKeDTO`, `BaoCaoTonKhoDTO`, `TopSanPhamBanChayDTO`, `ThongKeDoanhThuUseCase`, `ThongKeKhoHangUseCase`)*

* # Dành cho Người dùng (Khách hàng \- Web): 

  1. # Tổng quan tiêu dùng cá nhân (Mini Dashboard): Xem tổng chi tiêu tích lũy, hạng thành viên (Bạc / Vàng / Kim cương), điểm thưởng khả dụng để đổi voucher. (Note: bỏ hoàn toàn tính năng hạng thành viên \- bỏ tính năng voucher

* # Dành cho Admin : 

  1. # Báo cáo doanh thu & Lợi nhuận (`ThongKeDoanhThuUseCase`):

     * # Thống kê doanh thu theo mốc thời gian: theo ngày, tuần, tháng, quý, năm.

     * # Phân tích cơ cấu doanh thu: Bán hàng online Web.

  2. # Báo cáo top sản phẩm bán chạy (`TopSanPhamBanChayDTO`):

     * # Xếp hạng các mẫu trang sức bán chạy nhất theo doanh số/số lượng (giúp định hướng nhập hàng/chế tác thêm). \[note: bỏ chế tác\]

  3. # Báo cáo kho hàng & Cảnh báo tồn (`ThongKeKhoHangUseCase`, `BaoCaoTonKhoDTO`):

     * # Thống kê giá trị tồn kho theo danh mục, chất liệu (Vàng, Bạc, Kim cương). \[bỏ thống kê theo chất liệu đi\]

     * # Cảnh báo sản phẩm sắp hết size/hết hàng hoặc sản phẩm tồn đọng lâu ngày (chậm luân chuyển). \[Note: bỏ sản phẩm tồn đọng lâu ngày, thay bằng sản phẩm có doanh thu thấp nhất trong tháng, năm\]

  4. # Xuất báo cáo (Export Excel/PDF): Kết xuất dữ liệu báo cáo phục vụ lưu trữ kế toán và kiểm toán. \[Note: Bỏ tính năng này\]

# 