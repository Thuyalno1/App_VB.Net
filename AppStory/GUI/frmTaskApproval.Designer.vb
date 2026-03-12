<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTaskApproval
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
        Me.dgvPendingTasks = New System.Windows.Forms.DataGridView()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.pnlPagination = New System.Windows.Forms.Panel()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.lblPageInfo = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        CType(Me.dgvPendingTasks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        
        '--- pnlHeader ---
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(16, 185, 129)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.btnBack)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Size = New System.Drawing.Size(800, 60)
        
        '--- lblTitle ---
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(20, 15)
        Me.lblTitle.Text = "Duyệt Công Việc"
        
        '--- btnBack ---
        Me.btnBack.BackColor = System.Drawing.Color.FromArgb(99, 102, 241)
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Location = New System.Drawing.Point(680, 12)
        Me.btnBack.Size = New System.Drawing.Size(100, 36)
        Me.btnBack.Text = "← Quay lại"
        Me.btnBack.Cursor = System.Windows.Forms.Cursors.Hand

        '--- lblCount ---
        Me.lblCount.AutoSize = True
        Me.lblCount.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblCount.Location = New System.Drawing.Point(20, 80)
        Me.lblCount.Text = "Đang tải dữ liệu..."

        '--- dgvPendingTasks ---
        Me.dgvPendingTasks.AllowUserToAddRows = False
        Me.dgvPendingTasks.AllowUserToDeleteRows = False
        Me.dgvPendingTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvPendingTasks.BackgroundColor = System.Drawing.Color.White
        Me.dgvPendingTasks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvPendingTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPendingTasks.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPendingTasks.Location = New System.Drawing.Point(20, 110)
        Me.dgvPendingTasks.Size = New System.Drawing.Size(760, 275)
        Me.dgvPendingTasks.ReadOnly = True

        '--- pnlPagination ---
        Me.pnlPagination.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlPagination.Controls.Add(Me.btnPrev)
        Me.pnlPagination.Controls.Add(Me.lblPageInfo)
        Me.pnlPagination.Controls.Add(Me.btnNext)
        Me.pnlPagination.Location = New System.Drawing.Point(20, 395)
        Me.pnlPagination.Name = "pnlPagination"
        Me.pnlPagination.Size = New System.Drawing.Size(300, 40)

        Me.btnPrev.BackColor = System.Drawing.Color.FromArgb(107, 114, 128)
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

        Me.btnNext.BackColor = System.Drawing.Color.FromArgb(107, 114, 128)
        Me.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNext.FlatAppearance.BorderSize = 0
        Me.btnNext.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnNext.ForeColor = System.Drawing.Color.White
        Me.btnNext.Location = New System.Drawing.Point(190, 5)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(80, 28)
        Me.btnNext.Text = "Sau →"
        Me.btnNext.Cursor = System.Windows.Forms.Cursors.Hand

        '--- frmTaskApproval ---
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(249, 250, 251)
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.MinimumSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.dgvPendingTasks)
        Me.Controls.Add(Me.pnlPagination)
        Me.Name = "frmTaskApproval"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Duyệt Công Việc"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.dgvPendingTasks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents lblCount As System.Windows.Forms.Label
    Friend WithEvents dgvPendingTasks As System.Windows.Forms.DataGridView
    Friend WithEvents pnlPagination As System.Windows.Forms.Panel
    Friend WithEvents btnPrev As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents lblPageInfo As System.Windows.Forms.Label
End Class
