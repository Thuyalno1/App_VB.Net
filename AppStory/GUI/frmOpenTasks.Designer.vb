<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmOpenTasks
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
        Me.dgvOpenTasks = New System.Windows.Forms.DataGridView()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.pnlPagination = New System.Windows.Forms.Panel()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.lblPageInfo = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        CType(Me.dgvOpenTasks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(179, Byte), Integer), CType(CType(8, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.btnBack)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(800, 60)
        Me.pnlHeader.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(20, 15)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(201, 30)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "📥 Việc Cần Nhận"
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.btnBack.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Location = New System.Drawing.Point(680, 12)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(100, 35)
        Me.btnBack.TabIndex = 1
        Me.btnBack.Text = "← Quay lại"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'dgvOpenTasks
        '
        Me.dgvOpenTasks.AllowUserToAddRows = False
        Me.dgvOpenTasks.AllowUserToDeleteRows = False
        Me.dgvOpenTasks.BackgroundColor = System.Drawing.Color.White
        Me.dgvOpenTasks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvOpenTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvOpenTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOpenTasks.Location = New System.Drawing.Point(20, 120)
        Me.dgvOpenTasks.Name = "dgvOpenTasks"
        Me.dgvOpenTasks.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvOpenTasks.ReadOnly = True
        Me.dgvOpenTasks.RowHeadersVisible = False
        Me.dgvOpenTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvOpenTasks.Size = New System.Drawing.Size(760, 260)
        Me.dgvOpenTasks.TabIndex = 1

        '--- pnlPagination ---
        Me.pnlPagination.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlPagination.Controls.Add(Me.btnPrev)
        Me.pnlPagination.Controls.Add(Me.lblPageInfo)
        Me.pnlPagination.Controls.Add(Me.btnNext)
        Me.pnlPagination.Location = New System.Drawing.Point(20, 390)
        Me.pnlPagination.Name = "pnlPagination"
        Me.pnlPagination.Size = New System.Drawing.Size(300, 40)

        Me.btnPrev.BackColor = System.Drawing.Color.FromArgb(71, 100, 130)
        Me.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrev.FlatAppearance.BorderSize = 0
        Me.btnPrev.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnPrev.ForeColor = System.Drawing.Color.White
        Me.btnPrev.Location = New System.Drawing.Point(0, 5)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(80, 28)
        Me.btnPrev.Text = "← Trước"
        Me.btnPrev.Cursor = System.Windows.Forms.Cursors.Hand

        Me.lblPageInfo.AutoSize = False
        Me.lblPageInfo.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblPageInfo.Location = New System.Drawing.Point(85, 5)
        Me.lblPageInfo.Size = New System.Drawing.Size(100, 28)
        Me.lblPageInfo.Text = "Trang 1 / 1"
        Me.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        Me.btnNext.BackColor = System.Drawing.Color.FromArgb(71, 100, 130)
        Me.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNext.FlatAppearance.BorderSize = 0
        Me.btnNext.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnNext.ForeColor = System.Drawing.Color.White
        Me.btnNext.Location = New System.Drawing.Point(190, 5)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(80, 28)
        Me.btnNext.Text = "Sau →"
        Me.btnNext.Cursor = System.Windows.Forms.Cursors.Hand
        '
        'lblInfo
        '
        Me.lblInfo.AutoSize = True
        Me.lblInfo.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblInfo.Location = New System.Drawing.Point(20, 85)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(425, 19)
        Me.lblInfo.TabIndex = 2
        Me.lblInfo.Text = "Đây là danh sách công việc giao cho nhóm của bạn nhưng chưa có ai làm."
        '
        'frmOpenTasks
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.dgvOpenTasks)
        Me.Controls.Add(Me.pnlPagination)
        Me.Controls.Add(Me.pnlHeader)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.MinimumSize = New System.Drawing.Size(816, 489)
        Me.MaximizeBox = True
        Me.Name = "frmOpenTasks"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AppStory - Việc Cần Nhận"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.dgvOpenTasks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents dgvOpenTasks As System.Windows.Forms.DataGridView
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents pnlPagination As System.Windows.Forms.Panel
    Friend WithEvents btnPrev As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents lblPageInfo As System.Windows.Forms.Label
End Class

