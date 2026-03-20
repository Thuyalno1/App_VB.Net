Public Class Project
    Public Property ProjectId As Integer
    Public Property ProjectName As String
    Public Property Description As String
    Public Property StartDate As DateTime?
    Public Property EndDate As DateTime?
    Public Property Status As String ' Planning, Active, On Hold, Completed
    Public Property ManagerId As Integer? ' Project Manager
    Public Property CreatedAt As DateTime
    Public Property TaskCount As Integer ' Added for dashboard
    Public Property ApprovedTaskCount As Integer ' Added for dynamic completion logic

    ''' <summary>Hiển thị trạng thái tiếng Việt</summary>
    Public ReadOnly Property StatusDisplay As String
        Get
            If TaskCount > 0 AndAlso TaskCount = ApprovedTaskCount Then Return "Hoàn thành"
            Select Case Status
                Case "Planning" : Return "Lập kế hoạch"
                Case "Active" : Return "Đang thực hiện"
                Case "On Hold" : Return "Tạm dừng"
                Case "Completed" : Return "Hoàn thành"
                Case Else : Return If(Status, "")
            End Select
        End Get
    End Property
End Class
