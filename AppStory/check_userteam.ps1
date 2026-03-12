$connString = "Driver={MySQL ODBC 9.6 Unicode Driver};Server=localhost;Port=3306;Database=appstore;Uid=root;Pwd=thuymv;charset=utf8;"
$conn = New-Object System.Data.Odbc.OdbcConnection($connString)
try {
    $conn.Open()
    $cmd = New-Object System.Data.Odbc.OdbcCommand("SELECT * FROM UserTeam", $conn)
    $reader = $cmd.ExecuteReader()
    Write-Host "UserTeam table entries:"
    while ($reader.Read()) {
        Write-Host ("UserId: " + $reader["UserId"] + " | TeamId: " + $reader["TeamId"] + " | Role: " + $reader["Role"])
    }
} catch {
    Write-Host ("Error: " + $_.Exception.Message)
} finally {
    if ($conn.State -eq 'Open') { $conn.Close() }
}
