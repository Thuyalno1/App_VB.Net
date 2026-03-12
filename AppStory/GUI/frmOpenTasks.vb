Public Class frmOpenTasks
    Inherits Form

    Private ReadOnly _taskService As ITaskService
    Private _allOpenTasks As List(Of Task)
    Private Const PageSize As Integer = 7
    Private _currentPage As Integer = 1
    Private _totalPages As Integer = 1

    Public Sub New()
        InitializeComponent()
        _taskService = New TaskService()
    End Sub

    Private Sub frmOpenTasks_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupGrid()
        LoadOpenTasks()
    End Sub

    Private Sub SetupGrid()
        dgvOpenTasks.AutoGenerateColumns = False
        dgvOpenTasks.Columns.Clear()

        dgvOpenTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "TaskId", .HeaderText = "ID", .Width = 50})
        dgvOpenTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Title", .HeaderText = "Tiêu đề", .Width = 250})
        dgvOpenTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Priority", .HeaderText = "Mức ưu tiên", .Width = 100})
        dgvOpenTasks.Columns.Add(New DataGridViewTextBoxColumn() With {
            .DataPropertyName = "DueDate",
            .HeaderText = "Deadline",
            .Width = 120,
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "dd/MM/yyyy"}
        })
        dgvOpenTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "ProjectId", .HeaderText = "Dự án", .Width = 60})

        ' Thêm nút Nhận việc
        Dim btnCol As New DataGridViewButtonColumn()
        btnCol.Name = "ClaimColumn"
        btnCol.HeaderText = "Thao tác"
        btnCol.Text = "Nhận việc"
        btnCol.UseColumnTextForButtonValue = True
        btnCol.Width = 100
        btnCol.FlatStyle = FlatStyle.Flat
        btnCol.DefaultCellStyle.BackColor = Color.FromArgb(5, 150, 105)
        btnCol.DefaultCellStyle.ForeColor = Color.White
        dgvOpenTasks.Columns.Add(btnCol)
    End Sub

    Private Sub LoadOpenTasks()
        Try
            _allOpenTasks = _taskService.GetOpenTasksForUser(SessionManager.CurrentUser.UserId)
            _currentPage = 1
            DisplayPage()
        Catch ex As BusinessException
            MessageBox.Show("Lỗi khi tải danh sách việc: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DisplayPage()
        If _allOpenTasks Is Nothing Then Return

        ' 1. Calculate pagination
        _totalPages = Math.Max(1, CInt(Math.Ceiling(_allOpenTasks.Count / PageSize)))
        If _currentPage > _totalPages Then _currentPage = _totalPages
        If _currentPage < 1 Then _currentPage = 1

        ' 2. Slice data
        Dim pagedData = _allOpenTasks.Skip((_currentPage - 1) * PageSize).Take(PageSize).ToList()

        ' 3. Bind
        dgvOpenTasks.DataSource = Nothing
        dgvOpenTasks.DataSource = pagedData

        ' 4. UI Updates
        lblPageInfo.Text = $"Trang {_currentPage} / {_totalPages}"
        btnPrev.Enabled = (_currentPage > 1)
        btnNext.Enabled = (_currentPage < _totalPages)
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

    Private Sub dgvOpenTasks_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvOpenTasks.CellClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = dgvOpenTasks.Columns("ClaimColumn").Index Then
            Dim taskObj As Task = CType(dgvOpenTasks.Rows(e.RowIndex).DataBoundItem, Task)
            If taskObj IsNot Nothing Then
                Dim confirm As DialogResult = MessageBox.Show($"Bạn có chắc chắn muốn nhận công việc '{taskObj.Title}' này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If confirm = DialogResult.Yes Then
                    Dim result = _taskService.ClaimTask(taskObj.TaskId, SessionManager.CurrentUser.UserId)
                    If result.Success Then
                        MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadOpenTasks() ' Tải lại lưới
                    Else
                        MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
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
