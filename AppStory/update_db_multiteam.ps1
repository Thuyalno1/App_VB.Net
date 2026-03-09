# Script cập nhật Database hỗ trợ Nhiều Nhóm trên 1 Dự án
# Cần cấu hình ODBC connection string hoặc có thể chạy các lệnh SQL này trực tiếp trong MySQL Workbench/phpMyAdmin:

# 1. Tạo bảng trung gian ProjectTeam
param (
    [string]$Server = "localhost",
    [string]$Port = "3306",
    [string]$User = "root",
    [string]$Password = "thuymv",
    [string]$Database = "appstore"
)

$connString = "Driver={MySQL ODBC 9.6 Unicode Driver};Server=$Server;Port=$Port;Database=$Database;User=$User;Password=$Password;Option=3;"

$sqlCreate = @"
-- Tạo bảng trung gian
CREATE TABLE IF NOT EXISTS ProjectTeam (
    ProjectId INT NOT NULL,
    TeamId INT NOT NULL,
    PRIMARY KEY (ProjectId, TeamId),
    FOREIGN KEY (ProjectId) REFERENCES Project(ProjectId) ON DELETE CASCADE,
    FOREIGN KEY (TeamId) REFERENCES Team(TeamId) ON DELETE CASCADE
);
"@

$sqlMigrate = @"
-- Migrate dữ liệu cũ: Copy các TeamId đang có sẵn ở Project sang bảng mới ProjectTeam
INSERT INTO ProjectTeam (ProjectId, TeamId)
SELECT ProjectId, TeamId 
FROM Project 
WHERE TeamId IS NOT NULL;
"@

try {
    $conn = New-Object System.Data.Odbc.OdbcConnection($connString)
    $conn.Open()
    
    $cmd = New-Object System.Data.Odbc.OdbcCommand($sqlCreate, $conn)
    $cmd.ExecuteNonQuery()
    
    $cmd2 = New-Object System.Data.Odbc.OdbcCommand($sqlMigrate, $conn)
    $cmd2.ExecuteNonQuery()
    
    Write-Host "Cập nhật Database thành công!" -ForegroundColor Green
} catch {
    Write-Host "Lỗi: $($_.Exception.Message)" -ForegroundColor Red
} finally {
    if ($conn.State -eq 'Open') {
        $conn.Close()
    }
}
