-- ====================================================================================
-- DỰ ÁN BLINK BLINK JEWELRY - DỮ LIỆU THỰC TẾ MẪU (DML SEED DATA KIM HOÀN CAO CẤP)
-- Dành cho việc nạp dữ liệu vào cơ sở dữ liệu đã có sẵn các bảng
-- ====================================================================================

USE BlinkBlinkJewelryDb;
GO

-- 1. Nạp vai trò hệ thống
INSERT INTO dbo.VaiTro (MaVaiTro, TenVaiTro, MoTa) VALUES
('ADMIN', N'Quản trị viên', N'Quản trị toàn quyền hệ thống cửa hàng kim hoàn'),
('SALE', N'Nhân viên bán hàng', N'Thu ngân và tư vấn bán hàng tại quầy POS'),
('WAREHOUSE', N'Thủ kho', N'Quản lý nhập kho và lập biên bản kiểm kê trang sức'),
('KHACHHANG', N'Khách hàng', N'Khách hàng mua sắm trực tuyến và tra cứu bảo hành');
GO

-- 2. Nạp người dùng (Bảng cha NguoiDung)
INSERT INTO dbo.NguoiDung (MaNguoiDung, HoTen, SoDienThoai, TenDangNhap, MatKhau, MaVaiTro, TrangThai) VALUES
('USER_ADMIN01', N'Trần Minh Hoàng', '0901234567', 'admin', 'admin123', 'ADMIN', 1),
('USER_NV001', N'Lê Phương Thảo', '0912345678', 'thao.le', 'pass123', 'SALE', 1),
('USER_NV002', N'Nguyễn Quốc Huy', '0933445566', 'huy.nguyen', 'pass123', 'WAREHOUSE', 1),
('USER_KH001', N'Phạm Bích Ngọc', '0988112233', 'ngoc.pham', 'khach123', 'KHACHHANG', 1),
('USER_KH002', N'Hoàng Tuấn Anh', '0977223344', 'anh.hoang', 'khach123', 'KHACHHANG', 1);
GO

-- 3. Nạp thông tin nhân viên (Bảng con NhanVien)
INSERT INTO dbo.NhanVien (MaNguoiDung, MaNhanVien, LuongCoBan) VALUES
('USER_ADMIN01', 'NV_ADMIN', 25000000),
('USER_NV001', 'NV001', 12000000),
('USER_NV002', 'NV002', 14000000);
GO

-- 4. Nạp thông tin khách hàng (Bảng con KhachHang)
INSERT INTO dbo.KhachHang (MaNguoiDung, MaKhachHang, NgaySinh) VALUES
('USER_KH001', 'KH001', '1995-08-15'),
('USER_KH002', 'KH002', '1992-03-20');
GO

-- 5. Nạp danh mục trang sức cao cấp Blink Blink Jewelry
INSERT INTO dbo.DanhMuc (MaDanhMuc, TenDanhMuc, MoTa) VALUES
('DM01', N'Nhẫn Kim Cương', N'Các dòng nhẫn đính hôn, nhẫn cưới và nhẫn kim cương tự nhiên tinh xảo'),
('DM02', N'Dây Chuyền & Mặt Dây', N'Dây chuyền vàng Ý 18K và mặt dây đính đá quý phong thủy'),
('DM03', N'Bông Tai Quý Phái', N'Bông tai đính đá Sapphire, Ruby và ngọc trai thiên nhiên thượng hạng'),
('DM04', N'Vòng Tay & Lắc Kim Cương', N'Lắc tay Tennis và vòng kiềng chế tác thủ công cao cấp');
GO

-- 6. Nạp sản phẩm trang sức độc bản
INSERT INTO dbo.SanPhamTrangSuc (MaSanPham, TenSanPham, MaDanhMuc, ChatLieu, Size, GiaNhap, GiaBan, TrangThai, HinhAnh, PhanTramGiamGia) VALUES
('SP001', N'Nhẫn Solitaire Diamond 18K', 'DM01', N'Vàng trắng 18K đính kim cương chủ 5.4 ly', N'Size 14', 18000000, 28500000, 'SanSang', 'https://images.unsplash.com/photo-1605100804763-247f67b3557e?auto=format&fit=crop&w=800&q=80', 10),
('SP002', N'Dây Chuyền Eternal Heart Ruby', 'DM02', N'Vàng hồng 18K đính đá Ruby huyết bồ câu', N'Size 45cm', 12000000, 19200000, 'SanSang', 'https://images.unsplash.com/photo-1599643478518-a784e5dc4c8f?auto=format&fit=crop&w=800&q=80', 0),
('SP003', N'Bông Tai Royal Blue Sapphire', 'DM03', N'Bạch kim Platin 950 đính Sapphire Hoàng gia', N'Tiêu chuẩn', 22000000, 34000000, 'SanSang', 'https://images.unsplash.com/photo-1630019852942-f89202989a59?auto=format&fit=crop&w=800&q=80', 15),
('SP004', N'Lắc Tay Tennis Diamond Bracelet', 'DM04', N'Vàng trắng 18K đính 45 viên kim cương tự nhiên', N'Size 17cm', 48000000, 72000000, 'SanSang', 'https://images.unsplash.com/photo-1611591475152-47354900a359?auto=format&fit=crop&w=800&q=80', 20),
('SP005', N'Nhẫn Cưới Harmony Eternity', 'DM01', N'Vàng Ý 750 tinh tế đính kim cương tấm viền', N'Size 16', 11000000, 16800000, 'SanSang', 'https://images.unsplash.com/photo-1603561591411-07134e71a2a9?auto=format&fit=crop&w=800&q=80', 5),
('SP006', N'Mặt Dây Chuyền Ngọc Trai South Sea', 'DM02', N'Vàng vàng 18K đính ngọc trai biển và kim cương', N'Size 42cm', 28000000, 42500000, 'SanSang', 'https://images.unsplash.com/photo-1515562141207-7a88fb7ce338?auto=format&fit=crop&w=800&q=80', 0);
GO

-- 7. Nạp phiếu nhập kho mẫu
INSERT INTO dbo.PhieuNhapKho (MaPhieuNhap, NgayNhap, NhaCungCap, MaNhanVien, TongSoSanPham, TongGiaTri) VALUES
('PNK20260301', '2026-03-01 09:30:00', N'Tập đoàn Kim Hoàn Doji Atelier', 'USER_NV002', 10, 111000000);

INSERT INTO dbo.ChiTietNhapKho (MaPhieuNhap, MaSanPham, SoLuong, GiaVonNhap, SoChungTu) VALUES
('PNK20260301', 'SP001', 3, 18000000, 'CT-DOJI-991'),
('PNK20260301', 'SP002', 3, 12000000, 'CT-DOJI-992'),
('PNK20260301', 'SP005', 4, 11000000, 'CT-DOJI-993');
GO

-- 8. Nạp hóa đơn bán hàng thực tế
INSERT INTO dbo.HoaDon (MaHoaDon, NgayLap, MaKhachHang, MaNhanVien, DiaChiGiaoHang, SoDienThoaiNhan, PhuongThucThanhToan, TrangThaiDonHang, TongTienHang, TienGiamGia, TongThanhToan, GhiChu) VALUES
('HD1001', '2026-03-10 14:15:00', 'USER_KH001', 'USER_NV001', N'45 Lê Lợi, Phường Bến Nghé, Quận 1, TP.HCM', '0988112233', N'ChuyenKhoan', N'DaGiao', 28500000, 1000000, 27500000, N'Khách mua kỷ niệm ngày cưới, đóng gói hộp nhung cao cấp'),
('HD1002', '2026-03-15 16:45:00', 'USER_KH002', 'USER_NV001', N'128 Hai Bà Trưng, Phường Đa Kao, Quận 1, TP.HCM', '0977223344', N'TheNganHang', N'DaGiao', 19200000, 0, 19200000, N'Giao hàng hỏa tốc trong ngày'),
('HD1003', '2026-03-20 10:20:00', 'USER_KH001', NULL, N'45 Lê Lợi, Phường Bến Nghé, Quận 1, TP.HCM', '0988112233', N'ChuyenKhoan', N'ChoDuyet', 34000000, 0, 34000000, N'Khách đặt trực tuyến qua cổng Web khách hàng');

INSERT INTO dbo.ChiTietHoaDon (SoHoaDon, MaSanPham, DonGia, SoLuong) VALUES
('HD1001', 'SP001', 28500000, 1),
('HD1002', 'SP002', 19200000, 1),
('HD1003', 'SP003', 34000000, 1);
GO

-- 9. Nạp giỏ hàng trực tuyến mẫu cho khách hàng
INSERT INTO dbo.GioHang (MaGioHang, MaKhachHang, SoLuong) VALUES
('GH_KH001', 'USER_KH001', 1),
('GH_KH002', 'USER_KH002', 0);

INSERT INTO dbo.ChiTietGioHang (MaGioHang, MaSanPham, SoLuongSanPham) VALUES
('GH_KH001', 'SP004', 1);
GO

-- 10. Nạp phiếu kiểm kê kho
INSERT INTO dbo.PhieuKiemKe (MaPhieuKiemKe, NgayKiemKe, ViTriKiemKe, MaNhanVienKho, TrangThaiDuyet) VALUES
('PKK20260320', '2026-03-20 18:00:00', N'Tủ kính trưng bày khu A - Sảnh chính', 'USER_NV002', 1);

INSERT INTO dbo.ChiTietKiemKe (MaPhieuKiemKe, MaSanPham, TonSoSach, TonThucTe, ChenhLech, LyDoChenhLech) VALUES
('PKK20260320', 'SP001', 5, 5, 0, N'Khớp hoàn toàn sổ sách'),
('PKK20260320', 'SP002', 3, 3, 0, N'Khớp hoàn toàn sổ sách'),
('PKK20260320', 'SP003', 2, 2, 0, N'Khớp hoàn toàn sổ sách');
GO

-- 11. Nạp phiếu tiếp nhận bảo hành dịch vụ kim hoàn
INSERT INTO dbo.PhieuBaoHanh (SoPhieuBH, MaKhachHang, MaSanPham, LyDoBaoHanh, ChiPhiBaoHanh, TrangThaiXuLy, NgayTiepNhan, NgayHenTra) VALUES
('BH2001', 'USER_KH001', 'SP001', N'Làm sạch, đánh bóng siêu âm và kiểm tra chấu giữ viên kim cương chủ', 0, N'DaHoanTat', '2026-03-12 10:00:00', '2026-03-14 17:00:00'),
('BH2002', 'USER_KH002', 'SP002', N'Đứt khoen móc khóa cài dây chuyền vàng hồng, yêu cầu hàn laser thẩm mỹ', 150000, N'DangBaoHanh', '2026-03-18 15:30:00', '2026-03-22 17:00:00');
GO

PRINT N'>>> ĐÃ NẠP DỮ LIỆU MẪU KIM HOÀN THÀNH CÔNG! <<<';
GO
