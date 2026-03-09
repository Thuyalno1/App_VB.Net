Imports System.Data.Odbc
Imports System.Configuration

Public Class UserRepository
    Implements IUserRepository

    ''' <summary>Lấy connection string từ App.config</summary>
    Private ReadOnly Property ConnectionString As String
        Get
            Return ConfigurationManager.ConnectionStrings("AppStoryDB").ConnectionString
        End Get
    End Property

    ''' <summary>Lưu User mới vào bảng Users</summary>
    Public Sub Register(user As User) Implements IUserRepository.Register
        Try
            Dim sql As String = "INSERT INTO Users (UserName, PasswordHash, Email, RoleId) VALUES (?, ?, ?, ?)"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", user.UserName)
                    cmd.Parameters.AddWithValue("?", user.PasswordHash)
                    cmd.Parameters.AddWithValue("?", user.Email)
                    cmd.Parameters.AddWithValue("?", user.RoleId)
                    cmd.ExecuteNonQuery()
                End Using
                ' Lấy UserId vừa được tạo
                Using cmdId As New OdbcCommand("SELECT LAST_INSERT_ID()", conn)
                    user.UserId = Convert.ToInt32(cmdId.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            Throw New DataAccessException("Không thể đăng ký tài khoản mới vào cơ sở dữ liệu.", ex)
        End Try
    End Sub

    ''' <summary>Tìm User theo username (không phân biệt chữ hoa/thường)</summary>
    Public Function FindByUsername(username As String) As User Implements IUserRepository.FindByUsername
        Try
            Dim sql As String = "SELECT UserId, UserName, PasswordHash, Email, RoleId, CreatedAt FROM Users WHERE LOWER(UserName) = LOWER(?) LIMIT 1"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", username)
                    Using reader As OdbcDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return New User() With {
                                .UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                .UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                .PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                                .Email = reader.GetString(reader.GetOrdinal("Email")),
                                .RoleId = reader.GetString(reader.GetOrdinal("RoleId")),
                                .CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                            }
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        Catch ex As Exception
            Throw New DataAccessException($"Không thể tìm kiếm tài khoản '{username}' trong cơ sở dữ liệu.", ex)
        End Try
    End Function

    ''' <summary>Kiểm tra username đã tồn tại chưa</summary>
    Public Function IsUsernameExist(username As String) As Boolean Implements IUserRepository.IsUsernameExist
        Try
            Dim sql As String = "SELECT COUNT(*) FROM Users WHERE LOWER(UserName) = LOWER(?)"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", username)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    Return count > 0
                End Using
            End Using
        Catch ex As Exception
            Throw New DataAccessException($"Không thể kiểm tra tên đăng nhập '{username}'.", ex)
        End Try
    End Function

    ''' <summary>Kiểm tra email đã tồn tại chưa</summary>
    Public Function IsEmailExist(email As String) As Boolean Implements IUserRepository.IsEmailExist
        Try
            Dim sql As String = "SELECT COUNT(*) FROM Users WHERE LOWER(Email) = LOWER(?)"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    cmd.Parameters.AddWithValue("?", email)
                    Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    Return count > 0
                End Using
            End Using
        Catch ex As Exception
            Throw New DataAccessException($"Không thể kiểm tra email '{email}'.", ex)
        End Try
    End Function

    ''' <summary>Lấy toàn bộ danh sách User (cho ComboBox giao việc)</summary>
    Public Function GetAll() As List(Of User) Implements IUserRepository.GetAll
        Try
            Dim users As New List(Of User)()
            Dim sql As String = "SELECT UserId, UserName, Email, RoleId FROM Users ORDER BY UserName"
            Using conn As New OdbcConnection(ConnectionString)
                conn.Open()
                Using cmd As New OdbcCommand(sql, conn)
                    Using reader As OdbcDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            users.Add(New User() With {
                                .UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                .UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                .Email = reader.GetString(reader.GetOrdinal("Email")),
                                .RoleId = reader.GetString(reader.GetOrdinal("RoleId"))
                            })
                        End While
                    End Using
                End Using
            End Using
            Return users
        Catch ex As Exception
            Throw New DataAccessException("Không thể tải danh sách người dùng từ cơ sở dữ liệu.", ex)
        End Try
    End Function

End Class
