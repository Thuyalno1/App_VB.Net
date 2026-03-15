Public Interface ITaskRepository

    ''' <summary>Lấy tất cả task chưa bị xóa (dành cho Admin)</summary>
    Function GetAll() As List(Of Task)

    ''' <summary>Lấy tất cả task đang chờ duyệt (Progress = 90%)</summary>
    Function GetPendingApprovalTasks() As List(Of Task)

    ''' <summary>Lấy task của một nhân viên cụ thể (dành cho Employee)</summary>
    Function GetByUserId(userId As Integer) As List(Of Task)

    ''' <summary>Lấy task thuộc một dự án</summary>
    Function GetByProjectId(projectId As Integer) As List(Of Task)

    ''' <summary>Thêm task mới</summary>
    Sub Insert(task As Task)

    ''' <summary>Lấy danh sách các task chưa được ấn định thuộc các nhóm của người dùng</summary>
    Function GetOpenTasksForUser(userId As Integer) As List(Of Task)

    ''' <summary>Gán một task chưa có người làm cho một người dùng</summary>
    Sub ClaimTask(taskId As Integer, userId As Integer)

    ''' <summary>Cập nhật toàn bộ thông tin task (Admin)</summary>
    Sub Update(task As Task)

    ''' <summary>Cập nhật tiến độ task (0-100%)</summary>
    Sub UpdateProgress(taskId As Integer, progress As Integer)

    ''' <summary>Soft Delete: đánh dấu IsDeleted = 1</summary>
    Sub Delete(taskId As Integer)

    ''' <summary>Lấy task thuộc một nhóm</summary>
    Function GetByTeamId(teamId As Integer) As List(Of Task)

    ''' <summary>Phê duyệt công việc (Admin/Manager)</summary>
    Sub ApproveTask(taskId As Integer)

End Interface

