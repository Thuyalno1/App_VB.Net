Imports MySql.Data.MySqlClient
Imports System.Configuration

Public Class TeamRepository
    Implements ITeamRepository

    Private ReadOnly Property ConnectionString As String
        Get
            Return ConfigurationManager.ConnectionStrings("AppStoryDB").ConnectionString
        End Get
    End Property

    Private Function MapTeam(reader As MySqlDataReader) As Team
        Dim t As New Team()
        t.TeamId = reader.GetInt32("TeamId")
        t.TeamName = reader.GetString("TeamName")
        t.Description = If(reader.IsDBNull(reader.GetOrdinal("Description")), "", reader.GetString("Description"))
        t.CreatedAt = reader.GetDateTime("CreatedAt")
        Return t
    End Function

    Public Function GetAll() As List(Of Team) Implements ITeamRepository.GetAll
        Dim list As New List(Of Team)()
        Dim sql As String = "SELECT * FROM Team ORDER BY CreatedAt DESC"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(MapTeam(reader))
                    End While
                End Using
            End Using
        End Using
        Return list
    End Function

    Public Function GetById(teamId As Integer) As Team Implements ITeamRepository.GetById
        Dim sql As String = "SELECT * FROM Team WHERE TeamId = @TeamId"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@TeamId", teamId)
                Using reader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Return MapTeam(reader)
                    End If
                End Using
            End Using
        End Using
        Return Nothing
    End Function

    Public Function GetTeamsByUserId(userId As Integer) As List(Of Team) Implements ITeamRepository.GetTeamsByUserId
        Dim list As New List(Of Team)()
        Dim sql As String = "SELECT t.* FROM Team t INNER JOIN UserTeam ut ON t.TeamId = ut.TeamId WHERE ut.UserId = @UserId"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@UserId", userId)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(MapTeam(reader))
                    End While
                End Using
            End Using
        End Using
        Return list
    End Function

    Public Sub Insert(team As Team) Implements ITeamRepository.Insert
        Dim sql As String = "INSERT INTO Team (TeamName, Description) VALUES (@TeamName, @Description)"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@TeamName", team.TeamName)
                cmd.Parameters.AddWithValue("@Description", If(team.Description, ""))
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Update(team As Team) Implements ITeamRepository.Update
        Dim sql As String = "UPDATE Team SET TeamName=@TeamName, Description=@Description WHERE TeamId=@TeamId"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@TeamName", team.TeamName)
                cmd.Parameters.AddWithValue("@Description", If(team.Description, ""))
                cmd.Parameters.AddWithValue("@TeamId", team.TeamId)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Delete(teamId As Integer) Implements ITeamRepository.Delete
        ' Note: In a real scenario, consider soft-delete or checking foreign keys
        Dim sql As String = "DELETE FROM Team WHERE TeamId=@TeamId"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@TeamId", teamId)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub AddUserToTeam(userId As Integer, teamId As Integer, role As String) Implements ITeamRepository.AddUserToTeam
        ' Tạm thời UPDATE nếu đã có, hoặc INSERT nếu chưa. 
        ' Tuy nhiên, dùng INSERT ... ON DUPLICATE KEY UPDATE là tốt nhất.
        Dim sql As String = "INSERT INTO UserTeam (UserId, TeamId, Role, JoinedAt) VALUES (@UserId, @TeamId, @Role, NOW()) ON DUPLICATE KEY UPDATE Role=@Role"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@UserId", userId)
                cmd.Parameters.AddWithValue("@TeamId", teamId)
                cmd.Parameters.AddWithValue("@Role", role)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub RemoveUserFromTeam(userId As Integer, teamId As Integer) Implements ITeamRepository.RemoveUserFromTeam
        Dim sql As String = "DELETE FROM UserTeam WHERE UserId=@UserId AND TeamId=@TeamId"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@UserId", userId)
                cmd.Parameters.AddWithValue("@TeamId", teamId)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub ClearUsersFromTeam(teamId As Integer) Implements ITeamRepository.ClearUsersFromTeam
        Dim sql As String = "DELETE FROM UserTeam WHERE TeamId=@TeamId"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@TeamId", teamId)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Function GetUsersByTeamAndRole(teamId As Integer, role As String) As List(Of User) Implements ITeamRepository.GetUsersByTeamAndRole
        Dim list As New List(Of User)()
        Dim sql As String = "SELECT u.* FROM users u INNER JOIN UserTeam ut ON u.UserId = ut.UserId WHERE ut.TeamId = @TeamId AND ut.Role = @Role"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@TeamId", teamId)
                cmd.Parameters.AddWithValue("@Role", role)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim u As New User()
                        u.UserId = reader.GetInt32("UserId")
                        u.UserName = reader.GetString("UserName")
                        u.PasswordHash = reader.GetString("PasswordHash")
                        u.Email = reader.GetString("Email")
                        u.RoleId = reader.GetString("RoleId")
                        u.CreatedAt = reader.GetDateTime("CreatedAt")
                        list.Add(u)
                    End While
                End Using
            End Using
        End Using
        Return list
    End Function

End Class
