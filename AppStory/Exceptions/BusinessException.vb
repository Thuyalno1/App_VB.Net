''' <summary>
''' Exception dành cho lỗi nghiệp vụ (Business Logic) ở tầng BLL/Service.
''' Ném ra khi dữ liệu vi phạm quy tắc nghiệp vụ: thiếu trường bắt buộc,
''' sai định dạng, không có quyền thực hiện thao tác, v.v.
''' </summary>
''' <example>
''' ' Trong Service:
''' If String.IsNullOrWhiteSpace(dto.Title) Then
'''     Throw New BusinessException("Tiêu đề công việc không được để trống.")
''' End If
''' </example>
Public Class BusinessException
    Inherits AppException

    ''' <summary>
    ''' Khởi tạo BusinessException với thông điệp mô tả vi phạm nghiệp vụ.
    ''' </summary>
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

    ''' <summary>
    ''' Khởi tạo BusinessException với thông điệp và exception gốc.
    ''' </summary>
    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub

End Class
