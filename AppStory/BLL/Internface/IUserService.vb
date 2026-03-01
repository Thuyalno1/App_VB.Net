Public Interface IUserService

    ''' <summary>Đăng ký tài khoản mới</summary>
    Function Register(dto As RegisterDto) As (Success As Boolean, Message As String)
    ''' đầu vào  (dto As RegisterDto)  -> đầu ra  (Success As Boolean, Message As String)
    ''' 
    ''' <summary>Đăng nhập, trả về User nếu thành công</summary>
    Function Login(dto As LoginDto) As (Success As Boolean, UserData As User, Message As String)

End Interface
