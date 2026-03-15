<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDashboard
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
        Me.flpCards = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnlCardTotal = New System.Windows.Forms.Panel()
        Me.lblCardTotalCount = New System.Windows.Forms.Label()
        Me.lblCardTotalLabel = New System.Windows.Forms.Label()
        Me.pnlCardActive = New System.Windows.Forms.Panel()
        Me.lblCardActiveCount = New System.Windows.Forms.Label()
        Me.lblCardActiveLabel = New System.Windows.Forms.Label()
        Me.pnlCardCompleted = New System.Windows.Forms.Panel()
        Me.lblCardCompletedCount = New System.Windows.Forms.Label()
        Me.lblCardCompletedLabel = New System.Windows.Forms.Label()
        Me.pnlCardOverdue = New System.Windows.Forms.Panel()
        Me.lblCardOverdueCount = New System.Windows.Forms.Label()
        Me.lblCardOverdueLabel = New System.Windows.Forms.Label()
        Me.pnlCardPlanning = New System.Windows.Forms.Panel()
        Me.lblCardPlanningCount = New System.Windows.Forms.Label()
        Me.lblCardPlanningLabel = New System.Windows.Forms.Label()
        Me.lblFilterInfo = New System.Windows.Forms.Label()
        Me.dgvProjects = New System.Windows.Forms.DataGridView()
        Me.pnlTaskDetails = New System.Windows.Forms.Panel()
        Me.lblTaskDetailTitle = New System.Windows.Forms.Label()
        Me.lblTaskStats = New System.Windows.Forms.Label()
        Me.dgvTasks = New System.Windows.Forms.DataGridView()
        Me.pnlHeader.SuspendLayout()
        Me.flpCards.SuspendLayout()
        Me.pnlCardTotal.SuspendLayout()
        Me.pnlCardActive.SuspendLayout()
        Me.pnlCardCompleted.SuspendLayout()
        Me.pnlCardOverdue.SuspendLayout()
        Me.pnlCardPlanning.SuspendLayout()
        CType(Me.dgvProjects, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTaskDetails.SuspendLayout()
        CType(Me.dgvTasks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

        '--- pnlHeader ---
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(37, 99, 235)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.btnBack)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(950, 55)

        Me.lblTitle.AutoSize = False
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(15, 0)
        Me.lblTitle.Size = New System.Drawing.Size(500, 55)
        Me.lblTitle.Text = "📊 Dashboard Tổng Quan Dự Án"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

        Me.btnBack.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 30)
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Location = New System.Drawing.Point(860, 12)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(80, 32)
        Me.btnBack.Text = "← Quay lại"
        Me.btnBack.Cursor = System.Windows.Forms.Cursors.Hand

        '--- flpCards ---
        Me.flpCards.BackColor = System.Drawing.Color.Transparent
        Me.flpCards.Controls.Add(Me.pnlCardTotal)
        Me.flpCards.Controls.Add(Me.pnlCardActive)
        Me.flpCards.Controls.Add(Me.pnlCardCompleted)
        Me.flpCards.Controls.Add(Me.pnlCardOverdue)
        Me.flpCards.Controls.Add(Me.pnlCardPlanning)
        Me.flpCards.Location = New System.Drawing.Point(15, 65)
        Me.flpCards.Name = "flpCards"
        Me.flpCards.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flpCards.Size = New System.Drawing.Size(920, 120)
        Me.flpCards.WrapContents = False

        ' ===== CARD: Tổng dự án =====
        Me.pnlCardTotal.BackColor = System.Drawing.Color.FromArgb(37, 99, 235)
        Me.pnlCardTotal.Controls.Add(Me.lblCardTotalCount)
        Me.pnlCardTotal.Controls.Add(Me.lblCardTotalLabel)
        Me.pnlCardTotal.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pnlCardTotal.Margin = New System.Windows.Forms.Padding(0, 0, 12, 0)
        Me.pnlCardTotal.Name = "pnlCardTotal"
        Me.pnlCardTotal.Size = New System.Drawing.Size(172, 110)
        Me.pnlCardTotal.Tag = "Total"

        Me.lblCardTotalCount.AutoSize = False
        Me.lblCardTotalCount.Font = New System.Drawing.Font("Segoe UI", 28.0!, System.Drawing.FontStyle.Bold)
        Me.lblCardTotalCount.ForeColor = System.Drawing.Color.White
        Me.lblCardTotalCount.Location = New System.Drawing.Point(10, 10)
        Me.lblCardTotalCount.Size = New System.Drawing.Size(152, 50)
        Me.lblCardTotalCount.Text = "0"
        Me.lblCardTotalCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        Me.lblCardTotalLabel.AutoSize = False
        Me.lblCardTotalLabel.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblCardTotalLabel.ForeColor = System.Drawing.Color.FromArgb(200, 220, 255)
        Me.lblCardTotalLabel.Location = New System.Drawing.Point(10, 65)
        Me.lblCardTotalLabel.Size = New System.Drawing.Size(152, 35)
        Me.lblCardTotalLabel.Text = "📁 Tổng Dự Án"
        Me.lblCardTotalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        ' ===== CARD: Đang thực hiện (Active) =====
        Me.pnlCardActive.BackColor = System.Drawing.Color.FromArgb(245, 158, 11)
        Me.pnlCardActive.Controls.Add(Me.lblCardActiveCount)
        Me.pnlCardActive.Controls.Add(Me.lblCardActiveLabel)
        Me.pnlCardActive.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pnlCardActive.Margin = New System.Windows.Forms.Padding(0, 0, 12, 0)
        Me.pnlCardActive.Name = "pnlCardActive"
        Me.pnlCardActive.Size = New System.Drawing.Size(172, 110)
        Me.pnlCardActive.Tag = "Active"

        Me.lblCardActiveCount.AutoSize = False
        Me.lblCardActiveCount.Font = New System.Drawing.Font("Segoe UI", 28.0!, System.Drawing.FontStyle.Bold)
        Me.lblCardActiveCount.ForeColor = System.Drawing.Color.White
        Me.lblCardActiveCount.Location = New System.Drawing.Point(10, 10)
        Me.lblCardActiveCount.Size = New System.Drawing.Size(152, 50)
        Me.lblCardActiveCount.Text = "0"
        Me.lblCardActiveCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        Me.lblCardActiveLabel.AutoSize = False
        Me.lblCardActiveLabel.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblCardActiveLabel.ForeColor = System.Drawing.Color.FromArgb(255, 240, 200)
        Me.lblCardActiveLabel.Location = New System.Drawing.Point(10, 65)
        Me.lblCardActiveLabel.Size = New System.Drawing.Size(152, 35)
        Me.lblCardActiveLabel.Text = "🔄 Đang Thực Hiện"
        Me.lblCardActiveLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        ' ===== CARD: Hoàn thành (Completed) =====
        Me.pnlCardCompleted.BackColor = System.Drawing.Color.FromArgb(16, 185, 129)
        Me.pnlCardCompleted.Controls.Add(Me.lblCardCompletedCount)
        Me.pnlCardCompleted.Controls.Add(Me.lblCardCompletedLabel)
        Me.pnlCardCompleted.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pnlCardCompleted.Margin = New System.Windows.Forms.Padding(0, 0, 12, 0)
        Me.pnlCardCompleted.Name = "pnlCardCompleted"
        Me.pnlCardCompleted.Size = New System.Drawing.Size(172, 110)
        Me.pnlCardCompleted.Tag = "Completed"

        Me.lblCardCompletedCount.AutoSize = False
        Me.lblCardCompletedCount.Font = New System.Drawing.Font("Segoe UI", 28.0!, System.Drawing.FontStyle.Bold)
        Me.lblCardCompletedCount.ForeColor = System.Drawing.Color.White
        Me.lblCardCompletedCount.Location = New System.Drawing.Point(10, 10)
        Me.lblCardCompletedCount.Size = New System.Drawing.Size(152, 50)
        Me.lblCardCompletedCount.Text = "0"
        Me.lblCardCompletedCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        Me.lblCardCompletedLabel.AutoSize = False
        Me.lblCardCompletedLabel.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblCardCompletedLabel.ForeColor = System.Drawing.Color.FromArgb(200, 255, 220)
        Me.lblCardCompletedLabel.Location = New System.Drawing.Point(10, 65)
        Me.lblCardCompletedLabel.Size = New System.Drawing.Size(152, 35)
        Me.lblCardCompletedLabel.Text = "✅ Hoàn Thành"
        Me.lblCardCompletedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        ' ===== CARD: Quá deadline (Overdue) =====
        Me.pnlCardOverdue.BackColor = System.Drawing.Color.FromArgb(220, 38, 38)
        Me.pnlCardOverdue.Controls.Add(Me.lblCardOverdueCount)
        Me.pnlCardOverdue.Controls.Add(Me.lblCardOverdueLabel)
        Me.pnlCardOverdue.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pnlCardOverdue.Margin = New System.Windows.Forms.Padding(0, 0, 12, 0)
        Me.pnlCardOverdue.Name = "pnlCardOverdue"
        Me.pnlCardOverdue.Size = New System.Drawing.Size(172, 110)
        Me.pnlCardOverdue.Tag = "Overdue"

        Me.lblCardOverdueCount.AutoSize = False
        Me.lblCardOverdueCount.Font = New System.Drawing.Font("Segoe UI", 28.0!, System.Drawing.FontStyle.Bold)
        Me.lblCardOverdueCount.ForeColor = System.Drawing.Color.White
        Me.lblCardOverdueCount.Location = New System.Drawing.Point(10, 10)
        Me.lblCardOverdueCount.Size = New System.Drawing.Size(152, 50)
        Me.lblCardOverdueCount.Text = "0"
        Me.lblCardOverdueCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        Me.lblCardOverdueLabel.AutoSize = False
        Me.lblCardOverdueLabel.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblCardOverdueLabel.ForeColor = System.Drawing.Color.FromArgb(255, 200, 200)
        Me.lblCardOverdueLabel.Location = New System.Drawing.Point(10, 65)
        Me.lblCardOverdueLabel.Size = New System.Drawing.Size(152, 35)
        Me.lblCardOverdueLabel.Text = "⚠️ Quá Deadline"
        Me.lblCardOverdueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        ' ===== CARD: Chưa bắt đầu (Planning) =====
        Me.pnlCardPlanning.BackColor = System.Drawing.Color.FromArgb(107, 114, 128)
        Me.pnlCardPlanning.Controls.Add(Me.lblCardPlanningCount)
        Me.pnlCardPlanning.Controls.Add(Me.lblCardPlanningLabel)
        Me.pnlCardPlanning.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pnlCardPlanning.Margin = New System.Windows.Forms.Padding(0, 0, 12, 0)
        Me.pnlCardPlanning.Name = "pnlCardPlanning"
        Me.pnlCardPlanning.Size = New System.Drawing.Size(172, 110)
        Me.pnlCardPlanning.Tag = "Planning"

        Me.lblCardPlanningCount.AutoSize = False
        Me.lblCardPlanningCount.Font = New System.Drawing.Font("Segoe UI", 28.0!, System.Drawing.FontStyle.Bold)
        Me.lblCardPlanningCount.ForeColor = System.Drawing.Color.White
        Me.lblCardPlanningCount.Location = New System.Drawing.Point(10, 10)
        Me.lblCardPlanningCount.Size = New System.Drawing.Size(152, 50)
        Me.lblCardPlanningCount.Text = "0"
        Me.lblCardPlanningCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        Me.lblCardPlanningLabel.AutoSize = False
        Me.lblCardPlanningLabel.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblCardPlanningLabel.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220)
        Me.lblCardPlanningLabel.Location = New System.Drawing.Point(10, 65)
        Me.lblCardPlanningLabel.Size = New System.Drawing.Size(152, 35)
        Me.lblCardPlanningLabel.Text = "📝 Chưa Bắt Đầu"
        Me.lblCardPlanningLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        '--- lblFilterInfo ---
        Me.lblFilterInfo.AutoSize = False
        Me.lblFilterInfo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblFilterInfo.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235)
        Me.lblFilterInfo.Location = New System.Drawing.Point(15, 195)
        Me.lblFilterInfo.Name = "lblFilterInfo"
        Me.lblFilterInfo.Size = New System.Drawing.Size(920, 25)
        Me.lblFilterInfo.Text = "📋 Hiển thị: Tất cả dự án"
        Me.lblFilterInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)

        '--- dgvProjects ---
        Me.dgvProjects.BackgroundColor = System.Drawing.Color.White
        Me.dgvProjects.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvProjects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvProjects.ColumnHeadersDefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {
            .BackColor = System.Drawing.Color.FromArgb(37, 99, 235),
            .ForeColor = System.Drawing.Color.White,
            .Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        }
        Me.dgvProjects.ColumnHeadersHeight = 35
        Me.dgvProjects.DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {
            .Font = New System.Drawing.Font("Segoe UI", 9.5!),
            .SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254),
            .SelectionForeColor = System.Drawing.Color.Black
        }
        Me.dgvProjects.Location = New System.Drawing.Point(15, 225)
        Me.dgvProjects.Name = "dgvProjects"
        Me.dgvProjects.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvProjects.RowHeadersVisible = False
        Me.dgvProjects.RowTemplate.Height = 30
        Me.dgvProjects.Size = New System.Drawing.Size(920, 180)
        Me.dgvProjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvProjects.ReadOnly = True
        Me.dgvProjects.AllowUserToAddRows = False

        '--- pnlTaskDetails ---
        Me.pnlTaskDetails.BackColor = System.Drawing.Color.White
        Me.pnlTaskDetails.Controls.Add(Me.lblTaskDetailTitle)
        Me.pnlTaskDetails.Controls.Add(Me.lblTaskStats)
        Me.pnlTaskDetails.Controls.Add(Me.dgvTasks)
        Me.pnlTaskDetails.Location = New System.Drawing.Point(15, 415)
        Me.pnlTaskDetails.Name = "pnlTaskDetails"
        Me.pnlTaskDetails.Size = New System.Drawing.Size(920, 150)
        Me.pnlTaskDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)

        Me.lblTaskDetailTitle.AutoSize = True
        Me.lblTaskDetailTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblTaskDetailTitle.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235)
        Me.lblTaskDetailTitle.Location = New System.Drawing.Point(5, 5)
        Me.lblTaskDetailTitle.Text = "📌 CHI TIẾT CÔNG VIỆC CỦA DỰ ÁN"

        Me.lblTaskStats.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTaskStats.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular)
        Me.lblTaskStats.Location = New System.Drawing.Point(400, 5)
        Me.lblTaskStats.Size = New System.Drawing.Size(515, 20)
        Me.lblTaskStats.Text = "Chọn dự án để xem thống kê..."
        Me.lblTaskStats.TextAlign = System.Drawing.ContentAlignment.TopRight

        Me.dgvTasks.BackgroundColor = System.Drawing.Color.White
        Me.dgvTasks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTasks.Location = New System.Drawing.Point(5, 30)
        Me.dgvTasks.Name = "dgvTasks"
        Me.dgvTasks.Size = New System.Drawing.Size(910, 115)
        Me.dgvTasks.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvTasks.RowHeadersVisible = False
        Me.dgvTasks.ReadOnly = True
        Me.dgvTasks.AllowUserToAddRows = False

        '--- frmDashboard ---
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(243, 244, 246)
        Me.ClientSize = New System.Drawing.Size(950, 580)
        Me.MinimumSize = New System.Drawing.Size(966, 619)
        Me.Controls.Add(Me.pnlTaskDetails)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.flpCards)
        Me.Controls.Add(Me.lblFilterInfo)
        Me.Controls.Add(Me.dgvProjects)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.MaximizeBox = True
        Me.Name = "frmDashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AppStory – Dashboard Tổng Quan"
        Me.pnlHeader.ResumeLayout(False)
        Me.flpCards.ResumeLayout(False)
        Me.pnlCardTotal.ResumeLayout(False)
        Me.pnlCardActive.ResumeLayout(False)
        Me.pnlCardCompleted.ResumeLayout(False)
        Me.pnlCardOverdue.ResumeLayout(False)
        Me.pnlCardPlanning.ResumeLayout(False)
        CType(Me.dgvProjects, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTaskDetails.ResumeLayout(False)
        Me.pnlTaskDetails.PerformLayout()
        CType(Me.dgvTasks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents flpCards As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnlCardTotal As System.Windows.Forms.Panel
    Friend WithEvents lblCardTotalCount As System.Windows.Forms.Label
    Friend WithEvents lblCardTotalLabel As System.Windows.Forms.Label
    Friend WithEvents pnlCardActive As System.Windows.Forms.Panel
    Friend WithEvents lblCardActiveCount As System.Windows.Forms.Label
    Friend WithEvents lblCardActiveLabel As System.Windows.Forms.Label
    Friend WithEvents pnlCardCompleted As System.Windows.Forms.Panel
    Friend WithEvents lblCardCompletedCount As System.Windows.Forms.Label
    Friend WithEvents lblCardCompletedLabel As System.Windows.Forms.Label
    Friend WithEvents pnlCardOverdue As System.Windows.Forms.Panel
    Friend WithEvents lblCardOverdueCount As System.Windows.Forms.Label
    Friend WithEvents lblCardOverdueLabel As System.Windows.Forms.Label
    Friend WithEvents pnlCardPlanning As System.Windows.Forms.Panel
    Friend WithEvents lblCardPlanningCount As System.Windows.Forms.Label
    Friend WithEvents lblCardPlanningLabel As System.Windows.Forms.Label
    Friend WithEvents lblFilterInfo As System.Windows.Forms.Label
    Friend WithEvents dgvProjects As System.Windows.Forms.DataGridView
    Friend WithEvents pnlTaskDetails As System.Windows.Forms.Panel
    Friend WithEvents lblTaskDetailTitle As System.Windows.Forms.Label
    Friend WithEvents lblTaskStats As System.Windows.Forms.Label
    Friend WithEvents dgvTasks As System.Windows.Forms.DataGridView

End Class
