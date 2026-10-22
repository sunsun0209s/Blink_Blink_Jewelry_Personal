@echo off
chcp 65001 > nul
echo ==============================================================================
echo       BLINK BLINK HAUTE JOAILLERIE - KHỞI ĐỘNG HỆ THỐNG TOÀN DIỆN
echo       1. Web Storefront Khách Hàng (http://localhost:5095)
echo       2. Ứng Dụng WinForms Quản Trị Viên (Admin POS)
echo ==============================================================================
echo.

echo [1/2] Đang khởi động Web Server Khách Hàng (BlinkBlink.Web)...
start "BlinkBlink Web Server (Port 5095)" cmd /k "dotnet run --project src/BlinkBlink.Web --urls http://localhost:5095"

echo Đang chờ máy chủ web sẵn sàng trong 3 giây...
timeout /t 3 /nobreak > nul

echo Tự động mở trình duyệt Web Khách Hàng...
start http://localhost:5095/

echo.
echo [2/2] Đang khởi động Ứng dụng Desktop Quản trị (BlinkBlink.Admin)...
start "BlinkBlink WinForms Admin" cmd /c "dotnet run --project src/BlinkBlink.Admin"

echo.
echo Đã khởi động đồng thời cả 2 phân hệ thành công!
echo Quý khách có thể thao tác song song trên cả Web và Desktop.
echo.
pause
