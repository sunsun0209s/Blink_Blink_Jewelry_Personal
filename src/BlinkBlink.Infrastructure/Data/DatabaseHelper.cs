using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BlinkBlink.Infrastructure.Data;

/// <summary>
/// Lớp tiện ích trung tâm quản lý kết nối và thực thi các thao tác cơ sở dữ liệu qua ADO.NET thuần.
/// Khớp 100% với Class Diagram kỹ thuật sau khi chuyển đổi từ Clean Architecture.
/// </summary>
public class DatabaseHelper
{
    private readonly string _connectionString;

    /// <summary>
    /// Chuỗi kết nối tới SQL Server
    /// </summary>
    public string ConnectionString => _connectionString;

    /// <summary>
    /// Khởi tạo DatabaseHelper với chuỗi kết nối SQL Server
    /// </summary>
    /// <param name="connectionString">Chuỗi kết nối</param>
    public DatabaseHelper(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    /// <summary>
    /// Mở và trả về một kết nối SqlConnection mới
    /// </summary>
    /// <returns>Đối tượng SqlConnection đã mở</returns>
    public SqlConnection GetConnection()
    {
        var connection = new SqlConnection(_connectionString);
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
        return connection;
    }

    /// <summary>
    /// Kiểm tra xem có kết nối được tới cơ sở dữ liệu SQL Server hay không
    /// </summary>
    /// <returns>true nếu kết nối thành công, false nếu thất bại</returns>
    public bool TestConnection()
    {
        try
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn.State == ConnectionState.Open;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Thực thi lệnh SQL không trả về dữ liệu (INSERT, UPDATE, DELETE)
    /// </summary>
    public int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
    {
        using var conn = GetConnection();
        using var cmd = new SqlCommand(sql, conn);
        if (parameters != null && parameters.Length > 0)
        {
            cmd.Parameters.AddRange(parameters);
        }
        return cmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Thực thi lệnh SQL trả về một giá trị duy nhất (COUNT, SUM, MAX,...)
    /// </summary>
    public object? ExecuteScalar(string sql, params SqlParameter[] parameters)
    {
        using var conn = GetConnection();
        using var cmd = new SqlCommand(sql, conn);
        if (parameters != null && parameters.Length > 0)
        {
            cmd.Parameters.AddRange(parameters);
        }
        var result = cmd.ExecuteScalar();
        return result == DBNull.Value ? null : result;
    }

    /// <summary>
    /// Thực thi câu lệnh truy vấn và trả về bảng dữ liệu DataTable trong bộ nhớ
    /// </summary>
    public DataTable ExecuteDataTable(string sql, params SqlParameter[] parameters)
    {
        using var conn = GetConnection();
        using var cmd = new SqlCommand(sql, conn);
        if (parameters != null && parameters.Length > 0)
        {
            cmd.Parameters.AddRange(parameters);
        }
        using var adapter = new SqlDataAdapter(cmd);
        var table = new DataTable();
        adapter.Fill(table);
        return table;
    }

    /// <summary>
    /// Tự động kiểm tra và cập nhật các cột/dữ liệu cần thiết cho CSDL hiện có,
    /// đảm bảo tương thích ngược và không gây lỗi khi chạy trên CSDL cũ chưa được nạp lại script mới.
    /// </summary>
    public void EnsureSchemaCompatibility()
    {
        try
        {
            const string migrationSql = @"
                IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.ChiTietHoaDon') AND name = 'PhanTramGiamGia')
                BEGIN
                    ALTER TABLE dbo.ChiTietHoaDon ADD PhanTramGiamGia INT NOT NULL CONSTRAINT DF_ChiTietHoaDon_PhanTramGiamGia DEFAULT 0;
                END

                IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.PhieuBaoHanh') AND name = 'ChiPhiUocTinh')
                BEGIN
                    ALTER TABLE dbo.PhieuBaoHanh ADD ChiPhiUocTinh DECIMAL(18,2) NOT NULL CONSTRAINT DF_PhieuBaoHanh_ChiPhiUocTinh DEFAULT 0;
                END

                IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.PhieuBaoHanh') AND name = 'ChiPhiThucTe')
                BEGIN
                    ALTER TABLE dbo.PhieuBaoHanh ADD ChiPhiThucTe DECIMAL(18,2) NOT NULL CONSTRAINT DF_PhieuBaoHanh_ChiPhiThucTe DEFAULT 0;
                END

                IF NOT EXISTS (SELECT 1 FROM dbo.NguoiDung WHERE MaNguoiDung = '0')
                BEGIN
                    INSERT INTO dbo.NguoiDung (MaNguoiDung, HoTen, SoDienThoai, TenDangNhap, MatKhau, MaVaiTro, TrangThai)
                    VALUES ('0', N'Khách Hàng Vãng Lai (POS)', '', 'khach.pos', '123456789', 'KHACHHANG', 1);

                    IF NOT EXISTS (SELECT 1 FROM dbo.KhachHang WHERE MaKhachHang = 'KH_GUEST_POS' OR MaNguoiDung = '0')
                    BEGIN
                        INSERT INTO dbo.KhachHang (MaNguoiDung, MaKhachHang, NgaySinh)
                        VALUES ('0', 'KH_GUEST_POS', '2000-01-01');
                    END
                END";

            ExecuteNonQuery(migrationSql);
        }
        catch
        {
            // Bỏ qua nếu không có quyền DDL hoặc database ngoại tuyến
        }
    }
}

