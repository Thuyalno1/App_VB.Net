<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTeams
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dgvTeams = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnDetail = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.clbLeaders = New System.Windows.Forms.CheckedListBox()
        Me.clbMembers = New System.Windows.Forms.CheckedListBox()
        Me.lblMembers = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.txtTeamName = New System.Windows.Forms.TextBox()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        CType(Me.dgvTeams, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Location = New System.Drawing.Point(100, 17)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(65, 13)
        Me.lblSearch.TabIndex = 10
        Me.lblSearch.Text = "🔍 Tìm kiếm:"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(175, 14)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(274, 20)
        Me.txtSearch.TabIndex = 11
        '
        'dgvTeams
        '
        Me.dgvTeams.AllowUserToAddRows = False
        Me.dgvTeams.AllowUserToDeleteRows = False
        Me.dgvTeams.BackgroundColor = System.Drawing.Color.White
        Me.dgvTeams.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvTeams.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTeams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTeams.Location = New System.Drawing.Point(12, 41)
        Me.dgvTeams.Name = "dgvTeams"
        Me.dgvTeams.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvTeams.ReadOnly = True
        Me.dgvTeams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTeams.Size = New System.Drawing.Size(437, 397)
        Me.dgvTeams.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnDetail)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnClear)
        Me.GroupBox1.Controls.Add(Me.btnDelete)
        Me.GroupBox1.Controls.Add(Me.btnUpdate)
        Me.GroupBox1.Controls.Add(Me.btnAdd)
        Me.GroupBox1.Controls.Add(Me.clbLeaders)
        Me.GroupBox1.Controls.Add(Me.clbMembers)
        Me.GroupBox1.Controls.Add(Me.lblMembers)
        Me.GroupBox1.Controls.Add(Me.txtDescription)
        Me.GroupBox1.Controls.Add(Me.txtTeamName)
        Me.GroupBox1.Location = New System.Drawing.Point(455, 41)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Size = New System.Drawing.Size(333, 397)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Thông tin Nhóm"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 142)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Trưởng nhóm:"
        '
        'lblMembers
        '
        Me.lblMembers.AutoSize = True
        Me.lblMembers.Location = New System.Drawing.Point(170, 142)
        Me.lblMembers.Name = "lblMembers"
        Me.lblMembers.Size = New System.Drawing.Size(84, 13)
        Me.lblMembers.TabIndex = 10
        Me.lblMembers.Text = "Thành viên:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Mô tả:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Tên nhóm:"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(245, 290)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 6
        Me.btnClear.Text = "Làm mới"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnDetail
        '
        Me.btnDetail.BackColor = System.Drawing.Color.FromArgb(59, 130, 246)
        Me.btnDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDetail.FlatAppearance.BorderSize = 0
        Me.btnDetail.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
        Me.btnDetail.ForeColor = System.Drawing.Color.White
        Me.btnDetail.Location = New System.Drawing.Point(2, 320)
        Me.btnDetail.Name = "btnDetail"
        Me.btnDetail.Size = New System.Drawing.Size(318, 30)
        Me.btnDetail.Text = "👁 Xem chi tiết nhóm"
        Me.btnDetail.Cursor = System.Windows.Forms.Cursors.Hand
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(164, 290)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 5
        Me.btnDelete.Text = "Xóa"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(83, 290)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(75, 23)
        Me.btnUpdate.TabIndex = 4
        Me.btnUpdate.Text = "Cập nhật"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(2, 290)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 3
        Me.btnAdd.Text = "Thêm mới"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'clbLeaders
        '
        Me.clbLeaders.FormattingEnabled = True
        Me.clbLeaders.Location = New System.Drawing.Point(18, 158)
        Me.clbLeaders.Name = "clbLeaders"
        Me.clbLeaders.Size = New System.Drawing.Size(145, 120)
        Me.clbLeaders.TabIndex = 2
        '
        'clbMembers
        '
        Me.clbMembers.FormattingEnabled = True
        Me.clbMembers.Location = New System.Drawing.Point(173, 158)
        Me.clbMembers.Name = "clbMembers"
        Me.clbMembers.Size = New System.Drawing.Size(145, 120)
        Me.clbMembers.TabIndex = 3
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(18, 94)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(302, 45)
        Me.txtDescription.TabIndex = 1
        '
        'txtTeamName
        '
        Me.txtTeamName.Location = New System.Drawing.Point(18, 46)
        Me.txtTeamName.Name = "txtTeamName"
        Me.txtTeamName.Size = New System.Drawing.Size(302, 20)
        Me.txtTeamName.TabIndex = 0
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
        'frmTeams
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.MinimumSize = New System.Drawing.Size(816, 489)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.lblSearch)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvTeams)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.MaximizeBox = True
        Me.Name = "frmTeams"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Quản lý Đội/Nhóm"
        CType(Me.dgvTeams, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvTeams As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents clbLeaders As System.Windows.Forms.CheckedListBox
    Friend WithEvents clbMembers As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblMembers As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtTeamName As System.Windows.Forms.TextBox
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnDetail As System.Windows.Forms.Button
End Class
