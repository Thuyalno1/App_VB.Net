Add-Type -Path "d:\VSII\Learn_VSII\winfromapp\AppStory\packages\MySql.Data.8.0.33\lib\net462\MySql.Data.dll"

$connStr = "server=localhost;port=3306;database=appstore;uid=root;password=thuymv;charset=utf8;"
try {
    $conn = New-Object MySql.Data.MySqlClient.MySqlConnection($connStr)
    $conn.Open()
    $cmd = $conn.CreateCommand()
    $cmd.CommandText = "ALTER TABLE Tasks MODIFY COLUMN AssignedToUserId INT NULL;"
    $rows = $cmd.ExecuteNonQuery()
    Write-Host "Success: Altered AssignedToUserId to allow NULL. Rows affected: $rows"
    $conn.Close()
} catch {
    Write-Error $_.Exception.Message
}
