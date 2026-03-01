Imports MySql.Data.MySqlClient
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
        '' đây là câu lệnh thêm dữ liệu vào bảng 
        Dim sql As String = "INSERT INTO Users (UserName, PasswordHash, Email, RoleId) VALUES (@UserName, @PasswordHash, @Email, @RoleId)"

        Using conn As New MySqlConnection(ConnectionString)
            conn.Open() '' Mở đường kết nối tới MySQL Server.
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@UserName", user.UserName)
                ''Gán tham số @UserName → giá trị thực tế 
                '' vd @UserName = "admin" ,INSERT INTO Users (...) VALUES ('admin', ...)
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash)
                cmd.Parameters.AddWithValue("@Email", user.Email)
                cmd.Parameters.AddWithValue("@RoleId", user.RoleId)
                cmd.ExecuteNonQuery()

                ' Lấy UserId vừa được tạo
                cmd.CommandText = "SELECT LAST_INSERT_ID()"
                user.UserId = Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Sub

    ''' <summary>Tìm User theo username (không phân biệt chữ hoa/thường)</summary>
    Public Function FindByUsername(username As String) As User Implements IUserRepository.FindByUsername
        Dim sql As String = "SELECT UserId, UserName, PasswordHash, Email, RoleId, CreatedAt FROM Users WHERE LOWER(UserName) = LOWER(@UserName) LIMIT 1"

        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@UserName", username)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Return New User() With {
                            .UserId = reader.GetInt32("UserId"),
                            .UserName = reader.GetString("UserName"),
                            .PasswordHash = reader.GetString("PasswordHash"),
                            .Email = reader.GetString("Email"),
                            .RoleId = reader.GetString("RoleId"),
                            .CreatedAt = reader.GetDateTime("CreatedAt")
                        }
                    End If
                End Using
            End Using
        End Using
        Return Nothing
    End Function

    ''' <summary>Kiểm tra username đã tồn tại chưa</summary>
    Public Function IsUsernameExist(username As String) As Boolean Implements IUserRepository.IsUsernameExist
        Dim sql As String = "SELECT COUNT(*) FROM Users WHERE LOWER(UserName) = LOWER(@UserName)"

        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@UserName", username)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return count > 0
            End Using
        End Using
    End Function

    ''' <summary>Kiểm tra email đã tồn tại chưa</summary>
    Public Function IsEmailExist(email As String) As Boolean Implements IUserRepository.IsEmailExist
        Dim sql As String = "SELECT COUNT(*) FROM Users WHERE LOWER(Email) = LOWER(@Email)"

        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@Email", email)
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return count > 0
            End Using
        End Using
    End Function

    ''' <summary>Lấy toàn bộ danh sách User (cho ComboBox giao việc)</summary>
    Public Function GetAll() As List(Of User) Implements IUserRepository.GetAll
        Dim users As New List(Of User)()
        Dim sql As String = "SELECT UserId, UserName, Email, RoleId FROM Users ORDER BY UserName"
        Using conn As New MySqlConnection(ConnectionString)
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        users.Add(New User() With {
                            .UserId = reader.GetInt32("UserId"),
                            .UserName = reader.GetString("UserName"),
                            .Email = reader.GetString("Email"),
                            .RoleId = reader.GetString("RoleId")
                        })
                    End While
                End Using
            End Using
        End Using
        Return users
    End Function

End Class
