Public Class Project
    Public Property ProjectId As Integer
    Public Property ProjectName As String
    Public Property Description As String
    Public Property StartDate As DateTime?
    Public Property EndDate As DateTime?
    Public Property Status As String ' Planning, Active, On Hold, Completed
    Public Property ManagerId As Integer? ' Project Manager
    Public Property CreatedAt As DateTime
End Class
