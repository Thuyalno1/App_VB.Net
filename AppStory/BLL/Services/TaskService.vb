Public Class TaskService
    Implements ITaskService

    Private ReadOnly _repo As ITaskRepository

    Public Sub New()
        _repo = New TaskRepository()
    End Sub

    Public Function GetAllTasks() As List(Of Task) Implements ITaskService.GetAllTasks
        Try
            Return _repo.GetAll()
        Catch ex As DataAccessException
            Throw New BusinessException("Không thể tải danh sách công việc. " & ex.Message, ex)
        End Try
    End Function

    Public Function GetPendingApprovalTasks() As List(Of Task) Implements ITaskService.GetPendingApprovalTasks
        Try
            Return _repo.GetPendingApprovalTasks()
        Catch ex As DataAccessException
            Throw New BusinessException("Không thể tải danh sách công việc chờ duyệt. " & ex.Message, ex)
        End Try
    End Function

    Public Function GetMyTasks(userId As Integer) As List(Of Task) Implements ITaskService.GetMyTasks
        Try
            Return _repo.GetByUserId(userId)
        Catch ex As DataAccessException
            Throw New BusinessException("Không thể tải danh sách công việc của bạn. " & ex.Message, ex)
        End Try
    End Function

    Public Function GetTasksByProjectId(projectId As Integer) As List(Of Task) Implements ITaskService.GetTasksByProjectId
        Try
            Return _repo.GetByProjectId(projectId)
        Catch ex As DataAccessException
            Throw New BusinessException("Không thể tải danh sách công việc của dự án. " & ex.Message, ex)
        End Try
    End Function

    Public Function CreateTask(dto As TaskDto, createdByUserId As Integer) As (Success As Boolean, Message As String) Implements ITaskService.CreateTask
        ' Validation nghiệp vụ → BusinessException (trả về Result thay vì ném ra GUI)
        If String.IsNullOrWhiteSpace(dto.Title) Then
            Return (False, "Tiêu đề công việc không được để trống.")
        End If
        If (Not dto.AssignedToUserId.HasValue OrElse dto.AssignedToUserId.Value <= 0) AndAlso (Not dto.TeamId.HasValue OrElse dto.TeamId.Value <= 0) Then
            Return (False, "Vui lòng chọn Nhân viên được giao hoặc chọn Nhóm nhận việc.")
        End If

        Try
            Dim newTask As New Task() With {
                .Title = dto.Title.Trim(),
                .Description = If(dto.Description, ""),
                .AssignedToUserId = If(dto.AssignedToUserId.HasValue AndAlso dto.AssignedToUserId.Value > 0, dto.AssignedToUserId, CType(Nothing, Integer?)),
                .CreatedByUserId = createdByUserId,
                .Status = If(String.IsNullOrWhiteSpace(dto.Status), "Chờ xử lý", dto.Status),
                .Priority = If(String.IsNullOrWhiteSpace(dto.Priority), "Medium", dto.Priority),
                .DueDate = dto.DueDate,
                .ProjectId = If(dto.ProjectId.HasValue AndAlso dto.ProjectId.Value > 0, dto.ProjectId, CType(Nothing, Integer?)),
                .TeamId = If(dto.TeamId.HasValue AndAlso dto.TeamId.Value > 0, dto.TeamId, CType(Nothing, Integer?))
            }
            _repo.Insert(newTask)
            Return (True, "Tạo công việc thành công!")
        Catch ex As DataAccessException
            Return (False, "Lỗi cơ sở dữ liệu: " & ex.Message)
        End Try
    End Function

    Public Function UpdateTask(dto As TaskDto) As (Success As Boolean, Message As String) Implements ITaskService.UpdateTask
        If String.IsNullOrWhiteSpace(dto.Title) Then
            Return (False, "Tiêu đề công việc không được để trống.")
        End If

        ' Validate role: Employee cannot set task to "Đã hoàn thành"
        If dto.Status = "Đã hoàn thành" AndAlso SessionManager.CurrentUser IsNot Nothing Then
            Dim role = SessionManager.CurrentUser.RoleId
            If role = "Employee" Then
                Return (False, "Nhân viên không được quyền duyệt task thành 'Đã hoàn thành'. Vui lòng chọn 'Chờ duyệt'.")
            End If
        End If

        Try
            Dim updTask As New Task() With {
                .TaskId = dto.TaskId,
                .Title = dto.Title.Trim(),
                .Description = If(dto.Description, ""),
                .AssignedToUserId = If(dto.AssignedToUserId.HasValue AndAlso dto.AssignedToUserId.Value > 0, dto.AssignedToUserId, CType(Nothing, Integer?)),
                .Status = If(String.IsNullOrWhiteSpace(dto.Status), "Chờ xử lý", dto.Status),
                .Priority = If(String.IsNullOrWhiteSpace(dto.Priority), "Medium", dto.Priority),
                .DueDate = dto.DueDate,
                .ProjectId = If(dto.ProjectId.HasValue AndAlso dto.ProjectId.Value > 0, dto.ProjectId, CType(Nothing, Integer?)),
                .TeamId = If(dto.TeamId.HasValue AndAlso dto.TeamId.Value > 0, dto.TeamId, CType(Nothing, Integer?))
            }
            _repo.Update(updTask)
            Return (True, "Cập nhật công việc thành công!")
        Catch ex As DataAccessException
            Return (False, "Lỗi cơ sở dữ liệu: " & ex.Message)
        End Try
    End Function

    Public Function UpdateStatus(taskId As Integer, status As String) As (Success As Boolean, Message As String) Implements ITaskService.UpdateStatus
        Dim validStatuses = {"Chờ xử lý", "Đang thực hiện", "Chờ duyệt", "Đã hoàn thành"}
        If Not validStatuses.Contains(status) Then
            Return (False, "Trạng thái không hợp lệ.")
        End If

        ' Validate role: Employee cannot set task to "Đã hoàn thành"
        If status = "Đã hoàn thành" AndAlso SessionManager.CurrentUser IsNot Nothing Then
            Dim role = SessionManager.CurrentUser.RoleId
            If role = "Employee" Then
                Return (False, "Nhân viên không được quyền duyệt task thành 'Đã hoàn thành'. Vui lòng chọn 'Chờ duyệt'.")
            End If
        End If

        Try
            _repo.UpdateStatus(taskId, status)
            Return (True, "Cập nhật trạng thái thành công!")
        Catch ex As DataAccessException
            Return (False, "Lỗi cơ sở dữ liệu: " & ex.Message)
        End Try
    End Function

    Public Function DeleteTask(taskId As Integer) As (Success As Boolean, Message As String) Implements ITaskService.DeleteTask
        Try
            _repo.Delete(taskId)
            Return (True, "Đã xóa công việc (Soft Delete).")
        Catch ex As DataAccessException
            Return (False, "Lỗi cơ sở dữ liệu: " & ex.Message)
        End Try
    End Function

    Public Function GetOpenTasksForUser(userId As Integer) As List(Of Task) Implements ITaskService.GetOpenTasksForUser
        Try
            Return _repo.GetOpenTasksForUser(userId)
        Catch ex As DataAccessException
            Throw New BusinessException("Không thể tải danh sách việc mở. " & ex.Message, ex)
        End Try
    End Function

    Public Function ClaimTask(taskId As Integer, userId As Integer) As (Success As Boolean, Message As String) Implements ITaskService.ClaimTask
        Try
            _repo.ClaimTask(taskId, userId)
            Return (True, "Nhận việc thành công!")
        Catch ex As DataAccessException
            Return (False, "Lỗi cơ sở dữ liệu khi nhận việc: " & ex.Message)
        End Try
    End Function

End Class
