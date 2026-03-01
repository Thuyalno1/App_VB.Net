Public Interface IUserRepository

    ''' <summary>Lưu một User mới vào danh sách</summary>
    ''' tại sao là sub , vì không cần trả về gì , chỉ thực hiện insert 
    Sub Register(user As User)

    ''' <summary>Tìm User theo username, trả Nothing nếu không có</summary>
    Function FindByUsername(username As String) As User
    ''' username → tham số truyền vào, -> Kiểu dữ liệu trả về là User. 

    ''' <summary>Kiểm tra username đã tồn tại chưa</summary>
    Function IsUsernameExist(username As String) As Boolean

    ''' <summary>Kiểm tra email đã tồn tại chưa</summary>
    Function IsEmailExist(email As String) As Boolean

    ''' <summary>Lấy toàn bộ danh sách User (dùng cho ComboBox)</summary>
    Function GetAll() As List(Of User)

End Interface
