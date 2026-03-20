Public Class frmEmployeeDashboard
    Inherits System.Windows.Forms.Form

    Private ReadOnly _taskService As ITaskService
    Private _myTasks As List(Of Task)
    Private _activeFilter As String = "Assigned"

    Public Sub New()
        InitializeComponent()
        _taskService = New TaskService()
    End Sub

    Private Sub frmEmployeeDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupGrid()
        SetupCardClickHandlers()
        lblUserInfo.Text = $"👤 {SessionManager.CurrentUser.UserName} — {SessionManager.CurrentUser.RoleId}"
        LoadDashboard()
    End Sub

    Private Sub SetupGrid()
        dgvTasks.AutoGenerateColumns = False
        dgvTasks.Columns.Clear()
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Title", .HeaderText = "Tiêu Đề", .Width = 230})
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Progress", .HeaderText = "Tiến Độ (%)", .Width = 100})
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Priority", .HeaderText = "Ưu Tiên", .Width = 90})
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {
            .DataPropertyName = "DueDate",
            .HeaderText = "Deadline",
            .Width = 110,
            .DefaultCellStyle = New DataGridViewCellStyle() With {.Format = "dd/MM/yyyy"}
        })
        dgvTasks.Columns.Add(New DataGridViewTextBoxColumn() With {.DataPropertyName = "Description", .HeaderText = "Mô Tả", .Width = 200})
    End Sub

    Private Sub SetupCardClickHandlers()
        Dim cards() As Panel = {pnlCardAssigned, pnlCardInProgress, pnlCardDone, pnlCardDeadline}
        For Each card In cards
            AddHandler card.Click, AddressOf Card_Click
            For Each ctrl As Control In card.Controls
                AddHandler ctrl.Click, AddressOf CardChild_Click
                ctrl.Cursor = Cursors.Hand
            Next
        Next
    End Sub

    Private Sub LoadDashboard()
        Try
            _myTasks = _taskService.GetMyTasks(SessionManager.CurrentUser.UserId)
            If _myTasks Is Nothing Then _myTasks = New List(Of Task)()

            ' Tính toán thống kê
            Dim assigned As Integer = _myTasks.Count
            Dim inProgress As Integer = _myTasks.Where(Function(t) t.Progress >= 1 AndAlso t.Progress <= 89).Count()
            Dim done As Integer = _myTasks.Where(Function(t) t.Progress = 100).Count()
            Dim nearDeadline As Integer = _myTasks.Where(Function(t) _
                t.DueDate.HasValue AndAlso _
                t.DueDate.Value <= DateTime.Now.AddDays(3) AndAlso _
                t.DueDate.Value >= DateTime.Now.AddDays(-1) AndAlso _
                t.Progress < 100).Count()

            ' Cập nhật card
            lblCardAssignedCount.Text = assigned.ToString()
            lblCardInProgressCount.Text = inProgress.ToString()
            lblCardDoneCount.Text = done.ToString()
            lblCardDeadlineCount.Text = nearDeadline.ToString()

            ' Hiển thị mặc định
            ApplyFilter("Assigned")

        Catch ex As BusinessException
            MessageBox.Show("Lỗi tải dữ liệu: " & ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ApplyFilter(filter As String)
        If _myTasks Is Nothing Then Return

        _activeFilter = filter
        Dim filtered As List(Of Task) = Nothing

        Select Case filter
            Case "Assigned"
                filtered = _myTasks
                lblFilterInfo.Text = $"📋 Hiển thị: Tất cả công việc được giao ({_myTasks.Count})"
            Case "InProgress"
                filtered = _myTasks.Where(Function(t) t.Progress >= 1 AndAlso t.Progress <= 89).ToList()
                lblFilterInfo.Text = $"🔄 Hiển thị: Đang thực hiện ({filtered.Count})"
            Case "Done"
                filtered = _myTasks.Where(Function(t) t.Progress = 100).ToList()
                lblFilterInfo.Text = $"✅ Hiển thị: Hoàn thành ({filtered.Count})"
            Case "Deadline"
                filtered = _myTasks.Where(Function(t) _
                    t.DueDate.HasValue AndAlso _
                    t.DueDate.Value <= DateTime.Now.AddDays(3) AndAlso _
                    t.DueDate.Value >= DateTime.Now.AddDays(-1) AndAlso _
                    t.Progress < 100).ToList()
                lblFilterInfo.Text = $"⚠️ Hiển thị: Gần deadline / quá hạn ({filtered.Count})"
            Case Else
                filtered = _myTasks
        End Select

        dgvTasks.DataSource = Nothing
        dgvTasks.DataSource = filtered

        ' Highlight card
        HighlightSelectedCard(filter)
    End Sub

    Private Sub HighlightSelectedCard(tag As String)
        Dim cards() As Panel = {pnlCardAssigned, pnlCardInProgress, pnlCardDone, pnlCardDeadline}
        For Each card In cards
            Dim cardTag As String = If(card.Tag?.ToString(), "")
            card.BorderStyle = If(cardTag = tag, BorderStyle.FixedSingle, BorderStyle.None)
        Next
    End Sub

    ' ──────────────────────────────────────────────
    '   CARD CLICK HANDLERS
    ' ──────────────────────────────────────────────
    Private Sub Card_Click(sender As Object, e As EventArgs)
        Dim card As Panel = TryCast(sender, Panel)
        If card Is Nothing Then Return
        ApplyFilter(If(card.Tag?.ToString(), "Assigned"))
    End Sub

    Private Sub CardChild_Click(sender As Object, e As EventArgs)
        Dim ctrl As Control = TryCast(sender, Control)
        If ctrl IsNot Nothing AndAlso ctrl.Parent IsNot Nothing Then
            Card_Click(ctrl.Parent, e)
        End If
    End Sub

    ' ──────────────────────────────────────────────
    '   NAVIGATION
    ' ──────────────────────────────────────────────
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim mainForm As New frmMain()
        mainForm.Show()
        Me.Close()
    End Sub

End Class
