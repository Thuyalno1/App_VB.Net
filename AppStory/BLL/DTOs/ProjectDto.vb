Public Class ProjectDto
    Public Property ProjectId As Integer
    Public Property ProjectName As String
    Public Property Description As String
    Public Property StartDate As DateTime?
    Public Property EndDate As DateTime?
    Public Property Status As String
    Public Property ManagerId As Integer?
    Public Property TeamIds As New List(Of Integer)()
End Class
