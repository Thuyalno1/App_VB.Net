Public Interface ITeamRepository
    Function GetAll() As List(Of Team)
    Function GetById(teamId As Integer) As Team
    Function GetTeamsByUserId(userId As Integer) As List(Of Team)
    Sub Insert(team As Team)
    Sub Update(team As Team)
    Sub Delete(teamId As Integer)
    Sub AddUserToTeam(userId As Integer, teamId As Integer, role As String)
    Sub RemoveUserFromTeam(userId As Integer, teamId As Integer)
    Sub ClearUsersFromTeam(teamId As Integer)
    Function GetUsersByTeamAndRole(teamId As Integer, role As String) As List(Of User)
End Interface
