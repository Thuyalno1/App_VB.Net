# 🎯 HƯỚNG DẪN SỬ DỤNG HỆ THỐNG APPSTORY (QUẢN LÝ CÔNG VIỆC TỔNG THỂ)

## ✅ ĐÃ HOÀN THÀNH
Hệ thống quản lý công việc và đội nhóm với kiến trúc 3-Tier đã sẵn sàng:
- **✅ Database:** SQL Server với các bảng `User`, `Project`, `Team`, `Task`, `UserTeam`.
- **✅ Forms:** Quản lý Dự án (`frmProjects`), Nhóm (`frmTeams`), và Công việc (`frmTaskManagement`).
- **✅ Tích hợp:** Đăng nhập an toàn, chuyển hướng linh hoạt giữa My Tasks và Admin Dashboard.
- **✅ Phân quyền:** Phân tách dữ liệu cá nhân theo người dùng.

---

## 📋 BƯỚC 1: KHỞI TẠO CƠ SỞ DỮ LIỆU
**Chạy SQL Script trong SQL Server**

1. Tạo Database mới (Ví dụ: `AppStoryDB`).
2. Thực thi Script (hoặc Migrate từ Entity Framework) để sinh ra các bảng:
   - `User`: Quản lý tài khoản và phân quyền.
   - `Project`: Các dự án lớn.
   - `Team`: Nhóm làm việc.
   - `Task`: Các công việc chi tiết.
   - `UserTeam`: Bảng trung gian chia User vào các Team.

**Verify CSDL:**
Đảm bảo đã có ít nhất một tài khoản Admin mẫu để đăng nhập lần đầu.

---

## 🔨 BƯỚC 2: BUILD PROJECT (VISUAL STUDIO)
1. **Mở Solution:** Mở file `AppStory.sln`.
2. **Cấu hình Database:** Cập nhật `ConnectionString` trong `App.config` (thư mục GUI) để trỏ đúng tới Database của bạn.
3. **Clean Solution:** Menu Build → Clean Solution (hoặc chuột phải project).
4. **Rebuild Solution:** Nhấn `Ctrl + Shift + B` hoặc Build → Rebuild Solution.
5. **Kiểm tra lỗi:** Đảm bảo `Error List` trống (0 errors).

---

## 🚀 BƯỚC 3: CHẠY VÀ TEST (FLOW HOÀN CHỈNH)
1. Chạy app nhấn `F5`.
   ↓
2. Màn hình `frmLogin` hiển thị. Đăng nhập với tài khoản hợp lệ.
   ↓
3. Form `frmMain` (Dashboard) mở ra, tự động tải thông tin phiên người dùng (`Session/UserService`).
   ↓
4. Bắt đầu trải nghiệm các luồng quản lý!

---

## ✨ BƯỚC 4: TEST CÁC TÍNH NĂNG CỐT LÕI

### 1. QUẢN TRỊ DỰ ÁN (CREATE PROJECT / TEAM)
- ✅ Mở **frmProjects**: Tạo một dự án mới (Ví dụ: "Dự án AppStory V1.0").
- ✅ Mở **frmTeams**: Tạo một nhóm mới và gán vào Dự án vừa tạo. Thêm các User (nhân viên) vào Nhóm.
- ✅ *Verify:* Thông báo thành công và dữ liệu hiện trên DataGridView.

### 2. QUẢN LÝ CÔNG VIỆC (GIAO VIỆC)
- ✅ Mở **frmTaskManagement**:
- ✅ Tạo công việc mới: Tên công việc (VD: "Thiết kế CSDL"), chọn Dự Án, Assign cho một User cụ thể.
- ✅ *Verify:* Task được tạo thành công, tự động load lại danh sách.

### 3. QUẢN LÝ CÁ NHÂN (MY TASKS)
- ✅ Đăng nhập bằng tài khoản được giao việc ở bước trên.
- ✅ Mở màn hình **My Tasks**:
- ✅ *Verify:* User chỉ nhìn thấy các công việc được giao cho chính mình.
- ✅ Bấm chọn Task và thay đổi trạng thái (Ví dụ: Từ `Open` sang `InProgress` hoặc `Done`).

### 4. XÓA CÔNG VIỆC (DELETE)
- ✅ Click chọn một thông báo/công việc muốn xóa > Nhấn `Xóa`.
- ✅ *Verify:* Form xuất hiện hội thoại xác nhận. Nếu `Yes`, DB sẽ xóa an toàn, bảng Grid load lại rỗng.

---

## 📊 CẤU TRÚC KIẾN TRÚC MÃ NGUỒN (3-TIER)
```text
d:\VSII\Learn_VSII\winfromapp\AppStory\
├── AppStory\
│   ├── GUI\
│   │   ├── frmMain.vb, frmLogin.vb, frmTaskManagement.vb ... (UI Forms)
│   ├── BLL\
│   │   ├── DTOs\           # Data Transfer Objects
│   │   ├── Services\       # Business Logic (TaskService, UserService)
│   ├── DAL\
│   │   ├── Model\          # Entities (User.vb, Task.vb, Team.vb)
│   │   ├── Repositories\   # Gọi SQL Queries (TaskRepository.vb)
```

---

## 🎨 GIAO DIỆN QUẢN LÝ CÔNG VIỆC (MINH HỌA)
```text
┌────────────────────────────────────────────────────────┐
│  QUẢN LÝ CÔNG VIỆC                  Xin chào: admin     │
├────────────────────────────────────────────────────────┤
│                                                        │
│  Tên công việc:  [Thiết kế Database ERD_____]          │
│  Dự án:          [▼ AppStory V1.0 ]                    │
│  Assignee:       [▼ Nguyen Van A  ]                    │
│                                                        │
│  [➕ Tạo mới]    [✏️ Cập nhật]    [🗑️ Xóa]             │
│                                                        │
├────────────────────────────────────────────────────────┤
│  DANH SÁCH CÔNG VIỆC                                   │
│  ┌──────────────────────────────────────────────────┐  │
│  │ ID │ Tên Task      │ Trạng Thái │ Người Đảm Nhận │  │
│  ├────┼───────────────┼────────────┼────────────────┤  │
│  │ 1  │ Thiết kế ERD  │ Open       │ Nguyen Van A   │  │
│  │ 2  │ Viết API      │ InProgress │ Tran Van B     │  │
│  └──────────────────────────────────────────────────┘  │
│                                                        │
│                                        [Đóng Form]     │
└────────────────────────────────────────────────────────┘
```

---

## 📝 CHECKLIST CUỐI CÙNG
- [ ] Khởi tạo thành công CSDL trong SQL Server.
- [ ] Cấu hình ConnectionString chính xác.
- [ ] Build project không lỗi xanh chín.
- [ ] Test Login / Logout trơn tru.
- [ ] Thêm / Sửa / Xóa Dự Án & Team thành công.
- [ ] Thêm / Sửa / Xóa Task thành công.
- [ ] Phân luồng User xem đúng Task của mình (Data Isolation).

