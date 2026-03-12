<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMyTasks
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
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.lblUserInfo = New System.Windows.Forms.Label()
        Me.dgvMyTasks = New System.Windows.Forms.DataGridView()
        Me.pnlStatusUpdate = New System.Windows.Forms.Panel()
        Me.lblSelectedTask = New System.Windows.Forms.Label()
        Me.lblNewStatus = New System.Windows.Forms.Label()
        Me.cboNewStatus = New System.Windows.Forms.ComboBox()
        Me.btnConfirmStatus = New System.Windows.Forms.Button()
        Me.pnlPagination = New System.Windows.Forms.Panel()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.lblPageInfo = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        CType(Me.dgvMyTasks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlStatusUpdate.SuspendLayout()
        Me.pnlPagination.SuspendLayout()
        Me.SuspendLayout()

        '--- pnlHeader ---
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(16, 185, 129)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Controls.Add(Me.btnBack)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Size = New System.Drawing.Size(800, 55)

        Me.lblHeader.AutoSize = False
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(15, 0)
        Me.lblHeader.Size = New System.Drawing.Size(400, 55)
        Me.lblHeader.Text = "Công Việc Của Tôi"
        Me.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Location = New System.Drawing.Point(710, 12)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Size = New System.Drawing.Size(80, 32)
        Me.btnBack.Text = "← Quay lại"
        Me.btnBack.Cursor = System.Windows.Forms.Cursors.Hand

        '--- lblUserInfo ---
        Me.lblUserInfo.AutoSize = False
        Me.lblUserInfo.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.lblUserInfo.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129)
        Me.lblUserInfo.Location = New System.Drawing.Point(10, 62)
        Me.lblUserInfo.Size = New System.Drawing.Size(780, 28)
        Me.lblUserInfo.Text = "Công việc của: ..."

        '--- dgvMyTasks ---
        Me.dgvMyTasks.BackgroundColor = System.Drawing.Color.White
        Me.dgvMyTasks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvMyTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvMyTasks.ColumnHeadersDefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {
            .BackColor = System.Drawing.Color.FromArgb(16, 185, 129),
            .ForeColor = System.Drawing.Color.White,
            .Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        }
        Me.dgvMyTasks.ColumnHeadersHeight = 35
        Me.dgvMyTasks.DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {
            .Font = New System.Drawing.Font("Segoe UI", 9.5!),
            .SelectionBackColor = System.Drawing.Color.FromArgb(209, 250, 229),
            .SelectionForeColor = System.Drawing.Color.Black
        }
        Me.dgvMyTasks.Location = New System.Drawing.Point(10, 95)
        Me.dgvMyTasks.Name = "dgvMyTasks"
        Me.dgvMyTasks.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMyTasks.RowHeadersVisible = False
        Me.dgvMyTasks.RowTemplate.Height = 30
        Me.dgvMyTasks.Size = New System.Drawing.Size(780, 330)

        '--- pnlPagination ---
        Me.pnlPagination.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlPagination.Controls.Add(Me.btnPrev)
        Me.pnlPagination.Controls.Add(Me.lblPageInfo)
        Me.pnlPagination.Controls.Add(Me.btnNext)
        Me.pnlPagination.Location = New System.Drawing.Point(10, 430)
        Me.pnlPagination.Name = "pnlPagination"
        Me.pnlPagination.Size = New System.Drawing.Size(300, 35)

        Me.btnPrev.BackColor = System.Drawing.Color.FromArgb(107, 114, 128)
        Me.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrev.FlatAppearance.BorderSize = 0
        Me.btnPrev.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnPrev.ForeColor = System.Drawing.Color.White
        Me.btnPrev.Location = New System.Drawing.Point(0, 2)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(80, 28)
        Me.btnPrev.Text = "← Trước"
        Me.btnPrev.Cursor = System.Windows.Forms.Cursors.Hand

        Me.lblPageInfo.AutoSize = False
        Me.lblPageInfo.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblPageInfo.Location = New System.Drawing.Point(85, 2)
        Me.lblPageInfo.Size = New System.Drawing.Size(100, 28)
        Me.lblPageInfo.Text = "Trang 1 / 1"
        Me.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        Me.btnNext.BackColor = System.Drawing.Color.FromArgb(107, 114, 128)
        Me.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNext.FlatAppearance.BorderSize = 0
        Me.btnNext.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnNext.ForeColor = System.Drawing.Color.White
        Me.btnNext.Location = New System.Drawing.Point(190, 2)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(80, 28)
        Me.btnNext.Text = "Sau →"
        Me.btnNext.Cursor = System.Windows.Forms.Cursors.Hand

        '-- pnlStatusUpdate (RECREATED) --
        Me.pnlStatusUpdate.BackColor = System.Drawing.Color.White
        Me.pnlStatusUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlStatusUpdate.Controls.Add(Me.lblSelectedTask)
        Me.pnlStatusUpdate.Controls.Add(Me.lblNewStatus)
        Me.pnlStatusUpdate.Controls.Add(Me.cboNewStatus)
        Me.pnlStatusUpdate.Controls.Add(Me.btnConfirmStatus)
        Me.pnlStatusUpdate.Location = New System.Drawing.Point(10, 475)
        Me.pnlStatusUpdate.Name = "pnlStatusUpdate"
        Me.pnlStatusUpdate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlStatusUpdate.Size = New System.Drawing.Size(780, 75)

        Me.lblSelectedTask.AutoSize = False
        Me.lblSelectedTask.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic)
        Me.lblSelectedTask.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128)
        Me.lblSelectedTask.Location = New System.Drawing.Point(15, 8)
        Me.lblSelectedTask.Size = New System.Drawing.Size(750, 20)
        Me.lblSelectedTask.Text = "Đang chọn: Chưa chọn công việc"

        Me.lblNewStatus.AutoSize = True
        Me.lblNewStatus.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblNewStatus.Location = New System.Drawing.Point(15, 38)
        Me.lblNewStatus.Text = "Cập nhật trạng thái mới:"

        Me.cboNewStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNewStatus.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cboNewStatus.Location = New System.Drawing.Point(185, 34)
        Me.cboNewStatus.Size = New System.Drawing.Size(180, 26)

        Me.btnConfirmStatus.BackColor = System.Drawing.Color.FromArgb(37, 99, 235) ' Blue for "New" button feel
        Me.btnConfirmStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConfirmStatus.FlatAppearance.BorderSize = 0
        Me.btnConfirmStatus.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnConfirmStatus.ForeColor = System.Drawing.Color.White
        Me.btnConfirmStatus.Location = New System.Drawing.Point(380, 31)
        Me.btnConfirmStatus.Name = "btnConfirmStatus"
        Me.btnConfirmStatus.Size = New System.Drawing.Size(220, 34)
        Me.btnConfirmStatus.Text = "✔ XÁC NHẬN CẬP NHẬT"
        Me.btnConfirmStatus.Cursor = System.Windows.Forms.Cursors.Hand

        '-- frmMyTasks --
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(243, 244, 246)
        Me.ClientSize = New System.Drawing.Size(800, 560)
        Me.MinimumSize = New System.Drawing.Size(816, 599)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblUserInfo)
        Me.Controls.Add(Me.dgvMyTasks)
        Me.Controls.Add(Me.pnlPagination)
        Me.Controls.Add(Me.pnlStatusUpdate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.MaximizeBox = True
        Me.Name = "frmMyTasks"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AppStory – Công Việc Của Tôi"
        Me.pnlHeader.ResumeLayout(False)
        CType(Me.dgvMyTasks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlStatusUpdate.ResumeLayout(False)
        Me.pnlStatusUpdate.PerformLayout()
        Me.pnlPagination.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents lblUserInfo As System.Windows.Forms.Label
    Friend WithEvents dgvMyTasks As System.Windows.Forms.DataGridView
    Friend WithEvents pnlStatusUpdate As System.Windows.Forms.Panel
    Friend WithEvents lblSelectedTask As System.Windows.Forms.Label
    Friend WithEvents lblNewStatus As System.Windows.Forms.Label
    Friend WithEvents cboNewStatus As System.Windows.Forms.ComboBox
    Friend WithEvents btnConfirmStatus As System.Windows.Forms.Button
    Friend WithEvents pnlPagination As System.Windows.Forms.Panel
    Friend WithEvents btnPrev As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents lblPageInfo As System.Windows.Forms.Label
End Class
