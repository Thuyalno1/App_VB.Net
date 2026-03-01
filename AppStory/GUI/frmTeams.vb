Public Class frmTeams
    Inherits System.Windows.Forms.Form

    Private ReadOnly _teamService As ITeamService
    Private ReadOnly _userRepo As IUserRepository
    Private _selectedTeamId As Integer = -1

    Public Sub New()
        InitializeComponent()
        _teamService = New TeamService()
        _userRepo = New UserRepository()
    End Sub

    Private Sub frmTeams_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupGrid()
        LoadUsersToCombo()
        LoadTeams()
    End Sub

    Private Sub SetupGrid()
        dgvTeams.AutoGenerateColumns = False
        dgvTeams.Columns.Clear()
        dgvTeams.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "TeamId", .HeaderText = "ID", .Width = 50})
        dgvTeams.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "TeamName", .HeaderText = "Tên Nhóm", .Width = 150})
        dgvTeams.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Description", .HeaderText = "Mô tả", .Width = 150})
        dgvTeams.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "LeaderNames", .HeaderText = "Trưởng nhóm (Tên)", .Width = 120})
        dgvTeams.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "MemberNames", .HeaderText = "Thành viên (Tên)", .Width = 120})
    End Sub

    Private Sub LoadUsersToCombo()
        Dim users = _userRepo.GetAll()
        
        clbLeaders.Items.Clear()
        clbMembers.Items.Clear()
        
        For Each u In users
            ' DisplayMember/ValueMember không support tốt bằng tay ở CheckedListBox như ComboBox
            ' Cách đơn giản nhất trong class demo là bọc object vào một class hiển thị.
            ' Trong VB, có thể override ToString() hoặc dùng struct.
            Dim item = New CListBoxItem With {.UserId = u.UserId, .UserName = u.UserName}
            clbLeaders.Items.Add(item)
            clbMembers.Items.Add(item)
        Next
    End Sub

    Private Class CListBoxItem
        Public Property UserId As Integer
        Public Property UserName As String
        Public Overrides Function ToString() As String
            Return UserName
        End Function
    End Class

    Private Sub LoadTeams()
        Try
            Dim teams = _teamService.GetAllTeams()
            dgvTeams.DataSource = Nothing
            dgvTeams.DataSource = teams
        Catch ex As Exception
            MessageBox.Show("Lỗi tải danh sách nhóm: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvTeams_SelectionChanged(sender As Object, e As EventArgs) Handles dgvTeams.SelectionChanged
        If dgvTeams.SelectedRows.Count = 0 Then Return
        If dgvTeams.SelectedRows(0).DataBoundItem Is Nothing Then Return
        Dim t = TryCast(dgvTeams.SelectedRows(0).DataBoundItem, TeamDto)
        If t Is Nothing Then Return

        _selectedTeamId = t.TeamId
        txtTeamName.Text = t.TeamName
        txtDescription.Text = t.Description
        
        ' Uncheck all first
        For i As Integer = 0 To clbLeaders.Items.Count - 1
            clbLeaders.SetItemChecked(i, False)
        Next
        For i As Integer = 0 To clbMembers.Items.Count - 1
            clbMembers.SetItemChecked(i, False)
        Next

        If t.LeaderIds IsNot Nothing Then
            For i As Integer = 0 To clbLeaders.Items.Count - 1
                Dim item = CType(clbLeaders.Items(i), CListBoxItem)
                If t.LeaderIds.Contains(item.UserId) Then
                    clbLeaders.SetItemChecked(i, True)
                End If
            Next
        End If

        If t.MemberIds IsNot Nothing Then
            For i As Integer = 0 To clbMembers.Items.Count - 1
                Dim item = CType(clbMembers.Items(i), CListBoxItem)
                If t.MemberIds.Contains(item.UserId) Then
                    clbMembers.SetItemChecked(i, True)
                End If
            Next
        End If
    End Sub

    Private Function GetSelectedLeaders() As List(Of Integer)
        Dim list As New List(Of Integer)
        For Each item In clbLeaders.CheckedItems
            list.Add(CType(item, CListBoxItem).UserId)
        Next
        Return list
    End Function

    Private Function GetSelectedMembers() As List(Of Integer)
        Dim list As New List(Of Integer)
        For Each item In clbMembers.CheckedItems
            list.Add(CType(item, CListBoxItem).UserId)
        Next
        Return list
    End Function

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim dto As New TeamDto() With {
            .TeamName = txtTeamName.Text.Trim(),
            .Description = txtDescription.Text.Trim(),
            .LeaderIds = GetSelectedLeaders(),
            .MemberIds = GetSelectedMembers()
        }
        
        Dim result = _teamService.CreateTeam(dto)
        If result.Success Then
            MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadTeams()
            ClearForm()
        Else
            MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If _selectedTeamId < 0 Then
            MessageBox.Show("Vui lòng chọn nhóm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        
        Dim dto As New TeamDto() With {
            .TeamId = _selectedTeamId,
            .TeamName = txtTeamName.Text.Trim(),
            .Description = txtDescription.Text.Trim(),
            .LeaderIds = GetSelectedLeaders(),
            .MemberIds = GetSelectedMembers()
        }
        
        Dim result = _teamService.UpdateTeam(dto)
        If result.Success Then
            MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadTeams()
            ClearForm()
        Else
            MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If _selectedTeamId < 0 Then
            MessageBox.Show("Vui lòng chọn nhóm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        
        Dim confirm = MessageBox.Show("Xóa nhóm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm = DialogResult.Yes Then
            Dim result = _teamService.DeleteTeam(_selectedTeamId)
            MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadTeams()
            ClearForm()
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub
    
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim mainForm As New frmMain()
        mainForm.Show()
        Me.Close()
    End Sub

    Private Sub ClearForm()
        _selectedTeamId = -1
        txtTeamName.Text = ""
        txtDescription.Text = ""
        For i As Integer = 0 To clbLeaders.Items.Count - 1
            clbLeaders.SetItemChecked(i, False)
        Next
        For i As Integer = 0 To clbMembers.Items.Count - 1
            clbMembers.SetItemChecked(i, False)
        Next
        dgvTeams.ClearSelection()
    End Sub

End Class
