Public Class UserService
    Implements IUserService

    Private ReadOnly _userRepository As IUserRepository

    Public Sub New()
        _userRepository = New UserRepository()
    End Sub

    Public Sub New(repo As IUserRepository)
        _userRepository = repo
    End Sub

    ''' <summary>Đăng ký tài khoản mới với các validation cần thiết</summary>
    Public Function Register(dto As RegisterDto) As (Success As Boolean, Message As String) Implements IUserService.Register
        ' Validation nghiệp vụ
        If String.IsNullOrWhiteSpace(dto.UserName) Then
            Return (False, "Tên đăng nhập không được để trống.")
        End If
        If String.IsNullOrWhiteSpace(dto.Email) Then
            Return (False, "Email không được để trống.")
        End If
        If String.IsNullOrWhiteSpace(dto.Password) Then
            Return (False, "Mật khẩu không được để trống.")
        End If
        If dto.Password.Length < 6 Then
            Return (False, "Mật khẩu phải có ít nhất 6 ký tự.")
        End If
        If dto.Password <> dto.ConfirmPassword Then
            Return (False, "Xác nhận mật khẩu không khớp.")
        End If

        Try
            If _userRepository.IsUsernameExist(dto.UserName) Then
                Return (False, "Tên đăng nhập đã được sử dụng.")
            End If
            If _userRepository.IsEmailExist(dto.Email) Then
                Return (False, "Email đã được sử dụng.")
            End If

            Dim role As String = If(String.IsNullOrWhiteSpace(dto.RoleId), "Employee", dto.RoleId)
            Dim newUser As New User() With {
                .UserName = dto.UserName.Trim(),
                .Email = dto.Email.Trim().ToLower(),
                .PasswordHash = PasswordHelper.HashPassword(dto.Password),
                .RoleId = role
            }
            _userRepository.Register(newUser)
            Return (True, "Đăng ký thành công!")
        Catch ex As DataAccessException
            Return (False, "Lỗi cơ sở dữ liệu: " & ex.Message)
        End Try
    End Function

    ''' <summary>Đăng nhập: kiểm tra username, so sánh hash, trả về User nếu hợp lệ</summary>
    Public Function Login(dto As LoginDto) As (Success As Boolean, UserData As User, Message As String) Implements IUserService.Login
        If String.IsNullOrWhiteSpace(dto.UserName) OrElse String.IsNullOrWhiteSpace(dto.Password) Then
            Return (False, Nothing, "Vui lòng nhập đầy đủ thông tin.")
        End If

        Try
            Dim existingUser As User = _userRepository.FindByUsername(dto.UserName)
            If existingUser Is Nothing Then
                Return (False, Nothing, "Tên đăng nhập không tồn tại.")
            End If

            If Not PasswordHelper.VerifyPassword(dto.Password, existingUser.PasswordHash) Then
                Return (False, Nothing, "Mật khẩu không chính xác.")
            End If

            Return (True, existingUser, "Đăng nhập thành công!")
        Catch ex As DataAccessException
            Return (False, Nothing, "Lỗi cơ sở dữ liệu: " & ex.Message)
        End Try
    End Function

End Class
