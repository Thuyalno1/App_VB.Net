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
        ' Validation nghiệp vụ
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
                .Progress = dto.Progress,
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

        ' Validate progress range
        If dto.Progress < 0 OrElse dto.Progress > 100 Then
            Return (False, "Tiến độ phải nằm trong khoảng 0% đến 100%.")
        End If

        ' Progress is now allowed up to 100% for all roles

        Try
            Dim updTask As New Task() With {
                .TaskId = dto.TaskId,
                .Title = dto.Title.Trim(),
                .Description = If(dto.Description, ""),
                .AssignedToUserId = If(dto.AssignedToUserId.HasValue AndAlso dto.AssignedToUserId.Value > 0, dto.AssignedToUserId, CType(Nothing, Integer?)),
                .Progress = dto.Progress,
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

    Public Function UpdateProgress(taskId As Integer, progress As Integer) As (Success As Boolean, Message As String) Implements ITaskService.UpdateProgress
        ' Validate range
        If progress < 0 OrElse progress > 100 Then
            Return (False, "Tiến độ phải nằm trong khoảng 0% đến 100%.")
        End If

        ' Progress is now allowed up to 100% for all roles

        Try
            _repo.UpdateProgress(taskId, progress)
            Return (True, "Cập nhật tiến độ thành công!")
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

    Public Function GetTasksByTeamId(teamId As Integer) As List(Of Task) Implements ITaskService.GetTasksByTeamId
        Try
            Return _repo.GetByTeamId(teamId)
        Catch ex As DataAccessException
            Throw New BusinessException("Không thể tải danh sách công việc của nhóm. " & ex.Message, ex)
        End Try
    End Function

    Public Function ApproveTask(taskId As Integer) As (Success As Boolean, Message As String) Implements ITaskService.ApproveTask
        Try
            _repo.ApproveTask(taskId)
            Return (True, "Đã phê duyệt công việc!")
        Catch ex As DataAccessException
            Return (False, "Lỗi cơ sở dữ liệu: " & ex.Message)
        End Try
    End Function

End Class
