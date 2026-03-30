-- MySQL dump 10.13  Distrib 8.0.44, for Win64 (x86_64)
--
-- Host: localhost    Database: appstore
-- ------------------------------------------------------
-- Server version	9.6.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
SET @MYSQLDUMP_TEMP_LOG_BIN = @@SESSION.SQL_LOG_BIN;
SET @@SESSION.SQL_LOG_BIN= 0;

--
-- GTID state at the beginning of the backup 
--

SET @@GLOBAL.GTID_PURGED=/*!80000 '+'*/ 'ab0c278d-00b3-11f1-9604-088fc351985d:1-445';

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
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `project`
--

LOCK TABLES `project` WRITE;
/*!40000 ALTER TABLE `project` DISABLE KEYS */;
INSERT INTO `project` VALUES (17,'VII1','.','2026-03-20','2026-03-27','Active',5,NULL,'2026-03-20 11:01:07'),(19,'VII3','.','2026-03-04','2026-03-10','Active',2,NULL,'2026-03-20 11:02:01'),(20,'VII3','.','2026-03-20','2026-03-16','Active',2,NULL,'2026-03-20 11:02:50'),(21,'VII6_Update','.','2026-03-23','2026-03-25','Completed',5,NULL,'2026-03-20 11:16:16'),(23,'VII6','.','2026-03-23','2026-03-25','Active',2,NULL,'2026-03-20 14:21:58');
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
  `Progress` int DEFAULT '0',
  `IsApproved` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`TaskId`),
  KEY `AssignedToUserId` (`AssignedToUserId`),
  KEY `CreatedByUserId` (`CreatedByUserId`),
  KEY `FK_Task_Project` (`ProjectId`),
  CONSTRAINT `FK_Task_Project` FOREIGN KEY (`ProjectId`) REFERENCES `project` (`ProjectId`),
  CONSTRAINT `tasks_ibfk_1` FOREIGN KEY (`AssignedToUserId`) REFERENCES `users` (`UserId`),
  CONSTRAINT `tasks_ibfk_2` FOREIGN KEY (`CreatedByUserId`) REFERENCES `users` (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tasks`
--

LOCK TABLES `tasks` WRITE;
/*!40000 ALTER TABLE `tasks` DISABLE KEYS */;
INSERT INTO `tasks` VALUES (30,'Job_1_Update','.',5,1,'Pending','Trung bình','2026-03-20 13:23:05','2026-03-27 13:22:41',0,21,NULL,100,1),(32,'Job_3','.',5,1,'Pending','Trung bình','2026-03-20 13:23:50','2026-03-27 13:23:29',1,17,7,0,0),(33,'Job_4','.',5,1,'Pending','Trung bình','2026-03-20 13:24:15','2026-03-27 13:23:52',0,21,4,40,0),(35,'Job_6','.',3,2,'Pending','Trung bình','2026-03-20 13:59:53','2026-03-27 13:59:35',0,21,7,100,1);
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
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `team`
--

LOCK TABLES `team` WRITE;
/*!40000 ALTER TABLE `team` DISABLE KEYS */;
INSERT INTO `team` VALUES (4,'Team FE','.','2026-02-27 14:34:39'),(5,'Team BA','.','2026-02-27 14:48:51'),(6,'Team Tester','.','2026-03-02 14:23:34'),(7,'Team BE','.','2026-03-02 14:43:06'),(8,'Team BÊ','.','2026-03-10 16:30:36'),(9,'PM','.','2026-03-10 16:56:47'),(12,'New_team.Update','.','2026-03-20 15:21:11');
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
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'thuymai','d3fba36993dfb07a62a209a0e5fdaccdb0041eec9e8e94c9f3a92934dcbdd098','maivanthuy@gmail.com','Admin','2026-02-26 13:58:29'),(2,'haidang','3ff1c16ea682274f533cb9c211f619800674a8aeac5035cd502860c06e633ce6','hai@gmail.com','Manager','2026-02-26 13:59:22'),(3,'nhanvien','b01ba614e7ae08379c7fd1e568349c88e7657d1df1ad85f7180a995278006fd5','nhan@gmail.com','Employee','2026-02-26 14:25:39'),(4,'khanhthu','9338b3c03aacef47daa56feb8838f607524976cafa986df58ee1b9c9b3cda2a6','khanh@gmail.com','Employee','2026-02-27 14:05:52'),(5,'khanhthu1','7f74ebba484437837a1862b9c3635aaf93622ec3d113edf8eddb8fd185f4b054','khanhthu@gmail.com','Employee','2026-02-27 14:06:49'),(6,'nhatdang','9f24bfb5f99d6caa7c42099701ecac3a210afd91a3305254c86c2692410e54ff','dang@gmail.com','Employee','2026-02-27 16:24:11'),(7,'VanThuy','f578659196a8e11f8760d4497077d8067b98869182fbee6f0536094a69cea1a5','vanthuy@gmail.com','Employee','2026-03-20 10:21:03');
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
INSERT INTO `userteam` VALUES (1,12,'2026-03-20 16:14:47','Member'),(2,4,'2026-02-27 14:36:16','Member'),(2,5,'2026-02-27 14:48:51','Member'),(2,6,'2026-03-02 14:23:34','Member'),(2,7,'2026-03-02 14:43:06','Member'),(2,8,'2026-03-10 16:30:36','Member'),(2,9,'2026-03-10 16:56:47','Member'),(2,12,'2026-03-20 16:14:47','Member'),(3,4,'2026-02-27 14:36:16','Member'),(3,5,'2026-02-27 14:48:51','Leader'),(3,6,'2026-03-02 14:23:34','Member'),(3,7,'2026-03-02 14:43:06','Member'),(3,8,'2026-03-10 16:30:36','Member'),(3,9,'2026-03-10 16:56:47','Leader'),(3,12,'2026-03-20 16:14:47','Leader'),(4,4,'2026-02-27 14:36:16','Member'),(4,5,'2026-02-27 14:48:51','Member'),(4,6,'2026-03-02 14:23:34','Member'),(4,7,'2026-03-02 14:43:06','Member'),(4,8,'2026-03-10 16:30:36','Leader'),(4,12,'2026-03-20 16:14:47','Member'),(5,4,'2026-02-27 14:36:16','Leader'),(5,5,'2026-02-27 14:48:51','Member'),(5,6,'2026-03-02 14:23:34','Member'),(5,7,'2026-03-02 14:43:06','Member'),(5,8,'2026-03-10 16:30:36','Member'),(5,9,'2026-03-10 16:56:47','Member'),(6,6,'2026-03-02 14:23:34','Leader'),(6,7,'2026-03-02 14:43:06','Member'),(6,8,'2026-03-10 16:30:36','Member'),(6,9,'2026-03-10 16:56:47','Member'),(6,12,'2026-03-20 16:14:47','Member'),(7,12,'2026-03-20 16:14:47','Member');
/*!40000 ALTER TABLE `userteam` ENABLE KEYS */;
UNLOCK TABLES;
SET @@SESSION.SQL_LOG_BIN = @MYSQLDUMP_TEMP_LOG_BIN;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-03-30  9:03:48
