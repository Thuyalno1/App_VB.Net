CREATE DATABASE  IF NOT EXISTS `appstore` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `appstore`;
-- MySQL dump 10.13  Distrib 8.0.44, for Win64 (x86_64)
--
-- Host: localhost    Database: appstore
-- ------------------------------------------------------
-- Server version	9.6.0
SET @MYSQLDUMP_TEMP_LOG_BIN = @@SESSION.SQL_LOG_BIN;
SET @@SESSION.SQL_LOG_BIN= 0;

--
-- GTID state at the beginning of the backup 
--

SET @@GLOBAL.GTID_PURGED=/*!80000 '+'*/ 'ab0c278d-00b3-11f1-9604-088fc351985d:1-333';

--
-- Table structure for table `project`
--

DROP TABLE IF EXISTS `project`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `project` (
  `ProjectId` int NOT NULL AUTO_INCREMENT,
  `ProjectName` varchar(200) NOT NULL,
  `Description` text,
  `StartDate` date DEFAULT NULL,
  `EndDate` date DEFAULT NULL,
  `Status` varchar(50) DEFAULT 'Planning',
  `ManagerId` int DEFAULT NULL,
  `TeamId` int DEFAULT NULL,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ProjectId`),
  KEY `FK_Project_Manager` (`ManagerId`),
  KEY `FK_Project_Team` (`TeamId`),
  CONSTRAINT `FK_Project_Manager` FOREIGN KEY (`ManagerId`) REFERENCES `users` (`UserId`),
  CONSTRAINT `FK_Project_Team` FOREIGN KEY (`TeamId`) REFERENCES `team` (`TeamId`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `project`
--

LOCK TABLES `project` WRITE;
/*!40000 ALTER TABLE `project` DISABLE KEYS */;
INSERT INTO `project` VALUES (1,'VII_1','Dự án quốc gia','2026-02-27','2026-03-05','Planning',2,NULL,'2026-02-27 14:08:15'),(2,'VII_1','Dự án quốc gia','2026-02-27','2026-03-05','Planning',2,4,'2026-02-27 14:35:15'),(3,'VII_2','.','2026-02-27','2026-03-27','Planning',5,4,'2026-02-27 14:36:56'),(4,'VII_3','.','2026-02-27','2026-03-27','Planning',5,4,'2026-02-27 14:48:30'),(5,'VII_1','Dự án quốc gia','2026-02-27','2026-03-05','Planning',2,NULL,'2026-03-02 09:23:25'),(6,'VII_4','Dự án quốc gia','2026-02-27','2026-03-06','Active',5,4,'2026-03-02 09:49:03'),(7,'VII_5','Dự án nhà nước','2026-03-02','2026-03-08','On Hold',6,6,'2026-03-02 14:24:21'),(8,'VII_6','Dự án nhà nước','2026-03-02','2026-03-08','On Hold',6,6,'2026-03-02 14:42:38'),(9,'VII_6','Dự án nhà nước','2026-03-02','2026-03-08','On Hold',6,7,'2026-03-02 14:43:18'),(10,'VII_7','Dự án cá nhân','2026-03-02','2026-03-08','On Hold',5,NULL,'2026-03-09 14:05:32'),(11,'VII_8','Dự án cá nhân','2026-03-02','2026-03-08','On Hold',5,NULL,'2026-03-10 14:09:27'),(12,'VI_9','.','2026-03-10','2026-04-10','Planning',5,NULL,'2026-03-10 14:10:10');
/*!40000 ALTER TABLE `project` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `projectteam`
--

DROP TABLE IF EXISTS `projectteam`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `projectteam` (
  `ProjectId` int NOT NULL,
  `TeamId` int NOT NULL,
  PRIMARY KEY (`ProjectId`,`TeamId`),
  KEY `TeamId` (`TeamId`),
  CONSTRAINT `projectteam_ibfk_1` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ProjectId`) ON DELETE CASCADE,
  CONSTRAINT `projectteam_ibfk_2` FOREIGN KEY (`TeamId`) REFERENCES `team` (`TeamId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `projectteam`
--

LOCK TABLES `projectteam` WRITE;
/*!40000 ALTER TABLE `projectteam` DISABLE KEYS */;
INSERT INTO `projectteam` VALUES (2,4),(3,4),(4,4),(6,4),(10,5),(11,5),(12,5),(7,6),(8,6),(10,6),(11,6),(9,7),(10,7),(11,7);
/*!40000 ALTER TABLE `projectteam` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tasks`
--

DROP TABLE IF EXISTS `tasks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tasks` (
  `TaskId` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(200) NOT NULL,
  `Description` text,
  `AssignedToUserId` int DEFAULT NULL,
  `CreatedByUserId` int NOT NULL,
  `Status` varchar(50) NOT NULL DEFAULT 'Pending',
  `Priority` varchar(50) NOT NULL DEFAULT 'Medium',
  `CreatedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DueDate` datetime DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL DEFAULT '0',
  `ProjectId` int DEFAULT NULL,
  `TeamId` int DEFAULT NULL,
  PRIMARY KEY (`TaskId`),
  KEY `AssignedToUserId` (`AssignedToUserId`),
  KEY `CreatedByUserId` (`CreatedByUserId`),
  KEY `FK_Task_Project` (`ProjectId`),
  CONSTRAINT `FK_Task_Project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ProjectId`),
  CONSTRAINT `tasks_ibfk_1` FOREIGN KEY (`AssignedToUserId`) REFERENCES `users` (`UserId`),
  CONSTRAINT `tasks_ibfk_2` FOREIGN KEY (`CreatedByUserId`) REFERENCES `users` (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tasks`
--

LOCK TABLES `tasks` WRITE;
/*!40000 ALTER TABLE `tasks` DISABLE KEYS */;
INSERT INTO `tasks` VALUES (1,'Bai tap','.',3,1,'ÄÃ£ hoÃ n thÃ nh','Medium','2026-02-26 14:30:31','2026-03-05 14:30:07',1,7,NULL),(2,'De ve nha','',2,1,'Chá» xá»­ lÃ½','Low','2026-02-26 14:38:23','2026-03-05 14:37:32',1,NULL,NULL),(3,'dev','..',5,1,'Đã hoàn thành','High','2026-03-02 08:49:45','2026-03-06 08:49:00',0,3,4),(4,'small','..',5,1,'Đang thực hiện','Medium','2026-03-02 09:24:52','2026-03-09 09:23:34',0,4,4),(5,'test1','1',3,3,'Äang thá»±c hiá»‡n','Low','2026-03-02 10:15:05','2026-03-04 10:14:37',0,NULL,5),(6,'Task_1','.',3,6,'ÄÃ£ hoÃ n thÃ nh','Medium','2026-03-02 14:25:06','2026-03-05 14:24:49',0,NULL,6),(7,'Task_2','.',6,6,'ÄÃ£ hoÃ n thÃ nh','Low','2026-03-02 14:25:35','2026-03-05 14:25:19',0,NULL,6),(8,'Task_4','.',3,5,'Chá» xá»­ lÃ½','Medium','2026-03-02 14:44:31','2026-03-03 14:44:16',0,NULL,4),(9,'Task_6','.',5,5,'Chờ duyệt','Medium','2026-03-02 14:44:41','2026-03-09 14:44:33',0,NULL,4),(10,'Front_end','.',5,5,'Chờ duyệt','Medium','2026-03-06 08:42:23','2026-03-13 08:41:55',0,NULL,4),(11,'Cmc','.',3,1,'Chá» xá»­ lÃ½','Medium','2026-03-09 09:53:45','2026-03-16 09:53:19',0,2,6),(12,'11','',3,5,'Chá» xá»­ lÃ½','Medium','2026-03-10 14:11:13','2026-03-17 14:11:08',0,NULL,4),(13,'55','',5,5,'Đã hoàn thành','Medium','2026-03-10 14:11:30','2026-03-17 14:11:26',0,NULL,4),(14,'1','',5,5,'Đang thực hiện','Medium','2026-03-10 14:20:44','2026-03-17 14:20:40',0,NULL,4),(15,'2','',5,5,'Đang thực hiện','Medium','2026-03-10 14:20:51','2026-03-17 14:20:47',0,NULL,4),(16,'task1','',5,5,'Đang thực hiện','Medium','2026-03-10 14:24:10','2026-03-17 14:24:02',0,NULL,4),(17,'task2','',NULL,5,'Chá» xá»­ lÃ½','Medium','2026-03-10 14:24:17','2026-03-17 14:24:13',0,NULL,4),(18,'Ha Noi','',2,1,'Đang thực hiện','Medium','2026-03-10 14:51:29','2026-03-17 14:50:58',0,11,5),(19,'Bac Ninh','.',5,1,'Chờ xử lý','Trung bình','2026-03-10 16:37:23','2026-03-17 16:30:58',0,12,7),(20,'Task_1','.',2,1,'Đã hoàn thành','Trung bình','2026-03-12 09:06:48','2026-03-19 09:06:31',0,NULL,10);
/*!40000 ALTER TABLE `tasks` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `team`
--

DROP TABLE IF EXISTS `team`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `team` (
  `TeamId` int NOT NULL AUTO_INCREMENT,
  `TeamName` varchar(100) NOT NULL,
  `Description` varchar(500) DEFAULT NULL,
  `CreatedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`TeamId`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `team`
--

LOCK TABLES `team` WRITE;
/*!40000 ALTER TABLE `team` DISABLE KEYS */;
INSERT INTO `team` VALUES (1,'Intern','.','2026-02-27 14:08:43'),(2,'Dev1','','2026-02-27 14:28:24'),(3,'te','.','2026-02-27 14:30:09'),(4,'Team FE','.','2026-02-27 14:34:39'),(5,'Team BA','.','2026-02-27 14:48:51'),(6,'Team Tester','.','2026-03-02 14:23:34'),(7,'Team BE','.','2026-03-02 14:43:06'),(8,'Team BÊ','.','2026-03-10 16:30:36'),(9,'PM','.','2026-03-10 16:56:47'),(10,'FE_1','.','2026-03-12 09:06:14');
/*!40000 ALTER TABLE `team` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `UserId` int NOT NULL AUTO_INCREMENT,
  `UserName` varchar(100) NOT NULL,
  `PasswordHash` varchar(256) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `RoleId` varchar(50) NOT NULL DEFAULT 'Employee',
  `CreatedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`UserId`),
  UNIQUE KEY `UserName` (`UserName`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'thuymai','d3fba36993dfb07a62a209a0e5fdaccdb0041eec9e8e94c9f3a92934dcbdd098','maivanthuy@gmail.com','Admin','2026-02-26 13:58:29'),(2,'haidang','3ff1c16ea682274f533cb9c211f619800674a8aeac5035cd502860c06e633ce6','hai@gmail.com','Manager','2026-02-26 13:59:22'),(3,'nhanvien','b01ba614e7ae08379c7fd1e568349c88e7657d1df1ad85f7180a995278006fd5','nhan@gmail.com','Employee','2026-02-26 14:25:39'),(4,'khanhthu','9338b3c03aacef47daa56feb8838f607524976cafa986df58ee1b9c9b3cda2a6','khanh@gmail.com','Employee','2026-02-27 14:05:52'),(5,'khanhthu1','7f74ebba484437837a1862b9c3635aaf93622ec3d113edf8eddb8fd185f4b054','khanhthu@gmail.com','Employee','2026-02-27 14:06:49'),(6,'nhatdang','9f24bfb5f99d6caa7c42099701ecac3a210afd91a3305254c86c2692410e54ff','dang@gmail.com','Employee','2026-02-27 16:24:11');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userteam`
--

DROP TABLE IF EXISTS `userteam`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `userteam` (
  `UserId` int NOT NULL,
  `TeamId` int NOT NULL,
  `JoinedAt` datetime DEFAULT CURRENT_TIMESTAMP,
  `Role` varchar(50) NOT NULL DEFAULT 'Member',
  PRIMARY KEY (`UserId`,`TeamId`),
  KEY `FK_UserTeam_Team` (`TeamId`),
  CONSTRAINT `FK_UserTeam_Team` FOREIGN KEY (`TeamId`) REFERENCES `team` (`TeamId`),
  CONSTRAINT `FK_UserTeam_User` FOREIGN KEY (`UserId`) REFERENCES `users` (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userteam`
--

LOCK TABLES `userteam` WRITE;
/*!40000 ALTER TABLE `userteam` DISABLE KEYS */;
INSERT INTO `userteam` VALUES (2,4,'2026-02-27 14:36:16','Member'),(2,5,'2026-02-27 14:48:51','Member'),(2,6,'2026-03-02 14:23:34','Member'),(2,7,'2026-03-02 14:43:06','Member'),(2,8,'2026-03-10 16:30:36','Member'),(2,9,'2026-03-10 16:56:47','Member'),(2,10,'2026-03-12 09:06:14','Member'),(3,1,'2026-02-27 14:33:08','Leader'),(3,4,'2026-02-27 14:36:16','Member'),(3,5,'2026-02-27 14:48:51','Leader'),(3,6,'2026-03-02 14:23:34','Member'),(3,7,'2026-03-02 14:43:06','Member'),(3,8,'2026-03-10 16:30:36','Member'),(3,9,'2026-03-10 16:56:47','Leader'),(3,10,'2026-03-12 09:06:14','Leader'),(4,4,'2026-02-27 14:36:16','Member'),(4,5,'2026-02-27 14:48:51','Member'),(4,6,'2026-03-02 14:23:34','Member'),(4,7,'2026-03-02 14:43:06','Member'),(4,8,'2026-03-10 16:30:36','Leader'),(5,4,'2026-02-27 14:36:16','Leader'),(5,5,'2026-02-27 14:48:51','Member'),(5,6,'2026-03-02 14:23:34','Member'),(5,7,'2026-03-02 14:43:06','Member'),(5,8,'2026-03-10 16:30:36','Member'),(5,9,'2026-03-10 16:56:47','Member'),(6,6,'2026-03-02 14:23:34','Leader'),(6,7,'2026-03-02 14:43:06','Member'),(6,8,'2026-03-10 16:30:36','Member'),(6,9,'2026-03-10 16:56:47','Member'),(6,10,'2026-03-12 09:06:14','Member');
/*!40000 ALTER TABLE `userteam` ENABLE KEYS */;
UNLOCK TABLES;
SET @@SESSION.SQL_LOG_BIN = @MYSQLDUMP_TEMP_LOG_BIN;

