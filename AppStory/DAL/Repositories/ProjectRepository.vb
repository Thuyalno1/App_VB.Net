Imports MySql.Data.MySqlClient
Imports System.Configuration

Public Class ProjectRepository
    Implements IProjectRepository

    Private ReadOnly Property ConnectionString As String
        Get
            Return ConfigurationManager.ConnectionStrings("AppStoryDB").ConnectionString
        End Get
    End Property

    Private Function MapProject(reader As MySqlDataReader) As Project
        Dim p As New Project()
        p.ProjectId = reader.GetInt32("ProjectId")
        p.ProjectName = reader.GetString("ProjectName")
        p.Description = If(reader.IsDBNull(reader.GetOrdinal("Description")), "", reader.GetString("Description"))
        p.StartDate = If(reader.IsDBNull(reader.GetOrdinal("StartDate")), Nothing, CType(reader.GetDateTime("StartDate"), DateTime?))
        p.EndDate = If(reader.IsDBNull(reader.GetOrdinal("EndDate")), Nothing, CType(reader.GetDateTime("EndDate"), DateTime?))
        p.Status = reader.GetString("Status")
        p.ManagerId = If(reader.IsDBNull(reader.GetOrdinal("ManagerId")), Nothing, CType(reader.GetInt32("ManagerId"), Integer?))
        p.TeamId = If(reader.IsDBNull(reader.GetOrdinal("TeamId")), Nothing, CType(reader.GetInt32("TeamId"), Integer?))
        p.CreatedAt = reader.GetDateTime("CreatedAt")
        Return p
    End Function

    Public Function GetAll() As List(Of Project) Implements IProjectRepository.GetAll
        Dim list As New List(Of Project)()
        Dim sql As String = "SELECT * FROM Project ORDER BY CreatedAt DESC"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(MapProject(reader))
                    End While
                End Using
            End Using
        End Using
        Return list
    End Function

    Public Function GetById(projectId As Integer) As Project Implements IProjectRepository.GetById
        Dim sql As String = "SELECT * FROM Project WHERE ProjectId = @ProjectId"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@ProjectId", projectId)
                Using reader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Return MapProject(reader)
                    End If
                End Using
            End Using
        End Using
        Return Nothing
    End Function

    Public Function GetByManagerId(managerId As Integer) As List(Of Project) Implements IProjectRepository.GetByManagerId
        Dim list As New List(Of Project)()
        Dim sql As String = "SELECT * FROM Project WHERE ManagerId = @ManagerId ORDER BY CreatedAt DESC"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@ManagerId", managerId)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(MapProject(reader))
                    End While
                End Using
            End Using
        End Using
        Return list
    End Function

    Public Function GetByTeamId(teamId As Integer) As List(Of Project) Implements IProjectRepository.GetByTeamId
        Dim list As New List(Of Project)()
        Dim sql As String = "SELECT * FROM Project WHERE TeamId = @TeamId ORDER BY CreatedAt DESC"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@TeamId", teamId)
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        list.Add(MapProject(reader))
                    End While
                End Using
            End Using
        End Using
        Return list
    End Function

    Public Sub Insert(project As Project) Implements IProjectRepository.Insert
        Dim sql As String = "INSERT INTO Project (ProjectName, Description, StartDate, EndDate, Status, ManagerId, TeamId) 
                             VALUES (@ProjectName, @Description, @StartDate, @EndDate, @Status, @ManagerId, @TeamId)"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName)
                cmd.Parameters.AddWithValue("@Description", If(project.Description, ""))
                cmd.Parameters.AddWithValue("@StartDate", If(project.StartDate.HasValue, CObj(project.StartDate.Value), DBNull.Value))
                cmd.Parameters.AddWithValue("@EndDate", If(project.EndDate.HasValue, CObj(project.EndDate.Value), DBNull.Value))
                cmd.Parameters.AddWithValue("@Status", project.Status)
                cmd.Parameters.AddWithValue("@ManagerId", If(project.ManagerId.HasValue, CObj(project.ManagerId.Value), DBNull.Value))
                cmd.Parameters.AddWithValue("@TeamId", If(project.TeamId.HasValue, CObj(project.TeamId.Value), DBNull.Value))
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Update(project As Project) Implements IProjectRepository.Update
        Dim sql As String = "UPDATE Project SET ProjectName=@ProjectName, Description=@Description, 
                             StartDate=@StartDate, EndDate=@EndDate, Status=@Status, 
                             ManagerId=@ManagerId, TeamId=@TeamId WHERE ProjectId=@ProjectId"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@ProjectName", project.ProjectName)
                cmd.Parameters.AddWithValue("@Description", If(project.Description, ""))
                cmd.Parameters.AddWithValue("@StartDate", If(project.StartDate.HasValue, CObj(project.StartDate.Value), DBNull.Value))
                cmd.Parameters.AddWithValue("@EndDate", If(project.EndDate.HasValue, CObj(project.EndDate.Value), DBNull.Value))
                cmd.Parameters.AddWithValue("@Status", project.Status)
                cmd.Parameters.AddWithValue("@ManagerId", If(project.ManagerId.HasValue, CObj(project.ManagerId.Value), DBNull.Value))
                cmd.Parameters.AddWithValue("@TeamId", If(project.TeamId.HasValue, CObj(project.TeamId.Value), DBNull.Value))
                cmd.Parameters.AddWithValue("@ProjectId", project.ProjectId)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub Delete(projectId As Integer) Implements IProjectRepository.Delete
        Dim sql As String = "DELETE FROM Project WHERE ProjectId=@ProjectId"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@ProjectId", projectId)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class
