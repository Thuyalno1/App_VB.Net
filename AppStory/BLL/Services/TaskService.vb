Public Class TaskService
    Implements ITaskService

    Private ReadOnly _repo As ITaskRepository

    Public Sub New()
        _repo = New TaskRepository()
    End Sub

    Public Function GetAllTasks() As List(Of Task) Implements ITaskService.GetAllTasks
        Return _repo.GetAll()
    End Function

    Public Function GetMyTasks(userId As Integer) As List(Of Task) Implements ITaskService.GetMyTasks
        Return _repo.GetByUserId(userId)
    End Function

    Public Function GetTasksByProjectId(projectId As Integer) As List(Of Task) Implements ITaskService.GetTasksByProjectId
        Return _repo.GetByProjectId(projectId)
    End Function

    Public Function CreateTask(dto As TaskDto, createdByUserId As Integer) As (Success As Boolean, Message As String) Implements ITaskService.CreateTask
        If String.IsNullOrWhiteSpace(dto.Title) Then
            Return (False, "Tiêu đề công việc không được để trống.")
        End If
        If (Not dto.AssignedToUserId.HasValue OrElse dto.AssignedToUserId.Value <= 0) AndAlso (Not dto.TeamId.HasValue OrElse dto.TeamId.Value <= 0) Then
            Return (False, "Vui lòng chọn Nhân viên được giao hoặc chọn Nhóm nhận việc.")
        End If

        Dim newTask As New Task() With {
            .Title = dto.Title.Trim(),
            .Description = If(dto.Description, ""),
            .AssignedToUserId = If(dto.AssignedToUserId.HasValue AndAlso dto.AssignedToUserId.Value > 0, dto.AssignedToUserId, CType(Nothing, Integer?)),
            .CreatedByUserId = createdByUserId,
            .Status = If(String.IsNullOrWhiteSpace(dto.Status), "Pending", dto.Status),
            .Priority = If(String.IsNullOrWhiteSpace(dto.Priority), "Medium", dto.Priority),
            .DueDate = dto.DueDate,
            .ProjectId = If(dto.ProjectId.HasValue AndAlso dto.ProjectId.Value > 0, dto.ProjectId, CType(Nothing, Integer?)),
            .TeamId = If(dto.TeamId.HasValue AndAlso dto.TeamId.Value > 0, dto.TeamId, CType(Nothing, Integer?))
        }
        _repo.Insert(newTask)
        Return (True, "Tạo công việc thành công!")
    End Function

    Public Function UpdateTask(dto As TaskDto) As (Success As Boolean, Message As String) Implements ITaskService.UpdateTask
        If String.IsNullOrWhiteSpace(dto.Title) Then
            Return (False, "Tiêu đề công việc không được để trống.")
        End If

        Dim updTask As New Task() With {
            .TaskId = dto.TaskId,
            .Title = dto.Title.Trim(),
            .Description = If(dto.Description, ""),
            .AssignedToUserId = If(dto.AssignedToUserId.HasValue AndAlso dto.AssignedToUserId.Value > 0, dto.AssignedToUserId, CType(Nothing, Integer?)),
            .Status = If(String.IsNullOrWhiteSpace(dto.Status), "Pending", dto.Status),
            .Priority = If(String.IsNullOrWhiteSpace(dto.Priority), "Medium", dto.Priority),
            .DueDate = dto.DueDate,
            .ProjectId = If(dto.ProjectId.HasValue AndAlso dto.ProjectId.Value > 0, dto.ProjectId, CType(Nothing, Integer?)),
            .TeamId = If(dto.TeamId.HasValue AndAlso dto.TeamId.Value > 0, dto.TeamId, CType(Nothing, Integer?))
        }
        _repo.Update(updTask)
        Return (True, "Cập nhật công việc thành công!")
    End Function

    Public Function UpdateStatus(taskId As Integer, status As String) As (Success As Boolean, Message As String) Implements ITaskService.UpdateStatus
        Dim validStatuses = {"Pending", "In Progress", "Completed"}
        If Not validStatuses.Contains(status) Then
            Return (False, "Trạng thái không hợp lệ.")
        End If
        _repo.UpdateStatus(taskId, status)
        Return (True, "Cập nhật trạng thái thành công!")
    End Function

    Public Function DeleteTask(taskId As Integer) As (Success As Boolean, Message As String) Implements ITaskService.DeleteTask
        _repo.Delete(taskId)
        Return (True, "Đã xóa công việc (Soft Delete).")
    End Function

    Public Function GetOpenTasksForUser(userId As Integer) As List(Of Task) Implements ITaskService.GetOpenTasksForUser
        Return _repo.GetOpenTasksForUser(userId)
    End Function

    Public Function ClaimTask(taskId As Integer, userId As Integer) As (Success As Boolean, Message As String) Implements ITaskService.ClaimTask
        Try
            _repo.ClaimTask(taskId, userId)
            Return (True, "Nhận việc thành công!")
        Catch ex As Exception
            Return (False, "Lỗi khi nhận việc: " & ex.Message)
        End Try
    End Function

End Class
