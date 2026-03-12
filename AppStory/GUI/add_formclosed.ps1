Get-ChildItem -Path . -Filter *.vb -Recurse | Where-Object { $_.Name -like 'frm*.vb' -and $_.Name -notlike '*.Designer.vb' } | ForEach-Object {
    $content = Get-Content $_.FullName -Raw
    if ($content -notmatch 'Private Sub \w+_FormClosed\(sender As Object, e As FormClosedEventArgs\) Handles MyBase.FormClosed') {
        $formName = $_.BaseName
        $insertStr = "
    Private Sub ${formName}_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub

"
        $content = $content -replace 'End Class', ("$insertStr" + "End Class")
        Set-Content -Path $_.FullName -Value $content
        Write-Host "Updated $formName"
    } else {
        Write-Host "Skipped $formName (already has FormClosed)"
    }
}
