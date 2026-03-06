''' <summary>
''' Exception dành cho trường hợp không tìm thấy đối tượng trong hệ thống.
''' Ném ra khi truy vấn theo ID hoặc điều kiện mà không có kết quả.
''' Tương đương HTTP 404 Not Found trong ứng dụng Web.
''' </summary>
''' <example>
''' ' Trong Repository:
''' Dim task = GetById(taskId)
''' If task Is Nothing Then
'''     Throw New NotFoundException($"Không tìm thấy Task với ID = {taskId}.")
''' End If
''' </example>
Public Class NotFoundException
    Inherits AppException

    ''' <summary>
    ''' Tên của entity không tìm thấy (ví dụ: "Task", "User", "Project").
    ''' </summary>
    Public ReadOnly Property EntityName As String

    ''' <summary>
    ''' ID hoặc key của entity không tìm thấy.
    ''' </summary>
    Public ReadOnly Property EntityKey As Object

    ''' <summary>
    ''' Khởi tạo NotFoundException với thông điệp tùy chỉnh.
    ''' </summary>
    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

    ''' <summary>
    ''' Khởi tạo NotFoundException với tên entity và ID cụ thể.
    ''' Tự động tạo thông điệp theo chuẩn.
    ''' </summary>
    Public Sub New(entityName As String, entityKey As Object)
        MyBase.New($"Không tìm thấy {entityName} với ID = {entityKey}.")
        Me.EntityName = entityName
        Me.EntityKey = entityKey
    End Sub

    ''' <summary>
    ''' Khởi tạo NotFoundException với thông điệp và exception gốc.
    ''' </summary>
    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub

End Class
