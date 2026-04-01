<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEmployeeDashboard
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
        Me.lblUserInfo = New System.Windows.Forms.Label()
        Me.flpCards = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnlCardAssigned = New System.Windows.Forms.Panel()
        Me.lblCardAssignedCount = New System.Windows.Forms.Label()
        Me.lblCardAssignedLabel = New System.Windows.Forms.Label()
        Me.pnlCardInProgress = New System.Windows.Forms.Panel()
        Me.lblCardInProgressCount = New System.Windows.Forms.Label()
        Me.lblCardInProgressLabel = New System.Windows.Forms.Label()
        Me.pnlCardDone = New System.Windows.Forms.Panel()
        Me.lblCardDoneCount = New System.Windows.Forms.Label()
        Me.lblCardDoneLabel = New System.Windows.Forms.Label()
        Me.pnlCardDeadline = New System.Windows.Forms.Panel()
        Me.lblCardDeadlineCount = New System.Windows.Forms.Label()
        Me.lblCardDeadlineLabel = New System.Windows.Forms.Label()
        Me.lblFilterInfo = New System.Windows.Forms.Label()
        Me.dgvTasks = New System.Windows.Forms.DataGridView()
        Me.pnlHeader.SuspendLayout()
        Me.flpCards.SuspendLayout()
        Me.pnlCardAssigned.SuspendLayout()
        Me.pnlCardInProgress.SuspendLayout()
        Me.pnlCardDone.SuspendLayout()
        Me.pnlCardDeadline.SuspendLayout()
        CType(Me.dgvTasks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

        '─── HEADER ───
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(14, 165, 160)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.btnBack)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(900, 55)

        Me.lblTitle.AutoSize = False
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(15, 0)
        Me.lblTitle.Size = New System.Drawing.Size(500, 55)
        Me.lblTitle.Text = "📋 Dashboard Công Việc Cá Nhân"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Location = New System.Drawing.Point(810, 12)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(80, 32)
        Me.btnBack.Text = "← Quay lại"
        Me.btnBack.Cursor = System.Windows.Forms.Cursors.Hand

        '─── lblUserInfo ───
        Me.lblUserInfo.AutoSize = False
        Me.lblUserInfo.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.lblUserInfo.ForeColor = System.Drawing.Color.FromArgb(14, 165, 160)
        Me.lblUserInfo.Location = New System.Drawing.Point(15, 62)
        Me.lblUserInfo.Size = New System.Drawing.Size(870, 25)
        Me.lblUserInfo.Text = "Xin chào, ..."

        '─── CARDS ───
        Me.flpCards.BackColor = System.Drawing.Color.Transparent
        Me.flpCards.Controls.Add(Me.pnlCardAssigned)
        Me.flpCards.Controls.Add(Me.pnlCardInProgress)
        Me.flpCards.Controls.Add(Me.pnlCardDone)
        Me.flpCards.Controls.Add(Me.pnlCardDeadline)
        Me.flpCards.Location = New System.Drawing.Point(15, 92)
        Me.flpCards.Name = "flpCards"
        Me.flpCards.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flpCards.Size = New System.Drawing.Size(870, 120)
        Me.flpCards.WrapContents = False

        ' Card: Task được giao (Tổng)
        Me.pnlCardAssigned.BackColor = System.Drawing.Color.FromArgb(30, 58, 95)
        Me.pnlCardAssigned.Controls.Add(Me.lblCardAssignedCount)
        Me.pnlCardAssigned.Controls.Add(Me.lblCardAssignedLabel)
        Me.pnlCardAssigned.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pnlCardAssigned.Margin = New System.Windows.Forms.Padding(0, 0, 12, 0)
        Me.pnlCardAssigned.Name = "pnlCardAssigned"
        Me.pnlCardAssigned.Size = New System.Drawing.Size(205, 110)
        Me.pnlCardAssigned.Tag = "Assigned"

        Me.lblCardAssignedCount.AutoSize = False
        Me.lblCardAssignedCount.Font = New System.Drawing.Font("Segoe UI", 28.0!, System.Drawing.FontStyle.Bold)
        Me.lblCardAssignedCount.ForeColor = System.Drawing.Color.White
        Me.lblCardAssignedCount.Location = New System.Drawing.Point(10, 10)
        Me.lblCardAssignedCount.Size = New System.Drawing.Size(185, 50)
        Me.lblCardAssignedCount.Text = "0"
        Me.lblCardAssignedCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        Me.lblCardAssignedLabel.AutoSize = False
        Me.lblCardAssignedLabel.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblCardAssignedLabel.ForeColor = System.Drawing.Color.FromArgb(200, 220, 255)
        Me.lblCardAssignedLabel.Location = New System.Drawing.Point(10, 65)
        Me.lblCardAssignedLabel.Size = New System.Drawing.Size(185, 35)
        Me.lblCardAssignedLabel.Text = "📋 Tổng Được Giao"
        Me.lblCardAssignedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        ' Card: Đang làm (1-89%)
        Me.pnlCardInProgress.BackColor = System.Drawing.Color.FromArgb(14, 165, 160)
        Me.pnlCardInProgress.Controls.Add(Me.lblCardInProgressCount)
        Me.pnlCardInProgress.Controls.Add(Me.lblCardInProgressLabel)
        Me.pnlCardInProgress.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pnlCardInProgress.Margin = New System.Windows.Forms.Padding(0, 0, 12, 0)
        Me.pnlCardInProgress.Name = "pnlCardInProgress"
        Me.pnlCardInProgress.Size = New System.Drawing.Size(205, 110)
        Me.pnlCardInProgress.Tag = "InProgress"

        Me.lblCardInProgressCount.AutoSize = False
        Me.lblCardInProgressCount.Font = New System.Drawing.Font("Segoe UI", 28.0!, System.Drawing.FontStyle.Bold)
        Me.lblCardInProgressCount.ForeColor = System.Drawing.Color.White
        Me.lblCardInProgressCount.Location = New System.Drawing.Point(10, 10)
        Me.lblCardInProgressCount.Size = New System.Drawing.Size(185, 50)
        Me.lblCardInProgressCount.Text = "0"
        Me.lblCardInProgressCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        Me.lblCardInProgressLabel.AutoSize = False
        Me.lblCardInProgressLabel.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblCardInProgressLabel.ForeColor = System.Drawing.Color.FromArgb(255, 240, 200)
        Me.lblCardInProgressLabel.Location = New System.Drawing.Point(10, 65)
        Me.lblCardInProgressLabel.Size = New System.Drawing.Size(185, 35)
        Me.lblCardInProgressLabel.Text = "🔄 Đang Thực Hiện"
        Me.lblCardInProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        ' Card: Hoàn thành (100%)
        Me.pnlCardDone.BackColor = System.Drawing.Color.FromArgb(14, 165, 160)
        Me.pnlCardDone.Controls.Add(Me.lblCardDoneCount)
        Me.pnlCardDone.Controls.Add(Me.lblCardDoneLabel)
        Me.pnlCardDone.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pnlCardDone.Margin = New System.Windows.Forms.Padding(0, 0, 12, 0)
        Me.pnlCardDone.Name = "pnlCardDone"
        Me.pnlCardDone.Size = New System.Drawing.Size(205, 110)
        Me.pnlCardDone.Tag = "Done"

        Me.lblCardDoneCount.AutoSize = False
        Me.lblCardDoneCount.Font = New System.Drawing.Font("Segoe UI", 28.0!, System.Drawing.FontStyle.Bold)
        Me.lblCardDoneCount.ForeColor = System.Drawing.Color.White
        Me.lblCardDoneCount.Location = New System.Drawing.Point(10, 10)
        Me.lblCardDoneCount.Size = New System.Drawing.Size(185, 50)
        Me.lblCardDoneCount.Text = "0"
        Me.lblCardDoneCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        Me.lblCardDoneLabel.AutoSize = False
        Me.lblCardDoneLabel.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblCardDoneLabel.ForeColor = System.Drawing.Color.FromArgb(200, 255, 220)
        Me.lblCardDoneLabel.Location = New System.Drawing.Point(10, 65)
        Me.lblCardDoneLabel.Size = New System.Drawing.Size(185, 35)
        Me.lblCardDoneLabel.Text = "✅ Hoàn Thành"
        Me.lblCardDoneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        ' Card: Gần deadline (trong 3 ngày)
        Me.pnlCardDeadline.BackColor = System.Drawing.Color.FromArgb(229, 62, 62)
        Me.pnlCardDeadline.Controls.Add(Me.lblCardDeadlineCount)
        Me.pnlCardDeadline.Controls.Add(Me.lblCardDeadlineLabel)
        Me.pnlCardDeadline.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pnlCardDeadline.Margin = New System.Windows.Forms.Padding(0, 0, 12, 0)
        Me.pnlCardDeadline.Name = "pnlCardDeadline"
        Me.pnlCardDeadline.Size = New System.Drawing.Size(205, 110)
        Me.pnlCardDeadline.Tag = "Deadline"

        Me.lblCardDeadlineCount.AutoSize = False
        Me.lblCardDeadlineCount.Font = New System.Drawing.Font("Segoe UI", 28.0!, System.Drawing.FontStyle.Bold)
        Me.lblCardDeadlineCount.ForeColor = System.Drawing.Color.White
        Me.lblCardDeadlineCount.Location = New System.Drawing.Point(10, 10)
        Me.lblCardDeadlineCount.Size = New System.Drawing.Size(185, 50)
        Me.lblCardDeadlineCount.Text = "0"
        Me.lblCardDeadlineCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        Me.lblCardDeadlineLabel.AutoSize = False
        Me.lblCardDeadlineLabel.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblCardDeadlineLabel.ForeColor = System.Drawing.Color.FromArgb(255, 200, 200)
        Me.lblCardDeadlineLabel.Location = New System.Drawing.Point(10, 65)
        Me.lblCardDeadlineLabel.Size = New System.Drawing.Size(185, 35)
        Me.lblCardDeadlineLabel.Text = "⚠️ Gần Deadline"
        Me.lblCardDeadlineLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        '─── lblFilterInfo ───
        Me.lblFilterInfo.AutoSize = False
        Me.lblFilterInfo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblFilterInfo.ForeColor = System.Drawing.Color.FromArgb(14, 165, 160)
        Me.lblFilterInfo.Location = New System.Drawing.Point(15, 218)
        Me.lblFilterInfo.Name = "lblFilterInfo"
        Me.lblFilterInfo.Size = New System.Drawing.Size(870, 25)
        Me.lblFilterInfo.Text = "📋 Hiển thị: Tất cả công việc"
        Me.lblFilterInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)

        '─── dgvTasks ───
        Me.dgvTasks.BackgroundColor = System.Drawing.Color.White
        Me.dgvTasks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTasks.ColumnHeadersDefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {
            .BackColor = System.Drawing.Color.FromArgb(14, 165, 160),
            .ForeColor = System.Drawing.Color.White,
            .Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        }
        Me.dgvTasks.ColumnHeadersHeight = 35
        Me.dgvTasks.DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {
            .Font = New System.Drawing.Font("Segoe UI", 9.5!),
            .SelectionBackColor = System.Drawing.Color.FromArgb(209, 250, 229),
            .SelectionForeColor = System.Drawing.Color.Black
        }
        Me.dgvTasks.Location = New System.Drawing.Point(15, 248)
        Me.dgvTasks.Name = "dgvTasks"
        Me.dgvTasks.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvTasks.RowHeadersVisible = False
        Me.dgvTasks.RowTemplate.Height = 30
        Me.dgvTasks.Size = New System.Drawing.Size(870, 320)
        Me.dgvTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTasks.ReadOnly = True
        Me.dgvTasks.AllowUserToAddRows = False

        '─── frmEmployeeDashboard ───
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(240, 244, 248)
        Me.ClientSize = New System.Drawing.Size(900, 580)
        Me.MinimumSize = New System.Drawing.Size(916, 619)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblUserInfo)
        Me.Controls.Add(Me.flpCards)
        Me.Controls.Add(Me.lblFilterInfo)
        Me.Controls.Add(Me.dgvTasks)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.MaximizeBox = True
        Me.Name = "frmEmployeeDashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AppStory – Dashboard Cá Nhân"
        Me.pnlHeader.ResumeLayout(False)
        Me.flpCards.ResumeLayout(False)
        Me.pnlCardAssigned.ResumeLayout(False)
        Me.pnlCardInProgress.ResumeLayout(False)
        Me.pnlCardDone.ResumeLayout(False)
        Me.pnlCardDeadline.ResumeLayout(False)
        CType(Me.dgvTasks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents lblUserInfo As System.Windows.Forms.Label
    Friend WithEvents flpCards As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnlCardAssigned As System.Windows.Forms.Panel
    Friend WithEvents lblCardAssignedCount As System.Windows.Forms.Label
    Friend WithEvents lblCardAssignedLabel As System.Windows.Forms.Label
    Friend WithEvents pnlCardInProgress As System.Windows.Forms.Panel
    Friend WithEvents lblCardInProgressCount As System.Windows.Forms.Label
    Friend WithEvents lblCardInProgressLabel As System.Windows.Forms.Label
    Friend WithEvents pnlCardDone As System.Windows.Forms.Panel
    Friend WithEvents lblCardDoneCount As System.Windows.Forms.Label
    Friend WithEvents lblCardDoneLabel As System.Windows.Forms.Label
    Friend WithEvents pnlCardDeadline As System.Windows.Forms.Panel
    Friend WithEvents lblCardDeadlineCount As System.Windows.Forms.Label
    Friend WithEvents lblCardDeadlineLabel As System.Windows.Forms.Label
    Friend WithEvents lblFilterInfo As System.Windows.Forms.Label
    Friend WithEvents dgvTasks As System.Windows.Forms.DataGridView

End Class

