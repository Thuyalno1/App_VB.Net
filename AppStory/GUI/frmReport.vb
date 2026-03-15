Imports ClosedXML.Excel
Imports System.Linq
Imports QuestPDF.Fluent
Imports QuestPDF.Helpers
Imports QuestPDF.Infrastructure

Public Class frmReport
    Inherits System.Windows.Forms.Form

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
        dgvReport.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "ProjectId", .HeaderText = "ID", .Width = 45})
        dgvReport.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "ProjectName", .HeaderText = "Tên Dự Án", .Width = 220})
        dgvReport.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Status", .HeaderText = "Trạng Thái", .Width = 110})
        dgvReport.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "StartDate", .HeaderText = "Ngày BĐ", .Width = 100, .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "dd/MM/yyyy"}})
        dgvReport.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "EndDate", .HeaderText = "Ngày KT", .Width = 100, .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "dd/MM/yyyy"}})
        dgvReport.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Description", .HeaderText = "Mô Tả", .Width = 250})
        
        AddHandler dgvReport.CellFormatting, AddressOf dgvReport_CellFormatting
    End Sub

    Private Sub dgvReport_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If dgvReport.Columns(e.ColumnIndex).DataPropertyName = "Status" AndAlso e.Value IsNot Nothing Then
            Dim p = DirectCast(dgvReport.Rows(e.RowIndex).DataBoundItem, Project)
            If p IsNot Nothing Then
                If p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount Then
                    e.Value = "Hoàn thành"
                Else
                    Dim statusValue = e.Value.ToString()
                    Select Case statusValue
                        Case "Planning" : e.Value = "Lập kế hoạch"
                        Case "Active" : e.Value = "Đang thực hiện"
                        Case "On Hold" : e.Value = "Tạm dừng"
                        Case "Completed" : e.Value = "Hoàn thành"
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

    Private Sub rdoWeek_CheckedChanged(sender As Object, e As EventArgs) Handles rdoWeek.CheckedChanged
        UpdateFilterVisibility()
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

                If rdoWeek.Checked Then
                    Dim weekAgo = DateTime.Now.AddDays(-7)
                    _filteredProjects = _allProjects.Where(Function(p) p.CreatedAt >= weekAgo).ToList()
                ElseIf rdoMonth.Checked Then
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
        ' Sử dụng logic động giống Dashboard
        Dim completed = _filteredProjects.Where(Function(p) p.Status = "Completed" OrElse p.Status = "Hoàn thành" OrElse (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count
        Dim active = _filteredProjects.Where(Function(p) (p.Status = "Active" OrElse p.Status = "Đang thực hiện") AndAlso Not (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count
        Dim overdue = _filteredProjects.Where(Function(p) p.EndDate.HasValue AndAlso p.EndDate.Value < DateTime.Now AndAlso p.Status <> "Completed" AndAlso p.Status <> "Hoàn thành" AndAlso Not (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count

        lblSummaryTotal.Text = $"📁 Tổng: {total}"
        lblSummaryActive.Text = $"🔄 Đang TH: {active}"
        lblSummaryCompleted.Text = $"✅ Hoàn thành: {completed}"
        lblSummaryOverdue.Text = $"⚠️ Quá hạn: {overdue}"
    End Sub

    ' ──────────────────────────────────────────────
    '   EXPORT EXCEL (ClosedXML)
    ' ──────────────────────────────────────────────
    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        If _filteredProjects Is Nothing OrElse _filteredProjects.Count = 0 Then
            MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Using sfd As New SaveFileDialog()
            sfd.Title = "Lưu file Excel"
            sfd.Filter = "Excel Files (*.xlsx)|*.xlsx"
            sfd.FileName = $"BaoCao_DuAn_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
            If sfd.ShowDialog() <> DialogResult.OK Then Return

            Try
                Using wb As New XLWorkbook()
                    Dim ws = wb.Worksheets.Add("Báo Cáo Dự Án")

                    ' Tiêu đề
                    ws.Cell(1, 1).Value = "BÁO CÁO THỐNG KÊ DỰ ÁN"
                    ws.Cell(1, 1).Style.Font.Bold = True
                    ws.Cell(1, 1).Style.Font.FontSize = 14
                    ws.Range("A1:F1").Merge()

                    ws.Cell(2, 1).Value = $"Thời gian xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}"
                    ws.Range("A2:F2").Merge()

                    ' Thống kê tóm tắt (Logic động)
                    Dim total = _filteredProjects.Count
                    Dim completed = _filteredProjects.Where(Function(p) p.Status = "Completed" OrElse p.Status = "Hoàn thành" OrElse (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count
                    Dim active = _filteredProjects.Where(Function(p) (p.Status = "Active" OrElse p.Status = "Đang thực hiện") AndAlso Not (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count
                    Dim overdue = _filteredProjects.Where(Function(p) p.EndDate.HasValue AndAlso p.EndDate.Value < DateTime.Now AndAlso p.Status <> "Completed" AndAlso p.Status <> "Hoàn thành" AndAlso Not (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count

                    ws.Cell(4, 1).Value = "Tổng dự án" : ws.Cell(4, 2).Value = total
                    ws.Cell(5, 1).Value = "Đang thực hiện" : ws.Cell(5, 2).Value = active
                    ws.Cell(6, 1).Value = "Hoàn thành" : ws.Cell(6, 2).Value = completed
                    ws.Cell(7, 1).Value = "Quá hạn" : ws.Cell(7, 2).Value = overdue
                    ws.Range("A4:A7").Style.Font.Bold = True

                    ' Header các cột
                    Dim headers = {"ID", "Tên Dự Án", "Trạng Thái", "Ngày Bắt Đầu", "Ngày Kết Thúc", "Mô Tả"}
                    For i = 0 To headers.Length - 1
                        ws.Cell(9, i + 1).Value = headers(i)
                    Next
                    Dim headerRange = ws.Range("A9:F9")
                    headerRange.Style.Font.Bold = True
                    headerRange.Style.Fill.BackgroundColor = XLColor.FromArgb(99, 102, 241)
                    headerRange.Style.Font.FontColor = XLColor.White

                    ' Dữ liệu
                    Dim currentRow As Integer = 10
                    For Each p In _filteredProjects
                        ' Dòng Dự án
                        ws.Cell(currentRow, 1).Value = p.ProjectId
                        ws.Cell(currentRow, 2).Value = p.ProjectName
                        ws.Cell(currentRow, 2).Style.Font.Bold = True
                        
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
                        ws.Cell(currentRow, 3).Value = pStatus
                        If p.StartDate.HasValue Then ws.Cell(currentRow, 4).Value = p.StartDate.Value.ToString("dd/MM/yyyy")
                        If p.EndDate.HasValue Then ws.Cell(currentRow, 5).Value = p.EndDate.Value.ToString("dd/MM/yyyy")
                        ws.Cell(currentRow, 6).Value = If(p.Description, "")
                        
                        ' Tô màu dòng dự án
                        ws.Range(currentRow, 1, currentRow, 6).Style.Fill.BackgroundColor = XLColor.FromArgb(238, 242, 255)
                        
                        currentRow += 1

                        ' Lấy danh sách task của dự án
                        Dim tasks = _taskService.GetTasksByProjectId(p.ProjectId)
                        If tasks.Count > 0 Then
                            ' Header nhỏ cho task
                            ws.Cell(currentRow, 2).Value = "   └─ Công việc:"
                            ws.Cell(currentRow, 2).Style.Font.Italic = True
                            ws.Cell(currentRow, 3).Value = "Người thực hiện"
                            ws.Cell(currentRow, 4).Value = "Tiến độ"
                            ws.Cell(currentRow, 5).Value = "Trạng thái"
                            ws.Range(currentRow, 2, currentRow, 5).Style.Font.Bold = True
                            ws.Range(currentRow, 2, currentRow, 5).Style.Font.FontSize = 9
                            
                            currentRow += 1

                            For Each t In tasks
                                ws.Cell(currentRow, 2).Value = "      • " & t.Title
                                ws.Cell(currentRow, 3).Value = If(String.IsNullOrEmpty(t.AssignedUserName), "Chưa giao", t.AssignedUserName)
                                ws.Cell(currentRow, 4).Value = t.Progress & "%"
                                ws.Cell(currentRow, 5).Value = If(t.IsApproved = 1, "Đã duyệt", "Chưa duyệt")
                                ws.Range(currentRow, 2, currentRow, 5).Style.Font.FontSize = 9
                                currentRow += 1
                            Next
                        End If
                        
                        currentRow += 1 ' Khoảng cách giữa các dự án
                    Next

                    ' Auto-fit
                    ws.Columns().AdjustToContents()

                    wb.SaveAs(sfd.FileName)
                End Using

                MessageBox.Show($"Xuất Excel thành công!{Environment.NewLine}File: {sfd.FileName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)

                If MessageBox.Show("Mở file vừa xuất?", "Mở file", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo(sfd.FileName) With {.UseShellExecute = True})
                End If

            Catch ex As Exception
                MessageBox.Show("Lỗi xuất Excel: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' ──────────────────────────────────────────────
    '   EXPORT PDF (QuestPDF)
    ' ──────────────────────────────────────────────
    Private Sub btnExportPDF_Click(sender As Object, e As EventArgs) Handles btnExportPDF.Click
        If _filteredProjects Is Nothing OrElse _filteredProjects.Count = 0 Then
            MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Using sfd As New SaveFileDialog()
            sfd.Title = "Lưu file PDF"
            sfd.Filter = "PDF Files (*.pdf)|*.pdf"
            sfd.FileName = $"BaoCao_DuAn_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
            If sfd.ShowDialog() <> DialogResult.OK Then Return

            Try
                Dim total = _filteredProjects.Count
                Dim completed = _filteredProjects.Where(Function(p) p.Status = "Completed" OrElse p.Status = "Hoàn thành" OrElse (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count
                Dim active = _filteredProjects.Where(Function(p) (p.Status = "Active" OrElse p.Status = "Đang thực hiện") AndAlso Not (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count
                Dim overdue = _filteredProjects.Where(Function(p) p.EndDate.HasValue AndAlso p.EndDate.Value < DateTime.Now AndAlso p.Status <> "Completed" AndAlso p.Status <> "Hoàn thành" AndAlso Not (p.TaskCount > 0 AndAlso p.TaskCount = p.ApprovedTaskCount)).Count

                Dim projects = _filteredProjects ' capture for lambda

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
                    End Sub).GeneratePdf(sfd.FileName)

                MessageBox.Show($"Xuất PDF thành công!{Environment.NewLine}File: {sfd.FileName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)

                If MessageBox.Show("Mở file vừa xuất?", "Mở file", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo(sfd.FileName) With {.UseShellExecute = True})
                End If

            Catch ex As Exception
                MessageBox.Show("Lỗi xuất PDF: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' ──────────────────────────────────────────────
    '   NAVIGATION
    ' ──────────────────────────────────────────────
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim mainForm As New frmMain()
        mainForm.Show()
        Me.Close()
    End Sub

End Class
