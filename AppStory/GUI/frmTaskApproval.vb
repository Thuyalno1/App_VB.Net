Imports System.Drawing

Public Class frmTaskApproval
    Inherits Form

    Private ReadOnly _taskService As ITaskService
    Private _allTasks As List(Of Task)
    Private Const PageSize As Integer = 7
    Private _currentPage As Integer = 1
    Private _totalPages As Integer = 1

    Public Sub New()
        InitializeComponent()
        _taskService = New TaskService()
    End Sub

    Private Sub frmTaskApproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupGrid()
        LoadPendingTasks()
    End Sub

    Private Sub SetupGrid()
        dgvPendingTasks.AutoGenerateColumns = False
        dgvPendingTasks.Columns.Clear()

        dgvPendingTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "AssignedUserName", .HeaderText = "Người phụ trách", .Width = 150})
        dgvPendingTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Title", .HeaderText = "Tiêu đề", .Width = 250})
        dgvPendingTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Priority", .HeaderText = "Mức ưu tiên", .Width = 100})
        dgvPendingTasks.Columns.Add(New DataGridViewTextBoxColumn() With {
            .DataPropertyName = "DueDate",
            .HeaderText = "Deadline",
            .Width = 120,
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "dd/MM/yyyy"}
        })
        
        ' Nút Duyệt
        Dim btnApprove As New DataGridViewButtonColumn()
        btnApprove.Name = "ApproveColumn"
        btnApprove.HeaderText = ""
        btnApprove.Text = "Duyệt"
        btnApprove.UseColumnTextForButtonValue = True
        btnApprove.Width = 80
        btnApprove.FlatStyle = FlatStyle.Flat
        btnApprove.DefaultCellStyle.BackColor = Color.FromArgb(16, 185, 129) ' Xanh
        btnApprove.DefaultCellStyle.ForeColor = Color.White
        dgvPendingTasks.Columns.Add(btnApprove)

        ' Nút Từ chối
        Dim btnReject As New DataGridViewButtonColumn()
        btnReject.Name = "RejectColumn"
        btnReject.HeaderText = ""
        btnReject.Text = "Từ chối"
        btnReject.UseColumnTextForButtonValue = True
        btnReject.Width = 80
        btnReject.FlatStyle = FlatStyle.Flat
        btnReject.DefaultCellStyle.BackColor = Color.FromArgb(220, 38, 38) ' Đỏ
        btnReject.DefaultCellStyle.ForeColor = Color.White
        dgvPendingTasks.Columns.Add(btnReject)
    End Sub

    Private Sub LoadPendingTasks()
        Try
            _allTasks = _taskService.GetPendingApprovalTasks()
            _currentPage = 1
            DisplayPage()
        Catch ex As BusinessException
            MessageBox.Show("Lỗi khi tải danh sách chờ duyệt: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DisplayPage()
        If _allTasks Is Nothing Then Return

        ' Calculate pagination
        _totalPages = Math.Max(1, CInt(Math.Ceiling(_allTasks.Count / PageSize)))
        If _currentPage > _totalPages Then _currentPage = _totalPages
        If _currentPage < 1 Then _currentPage = 1

        ' Slice data
        Dim pagedData = _allTasks.Skip((_currentPage - 1) * PageSize).Take(PageSize).ToList()

        ' Bind
        dgvPendingTasks.DataSource = Nothing
        dgvPendingTasks.DataSource = pagedData

        ' UI Updates
        lblPageInfo.Text = $"Trang {_currentPage} / {_totalPages}"
        btnPrev.Enabled = (_currentPage > 1)
        btnNext.Enabled = (_currentPage < _totalPages)
        lblCount.Text = $"Số lượng: {pagedData.Count} / {_allTasks.Count} công việc đang chờ duyệt."
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        If _currentPage > 1 Then
            _currentPage -= 1
            DisplayPage()
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If _currentPage < _totalPages Then
            _currentPage += 1
            DisplayPage()
        End If
    End Sub

    Private Sub dgvPendingTasks_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPendingTasks.CellClick
        If e.RowIndex < 0 Then Return

        Dim taskObj As Task = CType(dgvPendingTasks.Rows(e.RowIndex).DataBoundItem, Task)
        If taskObj Is Nothing Then Return

        ' Xử lý bấm nút Duyệt
        If e.ColumnIndex = dgvPendingTasks.Columns("ApproveColumn").Index Then
            Dim confirm = MessageBox.Show($"Duyệt công việc '{taskObj.Title}' thành Đã hoàn thành?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                Dim result = _taskService.UpdateStatus(taskObj.TaskId, "Đã hoàn thành")
                If result.Success Then
                    MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LoadPendingTasks()
                Else
                    MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        End If

        ' Xử lý bấm nút Từ chối
        If e.ColumnIndex = dgvPendingTasks.Columns("RejectColumn").Index Then
            Dim confirm = MessageBox.Show($"Từ chối công việc '{taskObj.Title}' và trả về Đang thực hiện?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If confirm = DialogResult.Yes Then
                Dim result = _taskService.UpdateStatus(taskObj.TaskId, "Đang thực hiện")
                If result.Success Then
                    MessageBox.Show("Đã từ chối công việc. Trạng thái đã chuyển về Đang thực hiện.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LoadPendingTasks()
                Else
                    MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim mainForm As New frmMain()
        mainForm.Show()
        Me.Close()
    End Sub
End Class
