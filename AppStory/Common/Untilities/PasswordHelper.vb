Imports System.Security.Cryptography
Imports System.Text

Public Class PasswordHelper

    ''' <summary>
    ''' Hash mật khẩu bằng SHA256, trả về chuỗi hex
    ''' </summary>
    Public Shared Function HashPassword(password As String) As String
        Using sha256 As New SHA256Managed
            Dim bytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            Dim sb As New StringBuilder
            For Each b As Byte In bytes
                sb.Append(b.ToString("x2"))
            Next
            Return sb.ToString
        End Using
    End Function

    ''' <summary>
    ''' Kiểm tra mật khẩu nhập vào có khớp với hash đã lưu không
    ''' </summary>
    Public Shared Function VerifyPassword(password As String, hash As String) As Boolean
        Dim inputHash As String = HashPassword(password)
        Return inputHash = hash
    End Function

End Class
