Public Class Task

    Public Property TaskId As Integer
    Public Property Title As String
    Public Property Description As String
    Public Property AssignedToUserId As Integer?
    Public Property CreatedByUserId As Integer
    Public Property Status As String        ' Pending / In Progress / Completed
    Public Property Priority As String      ' High / Medium / Low
    Public Property CreatedAt As DateTime
    Public Property DueDate As DateTime?
    Public Property IsDeleted As Boolean    ' Soft Delete: True = đã xóa
    Public Property ProjectId As Integer?   ' Thuộc Dự án nào (Nullable: Có thể không thuộc dự án)
    Public Property TeamId As Integer?      ' Thuộc Nhóm/Team nào (Nullable)
    Public Property AssignedUserName As String ' Tên người được giao (dùng để hiển thị, không lưu DB)

End Class
