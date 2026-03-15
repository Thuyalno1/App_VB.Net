Public Class Task

    Public Property TaskId As Integer
    Public Property Title As String
    Public Property Description As String
    Public Property AssignedToUserId As Integer?
    Public Property CreatedByUserId As Integer
    Public Property Progress As Integer      ' 0% → chưa bắt đầu, 50% → đang thực hiện, 90% → chờ duyệt, 100% → hoàn thành
    Public Property Priority As String      ' High / Medium / Low
    Public Property CreatedAt As DateTime
    Public Property DueDate As DateTime?
    Public Property IsDeleted As Boolean    ' Soft Delete: True = đã xóa
    Public Property ProjectId As Integer?   ' Thuộc Dự án nào (Nullable: Có thể không thuộc dự án)
    Public Property TeamId As Integer?      ' Thuộc Nhóm/Team nào (Nullable)
    Public Property AssignedUserName As String ' Tên người được giao (dùng để hiển thị, không lưu DB)
    
    Public Property IsApproved As Boolean   ' True = đã được manager/admin duyệt
    
    Public ReadOnly Property ProgressDisplay As String
        Get
            If Progress = 100 Then
                Return If(IsApproved, "Đã duyệt", "Chưa duyệt")
            End If
            Return $"{Progress}%"
        End Get
    End Property

End Class
