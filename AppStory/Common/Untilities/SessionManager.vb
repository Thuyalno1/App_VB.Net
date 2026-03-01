Public Class SessionManager

    ''' <summary>User hiện đang đăng nhập trong phiên</summary>
    Public Shared Property CurrentUser As User = Nothing

    ''' <summary>Kiểm tra có đang đăng nhập không</summary>
    Public Shared Function IsLoggedIn() As Boolean
        Return CurrentUser IsNot Nothing
    End Function

    ''' <summary>Đăng xuất – xóa thông tin phiên</summary>
    Public Shared Sub Logout()
        CurrentUser = Nothing
    End Sub

End Class
