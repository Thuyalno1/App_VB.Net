<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTaskManagement
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
        Me.lblFilter = New System.Windows.Forms.Label()
        Me.cboFilterStatus = New System.Windows.Forms.ComboBox()
        Me.lblTaskCount = New System.Windows.Forms.Label()
        Me.dgvTasks = New System.Windows.Forms.DataGridView()
        Me.pnlForm = New System.Windows.Forms.Panel()
        Me.lblFormTitle = New System.Windows.Forms.Label()
        Me.lblTitleField = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.lblDesc = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.lblAssigned = New System.Windows.Forms.Label()
        Me.cboAssignedUser = New System.Windows.Forms.ComboBox()
        Me.lblPriority = New System.Windows.Forms.Label()
        Me.cboPriority = New System.Windows.Forms.ComboBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.lblProject = New System.Windows.Forms.Label()
        Me.cboProject = New System.Windows.Forms.ComboBox()
        Me.lblTeam = New System.Windows.Forms.Label()
        Me.cboTeam = New System.Windows.Forms.ComboBox()
        Me.lblDue = New System.Windows.Forms.Label()
        Me.dtpDueDate = New System.Windows.Forms.DateTimePicker()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        CType(Me.dgvTasks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlForm.SuspendLayout()
        Me.SuspendLayout()

        '--- pnlHeader ---
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(37, 99, 235)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.btnBack)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1100, 55)

        Me.lblTitle.AutoSize = False
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(15, 0)
        Me.lblTitle.Size = New System.Drawing.Size(400, 55)
        Me.lblTitle.Text = "Quản Lý Công Việc (Admin)"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

        Me.btnBack.BackColor = System.Drawing.Color.FromArgb(255, 255, 255, 30)
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Location = New System.Drawing.Point(1010, 12)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(80, 32)
        Me.btnBack.Text = "← Quay lại"
        Me.btnBack.Cursor = System.Windows.Forms.Cursors.Hand

        '--- Filter bar ---
        Me.lblFilter.AutoSize = True
        Me.lblFilter.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblFilter.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235)
        Me.lblFilter.Location = New System.Drawing.Point(10, 66)
        Me.lblFilter.Text = "🔍 Lọc theo trạng thái:"

        Me.cboFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFilterStatus.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.cboFilterStatus.Location = New System.Drawing.Point(165, 62)
        Me.cboFilterStatus.Name = "cboFilterStatus"
        Me.cboFilterStatus.Size = New System.Drawing.Size(155, 26)

        Me.lblTaskCount.AutoSize = False
        Me.lblTaskCount.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic)
        Me.lblTaskCount.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128)
        Me.lblTaskCount.Location = New System.Drawing.Point(335, 66)
        Me.lblTaskCount.Size = New System.Drawing.Size(240, 20)
        Me.lblTaskCount.Text = ""

        '--- dgvTasks ---
        Me.dgvTasks.BackgroundColor = System.Drawing.Color.White
        Me.dgvTasks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvTasks.ColumnHeadersDefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {
            .BackColor = System.Drawing.Color.FromArgb(37, 99, 235),
            .ForeColor = System.Drawing.Color.White,
            .Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        }
        Me.dgvTasks.ColumnHeadersHeight = 35
        Me.dgvTasks.DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {
            .Font = New System.Drawing.Font("Segoe UI", 9.5!),
            .SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254),
            .SelectionForeColor = System.Drawing.Color.Black
        }
        Me.dgvTasks.Location = New System.Drawing.Point(10, 92)
        Me.dgvTasks.Name = "dgvTasks"
        Me.dgvTasks.RowHeadersVisible = False
        Me.dgvTasks.RowTemplate.Height = 30
        Me.dgvTasks.Size = New System.Drawing.Size(700, 463)

        '--- pnlForm ---
        Me.pnlForm.BackColor = System.Drawing.Color.White
        Me.pnlForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlForm.Controls.Add(Me.lblFormTitle)
        Me.pnlForm.Controls.Add(Me.lblTitleField)
        Me.pnlForm.Controls.Add(Me.txtTitle)
        Me.pnlForm.Controls.Add(Me.lblDesc)
        Me.pnlForm.Controls.Add(Me.txtDescription)
        Me.pnlForm.Controls.Add(Me.lblAssigned)
        Me.pnlForm.Controls.Add(Me.cboAssignedUser)
        Me.pnlForm.Controls.Add(Me.lblPriority)
        Me.pnlForm.Controls.Add(Me.cboPriority)
        Me.pnlForm.Controls.Add(Me.lblStatus)
        Me.pnlForm.Controls.Add(Me.cboStatus)
        Me.pnlForm.Controls.Add(Me.lblProject)
        Me.pnlForm.Controls.Add(Me.cboProject)
        Me.pnlForm.Controls.Add(Me.lblTeam)
        Me.pnlForm.Controls.Add(Me.cboTeam)
        Me.pnlForm.Controls.Add(Me.lblDue)
        Me.pnlForm.Controls.Add(Me.dtpDueDate)
        Me.pnlForm.Controls.Add(Me.btnAdd)
        Me.pnlForm.Controls.Add(Me.btnUpdate)
        Me.pnlForm.Controls.Add(Me.btnDelete)
        Me.pnlForm.Controls.Add(Me.btnClear)
        Me.pnlForm.Controls.Add(Me.btnExport)
        Me.pnlForm.Location = New System.Drawing.Point(725, 65)
        Me.pnlForm.Name = "pnlForm"
        Me.pnlForm.Size = New System.Drawing.Size(365, 590)

        Dim yOff As Integer = 15
        Me.lblFormTitle.AutoSize = False
        Me.lblFormTitle.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235)
        Me.lblFormTitle.Location = New System.Drawing.Point(10, yOff)
        Me.lblFormTitle.Size = New System.Drawing.Size(340, 28)
        Me.lblFormTitle.Text = "Thông tin công việc"

        yOff += 38
        Me.lblTitleField.AutoSize = True : Me.lblTitleField.Font = New System.Drawing.Font("Segoe UI", 9.0!) : Me.lblTitleField.Location = New System.Drawing.Point(10, yOff) : Me.lblTitleField.Text = "Tiêu đề *"
        yOff += 18
        Me.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle : Me.txtTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!) : Me.txtTitle.Location = New System.Drawing.Point(10, yOff) : Me.txtTitle.Size = New System.Drawing.Size(340, 26)

        yOff += 34
        Me.lblDesc.AutoSize = True : Me.lblDesc.Font = New System.Drawing.Font("Segoe UI", 9.0!) : Me.lblDesc.Location = New System.Drawing.Point(10, yOff) : Me.lblDesc.Text = "Mô tả"
        yOff += 18
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle : Me.txtDescription.Font = New System.Drawing.Font("Segoe UI", 9.5!) : Me.txtDescription.Location = New System.Drawing.Point(10, yOff) : Me.txtDescription.Multiline = True : Me.txtDescription.Size = New System.Drawing.Size(340, 55)

        yOff += 63
        Me.lblAssigned.AutoSize = True : Me.lblAssigned.Font = New System.Drawing.Font("Segoe UI", 9.0!) : Me.lblAssigned.Location = New System.Drawing.Point(10, yOff) : Me.lblAssigned.Text = "Giao cho (Nhân viên)"
        yOff += 18
        Me.cboAssignedUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAssignedUser.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cboAssignedUser.Location = New System.Drawing.Point(10, yOff)
        Me.cboAssignedUser.Name = "cboAssignedUser"
        Me.cboAssignedUser.Size = New System.Drawing.Size(340, 26)

        yOff += 34
        Me.lblPriority.AutoSize = True : Me.lblPriority.Font = New System.Drawing.Font("Segoe UI", 9.0!) : Me.lblPriority.Location = New System.Drawing.Point(10, yOff) : Me.lblPriority.Text = "Mức ưu tiên"
        yOff += 18
        Me.cboPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList : Me.cboPriority.Font = New System.Drawing.Font("Segoe UI", 9.5!) : Me.cboPriority.Location = New System.Drawing.Point(10, yOff) : Me.cboPriority.Size = New System.Drawing.Size(160, 26)

        Me.lblStatus.AutoSize = True : Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!) : Me.lblStatus.Location = New System.Drawing.Point(185, yOff - 18) : Me.lblStatus.Text = "Trạng thái"
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList : Me.cboStatus.Font = New System.Drawing.Font("Segoe UI", 9.5!) : Me.cboStatus.Location = New System.Drawing.Point(185, yOff) : Me.cboStatus.Size = New System.Drawing.Size(165, 26)

        yOff += 34
        Me.lblDue.AutoSize = True : Me.lblDue.Font = New System.Drawing.Font("Segoe UI", 9.0!) : Me.lblDue.Location = New System.Drawing.Point(10, yOff) : Me.lblDue.Text = "Deadline"
        yOff += 18
        Me.dtpDueDate.Font = New System.Drawing.Font("Segoe UI", 9.5!) : Me.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short : Me.dtpDueDate.Location = New System.Drawing.Point(10, yOff) : Me.dtpDueDate.Size = New System.Drawing.Size(340, 26)

        yOff += 34
        Me.lblProject.AutoSize = True : Me.lblProject.Font = New System.Drawing.Font("Segoe UI", 9.0!) : Me.lblProject.Location = New System.Drawing.Point(10, yOff) : Me.lblProject.Text = "Thuộc Dự Án"
        yOff += 18
        Me.cboProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProject.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cboProject.Location = New System.Drawing.Point(10, yOff)
        Me.cboProject.Name = "cboProject"
        Me.cboProject.Size = New System.Drawing.Size(340, 26)

        yOff += 34
        Me.lblTeam.AutoSize = True : Me.lblTeam.Font = New System.Drawing.Font("Segoe UI", 9.0!) : Me.lblTeam.Location = New System.Drawing.Point(10, yOff) : Me.lblTeam.Text = "Giao cho Nhóm"
        yOff += 18
        Me.cboTeam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTeam.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cboTeam.Location = New System.Drawing.Point(10, yOff)
        Me.cboTeam.Name = "cboTeam"
        Me.cboTeam.Size = New System.Drawing.Size(340, 26)

        yOff += 40
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(37, 99, 235) : Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat : Me.btnAdd.FlatAppearance.BorderSize = 0 : Me.btnAdd.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold) : Me.btnAdd.ForeColor = System.Drawing.Color.White : Me.btnAdd.Location = New System.Drawing.Point(10, yOff) : Me.btnAdd.Name = "btnAdd" : Me.btnAdd.Size = New System.Drawing.Size(160, 34) : Me.btnAdd.Text = "+ Thêm mới" : Me.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnUpdate.BackColor = System.Drawing.Color.FromArgb(245, 158, 11) : Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat : Me.btnUpdate.FlatAppearance.BorderSize = 0 : Me.btnUpdate.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold) : Me.btnUpdate.ForeColor = System.Drawing.Color.White : Me.btnUpdate.Location = New System.Drawing.Point(185, yOff) : Me.btnUpdate.Name = "btnUpdate" : Me.btnUpdate.Size = New System.Drawing.Size(165, 34) : Me.btnUpdate.Text = "✎ Cập nhật" : Me.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand

        yOff += 42
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(220, 38, 38) : Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat : Me.btnDelete.FlatAppearance.BorderSize = 0 : Me.btnDelete.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold) : Me.btnDelete.ForeColor = System.Drawing.Color.White : Me.btnDelete.Location = New System.Drawing.Point(10, yOff) : Me.btnDelete.Name = "btnDelete" : Me.btnDelete.Size = New System.Drawing.Size(160, 34) : Me.btnDelete.Text = "🗑 Xóa (Soft)" : Me.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.BackColor = System.Drawing.Color.FromArgb(107, 114, 128) : Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat : Me.btnClear.FlatAppearance.BorderSize = 0 : Me.btnClear.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold) : Me.btnClear.ForeColor = System.Drawing.Color.White : Me.btnClear.Location = New System.Drawing.Point(185, yOff) : Me.btnClear.Name = "btnClear" : Me.btnClear.Size = New System.Drawing.Size(165, 34) : Me.btnClear.Text = "✖ Xóa form" : Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand

        yOff += 44
        Me.btnExport.BackColor = System.Drawing.Color.FromArgb(5, 150, 105) : Me.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat : Me.btnExport.FlatAppearance.BorderSize = 0 : Me.btnExport.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold) : Me.btnExport.ForeColor = System.Drawing.Color.White : Me.btnExport.Location = New System.Drawing.Point(10, yOff) : Me.btnExport.Name = "btnExport" : Me.btnExport.Size = New System.Drawing.Size(340, 34) : Me.btnExport.Text = "📄 Xuất Thống Kê CSV" : Me.btnExport.Cursor = System.Windows.Forms.Cursors.Hand

        '--- frmTaskManagement ---
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(243, 244, 246)
        Me.ClientSize = New System.Drawing.Size(1100, 670)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblFilter)
        Me.Controls.Add(Me.cboFilterStatus)
        Me.Controls.Add(Me.lblTaskCount)
        Me.Controls.Add(Me.dgvTasks)
        Me.Controls.Add(Me.pnlForm)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmTaskManagement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AppStory – Quản Lý Công Việc"
        Me.pnlHeader.ResumeLayout(False)
        CType(Me.dgvTasks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlForm.ResumeLayout(False)
        Me.pnlForm.PerformLayout()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents lblFilter As System.Windows.Forms.Label
    Friend WithEvents cboFilterStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblTaskCount As System.Windows.Forms.Label
    Friend WithEvents dgvTasks As System.Windows.Forms.DataGridView
    Friend WithEvents pnlForm As System.Windows.Forms.Panel
    Friend WithEvents lblFormTitle As System.Windows.Forms.Label
    Friend WithEvents lblTitleField As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents lblDesc As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblAssigned As System.Windows.Forms.Label
    Friend WithEvents cboAssignedUser As System.Windows.Forms.ComboBox
    Friend WithEvents lblPriority As System.Windows.Forms.Label
    Friend WithEvents cboPriority As System.Windows.Forms.ComboBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblProject As System.Windows.Forms.Label
    Friend WithEvents cboProject As System.Windows.Forms.ComboBox
    Friend WithEvents lblTeam As System.Windows.Forms.Label
    Friend WithEvents cboTeam As System.Windows.Forms.ComboBox
    Friend WithEvents lblDue As System.Windows.Forms.Label
    Friend WithEvents dtpDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnExport As System.Windows.Forms.Button

End Class
