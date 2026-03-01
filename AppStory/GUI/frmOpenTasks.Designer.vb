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
        Me.dgvOpenTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvOpenTasks.Location = New System.Drawing.Point(20, 120)
        Me.dgvOpenTasks.Name = "dgvOpenTasks"
        Me.dgvOpenTasks.ReadOnly = True
        Me.dgvOpenTasks.RowHeadersVisible = False
        Me.dgvOpenTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvOpenTasks.Size = New System.Drawing.Size(760, 300)
        Me.dgvOpenTasks.TabIndex = 1
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
        Me.Controls.Add(Me.pnlHeader)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
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
End Class
