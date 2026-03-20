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
        ' ── Khai báo controls ──
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblAppTitle = New System.Windows.Forms.Label()
        Me.lblDateTime = New System.Windows.Forms.Label()
        Me.btnLogout = New System.Windows.Forms.Button()

        ' Body: AutoScroll outer + FlowLayout inner
        Me.pnlScroll = New System.Windows.Forms.Panel()
        Me.flpBody = New System.Windows.Forms.FlowLayoutPanel()   ' <-- container chính

        ' Welcome badge
        Me.pnlRoleBadge = New System.Windows.Forms.Panel()
        Me.lblWelcome = New System.Windows.Forms.Label()
        Me.lblRole = New System.Windows.Forms.Label()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.pnlRoleDescRow = New System.Windows.Forms.Panel()
        Me.lblRoleDesc = New System.Windows.Forms.Label()

        ' Admin stats (5 cards)
        Me.pnlAdminStats = New System.Windows.Forms.Panel()
        Me.lblAdminStatsTitle = New System.Windows.Forms.Label()
        Me.flpAdminStatCards = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnlStatTotal = New System.Windows.Forms.Panel()
        Me.lblStatTotalCount = New System.Windows.Forms.Label()
        Me.lblStatTotalLabel = New System.Windows.Forms.Label()
        Me.pnlStatActive = New System.Windows.Forms.Panel()
        Me.lblStatActiveCount = New System.Windows.Forms.Label()
        Me.lblStatActiveLabel = New System.Windows.Forms.Label()
        Me.pnlStatCompleted = New System.Windows.Forms.Panel()
        Me.lblStatCompletedCount = New System.Windows.Forms.Label()
        Me.lblStatCompletedLabel = New System.Windows.Forms.Label()
        Me.pnlStatOverdue = New System.Windows.Forms.Panel()
        Me.lblStatOverdueCount = New System.Windows.Forms.Label()
        Me.lblStatOverdueLabel = New System.Windows.Forms.Label()
        Me.pnlStatPlanning = New System.Windows.Forms.Panel()
        Me.lblStatPlanningCount = New System.Windows.Forms.Label()
        Me.lblStatPlanningLabel = New System.Windows.Forms.Label()

        ' Employee stats (4 cards)
        Me.pnlEmployeeStats = New System.Windows.Forms.Panel()
        Me.lblEmployeeStatsTitle = New System.Windows.Forms.Label()
        Me.flpEmployeeStatCards = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnlEmpTotal = New System.Windows.Forms.Panel()
        Me.lblEmpTotalCount = New System.Windows.Forms.Label()
        Me.lblEmpTotalLabel = New System.Windows.Forms.Label()
        Me.pnlEmpInProgress = New System.Windows.Forms.Panel()
        Me.lblEmpInProgressCount = New System.Windows.Forms.Label()
        Me.lblEmpInProgressLabel = New System.Windows.Forms.Label()
        Me.pnlEmpDone = New System.Windows.Forms.Panel()
        Me.lblEmpDoneCount = New System.Windows.Forms.Label()
        Me.lblEmpDoneLabel = New System.Windows.Forms.Label()
        Me.pnlEmpDeadline = New System.Windows.Forms.Panel()
        Me.lblEmpDeadlineCount = New System.Windows.Forms.Label()
        Me.lblEmpDeadlineLabel = New System.Windows.Forms.Label()

        ' Detail panel (toggled by card click)
        Me.pnlCardDetail = New System.Windows.Forms.Panel()
        Me.pnlCardDetailHeader = New System.Windows.Forms.Panel()
        Me.lblCardDetailTitle = New System.Windows.Forms.Label()
        Me.btnCloseDetail = New System.Windows.Forms.Button()
        Me.dgvCardDetail = New System.Windows.Forms.DataGridView()

        ' Menu buttons (inside pnlMenuRow)
        Me.pnlMenuRow = New System.Windows.Forms.Panel()
        Me.flpMenu = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnGoTasks = New System.Windows.Forms.Button()
        Me.btnGoApproval = New System.Windows.Forms.Button()
        Me.btnGoOpenTasks = New System.Windows.Forms.Button()
        Me.btnGoMyTasks = New System.Windows.Forms.Button()
        Me.btnGoMyTeams = New System.Windows.Forms.Button()
        Me.btnGoProjects = New System.Windows.Forms.Button()
        Me.btnGoTeams = New System.Windows.Forms.Button()
        Me.btnGoReport = New System.Windows.Forms.Button()

        Me.pnlHeader.SuspendLayout()
        Me.pnlScroll.SuspendLayout()
        Me.flpBody.SuspendLayout()
        Me.pnlRoleBadge.SuspendLayout()
        Me.pnlRoleDescRow.SuspendLayout()
        Me.pnlAdminStats.SuspendLayout()
        Me.flpAdminStatCards.SuspendLayout()
        Me.pnlStatTotal.SuspendLayout()
        Me.pnlStatActive.SuspendLayout()
        Me.pnlStatCompleted.SuspendLayout()
        Me.pnlStatOverdue.SuspendLayout()
        Me.pnlStatPlanning.SuspendLayout()
        Me.pnlEmployeeStats.SuspendLayout()
        Me.flpEmployeeStatCards.SuspendLayout()
        Me.pnlEmpTotal.SuspendLayout()
        Me.pnlEmpInProgress.SuspendLayout()
        Me.pnlEmpDone.SuspendLayout()
        Me.pnlEmpDeadline.SuspendLayout()
        Me.pnlCardDetail.SuspendLayout()
        Me.pnlCardDetailHeader.SuspendLayout()
        CType(Me.dgvCardDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMenuRow.SuspendLayout()
        Me.flpMenu.SuspendLayout()
        Me.SuspendLayout()

        ' ═══════════════════════════════════════════════════
        '   HEADER  (Top-docked, 58px)
        ' ═══════════════════════════════════════════════════
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(37, 99, 235)
        Me.pnlHeader.Controls.Add(Me.lblAppTitle)
        Me.pnlHeader.Controls.Add(Me.lblDateTime)
        Me.pnlHeader.Controls.Add(Me.btnLogout)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Size = New System.Drawing.Size(1080, 58)

        Me.lblAppTitle.AutoSize = False
        Me.lblAppTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblAppTitle.ForeColor = System.Drawing.Color.White
        Me.lblAppTitle.Location = New System.Drawing.Point(18, 0)
        Me.lblAppTitle.Size = New System.Drawing.Size(220, 58)
        Me.lblAppTitle.Text = "🏠 AppStory"
        Me.lblAppTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

        Me.lblDateTime.AutoSize = False
        Me.lblDateTime.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.lblDateTime.ForeColor = System.Drawing.Color.FromArgb(200, 222, 255)
        Me.lblDateTime.Location = New System.Drawing.Point(250, 0)
        Me.lblDateTime.Size = New System.Drawing.Size(580, 58)
        Me.lblDateTime.Text = ""
        Me.lblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

        Me.btnLogout.BackColor = System.Drawing.Color.FromArgb(220, 38, 38)
        Me.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogout.FlatAppearance.BorderSize = 0
        Me.btnLogout.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnLogout.ForeColor = System.Drawing.Color.White
        Me.btnLogout.Location = New System.Drawing.Point(966, 12)
        Me.btnLogout.Size = New System.Drawing.Size(100, 34)
        Me.btnLogout.Text = "Đăng xuất"
        Me.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand

        ' ═══════════════════════════════════════════════════
        '   SCROLL PANEL (Fill)  →  contains flpBody
        ' ═══════════════════════════════════════════════════
        Me.pnlScroll.AutoScroll = True
        Me.pnlScroll.BackColor = System.Drawing.Color.FromArgb(243, 244, 246)
        Me.pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlScroll.Controls.Add(Me.flpBody)

        ' ═══════════════════════════════════════════════════
        '   flpBody: FlowLayout TopDown — tự động đẩy content
        '   Đây là "cột xương sống" của toàn bộ màn hình chính
        ' ═══════════════════════════════════════════════════
        Me.flpBody.AutoSize = True
        Me.flpBody.BackColor = System.Drawing.Color.Transparent
        Me.flpBody.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flpBody.Location = New System.Drawing.Point(10, 10)
        Me.flpBody.Name = "flpBody"
        Me.flpBody.WrapContents = False
        Me.flpBody.Width = 1050

        ' Controls trong flpBody (theo thứ tự từ trên xuống):
        '  1. pnlRoleBadge
        '  2. pnlRoleDescRow
        '  3. pnlAdminStats  (hoặc pnlEmployeeStats)
        '  4. pnlEmployeeStats
        '  5. pnlCardDetail   ← ẩn/hiện, khi hiện sẽ auto đẩy menu xuống
        '  6. pnlMenuRow
        Me.flpBody.Controls.Add(Me.pnlRoleBadge)
        Me.flpBody.Controls.Add(Me.pnlRoleDescRow)
        Me.flpBody.Controls.Add(Me.pnlAdminStats)
        Me.flpBody.Controls.Add(Me.pnlEmployeeStats)
        Me.flpBody.Controls.Add(Me.pnlCardDetail)
        Me.flpBody.Controls.Add(Me.pnlMenuRow)

        ' ═══════════════════════════════════════════════════
        '   1. WELCOME BADGE  h=76
        ' ═══════════════════════════════════════════════════
        Me.pnlRoleBadge.BackColor = System.Drawing.Color.FromArgb(16, 185, 129)
        Me.pnlRoleBadge.Controls.Add(Me.lblWelcome)
        Me.pnlRoleBadge.Controls.Add(Me.lblRole)
        Me.pnlRoleBadge.Controls.Add(Me.lblEmail)
        Me.pnlRoleBadge.Margin = New System.Windows.Forms.Padding(0, 0, 0, 0)
        Me.pnlRoleBadge.Size = New System.Drawing.Size(1040, 76)

        Me.lblWelcome.AutoSize = False
        Me.lblWelcome.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblWelcome.ForeColor = System.Drawing.Color.White
        Me.lblWelcome.Location = New System.Drawing.Point(16, 6)
        Me.lblWelcome.Size = New System.Drawing.Size(700, 28)
        Me.lblWelcome.Text = "Xin chào, ..."

        Me.lblRole.AutoSize = False
        Me.lblRole.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblRole.ForeColor = System.Drawing.Color.White
        Me.lblRole.Location = New System.Drawing.Point(16, 36)
        Me.lblRole.Size = New System.Drawing.Size(400, 20)
        Me.lblRole.Text = "Vai trò: ..."

        Me.lblEmail.AutoSize = False
        Me.lblEmail.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblEmail.ForeColor = System.Drawing.Color.FromArgb(210, 255, 245)
        Me.lblEmail.Location = New System.Drawing.Point(16, 56)
        Me.lblEmail.Size = New System.Drawing.Size(500, 18)
        Me.lblEmail.Text = "Email: ..."

        ' ═══════════════════════════════════════════════════
        '   2. ROLE DESC ROW  h=26
        ' ═══════════════════════════════════════════════════
        Me.pnlRoleDescRow.BackColor = System.Drawing.Color.FromArgb(243, 244, 246)
        Me.pnlRoleDescRow.Controls.Add(Me.lblRoleDesc)
        Me.pnlRoleDescRow.Margin = New System.Windows.Forms.Padding(0, 2, 0, 4)
        Me.pnlRoleDescRow.Size = New System.Drawing.Size(1040, 24)

        Me.lblRoleDesc.AutoSize = False
        Me.lblRoleDesc.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblRoleDesc.ForeColor = System.Drawing.Color.FromArgb(75, 85, 99)
        Me.lblRoleDesc.Location = New System.Drawing.Point(2, 4)
        Me.lblRoleDesc.Size = New System.Drawing.Size(1036, 18)
        Me.lblRoleDesc.Text = ""

        ' ═══════════════════════════════════════════════════
        '   3. ADMIN STATS  h=160
        ' ═══════════════════════════════════════════════════
        Me.pnlAdminStats.BackColor = System.Drawing.Color.Transparent
        Me.pnlAdminStats.Controls.Add(Me.lblAdminStatsTitle)
        Me.pnlAdminStats.Controls.Add(Me.flpAdminStatCards)
        Me.pnlAdminStats.Margin = New System.Windows.Forms.Padding(0, 0, 0, 8)
        Me.pnlAdminStats.Name = "pnlAdminStats"
        Me.pnlAdminStats.Size = New System.Drawing.Size(1040, 158)
        Me.pnlAdminStats.Visible = False

        Me.lblAdminStatsTitle.AutoSize = True
        Me.lblAdminStatsTitle.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.lblAdminStatsTitle.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235)
        Me.lblAdminStatsTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblAdminStatsTitle.Text = "📊 Tổng Quan Dự Án — click thẻ để xem chi tiết"

        Me.flpAdminStatCards.BackColor = System.Drawing.Color.Transparent
        Me.flpAdminStatCards.Controls.Add(Me.pnlStatTotal)
        Me.flpAdminStatCards.Controls.Add(Me.pnlStatActive)
        Me.flpAdminStatCards.Controls.Add(Me.pnlStatCompleted)
        Me.flpAdminStatCards.Controls.Add(Me.pnlStatOverdue)
        Me.flpAdminStatCards.Controls.Add(Me.pnlStatPlanning)
        Me.flpAdminStatCards.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight
        Me.flpAdminStatCards.Location = New System.Drawing.Point(0, 28)
        Me.flpAdminStatCards.Name = "flpAdminStatCards"
        Me.flpAdminStatCards.Size = New System.Drawing.Size(1040, 126)
        Me.flpAdminStatCards.WrapContents = False

        Call MakeStatCard(Me.pnlStatTotal, Me.lblStatTotalCount, Me.lblStatTotalLabel,
                          System.Drawing.Color.FromArgb(37, 99, 235), "0", "📁 Tổng Dự Án",
                          System.Drawing.Color.FromArgb(200, 220, 255))
        Call MakeStatCard(Me.pnlStatActive, Me.lblStatActiveCount, Me.lblStatActiveLabel,
                          System.Drawing.Color.FromArgb(245, 158, 11), "0", "🔄 Đang Thực Hiện",
                          System.Drawing.Color.FromArgb(255, 240, 200))
        Call MakeStatCard(Me.pnlStatCompleted, Me.lblStatCompletedCount, Me.lblStatCompletedLabel,
                          System.Drawing.Color.FromArgb(16, 185, 129), "0", "✅ Hoàn Thành",
                          System.Drawing.Color.FromArgb(200, 255, 220))
        Call MakeStatCard(Me.pnlStatOverdue, Me.lblStatOverdueCount, Me.lblStatOverdueLabel,
                          System.Drawing.Color.FromArgb(220, 38, 38), "0", "⚠️ Quá Deadline",
                          System.Drawing.Color.FromArgb(255, 200, 200))
        Call MakeStatCard(Me.pnlStatPlanning, Me.lblStatPlanningCount, Me.lblStatPlanningLabel,
                          System.Drawing.Color.FromArgb(107, 114, 128), "0", "📝 Chưa Bắt Đầu",
                          System.Drawing.Color.FromArgb(220, 220, 220))

        ' ═══════════════════════════════════════════════════
        '   4. EMPLOYEE STATS  h=160
        ' ═══════════════════════════════════════════════════
        Me.pnlEmployeeStats.BackColor = System.Drawing.Color.Transparent
        Me.pnlEmployeeStats.Controls.Add(Me.lblEmployeeStatsTitle)
        Me.pnlEmployeeStats.Controls.Add(Me.flpEmployeeStatCards)
        Me.pnlEmployeeStats.Margin = New System.Windows.Forms.Padding(0, 0, 0, 8)
        Me.pnlEmployeeStats.Name = "pnlEmployeeStats"
        Me.pnlEmployeeStats.Size = New System.Drawing.Size(1040, 158)
        Me.pnlEmployeeStats.Visible = False

        Me.lblEmployeeStatsTitle.AutoSize = True
        Me.lblEmployeeStatsTitle.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.lblEmployeeStatsTitle.ForeColor = System.Drawing.Color.FromArgb(16, 185, 129)
        Me.lblEmployeeStatsTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblEmployeeStatsTitle.Text = "📋 Công Việc Của Tôi — click thẻ để xem chi tiết"

        Me.flpEmployeeStatCards.BackColor = System.Drawing.Color.Transparent
        Me.flpEmployeeStatCards.Controls.Add(Me.pnlEmpTotal)
        Me.flpEmployeeStatCards.Controls.Add(Me.pnlEmpInProgress)
        Me.flpEmployeeStatCards.Controls.Add(Me.pnlEmpDone)
        Me.flpEmployeeStatCards.Controls.Add(Me.pnlEmpDeadline)
        Me.flpEmployeeStatCards.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight
        Me.flpEmployeeStatCards.Location = New System.Drawing.Point(0, 28)
        Me.flpEmployeeStatCards.Name = "flpEmployeeStatCards"
        Me.flpEmployeeStatCards.Size = New System.Drawing.Size(1040, 126)
        Me.flpEmployeeStatCards.WrapContents = False

        Call MakeStatCard(Me.pnlEmpTotal, Me.lblEmpTotalCount, Me.lblEmpTotalLabel,
                          System.Drawing.Color.FromArgb(37, 99, 235), "0", "📋 Tổng Việc",
                          System.Drawing.Color.FromArgb(200, 220, 255))
        Call MakeStatCard(Me.pnlEmpInProgress, Me.lblEmpInProgressCount, Me.lblEmpInProgressLabel,
                          System.Drawing.Color.FromArgb(245, 158, 11), "0", "🔄 Đang Làm",
                          System.Drawing.Color.FromArgb(255, 240, 200))
        Call MakeStatCard(Me.pnlEmpDone, Me.lblEmpDoneCount, Me.lblEmpDoneLabel,
                          System.Drawing.Color.FromArgb(16, 185, 129), "0", "✅ Đã Xong",
                          System.Drawing.Color.FromArgb(200, 255, 220))
        Call MakeStatCard(Me.pnlEmpDeadline, Me.lblEmpDeadlineCount, Me.lblEmpDeadlineLabel,
                          System.Drawing.Color.FromArgb(220, 38, 38), "0", "⚠️ Gần Deadline",
                          System.Drawing.Color.FromArgb(255, 200, 200))

        ' ═══════════════════════════════════════════════════
        '   5. CARD DETAIL PANEL  h=182 (ẩn ban đầu)
        '      Khi Visible=True → flpBody TỰ ĐỘNG đẩy pnlMenuRow xuống
        ' ═══════════════════════════════════════════════════
        Me.pnlCardDetail.BackColor = System.Drawing.Color.White
        Me.pnlCardDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCardDetail.Controls.Add(Me.pnlCardDetailHeader)
        Me.pnlCardDetail.Controls.Add(Me.dgvCardDetail)
        Me.pnlCardDetail.Margin = New System.Windows.Forms.Padding(0, 0, 0, 8)
        Me.pnlCardDetail.Name = "pnlCardDetail"
        Me.pnlCardDetail.Size = New System.Drawing.Size(1040, 182)
        Me.pnlCardDetail.Visible = False

        ' Header của detail (tiêu đề + nút [✕])
        Me.pnlCardDetailHeader.BackColor = System.Drawing.Color.FromArgb(239, 246, 255)
        Me.pnlCardDetailHeader.Controls.Add(Me.lblCardDetailTitle)
        Me.pnlCardDetailHeader.Controls.Add(Me.btnCloseDetail)
        Me.pnlCardDetailHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCardDetailHeader.Size = New System.Drawing.Size(1040, 34)

        Me.lblCardDetailTitle.AutoSize = False
        Me.lblCardDetailTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblCardDetailTitle.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235)
        Me.lblCardDetailTitle.Location = New System.Drawing.Point(8, 0)
        Me.lblCardDetailTitle.Size = New System.Drawing.Size(960, 34)
        Me.lblCardDetailTitle.Text = "Chi tiết..."
        Me.lblCardDetailTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

        Me.btnCloseDetail.BackColor = System.Drawing.Color.FromArgb(220, 38, 38)
        Me.btnCloseDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCloseDetail.FlatAppearance.BorderSize = 0
        Me.btnCloseDetail.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnCloseDetail.ForeColor = System.Drawing.Color.White
        Me.btnCloseDetail.Location = New System.Drawing.Point(1003, 5)
        Me.btnCloseDetail.Size = New System.Drawing.Size(30, 24)
        Me.btnCloseDetail.Text = "✕"
        Me.btnCloseDetail.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCloseDetail.Name = "btnCloseDetail"

        Me.dgvCardDetail.BackgroundColor = System.Drawing.Color.White
        Me.dgvCardDetail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvCardDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvCardDetail.ColumnHeadersDefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {
            .BackColor = System.Drawing.Color.FromArgb(37, 99, 235),
            .ForeColor = System.Drawing.Color.White,
            .Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        }
        Me.dgvCardDetail.ColumnHeadersHeight = 30
        Me.dgvCardDetail.DefaultCellStyle = New System.Windows.Forms.DataGridViewCellStyle() With {
            .Font = New System.Drawing.Font("Segoe UI", 9.0!),
            .SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254),
            .SelectionForeColor = System.Drawing.Color.Black
        }
        Me.dgvCardDetail.Location = New System.Drawing.Point(4, 36)
        Me.dgvCardDetail.Name = "dgvCardDetail"
        Me.dgvCardDetail.RowHeadersVisible = False
        Me.dgvCardDetail.RowTemplate.Height = 26
        Me.dgvCardDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCardDetail.ReadOnly = True
        Me.dgvCardDetail.AllowUserToAddRows = False
        Me.dgvCardDetail.Size = New System.Drawing.Size(1030, 140)

        ' ═══════════════════════════════════════════════════
        '   6. MENU ROW  (wrapper để có đủ height cho flpMenu)
        ' ═══════════════════════════════════════════════════
        Me.pnlMenuRow.BackColor = System.Drawing.Color.Transparent
        Me.pnlMenuRow.Controls.Add(Me.flpMenu)
        Me.pnlMenuRow.Margin = New System.Windows.Forms.Padding(0, 0, 0, 20)
        Me.pnlMenuRow.Name = "pnlMenuRow"
        Me.pnlMenuRow.Size = New System.Drawing.Size(1040, 480)

        Me.flpMenu.BackColor = System.Drawing.Color.Transparent
        Me.flpMenu.Controls.Add(Me.btnGoTasks)
        Me.flpMenu.Controls.Add(Me.btnGoApproval)
        Me.flpMenu.Controls.Add(Me.btnGoOpenTasks)
        Me.flpMenu.Controls.Add(Me.btnGoMyTasks)
        Me.flpMenu.Controls.Add(Me.btnGoMyTeams)
        Me.flpMenu.Controls.Add(Me.btnGoProjects)
        Me.flpMenu.Controls.Add(Me.btnGoTeams)
        Me.flpMenu.Controls.Add(Me.btnGoReport)
        Me.flpMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flpMenu.Location = New System.Drawing.Point(0, 0)
        Me.flpMenu.Name = "flpMenu"
        Me.flpMenu.Size = New System.Drawing.Size(1040, 480)
        Me.flpMenu.WrapContents = False

        Dim bw As Integer = 1030
        MakeMenuBtn(Me.btnGoTasks, "📋 Quản Lý Công Việc", System.Drawing.Color.FromArgb(37, 99, 235), bw)
        MakeMenuBtn(Me.btnGoApproval, "✔️ Duyệt Công Việc", System.Drawing.Color.FromArgb(16, 185, 129), bw)
        MakeMenuBtn(Me.btnGoOpenTasks, "📥 Việc Cần Nhận", System.Drawing.Color.FromArgb(5, 150, 105), bw)
        MakeMenuBtn(Me.btnGoMyTasks, "✅ Công Việc Của Tôi", System.Drawing.Color.FromArgb(16, 185, 129), bw)
        MakeMenuBtn(Me.btnGoMyTeams, "👥 Nhóm Của Tôi", System.Drawing.Color.FromArgb(16, 185, 129), bw)
        MakeMenuBtn(Me.btnGoProjects, "🚀 Quản Lý Dự Án", System.Drawing.Color.FromArgb(245, 158, 11), bw)
        MakeMenuBtn(Me.btnGoTeams, "👥 Quản Lý Nhóm", System.Drawing.Color.FromArgb(99, 102, 241), bw)
        MakeMenuBtn(Me.btnGoReport, "📈 Báo Cáo Thống Kê", System.Drawing.Color.FromArgb(168, 85, 247), bw)

        ' ═══════════════════════════════════════════════════
        '   FORM
        ' ═══════════════════════════════════════════════════
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(243, 244, 246)
        Me.ClientSize = New System.Drawing.Size(1080, 860)
        Me.Controls.Add(Me.pnlScroll)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.MaximizeBox = True
        Me.MinimumSize = New System.Drawing.Size(900, 700)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AppStory – Trang Chủ"

        Me.pnlHeader.ResumeLayout(False)
        Me.pnlScroll.ResumeLayout(False)
        Me.flpBody.ResumeLayout(False)
        Me.pnlRoleBadge.ResumeLayout(False)
        Me.pnlRoleDescRow.ResumeLayout(False)
        Me.pnlAdminStats.ResumeLayout(False)
        Me.pnlAdminStats.PerformLayout()
        Me.flpAdminStatCards.ResumeLayout(False)
        Me.pnlStatTotal.ResumeLayout(False)
        Me.pnlStatActive.ResumeLayout(False)
        Me.pnlStatCompleted.ResumeLayout(False)
        Me.pnlStatOverdue.ResumeLayout(False)
        Me.pnlStatPlanning.ResumeLayout(False)
        Me.pnlEmployeeStats.ResumeLayout(False)
        Me.pnlEmployeeStats.PerformLayout()
        Me.flpEmployeeStatCards.ResumeLayout(False)
        Me.pnlEmpTotal.ResumeLayout(False)
        Me.pnlEmpInProgress.ResumeLayout(False)
        Me.pnlEmpDone.ResumeLayout(False)
        Me.pnlEmpDeadline.ResumeLayout(False)
        Me.pnlCardDetail.ResumeLayout(False)
        Me.pnlCardDetailHeader.ResumeLayout(False)
        CType(Me.dgvCardDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMenuRow.ResumeLayout(False)
        Me.flpMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Private Sub MakeStatCard(pnl As System.Windows.Forms.Panel,
                              lblCount As System.Windows.Forms.Label,
                              lblLabel As System.Windows.Forms.Label,
                              bg As System.Drawing.Color, countText As String,
                              labelText As String, labelFg As System.Drawing.Color)
        pnl.BackColor = bg
        pnl.Controls.Add(lblCount)
        pnl.Controls.Add(lblLabel)
        pnl.Cursor = System.Windows.Forms.Cursors.Hand
        pnl.Margin = New System.Windows.Forms.Padding(0, 0, 10, 0)
        pnl.Size = New System.Drawing.Size(196, 116)

        lblCount.AutoSize = False
        lblCount.Font = New System.Drawing.Font("Segoe UI", 26.0!, System.Drawing.FontStyle.Bold)
        lblCount.ForeColor = System.Drawing.Color.White
        lblCount.Location = New System.Drawing.Point(0, 6)
        lblCount.Size = New System.Drawing.Size(196, 58)
        lblCount.Text = countText
        lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

        lblLabel.AutoSize = False
        lblLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        lblLabel.ForeColor = labelFg
        lblLabel.Location = New System.Drawing.Point(2, 68)
        lblLabel.Size = New System.Drawing.Size(192, 38)
        lblLabel.Text = labelText
        lblLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        lblLabel.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
    End Sub

    Private Sub MakeMenuBtn(btn As System.Windows.Forms.Button, txt As String,
                             bg As System.Drawing.Color, w As Integer)
        btn.BackColor = bg
        btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        btn.ForeColor = System.Drawing.Color.White
        btn.Margin = New System.Windows.Forms.Padding(0, 0, 0, 6)
        btn.Size = New System.Drawing.Size(w, 40)
        btn.Text = txt
        btn.Cursor = System.Windows.Forms.Cursors.Hand
        btn.Visible = False
        btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        btn.Padding = New System.Windows.Forms.Padding(16, 0, 0, 0)
    End Sub

    ' ── Field declarations ──
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblAppTitle As System.Windows.Forms.Label
    Friend WithEvents lblDateTime As System.Windows.Forms.Label
    Friend WithEvents btnLogout As System.Windows.Forms.Button
    Friend WithEvents pnlScroll As System.Windows.Forms.Panel
    Friend WithEvents flpBody As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnlRoleBadge As System.Windows.Forms.Panel
    Friend WithEvents lblWelcome As System.Windows.Forms.Label
    Friend WithEvents lblRole As System.Windows.Forms.Label
    Friend WithEvents lblEmail As System.Windows.Forms.Label
    Friend WithEvents pnlRoleDescRow As System.Windows.Forms.Panel
    Friend WithEvents lblRoleDesc As System.Windows.Forms.Label
    ' Admin cards
    Friend WithEvents pnlAdminStats As System.Windows.Forms.Panel
    Friend WithEvents lblAdminStatsTitle As System.Windows.Forms.Label
    Friend WithEvents flpAdminStatCards As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnlStatTotal As System.Windows.Forms.Panel
    Friend WithEvents lblStatTotalCount As System.Windows.Forms.Label
    Friend WithEvents lblStatTotalLabel As System.Windows.Forms.Label
    Friend WithEvents pnlStatActive As System.Windows.Forms.Panel
    Friend WithEvents lblStatActiveCount As System.Windows.Forms.Label
    Friend WithEvents lblStatActiveLabel As System.Windows.Forms.Label
    Friend WithEvents pnlStatCompleted As System.Windows.Forms.Panel
    Friend WithEvents lblStatCompletedCount As System.Windows.Forms.Label
    Friend WithEvents lblStatCompletedLabel As System.Windows.Forms.Label
    Friend WithEvents pnlStatOverdue As System.Windows.Forms.Panel
    Friend WithEvents lblStatOverdueCount As System.Windows.Forms.Label
    Friend WithEvents lblStatOverdueLabel As System.Windows.Forms.Label
    Friend WithEvents pnlStatPlanning As System.Windows.Forms.Panel
    Friend WithEvents lblStatPlanningCount As System.Windows.Forms.Label
    Friend WithEvents lblStatPlanningLabel As System.Windows.Forms.Label
    ' Employee cards
    Friend WithEvents pnlEmployeeStats As System.Windows.Forms.Panel
    Friend WithEvents lblEmployeeStatsTitle As System.Windows.Forms.Label
    Friend WithEvents flpEmployeeStatCards As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnlEmpTotal As System.Windows.Forms.Panel
    Friend WithEvents lblEmpTotalCount As System.Windows.Forms.Label
    Friend WithEvents lblEmpTotalLabel As System.Windows.Forms.Label
    Friend WithEvents pnlEmpInProgress As System.Windows.Forms.Panel
    Friend WithEvents lblEmpInProgressCount As System.Windows.Forms.Label
    Friend WithEvents lblEmpInProgressLabel As System.Windows.Forms.Label
    Friend WithEvents pnlEmpDone As System.Windows.Forms.Panel
    Friend WithEvents lblEmpDoneCount As System.Windows.Forms.Label
    Friend WithEvents lblEmpDoneLabel As System.Windows.Forms.Label
    Friend WithEvents pnlEmpDeadline As System.Windows.Forms.Panel
    Friend WithEvents lblEmpDeadlineCount As System.Windows.Forms.Label
    Friend WithEvents lblEmpDeadlineLabel As System.Windows.Forms.Label
    ' Detail panel
    Friend WithEvents pnlCardDetail As System.Windows.Forms.Panel
    Friend WithEvents pnlCardDetailHeader As System.Windows.Forms.Panel
    Friend WithEvents lblCardDetailTitle As System.Windows.Forms.Label
    Friend WithEvents btnCloseDetail As System.Windows.Forms.Button
    Friend WithEvents dgvCardDetail As System.Windows.Forms.DataGridView
    ' Menu
    Friend WithEvents pnlMenuRow As System.Windows.Forms.Panel
    Friend WithEvents flpMenu As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnGoTasks As System.Windows.Forms.Button
    Friend WithEvents btnGoOpenTasks As System.Windows.Forms.Button
    Friend WithEvents btnGoMyTasks As System.Windows.Forms.Button
    Friend WithEvents btnGoMyTeams As System.Windows.Forms.Button
    Friend WithEvents btnGoProjects As System.Windows.Forms.Button
    Friend WithEvents btnGoTeams As System.Windows.Forms.Button
    Friend WithEvents btnGoApproval As System.Windows.Forms.Button
    Friend WithEvents btnGoReport As System.Windows.Forms.Button

End Class
