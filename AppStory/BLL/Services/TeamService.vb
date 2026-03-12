Public Class TeamService
    Implements ITeamService

    Private ReadOnly _repo As ITeamRepository

    Public Sub New()
        _repo = New TeamRepository()
    End Sub

    Public Function GetAllTeams() As List(Of TeamDto) Implements ITeamService.GetAllTeams
        Try
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
        Catch ex As DataAccessException
            Throw New BusinessException("Không thể tải danh sách Nhóm. " & ex.Message, ex)
        End Try
    End Function

    Public Function GetTeamById(teamId As Integer) As TeamDto Implements ITeamService.GetTeamById
        Try
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
        Catch ex As NotFoundException
            Throw
        Catch ex As DataAccessException
            Throw New BusinessException("Không thể tải thông tin Nhóm. " & ex.Message, ex)
        End Try
    End Function

    Public Function GetTeamsByUserId(userId As Integer) As List(Of Team) Implements ITeamService.GetTeamsByUserId
        Try
            Return _repo.GetTeamsByUserId(userId)
        Catch ex As DataAccessException
            Throw New BusinessException("Không thể tải danh sách Nhóm của người dùng. " & ex.Message, ex)
        End Try
    End Function

    Public Function CreateTeam(dto As TeamDto) As (Success As Boolean, Message As String) Implements ITeamService.CreateTeam
        If String.IsNullOrWhiteSpace(dto.TeamName) Then
            Return (False, "Tên nhóm không được để trống.")
        End If

        Try
            Dim newTeam As New Team() With {
                .TeamName = dto.TeamName.Trim(),
                .Description = dto.Description
            }
            _repo.Insert(newTeam)

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
        Catch ex As DataAccessException
            Return (False, "Lỗi cơ sở dữ liệu: " & ex.Message)
        End Try
    End Function

    Public Function UpdateTeam(dto As TeamDto) As (Success As Boolean, Message As String) Implements ITeamService.UpdateTeam
        If String.IsNullOrWhiteSpace(dto.TeamName) Then
            Return (False, "Tên nhóm không được để trống.")
        End If

        Try
            Dim updTeam As New Team() With {
                .TeamId = dto.TeamId,
                .TeamName = dto.TeamName.Trim(),
                .Description = dto.Description
            }
            _repo.Update(updTeam)
            _repo.ClearUsersFromTeam(dto.TeamId)

            For Each leaderId In dto.LeaderIds
                _repo.AddUserToTeam(leaderId, dto.TeamId, "Leader")
            Next
            For Each memberId In dto.MemberIds
                _repo.AddUserToTeam(memberId, dto.TeamId, "Member")
            Next

            Return (True, "Cập nhật nhóm thành công!")
        Catch ex As DataAccessException
            Return (False, "Lỗi cơ sở dữ liệu: " & ex.Message)
        End Try
    End Function

    Public Function DeleteTeam(teamId As Integer) As (Success As Boolean, Message As String) Implements ITeamService.DeleteTeam
        Try
            _repo.Delete(teamId)
            Return (True, "Đã xóa nhóm.")
        Catch ex As DataAccessException
            Return (False, "Lỗi cơ sở dữ liệu: " & ex.Message)
        End Try
    End Function

    Public Function AddUserToTeam(userId As Integer, teamId As Integer, role As String) As (Success As Boolean, Message As String) Implements ITeamService.AddUserToTeam
        Try
            _repo.AddUserToTeam(userId, teamId, role)
            Return (True, "Đã thêm thành viên vào nhóm.")
        Catch ex As DataAccessException
            Return (False, "Lỗi cơ sở dữ liệu: " & ex.Message)
        End Try
    End Function

    Public Function RemoveUserFromTeam(userId As Integer, teamId As Integer) As (Success As Boolean, Message As String) Implements ITeamService.RemoveUserFromTeam
        Try
            _repo.RemoveUserFromTeam(userId, teamId)
            Return (True, "Đã xóa thành viên khỏi nhóm.")
        Catch ex As DataAccessException
            Return (False, "Lỗi cơ sở dữ liệu: " & ex.Message)
        End Try
    End Function

    Public Function IsUserTeamLeader(userId As Integer) As Boolean Implements ITeamService.IsUserTeamLeader
        Try
            Return _repo.IsLeader(userId)
        Catch ex As DataAccessException
            ' Log hoặc xử lý tùy ý, ở đây throw tiếp
            Throw
        End Try
    End Function

End Class
