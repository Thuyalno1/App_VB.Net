Public Class frmLogin
    Inherits System.Windows.Forms.Form

    Private ReadOnly _userService As IUserService

    Public Sub New()
        InitializeComponent()
        _userService = New UserService()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim dto As New LoginDto() With {
            .UserName = txtUsername.Text.Trim(),
            .Password = txtPassword.Text
        }

        Dim result = _userService.Login(dto)

        If result.Success Then
            ' Lưu session
            SessionManager.CurrentUser = result.UserData
            MessageBox.Show(result.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Điều hướng theo role
            Dim mainForm As New frmMain()
            mainForm.Show()
            Me.Hide()
        Else
            MessageBox.Show(result.Message, "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnGoRegister_Click(sender As Object, e As EventArgs) Handles btnGoRegister.Click
        Dim registerForm As New frmRegister()
        registerForm.Show()
        Me.Hide()
    End Sub

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Xóa session cũ khi mở lại form đăng nhập
        SessionManager.Logout()
        txtUsername.Text = ""
        txtPassword.Text = ""
    End Sub

    Private Sub txtUsername_TextChanged(sender As Object, e As EventArgs) Handles txtUsername.TextChanged

    End Sub
End Class
