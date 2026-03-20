Imports System.Drawing
Public Class frmTaskManagement
    Inherits System.Windows.Forms.Form
    Private _isNavigatingBack As Boolean = False

    Private ReadOnly _taskService As ITaskService
    Private ReadOnly _userRepo As IUserRepository
    Private ReadOnly _projectService As IProjectService
    Private ReadOnly _teamService As ITeamService
    Private _selectedTaskId As Integer = -1
    Private _allTasks As List(Of Task)   ' Cache toàn bộ task để filter client-side

    ' Lookup dictionaries để tra tên từ ID
    Private _userNames As New Dictionary(Of Integer, String)()
    Private _projectNames As New Dictionary(Of Integer, String)()
    Private _teamNames As New Dictionary(Of Integer, String)()

    ' Pagination
    Private Const PageSize As Integer = 7
    Private _currentPage As Integer = 1
    Private _totalPages As Integer = 1
    Private _filteredTasks As List(Of Task) = New List(Of Task)()

    Public Sub New()
        InitializeComponent()
        _taskService = New TaskService()
        _userRepo = New UserRepository()
        _projectService = New ProjectService()
        _teamService = New TeamService()
    End Sub

    Private Sub frmTaskManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupGrid()
        LoadLookupDictionaries()   ' Load lookup trước để có tên khi bind grid
        LoadUsersToCombo()
        LoadProjectsToCombo()
        LoadTeamsToCombo()
        LoadPriorityCombo()
        LoadFilterCombo()
        LoadTasks()
        ClearForm()

        ' Phân quyền: Chỉ Admin và Manager mới có quyền tạo/sửa/xóa và chọn dự án/người phụ trách
        Dim currentUser = SessionManager.CurrentUser
        If currentUser IsNot Nothing Then
            Dim role As String = If(currentUser.RoleId, "").ToLower()
            Dim isLeader As Boolean = _teamService.IsUserTeamLeader(currentUser.UserId)
            Dim hasFullAccess As Boolean = (role = "admin" OrElse role = "manager" OrElse isLeader)

            btnAdd.Enabled = hasFullAccess
            btnUpdate.Enabled = hasFullAccess
            btnDelete.Enabled = hasFullAccess

            ' Hạn chế chọn dự án và người phụ trách nếu không có quyền
            cboProject.Enabled = hasFullAccess
            cboAssignedUser.Enabled = hasFullAccess
            cboTeam.Enabled = hasFullAccess

            If Not hasFullAccess Then
                lblTaskCount.Text &= " (Chế độ chỉ xem)"
            End If
        End If
    End Sub

    ' ──────────────────────────────────────────────
    '   SETUP
    ' ──────────────────────────────────────────────
    Private Sub SetupGrid()
        dgvTasks.AutoGenerateColumns = False
        dgvTasks.Columns.Clear()
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Title", .HeaderText = "Tiêu đề", .Width = 180})
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "ProgressDisplay", .HeaderText = "Tiến độ", .Width = 100})
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Priority", .HeaderText = "Ưu tiên", .Width = 80})
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "AssignedToUserName", .HeaderText = "Giao cho", .Width = 110})
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "ProjectName", .HeaderText = "Dự án", .Width = 120})
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "TeamName", .HeaderText = "Team", .Width = 100})
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {
            .DataPropertyName = "DueDate",
            .HeaderText = "Deadline",
            .Width = 110,
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "dd/MM/yyyy"}
        })
        dgvTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvTasks.ReadOnly = True
        dgvTasks.AllowUserToAddRows = False
    End Sub

    ''' <summary>Load lookup dictionaries: Id → Name cho User, Project, Team</summary>
    Private Sub LoadLookupDictionaries()
        Try
            _userNames.Clear()
            For Each u In _userRepo.GetAll()
                _userNames(u.UserId) = u.UserName
            Next
        Catch ex As BusinessException
            ' Bỏ qua lỗi lookup — grid vẫn hiển thị ID nếu không tải được tên
        End Try

        Try
            _projectNames.Clear()
            For Each p In _projectService.GetAllProjects()
                _projectNames(p.ProjectId) = p.ProjectName
            Next
        Catch ex As BusinessException
            ' Bỏ qua lỗi lookup — grid vẫn hiển thị ID nếu không tải được tên
        End Try

        Try
            _teamNames.Clear()
            For Each t In _teamService.GetAllTeams()
                _teamNames(t.TeamId) = t.TeamName
            Next
        Catch ex As BusinessException
            ' Bỏ qua lỗi lookup — grid vẫn hiển thị ID nếu không tải được tên
        End Try
    End Sub

    ''' <summary>Chuyển List(Of Task) thành List(Of TaskViewItem) có tên thay vì ID</summary>
    Private Function BuildViewItems(tasks As List(Of Task)) As List(Of TaskViewItem)
        Dim result As New List(Of TaskViewItem)()
        For Each t In tasks
            Dim userName As String = "-- Chưa giao --"
            If t.AssignedToUserId.HasValue Then
                If Not _userNames.TryGetValue(t.AssignedToUserId.Value, userName) Then
                    userName = $"UserId {t.AssignedToUserId.Value}"
                End If
            End If

            Dim projectName As String = "-- Không có --"
            If t.ProjectId.HasValue Then
                If Not _projectNames.TryGetValue(t.ProjectId.Value, projectName) Then
                    projectName = $"ProjectId {t.ProjectId.Value}"
                End If
            End If

            Dim teamName As String = "-- Không có --"
            If t.TeamId.HasValue Then
                If Not _teamNames.TryGetValue(t.TeamId.Value, teamName) Then
                    teamName = $"TeamId {t.TeamId.Value}"
                End If
            End If

            result.Add(New TaskViewItem() With {
                .TaskId = t.TaskId,
                .Title = t.Title,
                .Progress = t.Progress,
                .Priority = t.Priority,
                .AssignedToUserName = userName,
                .ProjectName = projectName,
                .TeamName = teamName,
                .DueDate = t.DueDate,
                .IsApproved = t.IsApproved
            })
        Next
        Return result
    End Function

    ''' <summary>Load danh sách User từ DB vào ComboBox</summary>
    Private Sub LoadUsersToCombo()
        Try
            Dim users As List(Of User) = _userRepo.GetAll()
            cboAssignedUser.DataSource = Nothing
            cboAssignedUser.DataSource = users
            cboAssignedUser.DisplayMember = "UserName"   ' Hiển thị tên
            cboAssignedUser.ValueMember = "UserId"       ' Lưu ID
        Catch ex As BusinessException
            MessageBox.Show("Không thể tải danh sách nhân viên: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub LoadProjectsToCombo()
        Try
            Dim projects As List(Of Project) = _projectService.GetAllProjects()
            Dim listForCombo As New List(Of Object)
            listForCombo.Add(New With {.ProjectId = 0, .ProjectName = "-- Không thuộc dự án --"})
            For Each p In projects
                listForCombo.Add(New With {.ProjectId = p.ProjectId, .ProjectName = p.ProjectName})
            Next
            cboProject.DataSource = listForCombo
            cboProject.DisplayMember = "ProjectName"
            cboProject.ValueMember = "ProjectId"
        Catch ex As BusinessException
            MessageBox.Show("Không thể tải danh sách dự án: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub LoadTeamsToCombo()
        Try
            Dim teams As List(Of TeamDto) = _teamService.GetAllTeams()
            Dim listForCombo As New List(Of Object)
            listForCombo.Add(New With {.TeamId = 0, .TeamName = "-- Không giao cho nhóm --"})
            For Each t In teams
                listForCombo.Add(New With {.TeamId = t.TeamId, .TeamName = t.TeamName})
            Next
            cboTeam.DataSource = listForCombo
            cboTeam.DisplayMember = "TeamName"
            cboTeam.ValueMember = "TeamId"
        Catch ex As BusinessException
            MessageBox.Show("Không thể tải danh sách Nhóm: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub LoadPriorityCombo()
        cboPriority.Items.Clear()
        cboPriority.Items.AddRange({"Cao", "Trung bình", "Thấp"})
        cboPriority.SelectedIndex = 1

        ' Setup NumericUpDown cho Progress
        nudProgress.Minimum = 0
        nudProgress.Maximum = 100
        nudProgress.Increment = 10
        nudProgress.Value = 0
    End Sub

    Private Sub LoadTasks()
        Try
            _allTasks = _taskService.GetAllTasks()
            ApplyFilter()   ' Áp dụng filter hiện tại lên danh sách
        Catch ex As BusinessException
            MessageBox.Show("Lỗi tải danh sách task: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>Khởi tạo ComboBox lọc theo tiến độ</summary>
    Private Sub LoadFilterCombo()
        cboFilterStatus.Items.Clear()
        cboFilterStatus.Items.Add("Tất cả")
        cboFilterStatus.Items.Add("Chưa bắt đầu (0%)")
        cboFilterStatus.Items.Add("Đang thực hiện (1-89%)")
        cboFilterStatus.Items.Add("Chờ duyệt (90%)")
        cboFilterStatus.Items.Add("Đã duyệt (100%)")
        cboFilterStatus.Items.Add("Chưa duyệt (100%)")
        cboFilterStatus.SelectedIndex = 0
    End Sub

    ''' <summary>Lọc danh sách task theo giá trị ComboBox, bind lại DataGridView với phân trang</summary>
    Private Sub ApplyFilter()
        If _allTasks Is Nothing Then Return

        ' 1. Filter theo progress range
        If cboFilterStatus.SelectedIndex <= 0 OrElse cboFilterStatus.SelectedItem?.ToString() = "Tất cả" Then
            _filteredTasks = _allTasks
        Else
            Dim selected As String = cboFilterStatus.SelectedItem.ToString()
            Select Case selected
                Case "Chưa bắt đầu (0%)"
                    _filteredTasks = _allTasks.Where(Function(t) t.Progress = 0).ToList()
                Case "Đang thực hiện (1-89%)"
                    _filteredTasks = _allTasks.Where(Function(t) t.Progress >= 1 AndAlso t.Progress <= 89).ToList()
                Case "Chờ duyệt (90%)"
                    _filteredTasks = _allTasks.Where(Function(t) t.Progress = 90).ToList()
                Case "Đã duyệt (100%)"
                    _filteredTasks = _allTasks.Where(Function(t) t.Progress = 100 AndAlso t.IsApproved).ToList()
                Case "Chưa duyệt (100%)"
                    _filteredTasks = _allTasks.Where(Function(t) t.Progress = 100 AndAlso Not t.IsApproved).ToList()
                Case Else
                    _filteredTasks = _allTasks
            End Select
        End If

        ' 2. Calculate pagination
        _totalPages = Math.Max(1, CInt(Math.Ceiling(_filteredTasks.Count / PageSize)))
        If _currentPage > _totalPages Then _currentPage = _totalPages
        If _currentPage < 1 Then _currentPage = 1

        ' 3. Slice data
        Dim pagedData = _filteredTasks.Skip((_currentPage - 1) * PageSize).Take(PageSize).ToList()

        ' 4. Bind
        dgvTasks.DataSource = Nothing
        dgvTasks.DataSource = BuildViewItems(pagedData)

        ' 5. UI Updates
        lblPageInfo.Text = $"Trang {_currentPage} / {_totalPages}"
        btnPrev.Enabled = (_currentPage > 1)
        btnNext.Enabled = (_currentPage < _totalPages)

        lblTaskCount.Text = $"Hiển thị: {pagedData.Count} / {_filteredTasks.Count} (Tổng {_allTasks.Count})"
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        If _currentPage > 1 Then
            _currentPage -= 1
            ApplyFilter()
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If _currentPage < _totalPages Then
            _currentPage += 1
            ApplyFilter()
        End If
    End Sub

    Private Sub cboFilterStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFilterStatus.SelectedIndexChanged
        _currentPage = 1 ' Reset về trang 1 khi lọc
        ApplyFilter()
    End Sub

    ' ──────────────────────────────────────────────
    '   GRID - Chọn hàng → điền form
    ' ──────────────────────────────────────────────
    Private Sub dgvTasks_SelectionChanged(sender As Object, e As EventArgs) Handles dgvTasks.SelectionChanged
        If dgvTasks.SelectedRows.Count = 0 Then Return
        ' Grid bind TaskViewItem nên phải cast đúng kiểu, sau đó tìm Task thật từ _allTasks
        Dim viewItem = TryCast(dgvTasks.SelectedRows(0).DataBoundItem, TaskViewItem)
        If viewItem Is Nothing Then Return
        Dim t = _filteredTasks?.FirstOrDefault(Function(x) x.TaskId = viewItem.TaskId)
        If t Is Nothing Then Return

        _selectedTaskId = t.TaskId
        txtTitle.Text = t.Title
        txtDescription.Text = t.Description
        cboPriority.SelectedItem = t.Priority
        nudProgress.Value = Math.Min(Math.Max(t.Progress, nudProgress.Minimum), nudProgress.Maximum)
        ' Chọn đúng user trong ComboBox theo AssignedToUserId
        If t.AssignedToUserId.HasValue Then
            cboAssignedUser.SelectedValue = t.AssignedToUserId.Value
        Else
            cboAssignedUser.SelectedIndex = 0
        End If

        If t.ProjectId.HasValue Then
            cboProject.SelectedValue = t.ProjectId.Value
        Else
            cboProject.SelectedValue = 0
        End If
        If t.TeamId.HasValue Then
            cboTeam.SelectedValue = t.TeamId.Value
        Else
            cboTeam.SelectedValue = 0
        End If
        If t.DueDate.HasValue Then
            dtpDueDate.Value = t.DueDate.Value
        End If

        ' Hiển thị nút Duyệt nếu task 100% và chưa duyệt (cho Admin/Manager)
        Dim isMgOrAdmin = (SessionManager.CurrentUser.RoleId = "Admin" OrElse SessionManager.CurrentUser.RoleId = "Manager")
        btnApprove.Visible = isMgOrAdmin AndAlso (t.Progress = 100 AndAlso Not t.IsApproved)
    End Sub

    ' ──────────────────────────────────────────────
    '   BUTTONS
    ' ──────────────────────────────────────────────
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim dto As New TaskDto() With {
            .Title = txtTitle.Text.Trim(),
            .Description = txtDescription.Text.Trim(),
            .AssignedToUserId = GetSelectedUserId(),
            .Progress = CInt(nudProgress.Value),
            .Priority = cboPriority.SelectedItem?.ToString(),
            .DueDate = dtpDueDate.Value,
            .ProjectId = GetSelectedProjectId(),
            .TeamId = GetSelectedTeamId()
        }
        Dim result = _taskService.CreateTask(dto, SessionManager.CurrentUser.UserId)
        If result.Success Then
            MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadTasks()
            ClearForm()
        Else
            MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If _selectedTaskId < 0 Then
            MessageBox.Show("Vui lòng chọn một công việc trong bảng.", "Chưa chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim dto As New TaskDto() With {
            .TaskId = _selectedTaskId,
            .Title = txtTitle.Text.Trim(),
            .Description = txtDescription.Text.Trim(),
            .AssignedToUserId = GetSelectedUserId(),
            .Progress = CInt(nudProgress.Value),
            .Priority = cboPriority.SelectedItem?.ToString(),
            .DueDate = dtpDueDate.Value,
            .ProjectId = GetSelectedProjectId(),
            .TeamId = GetSelectedTeamId()
        }
        Dim result = _taskService.UpdateTask(dto)
        If result.Success Then
            MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadTasks()
            ClearForm()
        Else
            MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If _selectedTaskId < 0 Then
            MessageBox.Show("Vui lòng chọn một công việc trong bảng.", "Chưa chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim confirm As DialogResult = MessageBox.Show(
            "Bạn có chắc chắn muốn XÓA VĨNH VIỄN công việc này khỏi hệ thống?", "Xác nhận xóa cứng",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If confirm = DialogResult.Yes Then
            Dim result = _taskService.DeleteTask(_selectedTaskId)
            MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadTasks()
            ClearForm()
        End If
    End Sub


    ''' <summary>Button Duyệt Task</summary>
    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        If _selectedTaskId < 0 Then
            MessageBox.Show("Vui lòng chọn một công việc 100% để duyệt.", "Chưa chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim t = _allTasks.FirstOrDefault(Function(x) x.TaskId = _selectedTaskId)
        If t Is Nothing OrElse t.Progress < 100 Then
            MessageBox.Show("Chỉ có thể duyệt công việc đã đạt 100% tiến độ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim result = _taskService.ApproveTask(_selectedTaskId)
        If result.Success Then
            MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadTasks() : ClearForm()
        Else
            MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        _isNavigatingBack = True
        Dim mainForm As New frmMain()
        mainForm.Show()
        Me.Close()
    End Sub

    Private Sub frmTaskManagement_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If Not _isNavigatingBack Then
            Application.Exit()
        End If
    End Sub

    ' ──────────────────────────────────────────────
    '   HELPER
    ' ──────────────────────────────────────────────
    ''' <summary>Lấy UserId từ ComboBox đang chọn</summary>
    Private Function GetSelectedUserId() As Integer
        If cboAssignedUser.SelectedValue Is Nothing Then Return 0
        Return Convert.ToInt32(cboAssignedUser.SelectedValue)
    End Function

    Private Function GetSelectedProjectId() As Integer?
        If cboProject.SelectedValue Is Nothing Then Return Nothing
        Dim val As Integer = Convert.ToInt32(cboProject.SelectedValue)
        If val = 0 Then Return Nothing
        Return val
    End Function

    Private Function GetSelectedTeamId() As Integer?
        If cboTeam.SelectedValue Is Nothing Then Return Nothing
        Dim val As Integer = Convert.ToInt32(cboTeam.SelectedValue)
        If val = 0 Then Return Nothing
        Return val
    End Function

    Private Sub ClearForm()
        _selectedTaskId = -1
        txtTitle.Text = ""
        txtDescription.Text = ""
        If cboAssignedUser.Items.Count > 0 Then cboAssignedUser.SelectedIndex = 0
        If cboProject.Items.Count > 0 Then cboProject.SelectedIndex = 0
        If cboTeam.Items.Count > 0 Then cboTeam.SelectedIndex = 0
        cboPriority.SelectedIndex = 1
        nudProgress.Value = 0
        dtpDueDate.Value = DateTime.Now.AddDays(7)
        btnApprove.Visible = False
        dgvTasks.ClearSelection()
    End Sub

    ' ──────────────────────────────────────────────
    '   XUẤT THỐNG KÊ CSV
    ' ──────────────────────────────────────────────
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        ExportStatisticsToCSV()
    End Sub

    Private Sub ExportStatisticsToCSV()
        Try
            Dim tasks As List(Of Task) = _taskService.GetAllTasks()
            If tasks Is Nothing OrElse tasks.Count = 0 Then
                MessageBox.Show("Không có task nào để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Using sfd As New SaveFileDialog()
                sfd.Title = "Lưu file thống kê"
                sfd.Filter = "CSV Files (*.csv)|*.csv"
                sfd.FileName = $"ThongKe_Task_{DateTime.Now.ToString("yyyyMMdd_HHmm")}.csv"
                If sfd.ShowDialog() <> DialogResult.OK Then Return

                Using writer As New System.IO.StreamWriter(sfd.FileName, False, System.Text.Encoding.UTF8)
                    ' BOM UTF-8 để Excel mở đúng tiếng Việt
                    writer.Write(Chr(239) & Chr(187) & Chr(191))

                    ' ── PHẦN 1: THỐNG KÊ THEO TIẾN ĐỘ ──
                    writer.WriteLine("=== THỐNG KÊ CÔNG VIỆC THEO TIẾN ĐỘ ===")
                    writer.WriteLine($"Thời gian xuất:,{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}")
                    writer.WriteLine($"Tổng số task:,{tasks.Count}")
                    writer.WriteLine()
                    writer.WriteLine("Tiến độ,Số lượng,Tỷ lệ (%)")

                    Dim progressRanges = {
                        ("Chưa bắt đầu (0%)", Function(t As Task) t.Progress = 0),
                        ("Đang thực hiện (1-89%)", Function(t As Task) t.Progress >= 1 AndAlso t.Progress <= 89),
                        ("Chờ duyệt (90%)", Function(t As Task) t.Progress = 90),
                        ("Hoàn thành (100%)", Function(t As Task) t.Progress = 100)
                    }

                    For Each rng In progressRanges
                        Dim cnt As Integer = tasks.Where(rng.Item2).Count()
                        Dim pct As Double = If(tasks.Count > 0, Math.Round(cnt / tasks.Count * 100, 1), 0)
                        writer.WriteLine($"{rng.Item1},{cnt},{pct}%")
                    Next

                    writer.WriteLine()

                    ' ── PHẦN 2: THỐNG KÊ THEO MỨC ƯU TIÊN ──
                    writer.WriteLine("=== THỐNG KÊ THEO MỨC ƯU TIÊN ===")
                    writer.WriteLine("Mức ưu tiên,Số lượng,Tỷ lệ (%)")
                    For Each pri In {"Cao", "Trung bình", "Thấp"}
                        Dim cnt As Integer = tasks.Where(Function(t) t.Priority = pri).Count()
                        Dim pct As Double = If(tasks.Count > 0, Math.Round(cnt / tasks.Count * 100, 1), 0)
                        writer.WriteLine($"{pri},{cnt},{pct}%")
                    Next

                    writer.WriteLine()

                    ' ── PHẦN 3: DANH SÁCH CHI TIẾT ──
                    writer.WriteLine("=== DANH SÁCH CHI TIẾT CÔNG VIỆC ===")
                    writer.WriteLine("TaskId,Tiêu đề,Mô tả,Giao cho (UserId),Tạo bởi (UserId),Tiến độ (%),Ưu tiên,Ngày tạo,Deadline")
                    For Each t As Task In tasks
                        Dim due As String = If(t.DueDate.HasValue, t.DueDate.Value.ToString("dd/MM/yyyy"), "")
                        Dim statusStr = t.ProgressDisplay
                        writer.WriteLine($"{t.TaskId},{EscapeCsv(t.Title)},{EscapeCsv(If(t.Description, ""))},{t.AssignedToUserId},{t.CreatedByUserId},{statusStr},{t.Priority},{t.CreatedAt.ToString("dd/MM/yyyy")},{due}")
                    Next
                End Using

                MessageBox.Show($"Xuất thành công!{Environment.NewLine}File: {sfd.FileName}",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)

                If MessageBox.Show("Bạn có muốn mở file vừa xuất không?", "Mở file",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    System.Diagnostics.Process.Start(sfd.FileName)
                End If
            End Using

        Catch ex As Exception
            ' Bắt Exception chung vì lỗi có thể từ IO (ghi file) hoặc DB
            MessageBox.Show("Lỗi khi xuất file: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>Escape CSV: bọc ngoặc kép nếu có dấu phẩy hoặc xuống dòng</summary>
    Private Function EscapeCsv(value As String) As String
        If String.IsNullOrEmpty(value) Then Return ""
        If value.Contains(",") OrElse value.Contains("""") OrElse value.Contains(Environment.NewLine) Then
            Return """" & value.Replace("""", """""") & """"
        End If
        Return value
    End Function

    Private Sub dgvTasks_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvTasks.CellFormatting
        If e.RowIndex < 0 Then Return
        
        Dim grid = DirectCast(sender, DataGridView)
        Dim row = grid.Rows(e.RowIndex)
        Dim dataBoundItem = row.DataBoundItem

        If TypeOf dataBoundItem Is TaskViewItem Then
            Dim t = DirectCast(dataBoundItem, TaskViewItem)
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
        End If
    End Sub

End Class

''' <summary>
''' ViewModel dùng để hiển thị Task trong DataGridView với tên thay vì ID.
''' Không lưu DB, chỉ phục vụ tầng hiển thị (GUI).
''' </summary>
Public Class TaskViewItem
    Public Property TaskId As Integer
    Public Property Title As String
    Public Property Progress As Integer
    Public Property Priority As String
    Public Property AssignedToUserName As String   ' Tên người được giao (thay vì UserId)
    Public Property ProjectName As String           ' Tên dự án (thay vì ProjectId)
    Public Property TeamName As String             ' Tên team (thay vì TeamId)
    Public Property DueDate As DateTime?            ' Deadline
    Public Property IsApproved As Boolean

    ''' <summary>Hiển thị tiến độ dạng "50%"</summary>
    Public ReadOnly Property ProgressDisplay As String
        Get
            If Progress = 100 Then
                ' Chúng ta chưa có IsApproved trong TaskViewItem, tạm thời lấy logic từ model nếu có thể
                ' Nhưng để an toàn, ta nên map IsApproved vào TaskViewItem
                Return If(IsApproved, "Đã duyệt", "Chưa duyệt")
            End If
            Return $"{Progress}%"
        End Get
    End Property
End Class
