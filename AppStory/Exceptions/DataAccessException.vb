Public Class DataAccessException
    Inherits AppException

    ''' <summary>
    ''' Khởi tạo DataAccessException với thông điệp lỗi.
    ''' </summary>
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

    ''' <summary>
    ''' Khởi tạo DataAccessException với thông điệp lỗi và exception gốc từ DB.
    ''' </summary>
    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub

End Class
