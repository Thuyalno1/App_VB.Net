Imports System.Linq

Public Class frmMain
    Inherits System.Windows.Forms.Form
    Private _isLoggingOut As Boolean = False

    Private ReadOnly _teamService As ITeamService
    Private ReadOnly _projectService As IProjectService
    Private ReadOnly _taskService As ITaskService

    ' Cache dữ liệu để dùng khi click card
    Private _allProjects As List(Of Project)
    Private _myTasks As List(Of Task)
    ' Theo dõi thẻ đang được chọn (để toggle)
    Private _activeCardTag As String = ""
    ' Danh sách tất cả card panels để bỏ highlight
    Private _adminCards As System.Windows.Forms.Panel()
    Private _empCards As System.Windows.Forms.Panel()

    Public Sub New()
        InitializeComponent()
        _teamService = New TeamService()
        _projectService = New ProjectService()
        _taskService = New TaskService()
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Hiển thị ngày giờ
            lblDateTime.Text = $"📅 {DateTime.Now:dddd, dd/MM/yyyy  HH:mm}"

            If Not SessionManager.IsLoggedIn() Then
                MessageBox.Show("Phiên đăng nhập không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
                OpenLoginForm()
                Return
            End If

            Dim user As User = SessionManager.CurrentUser
            If user Is Nothing Then Return

            lblWelcome.Text = $"Xin chào, {user.UserName}!"
            lblRole.Text = $"Vai trò: {user.RoleId}"
            lblEmail.Text = $"Email: {user.Email}"

            Dim role As String = If(user.RoleId, "").ToLower()
            Select Case role
                Case "admin"
                    lblRoleDesc.Text = "Bạn có toàn quyền quản trị hệ thống."
                    pnlRoleBadge.BackColor = System.Drawing.Color.FromArgb(229, 62, 62)
                    btnGoTasks.Visible = True
                    btnGoTasks.Text = "📋 Quản Lý Công Việc (Admin)"
                    btnGoApproval.Visible = True
                    btnGoProjects.Visible = True
                    btnGoTeams.Visible = True
                    btnGoReport.Visible = True
                Case "manager"
                    lblRoleDesc.Text = "Bạn có quyền quản lý nhóm và phê duyệt."
                    pnlRoleBadge.BackColor = System.Drawing.Color.FromArgb(55, 80, 110)
                    btnGoTasks.Visible = True
                    btnGoTasks.Text = "📋 Quản Lý Công Việc (Manager)"
                    btnGoApproval.Visible = True
                    btnGoOpenTasks.Visible = True
                    btnGoMyTasks.Visible = True
                    btnGoMyTeams.Visible = True
                    btnGoProjects.Visible = True
                    btnGoReport.Visible = True
                Case Else ' Employee
                    Dim isLeader As Boolean = _teamService.IsUserTeamLeader(user.UserId)
                    If isLeader Then
                        lblRoleDesc.Text = "Bạn là Trưởng nhóm. Bạn có quyền quản lý công việc của nhóm mình."
                        pnlRoleBadge.BackColor = System.Drawing.Color.FromArgb(40, 90, 140)
                        btnGoTasks.Visible = True
                        btnGoTasks.Text = "📋 Quản Lý Công Việc (Leader)"
                    Else
                        lblRoleDesc.Text = "Bạn có thể xem và thực hiện các nhiệm vụ của mình."
                        pnlRoleBadge.BackColor = System.Drawing.Color.FromArgb(14, 165, 160)
                    End If
                    btnGoOpenTasks.Visible = True
                    btnGoMyTasks.Visible = True
                    btnGoMyTeams.Visible = True
            End Select

            ' Load số liệu thống kê
            LoadQuickStats(user)

            ' Đăng ký click handler cho các thẻ
            SetupCardClickHandlers(role)

            ' Wire nút đóng chi tiết
            AddHandler btnCloseDetail.Click, AddressOf CloseDetailPanel

        Catch ex As BusinessException
            MessageBox.Show("Lỗi khởi động ứng dụng: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ─── Setup card click handlers ───
    Private Sub SetupCardClickHandlers(role As String)
        If role = "admin" OrElse role = "manager" Then
            ' 5 thẻ Admin
            AddClickToCard(pnlStatTotal, lblStatTotalCount, lblStatTotalLabel, "Total")
            AddClickToCard(pnlStatActive, lblStatActiveCount, lblStatActiveLabel, "Active")
            AddClickToCard(pnlStatCompleted, lblStatCompletedCount, lblStatCompletedLabel, "Completed")
            AddClickToCard(pnlStatOverdue, lblStatOverdueCount, lblStatOverdueLabel, "Overdue")
            AddClickToCard(pnlStatPlanning, lblStatPlanningCount, lblStatPlanningLabel, "Planning")
            _adminCards = {pnlStatTotal, pnlStatActive, pnlStatCompleted, pnlStatOverdue, pnlStatPlanning}
        Else
            ' 4 thẻ Employee
            AddClickToCard(pnlEmpTotal, lblEmpTotalCount, lblEmpTotalLabel, "EmpTotal")
            AddClickToCard(pnlEmpInProgress, lblEmpInProgressCount, lblEmpInProgressLabel, "EmpInProgress")
            AddClickToCard(pnlEmpDone, lblEmpDoneCount, lblEmpDoneLabel, "EmpDone")
            AddClickToCard(pnlEmpDeadline, lblEmpDeadlineCount, lblEmpDeadlineLabel, "EmpDeadline")
            _empCards = {pnlEmpTotal, pnlEmpInProgress, pnlEmpDone, pnlEmpDeadline}
        End If
    End Sub

    Private Sub AddClickToCard(pnl As System.Windows.Forms.Panel,
                                lblCount As System.Windows.Forms.Label,
                                lblLabel As System.Windows.Forms.Label,
                                tag As String)
        pnl.Tag = tag
        AddHandler pnl.Click, AddressOf StatCard_Click
        lblCount.Tag = tag
        AddHandler lblCount.Click, AddressOf StatCard_Click
        lblLabel.Tag = tag
        AddHandler lblLabel.Click, AddressOf StatCard_Click
    End Sub

    Private Sub StatCard_Click(sender As Object, e As EventArgs)
        Dim ctrl = TryCast(sender, System.Windows.Forms.Control)
        If ctrl Is Nothing Then Return
        Dim tag As String = If(ctrl.Tag?.ToString(), "")
        If String.IsNullOrEmpty(tag) AndAlso ctrl.Parent IsNot Nothing Then
            tag = If(ctrl.Parent.Tag?.ToString(), "")
        End If

        ' Toggle: click lại thẻ đang chọn → ẩn chi tiết
        If tag = _activeCardTag AndAlso pnlCardDetail.Visible Then
            CloseDetailPanel(Nothing, Nothing)
            Return
        End If

        ShowCardDetail(tag)
    End Sub

    ''' <summary>Nhấn nút [✕] hoặc click lại thẻ để ẩn panel chi tiết</summary>
    Private Sub CloseDetailPanel(sender As Object, e As EventArgs)
        pnlCardDetail.Visible = False
        _activeCardTag = ""
        ClearCardHighlight()
    End Sub

    ''' <summary>Hiển thị DataGridView chi tiết tương ứng với card được click</summary>
    Private Sub ShowCardDetail(tag As String)
        If String.IsNullOrEmpty(tag) Then Return
        _activeCardTag = tag
        HighlightActiveCard(tag)

        ' Xóa cột cũ
        dgvCardDetail.AutoGenerateColumns = False
        dgvCardDetail.Columns.Clear()
        dgvCardDetail.DataSource = Nothing

        Select Case tag
            ' ─── ADMIN/MANAGER: Dự án ───
            Case "Total"
                SetupProjectDetailGrid()
                lblCardDetailTitle.Text = $"📁 Tất cả dự án ({_allProjects.Count})"
                dgvCardDetail.DataSource = _allProjects

            Case "Active"
                SetupProjectDetailGrid()
                Dim filtered = _allProjects.Where(Function(p) (p.Status = "Active" OrElse p.Status = "Đang thực hiện") AndAlso Not (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).ToList()
                lblCardDetailTitle.Text = $"🔄 Dự án đang thực hiện ({filtered.Count})"
                dgvCardDetail.DataSource = filtered

            Case "Completed"
                SetupProjectDetailGrid()
                Dim filtered = _allProjects.Where(Function(p) p.Status = "Completed" OrElse p.Status = "Hoàn thành" OrElse (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).ToList()
                lblCardDetailTitle.Text = $"✅ Dự án hoàn thành ({filtered.Count})"
                dgvCardDetail.DataSource = filtered

            Case "Overdue"
                SetupProjectDetailGrid()
                Dim filtered = _allProjects.Where(Function(p) p.EndDate.HasValue AndAlso p.EndDate.Value < DateTime.Now AndAlso p.Status <> "Completed" AndAlso p.Status <> "Hoàn thành" AndAlso Not (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).ToList()
                lblCardDetailTitle.Text = $"⚠️ Dự án quá deadline ({filtered.Count})"
                dgvCardDetail.DataSource = filtered

            Case "Planning"
                SetupProjectDetailGrid()
                Dim filtered = _allProjects.Where(Function(p) p.Status = "Planning" OrElse p.Status = "Lập kế hoạch").ToList()
                lblCardDetailTitle.Text = $"📝 Dự án chưa bắt đầu ({filtered.Count})"
                dgvCardDetail.DataSource = filtered

            ' ─── EMPLOYEE: Công việc ───
            Case "EmpTotal"
                SetupTaskDetailGrid()
                lblCardDetailTitle.Text = $"📋 Tất cả công việc của tôi ({_myTasks.Count})"
                dgvCardDetail.DataSource = _myTasks

            Case "EmpInProgress"
                SetupTaskDetailGrid()
                Dim filtered = _myTasks.Where(Function(t) t.Progress >= 1 AndAlso t.Progress <= 89).ToList()
                lblCardDetailTitle.Text = $"🔄 Việc đang làm ({filtered.Count})"
                dgvCardDetail.DataSource = filtered

            Case "EmpDone"
                SetupTaskDetailGrid()
                Dim filtered = _myTasks.Where(Function(t) t.Progress = 100).ToList()
                lblCardDetailTitle.Text = $"✅ Việc đã xong ({filtered.Count})"
                dgvCardDetail.DataSource = filtered

            Case "EmpDeadline"
                SetupTaskDetailGrid()
                Dim filtered = _myTasks.Where(Function(t) _
                    t.DueDate.HasValue AndAlso _
                    t.DueDate.Value <= DateTime.Now.AddDays(3) AndAlso _
                    t.DueDate.Value >= DateTime.Now.AddDays(-1) AndAlso _
                    t.Progress < 100).ToList()
                lblCardDetailTitle.Text = $"⚠️ Việc gần / quá deadline ({filtered.Count})"
                dgvCardDetail.DataSource = filtered
        End Select

        pnlCardDetail.Visible = True
    End Sub

    Private Sub SetupProjectDetailGrid()
        dgvCardDetail.Columns.Add(New System.Windows.Forms.DataGridViewTextBoxColumn() With {.DataPropertyName = "ProjectName", .HeaderText = "Tên Dự Án", .Width = 220})
        dgvCardDetail.Columns.Add(New System.Windows.Forms.DataGridViewTextBoxColumn() With {.DataPropertyName = "Status", .HeaderText = "Trạng Thái", .Width = 110})
        dgvCardDetail.Columns.Add(New System.Windows.Forms.DataGridViewTextBoxColumn() With {.DataPropertyName = "StartDate", .HeaderText = "Bắt Đầu", .Width = 90, .DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {.Format = "dd/MM/yyyy"}})
        dgvCardDetail.Columns.Add(New System.Windows.Forms.DataGridViewTextBoxColumn() With {.DataPropertyName = "EndDate", .HeaderText = "Kết Thúc", .Width = 90, .DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {.Format = "dd/MM/yyyy"}})
        dgvCardDetail.Columns.Add(New System.Windows.Forms.DataGridViewTextBoxColumn() With {.DataPropertyName = "TaskCount", .HeaderText = "Số Task", .Width = 70, .DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter}})
        dgvCardDetail.Columns.Add(New System.Windows.Forms.DataGridViewTextBoxColumn() With {.DataPropertyName = "Description", .HeaderText = "Mô Tả"})
    End Sub

    ''' <summary>Highlight thẻ đang chọn (viền trắng), bỏ highlight các thẻ khác</summary>
    Private Sub HighlightActiveCard(activeTag As String)
        Dim allCards As System.Windows.Forms.Panel() = If(_adminCards, _empCards)
        If allCards Is Nothing Then Return
        For Each card In allCards
            If card Is Nothing Then Continue For
            If card.Tag?.ToString() = activeTag Then
                card.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
                ' Làm sáng màu nền một chút
                card.Padding = New System.Windows.Forms.Padding(2)
            Else
                card.BorderStyle = System.Windows.Forms.BorderStyle.None
                card.Padding = New System.Windows.Forms.Padding(0)
            End If
        Next
    End Sub

    ''' <summary>Bỏ toàn bộ highlight</summary>
    Private Sub ClearCardHighlight()
        Dim allCards As System.Windows.Forms.Panel() = If(_adminCards, _empCards)
        If allCards Is Nothing Then Return
        For Each card In allCards
            If card IsNot Nothing Then
                card.BorderStyle = System.Windows.Forms.BorderStyle.None
                card.Padding = New System.Windows.Forms.Padding(0)
            End If
        Next
    End Sub

    Private Sub SetupTaskDetailGrid()
        dgvCardDetail.Columns.Add(New System.Windows.Forms.DataGridViewTextBoxColumn() With {.DataPropertyName = "Title", .HeaderText = "Tiêu Đề", .Width = 250})
        dgvCardDetail.Columns.Add(New System.Windows.Forms.DataGridViewTextBoxColumn() With {.DataPropertyName = "Progress", .HeaderText = "Tiến Độ (%)", .Width = 90, .DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter}})
        dgvCardDetail.Columns.Add(New System.Windows.Forms.DataGridViewTextBoxColumn() With {.DataPropertyName = "Priority", .HeaderText = "Ưu Tiên", .Width = 80})
        dgvCardDetail.Columns.Add(New System.Windows.Forms.DataGridViewTextBoxColumn() With {.DataPropertyName = "DueDate", .HeaderText = "Deadline", .Width = 100, .DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {.Format = "dd/MM/yyyy"}})
        dgvCardDetail.Columns.Add(New System.Windows.Forms.DataGridViewTextBoxColumn() With {.DataPropertyName = "Description", .HeaderText = "Mô Tả"})
    End Sub

    ' ─── Load số liệu thống kê ───
    Private Sub LoadQuickStats(user As User)
        Try
            Dim role As String = If(user.RoleId, "").ToLower()
            If role = "admin" OrElse role = "manager" Then
                pnlAdminStats.Visible = True
                pnlEmployeeStats.Visible = False

                _allProjects = _projectService.GetAllProjects()
                If _allProjects Is Nothing Then _allProjects = New List(Of Project)()

                Dim total As Integer = _allProjects.Count
                Dim active As Integer = _allProjects.Where(Function(p) (p.Status = "Active" OrElse p.Status = "Đang thực hiện") AndAlso Not (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count()
                Dim completed As Integer = _allProjects.Where(Function(p) p.Status = "Completed" OrElse p.Status = "Hoàn thành" OrElse (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count()
                Dim overdue As Integer = _allProjects.Where(Function(p) p.EndDate.HasValue AndAlso p.EndDate.Value < DateTime.Now AndAlso p.Status <> "Completed" AndAlso p.Status <> "Hoàn thành" AndAlso Not (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count()
                Dim planning As Integer = _allProjects.Where(Function(p) p.Status = "Planning" OrElse p.Status = "Lập kế hoạch").Count()

                lblStatTotalCount.Text = total.ToString()
                lblStatActiveCount.Text = active.ToString()
                lblStatCompletedCount.Text = completed.ToString()
                lblStatOverdueCount.Text = overdue.ToString()
                lblStatPlanningCount.Text = planning.ToString()
            Else
                pnlAdminStats.Visible = False
                pnlEmployeeStats.Visible = True

                _myTasks = _taskService.GetMyTasks(user.UserId)
                If _myTasks Is Nothing Then _myTasks = New List(Of Task)()

                Dim total As Integer = _myTasks.Count
                Dim inProgress As Integer = _myTasks.Where(Function(t) t.Progress >= 1 AndAlso t.Progress <= 89).Count()
                Dim done As Integer = _myTasks.Where(Function(t) t.Progress = 100).Count()
                Dim nearDeadline As Integer = _myTasks.Where(Function(t) _
                    t.DueDate.HasValue AndAlso _
                    t.DueDate.Value <= DateTime.Now.AddDays(3) AndAlso _
                    t.DueDate.Value >= DateTime.Now.AddDays(-1) AndAlso _
                    t.Progress < 100).Count()

                lblEmpTotalCount.Text = total.ToString()
                lblEmpInProgressCount.Text = inProgress.ToString()
                lblEmpDoneCount.Text = done.ToString()
                lblEmpDeadlineCount.Text = nearDeadline.ToString()
            End If
        Catch ex As Exception
            ' Silent fail for stats
        End Try
    End Sub

    ' ─── Navigation ───
    Private Sub btnGoTasks_Click(sender As Object, e As EventArgs) Handles btnGoTasks.Click
        Dim f As New frmTaskManagement()
        f.Show() : Me.Hide()
    End Sub

    Private Sub btnGoApproval_Click(sender As Object, e As EventArgs) Handles btnGoApproval.Click
        Dim f As New frmTaskApproval()
        f.Show() : Me.Hide()
    End Sub

    Private Sub btnGoOpenTasks_Click(sender As Object, e As EventArgs) Handles btnGoOpenTasks.Click
        Dim f As New frmOpenTasks()
        f.Show() : Me.Hide()
    End Sub

    Private Sub btnGoMyTasks_Click(sender As Object, e As EventArgs) Handles btnGoMyTasks.Click
        Dim f As New frmMyTasks()
        f.Show() : Me.Hide()
    End Sub

    Private Sub btnGoMyTeams_Click(sender As Object, e As EventArgs) Handles btnGoMyTeams.Click
        Dim f As New frmMyTeams()
        f.Show() : Me.Hide()
    End Sub

    Private Sub btnGoProjects_Click(sender As Object, e As EventArgs) Handles btnGoProjects.Click
        Dim f As New frmProjects()
        f.Show() : Me.Hide()
    End Sub

    Private Sub btnGoTeams_Click(sender As Object, e As EventArgs) Handles btnGoTeams.Click
        Dim f As New frmTeams()
        f.Show() : Me.Hide()
    End Sub


    Private Sub btnGoReport_Click(sender As Object, e As EventArgs) Handles btnGoReport.Click
        Dim f As New frmReport()
        f.Show() : Me.Hide()
    End Sub


    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Dim confirm As DialogResult = MessageBox.Show(
            "Bạn có chắc muốn đăng xuất không?",
            "Xác nhận",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)
        If confirm = DialogResult.Yes Then
            _isLoggingOut = True
            SessionManager.Logout()
            OpenLoginForm()
        End If
    End Sub

    Private Sub OpenLoginForm()
        Dim loginForm As New frmLogin()
        loginForm.Show()
        Me.Close()
    End Sub

    Private Sub frmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If Not _isLoggingOut Then
            Application.Exit()
        End If
    End Sub

End Class
