Public Class ProjectService
    Implements IProjectService

    Private ReadOnly _repo As IProjectRepository

    Public Sub New()
        _repo = New ProjectRepository()
    End Sub

    Public Function GetAllProjects() As List(Of Project) Implements IProjectService.GetAllProjects
        Try
            Return _repo.GetAll()
        Catch ex As DataAccessException
            Throw New BusinessException("Không thể tải danh sách Dự án. " & ex.Message, ex)
        End Try
    End Function

    Public Function GetProjectById(projectId As Integer) As Project Implements IProjectService.GetProjectById
        Try
            Return _repo.GetById(projectId)
        Catch ex As NotFoundException
            Throw
        Catch ex As DataAccessException
            Throw New BusinessException("Không thể tải thông tin Dự án. " & ex.Message, ex)
        End Try
    End Function

    Public Function GetProjectsByManagerId(managerId As Integer) As List(Of Project) Implements IProjectService.GetProjectsByManagerId
        Try
            Return _repo.GetByManagerId(managerId)
        Catch ex As DataAccessException
            Throw New BusinessException("Không thể tải danh sách Dự án của Manager. " & ex.Message, ex)
        End Try
    End Function

    Public Function GetProjectsByTeamId(teamId As Integer) As List(Of Project) Implements IProjectService.GetProjectsByTeamId
        Try
            Return _repo.GetByTeamId(teamId)
        Catch ex As DataAccessException
            Throw New BusinessException("Không thể tải danh sách Dự án của Nhóm. " & ex.Message, ex)
        End Try
    End Function

    Public Function CreateProject(dto As ProjectDto) As (Success As Boolean, Message As String) Implements IProjectService.CreateProject
        If String.IsNullOrWhiteSpace(dto.ProjectName) Then
            Return (False, "Tên dự án không được để trống.")
        End If

        Try
            Dim newProject As New Project() With {
                .ProjectName = dto.ProjectName.Trim(),
                .Description = dto.Description,
                .StartDate = dto.StartDate,
                .EndDate = dto.EndDate,
                .Status = If(String.IsNullOrWhiteSpace(dto.Status), "Planning", dto.Status),
                .ManagerId = dto.ManagerId,
                .TeamId = dto.TeamId
            }
            _repo.Insert(newProject)
            Return (True, "Tạo dự án thành công!")
        Catch ex As DataAccessException
            Return (False, "Lỗi cơ sở dữ liệu: " & ex.Message)
        End Try
    End Function

    Public Function UpdateProject(dto As ProjectDto) As (Success As Boolean, Message As String) Implements IProjectService.UpdateProject
        If String.IsNullOrWhiteSpace(dto.ProjectName) Then
            Return (False, "Tên dự án không được để trống.")
        End If

        Try
            Dim updProject As New Project() With {
                .ProjectId = dto.ProjectId,
                .ProjectName = dto.ProjectName.Trim(),
                .Description = dto.Description,
                .StartDate = dto.StartDate,
                .EndDate = dto.EndDate,
                .Status = If(String.IsNullOrWhiteSpace(dto.Status), "Planning", dto.Status),
                .ManagerId = dto.ManagerId,
                .TeamId = dto.TeamId
            }
            _repo.Update(updProject)
            Return (True, "Cập nhật dự án thành công!")
        Catch ex As DataAccessException
            Return (False, "Lỗi cơ sở dữ liệu: " & ex.Message)
        End Try
    End Function

    Public Function DeleteProject(projectId As Integer) As (Success As Boolean, Message As String) Implements IProjectService.DeleteProject
        Try
            _repo.Delete(projectId)
            Return (True, "Đã xóa dự án.")
        Catch ex As DataAccessException
            Return (False, "Lỗi cơ sở dữ liệu: " & ex.Message)
        End Try
    End Function

End Class
