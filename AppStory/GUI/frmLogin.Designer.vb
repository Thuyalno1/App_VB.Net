<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblUsername = New System.Windows.Forms.Label()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.lblRegisterLink = New System.Windows.Forms.Label()
        Me.btnGoRegister = New System.Windows.Forms.Button()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.White
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.lblTitle)
        Me.pnlMain.Controls.Add(Me.lblUsername)
        Me.pnlMain.Controls.Add(Me.txtUsername)
        Me.pnlMain.Controls.Add(Me.lblPassword)
        Me.pnlMain.Controls.Add(Me.txtPassword)
        Me.pnlMain.Controls.Add(Me.btnLogin)
        Me.pnlMain.Controls.Add(Me.lblRegisterLink)
        Me.pnlMain.Controls.Add(Me.btnGoRegister)
        Me.pnlMain.Location = New System.Drawing.Point(80, 62)
        Me.pnlMain.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(479, 492)
        Me.pnlMain.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(0, 37)
        Me.lblTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(480, 49)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Đăng Nhập"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblUsername
        '
        Me.lblUsername.AutoSize = True
        Me.lblUsername.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblUsername.ForeColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(99, Byte), Integer))
        Me.lblUsername.Location = New System.Drawing.Point(40, 123)
        Me.lblUsername.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUsername.Name = "lblUsername"
        Me.lblUsername.Size = New System.Drawing.Size(111, 21)
        Me.lblUsername.TabIndex = 1
        Me.lblUsername.Text = "Tên đăng nhập"
        '
        'txtUsername
        '
        Me.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUsername.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtUsername.Location = New System.Drawing.Point(40, 150)
        Me.txtUsername.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(399, 30)
        Me.txtUsername.TabIndex = 0
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(75, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(99, Byte), Integer))
        Me.lblPassword.Location = New System.Drawing.Point(40, 209)
        Me.lblPassword.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(75, 21)
        Me.lblPassword.TabIndex = 2
        Me.lblPassword.Text = "Mật khẩu"
        '
        'txtPassword
        '
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.txtPassword.Location = New System.Drawing.Point(40, 236)
        Me.txtPassword.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(399, 30)
        Me.txtPassword.TabIndex = 1
        '
        'btnLogin
        '
        Me.btnLogin.BackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLogin.FlatAppearance.BorderSize = 0
        Me.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogin.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.btnLogin.ForeColor = System.Drawing.Color.White
        Me.btnLogin.Location = New System.Drawing.Point(40, 308)
        Me.btnLogin.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(400, 49)
        Me.btnLogin.TabIndex = 2
        Me.btnLogin.Text = "Đăng Nhập"
        Me.btnLogin.UseVisualStyleBackColor = False
        '
        'lblRegisterLink
        '
        Me.lblRegisterLink.AutoSize = True
        Me.lblRegisterLink.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblRegisterLink.ForeColor = System.Drawing.Color.FromArgb(CType(CType(107, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblRegisterLink.Location = New System.Drawing.Point(73, 388)
        Me.lblRegisterLink.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRegisterLink.Name = "lblRegisterLink"
        Me.lblRegisterLink.Size = New System.Drawing.Size(135, 20)
        Me.lblRegisterLink.TabIndex = 3
        Me.lblRegisterLink.Text = "Chưa có tài khoản?"
        '
        'btnGoRegister
        '
        Me.btnGoRegister.BackColor = System.Drawing.Color.Transparent
        Me.btnGoRegister.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGoRegister.FlatAppearance.BorderSize = 0
        Me.btnGoRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGoRegister.Font = New System.Drawing.Font("Segoe UI", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
        Me.btnGoRegister.ForeColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnGoRegister.Location = New System.Drawing.Point(237, 380)
        Me.btnGoRegister.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnGoRegister.Name = "btnGoRegister"
        Me.btnGoRegister.Size = New System.Drawing.Size(147, 32)
        Me.btnGoRegister.TabIndex = 3
        Me.btnGoRegister.Text = "Đăng ký ngay"
        Me.btnGoRegister.UseVisualStyleBackColor = False
        '
        'frmLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(640, 615)
        Me.Controls.Add(Me.pnlMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.Name = "frmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AppStory – Đăng Nhập"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblUsername As System.Windows.Forms.Label
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents btnGoRegister As System.Windows.Forms.Button
    Friend WithEvents lblRegisterLink As System.Windows.Forms.Label

End Class
