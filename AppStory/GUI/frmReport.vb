Imports ClosedXML.Excel
Imports System.Linq
Imports System.Drawing
Imports QuestPDF.Fluent
Imports QuestPDF.Helpers
Imports QuestPDF.Infrastructure

Public Class frmReport
    Inherits System.Windows.Forms.Form
    Private _isNavigatingBack As Boolean = False

    Private ReadOnly _projectService As IProjectService
    Private ReadOnly _teamService As ITeamService
    Private ReadOnly _taskService As ITaskService
    Private _allProjects As List(Of Project)
    Private _filteredProjects As List(Of Project)

    Public Sub New()
        InitializeComponent()
        _projectService = New ProjectService()
        _teamService = New TeamService()
        _taskService = New TaskService()
    End Sub

    Private Sub frmReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' QuestPDF Community License
        QuestPDF.Settings.License = LicenseType.Community

        SetupGrid()
        LoadTeamsCombo()
        LoadStatusCombo()
        LoadMonthYearCombos()
        UpdateFilterVisibility()
        LoadAndFilter()
    End Sub

    Private Sub SetupGrid()
        dgvReport.AutoGenerateColumns = False
        dgvReport.Columns.Clear()
        dgvReport.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "ProjectName", .HeaderText = "Tên Dự Án", .Width = 240})
        dgvReport.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "StatusDisplay", .HeaderText = "Trạng Thái", .Width = 120})
        dgvReport.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "StartDate", .HeaderText = "Ngày BĐ", .Width = 100, .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "dd/MM/yyyy"}})
        dgvReport.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "EndDate", .HeaderText = "Ngày KT", .Width = 100, .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "dd/MM/yyyy"}})
        dgvReport.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Description", .HeaderText = "Mô Tả"})

        AddHandler dgvReport.CellFormatting, AddressOf dgvReport_CellFormatting
        ' Double-click dòng → xuất 1 dự án
        AddHandler dgvReport.CellDoubleClick, AddressOf dgvReport_CellDoubleClick
        ' Tooltip gợi ý
        dgvReport.ShowCellToolTips = True
        For Each col As DataGridViewColumn In dgvReport.Columns
            col.ToolTipText = "Double-click dòng để xuất riêng dự án này"
        Next
    End Sub

    Private Sub dgvReport_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If dgvReport.Columns(e.ColumnIndex).DataPropertyName = "StatusDisplay" AndAlso e.Value IsNot Nothing Then
            Dim p = DirectCast(dgvReport.Rows(e.RowIndex).DataBoundItem, Project)
            If p IsNot Nothing Then
                ' Logic xác định màu sắc (StatusDisplay đã lo việc chuyển ngữ tiếng Việt)
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
                e.FormattingApplied = True
            End If
        End If
    End Sub

    Private Sub LoadTeamsCombo()
        Try
            Dim teams = _teamService.GetAllTeams()
            cboTeam.DataSource = teams
            cboTeam.DisplayMember = "TeamName"
            cboTeam.ValueMember = "TeamId"
        Catch ex As BusinessException
            ' Bỏ qua — cboTeam sẽ trống
        End Try
    End Sub

    Private Sub LoadStatusCombo()
        cboStatus.Items.Clear()
        cboStatus.Items.AddRange({"Lập kế hoạch", "Đang thực hiện", "Tạm dừng", "Hoàn thành"})
        cboStatus.SelectedIndex = 1 ' Đang thực hiện
    End Sub

    Private Sub LoadMonthYearCombos()
        ' Months 1-12
        cboMonth.Items.Clear()
        For i As Integer = 1 To 12
            cboMonth.Items.Add(i.ToString())
        Next
        cboMonth.SelectedItem = DateTime.Now.Month.ToString()

        ' Years (current - 5 to current)
        cboYear.Items.Clear()
        Dim currentYear = DateTime.Now.Year
        For i As Integer = currentYear - 5 To currentYear
            cboYear.Items.Add(i.ToString())
        Next
        cboYear.SelectedItem = currentYear.ToString()
    End Sub

    ' ──────────────────────────────────────────────
    '   RADIO BUTTON → SHOW/HIDE FILTER CONTROLS
    ' ──────────────────────────────────────────────
    Private Sub UpdateFilterVisibility()
        ' Date range controls
        Dim showDateRange As Boolean = rdoDateRange.Checked
        lblFrom.Visible = showDateRange
        dtpFrom.Visible = showDateRange
        lblTo.Visible = showDateRange
        dtpTo.Visible = showDateRange

        ' Team
        Dim isTeam As Boolean = rdoTeam.Checked
        lblTeam.Visible = isTeam
        cboTeam.Visible = isTeam

        ' Status
        Dim isStatus As Boolean = rdoStatus.Checked
        lblStatus.Visible = isStatus
        cboStatus.Visible = isStatus

        ' Specific Month
        Dim isMonth As Boolean = rdoMonth.Checked
        lblMonth.Visible = isMonth
        cboMonth.Visible = isMonth
        lblYear.Visible = isMonth
        cboYear.Visible = isMonth

        ' Adjust original month logic to show Specific Month controls
        ' Week and others use default dtp range if needed, 
        ' but for now let's just use what's there.
    End Sub

    Private Sub rdoMonth_CheckedChanged(sender As Object, e As EventArgs) Handles rdoMonth.CheckedChanged
        UpdateFilterVisibility()
    End Sub
    Private Sub rdoDateRange_CheckedChanged(sender As Object, e As EventArgs) Handles rdoDateRange.CheckedChanged
        UpdateFilterVisibility()
    End Sub
    Private Sub rdoTeam_CheckedChanged(sender As Object, e As EventArgs) Handles rdoTeam.CheckedChanged
        UpdateFilterVisibility()
    End Sub
    Private Sub rdoStatus_CheckedChanged(sender As Object, e As EventArgs) Handles rdoStatus.CheckedChanged
        UpdateFilterVisibility()
    End Sub

    ' ──────────────────────────────────────────────
    '   FILTER LOGIC
    ' ──────────────────────────────────────────────
    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        LoadAndFilter()
    End Sub

    Private Sub LoadAndFilter()
        Try
            ' Nếu lọc theo team, dùng API riêng
            If rdoTeam.Checked Then
                If cboTeam.SelectedValue IsNot Nothing Then
                    Dim teamId As Integer = Convert.ToInt32(cboTeam.SelectedValue)
                    _filteredProjects = _projectService.GetProjectsByTeamId(teamId)
                Else
                    _filteredProjects = New List(Of Project)()
                End If
            Else
                ' Load tất cả rồi lọc client-side
                _allProjects = _projectService.GetAllProjects()
                If _allProjects Is Nothing Then _allProjects = New List(Of Project)()

                If rdoMonth.Checked Then
                    ' Lọc cụ thể theo tháng/năm đã chọn
                    Dim selectedMonth = Convert.ToInt32(cboMonth.SelectedItem)
                    Dim selectedYear = Convert.ToInt32(cboYear.SelectedItem)
                    _filteredProjects = _allProjects.Where(Function(p) p.CreatedAt.Month = selectedMonth AndAlso p.CreatedAt.Year = selectedYear).ToList()
                ElseIf rdoDateRange.Checked Then
                    Dim fromDate = dtpFrom.Value.Date
                    Dim toDate = dtpTo.Value.Date.AddDays(1)
                    _filteredProjects = _allProjects.Where(Function(p) p.CreatedAt >= fromDate AndAlso p.CreatedAt < toDate).ToList()
                ElseIf rdoStatus.Checked Then
                    Dim selectedStatus = cboStatus.SelectedItem?.ToString()
                    Dim statusKey = selectedStatus
                    Select Case selectedStatus
                        Case "Lập kế hoạch" : statusKey = "Planning"
                        Case "Đang thực hiện" : statusKey = "Active"
                        Case "Tạm dừng" : statusKey = "On Hold"
                        Case "Hoàn thành" : statusKey = "Completed"
                    End Select

                    If statusKey = "Completed" OrElse selectedStatus = "Hoàn thành" Then
                        ' Lọc theo logic động: DB status là Completed HOẶC (có task và đã duyệt hết)
                        _filteredProjects = _allProjects.Where(Function(p) p.Status = "Completed" OrElse p.Status = "Hoàn thành" OrElse (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).ToList()
                    ElseIf statusKey = "Active" OrElse selectedStatus = "Đang thực hiện" Then
                        ' Lọc Đang thực hiện: DB là Active NHƯNG loại trừ những cái đã xong việc (theo logic động)
                        _filteredProjects = _allProjects.Where(Function(p) (p.Status = "Active" OrElse p.Status = "Đang thực hiện") AndAlso Not (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).ToList()
                    Else
                        _filteredProjects = _allProjects.Where(Function(p) p.Status = statusKey OrElse p.Status = selectedStatus).ToList()
                    End If
                Else
                    _filteredProjects = _allProjects
                End If
            End If

            ' Bind grid
            dgvReport.DataSource = Nothing
            dgvReport.DataSource = _filteredProjects

            ' Update summary
            UpdateSummary()

        Catch ex As BusinessException
            MessageBox.Show("Lỗi tải dữ liệu: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateSummary()
        If _filteredProjects Is Nothing Then Return
        Dim total = _filteredProjects.Count
        
        ' 1. Hoàn thành (Ưu tiên cao nhất)
        Dim completedItems = _filteredProjects.Where(Function(p) p.Status = "Completed" OrElse p.Status = "Hoàn thành" OrElse (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).ToList()
        Dim completedIds = completedItems.Select(Function(p) p.ProjectId).ToList()

        ' 2. Quá hạn (Chưa xong và deadline đã qua)
        Dim overdueItems = _filteredProjects.Where(Function(p) Not completedIds.Contains(p.ProjectId) AndAlso p.EndDate.HasValue AndAlso p.EndDate.Value.Date < DateTime.Now.Date).ToList()
        Dim overdueIds = overdueItems.Select(Function(p) p.ProjectId).ToList()

        ' 3. Đang thực hiện (Chưa xong, chưa quá hạn và đang Active)
        Dim activeItems = _filteredProjects.Where(Function(p) Not completedIds.Contains(p.ProjectId) AndAlso Not overdueIds.Contains(p.ProjectId) AndAlso (p.Status = "Active" OrElse p.Status = "Đang thực hiện")).ToList()

        lblSummaryTotal.Text = $"📁 Tổng: {total}"
        lblSummaryActive.Text = $"🔄 Đang TH: {activeItems.Count}"
        lblSummaryCompleted.Text = $"✅ Hoàn thành: {completedItems.Count}"
        lblSummaryOverdue.Text = $"⚠️ Quá hạn: {overdueItems.Count}"
    End Sub

    ' ──────────────────────────────────────────────
    '   EXPORT EXCEL (ClosedXML)
    ' ──────────────────────────────────────────────
    ' ──────────────────────────────────────────────
    '   DOUBLE-CLICK DÒNG → XUẤT 1 DỰ ÁN
    ' ──────────────────────────────────────────────
    Private Sub dgvReport_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        Dim p = TryCast(dgvReport.Rows(e.RowIndex).DataBoundItem, Project)
        If p Is Nothing Then Return

        Dim choice = MessageBox.Show(
            $"Xuất dự án '{p.ProjectName}' ra file nào?{Environment.NewLine}[Yes] = Excel   [No] = PDF",
            "Chọn định dạng xuất",
            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

        If choice = DialogResult.Yes Then
            ExportSingleProjectExcel(p)
        ElseIf choice = DialogResult.No Then
            ExportSingleProjectPDF(p)
        End If
    End Sub

    Private Sub ExportSingleProjectExcel(p As Project)
        Using sfd As New SaveFileDialog()
            sfd.Title = "Xuất dự án ra Excel"
            sfd.Filter = "Excel Files (*.xlsx)|*.xlsx"
            sfd.FileName = $"DuAn_{p.ProjectName.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
            If sfd.ShowDialog() <> DialogResult.OK Then Return
            Try
                Using wb As New XLWorkbook()
                    Dim ws = wb.Worksheets.Add("Dự Án")
                    BuildProjectSheet(ws, p)
                    ws.Columns().AdjustToContents()
                    wb.SaveAs(sfd.FileName)
                End Using
                MessageBox.Show($"✅ Xuất thành công: {sfd.FileName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If MessageBox.Show("Mở file?", "", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo(sfd.FileName) With {.UseShellExecute = True})
                End If
            Catch ex As Exception
                MessageBox.Show("Lỗi: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    Private Sub ExportSingleProjectPDF(p As Project)
        Using sfd As New SaveFileDialog()
            sfd.Title = "Xuất dự án ra PDF"
            sfd.Filter = "PDF Files (*.pdf)|*.pdf"
            sfd.FileName = $"DuAn_{p.ProjectName.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
            If sfd.ShowDialog() <> DialogResult.OK Then Return
            Try
                BuildSingleProjectPDF({p}.ToList(), sfd.FileName, p.ProjectName)
                MessageBox.Show($"✅ Xuất PDF thành công: {sfd.FileName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If MessageBox.Show("Mở file?", "", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo(sfd.FileName) With {.UseShellExecute = True})
                End If
            Catch ex As Exception
                MessageBox.Show("Lỗi: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' ──────────────────────────────────────────────
    '   EXPORT EXCEL (CáCĨ / TẤT CẢ)
    ' ──────────────────────────────────────────────
    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        Dim selectedProjects As New List(Of Project)
        For Each row As DataGridViewRow In dgvReport.SelectedRows
            Dim p = TryCast(row.DataBoundItem, Project)
            If p IsNot Nothing Then
                selectedProjects.Add(p)
            End If
        Next
        
        If selectedProjects.Count = 0 Then
            MessageBox.Show("Vui lòng click chọn ít nhất 1 dự án để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ExportProjectListToExcel(selectedProjects, "BaoCao_DuAnDuocChon")
    End Sub

    Private Sub btnExportAllExcel_Click(sender As Object, e As EventArgs) Handles btnExportAllExcel.Click
        ExportProjectListToExcel(_filteredProjects, "BaoCao_TatCa")
    End Sub

    ''' <summary>Xuất danh sách các dự án ra 1 file Excel (mỗi dự án = 1 sheet)</summary>
    Private Sub ExportProjectListToExcel(projects As List(Of Project), baseFileName As String)
        If projects Is Nothing OrElse projects.Count = 0 Then
            MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Using sfd As New SaveFileDialog()
            sfd.Title = "Lưu file Excel"
            sfd.Filter = "Excel Files (*.xlsx)|*.xlsx"
            sfd.FileName = $"{baseFileName}_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
            If sfd.ShowDialog() <> DialogResult.OK Then Return

            Try
                Using wb As New XLWorkbook()
                    ' Sheet tóm tắt
                    Dim wsSummary = wb.Worksheets.Add("Tổng Hợp")
                    wsSummary.Cell(1, 1).Value = "BÁO CÁO THỐNG KÊ DỰ ÁN"
                    wsSummary.Cell(1, 1).Style.Font.Bold = True : wsSummary.Cell(1, 1).Style.Font.FontSize = 14
                    wsSummary.Range("A1:F1").Merge()
                    wsSummary.Cell(2, 1).Value = $"Thời gian xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}"
                    wsSummary.Range("A2:F2").Merge()

                    Dim total = projects.Count
                    Dim completed = projects.Where(Function(p) p.Status = "Completed" OrElse p.Status = "Hoàn thành" OrElse (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count()
                    Dim active = projects.Where(Function(p) (p.Status = "Active" OrElse p.Status = "Đang thực hiện") AndAlso Not (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count()
                    Dim overdue = projects.Where(Function(p) p.EndDate.HasValue AndAlso p.EndDate.Value < DateTime.Now AndAlso p.Status <> "Completed" AndAlso Not (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count()

                    wsSummary.Cell(4, 1).Value = "Tổng dự án" : wsSummary.Cell(4, 2).Value = total
                    wsSummary.Cell(5, 1).Value = "Đang thực hiện" : wsSummary.Cell(5, 2).Value = active
                    wsSummary.Cell(6, 1).Value = "Hoàn thành" : wsSummary.Cell(6, 2).Value = completed
                    wsSummary.Cell(7, 1).Value = "Quá hạn" : wsSummary.Cell(7, 2).Value = overdue
                    wsSummary.Range("A4:A7").Style.Font.Bold = True

                    ' Header các cột tổng hợp
                    Dim hRow = 9
                    Dim headers = {"Tên Dự Án", "Trạng Thái", "Ngày Bắt Đầu", "Ngày Kết Thúc", "Mô Tả"}
                    For i = 0 To headers.Length - 1
                        wsSummary.Cell(hRow, i + 1).Value = headers(i)
                    Next
                    Dim hRange = wsSummary.Range(hRow, 1, hRow, headers.Length)
                    hRange.Style.Font.Bold = True
                    hRange.Style.Fill.BackgroundColor = XLColor.FromArgb(99, 102, 241)
                    hRange.Style.Font.FontColor = XLColor.White

                    Dim currentRow = hRow + 1
                    For Each p In projects
                        wsSummary.Cell(currentRow, 1).Value = $"■ {p.ProjectName}"
                        wsSummary.Cell(currentRow, 1).Style.Font.Bold = True
                        Dim pStatus = GetStatusDisplay(p)
                        wsSummary.Cell(currentRow, 2).Value = pStatus
                        If p.StartDate.HasValue Then wsSummary.Cell(currentRow, 3).Value = p.StartDate.Value.ToString("dd/MM/yyyy")
                        If p.EndDate.HasValue Then wsSummary.Cell(currentRow, 4).Value = p.EndDate.Value.ToString("dd/MM/yyyy")
                        wsSummary.Cell(currentRow, 5).Value = If(p.Description, "")
                        wsSummary.Range(currentRow, 1, currentRow, headers.Length).Style.Fill.BackgroundColor = XLColor.FromArgb(238, 242, 255)
                        currentRow += 1

                        ' Nạp danh sách Task của dự án
                        Dim tasks = _taskService.GetTasksByProjectId(p.ProjectId)
                        If tasks.Count > 0 Then
                            ' Tạo dòng tiêu đề cho Task thụt lề
                            wsSummary.Cell(currentRow, 2).Value = "↳ Tên công việc"
                            wsSummary.Cell(currentRow, 3).Value = "Tiến độ"
                            wsSummary.Cell(currentRow, 4).Value = "Người thực hiện"
                            wsSummary.Cell(currentRow, 5).Value = "Trạng thái duyệt"
                            Dim taskHead = wsSummary.Range(currentRow, 2, currentRow, 5)
                            taskHead.Style.Font.Italic = True
                            taskHead.Style.Font.Bold = True
                            taskHead.Style.Font.FontColor = XLColor.DimGray
                            currentRow += 1

                            For Each t In tasks
                                wsSummary.Cell(currentRow, 2).Value = t.Title
                                wsSummary.Cell(currentRow, 3).Value = t.Progress & "%"
                                wsSummary.Cell(currentRow, 4).Value = If(String.IsNullOrEmpty(t.AssignedUserName), "Chưa phân công", t.AssignedUserName)
                                wsSummary.Cell(currentRow, 5).Value = If(t.IsApproved = 1, "Đã duyệt", "Chưa duyệt")
                                currentRow += 1
                            Next
                        Else
                            wsSummary.Cell(currentRow, 2).Value = "(Chưa có công việc nào)"
                            wsSummary.Cell(currentRow, 2).Style.Font.Italic = True
                            wsSummary.Cell(currentRow, 2).Style.Font.FontColor = XLColor.Gray
                            currentRow += 1
                        End If
                        currentRow += 1 ' dòng trống phân cách dự án
                    Next
                    wsSummary.Columns().AdjustToContents()

                    ' Mỗi dự án = 1 sheet riêng
                    For Each p In projects
                        Dim cleanName = If(p.ProjectName, "DuAn")
                        Dim invalidChars = {":", "\", "/", "?", "*", "[", "]"}
                        For Each c In invalidChars
                            cleanName = cleanName.Replace(c, "_")
                        Next
                        Dim safeName = $"ID{p.ProjectId}_" & New String(cleanName.Take(20).ToArray()).Trim()
                        Dim ws = wb.Worksheets.Add(safeName)
                        BuildProjectSheet(ws, p)
                        ws.Columns().AdjustToContents()
                    Next

                    wb.SaveAs(sfd.FileName)
                End Using

                MessageBox.Show($"✅ Xuất Excel thành công!{Environment.NewLine}File: {sfd.FileName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If MessageBox.Show("Mở file vừa xuất?", "Mở file", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo(sfd.FileName) With {.UseShellExecute = True})
                End If
            Catch ex As Exception
                MessageBox.Show("Lỗi xuất Excel: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ''' <summary>Xây dựng 1 worksheet cho 1 dự án (dùng lại cho xuất đơn lẻ và xuất tất cả)</summary>
    Private Sub BuildProjectSheet(ws As IXLWorksheet, p As Project)
        ws.Cell(1, 1).Value = $"DỰ ÁN: {p.ProjectName}"
        ws.Cell(1, 1).Style.Font.Bold = True : ws.Cell(1, 1).Style.Font.FontSize = 13
        ws.Range("A1:E1").Merge()

        ws.Cell(2, 1).Value = $"Trạng thái: {GetStatusDisplay(p)}"
        ws.Cell(3, 1).Value = $"Ngày BD: {If(p.StartDate.HasValue, p.StartDate.Value.ToString("dd/MM/yyyy"), "N/A")}"
        ws.Cell(4, 1).Value = $"Ngày KT: {If(p.EndDate.HasValue, p.EndDate.Value.ToString("dd/MM/yyyy"), "N/A")}"
        ws.Cell(5, 1).Value = $"Mô tả: {If(p.Description, "")}"
        ws.Range("A2:E5").Style.Font.Italic = True

        ' Header task
        ws.Cell(7, 1).Value = "Tên công việc" : ws.Cell(7, 2).Value = "Người thực hiện"
        ws.Cell(7, 3).Value = "Tiến độ" : ws.Cell(7, 4).Value = "Trạng thái duyệt"
        ws.Range("A7:D7").Style.Font.Bold = True
        ws.Range("A7:D7").Style.Fill.BackgroundColor = XLColor.FromArgb(224, 231, 255)

        Dim tasks = _taskService.GetTasksByProjectId(p.ProjectId)
        Dim r = 8
        For Each t In tasks
            ws.Cell(r, 1).Value = t.Title
            ws.Cell(r, 2).Value = If(String.IsNullOrEmpty(t.AssignedUserName), "Chưa giao", t.AssignedUserName)
            ws.Cell(r, 3).Value = t.Progress & "%"
            ws.Cell(r, 4).Value = If(t.IsApproved = 1, "Đã duyệt", "Chưa duyệt")
            r += 1
        Next
        If tasks.Count = 0 Then
            ws.Cell(8, 1).Value = "(Không có công việc nào)"
        End If
    End Sub

    ''' <summary>Trả về nhãn trạng thái hiển thị của Project</summary>
    Private Function GetStatusDisplay(p As Project) As String
        If p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount Then Return "Hoàn thành"
        Select Case p.Status
            Case "Planning" : Return "Lập kế hoạch"
            Case "Active" : Return "Đang thực hiện"
            Case "On Hold" : Return "Tạm dừng"
            Case "Completed" : Return "Hoàn thành"
            Case Else : Return If(p.Status, "")
        End Select
    End Function

    ' ──────────────────────────────────────────────
    '   EXPORT PDF (QuestPDF)
    ' ──────────────────────────────────────────────
    Private Sub btnExportPDF_Click(sender As Object, e As EventArgs) Handles btnExportPDF.Click
        Dim selectedProjects As New List(Of Project)
        For Each row As DataGridViewRow In dgvReport.SelectedRows
            Dim p = TryCast(row.DataBoundItem, Project)
            If p IsNot Nothing Then
                selectedProjects.Add(p)
            End If
        Next
        
        If selectedProjects.Count = 0 Then
            MessageBox.Show("Vui lòng click chọn ít nhất 1 dự án để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        BuildSingleProjectPDFFromList(selectedProjects, "Báo cáo tuỳ chọn")
    End Sub

    Private Sub btnExportAllPDF_Click(sender As Object, e As EventArgs) Handles btnExportAllPDF.Click
        BuildSingleProjectPDFFromList(_filteredProjects, "Báo cáo toàn bộ")
    End Sub

    ''' <summary>Helper gọi hộp thoại và tạo PDF cho một danh sách dự án</summary>
    Private Sub BuildSingleProjectPDFFromList(projects As List(Of Project), reportTitle As String)
        If projects Is Nothing OrElse projects.Count = 0 Then
            MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Using sfd As New SaveFileDialog()
            sfd.Title = "Lưu file PDF"
            sfd.Filter = "PDF Files (*.pdf)|*.pdf"
            sfd.FileName = $"BaoCao_DuAn_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
            If sfd.ShowDialog() <> DialogResult.OK Then Return
            Try
                BuildSingleProjectPDF(projects, sfd.FileName, reportTitle)
                MessageBox.Show($"✅ Xuất PDF thành công!{Environment.NewLine}File: {sfd.FileName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If MessageBox.Show("Mở file vừa xuất?", "Mở file", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo(sfd.FileName) With {.UseShellExecute = True})
                End If
            Catch ex As Exception
                MessageBox.Show("Lỗi xuất PDF: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ''' <summary>Tạo nội dung PDF và ghi ra file</summary>
    Private Sub BuildSingleProjectPDF(projects As List(Of Project), filePath As String, reportTitle As String)
        Dim total = projects.Count
        Dim completed = projects.Where(Function(p) p.Status = "Completed" OrElse p.Status = "Hoàn thành" OrElse (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count()
        Dim active = projects.Where(Function(p) (p.Status = "Active" OrElse p.Status = "Đang thực hiện") AndAlso Not (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count()
        Dim overdue = projects.Where(Function(p) p.EndDate.HasValue AndAlso p.EndDate.Value < DateTime.Now AndAlso p.Status <> "Completed" AndAlso Not (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count()

        Document.Create(
            Sub(container)
                        container.Page(
                            Sub(page)
                                page.Size(PageSizes.A4.Landscape())
                                page.Margin(1.5F, Unit.Centimetre)
                                page.DefaultTextStyle(Function(s) s.FontSize(10))

                                ' Header
                                page.Header().Column(
                                    Sub(col)
                                        col.Item().Text("BÁO CÁO THỐNG KÊ DỰ ÁN").FontSize(18).Bold().FontColor(Colors.Indigo.Medium)
                                        col.Item().Text($"Thời gian xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}").FontSize(9).FontColor(Colors.Grey.Medium)
                                        col.Item().PaddingTop(8).Row(
                                            Sub(row)
                                                row.AutoItem().Text($"Tổng: {total}  ").Bold()
                                                row.AutoItem().Text($"Đang TH: {active}  ").FontColor(Colors.Orange.Medium)
                                                row.AutoItem().Text($"Hoàn thành: {completed}  ").FontColor(Colors.Green.Medium)
                                                row.AutoItem().Text($"Quá hạn: {overdue}").FontColor(Colors.Red.Medium)
                                            End Sub)
                                        col.Item().PaddingBottom(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                                    End Sub)

                                ' Content — List of Projects and Tasks
                                page.Content().Column(
                                    Sub(col)
                                        For Each p In projects
                                            ' Project Block
                                            col.Item().PaddingVertical(5).Background(Colors.Grey.Lighten4).Padding(5).Row(
                                                Sub(row)
                                                    row.ConstantItem(40).Text($"#{p.ProjectId}").Bold()
                                                    row.RelativeItem(3).Text(p.ProjectName).Bold()
                                                    
                                                    Dim pStatus = p.Status
                                                    If p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount Then
                                                        pStatus = "Hoàn thành"
                                                    Else
                                                        Select Case pStatus
                                                            Case "Planning" : pStatus = "Lập kế hoạch"
                                                            Case "Active" : pStatus = "Đang thực hiện"
                                                            Case "On Hold" : pStatus = "Tạm dừng"
                                                            Case "Completed" : pStatus = "Hoàn thành"
                                                        End Select
                                                    End If
                                                    row.RelativeItem(1.5F).AlignRight().Text(pStatus).Bold().FontColor(Colors.Indigo.Medium)
                                                End Sub)
                                            
                                            col.Item().PaddingLeft(10).PaddingBottom(5).Text($"Mô tả: {If(p.Description, "N/A")}").FontSize(9).Italic()
                                            
                                            ' Task Sub-Table
                                            Dim projectTasks = _taskService.GetTasksByProjectId(p.ProjectId)
                                            If projectTasks.Count > 0 Then
                                                col.Item().PaddingLeft(20).Table(
                                                    Sub(tTable)
                                                        tTable.ColumnsDefinition(
                                                            Sub(tCols)
                                                                tCols.RelativeColumn(3)   ' Task Title
                                                                tCols.RelativeColumn(1.5F) ' Assignee
                                                                tCols.ConstantColumn(50)  ' Progress
                                                                tCols.ConstantColumn(70)  ' Approval Status
                                                            End Sub)
                                                        
                                                        ' Task Header
                                                        tTable.Header(
                                                            Sub(tHeader)
                                                                tHeader.Cell().BorderBottom(1).Padding(2).Text("Tên công việc").FontSize(8).Bold()
                                                                tHeader.Cell().BorderBottom(1).Padding(2).Text("Người thực hiện").FontSize(8).Bold()
                                                                tHeader.Cell().BorderBottom(1).Padding(2).Text("Tiến độ").FontSize(8).Bold()
                                                                tHeader.Cell().BorderBottom(1).Padding(2).Text("Trạng thái").FontSize(8).Bold()
                                                            End Sub)
                                                        
                                                        ' Task Rows
                                                        For Each t In projectTasks
                                                            tTable.Cell().BorderBottom(0.5F).BorderColor(Colors.Grey.Lighten2).Padding(2).Text(t.Title).FontSize(8)
                                                            tTable.Cell().BorderBottom(0.5F).BorderColor(Colors.Grey.Lighten2).Padding(2).Text(If(String.IsNullOrEmpty(t.AssignedUserName), "-", t.AssignedUserName)).FontSize(8)
                                                            tTable.Cell().BorderBottom(0.5F).BorderColor(Colors.Grey.Lighten2).Padding(2).Text($"{t.Progress}%").FontSize(8)
                                                            tTable.Cell().BorderBottom(0.5F).BorderColor(Colors.Grey.Lighten2).Padding(2).Text(If(t.IsApproved = 1, "Đã duyệt", "Chưa duyệt")).FontSize(8)
                                                        Next
                                                    End Sub)
                                            End If
                                            col.Item().PaddingVertical(5).BorderBottom(0.5F).BorderColor(Colors.Grey.Lighten3)
                                        Next
                                    End Sub)

                                ' Footer
                                page.Footer().AlignCenter().Text(
                                    Sub(t)
                                        t.Span("Trang ")
                                        t.CurrentPageNumber()
                                        t.Span(" / ")
                                        t.TotalPages()
                                    End Sub)
                            End Sub)
                    End Sub).GeneratePdf(filePath)
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

    Private Sub frmReport_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If Not _isNavigatingBack Then
            Application.Exit()
        End If
    End Sub

End Class
