Public Class frmMyTasks
    Inherits System.Windows.Forms.Form

    Private ReadOnly _taskService As ITaskService
    Private _selectedTaskId As Integer = -1

    Public Sub New()
        InitializeComponent()
        _taskService = New TaskService()
    End Sub

    Private Sub frmMyTasks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupGrid()
        cboNewStatus.Items.AddRange({"Pending", "In Progress", "Completed"})
        cboNewStatus.SelectedIndex = 0
        LoadMyTasks()
        lblUserInfo.Text = $"Công việc của: {SessionManager.CurrentUser.UserName}"
    End Sub

    Private Sub SetupGrid()
        dgvMyTasks.AutoGenerateColumns = False
        dgvMyTasks.Columns.Clear()
        dgvMyTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "TaskId", .HeaderText = "ID", .Width = 45})
        dgvMyTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Title", .HeaderText = "Tiêu đề", .Width = 220})
        dgvMyTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Status", .HeaderText = "Trạng thái", .Width = 110})
        dgvMyTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Priority", .HeaderText = "Ưu tiên", .Width = 80})
        dgvMyTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "DueDate", .HeaderText = "Deadline", .Width = 110, .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "dd/MM/yyyy"}})
        dgvMyTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvMyTasks.ReadOnly = True
        dgvMyTasks.AllowUserToAddRows = False
    End Sub

    Private Sub LoadMyTasks()
        Try
            Dim tasks = _taskService.GetMyTasks(SessionManager.CurrentUser.UserId)
            dgvMyTasks.DataSource = Nothing
            dgvMyTasks.DataSource = tasks
        Catch ex As BusinessException
            MessageBox.Show("Lỗi tải task: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvMyTasks_SelectionChanged(sender As Object, e As EventArgs) Handles dgvMyTasks.SelectionChanged
        If dgvMyTasks.SelectedRows.Count = 0 Then Return
        Dim t = CType(dgvMyTasks.SelectedRows(0).DataBoundItem, Task)
        If t Is Nothing Then Return
        _selectedTaskId = t.TaskId
        cboNewStatus.SelectedItem = t.Status
        lblSelectedTask.Text = $"Đang chọn: [{t.TaskId}] {t.Title}"
    End Sub

    Private Sub btnUpdateStatus_Click(sender As Object, e As EventArgs) Handles btnUpdateStatus.Click
        If _selectedTaskId < 0 Then
            MessageBox.Show("Vui lòng chọn một công việc.", "Chưa chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim newStatus As String = cboNewStatus.SelectedItem?.ToString()
        Dim result = _taskService.UpdateStatus(_selectedTaskId, newStatus)
        If result.Success Then
            MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadMyTasks()
        Else
            MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim mainForm As New frmMain()
        mainForm.Show()
        Me.Close()
    End Sub

End Class
