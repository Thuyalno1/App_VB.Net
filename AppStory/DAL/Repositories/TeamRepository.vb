Imports System.Data.Odbc
Imports System.Configuration

Public Class TeamRepository
    Implements ITeamRepository

    Private ReadOnly Property ConnectionString As String
        Get
            Return ConfigurationManager.ConnectionStrings("AppStoryDB").ConnectionString
        End Get
    End Property

    Private Function MapTeam(reader As OdbcDataReader) As Team
        Dim t As New Team()
        t.TeamId = reader.GetInt32(reader.GetOrdinal("TeamId"))
        t.TeamName = reader.GetString(reader.GetOrdinal("TeamName"))
        Dim descOrd As Integer = reader.GetOrdinal("Description")
        t.Description = If(reader.IsDBNull(descOrd), "", reader.GetString(descOrd))
        t.CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
        Return t
    End Function

    Public Function GetAll() As List(Of Team) Implements ITeamRepository.GetAll
        Try
            Dim list As New List(Of Team)()
            Dim sql As String = "SELECT * FROM Team ORDER BY CreatedAt DESC"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    Using reader As OdbcDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapTeam(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        Catch ex As Exception
            Throw New DataAccessException("Không thể tải danh sách Nhóm từ cơ sở dữ liệu.", ex)
        End Try
    End Function

    Public Function GetById(teamId As Integer) As Team Implements ITeamRepository.GetById
        Try
            Dim sql As String = "SELECT * FROM Team WHERE TeamId = ?"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", teamId)
                    Using reader As OdbcDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return MapTeam(reader)
                        End If
                    End Using
                End Using
            End Using
            Throw New NotFoundException("Team", teamId)
        Catch ex As NotFoundException
            Throw
        Catch ex As Exception
            Throw New DataAccessException($"Không thể tải Nhóm ID={teamId}.", ex)
        End Try
    End Function

    Public Function GetTeamsByUserId(userId As Integer) As List(Of Team) Implements ITeamRepository.GetTeamsByUserId
        Try
            Dim list As New List(Of Team)()
            Dim sql As String = "SELECT t.* FROM Team t INNER JOIN UserTeam ut ON t.TeamId = ut.TeamId WHERE ut.UserId = ?"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", userId)
                    Using reader As OdbcDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapTeam(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        Catch ex As Exception
            Throw New DataAccessException($"Không thể tải danh sách Nhóm của UserId={userId}.", ex)
        End Try
    End Function

    Public Sub Insert(team As Team) Implements ITeamRepository.Insert
        Try
            Dim sql As String = "INSERT INTO Team (TeamName, Description) VALUES (?, ?)"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", team.TeamName)
                    cmd.Parameters.AddWithValue("?", If(team.Description, ""))
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New DataAccessException("Không thể thêm Nhóm mới vào cơ sở dữ liệu.", ex)
        End Try
    End Sub

    Public Sub Update(team As Team) Implements ITeamRepository.Update
        Try
            Dim sql As String = "UPDATE Team SET TeamName=?, Description=? WHERE TeamId=?"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", team.TeamName)
                    cmd.Parameters.AddWithValue("?", If(team.Description, ""))
                    cmd.Parameters.AddWithValue("?", team.TeamId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New DataAccessException($"Không thể cập nhật Nhóm ID={team.TeamId}.", ex)
        End Try
    End Sub

    Public Sub Delete(teamId As Integer) Implements ITeamRepository.Delete
        Try
            Dim sql As String = "DELETE FROM Team WHERE TeamId=?"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", teamId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New DataAccessException($"Không thể xóa Nhóm ID={teamId}.", ex)
        End Try
    End Sub

    Public Sub AddUserToTeam(userId As Integer, teamId As Integer, role As String) Implements ITeamRepository.AddUserToTeam
        Try
            ' ON DUPLICATE KEY UPDATE dùng thêm 1 tham số ? cho giá trị Role lặp lại
            Dim sql As String = "INSERT INTO UserTeam (UserId, TeamId, Role, JoinedAt) VALUES (?, ?, ?, NOW()) ON DUPLICATE KEY UPDATE Role=?"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", userId)
                    cmd.Parameters.AddWithValue("?", teamId)
                    cmd.Parameters.AddWithValue("?", role)
                    cmd.Parameters.AddWithValue("?", role) ' tham số thứ 4 cho ON DUPLICATE KEY UPDATE Role=?
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New DataAccessException($"Không thể thêm UserId={userId} vào TeamId={teamId}.", ex)
        End Try
    End Sub

    Public Sub RemoveUserFromTeam(userId As Integer, teamId As Integer) Implements ITeamRepository.RemoveUserFromTeam
        Try
            Dim sql As String = "DELETE FROM UserTeam WHERE UserId=? AND TeamId=?"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", userId)
                    cmd.Parameters.AddWithValue("?", teamId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New DataAccessException($"Không thể xóa UserId={userId} khỏi TeamId={teamId}.", ex)
        End Try
    End Sub

    Public Sub ClearUsersFromTeam(teamId As Integer) Implements ITeamRepository.ClearUsersFromTeam
        Try
            Dim sql As String = "DELETE FROM UserTeam WHERE TeamId=?"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", teamId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New DataAccessException($"Không thể xóa toàn bộ thành viên khỏi TeamId={teamId}.", ex)
        End Try
    End Sub

    Public Function GetUsersByTeamAndRole(teamId As Integer, role As String) As List(Of User) Implements ITeamRepository.GetUsersByTeamAndRole
        Try
            Dim list As New List(Of User)()
            Dim sql As String = "SELECT u.* FROM users u INNER JOIN UserTeam ut ON u.UserId = ut.UserId WHERE ut.TeamId = ? AND ut.Role = ?"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", teamId)
                    cmd.Parameters.AddWithValue("?", role)
                    Using reader As OdbcDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim u As New User()
                            u.UserId = reader.GetInt32(reader.GetOrdinal("UserId"))
                            u.UserName = reader.GetString(reader.GetOrdinal("UserName"))
                            u.PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"))
                            u.Email = reader.GetString(reader.GetOrdinal("Email"))
                            u.RoleId = reader.GetString(reader.GetOrdinal("RoleId"))
                            u.CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                            list.Add(u)
                        End While
                    End Using
                End Using
            End Using
            Return list
        Catch ex As Exception
            Throw New DataAccessException($"Không thể tải danh sách thành viên của TeamId={teamId}, Role={role}.", ex)
        End Try
    End Function

End Class
