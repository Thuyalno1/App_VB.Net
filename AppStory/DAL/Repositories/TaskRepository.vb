Imports MySql.Data.MySqlClient
Imports System.Configuration

Public Class TaskRepository
    Implements ITaskRepository

    Private ReadOnly Property ConnectionString As String
        Get
            Return ConfigurationManager.ConnectionStrings("AppStoryDB").ConnectionString
        End Get
    End Property

    ''' <summary>Map một DataReader row thành đối tượng Task</summary>
    Private Function MapTask(reader As MySqlDataReader) As Task
        Dim t As New Task()
        t.TaskId = reader.GetInt32("TaskId")
        t.Title = reader.GetString("Title")
        t.Description = If(reader.IsDBNull(reader.GetOrdinal("Description")), "", reader.GetString("Description"))
        t.AssignedToUserId = If(reader.IsDBNull(reader.GetOrdinal("AssignedToUserId")), CType(Nothing, Integer?), reader.GetInt32("AssignedToUserId"))
        t.CreatedByUserId = reader.GetInt32("CreatedByUserId")
        t.Status = reader.GetString("Status")
        t.Priority = reader.GetString("Priority")
        t.CreatedAt = reader.GetDateTime("CreatedAt")
        t.DueDate = If(reader.IsDBNull(reader.GetOrdinal("DueDate")), Nothing, CType(reader.GetDateTime("DueDate"), DateTime?))
        t.IsDeleted = reader.GetBoolean("IsDeleted")
        t.ProjectId = If(reader.IsDBNull(reader.GetOrdinal("ProjectId")), Nothing, CType(reader.GetInt32("ProjectId"), Integer?))
        t.TeamId = If(reader.IsDBNull(reader.GetOrdinal("TeamId")), Nothing, CType(reader.GetInt32("TeamId"), Integer?))
        Return t
    End Function

    ''' <summary>Admin: lấy tất cả task chưa bị soft-delete</summary>
    Public Function GetAll() As List(Of Task) Implements ITaskRepository.GetAll
        Dim tasks As New List(Of Task)()
        Dim sql As String = "SELECT t.*, u.UserName AS AssignedUserName FROM Tasks t
                             LEFT JOIN Users u ON t.AssignedToUserId = u.UserId
                             WHERE t.IsDeleted = 0
                             ORDER BY t.CreatedAt DESC"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        tasks.Add(MapTask(reader))
                    End While
                End Using
            End Using
        End Using
        Return tasks
    End Function

    ''' <summary>Employee: chỉ lấy task được giao cho mình</summary>
    Public Function GetByUserId(userId As Integer) As List(Of Task) Implements ITaskRepository.GetByUserId
        Dim tasks As New List(Of Task)()
        Dim sql As String = "SELECT * FROM Tasks WHERE AssignedToUserId = @UserId AND IsDeleted = 0 ORDER BY CreatedAt DESC"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@UserId", userId)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        tasks.Add(MapTask(reader))
                    End While
                End Using
            End Using
        End Using
        Return tasks
    End Function

    ''' <summary>Lấy các task trong một Project cụ thể</summary>
    Public Function GetByProjectId(projectId As Integer) As List(Of Task) Implements ITaskRepository.GetByProjectId
        Dim tasks As New List(Of Task)()
        Dim sql As String = "SELECT * FROM Tasks WHERE ProjectId = @ProjectId AND IsDeleted = 0 ORDER BY CreatedAt DESC"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@ProjectId", projectId)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        tasks.Add(MapTask(reader))
                    End While
                End Using
            End Using
        End Using
        Return tasks
    End Function

    ''' <summary>Thêm task mới vào DB</summary>
    Public Sub Insert(task As Task) Implements ITaskRepository.Insert
        Dim sql As String = "INSERT INTO Tasks (Title, Description, AssignedToUserId, CreatedByUserId, Status, Priority, DueDate, ProjectId, TeamId)
                             VALUES (@Title, @Description, @AssignedToUserId, @CreatedByUserId, @Status, @Priority, @DueDate, @ProjectId, @TeamId)"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@Title", task.Title)
                cmd.Parameters.AddWithValue("@Description", If(task.Description, ""))
                cmd.Parameters.AddWithValue("@Status", task.Status)
                cmd.Parameters.AddWithValue("@Priority", task.Priority)
                cmd.Parameters.AddWithValue("@DueDate", If(task.DueDate.HasValue, CObj(task.DueDate.Value), DBNull.Value))
                cmd.Parameters.AddWithValue("@ProjectId", If(task.ProjectId.HasValue, CObj(task.ProjectId.Value), DBNull.Value))
                cmd.Parameters.AddWithValue("@TeamId", If(task.TeamId.HasValue, CObj(task.TeamId.Value), DBNull.Value))
                
                ' AssignedToUserId null check
                cmd.Parameters.AddWithValue("@AssignedToUserId", If(task.AssignedToUserId.HasValue, CObj(task.AssignedToUserId.Value), DBNull.Value))
                cmd.Parameters.AddWithValue("@CreatedByUserId", task.CreatedByUserId)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ''' <summary>Admin: cập nhật toàn bộ thông tin task</summary>
    Public Sub Update(task As Task) Implements ITaskRepository.Update
        Dim sql As String = "UPDATE Tasks SET Title=@Title, Description=@Description,
                             AssignedToUserId=@AssignedToUserId, Status=@Status,
                             Priority=@Priority, DueDate=@DueDate, ProjectId=@ProjectId, TeamId=@TeamId
                             WHERE TaskId=@TaskId AND IsDeleted=0"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@Title", task.Title)
                cmd.Parameters.AddWithValue("@Description", If(task.Description, ""))
                cmd.Parameters.AddWithValue("@Status", task.Status)
                cmd.Parameters.AddWithValue("@Priority", task.Priority)
                cmd.Parameters.AddWithValue("@DueDate", If(task.DueDate.HasValue, CObj(task.DueDate.Value), DBNull.Value))
                cmd.Parameters.AddWithValue("@ProjectId", If(task.ProjectId.HasValue, CObj(task.ProjectId.Value), DBNull.Value))
                cmd.Parameters.AddWithValue("@TeamId", If(task.TeamId.HasValue, CObj(task.TeamId.Value), DBNull.Value))
                
                cmd.Parameters.AddWithValue("@AssignedToUserId", If(task.AssignedToUserId.HasValue, CObj(task.AssignedToUserId.Value), DBNull.Value))
                
                cmd.Parameters.AddWithValue("@TaskId", task.TaskId)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ''' <summary>Employee: chỉ được sửa Status của task mình được giao</summary>
    Public Sub UpdateStatus(taskId As Integer, status As String) Implements ITaskRepository.UpdateStatus
        Dim sql As String = "UPDATE Tasks SET Status=@Status WHERE TaskId=@TaskId AND IsDeleted=0"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@Status", status)
                cmd.Parameters.AddWithValue("@TaskId", taskId)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ''' <summary>Soft Delete: đánh dấu IsDeleted=1, không xóa khỏi DB</summary>
    Public Sub Delete(taskId As Integer) Implements ITaskRepository.Delete
        Dim sql As String = "UPDATE Tasks SET IsDeleted=1 WHERE TaskId=@TaskId"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@TaskId", taskId)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ''' <summary>Lấy danh sách các task chưa được ấn định thuộc các nhóm của người dùng</summary>
    Public Function GetOpenTasksForUser(userId As Integer) As List(Of Task) Implements ITaskRepository.GetOpenTasksForUser
        Dim tasks As New List(Of Task)()
        Dim sql As String = "SELECT DISTINCT t.* FROM Tasks t
                             LEFT JOIN Project p ON t.ProjectId = p.ProjectId
                             INNER JOIN UserTeam ut ON (t.TeamId = ut.TeamId OR p.TeamId = ut.TeamId)
                             WHERE ut.UserId = @UserId 
                               AND t.AssignedToUserId IS NULL 
                               AND t.IsDeleted = 0
                             ORDER BY t.CreatedAt DESC"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@UserId", userId)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        tasks.Add(MapTask(reader))
                    End While
                End Using
            End Using
        End Using
        Return tasks
    End Function

    ''' <summary>Gán một task chưa có người làm cho một người dùng</summary>
    Public Sub ClaimTask(taskId As Integer, userId As Integer) Implements ITaskRepository.ClaimTask
        Dim sql As String = "UPDATE Tasks SET AssignedToUserId=@UserId, Status='In Progress' WHERE TaskId=@TaskId AND AssignedToUserId IS NULL AND IsDeleted=0"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@UserId", userId)
                cmd.Parameters.AddWithValue("@TaskId", taskId)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class
