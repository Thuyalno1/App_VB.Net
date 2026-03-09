Imports System.Data.Odbc
Imports System.Configuration
Imports System.Data

Public Class ProjectRepository
    Implements IProjectRepository

    Private ReadOnly Property ConnectionString As String
        Get
            Return ConfigurationManager.ConnectionStrings("AppStoryDB").ConnectionString
        End Get
    End Property

    Private Function MapProject(reader As OdbcDataReader) As Project
        Dim p As New Project()
        p.ProjectId = reader.GetInt32(reader.GetOrdinal("ProjectId"))
        p.ProjectName = reader.GetString(reader.GetOrdinal("ProjectName"))
        Dim descOrd As Integer = reader.GetOrdinal("Description")
        p.Description = If(reader.IsDBNull(descOrd), "", reader.GetString(descOrd))
        Dim startOrd As Integer = reader.GetOrdinal("StartDate")
        p.StartDate = If(reader.IsDBNull(startOrd), Nothing, CType(reader.GetDateTime(startOrd), DateTime?))
        Dim endOrd As Integer = reader.GetOrdinal("EndDate")
        p.EndDate = If(reader.IsDBNull(endOrd), Nothing, CType(reader.GetDateTime(endOrd), DateTime?))
        p.Status = reader.GetString(reader.GetOrdinal("Status"))
        Dim manOrd As Integer = reader.GetOrdinal("ManagerId")
        p.ManagerId = If(reader.IsDBNull(manOrd), Nothing, CType(reader.GetInt32(manOrd), Integer?))
        p.CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
        Return p
    End Function

    Public Function GetAll() As List(Of Project) Implements IProjectRepository.GetAll
        Try
            Dim list As New List(Of Project)()
            Dim sql As String = "SELECT * FROM Project ORDER BY CreatedAt DESC"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    Using reader As OdbcDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapProject(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        Catch ex As Exception
            Throw New DataAccessException("Không thể tải danh sách Dự án từ cơ sở dữ liệu.", ex)
        End Try
    End Function

    Public Function GetById(projectId As Integer) As Project Implements IProjectRepository.GetById
        Try
            Dim sql As String = "SELECT * FROM Project WHERE ProjectId = ?"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", projectId)
                    Using reader As OdbcDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return MapProject(reader)
                        End If
                    End Using
                End Using
            End Using
            Throw New NotFoundException("Project", projectId)
        Catch ex As NotFoundException
            Throw
        Catch ex As Exception
            Throw New DataAccessException($"Không thể tải Dự án ID={projectId}.", ex)
        End Try
    End Function

    Public Function GetByManagerId(managerId As Integer) As List(Of Project) Implements IProjectRepository.GetByManagerId
        Try
            Dim list As New List(Of Project)()
            Dim sql As String = "SELECT * FROM Project WHERE ManagerId = ? ORDER BY CreatedAt DESC"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", managerId)
                    Using reader As OdbcDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapProject(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        Catch ex As Exception
            Throw New DataAccessException($"Không thể tải danh sách Dự án của ManagerId={managerId}.", ex)
        End Try
    End Function

    Public Function GetByTeamId(teamId As Integer) As List(Of Project) Implements IProjectRepository.GetByTeamId
        Try
            Dim list As New List(Of Project)()
            Dim sql As String = "SELECT p.* FROM Project p INNER JOIN ProjectTeam pt ON p.ProjectId = pt.ProjectId WHERE pt.TeamId = ? ORDER BY p.CreatedAt DESC"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", teamId)
                    Using reader As OdbcDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(MapProject(reader))
                        End While
                    End Using
                End Using
            End Using
            Return list
        Catch ex As Exception
            Throw New DataAccessException($"Không thể tải danh sách Dự án của TeamId={teamId}.", ex)
        End Try
    End Function

    Public Sub Insert(project As Project) Implements IProjectRepository.Insert
        Try
            Dim sql As String = "INSERT INTO Project (ProjectName, Description, StartDate, EndDate, Status, ManagerId)
                                 VALUES (?, ?, ?, ?, ?, ?)"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", project.ProjectName)
                    cmd.Parameters.AddWithValue("?", If(project.Description, ""))
                    cmd.Parameters.AddWithValue("?", If(project.StartDate.HasValue, CObj(project.StartDate.Value), DBNull.Value))
                    cmd.Parameters.AddWithValue("?", If(project.EndDate.HasValue, CObj(project.EndDate.Value), DBNull.Value))
                    cmd.Parameters.AddWithValue("?", project.Status)
                    cmd.Parameters.AddWithValue("?", If(project.ManagerId.HasValue, CObj(project.ManagerId.Value), DBNull.Value))
                    cmd.ExecuteNonQuery()
                End Using
                
                Using cmdId As New OdbcCommand("SELECT LAST_INSERT_ID()", conn)
                    project.ProjectId = Convert.ToInt32(cmdId.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            Throw New DataAccessException("Không thể thêm Dự án mới vào cơ sở dữ liệu.", ex)
        End Try
    End Sub

    Public Sub Update(project As Project) Implements IProjectRepository.Update
        Try
            Dim sql As String = "UPDATE Project SET ProjectName=?, Description=?,
                                 StartDate=?, EndDate=?, Status=?,
                                 ManagerId=? WHERE ProjectId=?"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", project.ProjectName)
                    cmd.Parameters.AddWithValue("?", If(project.Description, ""))
                    cmd.Parameters.AddWithValue("?", If(project.StartDate.HasValue, CObj(project.StartDate.Value), DBNull.Value))
                    cmd.Parameters.AddWithValue("?", If(project.EndDate.HasValue, CObj(project.EndDate.Value), DBNull.Value))
                    cmd.Parameters.AddWithValue("?", project.Status)
                    cmd.Parameters.AddWithValue("?", If(project.ManagerId.HasValue, CObj(project.ManagerId.Value), DBNull.Value))
                    cmd.Parameters.AddWithValue("?", project.ProjectId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New DataAccessException($"Không thể cập nhật Dự án ID={project.ProjectId}.", ex)
        End Try
    End Sub

    Public Sub Delete(projectId As Integer) Implements IProjectRepository.Delete
        Try
            Dim sql As String = "DELETE FROM Project WHERE ProjectId=?"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", projectId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Throw New DataAccessException($"Không thể xóa Dự án ID={projectId}.", ex)
        End Try
    End Sub

    Public Function GetTeamIdsByProjectId(projectId As Integer) As List(Of Integer) Implements IProjectRepository.GetTeamIdsByProjectId
        Try
            Dim list As New List(Of Integer)()
            Dim sql As String = "SELECT TeamId FROM ProjectTeam WHERE ProjectId = ?"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", projectId)
                    Using reader As OdbcDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            list.Add(reader.GetInt32(0))
                        End While
                    End Using
                End Using
            End Using
            Return list
        Catch ex As Exception
            Throw New DataAccessException($"Không thể tải danh sách Nhóm của Dự án ID={projectId}.", ex)
        End Try
    End Function

    Public Sub AssignTeamsToProject(projectId As Integer, teamIds As List(Of Integer)) Implements IProjectRepository.AssignTeamsToProject
        Dim conn As OdbcConnection = Nothing
        Dim trans As OdbcTransaction = Nothing
        Try
            conn = New OdbcConnection(ConnectionString)
            conn.Open()
            trans = conn.BeginTransaction()

            ' 1. Delete old teams
            Dim sqlDelete As String = "DELETE FROM ProjectTeam WHERE ProjectId = ?"
            Using cmdDel As New OdbcCommand(sqlDelete, conn, trans)
                cmdDel.Parameters.AddWithValue("?", projectId)
                cmdDel.ExecuteNonQuery()
            End Using

            ' 2. Insert new teams
            If teamIds IsNot Nothing AndAlso teamIds.Count > 0 Then
                Dim sqlInsert As String = "INSERT INTO ProjectTeam (ProjectId, TeamId) VALUES (?, ?)"
                Using cmdIns As New OdbcCommand(sqlInsert, conn, trans)
                    For Each tId In teamIds
                        cmdIns.Parameters.Clear()
                        cmdIns.Parameters.AddWithValue("?", projectId)
                        cmdIns.Parameters.AddWithValue("?", tId)
                        cmdIns.ExecuteNonQuery()
                    Next
                End Using
            End If

            trans.Commit()
        Catch ex As Exception
            If trans IsNot Nothing Then trans.Rollback()
            Throw New DataAccessException($"Lỗi khi gán nhóm cho Dự án ID={projectId}.", ex)
        Finally
            If trans IsNot Nothing Then trans.Dispose()
            If conn IsNot Nothing Then
                If conn.State = ConnectionState.Open Then conn.Close()
                conn.Dispose()
            End If
        End Try
    End Sub

End Class
