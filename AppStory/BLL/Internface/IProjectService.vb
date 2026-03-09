Public Interface IProjectService
    Function GetAllProjects() As List(Of Project)
    Function GetProjectById(projectId As Integer) As Project
    Function GetProjectsByManagerId(managerId As Integer) As List(Of Project)
    Function GetProjectsByTeamId(teamId As Integer) As List(Of Project)
    Function CreateProject(dto As ProjectDto) As (Success As Boolean, Message As String)
    Function UpdateProject(dto As ProjectDto) As (Success As Boolean, Message As String)
    Function DeleteProject(projectId As Integer) As (Success As Boolean, Message As String)
    Function GetTeamIdsByProjectId(projectId As Integer) As List(Of Integer)
End Interface
