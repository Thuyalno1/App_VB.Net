Imports System.Linq
Imports System.Drawing

Public Class frmDashboard
    Inherits System.Windows.Forms.Form
    Private _isNavigatingBack As Boolean = False

    Private ReadOnly _projectService As IProjectService
    Private ReadOnly _taskService As ITaskService
    Private _allProjects As List(Of Project)
    Private _activeFilter As String = "Total"
    Private _selectedCardPanel As Panel = Nothing

    ' Danh sách lưu trữ theo phân loại để thống kê và filter đồng nhất
    Private _projectsCompleted As New List(Of Project)
    Private _projectsOverdue As New List(Of Project)
    Private _projectsActive As New List(Of Project)
    Private _projectsPlanning As New List(Of Project)

    ' Màu gốc của các card để khôi phục khi bỏ chọn
    Private ReadOnly _cardColors As New Dictionary(Of String, System.Drawing.Color) From {
        {"Total", System.Drawing.Color.FromArgb(37, 99, 235)},
        {"Đang thực hiện", System.Drawing.Color.FromArgb(245, 158, 11)},
        {"Hoàn thành", System.Drawing.Color.FromArgb(16, 185, 129)},
        {"Quá hạn", System.Drawing.Color.FromArgb(220, 38, 38)},
        {"Lập kế hoạch", System.Drawing.Color.FromArgb(107, 114, 128)}
    }

    Public Sub New()
        InitializeComponent()
        _projectService = New ProjectService()
        _taskService = New TaskService()
    End Sub

    Private Sub frmDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupGrid()
        SetupCardClickHandlers()
        LoadDashboard()
    End Sub

    Private Sub SetupGrid()
        dgvProjects.AutoGenerateColumns = False
        dgvProjects.Columns.Clear()
        dgvProjects.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "ProjectName", .HeaderText = "Tên Dự Án", .Width = 200})
        dgvProjects.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "StatusDisplay", .HeaderText = "Trạng Thái", .Width = 110})
        AddHandler dgvProjects.CellFormatting, AddressOf dgvProjects_CellFormatting
        dgvProjects.Columns.Add(New DataGridViewTextBoxColumn() With {
            .DataPropertyName = "StartDate",
            .HeaderText = "Ngày Bắt Đầu",
            .Width = 110,
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "dd/MM/yyyy"}
        })
        dgvProjects.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "EndDate", .HeaderText = "Ngày Kết Thúc", .Width = 110, .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "dd/MM/yyyy"}})
        dgvProjects.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "TaskCount", .HeaderText = "Số Task", .Width = 70, .DefaultCellStyle = New DataGridViewCellStyle() With {.Alignment = DataGridViewContentAlignment.MiddleCenter}})
        dgvProjects.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Description", .HeaderText = "Mô Tả", .Width = 200})

        ' Cấu hình dgvTasks (Chi tiết công việc dự án)
        dgvTasks.AutoGenerateColumns = False
        dgvTasks.Columns.Clear()
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Title", .HeaderText = "Tên Công Việc", .Width = 250})
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "AssignedUserName", .HeaderText = "Người Thực Hiện", .Width = 150})
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "ProgressDisplay", .HeaderText = "Tiến độ", .Width = 100, .DefaultCellStyle = New DataGridViewCellStyle() With {.Alignment = DataGridViewContentAlignment.MiddleCenter}})
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Priority", .HeaderText = "Ưu Tiên", .Width = 80})
        AddHandler dgvTasks.CellFormatting, AddressOf dgvTasks_CellFormatting
    End Sub

    ''' <summary>Đăng ký click handler cho tất cả card panels</summary>
    Private Sub SetupCardClickHandlers()
        Dim cards() As Panel = {pnlCardTotal, pnlCardActive, pnlCardCompleted, pnlCardOverdue, pnlCardPlanning}
        For Each card In cards
            AddHandler card.Click, AddressOf Card_Click
            ' Đăng ký click handler cho cả label con (vì click trên label không bubble lên panel)
            For Each ctrl As Control In card.Controls
                AddHandler ctrl.Click, AddressOf CardChild_Click
                ctrl.Cursor = Cursors.Hand
            Next
        Next
    End Sub

    ''' <summary>Tải dữ liệu và cập nhật thống kê</summary>
    Private Sub LoadDashboard()
        Try
            _allProjects = _projectService.GetAllProjects()
            If _allProjects Is Nothing Then _allProjects = New List(Of Project)()

            ' Tính toán thống kê - Phân loại tuyệt đối (1 dự án chỉ vào 1 giỏ duy nhất)
            _projectsCompleted.Clear()
            _projectsOverdue.Clear()
            _projectsActive.Clear()
            _projectsPlanning.Clear()

            For Each p In _allProjects
                ' 1. Hoàn thành: Có status hoàn thành hoặc tất cả task đã được duyệt
                Dim isCompleted As Boolean = (p.Status = "Completed" OrElse p.Status = "Hoàn thành" OrElse (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount))
                
                If isCompleted Then
                    _projectsCompleted.Add(p)
                Else
                    ' Nếu chưa hoàn thành, kiểm tra ngày hết hạn
                    Dim isOverdue As Boolean = (p.EndDate.HasValue AndAlso p.EndDate.Value.Date < DateTime.Now.Date)
                    
                    If isOverdue Then
                        _projectsOverdue.Add(p)
                    Else
                        ' Nếu chưa hoàn thành, chưa quá hạn, kiểm tra xem có đang Active không
                        Dim isActive As Boolean = (p.Status = "Active" OrElse p.Status = "Đang thực hiện")
                        If isActive Then
                            _projectsActive.Add(p)
                        Else
                            ' Còn lại là Chưa bắt đầu hoặc Lập kế hoạch
                            _projectsPlanning.Add(p)
                        End If
                    End If
                End If
            Next

            ' Cập nhật số liệu trên card - Tổng card con CHẮC CHẮN bằng Tổng dự án
            lblCardTotalCount.Text = _allProjects.Count.ToString()
            lblCardActiveCount.Text = _projectsActive.Count.ToString()
            lblCardCompletedCount.Text = _projectsCompleted.Count.ToString()
            lblCardOverdueCount.Text = _projectsOverdue.Count.ToString()
            lblCardPlanningCount.Text = _projectsPlanning.Count.ToString()

            ' Hiển thị tất cả dự án mặc định
            ApplyFilter("Total")

        Catch ex As BusinessException
            MessageBox.Show("Lỗi tải dữ liệu dashboard: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>Lọc DataGridView theo filter đang chọn</summary>
    Private Sub ApplyFilter(filter As String)
        If _allProjects Is Nothing Then Return

        _activeFilter = filter
        Dim filtered As List(Of Project) = Nothing

        Select Case filter
            Case "Total"
                filtered = _allProjects
                lblFilterInfo.Text = $"📋 Hiển thị: Tất cả dự án ({_allProjects.Count})"
            Case "Completed", "Hoàn thành"
                filtered = _projectsCompleted
                lblFilterInfo.Text = $"✅ Hiển thị: Dự án hoàn thành ({filtered.Count})"
            Case "Overdue", "Quá hạn"
                filtered = _projectsOverdue
                lblFilterInfo.Text = $"⚠️ Hiển thị: Dự án quá deadline ({filtered.Count})"
            Case "Active", "Đang thực hiện"
                filtered = _projectsActive
                lblFilterInfo.Text = $"🔄 Hiển thị: Dự án đang thực hiện ({filtered.Count})"
            Case "Planning", "Lập kế hoạch"
                filtered = _projectsPlanning
                lblFilterInfo.Text = $"📝 Hiển thị: Dự án chưa bắt đầu ({filtered.Count})"
            Case Else
                filtered = _allProjects
        End Select

        dgvProjects.DataSource = Nothing
        dgvProjects.DataSource = filtered

        ' Highlight card đang chọn
        HighlightSelectedCard(filter)
    End Sub

    ''' <summary>Highlight card đang chọn (viền trắng đậm), bỏ highlight các card khác</summary>
    Private Sub HighlightSelectedCard(tag As String)
        Dim cards() As Panel = {pnlCardTotal, pnlCardActive, pnlCardCompleted, pnlCardOverdue, pnlCardPlanning}
        For Each card In cards
            Dim cardTag As String = If(card.Tag?.ToString(), "")
            If cardTag = tag Then
                card.BorderStyle = BorderStyle.FixedSingle
                _selectedCardPanel = card
            Else
                card.BorderStyle = BorderStyle.None
            End If
        Next
    End Sub

    ' ──────────────────────────────────────────────
    '   GRID EVENTS
    ' ──────────────────────────────────────────────
    Private Sub dgvProjects_SelectionChanged(sender As Object, e As EventArgs) Handles dgvProjects.SelectionChanged
        If dgvProjects.SelectedRows.Count = 0 Then
            lblTaskStats.Text = "Chọn dự án để xem thống kê..."
            dgvTasks.DataSource = Nothing
            Return
        End If

        Try
            Dim proj = CType(dgvProjects.SelectedRows(0).DataBoundItem, Project)
            If proj Is Nothing Then Return

            lblTaskDetailTitle.Text = $"📌 CHI TIẾT CÔNG VIỆC: {proj.ProjectName.ToUpper()}"

            Dim tasks = _taskService.GetTasksByProjectId(proj.ProjectId)
            If tasks Is Nothing Then tasks = New List(Of Task)()

            ' Tính toán thống kê theo yêu cầu: hoàn thành (bao gồm cả chưa duyệt), đã duyệt, chờ duyệt
            ' Hoàn thành: 100% (bất kể approved)
            ' Đã duyệt: 100% + IsApproved
            ' Chờ duyệt: 90%
            Dim pending = tasks.Where(Function(t) t.Progress = 90 OrElse (t.Progress = 100 AndAlso Not t.IsApproved)).Count
            Dim completed = tasks.Where(Function(t) t.Progress = 100).Count
            Dim approved = tasks.Where(Function(t) t.Progress = 100 AndAlso t.IsApproved).Count

            lblTaskStats.Text = $"Tổng Task: {tasks.Count}  |  ✅ Hoàn thành: {completed}  |  ✔️ Đã duyệt: {approved}  |  ⏳ Chờ duyệt: {pending}"

            dgvTasks.DataSource = Nothing
            dgvTasks.DataSource = tasks

        Catch ex As Exception
            ' Silent fail or log
        End Try
    End Sub

    ' ──────────────────────────────────────────────
    '   CARD CLICK HANDLERS
    ' ──────────────────────────────────────────────
    Private Sub Card_Click(sender As Object, e As EventArgs)
        Dim card As Panel = TryCast(sender, Panel)
        If card Is Nothing Then Return
        Dim filter As String = If(card.Tag?.ToString(), "Total")
        ApplyFilter(filter)
    End Sub

    ''' <summary>Khi click label con bên trong card → delegate lên card cha</summary>
    Private Sub CardChild_Click(sender As Object, e As EventArgs)
        Dim ctrl As Control = TryCast(sender, Control)
        If ctrl IsNot Nothing AndAlso ctrl.Parent IsNot Nothing Then
            Card_Click(ctrl.Parent, e)
        End If
    End Sub

    ' ──────────────────────────────────────────────
    '   NAVIGATION
    ' ──────────────────────────────────────────────
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        _isNavigatingBack = True
        Dim mainForm As New frmMain()
        mainForm.Show()
        Me.Close()
    End Sub

    Private Sub frmDashboard_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If Not _isNavigatingBack Then
            Application.Exit()
        End If
    End Sub

    Private Sub dgvTasks_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If e.RowIndex < 0 Then Return
        Dim grid = DirectCast(sender, DataGridView)
        Dim t = TryCast(grid.Rows(e.RowIndex).DataBoundItem, Task)
        If t Is Nothing Then Return
        Dim progress As Integer = t.Progress
        Dim dueDate As DateTime? = t.DueDate
        If progress = 100 Then
            e.CellStyle.BackColor = Drawing.Color.FromArgb(16, 185, 129)   ' Xanh la - Hoan thanh
            e.CellStyle.ForeColor = Drawing.Color.White
            e.CellStyle.SelectionBackColor = Drawing.Color.FromArgb(5, 150, 105)
        ElseIf dueDate.HasValue AndAlso dueDate.Value.Date < DateTime.Now.Date Then
            e.CellStyle.BackColor = Drawing.Color.FromArgb(231, 76, 60)
            e.CellStyle.ForeColor = Drawing.Color.White
            e.CellStyle.SelectionBackColor = Drawing.Color.FromArgb(192, 57, 43)
        ElseIf progress > 0 AndAlso progress < 100 Then
            e.CellStyle.BackColor = Drawing.Color.FromArgb(245, 158, 11)   ' Cam - Dang thuc hien
            e.CellStyle.ForeColor = Drawing.Color.White
            e.CellStyle.SelectionBackColor = Drawing.Color.FromArgb(217, 119, 6)
        ElseIf progress = 0 Then
            e.CellStyle.BackColor = Drawing.Color.FromArgb(107, 114, 128) ' Xam - Chua bat dau
            e.CellStyle.ForeColor = Drawing.Color.White
            e.CellStyle.SelectionBackColor = Drawing.Color.FromArgb(75, 85, 99)
        End If
    End Sub

    Private Sub dgvProjects_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If dgvProjects.Columns(e.ColumnIndex).DataPropertyName = "StatusDisplay" AndAlso e.Value IsNot Nothing Then
            Dim p = DirectCast(dgvProjects.Rows(e.RowIndex).DataBoundItem, Project)
            If p IsNot Nothing Then
                Dim isCompleted As Boolean = (p.Status = "Completed" OrElse p.Status = "Hoàn thành" OrElse (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount))
                Dim isOverdue As Boolean = (p.EndDate.HasValue AndAlso p.EndDate.Value.Date < DateTime.Now.Date AndAlso Not isCompleted)

                If isCompleted Then
                    e.CellStyle.BackColor = Drawing.Color.FromArgb(16, 185, 129) ' Green
                    e.CellStyle.ForeColor = Drawing.Color.White
                ElseIf isOverdue Then
                    e.CellStyle.BackColor = Drawing.Color.FromArgb(220, 38, 38) ' Red
                    e.CellStyle.ForeColor = Drawing.Color.White
                Else
                    Select Case p.Status
                        Case "Active", "Đang thực hiện"
                            e.CellStyle.BackColor = Drawing.Color.FromArgb(245, 158, 11) ' Orange
                            e.CellStyle.ForeColor = Drawing.Color.White
                        Case "Planning", "Lập kế hoạch"
                            e.CellStyle.BackColor = Drawing.Color.FromArgb(107, 114, 128) ' Gray
                            e.CellStyle.ForeColor = Drawing.Color.White
                    End Select
                End If
            End If
        End If
    End Sub

End Class
