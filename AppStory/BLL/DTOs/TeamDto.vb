Public Class TeamDto
    Public Property TeamId As Integer
    Public Property TeamName As String
    Public Property Description As String
    
    ' For Display in UI
    Public Property LeaderNames As String = ""
    Public Property MemberNames As String = ""

    ' For Data Transfer from UI to BLL
    Public Property LeaderIds As New List(Of Integer)
    Public Property MemberIds As New List(Of Integer)
End Class
