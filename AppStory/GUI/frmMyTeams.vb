Public Class frmMyTeams

    Private ReadOnly _teamService As ITeamService

    Public Sub New()
        InitializeComponent()
        _teamService = New TeamService()
    End Sub

    Private Sub frmMyTeams_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupGrid()
        LoadMyTeams()
        lblUserInfo.Text = $"Các nhóm của {SessionManager.CurrentUser.UserName}"
    End Sub

    Private Sub SetupGrid()
        dgvMyTeams.AutoGenerateColumns = False
        dgvMyTeams.Columns.Clear()
        dgvMyTeams.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "TeamId", .HeaderText = "ID", .Width = 50})
        dgvMyTeams.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "TeamName", .HeaderText = "Tên Nhóm", .Width = 150})
        dgvMyTeams.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Description", .HeaderText = "Mô tả", .Width = 200})
        dgvMyTeams.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "LeaderNames", .HeaderText = "Trưởng nhóm (Tên)", .Width = 150})
        dgvMyTeams.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "MemberNames", .HeaderText = "Thành viên (Tên)", .Width = 180})
    End Sub

    Private Sub LoadMyTeams()
        Try
            ' Cải tiến: GetTeamsByUserId trả về List(Of Team) nguyên thuỷ
            ' Nếu cần hiển thị LeaderNames/MemberNames đẹp, nên gọi GetTeamById cho từng Team để lấy TeamDto, 
            ' hoặc cải tiến hàm GetAllTeams để lọc theo UserId.
            ' Về mặt kỹ thuật, mình có thể lấy GetAllTeams() (đã có đủ list user) và lọc ra các team có mặt UserId này trong đó
            Dim allTeamsDto = _teamService.GetAllTeams()
            Dim myTeams As New List(Of TeamDto)()

            If allTeamsDto IsNot Nothing Then
                Dim currentUserId = SessionManager.CurrentUser.UserId
                myTeams = allTeamsDto.Where(Function(t) (t.LeaderIds IsNot Nothing AndAlso t.LeaderIds.Contains(currentUserId)) OrElse (t.MemberIds IsNot Nothing AndAlso t.MemberIds.Contains(currentUserId))).ToList()
            End If

            dgvMyTeams.DataSource = Nothing
            dgvMyTeams.DataSource = myTeams
        Catch ex As BusinessException
            MessageBox.Show("Lỗi tải thông tin nhóm: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim mainForm As New frmMain()
        mainForm.Show()
        Me.Close()
    End Sub

    Private Sub btnCreateTeamTask_Click(sender As Object, e As EventArgs) Handles btnCreateTeamTask.Click
        If dgvMyTeams.SelectedRows.Count = 0 Then
            MessageBox.Show("Vui lòng chọn một nhóm trên lưới để tạo công việc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim selectedTeam = CType(dgvMyTeams.SelectedRows(0).DataBoundItem, TeamDto)
        If selectedTeam Is Nothing Then Return

        Dim currentUser = SessionManager.CurrentUser
        Dim role = If(currentUser.RoleId, "").ToLower()
        Dim isAdmin = (role = "admin")

        If Not isAdmin AndAlso (selectedTeam.LeaderIds Is Nothing OrElse Not selectedTeam.LeaderIds.Contains(currentUser.UserId)) Then
            MessageBox.Show("Chỉ Trưởng nhóm hoặc Quản trị viên mới có quyền tạo Task chung cho nhóm này.", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim f As New frmCreateTeamTask(selectedTeam.TeamId, selectedTeam.TeamName)
        If f.ShowDialog() = DialogResult.OK Then
            ' Nothing specific needed after returning unless we show stats
        End If
    End Sub

End Class
