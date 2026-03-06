-- Tạo Database

CREATE DATABASE IF NOT EXISTS appstore
DEFAULT CHARACTER SET utf8mb4
COLLATE utf8mb4_0900_ai_ci;
USE appstore;

-- ========================
-- Tạo bảng USERS
-- ========================

DROP TABLE IF EXISTS users;

CREATE TABLE users (
  UserId INT NOT NULL AUTO_INCREMENT,
  UserName VARCHAR(100) NOT NULL,
  PasswordHash VARCHAR(256) NOT NULL,
  Email VARCHAR(255) NOT NULL,
  RoleId VARCHAR(50) NOT NULL DEFAULT 'Employee',
  CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (UserId),
  UNIQUE (UserName),
  UNIQUE (Email)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO users VALUES
(1,'thuymai','d3fba36993dfb07a62a209a0e5fdaccdb0041eec9e8e94c9f3a92934dcbdd098','maivanthuy@gmail.com','Admin','2026-02-26 13:58:29'),
(2,'haidang','3ff1c16ea682274f533cb9c211f619800674a8aeac5035cd502860c06e633ce6','hai@gmail.com','Manager','2026-02-26 13:59:22'),
(3,'nhanvien','b01ba614e7ae08379c7fd1e568349c88e7657d1df1ad85f7180a995278006fd5','nhan@gmail.com','Employee','2026-02-26 14:25:39'),
(4,'khanhthu','9338b3c03aacef47daa56feb8838f607524976cafa986df58ee1b9c9b3cda2a6','khanh@gmail.com','Employee','2026-02-27 14:05:52'),
(5,'khanhthu1','7f74ebba484437837a1862b9c3635aaf93622ec3d113edf8eddb8fd185f4b054','khanhthu@gmail.com','Employee','2026-02-27 14:06:49'),
(6,'nhatdang','9f24bfb5f99d6caa7c42099701ecac3a210afd91a3305254c86c2692410e54ff','dang@gmail.com','Employee','2026-02-27 16:24:11');

-- ========================
-- -- Tạo bảng TEAM
-- ========================

DROP TABLE IF EXISTS team;

CREATE TABLE team (
  TeamId INT NOT NULL AUTO_INCREMENT,
  TeamName VARCHAR(100) NOT NULL,
  Description VARCHAR(500),
  CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (TeamId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO team VALUES
(1,'Intern','.','2026-02-27 14:08:43'),
(2,'Dev1','','2026-02-27 14:28:24'),
(3,'te','.','2026-02-27 14:30:09'),
(4,'Team FE','.','2026-02-27 14:34:39'),
(5,'Team BA','.','2026-02-27 14:48:51');

-- ========================
--  Tạo bảng PROJECT
-- ========================

DROP TABLE IF EXISTS project;

CREATE TABLE project (
  ProjectId INT NOT NULL AUTO_INCREMENT,
  ProjectName VARCHAR(200) NOT NULL,
  Description TEXT,
  StartDate DATE,
  EndDate DATE,
  Status VARCHAR(50) DEFAULT 'Planning',
  ManagerId INT,
  TeamId INT,
  CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (ProjectId),
  FOREIGN KEY (ManagerId) REFERENCES users(UserId),
  FOREIGN KEY (TeamId) REFERENCES team(TeamId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO project VALUES
(1,'VII_1','Dự án quốc gia','2026-02-27','2026-03-05','Planning',2,NULL,'2026-02-27 14:08:15'),
(2,'VII_1','Dự án quốc gia','2026-02-27','2026-03-05','Planning',2,4,'2026-02-27 14:35:15'),
(3,'VII_2','.','2026-02-27','2026-03-27','Planning',5,4,'2026-02-27 14:36:56'),
(4,'VII_3','.','2026-02-27','2026-03-27','Planning',5,4,'2026-02-27 14:48:30'),
(5,'VII_1','Dự án quốc gia','2026-02-27','2026-03-05','Planning',2,NULL,'2026-03-02 09:23:25'),
(6,'VII_4','Dự án quốc gia','2026-02-27','2026-03-06','Active',5,4,'2026-03-02 09:49:03');

-- ========================
--  Tạo bảng TASKS
-- ========================

DROP TABLE IF EXISTS tasks;

CREATE TABLE tasks (
  TaskId INT NOT NULL AUTO_INCREMENT,
  Title VARCHAR(200) NOT NULL,
  Description TEXT,
  AssignedToUserId INT,
  CreatedByUserId INT NOT NULL,
  Status VARCHAR(50) NOT NULL DEFAULT 'Pending',
  Priority VARCHAR(50) NOT NULL DEFAULT 'Medium',
  CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  DueDate DATETIME,
  IsDeleted TINYINT(1) NOT NULL DEFAULT 0,
  ProjectId INT,
  TeamId INT,
  PRIMARY KEY (TaskId),
  FOREIGN KEY (AssignedToUserId) REFERENCES users(UserId),
  FOREIGN KEY (CreatedByUserId) REFERENCES users(UserId),
  FOREIGN KEY (ProjectId) REFERENCES project(ProjectId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO tasks VALUES
(1,'Bai tap','.',3,1,'Completed','Medium','2026-02-26 14:30:31','2026-03-05 14:30:07',0,NULL,NULL),
(2,'De ve nha','',2,1,'Pending','Low','2026-02-26 14:38:23','2026-03-05 14:37:32',0,NULL,NULL),
(3,'dev','..',5,1,'In Progress','High','2026-03-02 08:49:45','2026-03-06 08:49:00',0,3,4),
(4,'small','..',5,1,'Pending','Medium','2026-03-02 09:24:52','2026-03-09 09:23:34',0,4,4),
(5,'test1','1',3,3,'In Progress','Low','2026-03-02 10:15:05','2026-03-04 10:14:37',0,NULL,5);

-- ========================
--  Tạo bảng USERTEAM
-- ========================

DROP TABLE IF EXISTS userteam;

CREATE TABLE userteam (
  UserId INT NOT NULL,
  TeamId INT NOT NULL,
  JoinedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
  Role VARCHAR(50) NOT NULL DEFAULT 'Member',
  PRIMARY KEY (UserId, TeamId),
  FOREIGN KEY (UserId) REFERENCES users(UserId),
  FOREIGN KEY (TeamId) REFERENCES team(TeamId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO userteam VALUES
(2,4,'2026-02-27 14:36:16','Member'),
(2,5,'2026-02-27 14:48:51','Member'),
(3,1,'2026-02-27 14:33:08','Leader'),
(3,4,'2026-02-27 14:36:16','Member'),
(3,5,'2026-02-27 14:48:51','Leader'),
(4,4,'2026-02-27 14:36:16','Member'),
(4,5,'2026-02-27 14:48:51','Member'),
(5,4,'2026-02-27 14:36:16','Leader'),
(5,5,'2026-02-27 14:48:51','Member');