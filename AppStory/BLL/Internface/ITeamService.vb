Public Interface ITeamService
    Function GetAllTeams() As List(Of TeamDto)
    Function GetTeamById(teamId As Integer) As TeamDto
    Function GetTeamsByUserId(userId As Integer) As List(Of Team)
    Function CreateTeam(dto As TeamDto) As (Success As Boolean, Message As String)
    Function UpdateTeam(dto As TeamDto) As (Success As Boolean, Message As String)
    Function DeleteTeam(teamId As Integer) As (Success As Boolean, Message As String)
    Function AddUserToTeam(userId As Integer, teamId As Integer, role As String) As (Success As Boolean, Message As String)
    Function RemoveUserFromTeam(userId As Integer, teamId As Integer) As (Success As Boolean, Message As String)
    Function IsUserTeamLeader(userId As Integer) As Boolean
End Interface
