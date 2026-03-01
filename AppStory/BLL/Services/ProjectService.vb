Public Class ProjectService
    Implements IProjectService

    Private ReadOnly _repo As IProjectRepository

    Public Sub New()
        _repo = New ProjectRepository()
    End Sub

    Public Function GetAllProjects() As List(Of Project) Implements IProjectService.GetAllProjects
        Return _repo.GetAll()
    End Function

    Public Function GetProjectById(projectId As Integer) As Project Implements IProjectService.GetProjectById
        Return _repo.GetById(projectId)
    End Function

    Public Function GetProjectsByManagerId(managerId As Integer) As List(Of Project) Implements IProjectService.GetProjectsByManagerId
        Return _repo.GetByManagerId(managerId)
    End Function

    Public Function GetProjectsByTeamId(teamId As Integer) As List(Of Project) Implements IProjectService.GetProjectsByTeamId
        Return _repo.GetByTeamId(teamId)
    End Function

    Public Function CreateProject(dto As ProjectDto) As (Success As Boolean, Message As String) Implements IProjectService.CreateProject
        If String.IsNullOrWhiteSpace(dto.ProjectName) Then
            Return (False, "Tên dự án không được để trống.")
        End If

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
    End Function

    Public Function UpdateProject(dto As ProjectDto) As (Success As Boolean, Message As String) Implements IProjectService.UpdateProject
        If String.IsNullOrWhiteSpace(dto.ProjectName) Then
            Return (False, "Tên dự án không được để trống.")
        End If

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
    End Function

    Public Function DeleteProject(projectId As Integer) As (Success As Boolean, Message As String) Implements IProjectService.DeleteProject
        _repo.Delete(projectId)
        Return (True, "Đã xóa dự án.")
    End Function

End Class
