''' <summary>
''' Base exception của toàn bộ ứng dụng AppStory.
''' Tất cả custom exception đều kế thừa từ class này.
''' </summary>
Public Class AppException
    Inherits Exception

    ''' <summary>
    ''' Khởi tạo AppException với thông điệp lỗi.
    ''' </summary>
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

    ''' <summary>
    ''' Khởi tạo AppException với thông điệp lỗi và exception gốc (inner exception).
    ''' </summary>
    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub

End Class
