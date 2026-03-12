<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMyTeams
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
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

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dgvMyTeams = New System.Windows.Forms.DataGridView()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.lblUserInfo = New System.Windows.Forms.Label()
        Me.btnCreateTeamTask = New System.Windows.Forms.Button()
        CType(Me.dgvMyTeams, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvMyTeams
        '
        Me.dgvMyTeams.AllowUserToAddRows = False
        Me.dgvMyTeams.AllowUserToDeleteRows = False
        Me.dgvMyTeams.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvMyTeams.BackgroundColor = System.Drawing.Color.White
        Me.dgvMyTeams.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvMyTeams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMyTeams.Location = New System.Drawing.Point(12, 59)
        Me.dgvMyTeams.Name = "dgvMyTeams"
        Me.dgvMyTeams.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMyTeams.ReadOnly = True
        Me.dgvMyTeams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMyTeams.Size = New System.Drawing.Size(776, 379)
        Me.dgvMyTeams.TabIndex = 0
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(12, 12)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(75, 23)
        Me.btnBack.TabIndex = 2
        Me.btnBack.Text = "Quay lại"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'lblUserInfo
        '
        Me.lblUserInfo.AutoSize = True
        Me.lblUserInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserInfo.Location = New System.Drawing.Point(111, 15)
        Me.lblUserInfo.Name = "lblUserInfo"
        Me.lblUserInfo.Size = New System.Drawing.Size(123, 20)
        Me.lblUserInfo.TabIndex = 3
        Me.lblUserInfo.Text = "Nhóm của tôi"
        '
        'btnCreateTeamTask
        '
        Me.btnCreateTeamTask.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.btnCreateTeamTask.ForeColor = System.Drawing.Color.White
        Me.btnCreateTeamTask.Location = New System.Drawing.Point(620, 15)
        Me.btnCreateTeamTask.Name = "btnCreateTeamTask"
        Me.btnCreateTeamTask.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCreateTeamTask.Size = New System.Drawing.Size(150, 30)
        Me.btnCreateTeamTask.TabIndex = 4
        Me.btnCreateTeamTask.Text = "Tạo Task cho nhóm"
        Me.btnCreateTeamTask.UseVisualStyleBackColor = False
        '
        'frmMyTeams
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.MinimumSize = New System.Drawing.Size(816, 489)
        Me.Controls.Add(Me.btnCreateTeamTask)
        Me.Controls.Add(Me.lblUserInfo)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.dgvMyTeams)
        Me.Name = "frmMyTeams"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Nhóm Của Tôi"
        CType(Me.dgvMyTeams, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgvMyTeams As System.Windows.Forms.DataGridView
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents lblUserInfo As System.Windows.Forms.Label
    Friend WithEvents btnCreateTeamTask As System.Windows.Forms.Button
End Class
