# Script cập nhật trạng thái Task sang tiếng Việt
param (
    [string]$Server = "localhost",
    [string]$Port = "3306",
    [string]$User = "root",
    [string]$Password = "thuymv",
    [string]$Database = "appstore"
)

$connString = "Driver={MySQL ODBC 9.6 Unicode Driver};Server=$Server;Port=$Port;Database=$Database;User=$User;Password=$Password;charset=utf8;"

$queries = @(
    "UPDATE Tasks SET Status = 'Chờ xử lý' WHERE Status = 'Pending'",
    "UPDATE Tasks SET Status = 'Đang thực hiện' WHERE Status = 'In Progress'",
    "UPDATE Tasks SET Status = 'Chờ duyệt' WHERE Status = 'Pending Approval'",
    "UPDATE Tasks SET Status = 'Đã hoàn thành' WHERE Status = 'Completed'"
)

try {
    $conn = New-Object System.Data.Odbc.OdbcConnection($connString)
    $conn.Open()
    
    foreach ($sql in $queries) {
        $cmd = New-Object System.Data.Odbc.OdbcCommand($sql, $conn)
        $rowsAffected = $cmd.ExecuteNonQuery()
        Write-Host "Xử lý: $sql. Số hàng affected: $rowsAffected"
    }
    
    Write-Host "Cập nhật trạng thái thành công!" -ForegroundColor Green
} catch {
    Write-Host "Lỗi: $($_.Exception.Message)" -ForegroundColor Red
} finally {
    if ($conn -and $conn.State -eq 'Open') {
        $conn.Close()
    }
}
