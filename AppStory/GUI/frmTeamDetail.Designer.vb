<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTeamDetail
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
        Me.lblTeamName = New System.Windows.Forms.Label()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.tabControl = New System.Windows.Forms.TabControl()
        Me.tabMembers = New System.Windows.Forms.TabPage()
        Me.tabTasks = New System.Windows.Forms.TabPage()
        Me.dgvMembers = New System.Windows.Forms.DataGridView()
        Me.dgvTasks = New System.Windows.Forms.DataGridView()
        Me.lblMemberCount = New System.Windows.Forms.Label()
        Me.lblTaskCount = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        Me.tabControl.SuspendLayout()
        Me.tabMembers.SuspendLayout()
        Me.tabTasks.SuspendLayout()
        CType(Me.dgvMembers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTasks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

        '--- pnlHeader ---
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(59, 130, 246)
        Me.pnlHeader.Controls.Add(Me.lblTeamName)
        Me.pnlHeader.Controls.Add(Me.lblDescription)
        Me.pnlHeader.Controls.Add(Me.btnBack)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(850, 70)

        '--- lblTeamName ---
        Me.lblTeamName.AutoSize = True
        Me.lblTeamName.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTeamName.ForeColor = System.Drawing.Color.White
        Me.lblTeamName.Location = New System.Drawing.Point(20, 8)
        Me.lblTeamName.Name = "lblTeamName"
        Me.lblTeamName.Text = "Tên nhóm"

        '--- lblDescription ---
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblDescription.ForeColor = System.Drawing.Color.FromArgb(219, 234, 254)
        Me.lblDescription.Location = New System.Drawing.Point(22, 42)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Text = ""

        '--- btnBack ---
        Me.btnBack.BackColor = System.Drawing.Color.FromArgb(99, 102, 241)
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.Location = New System.Drawing.Point(730, 18)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(100, 36)
        Me.btnBack.Text = "← Quay lại"
        Me.btnBack.Cursor = System.Windows.Forms.Cursors.Hand

        '--- tabControl ---
        Me.tabControl.Controls.Add(Me.tabMembers)
        Me.tabControl.Controls.Add(Me.tabTasks)
        Me.tabControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabControl.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.tabControl.Location = New System.Drawing.Point(12, 80)
        Me.tabControl.Name = "tabControl"
        Me.tabControl.SelectedIndex = 0
        Me.tabControl.Size = New System.Drawing.Size(826, 350)

        '--- tabMembers ---
        Me.tabMembers.Controls.Add(Me.lblMemberCount)
        Me.tabMembers.Controls.Add(Me.dgvMembers)
        Me.tabMembers.Name = "tabMembers"
        Me.tabMembers.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMembers.Text = "👥 Thành viên"

        '--- tabTasks ---
        Me.tabTasks.Controls.Add(Me.lblTaskCount)
        Me.tabTasks.Controls.Add(Me.dgvTasks)
        Me.tabTasks.Name = "tabTasks"
        Me.tabTasks.Padding = New System.Windows.Forms.Padding(3)
        Me.tabTasks.Text = "📋 Công việc"

        '--- lblMemberCount ---
        Me.lblMemberCount.AutoSize = True
        Me.lblMemberCount.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblMemberCount.Location = New System.Drawing.Point(10, 10)
        Me.lblMemberCount.Name = "lblMemberCount"
        Me.lblMemberCount.Text = "Đang tải..."

        '--- dgvMembers ---
        Me.dgvMembers.AllowUserToAddRows = False
        Me.dgvMembers.AllowUserToDeleteRows = False
        Me.dgvMembers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvMembers.BackgroundColor = System.Drawing.Color.White
        Me.dgvMembers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMembers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvMembers.Location = New System.Drawing.Point(10, 35)
        Me.dgvMembers.Name = "dgvMembers"
        Me.dgvMembers.ReadOnly = True
        Me.dgvMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvMembers.Size = New System.Drawing.Size(798, 275)

        '--- lblTaskCount ---
        Me.lblTaskCount.AutoSize = True
        Me.lblTaskCount.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblTaskCount.Location = New System.Drawing.Point(10, 10)
        Me.lblTaskCount.Name = "lblTaskCount"
        Me.lblTaskCount.Text = "Đang tải..."

        '--- dgvTasks ---
        Me.dgvTasks.AllowUserToAddRows = False
        Me.dgvTasks.AllowUserToDeleteRows = False
        Me.dgvTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTasks.BackgroundColor = System.Drawing.Color.White
        Me.dgvTasks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTasks.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvTasks.Location = New System.Drawing.Point(10, 35)
        Me.dgvTasks.Name = "dgvTasks"
        Me.dgvTasks.ReadOnly = True
        Me.dgvTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTasks.Size = New System.Drawing.Size(798, 275)

        '--- frmTeamDetail ---
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(249, 250, 251)
        Me.ClientSize = New System.Drawing.Size(850, 450)
        Me.MinimumSize = New System.Drawing.Size(850, 450)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.tabControl)
        Me.Name = "frmTeamDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Chi tiết Nhóm"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.tabControl.ResumeLayout(False)
        Me.tabMembers.ResumeLayout(False)
        Me.tabMembers.PerformLayout()
        Me.tabTasks.ResumeLayout(False)
        Me.tabTasks.PerformLayout()
        CType(Me.dgvMembers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvTasks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTeamName As System.Windows.Forms.Label
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents tabControl As System.Windows.Forms.TabControl
    Friend WithEvents tabMembers As System.Windows.Forms.TabPage
    Friend WithEvents tabTasks As System.Windows.Forms.TabPage
    Friend WithEvents dgvMembers As System.Windows.Forms.DataGridView
    Friend WithEvents dgvTasks As System.Windows.Forms.DataGridView
    Friend WithEvents lblMemberCount As System.Windows.Forms.Label
    Friend WithEvents lblTaskCount As System.Windows.Forms.Label
End Class
