Public Interface ITaskService

    Function GetAllTasks() As List(Of Task)
    Function GetMyTasks(userId As Integer) As List(Of Task)
    Function GetTasksByProjectId(projectId As Integer) As List(Of Task)
    Function GetOpenTasksForUser(userId As Integer) As List(Of Task)
    Function ClaimTask(taskId As Integer, userId As Integer) As (Success As Boolean, Message As String)
    Function CreateTask(dto As TaskDto, createdByUserId As Integer) As (Success As Boolean, Message As String)
    Function UpdateTask(dto As TaskDto) As (Success As Boolean, Message As String)
    Function UpdateProgress(taskId As Integer, progress As Integer) As (Success As Boolean, Message As String)
    Function DeleteTask(taskId As Integer) As (Success As Boolean, Message As String)
    Function GetPendingApprovalTasks() As List(Of Task)
    Function GetTasksByTeamId(teamId As Integer) As List(Of Task)
    Function ApproveTask(taskId As Integer) As (Success As Boolean, Message As String)

End Interface

