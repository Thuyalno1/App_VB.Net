<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRegister
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
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.lblConfirmPassword = New System.Windows.Forms.Label()
        Me.txtConfirmPassword = New System.Windows.Forms.TextBox()
        Me.lblRole = New System.Windows.Forms.Label()
        Me.cboRole = New System.Windows.Forms.ComboBox()
        Me.btnRegister = New System.Windows.Forms.Button()
        Me.btnBackLogin = New System.Windows.Forms.Button()
        Me.lblLoginLink = New System.Windows.Forms.Label()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()

        '--- pnlMain ---
        Me.pnlMain.BackColor = System.Drawing.Color.White
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.lblTitle)
        Me.pnlMain.Controls.Add(Me.lblUsername)
        Me.pnlMain.Controls.Add(Me.txtUsername)
        Me.pnlMain.Controls.Add(Me.lblEmail)
        Me.pnlMain.Controls.Add(Me.txtEmail)
        Me.pnlMain.Controls.Add(Me.lblPassword)
        Me.pnlMain.Controls.Add(Me.txtPassword)
        Me.pnlMain.Controls.Add(Me.lblConfirmPassword)
        Me.pnlMain.Controls.Add(Me.txtConfirmPassword)
        Me.pnlMain.Controls.Add(Me.lblRole)
        Me.pnlMain.Controls.Add(Me.cboRole)
        Me.pnlMain.Controls.Add(Me.btnRegister)
        Me.pnlMain.Controls.Add(Me.lblLoginLink)
        Me.pnlMain.Controls.Add(Me.btnBackLogin)
        Me.pnlMain.Location = New System.Drawing.Point(60, 30)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(380, 560)
        Me.pnlMain.TabIndex = 0

        '--- lblTitle ---
        Me.lblTitle.AutoSize = False
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235)
        Me.lblTitle.Location = New System.Drawing.Point(0, 25)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(380, 40)
        Me.lblTitle.Text = "Tạo Tài Khoản"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        '--- lblUsername ---
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblUsername.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99)
        Me.lblUsername.Location = New System.Drawing.Point(30, 85)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Text = "Tên đăng nhập"

        '--- txtUsername ---
        Me.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUsername.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtUsername.Location = New System.Drawing.Point(30, 107)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(320, 28)
        Me.txtUsername.TabIndex = 0

        '--- lblEmail ---
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblEmail.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99)
        Me.lblEmail.Location = New System.Drawing.Point(30, 150)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Text = "Email"

        '--- txtEmail ---
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtEmail.Location = New System.Drawing.Point(30, 172)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(320, 28)
        Me.txtEmail.TabIndex = 1

        '--- lblPassword ---
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblPassword.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99)
        Me.lblPassword.Location = New System.Drawing.Point(30, 215)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Text = "Mật khẩu"

        '--- txtPassword ---
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtPassword.Location = New System.Drawing.Point(30, 237)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = "*"c
        Me.txtPassword.Size = New System.Drawing.Size(320, 28)
        Me.txtPassword.TabIndex = 2

        '--- lblConfirmPassword ---
        Me.lblConfirmPassword.AutoSize = True
        Me.lblConfirmPassword.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99)
        Me.lblConfirmPassword.Location = New System.Drawing.Point(30, 280)
        Me.lblConfirmPassword.Name = "lblConfirmPassword"
        Me.lblConfirmPassword.Text = "Xác nhận mật khẩu"

        '--- txtConfirmPassword ---
        Me.txtConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtConfirmPassword.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtConfirmPassword.Location = New System.Drawing.Point(30, 302)
        Me.txtConfirmPassword.Name = "txtConfirmPassword"
        Me.txtConfirmPassword.PasswordChar = "*"c
        Me.txtConfirmPassword.Size = New System.Drawing.Size(320, 28)
        Me.txtConfirmPassword.TabIndex = 3

        '--- lblRole ---
        Me.lblRole.AutoSize = True
        Me.lblRole.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblRole.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99)
        Me.lblRole.Location = New System.Drawing.Point(30, 345)
        Me.lblRole.Name = "lblRole"
        Me.lblRole.Text = "Vai trò"

        '--- cboRole ---
        Me.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRole.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cboRole.Location = New System.Drawing.Point(30, 367)
        Me.cboRole.Name = "cboRole"
        Me.cboRole.Size = New System.Drawing.Size(320, 28)
        Me.cboRole.TabIndex = 4

        '--- btnRegister ---
        Me.btnRegister.BackColor = System.Drawing.Color.FromArgb(37, 99, 235)
        Me.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRegister.FlatAppearance.BorderSize = 0
        Me.btnRegister.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.btnRegister.ForeColor = System.Drawing.Color.White
        Me.btnRegister.Location = New System.Drawing.Point(30, 420)
        Me.btnRegister.Name = "btnRegister"
        Me.btnRegister.Size = New System.Drawing.Size(320, 40)
        Me.btnRegister.TabIndex = 5
        Me.btnRegister.Text = "Đăng Ký"
        Me.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand

        '--- lblLoginLink ---
        Me.lblLoginLink.AutoSize = True
        Me.lblLoginLink.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblLoginLink.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128)
        Me.lblLoginLink.Location = New System.Drawing.Point(60, 485)
        Me.lblLoginLink.Name = "lblLoginLink"
        Me.lblLoginLink.Text = "Đã có tài khoản?"

        '--- btnBackLogin ---
        Me.btnBackLogin.BackColor = System.Drawing.Color.Transparent
        Me.btnBackLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBackLogin.FlatAppearance.BorderSize = 0
        Me.btnBackLogin.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline)
        Me.btnBackLogin.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235)
        Me.btnBackLogin.Location = New System.Drawing.Point(190, 479)
        Me.btnBackLogin.Name = "btnBackLogin"
        Me.btnBackLogin.Size = New System.Drawing.Size(120, 26)
        Me.btnBackLogin.TabIndex = 6
        Me.btnBackLogin.Text = "Đăng nhập ngay"
        Me.btnBackLogin.Cursor = System.Windows.Forms.Cursors.Hand

        '--- frmRegister ---
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(239, 246, 255)
        Me.ClientSize = New System.Drawing.Size(500, 620)
        Me.Controls.Add(Me.pnlMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmRegister"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AppStory – Đăng Ký"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblUsername As System.Windows.Forms.Label
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblConfirmPassword As System.Windows.Forms.Label
    Friend WithEvents txtConfirmPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblRole As System.Windows.Forms.Label
    Friend WithEvents cboRole As System.Windows.Forms.ComboBox
    Friend WithEvents btnRegister As System.Windows.Forms.Button
    Friend WithEvents btnBackLogin As System.Windows.Forms.Button
    Friend WithEvents lblLoginLink As System.Windows.Forms.Label

End Class
