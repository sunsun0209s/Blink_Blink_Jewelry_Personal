/**
 * ====================================================================================
 * BLINK BLINK HAUTE JOAILLERIE - XỬ LÝ TƯƠNG TÁC GIAO DIỆN (STOREFRONT CLIENT SCRIPT)
 * Quy chuẩn: Prototype-First, tương tác trực quan trước khi gắn logic API thật
 * Dữ liệu mẫu đồng bộ 100% với SeedData.sql và sơ đồ thực thể Clean Architecture
 * ====================================================================================
 */

// 1. Dữ liệu mẫu trang sức cao cấp (Khớp chính xác với SeedData.sql)
const sampleProducts = [
    {
        maSanPham: 'SP001',
        tenSanPham: 'Nhẫn Solitaire Diamond 18K',
        maDanhMuc: 'DM01',
        tenDanhMuc: 'Nhẫn Kim Cương',
        chatLieu: 'Vàng trắng 18K đính kim cương chủ 5.4 ly',
        size: 'Size 14',
        giaBan: 28500000,
        trangThai: 'SanSang',
        hinhAnh: 'https://images.unsplash.com/photo-1605100804763-247f67b3557e?auto=format&fit=crop&w=800&q=80',
        moTa: 'Thiết kế nhẫn đính hôn kinh điển với 6 chấu giữ viên kim cương giác cắt tròn hoàn hảo, phản chiếu ánh lửa tinh khiết.'
    },
    {
        maSanPham: 'SP002',
        tenSanPham: 'Dây Chuyền Eternal Heart Ruby',
        maDanhMuc: 'DM02',
        tenDanhMuc: 'Dây Chuyền & Mặt Dây',
        chatLieu: 'Vàng hồng 18K đính đá Ruby huyết bồ câu',
        size: 'Size 45cm',
        giaBan: 19200000,
        trangThai: 'SanSang',
        hinhAnh: 'https://images.unsplash.com/photo-1599643478518-a784e5dc4c8f?auto=format&fit=crop&w=800&q=80',
        moTa: 'Mặt dây chuyền hình trái tim gắn kết đá Ruby thiên nhiên màu đỏ huyết bồ câu rực rỡ, tượng trưng cho tình yêu bất diệt.'
    },
    {
        maSanPham: 'SP003',
        tenSanPham: 'Bông Tai Royal Blue Sapphire',
        maDanhMuc: 'DM03',
        tenDanhMuc: 'Bông Tai Quý Phái',
        chatLieu: 'Bạch kim Platin 950 đính Sapphire Hoàng gia',
        size: 'Tiêu chuẩn',
        giaBan: 34000000,
        trangThai: 'SanSang',
        hinhAnh: 'https://images.unsplash.com/photo-1630019852942-f89202989a59?auto=format&fit=crop&w=800&q=80',
        moTa: 'Cặp bông tai dáng giọt nước kiêu sa từ chất liệu bạch kim Platin quý hiếm, ôm trọn viên đá Sapphire xanh thăm thẳm.'
    },
    {
        maSanPham: 'SP004',
        tenSanPham: 'Lắc Tay Tennis Diamond Bracelet',
        maDanhMuc: 'DM04',
        tenDanhMuc: 'Vòng Tay & Lắc',
        chatLieu: 'Vàng trắng 18K đính 45 viên kim cương tự nhiên',
        size: 'Size 17cm',
        giaBan: 72000000,
        trangThai: 'SanSang',
        hinhAnh: 'https://images.unsplash.com/photo-1611591475152-47354900a359?auto=format&fit=crop&w=800&q=80',
        moTa: 'Biểu tượng của sự sang trọng vượt thời gian với 45 viên kim cương thiên nhiên tuyển chọn kỹ lưỡng, ôm sát cổ tay.'
    },
    {
        maSanPham: 'SP005',
        tenSanPham: 'Nhẫn Cưới Harmony Eternity',
        maDanhMuc: 'DM01',
        tenDanhMuc: 'Nhẫn Kim Cương',
        chatLieu: 'Vàng Ý 750 tinh tế đính kim cương tấm viền',
        size: 'Size 16',
        giaBan: 16800000,
        trangThai: 'SanSang',
        hinhAnh: 'https://images.unsplash.com/photo-1603561591411-07134e71a2a9?auto=format&fit=crop&w=800&q=80',
        moTa: 'Dòng nhẫn cưới tinh xảo với hàng kim cương tấm chạy vòng quanh, cam kết cho sự viên mãn và gắn kết sắt son.'
    },
    {
        maSanPham: 'SP006',
        tenSanPham: 'Mặt Dây Chuyền Ngọc Trai South Sea',
        maDanhMuc: 'DM02',
        tenDanhMuc: 'Dây Chuyền & Mặt Dây',
        chatLieu: 'Vàng vàng 18K đính ngọc trai biển và kim cương',
        size: 'Size 42cm',
        giaBan: 42500000,
        trangThai: 'SanSang',
        hinhAnh: 'https://images.unsplash.com/photo-1515562141207-7a88fb7ce338?auto=format&fit=crop&w=800&q=80',
        moTa: 'Viên ngọc trai biển South Sea ánh vàng kim ấm áp, phối cùng móc cài vàng 18K đính kim cương viền sang trọng.'
    }
];

// 2. Dữ liệu mẫu phiếu tiếp nhận bảo hành dịch vụ kim hoàn (SeedData.sql)
const sampleWarrantyTickets = {
    'BH2001': {
        soPhieu: 'BH2001',
        maKhachHang: 'USER_KH001',
        tenKhachHang: 'Phạm Bích Ngọc',
        maSanPham: 'SP001',
        tenSanPham: 'Nhẫn Solitaire Diamond 18K',
        ngayTiepNhan: '12/03/2026',
        ngayHenTra: '14/03/2026',
        chiPhi: 0,
        trangThai: 'DaHoanTat',
        trangThaiText: 'Đã hoàn tất',
        lyDo: 'Làm sạch, đánh bóng siêu âm và kiểm tra chấu giữ viên kim cương chủ',
        ghiChu: 'Trang sức đã được phục hồi độ sáng bóng 99%, sẵn sàng giao trả tại showroom Quận 1.'
    },
    'BH2002': {
        soPhieu: 'BH2002',
        maKhachHang: 'USER_KH002',
        tenKhachHang: 'Hoàng Tuấn Anh',
        maSanPham: 'SP002',
        tenSanPham: 'Dây Chuyền Eternal Heart Ruby',
        ngayTiepNhan: '18/03/2026',
        ngayHenTra: '22/03/2026',
        chiPhi: 150000,
        trangThai: 'DangBaoHanh',
        trangThaiText: 'Đang bảo dưỡng',
        lyDo: 'Đứt khoen móc khóa cài dây chuyền vàng hồng, yêu cầu hàn laser thẩm mỹ',
        ghiChu: 'Nghệ nhân đang gia công hàn nhiệt laser tại xưởng chế tác Blink Blink.'
    }
};



// 4. Dữ liệu mẫu lịch sử đơn hàng khách hàng (Khớp chính xác với SeedData.sql)
const sampleOrders = [
    {
        maHoaDon: 'HD1001',
        maKhachHang: 'USER_KH001',
        ngayLap: '10/03/2026 14:15',
        trangThai: 'DaGiao',
        trangThaiText: 'Đã Giao Thành Công',
        diaChi: '45 Lê Lợi, Phường Bến Nghé, Quận 1, TP.HCM',
        soDienThoai: '0988112233',
        tongTien: 27500000,
        tienGiamGia: 1000000,
        phuongThuc: 'Chuyển khoản ngân hàng',
        sanPham: [
            {
                maSanPham: 'SP001',
                tenSanPham: 'Nhẫn Solitaire Diamond 18K',
                chatLieu: 'Vàng trắng 18K đính kim cương chủ 5.4 ly',
                soLuong: 1,
                donGia: 28500000,
                hinhAnh: 'https://images.unsplash.com/photo-1605100804763-247f67b3557e?auto=format&fit=crop&w=400&q=80'
            }
        ]
    },
    {
        maHoaDon: 'HD1003',
        maKhachHang: 'USER_KH001',
        ngayLap: '20/03/2026 10:20',
        trangThai: 'ChoDuyet',
        trangThaiText: 'Chờ Xưởng Kim Hoàn Duyệt',
        diaChi: '45 Lê Lợi, Phường Bến Nghé, Quận 1, TP.HCM',
        soDienThoai: '0988112233',
        tongTien: 34000000,
        tienGiamGia: 0,
        phuongThuc: 'Chuyển khoản ngân hàng',
        sanPham: [
            {
                maSanPham: 'SP003',
                tenSanPham: 'Bông Tai Royal Blue Sapphire',
                chatLieu: 'Bạch kim Platin 950 đính Sapphire Hoàng gia',
                soLuong: 1,
                donGia: 34000000,
                hinhAnh: 'https://images.unsplash.com/photo-1630019852942-f89202989a59?auto=format&fit=crop&w=400&q=80'
            }
        ]
    },
    {
        maHoaDon: 'HD1002',
        maKhachHang: 'USER_KH002',
        ngayLap: '15/03/2026 16:45',
        trangThai: 'DaGiao',
        trangThaiText: 'Đã Giao Thành Công',
        diaChi: '128 Hai Bà Trưng, Phường Đa Kao, Quận 1, TP.HCM',
        soDienThoai: '0977223344',
        tongTien: 19200000,
        tienGiamGia: 0,
        phuongThuc: 'Thẻ ngân hàng POS',
        sanPham: [
            {
                maSanPham: 'SP002',
                tenSanPham: 'Dây Chuyền Eternal Heart Ruby',
                chatLieu: 'Vàng hồng 18K đính đá Ruby huyết bồ câu',
                soLuong: 1,
                donGia: 19200000,
                hinhAnh: 'https://images.unsplash.com/photo-1599643478518-a784e5dc4c8f?auto=format&fit=crop&w=400&q=80'
            }
        ]
    }
];

// Trạng thái người dùng đăng nhập hiện tại
let currentUser = null;

// Khóa định danh lưu trữ giỏ hàng trong bộ nhớ trình duyệt
const CART_STORAGE_KEY = 'blink_jewelry_cart';

// 3. Trạng thái giỏ hàng (Tự động phục hồi từ bộ nhớ localStorage)
let cart = [];

/**
 * Nạp dữ liệu giỏ hàng từ bộ nhớ trình duyệt
 */
function loadCartFromStorage() {
    try {
        const raw = localStorage.getItem(CART_STORAGE_KEY);
        if (raw) {
            const parsed = JSON.parse(raw);
            if (Array.isArray(parsed)) {
                cart = parsed;
            }
        }
    } catch (e) {
        console.error('Không thể phục hồi giỏ hàng:', e);
        cart = [];
    }
}

/**
 * Lưu trạng thái giỏ hàng vào bộ nhớ trình duyệt để không bị mất khi đăng xuất hay tải lại trang
 */
function saveCartToStorage() {
    try {
        localStorage.setItem(CART_STORAGE_KEY, JSON.stringify(cart));
    } catch (e) {
        console.error('Không thể lưu giỏ hàng:', e);
    }
}

// Định dạng tiền tệ VND
function formatCurrency(amount) {
    return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(amount);
}

// Khởi chạy khi tài liệu DOM sẵn sàng
document.addEventListener('DOMContentLoaded', () => {
    loadCartFromStorage();
    initProductGrid(sampleProducts);
    initCategoryFilters();
    updateCartUI();
    initAuth();
    initEventListeners();
});

/**
 * Hiển thị lưới sản phẩm kim hoàn
 * @param {Array} products - Danh sách sản phẩm cần vẽ lên lưới
 */
function initProductGrid(products) {
    const gridContainer = document.getElementById('products-grid');
    if (!gridContainer) return;

    gridContainer.innerHTML = '';

    if (products.length === 0) {
        gridContainer.innerHTML = `
            <div style="grid-column: 1/-1; text-align: center; padding: 60px 20px; color: var(--text-muted);">
                <p style="font-size: 1.1rem; font-family: var(--font-heading);">Không tìm thấy tuyệt tác trang sức phù hợp với tiêu chí đã chọn.</p>
            </div>
        `;
        return;
    }

    products.forEach(p => {
        const card = document.createElement('div');
        card.className = 'product-card';
        card.innerHTML = `
            <div class="product-thumb-wrap">
                <img src="${p.hinhAnh}" alt="${p.tenSanPham}" loading="lazy" />
                <span class="product-status-tag">Kiệt Tác</span>
                <div class="product-card-actions">
                    <button class="btn-card-quickview" onclick="openQuickView('${p.maSanPham}')">
                        <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z"/><circle cx="12" cy="12" r="3"/></svg>
                        Xem Nhanh
                    </button>
                    <button class="btn-card-addcart" onclick="addToCart('${p.maSanPham}')">
                        <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2"><path d="M6 2L3 6v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2V6l-3-4z"/><line x1="3" y1="6" x2="21" y2="6"/><path d="M16 10a4 4 0 0 1-8 0"/></svg>
                        Chọn Mua
                    </button>
                </div>
            </div>
            <div class="product-info-body">
                <span class="product-category-meta">${p.tenDanhMuc}</span>
                <h3 class="product-name-title">${p.tenSanPham}</h3>
                <p class="product-material-desc">${p.chatLieu}</p>
                <div class="product-footer-row">
                    <span class="product-size-badge">${p.size}</span>
                    <span class="product-price-tabular">${formatCurrency(p.giaBan)}</span>
                </div>
            </div>
        `;
        gridContainer.appendChild(card);
    });
}

/**
 * Khởi tạo bộ lọc danh mục và ô tìm kiếm
 */
function initCategoryFilters() {
    const filterButtons = document.querySelectorAll('.filter-pill-btn');
    const searchInput = document.getElementById('catalog-search-input');

    filterButtons.forEach(btn => {
        btn.addEventListener('click', () => {
            filterButtons.forEach(b => b.classList.remove('active'));
            btn.classList.add('active');

            const category = btn.getAttribute('data-category');
            filterProducts(category, searchInput ? searchInput.value : '');
        });
    });

    if (searchInput) {
        searchInput.addEventListener('input', (e) => {
            const activeCategoryBtn = document.querySelector('.filter-pill-btn.active');
            const category = activeCategoryBtn ? activeCategoryBtn.getAttribute('data-category') : 'ALL';
            filterProducts(category, e.target.value);
        });
    }
}

/**
 * Lọc danh sách sản phẩm theo danh mục và từ khóa
 */
function filterProducts(category, keyword) {
    let filtered = sampleProducts;

    if (category && category !== 'ALL') {
        filtered = filtered.filter(p => p.maDanhMuc === category);
    }

    if (keyword && keyword.trim() !== '') {
        const query = keyword.trim().toLowerCase();
        filtered = filtered.filter(p => 
            p.tenSanPham.toLowerCase().includes(query) || 
            p.chatLieu.toLowerCase().includes(query)
        );
    }

    initProductGrid(filtered);
}

/**
 * Thêm sản phẩm vào giỏ hàng
 * @param {string} maSanPham - Mã sản phẩm kim hoàn cần thêm
 */
function addToCart(maSanPham) {
    const product = sampleProducts.find(p => p.maSanPham === maSanPham);
    if (!product) return;

    const existingIndex = cart.findIndex(item => item.maSanPham === maSanPham);
    if (existingIndex > -1) {
        cart[existingIndex].soLuong += 1;
    } else {
        cart.push({
            maSanPham: product.maSanPham,
            tenSanPham: product.tenSanPham,
            chatLieu: product.chatLieu,
            size: product.size,
            giaBan: product.giaBan,
            soLuong: 1,
            hinhAnh: product.hinhAnh
        });
    }

    saveCartToStorage();
    updateCartUI();
    showToast(`Đã thêm "${product.tenSanPham}" vào giỏ hàng thành công!`);
}

/**
 * Cập nhật giao diện ngăn kéo giỏ hàng
 */
function updateCartUI() {
    const badge = document.getElementById('cart-badge');
    const itemsContainer = document.getElementById('cart-items-list');
    const subtotalEl = document.getElementById('cart-subtotal');
    const totalCount = cart.reduce((sum, item) => sum + item.soLuong, 0);

    if (badge) {
        badge.textContent = totalCount;
    }

    if (!itemsContainer) return;

    if (cart.length === 0) {
        itemsContainer.innerHTML = `
            <div class="cart-empty-message">
                <svg width="48" height="48" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5"><path d="M6 2L3 6v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2V6l-3-4z"/><line x1="3" y1="6" x2="21" y2="6"/><path d="M16 10a4 4 0 0 1-8 0"/></svg>
                <p>Giỏ hàng của quý khách hiện đang trống.</p>
                <button class="btn-luxury-outline" style="margin-top: 16px; font-size: 0.78rem;" onclick="toggleCartDrawer(false)">Khám Phá Bộ Sưu Tập</button>
            </div>
        `;
        if (subtotalEl) subtotalEl.textContent = formatCurrency(0);
        return;
    }

    itemsContainer.innerHTML = '';
    let total = 0;

    cart.forEach(item => {
        total += item.giaBan * item.soLuong;
        const row = document.createElement('div');
        row.className = 'cart-item-row';
        row.innerHTML = `
            <img class="cart-item-thumb" src="${item.hinhAnh}" alt="${item.tenSanPham}" />
            <div class="cart-item-info">
                <h4 class="cart-item-name">${item.tenSanPham}</h4>
                <div class="cart-item-meta">${item.size} • ${item.chatLieu}</div>
                <div class="cart-item-price">${formatCurrency(item.giaBan)}</div>
                <div class="cart-item-qty-row">
                    <div class="qty-control-box">
                        <button class="btn-qty-adj" onclick="changeQty('${item.maSanPham}', -1)">-</button>
                        <span class="qty-display-num">${item.soLuong}</span>
                        <button class="btn-qty-adj" onclick="changeQty('${item.maSanPham}', 1)">+</button>
                    </div>
                    <button class="btn-remove-item" onclick="removeFromCart('${item.maSanPham}')">Xoá</button>
                </div>
            </div>
        `;
        itemsContainer.appendChild(row);
    });

    if (subtotalEl) {
        subtotalEl.textContent = formatCurrency(total);
    }
}

/**
 * Tăng hoặc giảm số lượng món đồ trong giỏ
 */
function changeQty(maSanPham, delta) {
    const item = cart.find(i => i.maSanPham === maSanPham);
    if (!item) return;

    item.soLuong += delta;
    if (item.soLuong <= 0) {
        removeFromCart(maSanPham);
    } else {
        saveCartToStorage();
        updateCartUI();
    }
}

/**
 * Xóa một món khỏi giỏ hàng
 */
function removeFromCart(maSanPham) {
    cart = cart.filter(i => i.maSanPham !== maSanPham);
    saveCartToStorage();
    updateCartUI();
    showToast('Đã xóa món đồ khỏi giỏ hàng.');
}

/**
 * Bật/tắt ngăn kéo giỏ hàng (Cart Drawer)
 */
function toggleCartDrawer(open) {
    const overlay = document.getElementById('cart-drawer-overlay');
    if (!overlay) return;

    if (open) {
        overlay.classList.add('active');
        document.body.style.overflow = 'hidden';
    } else {
        overlay.classList.remove('active');
        document.body.style.overflow = '';
    }
}

/**
 * Mở hộp thoại xem nhanh sản phẩm (Quick View Modal)
 */
function openQuickView(maSanPham) {
    const product = sampleProducts.find(p => p.maSanPham === maSanPham);
    if (!product) return;

    const modal = document.getElementById('quickview-modal');
    if (!modal) return;

    document.getElementById('modal-img').src = product.hinhAnh;
    document.getElementById('modal-cat').textContent = product.tenDanhMuc;
    document.getElementById('modal-title').textContent = product.tenSanPham;
    document.getElementById('modal-price').textContent = formatCurrency(product.giaBan);
    document.getElementById('modal-material').textContent = product.chatLieu;
    document.getElementById('modal-size').textContent = product.size;
    document.getElementById('modal-desc').textContent = product.moTa;

    const addBtn = document.getElementById('modal-btn-add');
    if (addBtn) {
        addBtn.onclick = () => {
            addToCart(product.maSanPham);
            closeQuickView();
        };
    }

    modal.classList.add('active');
    document.body.style.overflow = 'hidden';
}

/**
 * Đóng hộp thoại xem nhanh
 */
function closeQuickView() {
    const modal = document.getElementById('quickview-modal');
    if (modal) {
        modal.classList.remove('active');
        document.body.style.overflow = '';
    }
}

/**
 * Tra cứu bảo hành kim hoàn
 */
function lookupWarranty(ticketCode) {
    const input = document.getElementById('warranty-search-input');
    const code = (ticketCode || (input ? input.value : '')).trim().toUpperCase();

    if (!code) {
        showToast('Vui lòng nhập mã phiếu bảo hành (VD: BH2001, BH2002)');
        return;
    }

    if (input) input.value = code;

    const card = document.getElementById('warranty-result-card');
    const ticket = sampleWarrantyTickets[code];

    if (!ticket) {
        showToast(`Không tìm thấy thông tin cho mã phiếu: ${code}`);
        if (card) card.style.display = 'none';
        return;
    }

    // Nạp dữ liệu vào thẻ bảo hành
    document.getElementById('wb-ticket-no').textContent = ticket.soPhieu;
    document.getElementById('wb-customer').textContent = ticket.tenKhachHang;
    document.getElementById('wb-product').textContent = ticket.tenSanPham;
    document.getElementById('wb-date-in').textContent = ticket.ngayTiepNhan;
    document.getElementById('wb-date-out').textContent = ticket.ngayHenTra;
    document.getElementById('wb-cost').textContent = ticket.chiPhi === 0 ? 'Miễn phí bảo dưỡng' : formatCurrency(ticket.chiPhi);
    document.getElementById('wb-note').textContent = `${ticket.lyDo} — ${ticket.ghiChu}`;

    const badgeEl = document.getElementById('wb-status-badge');
    if (badgeEl) {
        if (ticket.trangThai === 'DaHoanTat') {
            badgeEl.className = 'badge-status-completed';
            badgeEl.textContent = 'Đã Hoàn Tất Dịch Vụ';
        } else {
            badgeEl.className = 'badge-status-processing';
            badgeEl.textContent = 'Đang Gia Công Bảo Dưỡng';
        }
    }

    if (card) {
        card.style.display = 'block';
        card.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
    }

    showToast(`Đã tìm thấy thông tin bảo hành ${code}`);
}

/**
 * Khởi tạo các sự kiện lắng nghe giao diện chung
 */
function initEventListeners() {
    // Nút mở/đóng giỏ hàng
    const openCartBtn = document.getElementById('btn-open-cart');
    const closeCartBtn = document.getElementById('btn-close-cart');
    const overlay = document.getElementById('cart-drawer-overlay');

    if (openCartBtn) {
        openCartBtn.addEventListener('click', () => toggleCartDrawer(true));
    }
    if (closeCartBtn) {
        closeCartBtn.addEventListener('click', () => toggleCartDrawer(false));
    }
    if (overlay) {
        overlay.addEventListener('click', (e) => {
            if (e.target === overlay) toggleCartDrawer(false);
        });
    }

    // Modal xem nhanh
    const closeModalBtn = document.getElementById('btn-close-modal');
    const modalBackdrop = document.getElementById('quickview-modal');
    if (closeModalBtn) {
        closeModalBtn.addEventListener('click', closeQuickView);
    }
    if (modalBackdrop) {
        modalBackdrop.addEventListener('click', (e) => {
            if (e.target === modalBackdrop) closeQuickView();
        });
    }

    // Nút tra cứu bảo hành
    const btnSearchWarranty = document.getElementById('btn-search-warranty');
    if (btnSearchWarranty) {
        btnSearchWarranty.addEventListener('click', () => lookupWarranty());
    }

    // Nút thanh toán đơn hàng (Mở form thông tin người nhận)
    const btnCheckout = document.getElementById('btn-proceed-checkout');
    if (btnCheckout) {
        btnCheckout.addEventListener('click', () => {
            if (cart.length === 0) {
                showToast('Giỏ hàng chưa có sản phẩm nào để đặt hàng.');
                return;
            }
            toggleCartDrawer(false);
            openCheckoutDialog();
        });
    }
}

/**
 * Mở hộp thoại thông tin đặt hàng online
 */
function openCheckoutDialog() {
    const defaultName = currentUser?.hoTen || '';
    const defaultPhone = currentUser?.soDienThoai || '';

    const name = prompt('Vui lòng nhập Họ tên người nhận hàng:', defaultName);
    if (!name) return;
    const phone = prompt('Vui lòng nhập Số điện thoại liên hệ:', defaultPhone);
    if (!phone) return;
    const address = prompt('Vui lòng nhập Địa chỉ giao hàng:', '45 Lê Lợi, Phường Bến Nghé, Quận 1, TP.HCM');
    if (!address) return;

    const total = cart.reduce((sum, item) => sum + item.giaBan * item.soLuong, 0);

    alert(`Blink Blink Haute Joaillerie xác nhận đơn hàng thành công!\n\n` +
          `• Mã đơn hàng: HD${Date.now().toString().slice(-4)}\n` +
          `• Khách hàng: ${name} (${phone})\n` +
          `• Địa chỉ: ${address}\n` +
          `• Tổng thanh toán: ${formatCurrency(total)}\n` +
          `• Hình thức: Chuyển khoản ngân hàng & Hộp quà kim hoàn sang trọng\n\n` +
          `Cảm ơn Quý khách đã tin tưởng lựa chọn Blink Blink Jewelry!`);

    cart = [];
    saveCartToStorage();
    updateCartUI();
    showToast('Đặt hàng thành công! Đơn hàng đang được điều phối.');
}

/**
 * Hiển thị thông báo Toast góc màn hình
 * @param {string} message - Nội dung thông báo
 */
function showToast(message) {
    let container = document.getElementById('luxury-toast-container');
    if (!container) {
        container = document.createElement('div');
        container.id = 'luxury-toast-container';
        container.className = 'luxury-toast-container';
        document.body.appendChild(container);
    }

    const toast = document.createElement('div');
    toast.className = 'luxury-toast-msg';
    toast.innerHTML = `
        <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="var(--gold-primary)" stroke-width="2.2"><path d="M22 11.08V12a10 10 0 1 1-5.93-9.14"/><polyline points="22 4 12 14.01 9 11.01"/></svg>
        <span>${message}</span>
    `;

    container.appendChild(toast);

    setTimeout(() => {
        toast.style.opacity = '0';
        toast.style.transform = 'translateY(10px)';
        toast.style.transition = 'all 0.3s ease';
        setTimeout(() => toast.remove(), 300);
    }, 3800);
}

// ====================================================================================
// KHỐI XỬ LÝ XÁC THỰC KHÁCH HÀNG & TRA CỨU ĐƠN HÀNG (AUTHENTICATION & ORDER TRACKING)
// Tuân thủ Clean Architecture và Prototype-First: Ưu tiên trải nghiệm mượt mà,
// kết hợp gọi REST API với cơ chế dự phòng dữ liệu mẫu chuẩn SeedData.sql
// ====================================================================================

/**
 * Khởi tạo hệ thống xác thực người dùng từ bộ nhớ trình duyệt (localStorage)
 */
function initAuth() {
    // 1. Phục hồi trạng thái phiên đăng nhập nếu đã lưu trước đó
    const savedUserJson = localStorage.getItem('blink_currentUser');
    if (savedUserJson) {
        try {
            const savedUser = JSON.parse(savedUserJson);
            if (savedUser && (savedUser.tenDangNhap || savedUser.hoTen)) {
                applyLoggedInUser(savedUser);
            }
        } catch (e) {
            console.error('Không thể phục hồi phiên người dùng:', e);
            localStorage.removeItem('blink_currentUser');
        }
    } else {
        // Khách vãng lai: Đảm bảo wrapper và menu ở trạng thái ẩn hoàn toàn
        const wrapper = document.querySelector('.user-auth-action-wrapper');
        if (wrapper) wrapper.classList.remove('logged-in');
        closeUserDropdown();
    }

    // 2. Sự kiện nhấn nút Tài Khoản trên Header
    const authBtn = document.getElementById('btn-auth-user');
    const dropdown = document.getElementById('user-dropdown-menu');

    if (authBtn) {
        authBtn.addEventListener('click', (e) => {
            e.stopPropagation();
            if (currentUser) {
                // Đã đăng nhập: Bật/tắt menu dropdown cá nhân
                if (dropdown) dropdown.classList.toggle('open');
            } else {
                // Chưa đăng nhập: Đảm bảo đóng dropdown và mở Modal đăng nhập
                closeUserDropdown();
                openAuthModal('login');
            }
        });
    }

    // 3. Đóng dropdown khi nhấn ra ngoài khu vực menu
    document.addEventListener('click', (e) => {
        if (dropdown && dropdown.classList.contains('open')) {
            if (!dropdown.contains(e.target) && e.target !== authBtn && !authBtn.contains(e.target)) {
                closeUserDropdown();
            }
        }
    });

    // 4. Sự kiện đóng Modal Đăng nhập / Đăng ký
    const closeAuthBtn = document.getElementById('btn-close-auth');
    const authBackdrop = document.getElementById('auth-modal');

    if (closeAuthBtn) {
        closeAuthBtn.addEventListener('click', closeAuthModal);
    }
    if (authBackdrop) {
        authBackdrop.addEventListener('click', (e) => {
            if (e.target === authBackdrop) closeAuthModal();
        });
    }

    // 5. Sự kiện đóng Ngăn kéo Đơn hàng
    const closeOrdersBtn = document.getElementById('btn-close-orders');
    const ordersOverlay = document.getElementById('orders-drawer-overlay');

    if (closeOrdersBtn) {
        closeOrdersBtn.addEventListener('click', closeOrdersDrawer);
    }
    if (ordersOverlay) {
        ordersOverlay.addEventListener('click', (e) => {
            if (e.target === ordersOverlay) closeOrdersDrawer();
        });
    }
}

/**
 * Mở hộp thoại Đăng nhập / Đăng ký
 * @param {'login'|'register'} defaultTab - Tab hiển thị mặc định
 */
function openAuthModal(defaultTab = 'login') {
    const modal = document.getElementById('auth-modal');
    if (!modal) return;

    switchAuthTab(defaultTab);
    modal.classList.add('active');
    document.body.style.overflow = 'hidden';
}

/**
 * Đóng hộp thoại Đăng nhập / Đăng ký
 */
function closeAuthModal() {
    const modal = document.getElementById('auth-modal');
    if (modal) {
        modal.classList.remove('active');
        document.body.style.overflow = '';
    }
}

/**
 * Chuyển đổi qua lại giữa Tab Đăng nhập và Tab Đăng ký
 * @param {'login'|'register'} tab - Tên tab cần kích hoạt
 */
function switchAuthTab(tab) {
    const tabLoginBtn = document.getElementById('tab-btn-login');
    const tabRegBtn = document.getElementById('tab-btn-register');
    const paneLogin = document.getElementById('pane-login');
    const paneReg = document.getElementById('pane-register');

    if (tab === 'login') {
        if (tabLoginBtn) tabLoginBtn.classList.add('active');
        if (tabRegBtn) tabRegBtn.classList.remove('active');
        if (paneLogin) paneLogin.classList.add('active');
        if (paneReg) paneReg.classList.remove('active');
    } else {
        if (tabLoginBtn) tabLoginBtn.classList.remove('active');
        if (tabRegBtn) tabRegBtn.classList.add('active');
        if (paneLogin) paneLogin.classList.remove('active');
        if (paneReg) paneReg.classList.add('active');
    }
}

/**
 * Tự động điền tài khoản mẫu và đăng nhập nhanh chỉ với 1 cú click chuột
 * Phục vụ kiểm thử tức thì kịch bản người dùng trong SeedData.sql
/**
 * Xử lý biểu mẫu Đăng nhập tài khoản khách hàng
 * Gửi yêu cầu xác thực trực tiếp tới RESTful API POST /api/XacThuc/dang-nhap
 * @param {Event} [event] - Sự kiện submit form
 */
async function handleLoginSubmit(event) {
    if (event) event.preventDefault();

    const usernameInput = document.getElementById('login-username');
    const passwordInput = document.getElementById('login-password');

    const username = usernameInput ? usernameInput.value.trim() : '';
    const password = passwordInput ? passwordInput.value : '';

    if (!username || !password) {
        showToast('Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.');
        return;
    }

    try {
        const response = await fetch('/api/XacThuc/dang-nhap', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            body: JSON.stringify({
                TenDangNhap: username,
                MatKhau: password
            })
        });

        const result = await response.json();

        if (response.ok && result.success) {
            const data = result.data || result;
            const userObj = {
                maNguoiDung: data.maNguoiDung || data.MaNguoiDung || ('USER_' + username),
                maKhachHang: data.maKhachHang || data.MaKhachHang || 'KH001',
                hoTen: data.hoTen || data.HoTen || username,
                tenDangNhap: username,
                soDienThoai: data.soDienThoai || data.SoDienThoai || '',
                maVaiTro: data.maVaiTro || data.MaVaiTro || 'KHACHHANG'
            };

            applyLoggedInUser(userObj);
            localStorage.setItem('blink_currentUser', JSON.stringify(userObj));
            closeAuthModal();
            showToast(`Chào mừng quý khách ${userObj.hoTen} trở lại với Blink Blink!`);
        } else {
            alert(result.message || 'Tên đăng nhập hoặc mật khẩu không chính xác.');
        }
    } catch (err) {
        console.error('Lỗi khi gọi API đăng nhập:', err);
        alert('Không thể kết nối tới máy chủ xác thực. Vui lòng thử lại sau.');
    }
}

/**
 * Xử lý biểu mẫu Đăng ký tài khoản khách hàng mới
 * Gửi yêu cầu lưu trữ trực tiếp vào CSDL qua API POST /api/KhachHang/dang-ky
 * @param {Event} [event] - Sự kiện submit form
 */
async function handleRegisterSubmit(event) {
    if (event) event.preventDefault();

    const hoTen = document.getElementById('reg-fullname')?.value.trim();
    const sdt = document.getElementById('reg-phone')?.value.trim();
    const ngaySinh = document.getElementById('reg-dob')?.value;
    const username = document.getElementById('reg-username')?.value.trim();
    const password = document.getElementById('reg-password')?.value;

    if (!hoTen || !sdt || !username || !password) {
        showToast('Vui lòng điền đầy đủ các trường thông tin bắt buộc (*).');
        return;
    }

    try {
        const response = await fetch('/api/KhachHang/dang-ky', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            body: JSON.stringify({
                HoTen: hoTen,
                SoDienThoai: sdt,
                NgaySinh: ngaySinh || '1995-01-01',
                TenDangNhap: username,
                MatKhau: password
            })
        });

        const result = await response.json();

        if (response.ok && result.success) {
            showToast(`Đăng ký thành công! Đang tiến hành đăng nhập...`);

            // Tự động gọi xác thực đăng nhập để thiết lập phiên làm việc
            try {
                const loginRes = await fetch('/api/XacThuc/dang-nhap', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json', 'Accept': 'application/json' },
                    body: JSON.stringify({ TenDangNhap: username, MatKhau: password })
                });

                if (loginRes.ok) {
                    const loginData = await loginRes.json();
                    const info = loginData.data || loginData;
                    const loggedUser = {
                        maNguoiDung: info.maNguoiDung || info.MaNguoiDung,
                        maKhachHang: info.maKhachHang || info.MaKhachHang,
                        hoTen: info.hoTen || info.HoTen || hoTen,
                        tenDangNhap: username,
                        soDienThoai: sdt,
                        maVaiTro: 'KHACHHANG'
                    };
                    applyLoggedInUser(loggedUser);
                    localStorage.setItem('blink_currentUser', JSON.stringify(loggedUser));
                    closeAuthModal();
                    showToast(`Chào mừng ${hoTen} gia nhập hội viên Blink Blink Jewelry!`);
                    return;
                }
            } catch {
                // Nếu login tự động gặp trở ngại, chuyển sang tab đăng nhập cho khách tự đăng nhập
            }

            switchAuthTab('login');
            const loginUserInput = document.getElementById('login-username');
            if (loginUserInput) loginUserInput.value = username;
            showToast('Đăng ký tài khoản thành công! Quý khách vui lòng đăng nhập.');
        } else {
            alert(result.message || 'Không thể đăng ký tài khoản. Vui lòng kiểm tra lại thông tin.');
        }
    } catch (err) {
        console.error('Lỗi khi gọi API đăng ký:', err);
        alert('Không thể kết nối tới máy chủ đăng ký. Vui lòng thử lại sau.');
    }
}

/**
 * Cập nhật giao diện Header và menu người dùng sau khi đã xác thực
 * @param {Object} user - Thông tin thực thể người dùng
 */
function applyLoggedInUser(user) {
    currentUser = user;

    const authBtn = document.getElementById('btn-auth-user');
    const wrapper = document.querySelector('.user-auth-action-wrapper');
    const nameLabel = document.getElementById('user-display-name');
    const menuFullName = document.getElementById('menu-user-fullname');
    const menuUserId = document.getElementById('menu-user-id');

    if (nameLabel) {
        nameLabel.textContent = user.hoTen || user.HoTen || user.tenDangNhap;
    }

    if (menuFullName) {
        menuFullName.textContent = user.hoTen || user.HoTen || 'Quý Khách';
    }

    if (menuUserId) {
        const idText = user.maKhachHang || user.MaKhachHang || user.maNguoiDung || 'VIP';
        menuUserId.textContent = `Hội Viên Danh Dự #${idText}`;
    }

    if (authBtn) {
        authBtn.classList.add('logged-in');
        authBtn.setAttribute('title', `Tài khoản: ${user.hoTen} - Nhấn để mở menu tùy chọn`);
    }

    if (wrapper) {
        wrapper.classList.add('logged-in');
    }
}

/**
 * Đóng menu dropdown cá nhân
 */
function closeUserDropdown() {
    const dropdown = document.getElementById('user-dropdown-menu');
    if (dropdown) {
        dropdown.classList.remove('open');
        dropdown.classList.remove('active');
    }
}

/**
 * Đăng xuất người dùng, đưa giao diện về trạng thái vãng lai ngay lập tức
 */
function logoutUser() {
    currentUser = null;
    localStorage.removeItem('blink_currentUser');

    const authBtn = document.getElementById('btn-auth-user');
    const wrapper = document.querySelector('.user-auth-action-wrapper');
    const nameLabel = document.getElementById('user-display-name');
    const menuFullName = document.getElementById('menu-user-fullname');
    const menuUserId = document.getElementById('menu-user-id');

    // 1. Khôi phục nhãn trên nút Header về trạng thái chưa đăng nhập
    if (nameLabel) {
        nameLabel.textContent = 'Đăng Nhập / Đăng Ký';
    }

    // 2. Reset thông tin họ tên và mã hội viên trong dropdown menu về mặc định
    if (menuFullName) {
        menuFullName.textContent = 'Khách Hàng';
    }
    if (menuUserId) {
        menuUserId.textContent = 'Thành viên Blink Blink';
    }

    // 3. Gỡ bỏ toàn bộ class trạng thái logged-in để menu không bao giờ bung ra khi chưa đăng nhập
    if (authBtn) {
        authBtn.classList.remove('logged-in');
        authBtn.removeAttribute('title');
    }

    if (wrapper) {
        wrapper.classList.remove('logged-in');
    }

    // 4. Giữ nguyên vẹn giỏ hàng hiện tại, cập nhật lại giao diện giỏ hàng
    updateCartUI();

    // 5. Đóng ngay dropdown và ngăn kéo đơn hàng
    closeUserDropdown();
    closeOrdersDrawer();
    showToast('Quý khách đã đăng xuất tài khoản thành công.');
}

/**
 * Mở Ngăn kéo xem danh sách lịch sử đơn hàng của người dùng hiện tại
 */
function openOrdersDrawer() {
    closeUserDropdown();

    if (!currentUser) {
        showToast('Vui lòng đăng nhập để tra cứu lịch sử mua hàng.');
        openAuthModal('login');
        return;
    }

    renderCustomerOrders(currentUser);

    const overlay = document.getElementById('orders-drawer-overlay');
    if (overlay) {
        overlay.classList.add('active');
        document.body.style.overflow = 'hidden';
    }
}

/**
 * Đóng Ngăn kéo danh sách đơn hàng
 */
function closeOrdersDrawer() {
    const overlay = document.getElementById('orders-drawer-overlay');
    if (overlay) {
        overlay.classList.remove('active');
        document.body.style.overflow = '';
    }
}

/**
 * Vẽ danh sách hóa đơn đơn hàng của khách hàng lên Drawer
 * @param {Object} user - Đối tượng khách hàng đang đăng nhập
 */
function renderCustomerOrders(user) {
    const container = document.getElementById('orders-list-container');
    if (!container) return;

    // Lọc các hóa đơn thuộc về người dùng này theo mã người dùng hoặc mã khách hàng
    const userOrders = sampleOrders.filter(o => 
        o.maKhachHang === user.maNguoiDung || 
        o.maKhachHang === user.maKhachHang ||
        o.soDienThoai === user.soDienThoai
    );

    if (userOrders.length === 0) {
        container.innerHTML = `
            <div style="text-align: center; padding: 60px 24px; color: var(--text-muted);">
                <svg width="48" height="48" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" style="margin-bottom: 12px; opacity: 0.6;">
                    <path d="M6 2L3 6v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2V6l-3-4z"/>
                    <line x1="3" y1="6" x2="21" y2="6"/>
                    <path d="M16 10a4 4 0 0 1-8 0"/>
                </svg>
                <h4 style="font-family: var(--font-heading); font-size: 1.15rem; color: var(--dark-obsidian); margin-bottom: 8px;">Chưa Có Giao Dịch Nào</h4>
                <p style="font-size: 0.85rem; line-height: 1.5;">Hội viên ${user.hoTen} chưa có đơn đặt hàng kim hoàn nào được ghi nhận trên hệ thống.</p>
                <button type="button" class="btn-luxury-outline" style="margin-top: 18px; font-size: 0.78rem;" onclick="closeOrdersDrawer()">
                    Khám Phá Tuyệt Tác Trang Sức
                </button>
            </div>
        `;
        return;
    }

    container.innerHTML = '';

    userOrders.forEach(order => {
        const card = document.createElement('div');
        card.className = 'order-card-item';

        // Xác định huy hiệu trạng thái giao dịch
        let badgeClass = 'status-badge-danggiao';
        if (order.trangThai === 'DaGiao') {
            badgeClass = 'status-badge-dagiao';
        } else if (order.trangThai === 'ChoDuyet') {
            badgeClass = 'status-badge-choduyet';
        }

        // Tạo danh sách sản phẩm trong hóa đơn
        const itemsHtml = order.sanPham.map(p => `
            <div class="order-mini-product">
                <img src="${p.hinhAnh}" alt="${p.tenSanPham}" class="order-mini-img" />
                <div class="order-mini-info">
                    <div style="font-weight: 600; color: var(--dark-obsidian);">${p.tenSanPham}</div>
                    <div style="font-size: 0.75rem; color: var(--text-muted);">${p.chatLieu}</div>
                    <div style="font-size: 0.78rem; color: var(--gold-hover); margin-top: 2px;">
                        ${formatCurrency(p.donGia)} × ${p.soLuong}
                    </div>
                </div>
            </div>
        `).join('');

        card.innerHTML = `
            <div class="order-card-head">
                <div>
                    <span class="order-id-label">${order.maHoaDon}</span>
                    <div class="order-date-text">Ngày đặt: ${order.ngayLap}</div>
                </div>
                <span class="${badgeClass}">${order.trangThaiText}</span>
            </div>

            <div class="order-items-preview">
                ${itemsHtml}
            </div>

            <div class="order-meta-info-row">
                <div><strong>Nơi nhận:</strong> ${order.diaChi}</div>
                <div><strong>Phương thức:</strong> ${order.phuongThuc}</div>
                ${order.tienGiamGia > 0 ? `<div style="color: #2E7D32;"><strong>Ưu đãi:</strong> -${formatCurrency(order.tienGiamGia)}</div>` : ''}
            </div>

            <div class="order-foot-row">
                <span class="order-foot-label">Tổng thanh toán:</span>
                <span class="order-foot-total">${formatCurrency(order.tongTien)}</span>
            </div>
        `;

        container.appendChild(card);
    });
}

