Imports System.Data.Odbc
Imports System.Configuration

Public Class TaskRepository
    Implements ITaskRepository

    Private ReadOnly Property ConnectionString As String
        Get
            Return ConfigurationManager.ConnectionStrings("AppStoryDB").ConnectionString
        End Get
    End Property

    ''' <summary>Map một DataReader row thành đối tượng Task</summary>
    Private Function MapTask(reader As OdbcDataReader) As Task
        Dim t As New Task()
        t.TaskId = reader.GetInt32(reader.GetOrdinal("TaskId"))
        t.Title = reader.GetString(reader.GetOrdinal("Title"))
        Dim descOrd As Integer = reader.GetOrdinal("Description")
        t.Description = If(reader.IsDBNull(descOrd), "", reader.GetString(descOrd))
        Dim assignedOrd As Integer = reader.GetOrdinal("AssignedToUserId")
        t.AssignedToUserId = If(reader.IsDBNull(assignedOrd), CType(Nothing, Integer?), reader.GetInt32(assignedOrd))
        t.CreatedByUserId = reader.GetInt32(reader.GetOrdinal("CreatedByUserId"))
        t.Status = reader.GetString(reader.GetOrdinal("Status"))
        t.Priority = reader.GetString(reader.GetOrdinal("Priority"))
        t.CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
        Dim dueOrd As Integer = reader.GetOrdinal("DueDate")
        t.DueDate = If(reader.IsDBNull(dueOrd), Nothing, CType(reader.GetDateTime(dueOrd), DateTime?))
        Dim isDeletedOrd As Integer = reader.GetOrdinal("IsDeleted")
        t.IsDeleted = Convert.ToBoolean(reader.GetValue(isDeletedOrd))
        Dim projOrd As Integer = reader.GetOrdinal("ProjectId")
        t.ProjectId = If(reader.IsDBNull(projOrd), Nothing, CType(reader.GetInt32(projOrd), Integer?))
        Dim teamOrd As Integer = reader.GetOrdinal("TeamId")
        t.TeamId = If(reader.IsDBNull(teamOrd), Nothing, CType(reader.GetInt32(teamOrd), Integer?))
        ' Đọc AssignedUserName nếu có trong kết quả truy vấn (từ JOIN Users)
        Try
            Dim userNameOrd As Integer = reader.GetOrdinal("AssignedUserName")
            t.AssignedUserName = If(reader.IsDBNull(userNameOrd), "", reader.GetString(userNameOrd))
        Catch ex As IndexOutOfRangeException
            ' Cột không tồn tại trong truy vấn này, bỏ qua
        End Try
        Return t
    End Function

    ''' <summary>Admin: lấy tất cả task chưa bị soft-delete</summary>
    Public Function GetAll() As List(Of Task) Implements ITaskRepository.GetAll
        Try
            Dim tasks As New List(Of Task)()
            Dim sql As String = "SELECT t.*, u.UserName AS AssignedUserName FROM Tasks t
                                 LEFT JOIN Users u ON t.AssignedToUserId = u.UserId
                                 WHERE t.IsDeleted = 0
                                 ORDER BY t.CreatedAt DESC"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    Using reader As OdbcDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            tasks.Add(MapTask(reader))
                        End While
                    End Using
                End Using
            End Using
            Return tasks
        Catch ex As Exception
            Throw New DataAccessException("Không thể tải danh sách Task từ cơ sở dữ liệu.", ex)
        End Try
    End Function

    ''' <summary>Lấy tất cả task đang chờ duyệt</summary>
    Public Function GetPendingApprovalTasks() As List(Of Task) Implements ITaskRepository.GetPendingApprovalTasks
        Try
            Dim tasks As New List(Of Task)()
            Dim sql As String = "SELECT t.*, u.UserName AS AssignedUserName FROM Tasks t
                                 LEFT JOIN Users u ON t.AssignedToUserId = u.UserId
                                 WHERE t.IsDeleted = 0 AND t.Status = 'Chờ duyệt'
                                 ORDER BY t.CreatedAt DESC"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    Using reader As OdbcDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            tasks.Add(MapTask(reader))
                        End While
                    End Using
                End Using
            End Using
            Return tasks
        Catch ex As Exception
            Throw New DataAccessException("Không thể tải danh sách Task chờ duyệt từ cơ sở dữ liệu.", ex)
        End Try
    End Function

    ''' <summary>Employee: chỉ lấy task được giao cho mình</summary>
    Public Function GetByUserId(userId As Integer) As List(Of Task) Implements ITaskRepository.GetByUserId
        Try
            Dim tasks As New List(Of Task)()
            Dim sql As String = "SELECT * FROM Tasks WHERE AssignedToUserId = ? AND IsDeleted = 0 ORDER BY CreatedAt DESC"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", userId)
                    Using reader As OdbcDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            tasks.Add(MapTask(reader))
                        End While
                    End Using
                End Using
            End Using
            Return tasks
        Catch ex As Exception
            Throw New DataAccessException($"Không thể tải danh sách Task của UserId={userId}.", ex)
        End Try
    End Function

    ''' <summary>Lấy các task trong một Project cụ thể</summary>
    Public Function GetByProjectId(projectId As Integer) As List(Of Task) Implements ITaskRepository.GetByProjectId
        Try
            Dim tasks As New List(Of Task)()
            Dim sql As String = "SELECT * FROM Tasks WHERE ProjectId = ? AND IsDeleted = 0 ORDER BY CreatedAt DESC"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", projectId)
                    Using reader As OdbcDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            tasks.Add(MapTask(reader))
                        End While
                    End Using
                End Using
            End Using
            Return tasks
        Catch ex As Exception
            Throw New DataAccessException($"Không thể tải danh sách Task của ProjectId={projectId}.", ex)
        End Try
    End Function

    ''' <summary>Thêm task mới vào DB</summary>
    Public Sub Insert(task As Task) Implements ITaskRepository.Insert
        Try
            Dim sql As String = "INSERT INTO Tasks (Title, Description, AssignedToUserId, CreatedByUserId, Status, Priority, DueDate, ProjectId, TeamId)
                                 VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", task.Title)
                    cmd.Parameters.AddWithValue("?", If(task.Description, ""))
                    cmd.Parameters.AddWithValue("?", If(task.AssignedToUserId.HasValue, CObj(task.AssignedToUserId.Value), DBNull.Value))
                    cmd.Parameters.AddWithValue("?", task.CreatedByUserId)
                    cmd.Parameters.AddWithValue("?", task.Status)
                    cmd.Parameters.AddWithValue("?", task.Priority)
                    cmd.Parameters.AddWithValue("?", If(task.DueDate.HasValue, CObj(task.DueDate.Value), DBNull.Value))
                    cmd.Parameters.AddWithValue("?", If(task.ProjectId.HasValue, CObj(task.ProjectId.Value), DBNull.Value))
                    cmd.Parameters.AddWithValue("?", If(task.TeamId.HasValue, CObj(task.TeamId.Value), DBNull.Value))
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New DataAccessException("Không thể thêm Task mới vào cơ sở dữ liệu.", ex)
        End Try
    End Sub

    ''' <summary>Admin: cập nhật toàn bộ thông tin task</summary>
    Public Sub Update(task As Task) Implements ITaskRepository.Update
        Try
            Dim sql As String = "UPDATE Tasks SET Title=?, Description=?,
                                 AssignedToUserId=?, Status=?,
                                 Priority=?, DueDate=?, ProjectId=?, TeamId=?
                                 WHERE TaskId=? AND IsDeleted=0"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", task.Title)
                    cmd.Parameters.AddWithValue("?", If(task.Description, ""))
                    cmd.Parameters.AddWithValue("?", If(task.AssignedToUserId.HasValue, CObj(task.AssignedToUserId.Value), DBNull.Value))
                    cmd.Parameters.AddWithValue("?", task.Status)
                    cmd.Parameters.AddWithValue("?", task.Priority)
                    cmd.Parameters.AddWithValue("?", If(task.DueDate.HasValue, CObj(task.DueDate.Value), DBNull.Value))
                    cmd.Parameters.AddWithValue("?", If(task.ProjectId.HasValue, CObj(task.ProjectId.Value), DBNull.Value))
                    cmd.Parameters.AddWithValue("?", If(task.TeamId.HasValue, CObj(task.TeamId.Value), DBNull.Value))
                    cmd.Parameters.AddWithValue("?", task.TaskId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New DataAccessException($"Không thể cập nhật Task ID={task.TaskId}.", ex)
        End Try
    End Sub

    ''' <summary>Employee: chỉ được sửa Status của task mình được giao</summary>
    Public Sub UpdateStatus(taskId As Integer, status As String) Implements ITaskRepository.UpdateStatus
        Try
            Dim sql As String = "UPDATE Tasks SET Status=? WHERE TaskId=? AND IsDeleted=0"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", status)
                    cmd.Parameters.AddWithValue("?", taskId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New DataAccessException($"Không thể cập nhật trạng thái Task ID={taskId}.", ex)
        End Try
    End Sub

    ''' <summary>Soft Delete: đánh dấu IsDeleted=1, không xóa khỏi DB</summary>
    Public Sub Delete(taskId As Integer) Implements ITaskRepository.Delete
        Try
            Dim sql As String = "UPDATE Tasks SET IsDeleted=1 WHERE TaskId=?"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", taskId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New DataAccessException($"Không thể xóa Task ID={taskId}.", ex)
        End Try
    End Sub

    ''' <summary>Lấy danh sách các task chưa được ấn định thuộc các nhóm của người dùng</summary>
    Public Function GetOpenTasksForUser(userId As Integer) As List(Of Task) Implements ITaskRepository.GetOpenTasksForUser
        Try
            Dim tasks As New List(Of Task)()
            Dim sql As String = "SELECT DISTINCT t.* FROM Tasks t
                                 LEFT JOIN Project p ON t.ProjectId = p.ProjectId
                                 INNER JOIN UserTeam ut ON (t.TeamId = ut.TeamId OR p.TeamId = ut.TeamId)
                                 WHERE ut.UserId = ?
                                   AND t.AssignedToUserId IS NULL
                                   AND t.IsDeleted = 0
                                 ORDER BY t.CreatedAt DESC"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", userId)
                    Using reader As OdbcDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            tasks.Add(MapTask(reader))
                        End While
                    End Using
                End Using
            End Using
            Return tasks
        Catch ex As Exception
            Throw New DataAccessException($"Không thể tải danh sách Task mở cho UserId={userId}.", ex)
        End Try
    End Function

    ''' <summary>Gán một task chưa có người làm cho một người dùng</summary>
    Public Sub ClaimTask(taskId As Integer, userId As Integer) Implements ITaskRepository.ClaimTask
        Try
            Dim sql As String = "UPDATE Tasks SET AssignedToUserId=?, Status='Đang thực hiện' WHERE TaskId=? AND AssignedToUserId IS NULL AND IsDeleted=0"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", userId)
                    cmd.Parameters.AddWithValue("?", taskId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New DataAccessException($"Không thể nhận Task ID={taskId} cho UserId={userId}.", ex)
        End Try
    End Sub

End Class
