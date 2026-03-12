$connString = "Driver={MySQL ODBC 9.6 Unicode Driver};Server=localhost;Port=3306;Database=appstore;Uid=root;Pwd=thuymv;charset=utf8;"
$conn = New-Object System.Data.Odbc.OdbcConnection($connString)
try {
    $conn.Open()
    $cmd = New-Object System.Data.Odbc.OdbcCommand("SELECT DISTINCT Role FROM UserTeam", $conn)
    $reader = $cmd.ExecuteReader()
    Write-Host "Roles found in UserTeam table:"
    while ($reader.Read()) {
        Write-Host ("- " + $reader["Role"])
    }
} catch {
    Write-Host ("Error: " + $_.Exception.Message)
} finally {
    if ($conn.State -eq 'Open') { $conn.Close() }
}
