Public Class frmMain
    Inherits System.Windows.Forms.Form

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If Not SessionManager.IsLoggedIn() Then
                MessageBox.Show("Phiên đăng nhập không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
                OpenLoginForm()
                Return
            End If

            Dim user As User = SessionManager.CurrentUser
            If user Is Nothing Then Return

            lblWelcome.Text = $"Xin chào, {user.UserName}!"
            lblRole.Text = $"Vai trò: {user.RoleId}"
            lblEmail.Text = $"Email: {user.Email}"

            ' Màu badge và mô tả theo role
            Dim role As String = If(user.RoleId, "").ToLower()
            Select Case role
                Case "admin"
                    lblRoleDesc.Text = "Bạn có toàn quyền quản trị hệ thống."
                    pnlRoleBadge.BackColor = System.Drawing.Color.FromArgb(220, 38, 38)
                    btnGoTasks.Visible = True
                    btnGoTasks.Text = "Quản Lý Công Việc (Admin)"
                    btnGoOpenTasks.Visible = False
                    btnGoMyTasks.Visible = False
                    btnGoMyTeams.Visible = False
                    btnGoProjects.Visible = True
                    btnGoTeams.Visible = True
                Case "manager"
                    lblRoleDesc.Text = "Bạn có quyền quản lý nhóm và phê duyệt."
                    pnlRoleBadge.BackColor = System.Drawing.Color.FromArgb(245, 158, 11)
                    btnGoTasks.Visible = True
                    btnGoTasks.Text = "Quản Lý Công Việc (Manager)"
                    btnGoOpenTasks.Visible = True
                    btnGoMyTasks.Visible = True
                    btnGoMyTeams.Visible = True
                    btnGoProjects.Visible = True
                    btnGoTeams.Visible = False
                Case Else ' Employee
                    lblRoleDesc.Text = "Bạn có thể xem và thực hiện các nhiệm vụ của mình."
                    pnlRoleBadge.BackColor = System.Drawing.Color.FromArgb(16, 185, 129)
                    btnGoTasks.Visible = False
                    btnGoOpenTasks.Visible = True
                    btnGoMyTasks.Visible = True
                    btnGoMyTeams.Visible = True
                    btnGoProjects.Visible = False
                    btnGoTeams.Visible = False
            End Select
        Catch ex As BusinessException
            MessageBox.Show("Lỗi khởi động ứng dụng: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '--- Admin / Manager → Task Management ---
    Private Sub btnGoTasks_Click(sender As Object, e As EventArgs) Handles btnGoTasks.Click
        Dim taskForm As New frmTaskManagement()
        taskForm.Show()
        Me.Hide()
    End Sub

    '--- Employee / Manager → Open Tasks ---
    Private Sub btnGoOpenTasks_Click(sender As Object, e As EventArgs) Handles btnGoOpenTasks.Click
        Dim openTasksForm As New frmOpenTasks()
        openTasksForm.Show()
        Me.Hide()
    End Sub

    '--- Employee / Manager → My Tasks ---
    Private Sub btnGoMyTasks_Click(sender As Object, e As EventArgs) Handles btnGoMyTasks.Click
        Dim myTaskForm As New frmMyTasks()
        myTaskForm.Show()
        Me.Hide()
    End Sub

    '--- Employee / Manager → My Teams ---
    Private Sub btnGoMyTeams_Click(sender As Object, e As EventArgs) Handles btnGoMyTeams.Click
        Dim myTeamsForm As New frmMyTeams()
        myTeamsForm.Show()
        Me.Hide()
    End Sub

    '--- Admin / Manager → Projects ---
    Private Sub btnGoProjects_Click(sender As Object, e As EventArgs) Handles btnGoProjects.Click
        Dim f As New frmProjects()
        f.Show()
        Me.Hide()
    End Sub

    '--- Admin → Teams ---
    Private Sub btnGoTeams_Click(sender As Object, e As EventArgs) Handles btnGoTeams.Click
        Dim f As New frmTeams()
        f.Show()
        Me.Hide()
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Dim confirm As DialogResult = MessageBox.Show(
            "Bạn có chắc muốn đăng xuất không?",
            "Xác nhận",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)
        If confirm = DialogResult.Yes Then
            SessionManager.Logout()
            OpenLoginForm()
        End If
    End Sub

    Private Sub OpenLoginForm()
        Dim loginForm As New frmLogin()
        loginForm.Show()
        Me.Close()
    End Sub

End Class
