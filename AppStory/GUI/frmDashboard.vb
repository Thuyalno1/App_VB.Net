Imports System.Linq

Public Class frmDashboard
    Inherits System.Windows.Forms.Form
    Private _isNavigatingBack As Boolean = False

    Private ReadOnly _projectService As IProjectService
    Private ReadOnly _taskService As ITaskService
    Private _allProjects As List(Of Project)
    Private _activeFilter As String = "Total"
    Private _selectedCardPanel As Panel = Nothing

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
        dgvProjects.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "ProjectId", .HeaderText = "ID", .Width = 45})
        dgvProjects.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "ProjectName", .HeaderText = "Tên Dự Án", .Width = 200})
        dgvProjects.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Status", .HeaderText = "Trạng Thái", .Width = 110})
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

            ' Tính toán thống kê
            Dim total As Integer = _allProjects.Count
            Dim active = _allProjects.Where(Function(p) (p.Status = "Active" OrElse p.Status = "Đang thực hiện") AndAlso Not (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count
            Dim completed = _allProjects.Where(Function(p) p.Status = "Completed" OrElse p.Status = "Hoàn thành" OrElse (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count
            Dim overdue = _allProjects.Where(Function(p) p.EndDate.HasValue AndAlso p.EndDate.Value < DateTime.Now AndAlso p.Status <> "Completed" AndAlso p.Status <> "Hoàn thành" AndAlso Not (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count
            Dim planning As Integer = _allProjects.Where(Function(p) p.Status = "Planning" OrElse p.Status = "Lập kế hoạch").Count

            ' Cập nhật số liệu trên card
            lblCardTotalCount.Text = total.ToString()
            lblCardActiveCount.Text = active.ToString()
            lblCardCompletedCount.Text = completed.ToString()
            lblCardOverdueCount.Text = overdue.ToString()
            lblCardPlanningCount.Text = planning.ToString()

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
            Case "Active", "Đang thực hiện"
                filtered = _allProjects.Where(Function(p) (p.Status = "Active" OrElse p.Status = "Đang thực hiện") AndAlso Not (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).ToList()
                lblFilterInfo.Text = $"🔄 Hiển thị: Dự án đang thực hiện ({filtered.Count})"
            Case "Completed", "Hoàn thành"
                filtered = _allProjects.Where(Function(p) p.Status = "Completed" OrElse p.Status = "Hoàn thành" OrElse (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).ToList()
                lblFilterInfo.Text = $"✅ Hiển thị: Dự án hoàn thành ({filtered.Count})"
            Case "Overdue", "Quá hạn"
                filtered = _allProjects.Where(Function(p) p.EndDate.HasValue AndAlso p.EndDate.Value < DateTime.Now AndAlso p.Status <> "Completed" AndAlso p.Status <> "Hoàn thành" AndAlso Not (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).ToList()
                lblFilterInfo.Text = $"⚠️ Hiển thị: Dự án quá deadline ({filtered.Count})"
            Case "Planning", "Lập kế hoạch"
                filtered = _allProjects.Where(Function(p) p.Status = "Planning" OrElse p.Status = "Lập kế hoạch").ToList()
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

End Class
