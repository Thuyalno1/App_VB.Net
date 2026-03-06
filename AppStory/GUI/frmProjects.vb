Public Class frmProjects
    Inherits System.Windows.Forms.Form

    Private ReadOnly _projectService As IProjectService
    Private ReadOnly _teamService As ITeamService
    Private ReadOnly _userRepo As IUserRepository
    Private _selectedProjectId As Integer = -1
    Private _allProjects As List(Of Project)   ' Cache để filter tìm kiếm client-side

    Public Sub New()
        InitializeComponent()
        _projectService = New ProjectService()
        _teamService = New TeamService()
        _userRepo = New UserRepository()
    End Sub

    Private Sub frmProjects_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupGrid()
        LoadUsersToCombo()
        LoadTeamsToCombo()
        cboStatus.SelectedIndex = 0
        LoadProjects()
    End Sub

    Private Sub SetupGrid()
        dgvProjects.AutoGenerateColumns = False
        dgvProjects.Columns.Clear()
        dgvProjects.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "ProjectId", .HeaderText = "ID", .Width = 40})
        dgvProjects.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "ProjectName", .HeaderText = "Tên Dự Án", .Width = 150})
        dgvProjects.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Status", .HeaderText = "Trạng Thái", .Width = 80})
        dgvProjects.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "StartDate", .HeaderText = "Bắt đầu", .Width = 90, .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "dd/MM/yyyy"}})
        dgvProjects.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "EndDate", .HeaderText = "Kết thúc", .Width = 90, .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "dd/MM/yyyy"}})
    End Sub

    Private Sub LoadUsersToCombo()
        Dim users = _userRepo.GetAll()
        Dim listForCombo As New List(Of Object)
        listForCombo.Add(New With {.UserId = 0, .UserName = "-- Chưa chọn --"})
        For Each u In users
            listForCombo.Add(New With {.UserId = u.UserId, .UserName = u.UserName})
        Next
        cboManager.DataSource = listForCombo
        cboManager.DisplayMember = "UserName"
        cboManager.ValueMember = "UserId"
    End Sub

    Private Sub LoadTeamsToCombo()
        Dim teams = _teamService.GetAllTeams()
        Dim listForCombo As New List(Of Object)
        listForCombo.Add(New With {.TeamId = 0, .TeamName = "-- Có thể cho tất cả --"})
        For Each t In teams
            listForCombo.Add(New With {.TeamId = t.TeamId, .TeamName = t.TeamName})
        Next
        cboTeam.DataSource = listForCombo
        cboTeam.DisplayMember = "TeamName"
        cboTeam.ValueMember = "TeamId"
    End Sub

    Private Sub LoadProjects()
        Try
            _allProjects = _projectService.GetAllProjects()
            ApplySearch()
        Catch ex As BusinessException
            MessageBox.Show("Lỗi tải danh sách dự án: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>Lọc danh sách dự án theo từ khóa (client-side)</summary>
    Private Sub ApplySearch()
        If _allProjects Is Nothing Then Return

        Dim keyword As String = txtSearch.Text.Trim().ToLower()

        Dim filtered As List(Of Project)
        If String.IsNullOrEmpty(keyword) Then
            filtered = _allProjects
        Else
            filtered = _allProjects.Where(Function(p)
                Return (p.ProjectName IsNot Nothing AndAlso p.ProjectName.ToLower().Contains(keyword)) OrElse
                       (p.Description IsNot Nothing AndAlso p.Description.ToLower().Contains(keyword)) OrElse
                       (p.Status IsNot Nothing AndAlso p.Status.ToLower().Contains(keyword))
            End Function).ToList()
        End If

        dgvProjects.DataSource = Nothing
        dgvProjects.DataSource = filtered
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        ApplySearch()
    End Sub

    Private Sub dgvProjects_SelectionChanged(sender As Object, e As EventArgs) Handles dgvProjects.SelectionChanged
        If dgvProjects.SelectedRows.Count = 0 Then Return
        Dim p = CType(dgvProjects.SelectedRows(0).DataBoundItem, Project)
        If p Is Nothing Then Return

        _selectedProjectId = p.ProjectId
        txtProjectName.Text = p.ProjectName
        txtDescription.Text = p.Description
        
        If p.StartDate.HasValue Then dtpStartDate.Value = p.StartDate.Value
        If p.EndDate.HasValue Then dtpEndDate.Value = p.EndDate.Value
        
        cboStatus.SelectedItem = p.Status
        
        cboManager.SelectedValue = If(p.ManagerId.HasValue, p.ManagerId.Value, 0)
        cboTeam.SelectedValue = If(p.TeamId.HasValue, p.TeamId.Value, 0)
    End Sub

    Private Function GetSelectedComboboxId(cbo As ComboBox) As Integer?
        If cbo.SelectedValue Is Nothing Then Return Nothing
        Dim val As Integer = Convert.ToInt32(cbo.SelectedValue)
        If val = 0 Then Return Nothing
        Return val
    End Function

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim dto As New ProjectDto() With {
            .ProjectName = txtProjectName.Text.Trim(),
            .Description = txtDescription.Text.Trim(),
            .StartDate = dtpStartDate.Value,
            .EndDate = dtpEndDate.Value,
            .Status = cboStatus.SelectedItem?.ToString(),
            .ManagerId = GetSelectedComboboxId(cboManager),
            .TeamId = GetSelectedComboboxId(cboTeam)
        }
        
        Dim result = _projectService.CreateProject(dto)
        If result.Success Then
            MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadProjects()
            ClearForm()
        Else
            MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If _selectedProjectId < 0 Then
            MessageBox.Show("Vui lòng chọn dự án.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        
        Dim dto As New ProjectDto() With {
            .ProjectId = _selectedProjectId,
            .ProjectName = txtProjectName.Text.Trim(),
            .Description = txtDescription.Text.Trim(),
            .StartDate = dtpStartDate.Value,
            .EndDate = dtpEndDate.Value,
            .Status = cboStatus.SelectedItem?.ToString(),
            .ManagerId = GetSelectedComboboxId(cboManager),
            .TeamId = GetSelectedComboboxId(cboTeam)
        }
        
        Dim result = _projectService.UpdateProject(dto)
        If result.Success Then
            MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadProjects()
            ClearForm()
        Else
            MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If _selectedProjectId < 0 Then
            MessageBox.Show("Vui lòng chọn dự án.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        
        Dim confirm = MessageBox.Show("Xóa dự án này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm = DialogResult.Yes Then
            Dim result = _projectService.DeleteProject(_selectedProjectId)
            MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LoadProjects()
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

    Private Sub ClearForm()
        _selectedProjectId = -1
        txtProjectName.Text = ""
        txtDescription.Text = ""
        dtpStartDate.Value = DateTime.Now
        dtpEndDate.Value = DateTime.Now.AddMonths(1)
        cboStatus.SelectedIndex = 0
        cboManager.SelectedValue = 0
        cboTeam.SelectedValue = 0
        dgvProjects.ClearSelection()
    End Sub

End Class
