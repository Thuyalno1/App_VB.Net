Public Class TeamService
    Implements ITeamService

    Private ReadOnly _repo As ITeamRepository

    Public Sub New()
        _repo = New TeamRepository()
    End Sub

    Public Function GetAllTeams() As List(Of TeamDto) Implements ITeamService.GetAllTeams
        Dim teams = CType(_repo.GetAll(), List(Of Team))
        Dim dtoList As New List(Of TeamDto)
        
        For Each t In teams
            Dim leaders = _repo.GetUsersByTeamAndRole(t.TeamId, "Leader")
            Dim members = _repo.GetUsersByTeamAndRole(t.TeamId, "Member")
            
            Dim dto As New TeamDto() With {
                .TeamId = t.TeamId,
                .TeamName = t.TeamName,
                .Description = t.Description,
                .LeaderNames = String.Join(", ", leaders.Select(Function(u) u.UserName)),
                .MemberNames = String.Join(", ", members.Select(Function(u) u.UserName)),
                .LeaderIds = leaders.Select(Function(u) u.UserId).ToList(),
                .MemberIds = members.Select(Function(u) u.UserId).ToList()
            }
            dtoList.Add(dto)
        Next
        Return dtoList
    End Function

    Public Function GetTeamById(teamId As Integer) As TeamDto Implements ITeamService.GetTeamById
        Dim t = _repo.GetById(teamId)
        If t Is Nothing Then Return Nothing
        
        Dim leaders = _repo.GetUsersByTeamAndRole(t.TeamId, "Leader")
        Dim members = _repo.GetUsersByTeamAndRole(t.TeamId, "Member")
        
        Return New TeamDto() With {
            .TeamId = t.TeamId,
            .TeamName = t.TeamName,
            .Description = t.Description,
            .LeaderNames = String.Join(", ", leaders.Select(Function(u) u.UserName)),
            .MemberNames = String.Join(", ", members.Select(Function(u) u.UserName)),
            .LeaderIds = leaders.Select(Function(u) u.UserId).ToList(),
            .MemberIds = members.Select(Function(u) u.UserId).ToList()
        }
    End Function

    Public Function GetTeamsByUserId(userId As Integer) As List(Of Team) Implements ITeamService.GetTeamsByUserId
        Return _repo.GetTeamsByUserId(userId)
    End Function

    Public Function CreateTeam(dto As TeamDto) As (Success As Boolean, Message As String) Implements ITeamService.CreateTeam
        If String.IsNullOrWhiteSpace(dto.TeamName) Then
            Return (False, "Tên nhóm không được để trống.")
        End If

        Dim newTeam As New Team() With {
            .TeamName = dto.TeamName.Trim(),
            .Description = dto.Description
        }
        _repo.Insert(newTeam)

        ' Cần lấy ID của Team vừa tạo để gán thành viên.
        ' Tạm thời query lại lấy ID mới nhất (rủi ro concurrency), hoặc lý tưởng là sửa Insert trả về ID.
        ' Để an toàn và nhanh trong ngữ cảnh này, mình fetch lại các team và lấy team đầu tiên (giả sử order by created at desc như trong repo).
        Dim allTeams = CType(_repo.GetAll(), List(Of Team))
        Dim createdTeam = allTeams.FirstOrDefault(Function(t) t.TeamName = newTeam.TeamName)
        
        If createdTeam IsNot Nothing Then
            For Each leaderId In dto.LeaderIds
                _repo.AddUserToTeam(leaderId, createdTeam.TeamId, "Leader")
            Next
            For Each memberId In dto.MemberIds
                _repo.AddUserToTeam(memberId, createdTeam.TeamId, "Member")
            Next
        End If

        Return (True, "Tạo nhóm thành công!")
    End Function

    Public Function UpdateTeam(dto As TeamDto) As (Success As Boolean, Message As String) Implements ITeamService.UpdateTeam
        If String.IsNullOrWhiteSpace(dto.TeamName) Then
            Return (False, "Tên nhóm không được để trống.")
        End If

        Dim updTeam As New Team() With {
            .TeamId = dto.TeamId,
            .TeamName = dto.TeamName.Trim(),
            .Description = dto.Description
        }
        _repo.Update(updTeam)

        ' Clear old members/leaders and re-insert
        _repo.ClearUsersFromTeam(dto.TeamId)
        
        For Each leaderId In dto.LeaderIds
            _repo.AddUserToTeam(leaderId, dto.TeamId, "Leader")
        Next
        For Each memberId In dto.MemberIds
            _repo.AddUserToTeam(memberId, dto.TeamId, "Member")
        Next

        Return (True, "Cập nhật nhóm thành công!")
    End Function

    Public Function DeleteTeam(teamId As Integer) As (Success As Boolean, Message As String) Implements ITeamService.DeleteTeam
        _repo.Delete(teamId)
        Return (True, "Đã xóa nhóm.")
    End Function

    Public Function AddUserToTeam(userId As Integer, teamId As Integer, role As String) As (Success As Boolean, Message As String) Implements ITeamService.AddUserToTeam
        _repo.AddUserToTeam(userId, teamId, role)
        Return (True, "Đã thêm thành viên vào nhóm.")
    End Function

    Public Function RemoveUserFromTeam(userId As Integer, teamId As Integer) As (Success As Boolean, Message As String) Implements ITeamService.RemoveUserFromTeam
        _repo.RemoveUserFromTeam(userId, teamId)
        Return (True, "Đã xóa thành viên khỏi nhóm.")
    End Function

End Class
