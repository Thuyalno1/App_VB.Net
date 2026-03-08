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
