<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
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
        Me.lblAppTitle = New System.Windows.Forms.Label()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.pnlContent = New System.Windows.Forms.Panel()
        Me.pnlRoleBadge = New System.Windows.Forms.Panel()
        Me.lblWelcome = New System.Windows.Forms.Label()
        Me.lblRole = New System.Windows.Forms.Label()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.lblSeparator = New System.Windows.Forms.Label()
        Me.lblRoleDesc = New System.Windows.Forms.Label()
        Me.btnGoTasks = New System.Windows.Forms.Button()
        Me.btnGoOpenTasks = New System.Windows.Forms.Button()
        Me.btnGoMyTasks = New System.Windows.Forms.Button()
        Me.btnGoMyTeams = New System.Windows.Forms.Button()
        Me.btnGoProjects = New System.Windows.Forms.Button()
        Me.btnGoTeams = New System.Windows.Forms.Button()
        Me.btnGoApproval = New System.Windows.Forms.Button()
        Me.flpMenu = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnlHeader.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.pnlRoleBadge.SuspendLayout()
        Me.flpMenu.SuspendLayout()
        Me.SuspendLayout()

        '--- pnlHeader ---
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(37, 99, 235)
        Me.pnlHeader.Controls.Add(Me.lblAppTitle)
        Me.pnlHeader.Controls.Add(Me.btnLogout)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Size = New System.Drawing.Size(600, 60)

        Me.lblAppTitle.AutoSize = False
        Me.lblAppTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblAppTitle.ForeColor = System.Drawing.Color.White
        Me.lblAppTitle.Location = New System.Drawing.Point(20, 0)
        Me.lblAppTitle.Size = New System.Drawing.Size(200, 60)
        Me.lblAppTitle.Text = "AppStory"
        Me.lblAppTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

        Me.btnLogout.BackColor = System.Drawing.Color.FromArgb(220, 38, 38)
        Me.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogout.FlatAppearance.BorderSize = 0
        Me.btnLogout.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnLogout.ForeColor = System.Drawing.Color.White
        Me.btnLogout.Location = New System.Drawing.Point(490, 13)
        Me.btnLogout.Size = New System.Drawing.Size(90, 34)
        Me.btnLogout.Text = "Đăng xuất"
        Me.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand

        '--- pnlContent ---
        Me.pnlContent.BackColor = System.Drawing.Color.FromArgb(249, 250, 251)
        Me.pnlContent.Controls.Add(Me.pnlRoleBadge)
        Me.pnlContent.Controls.Add(Me.lblSeparator)
        Me.pnlContent.Controls.Add(Me.lblRoleDesc)
        Me.pnlContent.Controls.Add(Me.flpMenu)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill

        '--- flpMenu ---
        Me.flpMenu.BackColor = System.Drawing.Color.Transparent
        Me.flpMenu.Controls.Add(Me.btnGoTasks)
        Me.flpMenu.Controls.Add(Me.btnGoApproval)
        Me.flpMenu.Controls.Add(Me.btnGoOpenTasks)
        Me.flpMenu.Controls.Add(Me.btnGoMyTasks)
        Me.flpMenu.Controls.Add(Me.btnGoMyTeams)
        Me.flpMenu.Controls.Add(Me.btnGoProjects)
        Me.flpMenu.Controls.Add(Me.btnGoTeams)
        Me.flpMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flpMenu.Location = New System.Drawing.Point(80, 278)
        Me.flpMenu.Name = "flpMenu"
        Me.flpMenu.Size = New System.Drawing.Size(460, 320)
        Me.flpMenu.WrapContents = False
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill

        '--- pnlRoleBadge ---
        Me.pnlRoleBadge.BackColor = System.Drawing.Color.FromArgb(16, 185, 129)
        Me.pnlRoleBadge.Controls.Add(Me.lblWelcome)
        Me.pnlRoleBadge.Controls.Add(Me.lblRole)
        Me.pnlRoleBadge.Controls.Add(Me.lblEmail)
        Me.pnlRoleBadge.Location = New System.Drawing.Point(80, 55)
        Me.pnlRoleBadge.Size = New System.Drawing.Size(440, 155)

        Me.lblWelcome.AutoSize = False
        Me.lblWelcome.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lblWelcome.ForeColor = System.Drawing.Color.White
        Me.lblWelcome.Location = New System.Drawing.Point(20, 18)
        Me.lblWelcome.Size = New System.Drawing.Size(400, 38)
        Me.lblWelcome.Text = "Xin chào, ..."

        Me.lblRole.AutoSize = False
        Me.lblRole.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblRole.ForeColor = System.Drawing.Color.White
        Me.lblRole.Location = New System.Drawing.Point(20, 64)
        Me.lblRole.Size = New System.Drawing.Size(400, 28)
        Me.lblRole.Text = "Vai trò: ..."

        Me.lblEmail.AutoSize = False
        Me.lblEmail.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblEmail.ForeColor = System.Drawing.Color.FromArgb(220, 255, 255)
        Me.lblEmail.Location = New System.Drawing.Point(20, 102)
        Me.lblEmail.Size = New System.Drawing.Size(400, 26)
        Me.lblEmail.Text = "Email: ..."

        '--- separator ---
        Me.lblSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSeparator.Location = New System.Drawing.Point(80, 228)
        Me.lblSeparator.Size = New System.Drawing.Size(440, 2)

        '--- lblRoleDesc ---
        Me.lblRoleDesc.AutoSize = False
        Me.lblRoleDesc.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblRoleDesc.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99)
        Me.lblRoleDesc.Location = New System.Drawing.Point(80, 236)
        Me.lblRoleDesc.Size = New System.Drawing.Size(440, 30)
        Me.lblRoleDesc.Text = ""

        '--- btnGoTasks (Admin/Manager) ---
        Me.btnGoTasks.BackColor = System.Drawing.Color.FromArgb(37, 99, 235)
        Me.btnGoTasks.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGoTasks.FlatAppearance.BorderSize = 0
        Me.btnGoTasks.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.btnGoTasks.ForeColor = System.Drawing.Color.White
        Me.btnGoTasks.Location = New System.Drawing.Point(0, 0)
        Me.btnGoTasks.Margin = New System.Windows.Forms.Padding(0, 0, 0, 10)
        Me.btnGoTasks.Name = "btnGoTasks"
        Me.btnGoTasks.Size = New System.Drawing.Size(440, 42)
        Me.btnGoTasks.Text = "📋 Quản Lý Công Việc"
        Me.btnGoTasks.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGoTasks.Visible = False

        '--- btnGoOpenTasks (Employee/Manager) ---
        Me.btnGoOpenTasks.BackColor = System.Drawing.Color.FromArgb(5, 150, 105)
        Me.btnGoOpenTasks.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGoOpenTasks.FlatAppearance.BorderSize = 0
        Me.btnGoOpenTasks.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.btnGoOpenTasks.ForeColor = System.Drawing.Color.White
        Me.btnGoOpenTasks.Location = New System.Drawing.Point(0, 52)
        Me.btnGoOpenTasks.Margin = New System.Windows.Forms.Padding(0, 0, 0, 10)
        Me.btnGoOpenTasks.Name = "btnGoOpenTasks"
        Me.btnGoOpenTasks.Size = New System.Drawing.Size(440, 42)
        Me.btnGoOpenTasks.Text = "📥 Việc Cần Nhận"
        Me.btnGoOpenTasks.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGoOpenTasks.Visible = False

        '--- btnGoMyTasks (Employee/Manager) ---
        Me.btnGoMyTasks.BackColor = System.Drawing.Color.FromArgb(16, 185, 129)
        Me.btnGoMyTasks.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGoMyTasks.FlatAppearance.BorderSize = 0
        Me.btnGoMyTasks.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.btnGoMyTasks.ForeColor = System.Drawing.Color.White
        Me.btnGoMyTasks.Location = New System.Drawing.Point(0, 104)
        Me.btnGoMyTasks.Margin = New System.Windows.Forms.Padding(0, 0, 0, 10)
        Me.btnGoMyTasks.Name = "btnGoMyTasks"
        Me.btnGoMyTasks.Size = New System.Drawing.Size(440, 42)
        Me.btnGoMyTasks.Text = "✅ Công Việc Của Tôi"
        Me.btnGoMyTasks.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGoMyTasks.Visible = False

        '--- btnGoMyTeams (Employee/Manager) ---
        Me.btnGoMyTeams.BackColor = System.Drawing.Color.FromArgb(16, 185, 129)
        Me.btnGoMyTeams.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGoMyTeams.FlatAppearance.BorderSize = 0
        Me.btnGoMyTeams.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.btnGoMyTeams.ForeColor = System.Drawing.Color.White
        Me.btnGoMyTeams.Location = New System.Drawing.Point(0, 156)
        Me.btnGoMyTeams.Margin = New System.Windows.Forms.Padding(0, 0, 0, 10)
        Me.btnGoMyTeams.Name = "btnGoMyTeams"
        Me.btnGoMyTeams.Size = New System.Drawing.Size(440, 42)
        Me.btnGoMyTeams.Text = "👥 Nhóm Của Tôi"
        Me.btnGoMyTeams.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGoMyTeams.Visible = False

        '--- btnGoProjects (Admin/Manager) ---
        Me.btnGoProjects.BackColor = System.Drawing.Color.FromArgb(245, 158, 11)
        Me.btnGoProjects.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGoProjects.FlatAppearance.BorderSize = 0
        Me.btnGoProjects.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.btnGoProjects.ForeColor = System.Drawing.Color.White
        Me.btnGoProjects.Location = New System.Drawing.Point(0, 208)
        Me.btnGoProjects.Margin = New System.Windows.Forms.Padding(0, 0, 0, 10)
        Me.btnGoProjects.Name = "btnGoProjects"
        Me.btnGoProjects.Size = New System.Drawing.Size(440, 42)
        Me.btnGoProjects.Text = "🚀 Quản Lý Dự Án"
        Me.btnGoProjects.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGoProjects.Visible = False

        '--- btnGoTeams (Admin) ---
        Me.btnGoTeams.BackColor = System.Drawing.Color.FromArgb(99, 102, 241)
        Me.btnGoTeams.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGoTeams.FlatAppearance.BorderSize = 0
        Me.btnGoTeams.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.btnGoTeams.ForeColor = System.Drawing.Color.White
        Me.btnGoTeams.Location = New System.Drawing.Point(0, 260)
        Me.btnGoTeams.Margin = New System.Windows.Forms.Padding(0, 0, 0, 10)
        Me.btnGoTeams.Name = "btnGoTeams"
        Me.btnGoTeams.Size = New System.Drawing.Size(440, 42)
        Me.btnGoTeams.Text = "👥 Quản Lý Nhóm"
        Me.btnGoTeams.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGoTeams.Visible = False

        '--- btnGoApproval (Admin/Manager) ---
        Me.btnGoApproval.BackColor = System.Drawing.Color.FromArgb(16, 185, 129)
        Me.btnGoApproval.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGoApproval.FlatAppearance.BorderSize = 0
        Me.btnGoApproval.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.btnGoApproval.ForeColor = System.Drawing.Color.White
        Me.btnGoApproval.Location = New System.Drawing.Point(0, 0)
        Me.btnGoApproval.Margin = New System.Windows.Forms.Padding(0, 0, 0, 10)
        Me.btnGoApproval.Name = "btnGoApproval"
        Me.btnGoApproval.Size = New System.Drawing.Size(440, 42)
        Me.btnGoApproval.Text = "✔️ Duyệt Công Việc"
        Me.btnGoApproval.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGoApproval.Visible = False

        '--- frmMain ---
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(249, 250, 251)
        Me.ClientSize = New System.Drawing.Size(600, 632)
        Me.Controls.Add(Me.pnlContent)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AppStory – Trang Chủ"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlContent.ResumeLayout(False)
        Me.pnlRoleBadge.ResumeLayout(False)
        Me.flpMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblAppTitle As System.Windows.Forms.Label
    Friend WithEvents btnLogout As System.Windows.Forms.Button
    Friend WithEvents pnlContent As System.Windows.Forms.Panel
    Friend WithEvents pnlRoleBadge As System.Windows.Forms.Panel
    Friend WithEvents lblWelcome As System.Windows.Forms.Label
    Friend WithEvents lblRole As System.Windows.Forms.Label
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents lblSeparator As System.Windows.Forms.Label
    Friend WithEvents lblRoleDesc As System.Windows.Forms.Label
    Friend WithEvents btnGoTasks As System.Windows.Forms.Button
    Friend WithEvents btnGoOpenTasks As System.Windows.Forms.Button
    Friend WithEvents btnGoMyTasks As System.Windows.Forms.Button
    Friend WithEvents btnGoMyTeams As System.Windows.Forms.Button
    Friend WithEvents btnGoProjects As System.Windows.Forms.Button
    Friend WithEvents btnGoTeams As System.Windows.Forms.Button
    Friend WithEvents btnGoApproval As System.Windows.Forms.Button
    Friend WithEvents flpMenu As System.Windows.Forms.FlowLayoutPanel

End Class
