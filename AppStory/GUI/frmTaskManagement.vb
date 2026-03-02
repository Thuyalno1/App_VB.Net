Public Class frmTaskManagement
    Inherits System.Windows.Forms.Form

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
        LoadPriorityAndStatus()
        LoadFilterCombo()
        LoadTasks()
        ClearForm()
    End Sub

    ' ──────────────────────────────────────────────
    '   SETUP
    ' ──────────────────────────────────────────────
    Private Sub SetupGrid()
        dgvTasks.AutoGenerateColumns = False
        dgvTasks.Columns.Clear()
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "TaskId", .HeaderText = "ID", .Width = 45})
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Title", .HeaderText = "Tiêu đề", .Width = 180})
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Status", .HeaderText = "Trạng thái", .Width = 100})
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
        Catch : End Try

        Try
            _projectNames.Clear()
            For Each p In _projectService.GetAllProjects()
                _projectNames(p.ProjectId) = p.ProjectName
            Next
        Catch : End Try

        Try
            _teamNames.Clear()
            For Each t In _teamService.GetAllTeams()
                _teamNames(t.TeamId) = t.TeamName
            Next
        Catch : End Try
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
                .Status = t.Status,
                .Priority = t.Priority,
                .AssignedToUserName = userName,
                .ProjectName = projectName,
                .TeamName = teamName,
                .DueDate = t.DueDate
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
        Catch ex As Exception
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
        Catch ex As Exception
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
        Catch ex As Exception
            MessageBox.Show("Không thể tải danh sách Nhóm: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub LoadPriorityAndStatus()
        cboPriority.Items.Clear()
        cboPriority.Items.AddRange({"High", "Medium", "Low"})
        cboPriority.SelectedIndex = 1

        cboStatus.Items.Clear()
        cboStatus.Items.AddRange({"Pending", "In Progress", "Completed"})
        cboStatus.SelectedIndex = 0
    End Sub

    Private Sub LoadTasks()
        Try
            _allTasks = _taskService.GetAllTasks()
            ApplyFilter()   ' Áp dụng filter hiện tại lên danh sách
        Catch ex As Exception
            MessageBox.Show("Lỗi tải danh sách task: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>Khởi tạo ComboBox lọc theo trạng thái</summary>
    Private Sub LoadFilterCombo()
        cboFilterStatus.Items.Clear()
        cboFilterStatus.Items.Add("Tất cả")
        cboFilterStatus.Items.Add("Pending")
        cboFilterStatus.Items.Add("In Progress")
        cboFilterStatus.Items.Add("Completed")
        cboFilterStatus.SelectedIndex = 0
    End Sub

    ''' <summary>Lọc danh sách task theo giá trị ComboBox, bind lại DataGridView</summary>
    Private Sub ApplyFilter()
        If _allTasks Is Nothing Then Return
        Dim filtered As List(Of Task)
        If cboFilterStatus.SelectedIndex <= 0 OrElse cboFilterStatus.SelectedItem?.ToString() = "Tất cả" Then
            filtered = _allTasks
        Else
            Dim selected As String = cboFilterStatus.SelectedItem.ToString()
            filtered = _allTasks.Where(Function(t) t.Status = selected).ToList()
        End If
        ' Chuyển sang TaskViewItem để hiển thị tên thay vì ID
        dgvTasks.DataSource = Nothing
        dgvTasks.DataSource = BuildViewItems(filtered)
        lblTaskCount.Text = $"Hiển thị: {filtered.Count} / {_allTasks.Count} task"
    End Sub

    Private Sub cboFilterStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFilterStatus.SelectedIndexChanged
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
        Dim t = _allTasks?.FirstOrDefault(Function(x) x.TaskId = viewItem.TaskId)
        If t Is Nothing Then Return

        _selectedTaskId = t.TaskId
        txtTitle.Text = t.Title
        txtDescription.Text = t.Description
        cboPriority.SelectedItem = t.Priority
        cboStatus.SelectedItem = t.Status
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
    End Sub

    ' ──────────────────────────────────────────────
    '   BUTTONS
    ' ──────────────────────────────────────────────
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim dto As New TaskDto() With {
            .Title = txtTitle.Text.Trim(),
            .Description = txtDescription.Text.Trim(),
            .AssignedToUserId = GetSelectedUserId(),
            .Status = cboStatus.SelectedItem?.ToString(),
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
            .Status = cboStatus.SelectedItem?.ToString(),
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
            "Xác nhận xóa công việc này? (Soft Delete)", "Xác nhận",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm = DialogResult.Yes Then
            Dim result = _taskService.DeleteTask(_selectedTaskId)
            MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadTasks()
            ClearForm()
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim mainForm As New frmMain()
        mainForm.Show()
        Me.Close()
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
        cboStatus.SelectedIndex = 0
        dtpDueDate.Value = DateTime.Now.AddDays(7)
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

                    ' ── PHẦN 1: THỐNG KÊ THEO TRẠNG THÁI ──
                    writer.WriteLine("=== THỐNG KÊ CÔNG VIỆC THEO TRẠNG THÁI ===")
                    writer.WriteLine($"Thời gian xuất:,{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}")
                    writer.WriteLine($"Tổng số task:,{tasks.Count}")
                    writer.WriteLine()
                    writer.WriteLine("Trạng thái,Số lượng,Tỷ lệ (%)")

                    Dim statusList = {"Pending", "In Progress", "Completed"}
                    For Each st In statusList
                        Dim cnt As Integer = tasks.Where(Function(t) t.Status = st).Count()
                        Dim pct As Double = Math.Round(cnt / tasks.Count * 100, 1)
                        writer.WriteLine($"{st},{cnt},{pct}%")
                    Next
                    Dim otherSt As Integer = tasks.Where(Function(t) Not statusList.Contains(t.Status)).Count()
                    If otherSt > 0 Then writer.WriteLine($"Khác,{otherSt},{Math.Round(otherSt / tasks.Count * 100, 1)}%")

                    writer.WriteLine()

                    ' ── PHẦN 2: THỐNG KÊ THEO MỨC ƯU TIÊN ──
                    writer.WriteLine("=== THỐNG KÊ THEO MỨC ƯU TIÊN ===")
                    writer.WriteLine("Mức ưu tiên,Số lượng,Tỷ lệ (%)")
                    For Each pri In {"High", "Medium", "Low"}
                        Dim cnt As Integer = tasks.Where(Function(t) t.Priority = pri).Count()
                        Dim pct As Double = Math.Round(cnt / tasks.Count * 100, 1)
                        writer.WriteLine($"{pri},{cnt},{pct}%")
                    Next

                    writer.WriteLine()

                    ' ── PHẦN 3: DANH SÁCH CHI TIẾT ──
                    writer.WriteLine("=== DANH SÁCH CHI TIẾT CÔNG VIỆC ===")
                    writer.WriteLine("TaskId,Tiêu đề,Mô tả,Giao cho (UserId),Tạo bởi (UserId),Trạng thái,Ưu tiên,Ngày tạo,Deadline")
                    For Each t As Task In tasks
                        Dim due As String = If(t.DueDate.HasValue, t.DueDate.Value.ToString("dd/MM/yyyy"), "")
                        writer.WriteLine($"{t.TaskId},{EscapeCsv(t.Title)},{EscapeCsv(If(t.Description, ""))},{t.AssignedToUserId},{t.CreatedByUserId},{t.Status},{t.Priority},{t.CreatedAt.ToString("dd/MM/yyyy")},{due}")
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
            MessageBox.Show("Lỗi khi xuất file: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>Escape CSV: bọc ngoặc kép nếu có dấu phẩy hoặc xuống dòng</summary>
    Private Function EscapeCsv(value As String) As String
        If String.IsNullOrEmpty(value) Then Return ""
        If value.Contains(",") OrElse value.Contains("""") OrElse value.Contains(vbNewLine) Then
            Return """" & value.Replace("""", """""") & """"
        End If
        Return value
    End Function

End Class

''' <summary>
''' ViewModel dùng để hiển thị Task trong DataGridView với tên thay vì ID.
''' Không lưu DB, chỉ phục vụ tầng hiển thị (GUI).
''' </summary>
Public Class TaskViewItem
    Public Property TaskId As Integer
    Public Property Title As String
    Public Property Status As String
    Public Property Priority As String
    Public Property AssignedToUserName As String   ' Tên người được giao (thay vì UserId)
    Public Property ProjectName As String           ' Tên dự án (thay vì ProjectId)
    Public Property TeamName As String             ' Tên team (thay vì TeamId)
    Public Property DueDate As DateTime?            ' Deadline
End Class
