Public Class TaskDto
    Public Property TaskId As Integer
    Public Property Title As String
    Public Property Description As String
    Public Property AssignedToUserId As Integer?
    Public Property Progress As Integer
    Public Property Priority As String      ' High / Medium / Low
    Public Property DueDate As DateTime?
    Public Property ProjectId As Integer?
    Public Property TeamId As Integer?
    Public Property TeamName As String
    Public Property IsApproved As Boolean
End Class
