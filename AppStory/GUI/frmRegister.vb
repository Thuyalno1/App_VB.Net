Public Class frmRegister
    Inherits System.Windows.Forms.Form

    Private ReadOnly _userService As IUserService

    Public Sub New()
        InitializeComponent()
        _userService = New UserService()
    End Sub

    Private Sub frmRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Thêm các role vào ComboBox
        cboRole.Items.Clear()
        cboRole.Items.Add("Employee")
        cboRole.Items.Add("Manager")
        cboRole.Items.Add("Admin")
        cboRole.SelectedIndex = 0  ' Mặc định là Employee
    End Sub

    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Dim dto As New RegisterDto() With {
            .UserName = txtUsername.Text.Trim(),
            .Email = txtEmail.Text.Trim(),
            .Password = txtPassword.Text,
            .ConfirmPassword = txtConfirmPassword.Text,
            .RoleId = cboRole.SelectedItem?.ToString()
        }

        Dim result = _userService.Register(dto)

        If result.Success Then
            MessageBox.Show(result.Message, "Đăng ký thành công", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' Quay về trang đăng nhập
            OpenLoginForm()
        Else
            MessageBox.Show(result.Message, "Đăng ký thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnBackLogin_Click(sender As Object, e As EventArgs) Handles btnBackLogin.Click
        OpenLoginForm()
    End Sub

    Private Sub OpenLoginForm()
        Dim loginForm As New frmLogin()
        loginForm.Show()
        Me.Close()
    End Sub

End Class
