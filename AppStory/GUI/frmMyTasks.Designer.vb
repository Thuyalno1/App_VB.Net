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
        Me.btnUpdateStatus = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        CType(Me.dgvMyTasks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlStatusUpdate.SuspendLayout()
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
        Me.dgvMyTasks.RowHeadersVisible = False
        Me.dgvMyTasks.RowTemplate.Height = 30
        Me.dgvMyTasks.Size = New System.Drawing.Size(780, 380)

        '--- pnlStatusUpdate ---
        Me.pnlStatusUpdate.BackColor = System.Drawing.Color.White
        Me.pnlStatusUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlStatusUpdate.Controls.Add(Me.lblSelectedTask)
        Me.pnlStatusUpdate.Controls.Add(Me.lblNewStatus)
        Me.pnlStatusUpdate.Controls.Add(Me.cboNewStatus)
        Me.pnlStatusUpdate.Controls.Add(Me.btnUpdateStatus)
        Me.pnlStatusUpdate.Location = New System.Drawing.Point(10, 482)
        Me.pnlStatusUpdate.Size = New System.Drawing.Size(780, 65)

        Me.lblSelectedTask.AutoSize = False
        Me.lblSelectedTask.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic)
        Me.lblSelectedTask.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128)
        Me.lblSelectedTask.Location = New System.Drawing.Point(10, 8)
        Me.lblSelectedTask.Size = New System.Drawing.Size(360, 20)
        Me.lblSelectedTask.Text = "Chưa chọn công việc nào"

        Me.lblNewStatus.AutoSize = True
        Me.lblNewStatus.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblNewStatus.Location = New System.Drawing.Point(10, 32)
        Me.lblNewStatus.Text = "Cập nhật trạng thái:"

        Me.cboNewStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNewStatus.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.cboNewStatus.Location = New System.Drawing.Point(165, 29)
        Me.cboNewStatus.Size = New System.Drawing.Size(170, 26)

        Me.btnUpdateStatus.BackColor = System.Drawing.Color.FromArgb(16, 185, 129)
        Me.btnUpdateStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdateStatus.FlatAppearance.BorderSize = 0
        Me.btnUpdateStatus.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnUpdateStatus.ForeColor = System.Drawing.Color.White
        Me.btnUpdateStatus.Location = New System.Drawing.Point(350, 26)
        Me.btnUpdateStatus.Name = "btnUpdateStatus"
        Me.btnUpdateStatus.Size = New System.Drawing.Size(180, 32)
        Me.btnUpdateStatus.Text = "✔ Cập nhật trạng thái"
        Me.btnUpdateStatus.Cursor = System.Windows.Forms.Cursors.Hand

        '--- frmMyTasks ---
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(243, 244, 246)
        Me.ClientSize = New System.Drawing.Size(800, 560)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblUserInfo)
        Me.Controls.Add(Me.dgvMyTasks)
        Me.Controls.Add(Me.pnlStatusUpdate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmMyTasks"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AppStory – Công Việc Của Tôi"
        Me.pnlHeader.ResumeLayout(False)
        CType(Me.dgvMyTasks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlStatusUpdate.ResumeLayout(False)
        Me.pnlStatusUpdate.PerformLayout()
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
    Friend WithEvents btnUpdateStatus As System.Windows.Forms.Button

End Class
