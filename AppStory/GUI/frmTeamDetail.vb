Imports System.Drawing

Public Class frmTeamDetail
    Inherits Form

    Private ReadOnly _teamService As ITeamService
    Private ReadOnly _taskService As ITaskService
    Private ReadOnly _teamId As Integer

    ''' <summary>Tạo form chi tiết nhóm với teamId cụ thể</summary>
    Public Sub New(teamId As Integer)
        InitializeComponent()
        _teamId = teamId
        _teamService = New TeamService()
        _taskService = New TaskService()
    End Sub

    Private Sub frmTeamDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupMembersGrid()
        SetupTasksGrid()
        LoadTeamInfo()
        LoadMembers()
        LoadTasks()
    End Sub

    Private Sub SetupMembersGrid()
        dgvMembers.AutoGenerateColumns = False
        dgvMembers.Columns.Clear()
        dgvMembers.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "UserName", .HeaderText = "Tên tài khoản", .Width = 200})
        dgvMembers.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Email", .HeaderText = "Email", .Width = 250})
        dgvMembers.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Role", .HeaderText = "Vai trò", .Width = 120})
    End Sub

    Private Sub SetupTasksGrid()
        dgvTasks.AutoGenerateColumns = False
        dgvTasks.Columns.Clear()
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Title", .HeaderText = "Tiêu đề", .Width = 250})
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "AssignedUserName", .HeaderText = "Người phụ trách", .Width = 150})
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Status", .HeaderText = "Trạng thái", .Width = 120})
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Priority", .HeaderText = "Mức ưu tiên", .Width = 100})
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {
            .DataPropertyName = "DueDate",
            .HeaderText = "Deadline",
            .Width = 120,
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "dd/MM/yyyy"}
        })
    End Sub

    Private Sub LoadTeamInfo()
        Try
            Dim team = _teamService.GetTeamById(_teamId)
            If team IsNot Nothing Then
                lblTeamName.Text = team.TeamName
                lblDescription.Text = If(String.IsNullOrWhiteSpace(team.Description), "(Không có mô tả)", team.Description)
            End If
        Catch ex As BusinessException
            MessageBox.Show("Không thể tải thông tin nhóm: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As NotFoundException
            MessageBox.Show("Không tìm thấy nhóm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadMembers()
        Try
            Dim team = _teamService.GetTeamById(_teamId)
            If team Is Nothing Then Return

            ' Tạo danh sách gồm cả leaders và members
            Dim memberList As New List(Of TeamMemberDisplay)()

            ' Lấy Leaders
            Dim leaderUsers = New TeamRepository().GetUsersByTeamAndRole(_teamId, "Leader")
            For Each u In leaderUsers
                memberList.Add(New TeamMemberDisplay() With {
                    .UserName = u.UserName,
                    .Email = u.Email,
                    .Role = "Trưởng nhóm"
                })
            Next

            ' Lấy Members
            Dim memberUsers = New TeamRepository().GetUsersByTeamAndRole(_teamId, "Member")
            For Each u In memberUsers
                memberList.Add(New TeamMemberDisplay() With {
                    .UserName = u.UserName,
                    .Email = u.Email,
                    .Role = "Thành viên"
                })
            Next

            dgvMembers.DataSource = Nothing
            dgvMembers.DataSource = memberList

            lblMemberCount.Text = $"Tổng: {memberList.Count} thành viên ({leaderUsers.Count} trưởng nhóm, {memberUsers.Count} thành viên)"
        Catch ex As BusinessException
            MessageBox.Show("Không thể tải thành viên: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadTasks()
        Try
            Dim tasks = _taskService.GetTasksByTeamId(_teamId)
            dgvTasks.DataSource = Nothing
            dgvTasks.DataSource = tasks

            lblTaskCount.Text = $"Tổng: {tasks.Count} công việc"
        Catch ex As BusinessException
            MessageBox.Show("Không thể tải công việc: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub

    ''' <summary>Class hiển thị thành viên trong DataGridView</summary>
    Private Class TeamMemberDisplay
        Public Property UserName As String
        Public Property Email As String
        Public Property Role As String
    End Class
End Class
