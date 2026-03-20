<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmReport
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.pnlFilter = New System.Windows.Forms.Panel()
        Me.lblFilterTitle = New System.Windows.Forms.Label()
        Me.rdoMonth = New System.Windows.Forms.RadioButton()
        Me.rdoDateRange = New System.Windows.Forms.RadioButton()
        Me.rdoTeam = New System.Windows.Forms.RadioButton()
        Me.rdoStatus = New System.Windows.Forms.RadioButton()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.lblTeam = New System.Windows.Forms.Label()
        Me.cboTeam = New System.Windows.Forms.ComboBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.lblMonth = New System.Windows.Forms.Label()
        Me.cboMonth = New System.Windows.Forms.ComboBox()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.cboYear = New System.Windows.Forms.ComboBox()
        Me.btnFilter = New System.Windows.Forms.Button()
        Me.pnlSummary = New System.Windows.Forms.Panel()
        Me.lblSummaryTotal = New System.Windows.Forms.Label()
        Me.lblSummaryActive = New System.Windows.Forms.Label()
        Me.lblSummaryCompleted = New System.Windows.Forms.Label()
        Me.lblSummaryOverdue = New System.Windows.Forms.Label()
        Me.dgvReport = New System.Windows.Forms.DataGridView()
        Me.pnlExport = New System.Windows.Forms.Panel()
        Me.btnExportExcel = New System.Windows.Forms.Button()
        Me.btnExportPDF = New System.Windows.Forms.Button()
        Me.lblExportHint = New System.Windows.Forms.Label()
        Me.btnExportAllExcel = New System.Windows.Forms.Button()
        Me.btnExportAllPDF = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        Me.pnlFilter.SuspendLayout()
        Me.pnlSummary.SuspendLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlExport.SuspendLayout()
        Me.SuspendLayout()

        '─── HEADER ───
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(99, 102, 241)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.btnBack)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(960, 55)

        Me.lblTitle.AutoSize = False
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(15, 0)
        Me.lblTitle.Size = New System.Drawing.Size(500, 55)
        Me.lblTitle.Text = "📊 Báo Cáo Thống Kê Dự Án"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Location = New System.Drawing.Point(870, 12)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(80, 32)
        Me.btnBack.Text = "← Quay lại"
        Me.btnBack.Cursor = System.Windows.Forms.Cursors.Hand

        '─── FILTER PANEL ───
        Me.pnlFilter.BackColor = System.Drawing.Color.White
        Me.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlFilter.Controls.Add(Me.lblFilterTitle)
        Me.pnlFilter.Controls.Add(Me.rdoMonth)
        Me.pnlFilter.Controls.Add(Me.rdoDateRange)
        Me.pnlFilter.Controls.Add(Me.rdoTeam)
        Me.pnlFilter.Controls.Add(Me.rdoStatus)
        Me.pnlFilter.Controls.Add(Me.lblFrom)
        Me.pnlFilter.Controls.Add(Me.dtpFrom)
        Me.pnlFilter.Controls.Add(Me.lblTo)
        Me.pnlFilter.Controls.Add(Me.dtpTo)
        Me.pnlFilter.Controls.Add(Me.lblTeam)
        Me.pnlFilter.Controls.Add(Me.cboTeam)
        Me.pnlFilter.Controls.Add(Me.lblStatus)
        Me.pnlFilter.Controls.Add(Me.cboStatus)
        Me.pnlFilter.Controls.Add(Me.lblMonth)
        Me.pnlFilter.Controls.Add(Me.cboMonth)
        Me.pnlFilter.Controls.Add(Me.lblYear)
        Me.pnlFilter.Controls.Add(Me.cboYear)
        Me.pnlFilter.Controls.Add(Me.btnFilter)
        Me.pnlFilter.Location = New System.Drawing.Point(12, 62)
        Me.pnlFilter.Name = "pnlFilter"
        Me.pnlFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlFilter.Size = New System.Drawing.Size(936, 115)

        Me.lblFilterTitle.AutoSize = True
        Me.lblFilterTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblFilterTitle.ForeColor = System.Drawing.Color.FromArgb(99, 102, 241)
        Me.lblFilterTitle.Location = New System.Drawing.Point(10, 8)
        Me.lblFilterTitle.Text = "🔍 Bộ lọc báo cáo"

        ' Radio buttons - row 1
        Dim rdoFont As New System.Drawing.Font("Segoe UI", 9.5!)
        Me.rdoMonth.AutoSize = True : Me.rdoMonth.Font = rdoFont : Me.rdoMonth.Location = New System.Drawing.Point(12, 35) : Me.rdoMonth.Text = "Theo tháng" : Me.rdoMonth.Checked = True
        Me.rdoDateRange.AutoSize = True : Me.rdoDateRange.Font = rdoFont : Me.rdoDateRange.Location = New System.Drawing.Point(140, 35) : Me.rdoDateRange.Text = "Khoảng ngày"
        Me.rdoTeam.AutoSize = True : Me.rdoTeam.Font = rdoFont : Me.rdoTeam.Location = New System.Drawing.Point(290, 35) : Me.rdoTeam.Text = "Theo team"
        Me.rdoStatus.AutoSize = True : Me.rdoStatus.Font = rdoFont : Me.rdoStatus.Location = New System.Drawing.Point(420, 35) : Me.rdoStatus.Text = "Theo trạng thái"

        ' Date range controls - row 2
        Me.lblFrom.AutoSize = True : Me.lblFrom.Font = rdoFont : Me.lblFrom.Location = New System.Drawing.Point(12, 72) : Me.lblFrom.Text = "Từ ngày:"
        Me.dtpFrom.Font = rdoFont : Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short : Me.dtpFrom.Location = New System.Drawing.Point(80, 68) : Me.dtpFrom.Size = New System.Drawing.Size(120, 26) : Me.dtpFrom.Value = DateTime.Now.AddDays(-30)
        Me.lblTo.AutoSize = True : Me.lblTo.Font = rdoFont : Me.lblTo.Location = New System.Drawing.Point(210, 72) : Me.lblTo.Text = "Đến ngày:"
        Me.dtpTo.Font = rdoFont : Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short : Me.dtpTo.Location = New System.Drawing.Point(285, 68) : Me.dtpTo.Size = New System.Drawing.Size(120, 26) : Me.dtpTo.Value = DateTime.Now

        ' Team combo
        Me.lblTeam.AutoSize = True : Me.lblTeam.Font = rdoFont : Me.lblTeam.Location = New System.Drawing.Point(420, 72) : Me.lblTeam.Text = "Team:" : Me.lblTeam.Visible = False
        Me.cboTeam.Font = rdoFont : Me.cboTeam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList : Me.cboTeam.Location = New System.Drawing.Point(475, 68) : Me.cboTeam.Size = New System.Drawing.Size(180, 26) : Me.cboTeam.Visible = False

        ' Status combo
        Me.lblStatus.AutoSize = True : Me.lblStatus.Font = rdoFont : Me.lblStatus.Location = New System.Drawing.Point(420, 72) : Me.lblStatus.Text = "Trạng thái:" : Me.lblStatus.Visible = False
        Me.cboStatus.Font = rdoFont : Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList : Me.cboStatus.Location = New System.Drawing.Point(505, 68) : Me.cboStatus.Size = New System.Drawing.Size(150, 26) : Me.cboStatus.Visible = False

        ' Month combo
        Me.lblMonth.AutoSize = True : Me.lblMonth.Font = rdoFont : Me.lblMonth.Location = New System.Drawing.Point(12, 72) : Me.lblMonth.Text = "Tháng:" : Me.lblMonth.Visible = False
        Me.cboMonth.Font = rdoFont : Me.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList : Me.cboMonth.Location = New System.Drawing.Point(65, 68) : Me.cboMonth.Size = New System.Drawing.Size(100, 26) : Me.cboMonth.Visible = False

        ' Year combo
        Me.lblYear.AutoSize = True : Me.lblYear.Font = rdoFont : Me.lblYear.Location = New System.Drawing.Point(180, 72) : Me.lblYear.Text = "Năm:" : Me.lblYear.Visible = False
        Me.cboYear.Font = rdoFont : Me.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList : Me.cboYear.Location = New System.Drawing.Point(225, 68) : Me.cboYear.Size = New System.Drawing.Size(100, 26) : Me.cboYear.Visible = False

        ' Filter button
        Me.btnFilter.BackColor = System.Drawing.Color.FromArgb(99, 102, 241)
        Me.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFilter.FlatAppearance.BorderSize = 0
        Me.btnFilter.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnFilter.ForeColor = System.Drawing.Color.White
        Me.btnFilter.Location = New System.Drawing.Point(820, 65)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Size = New System.Drawing.Size(100, 32)
        Me.btnFilter.Text = "🔎 Lọc"
        Me.btnFilter.Cursor = System.Windows.Forms.Cursors.Hand

        '─── SUMMARY PANEL ───
        Me.pnlSummary.BackColor = System.Drawing.Color.FromArgb(249, 250, 251)
        Me.pnlSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSummary.Controls.Add(Me.lblSummaryTotal)
        Me.pnlSummary.Controls.Add(Me.lblSummaryActive)
        Me.pnlSummary.Controls.Add(Me.lblSummaryCompleted)
        Me.pnlSummary.Controls.Add(Me.lblSummaryOverdue)
        Me.pnlSummary.Location = New System.Drawing.Point(12, 184)
        Me.pnlSummary.Name = "pnlSummary"
        Me.pnlSummary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlSummary.Size = New System.Drawing.Size(936, 35)

        Dim sumFont As New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblSummaryTotal.AutoSize = True : Me.lblSummaryTotal.Font = sumFont : Me.lblSummaryTotal.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235) : Me.lblSummaryTotal.Location = New System.Drawing.Point(10, 8) : Me.lblSummaryTotal.Text = "Tổng: 0"
        Me.lblSummaryActive.AutoSize = True : Me.lblSummaryActive.Font = sumFont : Me.lblSummaryActive.ForeColor = System.Drawing.Color.FromArgb(245, 158, 11) : Me.lblSummaryActive.Location = New System.Drawing.Point(170, 8) : Me.lblSummaryActive.Text = "Đang TH: 0"
        Me.lblSummaryCompleted.AutoSize = True : Me.lblSummaryCompleted.Font = sumFont : Me.lblSummaryCompleted.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129) : Me.lblSummaryCompleted.Location = New System.Drawing.Point(350, 8) : Me.lblSummaryCompleted.Text = "Hoàn thành: 0"
        Me.lblSummaryOverdue.AutoSize = True : Me.lblSummaryOverdue.Font = sumFont : Me.lblSummaryOverdue.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38) : Me.lblSummaryOverdue.Location = New System.Drawing.Point(550, 8) : Me.lblSummaryOverdue.Text = "Quá hạn: 0"

        '─── DATA GRID ───
        Me.dgvReport.BackgroundColor = System.Drawing.Color.White
        Me.dgvReport.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvReport.ColumnHeadersDefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {
            .BackColor = System.Drawing.Color.FromArgb(99, 102, 241),
            .ForeColor = System.Drawing.Color.White,
            .Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        }
        Me.dgvReport.ColumnHeadersHeight = 35
        Me.dgvReport.DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {
            .Font = New System.Drawing.Font("Segoe UI", 9.5!),
            .SelectionBackColor = System.Drawing.Color.FromArgb(224, 231, 255),
            .SelectionForeColor = System.Drawing.Color.Black
        }
        Me.dgvReport.Location = New System.Drawing.Point(12, 226)
        Me.dgvReport.Name = "dgvReport"
        Me.dgvReport.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvReport.RowHeadersVisible = False
        Me.dgvReport.RowTemplate.Height = 30
        Me.dgvReport.Size = New System.Drawing.Size(936, 300)
        Me.dgvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvReport.ReadOnly = True
        Me.dgvReport.AllowUserToAddRows = False

        '─── EXPORT PANEL ───
        Me.pnlExport.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlExport.Controls.Add(Me.btnExportExcel)
        Me.pnlExport.Controls.Add(Me.btnExportPDF)
        Me.pnlExport.Location = New System.Drawing.Point(12, 532)
        Me.pnlExport.Name = "pnlExport"
        Me.pnlExport.Size = New System.Drawing.Size(936, 68)

        Me.btnExportExcel.BackColor = System.Drawing.Color.FromArgb(16, 185, 129)
        Me.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportExcel.FlatAppearance.BorderSize = 0
        Me.btnExportExcel.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnExportExcel.ForeColor = System.Drawing.Color.White
        Me.btnExportExcel.Location = New System.Drawing.Point(0, 2)
        Me.btnExportExcel.Name = "btnExportExcel"
        Me.btnExportExcel.Size = New System.Drawing.Size(180, 38)
        Me.btnExportExcel.Text = "📗 Xuất Excel (Đang chọn)"
        Me.btnExportExcel.Cursor = System.Windows.Forms.Cursors.Hand

        Me.btnExportPDF.BackColor = System.Drawing.Color.FromArgb(220, 38, 38)
        Me.btnExportPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportPDF.FlatAppearance.BorderSize = 0
        Me.btnExportPDF.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnExportPDF.ForeColor = System.Drawing.Color.White
        Me.btnExportPDF.Location = New System.Drawing.Point(200, 2)
        Me.btnExportPDF.Name = "btnExportPDF"
        Me.btnExportPDF.Size = New System.Drawing.Size(185, 38)
        Me.btnExportPDF.Text = "📕 Xuất PDF (Đang chọn)"
        Me.btnExportPDF.Cursor = System.Windows.Forms.Cursors.Hand

        ' ─ Hint label
        Me.lblExportHint = New System.Windows.Forms.Label()
        Me.lblExportHint.AutoSize = False
        Me.lblExportHint.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Italic)
        Me.lblExportHint.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128)
        Me.lblExportHint.Location = New System.Drawing.Point(0, 47)
        Me.lblExportHint.Size = New System.Drawing.Size(936, 18)
        Me.lblExportHint.Text = "  💡 Mẹo: Nhấn giữ phím Ctrl (hoặc Shift) và click vào dòng để bôi đen nhiều dự án rồi bấm Xuất Đang Chọn."
        Me.pnlExport.Controls.Add(Me.lblExportHint)

        ' ─ Xuất tất cả (Excel)
        Me.btnExportAllExcel.BackColor = System.Drawing.Color.FromArgb(5, 150, 105)
        Me.btnExportAllExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportAllExcel.FlatAppearance.BorderSize = 0
        Me.btnExportAllExcel.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnExportAllExcel.ForeColor = System.Drawing.Color.White
        Me.btnExportAllExcel.Location = New System.Drawing.Point(400, 2)
        Me.btnExportAllExcel.Name = "btnExportAllExcel"
        Me.btnExportAllExcel.Size = New System.Drawing.Size(230, 38)
        Me.btnExportAllExcel.Text = "📦 Xuất tất cả Excel"
        Me.btnExportAllExcel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pnlExport.Controls.Add(Me.btnExportAllExcel)

        ' ─ Xuất tất cả (PDF)
        Me.btnExportAllPDF.BackColor = System.Drawing.Color.FromArgb(185, 28, 28)
        Me.btnExportAllPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportAllPDF.FlatAppearance.BorderSize = 0
        Me.btnExportAllPDF.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnExportAllPDF.ForeColor = System.Drawing.Color.White
        Me.btnExportAllPDF.Location = New System.Drawing.Point(645, 2)
        Me.btnExportAllPDF.Name = "btnExportAllPDF"
        Me.btnExportAllPDF.Size = New System.Drawing.Size(220, 38)
        Me.btnExportAllPDF.Text = "📦 Xuất tất cả PDF"
        Me.btnExportAllPDF.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pnlExport.Controls.Add(Me.btnExportAllPDF)

        '─── FORM ───
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(243, 244, 246)
        Me.ClientSize = New System.Drawing.Size(960, 585)
        Me.MinimumSize = New System.Drawing.Size(976, 624)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.pnlFilter)
        Me.Controls.Add(Me.pnlSummary)
        Me.Controls.Add(Me.dgvReport)
        Me.Controls.Add(Me.pnlExport)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.MaximizeBox = True
        Me.Name = "frmReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AppStory – Báo Cáo Thống Kê"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlFilter.ResumeLayout(False)
        Me.pnlFilter.PerformLayout()
        Me.pnlSummary.ResumeLayout(False)
        Me.pnlSummary.PerformLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlExport.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents pnlFilter As System.Windows.Forms.Panel
    Friend WithEvents lblFilterTitle As System.Windows.Forms.Label
    Friend WithEvents rdoMonth As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDateRange As System.Windows.Forms.RadioButton
    Friend WithEvents rdoTeam As System.Windows.Forms.RadioButton
    Friend WithEvents rdoStatus As System.Windows.Forms.RadioButton
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTeam As System.Windows.Forms.Label
    Friend WithEvents cboTeam As System.Windows.Forms.ComboBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblMonth As System.Windows.Forms.Label
    Friend WithEvents cboMonth As System.Windows.Forms.ComboBox
    Friend WithEvents lblYear As System.Windows.Forms.Label
    Friend WithEvents cboYear As System.Windows.Forms.ComboBox
    Friend WithEvents btnFilter As System.Windows.Forms.Button
    Friend WithEvents pnlSummary As System.Windows.Forms.Panel
    Friend WithEvents lblSummaryTotal As System.Windows.Forms.Label
    Friend WithEvents lblSummaryActive As System.Windows.Forms.Label
    Friend WithEvents lblSummaryCompleted As System.Windows.Forms.Label
    Friend WithEvents lblSummaryOverdue As System.Windows.Forms.Label
    Friend WithEvents dgvReport As System.Windows.Forms.DataGridView
    Friend WithEvents pnlExport As System.Windows.Forms.Panel
    Friend WithEvents lblExportHint As System.Windows.Forms.Label
    Friend WithEvents btnExportExcel As System.Windows.Forms.Button
    Friend WithEvents btnExportPDF As System.Windows.Forms.Button
    Friend WithEvents btnExportAllExcel As System.Windows.Forms.Button
    Friend WithEvents btnExportAllPDF As System.Windows.Forms.Button

End Class
