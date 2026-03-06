# 📋 PROJECT DOCUMENTATION — AppStory

> **Ngôn ngữ:** Visual Basic .NET (WinForms) | **CSDL:** MySQL (`appstore`) | **Kiến trúc:** 3 tầng DAL / BLL / GUI

---

## 1. Kiến Trúc Hệ Thống

```
                        ┌──────────────────────┐
                        │      AppStory App     │
                        │   (WinForms / VB.NET) │
                        └──────────┬───────────┘
                                   │
               ┌───────────────────┼───────────────────┐
               │                   │                   │
               ▼                   ▼                   ▼
        ┌────────────┐     ┌──────────────┐    ┌──────────────┐
        │    GUI     │     │     BLL      │    │     DAL      │
        │ (Forms)    │────▶│  (Services)  │───▶│(Repositories)│
        └────────────┘     └──────────────┘    └──────┬───────┘
                                                       │
                                               ┌───────▼───────┐
                                               │  MySQL DB     │
                                               │  (appstore)   │
                                               └───────────────┘
```

---

## 2. Phân Quyền Theo Vai Trò (Use Case)

```
                    ┌──────────────────┐
                    │     AppStory     │
                    │   (Hệ thống)     │
    ┌───────────────┴──────────────────┴───────────────┐
    │                                                  │
    ▼                                                  ▼
┌─────────┐                                     ┌──────────┐
│  Admin  │                                     │ Manager  │
└────┬────┘                                     └────┬─────┘
     │                                               │
     ├─ Đăng nhập / Đăng xuất                        ├─ Đăng nhập / Đăng xuất
     │                                               │
     ├─ Quản lý Công việc (frmTaskManagement)        ├─ Quản lý Công việc (frmTaskManagement)
     │   ├─ Thêm task mới                            │   ├─ Thêm task mới
     │   ├─ Sửa task                                 │   ├─ Sửa task
     │   ├─ Xóa task (Soft Delete)                   │   ├─ Xóa task (Soft Delete)
     │   ├─ Lọc task theo trạng thái                 │   ├─ Lọc task theo trạng thái
     │   └─ Xuất thống kê file CSV                   │   └─ Xuất thống kê file CSV
     │                                               │
     ├─ Quản lý Dự án (frmProjects)                  ├─ Quản lý Dự án (frmProjects)
     │   ├─ Thêm dự án                               │   ├─ Thêm dự án
     │   ├─ Sửa dự án                                │   ├─ Sửa dự án
     │   └─ Xóa dự án                                │   └─ Xóa dự án
     │                                               │
     ├─ Quản lý Nhóm (frmTeams)                      ├─ Nhận việc tự do (frmOpenTasks)
     │   ├─ Thêm nhóm                                │   └─ Nhấn "Nhận việc" → tự gán task
     │   ├─ Sửa nhóm + phân Leader/Member            │
     │   └─ Xóa nhóm                                 └─ Công việc của tôi (frmMyTasks)
     │                                                   └─ Cập nhật trạng thái task
     └─ Đăng ký nhân viên (frmRegister)


                                               ┌──────────┐
                                               │ Employee │
                                               └────┬─────┘
                                                    │
                                                    ├─ Đăng nhập / Đăng xuất
                                                    │
                                                    ├─ Nhận việc tự do (frmOpenTasks)
                                                    │   └─ Xem task chưa có người nhận
                                                    │       └─ Nhấn "Nhận việc"
                                                    │
                                                    ├─ Công việc của tôi (frmMyTasks)
                                                    │   ├─ Xem danh sách task được giao
                                                    │   └─ Cập nhật trạng thái
                                                    │       ├─ Pending
                                                    │       ├─ In Progress
                                                    │       └─ Completed
                                                    │
                                                    └─ Nhóm của tôi (frmMyTeams)
                                                        └─ Xem nhóm mình thuộc về
```

---

## 3. Luồng Đăng Nhập

```
┌─────────────────────────────────────────────────────────┐
│                     frmLogin                            │
│          Nhập Username + Password                        │
└───────────────────────┬─────────────────────────────────┘
                        │
                        ▼
              UserService.Login(LoginDto)
              (Kiểm tra DB + xác thực mật khẩu hash)
                        │
           ┌────────────┴────────────┐
           │                         │
           ▼                         ▼
    ❌ Sai thông tin           ✅ Đúng thông tin
    Hiện thông báo lỗi        SessionManager.CurrentUser = user
    Ở lại frmLogin                    │
                                      ▼
                              ┌──────────────┐
                              │   frmMain    │
                              │  Dashboard   │
                              └──────┬───────┘
                                     │
                    ┌────────────────┼────────────────┐
                    ▼                ▼                 ▼
             RoleId=Admin     RoleId=Manager    RoleId=Employee
             (🔴 Đỏ)          (🟡 Vàng)          (🟢 Xanh)
```

---

## 4. Luồng Quản Lý Công Việc

```
┌──────────────────────────────────────────────────────────────┐
│                  frmTaskManagement                           │
│  Load: Users | Projects | Teams | Tasks (hiển thị tên thay ID)│
└──────────────────────────┬───────────────────────────────────┘
                           │
          ┌────────────────┼────────────────┬───────────────┐
          │                │                │               │
          ▼                ▼                ▼               ▼
    ➕ Thêm Task      ✏️ Sửa Task     🗑️ Xóa Task    📤 Xuất CSV
          │                │                │               │
    Điền form         Chọn task        Chọn task       ExportStatisticsToCSV()
    (Title, Priority, trên grid →     trên grid →             │
     Status, User,    điền form →     Xác nhận →      ┌──────┴──────┐
     Project, Team,   sửa thông tin   SoftDelete       │  File CSV  │
     DueDate)               │         IsDeleted=1      ├─ Thống kê  │
          │                 │               │          │  trạng thái│
          ▼                 ▼               ▼          ├─ Thống kê  │
     TaskDto →         TaskDto →      DeleteTask()     │  ưu tiên   │
     CreateTask()      UpdateTask()        │           └─ Danh sách │
          │                 │              │             chi tiết   │
          └─────────────────┴──────────────┘
                            │
                     🔄 Reload DataGridView
```

---

## 5. Luồng Nhận Việc Tự Do

```
┌──────────────────────────────────────────────────────────┐
│                      frmOpenTasks                        │
│  Load: Task có Status='Pending' & AssignedToUserId=NULL  │
└──────────────────────────┬───────────────────────────────┘
                           │
                           ▼
              Nhấn nút "Nhận việc" trên lưới
                           │
                           ▼
               Hộp thoại xác nhận (Yes/No)
                           │
              ┌────────────┴────────────┐
              │                         │
              ▼                         ▼
           Hủy bỏ               TaskService.ClaimTask()
         Ở lại lưới          (Gán AssignedToUserId = UserId mình)
                                        │
                                        ▼
                                🔄 Reload danh sách
                               (Task vừa nhận biến mất)
```

---

## 6. Luồng Công Việc Cá Nhân

```
┌───────────────────────────────────────────────────────┐
│                     frmMyTasks                        │
│  Load: Task có AssignedToUserId = UserId hiện tại     │
└──────────────────────────┬────────────────────────────┘
                           │
                           ▼
              Chọn một task trên lưới
                           │
                           ▼
             Chọn trạng thái mới từ ComboBox
             ┌─────────────────────────────┐
             │  Pending                    │
             │  In Progress                │
             │  Completed                  │
             └──────────────┬──────────────┘
                            │
                            ▼
              TaskService.UpdateStatus(TaskId, newStatus)
                            │
                            ▼
                    🔄 Reload danh sách
```

---

## 7. Luồng Quản Lý Nhóm

```
┌──────────────────────────────────────────────────────────────┐
│                         frmTeams                             │
│       Load: Danh sách nhóm (tên Leader + Member)             │
└──────────────────────────┬───────────────────────────────────┘
                           │
           ┌───────────────┼───────────────┐
           │               │               │
           ▼               ▼               ▼
     ➕ Thêm Nhóm    ✏️ Sửa Nhóm    🗑️ Xóa Nhóm
           │               │               │
     Nhập tên,         Chọn nhóm      Xác nhận →
     mô tả             trên grid →    DeleteTeam()
           │           sửa thông tin
           ▼               │
     Chọn Leader(s)  ───────┘
     (CheckedListBox)
           │
           ▼
     Chọn Member(s)
     (CheckedListBox)
           │
           ▼
     TeamService.CreateTeam() / UpdateTeam()
     → Ghi bảng userteam (Role: Leader / Member)
           │
           ▼
     🔄 Reload DataGridView
```

---

## 8. Luồng Xử Lý Lỗi

```
┌──────────────────────┐
│  DAL — Repository    │
│  (Truy vấn MySQL)    │
└──────────┬───────────┘
           │ throws
           ▼
   DataAccessException
   (Lỗi kết nối / SQL)
           │ bắt & ném lại
           ▼
┌──────────────────────┐
│  BLL — Service       │
│  (Logic nghiệp vụ)   │
└──────────┬───────────┘
           │ throws
           ├─ BusinessException  (thông báo thân thiện)
           └─ NotFoundException  (không tìm thấy record)
                      │ bắt
                      ▼
           ┌──────────────────────┐
           │  GUI — Form          │
           │  (catch ex As ...)   │
           └──────────┬───────────┘
                      │
                      ▼
           MessageBox.Show(ex.Message)
           (Thông báo lỗi cho người dùng)
```

---

## 9. Cấu Trúc Database

```
users ──────────┬──< tasks (AssignedToUserId)
                └──< tasks (CreatedByUserId)
                └──< project (ManagerId)
                └──< userteam >──── team

team  ──────────┬──< project (TeamId)
                └──< tasks (TeamId)

project ─────────── < tasks (ProjectId)
```

| Bảng       | Cột chính                                                       |
|------------|-----------------------------------------------------------------|
| `users`    | UserId, UserName, PasswordHash, Email, **RoleId**               |
| `tasks`    | TaskId, Title, Status, Priority, AssignedToUserId, **IsDeleted**|
| `project`  | ProjectId, ProjectName, Status, ManagerId, TeamId               |
| `team`     | TeamId, TeamName, Description                                   |
| `userteam` | UserId, TeamId, **Role** (Leader / Member)                      |

---

## 10. Danh Mục Form

| Form                 | Ai dùng            | Chức năng chính                         |
|----------------------|--------------------|-----------------------------------------|
| `frmLogin`           | Tất cả             | Đăng nhập hệ thống                      |
| `frmRegister`        | Tất cả             | Đăng ký tài khoản                       |
| `frmMain`            | Tất cả             | Dashboard, điều hướng theo Role         |
| `frmTaskManagement`  | Admin, Manager     | CRUD task, lọc, xuất CSV thống kê      |
| `frmOpenTasks`       | Employee, Manager  | Xem & nhận task chưa có người làm      |
| `frmMyTasks`         | Employee, Manager  | Xem & cập nhật trạng thái task cá nhân |
| `frmMyTeams`         | Employee, Manager  | Xem nhóm mình thuộc về                 |
| `frmProjects`        | Admin, Manager     | CRUD dự án                              |
| `frmTeams`           | Admin              | CRUD nhóm, phân vai trò Leader/Member  |
| `frmCreateTeamTask`  | Admin, Manager     | Tạo task dành cho nhóm                 |

---

*Tài liệu được tạo ngày 06/03/2026*
