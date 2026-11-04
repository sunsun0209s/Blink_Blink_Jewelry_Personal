Trang bảo hành của admin và nhân viên sẽ có nút: Tiếp nhận mới, Đang xử lý, Đã xử lý, Hoàn tất & trả khách, Hủy phiếu
Bảo hành có 3 trạng thái: Tiếp nhận, Đang xử lý, Đã xử lý, Đã trả khách.
Nhân viên bán hàng sẽ là người tạo phiếu bảo hành, có thể ghi chi phí dự tính hoặc để trống cũng không sao, có phần để ghi tình trạng sản phẩm khi tiếp nhận, và phần ghi chú. Phiếu bảo hành vừa tạo sẽ có trạng thái là: Tiếp nhận.
Thêm nhân viên kỹ thuật, nhân viên kỹ thuật chỉ có thể thay đổi trạng thái của dịch vụ bảo hành từ tiếp nhận(trạng thái ban đầu khi phiếu bảo hành được khởi tạo bởi nhân viên bán hàng) -> Đang xử lý (để báo rằng sản phẩm đó đang được bảo hành) và từ Đang xử lý -> Đã xử lý (Báo rằng đã sửa xong), ghi nhận chi phí thực tế khi sửa chữa.

Nhân viên bán hàng khi trả lại sản phẩm cho khách sẽ chuyển trạng thái Đã xử lý -> Đã trả khách bằng việc nhấn Hoàn tất và trả khách. Nếu chi phi thực tế khác 0, thì sẽ hiện lên giao diện tính tiền bằng tiền mặt hoặc chuyển khoản giống trang bán offline, sau đó nhấn xác nhận thanh toán mới chuyển trạng thái của phiếu bảo hành. 

{vì trong database chưa có cơ chế nào gom nhóm các sản phẩm cùng mẫu nhưng khác size nên khi làm web, sản phẩm phải hiện thị thẻ size tại các <div> sản phẩm, phiếu nhập phải hiện tên sản phẩm kèm size}[để yên có thằng khác làm]

trang bán offline phải có ràng buộc về số lượng sản phẩm, sẽ kiểm tra số lượng sản phẩm khi nhân viên thực hiện thao tác thay đổi số lượng, và chỉ bán được những sản phẩm cho phép bán (tức là sản phẩm không bị ẩn khỏi trang mua sắm của người dùng), rà soát lại: có phải đối với khách vãng lai có để lại sdt cho đơn hàng, khi khách hàng tạo tài khoản với số điện thoại đó, hệ thống cũng sẽ tự liên kết lại các đơn hàng đó vô tài khoản vừa tạo.

