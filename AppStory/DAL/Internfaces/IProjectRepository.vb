Public Interface IProjectRepository
    Function GetAll() As List(Of Project)
    Function GetById(projectId As Integer) As Project
    Function GetByManagerId(managerId As Integer) As List(Of Project)
    Function GetByTeamId(teamId As Integer) As List(Of Project)
    Sub Insert(project As Project)
    Sub Update(project As Project)
    Sub Delete(projectId As Integer)
    Function GetTeamIdsByProjectId(projectId As Integer) As List(Of Integer)
    Sub AssignTeamsToProject(projectId As Integer, teamIds As List(Of Integer))
End Interface
