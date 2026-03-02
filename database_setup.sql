-- ============================================================
--  AppStory Database Setup Script
--  Database  : appstore
--  Engine    : MySQL 5.7+ / MariaDB
--  Charset   : utf8mb4
--
--  Cách sử dụng:
--    1. Mở MySQL Workbench hoặc HeidiSQL
--    2. Chạy toàn bộ file này (F5 / Execute)
--    3. Mở App.config, sửa uid / password cho đúng máy bạn
--
--  Tài khoản mặc định sau khi seed:
--    Username : admin   | Password : 123  (Role: Admin)
--    Username : user1   | Password : 123  (Role: Employee)
--    Username : user2   | Password : 123  (Role: Employee)
-- ============================================================

-- --------------------------------------------------------
-- 0. Tạo & chọn database
-- --------------------------------------------------------
CREATE DATABASE IF NOT EXISTS appstore
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE appstore;

-- --------------------------------------------------------
-- 1. Bảng Roles – phân quyền (Admin / Employee)
-- --------------------------------------------------------
CREATE TABLE IF NOT EXISTS Roles (
    RoleId   INT          PRIMARY KEY AUTO_INCREMENT,
    RoleName VARCHAR(50)  NOT NULL UNIQUE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------
-- 2. Bảng Users – tài khoản người dùng
-- --------------------------------------------------------
CREATE TABLE IF NOT EXISTS Users (
    UserId       INT          PRIMARY KEY AUTO_INCREMENT,
    UserName     VARCHAR(50)  NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Email        VARCHAR(100) NOT NULL,
    RoleId       INT          NOT NULL,
    CreatedAt    DATETIME     DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------
-- 3. Bảng Team – nhóm làm việc
--    (Tên bảng là "Team" theo đúng code trong Repositories)
-- --------------------------------------------------------
CREATE TABLE IF NOT EXISTS Team (
    TeamId      INT          PRIMARY KEY AUTO_INCREMENT,
    TeamName    VARCHAR(100) NOT NULL,
    Description TEXT,
    CreatedAt   DATETIME     DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------
-- 4. Bảng UserTeam – quan hệ nhiều-nhiều User <-> Team
--    Role: "Leader" | "Member"
-- --------------------------------------------------------
CREATE TABLE IF NOT EXISTS UserTeam (
    UserId    INT         NOT NULL,
    TeamId    INT         NOT NULL,
    Role      VARCHAR(20) NOT NULL DEFAULT 'Member',   -- Leader / Member
    JoinedAt  DATETIME    DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (UserId, TeamId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE,
    FOREIGN KEY (TeamId) REFERENCES Team(TeamId)  ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------
-- 5. Bảng Project – dự án
--    Status: 'Planning' | 'Active' | 'On Hold' | 'Completed'
-- --------------------------------------------------------
CREATE TABLE IF NOT EXISTS Project (
    ProjectId   INT          PRIMARY KEY AUTO_INCREMENT,
    ProjectName VARCHAR(200) NOT NULL,
    Description TEXT,
    StartDate   DATE,
    EndDate     DATE,
    Status      VARCHAR(20)  NOT NULL DEFAULT 'Planning',
    ManagerId   INT,                           -- Project Manager (UserId)
    TeamId      INT,                           -- Team thực hiện (Nullable)
    CreatedAt   DATETIME     DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ManagerId) REFERENCES Users(UserId)  ON DELETE SET NULL,
    FOREIGN KEY (TeamId)    REFERENCES Team(TeamId)   ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------
-- 6. Bảng Tasks – công việc / task
--    Status   : 'Pending' | 'In Progress' | 'Completed'
--    Priority : 'High'    | 'Medium'      | 'Low'
--    IsDeleted: Soft Delete (1 = đã xóa)
-- --------------------------------------------------------
CREATE TABLE IF NOT EXISTS Tasks (
    TaskId           INT          PRIMARY KEY AUTO_INCREMENT,
    Title            VARCHAR(200) NOT NULL,
    Description      TEXT,
    AssignedToUserId INT,                      -- NULL = chưa giao
    CreatedByUserId  INT          NOT NULL,
    Status           VARCHAR(20)  NOT NULL DEFAULT 'Pending',
    Priority         VARCHAR(10)  NOT NULL DEFAULT 'Medium',
    CreatedAt        DATETIME     DEFAULT CURRENT_TIMESTAMP,
    DueDate          DATETIME,
    IsDeleted        TINYINT(1)   NOT NULL DEFAULT 0,
    ProjectId        INT,                      -- Nullable
    TeamId           INT,                      -- Nullable
    FOREIGN KEY (AssignedToUserId) REFERENCES Users(UserId)   ON DELETE SET NULL,
    FOREIGN KEY (CreatedByUserId)  REFERENCES Users(UserId),
    FOREIGN KEY (ProjectId)        REFERENCES Project(ProjectId) ON DELETE SET NULL,
    FOREIGN KEY (TeamId)           REFERENCES Team(TeamId)    ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================================
-- SEED DATA – Dữ liệu mẫu
-- ============================================================

-- --------------------------------------------------------
-- Roles
-- --------------------------------------------------------
INSERT INTO Roles (RoleName) VALUES
    ('Admin'),
    ('Employee');

-- --------------------------------------------------------
-- Users  (PasswordHash lưu plaintext "123" để demo)
-- Thực tế nên hash bằng BCrypt trước khi lưu
-- --------------------------------------------------------
INSERT INTO Users (UserName, PasswordHash, Email, RoleId) VALUES
    ('admin',  '123', 'admin@appstory.com',  1),
    ('user1',  '123', 'user1@appstory.com',  2),
    ('user2',  '123', 'user2@appstory.com',  2),
    ('user3',  '123', 'user3@appstory.com',  2);

-- --------------------------------------------------------
-- Teams
-- --------------------------------------------------------
INSERT INTO Team (TeamName, Description) VALUES
    ('Team Alpha',   'Nhóm phát triển tính năng chính'),
    ('Team Beta',    'Nhóm kiểm thử và QA'),
    ('Team Gamma',   'Nhóm thiết kế và UI/UX');

-- --------------------------------------------------------
-- UserTeam – phân công thành viên
-- --------------------------------------------------------
INSERT INTO UserTeam (UserId, TeamId, Role) VALUES
    (1, 1, 'Leader'),   -- admin  → Team Alpha (Leader)
    (2, 1, 'Member'),   -- user1  → Team Alpha (Member)
    (3, 1, 'Member'),   -- user2  → Team Alpha (Member)
    (2, 2, 'Leader'),   -- user1  → Team Beta  (Leader)
    (4, 2, 'Member'),   -- user3  → Team Beta  (Member)
    (3, 3, 'Leader'),   -- user2  → Team Gamma (Leader)
    (4, 3, 'Member');   -- user3  → Team Gamma (Member)

-- --------------------------------------------------------
-- Projects
-- --------------------------------------------------------
INSERT INTO Project (ProjectName, Description, StartDate, EndDate, Status, ManagerId, TeamId) VALUES
    ('AppStory v1.0',    'Xây dựng ứng dụng quản lý công việc',       '2025-01-01', '2025-06-30', 'Active',    1, 1),
    ('QA Dashboard',     'Dashboard theo dõi tiến độ kiểm thử',        '2025-02-01', '2025-05-31', 'Planning',  2, 2),
    ('UI Redesign',      'Thiết kế lại giao diện người dùng',          '2025-03-01', '2025-07-31', 'Planning',  3, 3);

-- --------------------------------------------------------
-- Tasks
-- --------------------------------------------------------
INSERT INTO Tasks (Title, Description, AssignedToUserId, CreatedByUserId, Status, Priority, DueDate, ProjectId, TeamId) VALUES
    -- Project 1 – Team Alpha
    ('Thiết kế database',      'Tạo schema MySQL cho toàn bộ hệ thống',     2, 1, 'Completed',   'High',   '2025-01-15', 1, 1),
    ('Xây dựng DAL Layer',     'Viết Repository cho User, Task, Team',       2, 1, 'In Progress', 'High',   '2025-02-01', 1, 1),
    ('Xây dựng BLL Layer',     'Viết Service và DTO',                        3, 1, 'Pending',     'Medium', '2025-02-15', 1, 1),
    ('Thiết kế form Login',    'WinForm đăng nhập và phân quyền',            NULL,1, 'Pending',   'High',   '2025-02-10', 1, 1),
    -- Project 2 – Team Beta
    ('Viết test case',         'Viết tất cả các kịch bản kiểm thử',         4, 2, 'Pending',     'Medium', '2025-03-01', 2, 2),
    ('Kiểm thử tích hợp',      'Test toàn bộ luồng đăng nhập – phân quyền', NULL,2, 'Pending',   'High',   '2025-04-01', 2, 2),
    -- Project 3 – Team Gamma
    ('Thiết kế màn hình chính','Mockup giao diện Dashboard',                 3, 3, 'In Progress', 'Medium', '2025-03-20', 3, 3),
    ('Chọn bảng màu & font',   'Xác định design system',                     NULL,3, 'Pending',   'Low',    '2025-03-10', 3, 3),
    -- Task không thuộc project nào
    ('Review code tuần 1',     'Họp review code nội bộ nhóm',               NULL,1, 'Pending',   'Low',    '2025-01-20', NULL, 1),
    ('Họp kickoff dự án',      'Kickoff meeting toàn team',                  NULL,1, 'Completed', 'Medium', '2025-01-05', NULL, NULL);

-- ============================================================
-- Kiểm tra nhanh sau khi chạy
-- ============================================================
-- SELECT * FROM Roles;
-- SELECT * FROM Users;
-- SELECT * FROM Team;
-- SELECT t.TeamName, u.UserName, ut.Role FROM UserTeam ut
--    JOIN Team t ON t.TeamId=ut.TeamId
--    JOIN Users u ON u.UserId=ut.UserId;
-- SELECT * FROM Project;
-- SELECT * FROM Tasks;
