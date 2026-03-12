Public Class frmCreateTeamTask
    Private ReadOnly _teamId As Integer
    Private ReadOnly _teamName As String
    Private ReadOnly _taskService As ITaskService

    Public Sub New(teamId As Integer, teamName As String)
        InitializeComponent()
        _teamId = teamId
        _teamName = teamName
        _taskService = New TaskService()
    End Sub

    Private Sub frmCreateTeamTask_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = $"Tạo công việc mới - Nhóm {_teamName}"
        lblTeamName.Text = $"Nhóm: {_teamName}"

        cboPriority.Items.Clear()
        cboPriority.Items.AddRange({"High", "Medium", "Low"})
        cboPriority.SelectedIndex = 1

        dtpDueDate.Value = DateTime.Now.AddDays(7)

        ' Note: Quyền tạo task được kiểm tra từ màn hình gọi (frmMyTeams) 
        ' để đảm bảo cả Admin và Trưởng nhóm (kể cả role Employee) đều có quyền.
    End Sub

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        Dim titleStr As String = txtTitle.Text.Trim()
        If String.IsNullOrEmpty(titleStr) Then
            MessageBox.Show("Vui lòng nhập tiêu đề công việc.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim confirm As DialogResult = MessageBox.Show(
            "Tạo công việc này cho toàn bộ nhóm nhận?", "Xác nhận",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        
        If confirm = DialogResult.Yes Then
            Dim dto As New TaskDto() With {
                .Title = titleStr,
                .Description = txtDescription.Text.Trim(),
                .Status = "Chờ xử lý",
                .Priority = cboPriority.SelectedItem?.ToString(),
                .DueDate = dtpDueDate.Value,
                .TeamId = _teamId,
                .AssignedToUserId = Nothing   ' Task Pool
            }
            Dim result = _taskService.CreateTask(dto, SessionManager.CurrentUser.UserId)
            If result.Success Then
                MessageBox.Show("Đã tạo công việc thành công! Thành viên trong nhóm có thể vào nhận việc.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class
