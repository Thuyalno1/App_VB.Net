Public Class User

    ' Khai báo các trường đại diện cho người dùng
    Private _userId As Integer
    Private _userName As String
    Private _passwordHash As String
    Private _email As String
    Private _roleId As String
    Private _createdAt As DateTime

    Public Property UserId() As Integer
        Get
            Return _userId
        End Get
        Set(value As Integer)
            _userId = value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return _userName
        End Get
        Set(value As String)
            _userName = value
        End Set
    End Property

    Public Property PasswordHash() As String
        Get
            Return _passwordHash
        End Get
        Set(value As String)
            _passwordHash = value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _email
        End Get
        Set(value As String)
            _email = value
        End Set
    End Property

    Public Property RoleId() As String
        Get
            Return _roleId
        End Get
        Set(value As String)
            _roleId = value
        End Set
    End Property

    Public Property CreatedAt() As DateTime
        Get
            Return _createdAt
        End Get
        Set(value As DateTime)
            _createdAt = value
        End Set
    End Property

End Class
