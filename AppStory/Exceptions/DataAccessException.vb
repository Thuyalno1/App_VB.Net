''' <summary>
''' Exception dành cho lỗi xảy ra ở tầng DAL (Repository / Database).
''' Ném ra khi có lỗi kết nối DB, câu lệnh SQL lỗi, hoặc thao tác CRUD thất bại.
''' </summary>
''' <example>
''' ' Trong Repository:
''' Try
'''     ' thao tác DB...
''' Catch ex As Exception
'''     Throw New DataAccessException("Không thể đọc dữ liệu Task từ cơ sở dữ liệu.", ex)
''' End Try
''' </example>
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
