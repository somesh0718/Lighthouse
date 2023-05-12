CREATE DATABASE  IF NOT EXISTS `lighthousemh19d` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `lighthousemh19d`;
-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: localhost    Database: lighthousemh19d
-- ------------------------------------------------------
-- Server version	8.0.32

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

--
-- Table structure for table `academicyearphases`
--

DROP TABLE IF EXISTS `academicyearphases`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `academicyearphases` (
  `AcademicYearPhaseId` char(36) NOT NULL,
  `AcademicYearId` char(36) NOT NULL,
  `PhaseId` char(36) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`AcademicYearPhaseId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `academicyearphases`
--

LOCK TABLES `academicyearphases` WRITE;
/*!40000 ALTER TABLE `academicyearphases` DISABLE KEYS */;
/*!40000 ALTER TABLE `academicyearphases` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `academicyears`
--

DROP TABLE IF EXISTS `academicyears`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `academicyears` (
  `AcademicYearId` varchar(36) NOT NULL,
  `PhaseId` varchar(36) DEFAULT NULL,
  `YearName` varchar(100) NOT NULL,
  `Description` varchar(250) DEFAULT NULL,
  `StartMonth` datetime DEFAULT NULL,
  `EndMonth` datetime DEFAULT NULL,
  `IsCurrentAcademicYear` bit(1) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`AcademicYearId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `academicyears`
--

LOCK TABLES `academicyears` WRITE;
/*!40000 ALTER TABLE `academicyears` DISABLE KEYS */;
INSERT INTO `academicyears` VALUES ('606ed468-fba1-46ac-ba70-ead58dc4ab29','d4917323-570a-4018-92a5-0a96806d5045','2022 Year End','',NULL,NULL,_binary '','','2023-05-08 16:30:49.882','','2023-05-08 16:30:49.882',_binary '');
/*!40000 ALTER TABLE `academicyears` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `accountroles`
--

DROP TABLE IF EXISTS `accountroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `accountroles` (
  `AccountRoleId` varchar(36) NOT NULL,
  `AccountId` varchar(36) DEFAULT NULL,
  `RoleId` varchar(36) DEFAULT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`AccountRoleId`),
  KEY `FK_dbo.AccountRoleMap_dbo.Accounts_AccountId` (`AccountId`),
  KEY `FK_dbo.AccountRoleMap_dbo.Roles_RoleId` (`RoleId`),
  CONSTRAINT `FK_dbo.AccountRoleMap_dbo.Accounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `accounts` (`AccountId`) ON DELETE CASCADE,
  CONSTRAINT `FK_dbo.AccountRoleMap_dbo.Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`RoleId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accountroles`
--

LOCK TABLES `accountroles` WRITE;
/*!40000 ALTER TABLE `accountroles` DISABLE KEYS */;
INSERT INTO `accountroles` VALUES ('3fd3c90b-3045-4b6d-b0c6-0817376024db','e378f627-52de-41de-b2a1-64f19ad6dc44','1b91f252-32c1-43e4-8fa5-6ce0c71f5d45',NULL,'kranthiND','2023-05-09 14:04:28.503','kranthiND','2023-05-09 14:04:28.503',_binary ''),('8a4a65e3-a801-4f1d-8cbf-3b8c9b80bda5','d46b52d1-d544-40fe-8e2e-0fd4953ad005','d85259ec-9c98-4b1a-9e9f-9cb92414ee8e',NULL,'','2023-05-04 21:58:17.270','','2023-05-04 21:58:17.270',_binary ''),('d8dec215-581b-4c6d-b1d0-c2f39e53534c','e005e0e6-3bd9-4e3e-92e3-4b72c99599df','bc035793-f06f-45ca-a93c-ce97493f78fa',NULL,'','2023-05-10 16:55:34.254','','2023-05-10 16:55:34.254',_binary '');
/*!40000 ALTER TABLE `accountroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `accounts`
--

DROP TABLE IF EXISTS `accounts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `accounts` (
  `AccountId` varchar(36) NOT NULL,
  `LoginId` varchar(135) NOT NULL,
  `Password` varchar(500) NOT NULL,
  `UserId` varchar(135) NOT NULL,
  `UserName` varchar(150) DEFAULT NULL,
  `FirstName` varchar(25) DEFAULT NULL,
  `LastName` varchar(25) DEFAULT NULL,
  `Designation` varchar(100) DEFAULT NULL,
  `EmailId` varchar(100) NOT NULL,
  `Mobile` varchar(10) NOT NULL,
  `AccountType` varchar(100) DEFAULT NULL,
  `StateId` varchar(15) DEFAULT NULL,
  `DivisionId` varchar(36) DEFAULT NULL,
  `DistrictId` varchar(15) DEFAULT NULL,
  `BlockId` varchar(150) DEFAULT NULL,
  `ClusterId` varchar(150) DEFAULT NULL,
  `PasswordUpdateDate` datetime(3) DEFAULT NULL,
  `PasswordExpiredOn` datetime(3) DEFAULT NULL,
  `LastLoginDate` datetime(3) DEFAULT NULL,
  `InvalidAttempt` int DEFAULT NULL,
  `IsPasswordReset` bit(1) NOT NULL,
  `PasswordResetToken` varchar(500) DEFAULT NULL,
  `AuthToken` longtext,
  `TokenExpiredOn` datetime(3) DEFAULT NULL,
  `IsLocked` bit(1) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`AccountId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accounts`
--

LOCK TABLES `accounts` WRITE;
/*!40000 ALTER TABLE `accounts` DISABLE KEYS */;
INSERT INTO `accounts` VALUES ('d46b52d1-d544-40fe-8e2e-0fd4953ad005','kranthiND@gmail.com','SwjxBNIHDYiUcq4K6Fsl/g==','kranthiND','KranthiND','Kranthi','ND','PMU-Admin','kranthiND@gmail.com','9121680048','PMU-Admin','KA','b52fa10d-0e6f-4fc6-af51-f097f7261485','KDHLI','','','2023-05-04 21:58:17.213','2023-11-04 21:58:17.213',NULL,0,_binary '','','',NULL,_binary '\0','','2023-05-04 21:58:17.201','','2023-05-04 21:58:17.201',_binary ''),('e005e0e6-3bd9-4e3e-92e3-4b72c99599df','someshwarvt@gmail.com','Si91JG0GZeKUcq4K6Fsl/g==','someshwarvt','Someshwar Reddy','Someshwar','Reddy','Vocational Trainer','someshwarvt@gmail.com','9603071210','Vocational Trainer',NULL,NULL,NULL,NULL,NULL,NULL,'2023-11-10 16:55:34.208',NULL,0,_binary '\0',NULL,NULL,NULL,_binary '\0','','2023-05-10 16:55:34.208','','2023-05-10 16:55:34.208',_binary ''),('e378f627-52de-41de-b2a1-64f19ad6dc44','someshvc@gmail.com','SwjxBNIHDYiUcq4K6Fsl/g==','someshvc','someshvc','Somesh','Reddy','Vocational Coordinator','someshvc@gmail.com','9603071210','Vocational Coordinator','KA','b52fa10d-0e6f-4fc6-af51-f097f7261485','KDHLI','','','2023-05-09 14:04:28.297','2023-11-09 14:04:28.297',NULL,0,_binary '','','',NULL,_binary '\0','kranthiND','2023-05-09 14:04:28.274','kranthiND','2023-05-09 14:04:28.274',_binary '');
/*!40000 ALTER TABLE `accounts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `accounttransactions`
--

DROP TABLE IF EXISTS `accounttransactions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `accounttransactions` (
  `AccountTransactionId` varchar(36) NOT NULL,
  `AccountId` varchar(36) DEFAULT NULL,
  `TransactionId` varchar(36) DEFAULT NULL,
  `Rights` bit(1) NOT NULL,
  `CanAdd` bit(1) NOT NULL,
  `CanEdit` bit(1) NOT NULL,
  `CanDelete` bit(1) NOT NULL,
  `CanView` bit(1) NOT NULL,
  `CanExport` bit(1) NOT NULL,
  `ListView` bit(1) NOT NULL,
  `BasicView` bit(1) NOT NULL,
  `DetailView` bit(1) NOT NULL,
  `IsPublic` bit(1) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`AccountTransactionId`),
  KEY `FK_dbo.AccountTransactionMap_dbo.Accounts_AccountId` (`AccountId`),
  KEY `FK_dbo.AccountTransactionMap_dbo.Transactions_TransactionId` (`TransactionId`),
  CONSTRAINT `FK_dbo.AccountTransactionMap_dbo.Accounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `accounts` (`AccountId`) ON DELETE CASCADE,
  CONSTRAINT `FK_dbo.AccountTransactionMap_dbo.Transactions_TransactionId` FOREIGN KEY (`TransactionId`) REFERENCES `transactions` (`TransactionId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accounttransactions`
--

LOCK TABLES `accounttransactions` WRITE;
/*!40000 ALTER TABLE `accounttransactions` DISABLE KEYS */;
/*!40000 ALTER TABLE `accounttransactions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `accountuserotps`
--

DROP TABLE IF EXISTS `accountuserotps`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `accountuserotps` (
  `AccountOTPId` varchar(36) NOT NULL,
  `AccountId` varchar(36) DEFAULT NULL,
  `OTPId` varchar(36) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`AccountOTPId`),
  KEY `FK_AccountOTPMap_Accounts_AccountId` (`AccountId`),
  KEY `FK_AccountOTPMap_UserOTPDetails_OTPId` (`OTPId`),
  CONSTRAINT `FK_AccountOTPMap_Accounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `accounts` (`AccountId`) ON DELETE CASCADE,
  CONSTRAINT `FK_AccountOTPMap_UserOTPDetails_OTPId` FOREIGN KEY (`OTPId`) REFERENCES `userotpdetails` (`OTPId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accountuserotps`
--

LOCK TABLES `accountuserotps` WRITE;
/*!40000 ALTER TABLE `accountuserotps` DISABLE KEYS */;
/*!40000 ALTER TABLE `accountuserotps` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `accountuserterms`
--

DROP TABLE IF EXISTS `accountuserterms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `accountuserterms` (
  `AccountTermsId` varchar(36) NOT NULL,
  `AccountId` varchar(36) DEFAULT NULL,
  `TermsConditionId` varchar(36) DEFAULT NULL,
  `IsLatestTerms` bit(1) NOT NULL,
  `AcceptedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`AccountTermsId`),
  KEY `FK_AccountTermsMap_Accounts_AccountId` (`AccountId`),
  KEY `FK_AccountTermsMap_TermsConditions_TermsConditionId` (`TermsConditionId`),
  CONSTRAINT `FK_AccountTermsMap_Accounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `accounts` (`AccountId`) ON DELETE CASCADE,
  CONSTRAINT `FK_AccountTermsMap_TermsConditions_TermsConditionId` FOREIGN KEY (`TermsConditionId`) REFERENCES `termsconditions` (`TermsConditionId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accountuserterms`
--

LOCK TABLES `accountuserterms` WRITE;
/*!40000 ALTER TABLE `accountuserterms` DISABLE KEYS */;
/*!40000 ALTER TABLE `accountuserterms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `accountworklocations`
--

DROP TABLE IF EXISTS `accountworklocations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `accountworklocations` (
  `AccountWorkLocationId` varchar(36) NOT NULL,
  `AccountId` varchar(36) DEFAULT NULL,
  `StateCode` varchar(15) DEFAULT NULL,
  `DivisionId` varchar(36) DEFAULT NULL,
  `DistrictId` varchar(15) DEFAULT NULL,
  `BlockId` varchar(36) DEFAULT NULL,
  `ClusterId` varchar(36) DEFAULT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`AccountWorkLocationId`),
  KEY `FK_AccountWorkLocations_Accounts_AccountId` (`AccountId`),
  CONSTRAINT `FK_AccountWorkLocations_Accounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `accounts` (`AccountId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accountworklocations`
--

LOCK TABLES `accountworklocations` WRITE;
/*!40000 ALTER TABLE `accountworklocations` DISABLE KEYS */;
INSERT INTO `accountworklocations` VALUES ('26257467-5d90-4c3a-be6a-9041c02e5ea9','e378f627-52de-41de-b2a1-64f19ad6dc44','KA','b52fa10d-0e6f-4fc6-af51-f097f7261485','KDHLI','','',NULL,'kranthiND','2023-05-09 14:04:28.358','kranthiND','2023-05-09 14:04:28.358',_binary ''),('3d3e6c52-684f-4924-8d8a-5f1254955a4f','6bce4619-0687-4409-91f3-b3afbe62938b','KA','3fa85f64-5717-4562-b3fc-2c963f66afa6','BGLR','','',NULL,'','2023-05-03 18:42:27.143','','2023-05-03 18:42:27.143',_binary ''),('a84ef1ca-17de-4a5a-b468-97d2c9fee3dc','d46b52d1-d544-40fe-8e2e-0fd4953ad005','KA','b52fa10d-0e6f-4fc6-af51-f097f7261485','KDHLI','','',NULL,'','2023-05-04 21:58:17.243','','2023-05-04 21:58:17.243',_binary '');
/*!40000 ALTER TABLE `accountworklocations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `blocks`
--

DROP TABLE IF EXISTS `blocks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `blocks` (
  `BlockId` varchar(36) NOT NULL,
  `DistrictId` varchar(36) DEFAULT NULL,
  `BlockName` varchar(150) NOT NULL,
  `Description` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`BlockId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `blocks`
--

LOCK TABLES `blocks` WRITE;
/*!40000 ALTER TABLE `blocks` DISABLE KEYS */;
INSERT INTO `blocks` VALUES ('cfcef538-d03c-4452-afb7-56b6ff082587','KA','Testblock','','','2023-05-08 16:42:44.319','','2023-05-08 16:42:44.319',_binary '');
/*!40000 ALTER TABLE `blocks` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `broadcastmessages`
--

DROP TABLE IF EXISTS `broadcastmessages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `broadcastmessages` (
  `BroadcastMessageId` varchar(36) NOT NULL,
  `MessageText` varchar(1000) NOT NULL,
  `FromDate` datetime NOT NULL,
  `ToDate` datetime NOT NULL,
  `ApplicableFor` varchar(100) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`BroadcastMessageId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `broadcastmessages`
--

LOCK TABLES `broadcastmessages` WRITE;
/*!40000 ALTER TABLE `broadcastmessages` DISABLE KEYS */;
/*!40000 ALTER TABLE `broadcastmessages` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clusters`
--

DROP TABLE IF EXISTS `clusters`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clusters` (
  `ClusterId` varchar(36) NOT NULL,
  `BlockId` varchar(36) NOT NULL,
  `ClusterName` varchar(150) NOT NULL,
  `Description` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`ClusterId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clusters`
--

LOCK TABLES `clusters` WRITE;
/*!40000 ALTER TABLE `clusters` DISABLE KEYS */;
INSERT INTO `clusters` VALUES ('3c16a1e3-2eb8-4b55-b16f-3634f31688b8','cfcef538-d03c-4452-afb7-56b6ff082587','Testcluster','','','2023-05-08 16:44:48.868','','2023-05-08 16:44:48.868',_binary '');
/*!40000 ALTER TABLE `clusters` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `complaintregistrations`
--

DROP TABLE IF EXISTS `complaintregistrations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `complaintregistrations` (
  `ComplaintRegistrationId` varchar(36) NOT NULL,
  `UserType` varchar(45) DEFAULT NULL,
  `UserName` varchar(150) DEFAULT NULL,
  `EmailId` varchar(150) DEFAULT NULL,
  `Subject` varchar(200) DEFAULT NULL,
  `IssueDetails` text,
  `IssueStatus` varchar(45) DEFAULT NULL,
  `Attachment` varchar(250) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `ResolvedBy` varchar(30) DEFAULT NULL,
  `ResolvedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) DEFAULT NULL,
  PRIMARY KEY (`ComplaintRegistrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `complaintregistrations`
--

LOCK TABLES `complaintregistrations` WRITE;
/*!40000 ALTER TABLE `complaintregistrations` DISABLE KEYS */;
/*!40000 ALTER TABLE `complaintregistrations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `coordinatortrainers`
--

DROP TABLE IF EXISTS `coordinatortrainers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `coordinatortrainers` (
  `VCId` varchar(36) NOT NULL,
  `VTId` varchar(36) NOT NULL,
  `VTPId` varchar(36) NOT NULL,
  `DateOfJoining` datetime NOT NULL,
  `DateOfResignation` datetime DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VCId`,`VTId`,`VTPId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `coordinatortrainers`
--

LOCK TABLES `coordinatortrainers` WRITE;
/*!40000 ALTER TABLE `coordinatortrainers` DISABLE KEYS */;
/*!40000 ALTER TABLE `coordinatortrainers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `countries`
--

DROP TABLE IF EXISTS `countries`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `countries` (
  `CountryCode` varchar(15) NOT NULL,
  `CountryName` varchar(75) NOT NULL,
  `ISDCode` varchar(20) DEFAULT NULL,
  `ISOCode` varchar(5) DEFAULT NULL,
  `CurrencyName` varchar(100) DEFAULT NULL,
  `CurrencyCode` varchar(10) DEFAULT NULL,
  `CountryIcon` varchar(100) DEFAULT NULL,
  `Description` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`CountryCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `countries`
--

LOCK TABLES `countries` WRITE;
/*!40000 ALTER TABLE `countries` DISABLE KEYS */;
INSERT INTO `countries` VALUES ('IN','India','+91','IND','Indian Rupees','INR','','Indian Rupees','','2023-05-04 21:50:41.735','','2023-05-04 21:50:41.735',_binary '');
/*!40000 ALTER TABLE `countries` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `coursematerials`
--

DROP TABLE IF EXISTS `coursematerials`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `coursematerials` (
  `CourseMaterialId` varchar(36) NOT NULL,
  `VTId` varchar(36) NOT NULL,
  `AcademicYearId` varchar(36) NOT NULL,
  `ClassId` varchar(36) NOT NULL,
  `ReceiptDate` datetime DEFAULT NULL,
  `Details` varchar(350) DEFAULT NULL,
  `CMStatus` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`CourseMaterialId`),
  KEY `FK_CourseMaterials_AcademicYears_idx` (`AcademicYearId`),
  KEY `FK_CourseMaterials_VTClasses_idx` (`ClassId`),
  CONSTRAINT `FK_CourseMaterials_AcademicYears` FOREIGN KEY (`AcademicYearId`) REFERENCES `academicyears` (`AcademicYearId`),
  CONSTRAINT `FK_CourseMaterials_VTClasses` FOREIGN KEY (`ClassId`) REFERENCES `vtclasses` (`VTClassId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `coursematerials`
--

LOCK TABLES `coursematerials` WRITE;
/*!40000 ALTER TABLE `coursematerials` DISABLE KEYS */;
/*!40000 ALTER TABLE `coursematerials` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `coursemodules`
--

DROP TABLE IF EXISTS `coursemodules`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `coursemodules` (
  `CourseModuleId` varchar(36) NOT NULL,
  `ClassId` varchar(36) NOT NULL,
  `ModuleTypeId` varchar(50) NOT NULL,
  `SectorId` varchar(36) DEFAULT NULL,
  `JobRoleId` varchar(36) DEFAULT NULL,
  `UnitName` varchar(200) NOT NULL,
  `DisplayOrder` int NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) DEFAULT NULL,
  PRIMARY KEY (`CourseModuleId`),
  KEY `FK_CourseModules_SchoolClasses_idx` (`ClassId`),
  KEY `FK_CourseModules_Sectors_idx` (`SectorId`),
  KEY `FK_CourseModules_JobRoles_idx` (`JobRoleId`),
  CONSTRAINT `FK_CourseModules_JobRoles` FOREIGN KEY (`JobRoleId`) REFERENCES `jobroles` (`JobRoleId`),
  CONSTRAINT `FK_CourseModules_SchoolClasses` FOREIGN KEY (`ClassId`) REFERENCES `schoolclasses` (`ClassId`),
  CONSTRAINT `FK_CourseModules_Sectors` FOREIGN KEY (`SectorId`) REFERENCES `sectors` (`SectorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `coursemodules`
--

LOCK TABLES `coursemodules` WRITE;
/*!40000 ALTER TABLE `coursemodules` DISABLE KEYS */;
/*!40000 ALTER TABLE `coursemodules` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `courseunitsessions`
--

DROP TABLE IF EXISTS `courseunitsessions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `courseunitsessions` (
  `CourseUnitSessionId` varchar(36) NOT NULL,
  `CourseModuleId` varchar(36) DEFAULT NULL,
  `SessionName` varchar(250) DEFAULT NULL,
  `DisplayOrder` int DEFAULT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) DEFAULT NULL,
  PRIMARY KEY (`CourseUnitSessionId`),
  KEY `FK_CourseUnitSessions_CourseModules_idx` (`CourseModuleId`),
  CONSTRAINT `FK_CourseUnitSessions_CourseModules` FOREIGN KEY (`CourseModuleId`) REFERENCES `coursemodules` (`CourseModuleId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `courseunitsessions`
--

LOCK TABLES `courseunitsessions` WRITE;
/*!40000 ALTER TABLE `courseunitsessions` DISABLE KEYS */;
/*!40000 ALTER TABLE `courseunitsessions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `datatypes`
--

DROP TABLE IF EXISTS `datatypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `datatypes` (
  `DataTypeId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(350) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`DataTypeId`)
) ENGINE=InnoDB AUTO_INCREMENT=78 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `datatypes`
--

LOCK TABLES `datatypes` WRITE;
/*!40000 ALTER TABLE `datatypes` DISABLE KEYS */;
INSERT INTO `datatypes` VALUES (72,'SchoolType','SchoolType','','2023-05-08 18:46:35.340','','2023-05-08 18:46:35.340',_binary ''),(73,'SchoolManagement','SchoolManagement','Somesh','2023-05-08 18:46:35.340',NULL,'2023-05-08 18:46:35.340',_binary ''),(74,'SocialCategory','SocialCategory','kranthiND','2023-05-09 15:01:09.771','kranthiND','2023-05-09 15:01:09.771',_binary ''),(75,'AcademicYears','AcademicYears','kranthiND','2023-05-09 15:01:57.614','kranthiND','2023-05-09 15:01:57.614',_binary ''),(76,'ProfessionalQualification','ProfessionalQualification','kranthiND','2023-05-09 15:01:57.614',NULL,'2023-05-09 15:01:57.614',_binary ''),(77,'NatureOfAppointment','NatureOfAppointment','kranthiND','2023-05-09 15:01:57.614',NULL,'2023-05-09 15:01:57.614',_binary '');
/*!40000 ALTER TABLE `datatypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `datavalues`
--

DROP TABLE IF EXISTS `datavalues`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `datavalues` (
  `DataValueId` varchar(5) NOT NULL,
  `DataTypeId` varchar(50) NOT NULL,
  `ParentId` varchar(50) DEFAULT NULL,
  `Code` varchar(10) DEFAULT NULL,
  `Name` varchar(200) NOT NULL,
  `Description` varchar(350) DEFAULT NULL,
  `DisplayOrder` int NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`DataValueId`,`DataTypeId`),
  UNIQUE KEY `UC_DataValueId_DataTypeId` (`DataValueId`,`DataTypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `datavalues`
--

LOCK TABLES `datavalues` WRITE;
/*!40000 ALTER TABLE `datavalues` DISABLE KEYS */;
INSERT INTO `datavalues` VALUES ('101','72','SchoolType','1','Higher Secondary with grades 1 to 12','Higher Secondary with grades 1 to 12',1,'','2023-05-08 18:50:10.097','','2023-05-08 18:50:10.097',_binary ''),('102','72','SchoolType','1','Secondary with grades 1 to 10','Secondary with grades 1 to 10',2,'kranthiND','2023-05-08 18:50:10.097',NULL,'2023-05-08 18:50:10.097',_binary '\0'),('103','73','SchoolManagement','1','Tribal Welfare Department','Tribal Welfare Department',3,'kranthiND','2023-05-08 18:50:10.097',NULL,'2023-05-08 18:50:10.097',_binary ''),('104','73','SchoolManagement','1','Social Welfare Depatment','Social Welfare Depatment',4,'kranthiND','2023-05-08 18:50:10.097',NULL,'2023-05-08 18:50:10.097',_binary ''),('105','74',NULL,'SCG','General','General',1,'kranthiND','2023-05-08 18:50:10.097',NULL,'2023-05-08 18:50:10.097',_binary ''),('106','76',NULL,'51','Certificate Course In Concerned Vocational Sector','Certificate Course In Concerned Vocational Sector',1,'kranthiND','2023-05-08 18:50:10.097',NULL,'2023-05-08 18:50:10.097',_binary ''),('107','77',NULL,'11','Through VTP','Through VTP',1,'kranthiND','2023-05-08 18:50:10.097',NULL,'2023-05-08 18:50:10.097',_binary '');
/*!40000 ALTER TABLE `datavalues` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `districts`
--

DROP TABLE IF EXISTS `districts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `districts` (
  `DistrictCode` varchar(15) NOT NULL,
  `StateCode` varchar(15) NOT NULL,
  `DivisionId` varchar(36) DEFAULT NULL,
  `DistrictName` varchar(50) NOT NULL,
  `Description` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`DistrictCode`),
  KEY `FK_dbo.Cities_dbo.States_StateCode` (`StateCode`),
  CONSTRAINT `FK_dbo.Cities_dbo.States_StateCode` FOREIGN KEY (`StateCode`) REFERENCES `states` (`StateCode`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `districts`
--

LOCK TABLES `districts` WRITE;
/*!40000 ALTER TABLE `districts` DISABLE KEYS */;
INSERT INTO `districts` VALUES ('KDHLI','KA','b52fa10d-0e6f-4fc6-af51-f097f7261485','Kodihalli',NULL,'Somesh','2023-05-04 21:54:14.813',NULL,'2023-05-04 21:54:14.813',_binary '');
/*!40000 ALTER TABLE `districts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `divisiondistricts`
--

DROP TABLE IF EXISTS `divisiondistricts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `divisiondistricts` (
  `DivisionDistrictId` char(36) NOT NULL,
  `DivisionId` char(36) NOT NULL,
  `DistrictId` char(36) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`DivisionDistrictId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `divisiondistricts`
--

LOCK TABLES `divisiondistricts` WRITE;
/*!40000 ALTER TABLE `divisiondistricts` DISABLE KEYS */;
/*!40000 ALTER TABLE `divisiondistricts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `divisions`
--

DROP TABLE IF EXISTS `divisions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `divisions` (
  `DivisionId` varchar(36) NOT NULL,
  `StateCode` varchar(15) DEFAULT NULL,
  `DivisionName` varchar(100) NOT NULL,
  `Description` varchar(250) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`DivisionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `divisions`
--

LOCK TABLES `divisions` WRITE;
/*!40000 ALTER TABLE `divisions` DISABLE KEYS */;
INSERT INTO `divisions` VALUES ('b52fa10d-0e6f-4fc6-af51-f097f7261485','KA','Bangalore','','','2023-05-04 21:54:14.813','','2023-05-04 21:54:14.813',_binary '');
/*!40000 ALTER TABLE `divisions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `drpdailyreporting`
--

DROP TABLE IF EXISTS `drpdailyreporting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drpdailyreporting` (
  `DRPDailyReportingId` varchar(36) NOT NULL,
  `DRPId` varchar(36) DEFAULT NULL,
  `ReportDate` datetime(3) DEFAULT NULL,
  `ReportType` varchar(50) DEFAULT NULL,
  `SchoolId` varchar(36) DEFAULT NULL,
  `WorkTypeDetails` varchar(250) DEFAULT NULL,
  `GeoLocation` varchar(50) DEFAULT NULL,
  `Latitude` varchar(20) DEFAULT NULL,
  `Longitude` varchar(20) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`DRPDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drpdailyreporting`
--

LOCK TABLES `drpdailyreporting` WRITE;
/*!40000 ALTER TABLE `drpdailyreporting` DISABLE KEYS */;
/*!40000 ALTER TABLE `drpdailyreporting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `drprholidays`
--

DROP TABLE IF EXISTS `drprholidays`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drprholidays` (
  `DRPRHolidayId` varchar(36) NOT NULL,
  `DRPDailyReportingId` varchar(36) NOT NULL,
  `HolidayTypeId` varchar(5) NOT NULL,
  `HolidayDetails` varchar(350) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`DRPRHolidayId`),
  KEY `FK_DRPRHolidays_DRPDailyReporting_idx` (`DRPDailyReportingId`),
  CONSTRAINT `FK_DRPRHolidays_DRPDailyReporting` FOREIGN KEY (`DRPDailyReportingId`) REFERENCES `drpdailyreporting` (`DRPDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drprholidays`
--

LOCK TABLES `drprholidays` WRITE;
/*!40000 ALTER TABLE `drprholidays` DISABLE KEYS */;
/*!40000 ALTER TABLE `drprholidays` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `drprindustryexposurevisits`
--

DROP TABLE IF EXISTS `drprindustryexposurevisits`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drprindustryexposurevisits` (
  `DRPRIndustryExposureVisitId` varchar(36) NOT NULL,
  `DRPDailyReportingId` varchar(36) NOT NULL,
  `TypeOfIndustryLinkage` varchar(250) DEFAULT NULL,
  `ContactPersonName` varchar(100) NOT NULL,
  `ContactPersonMobile` varchar(15) NOT NULL,
  `ContactPersonEmail` varchar(100) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`DRPRIndustryExposureVisitId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drprindustryexposurevisits`
--

LOCK TABLES `drprindustryexposurevisits` WRITE;
/*!40000 ALTER TABLE `drprindustryexposurevisits` DISABLE KEYS */;
/*!40000 ALTER TABLE `drprindustryexposurevisits` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `drprleaves`
--

DROP TABLE IF EXISTS `drprleaves`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drprleaves` (
  `DRPRLeaveId` varchar(36) NOT NULL,
  `DRPDailyReportingId` varchar(36) NOT NULL,
  `LeaveTypeId` varchar(5) NOT NULL,
  `LeaveApprovalStatus` varchar(20) NOT NULL,
  `LeaveApprover` varchar(5) NOT NULL,
  `LeaveReason` varchar(350) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`DRPRLeaveId`),
  KEY `FK_DRPRLeaves_DRPDailyReporting_idx` (`DRPDailyReportingId`),
  CONSTRAINT `FK_DRPRLeaves_DRPDailyReporting` FOREIGN KEY (`DRPDailyReportingId`) REFERENCES `drpdailyreporting` (`DRPDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drprleaves`
--

LOCK TABLES `drprleaves` WRITE;
/*!40000 ALTER TABLE `drprleaves` DISABLE KEYS */;
/*!40000 ALTER TABLE `drprleaves` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `drprschoolvisits`
--

DROP TABLE IF EXISTS `drprschoolvisits`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drprschoolvisits` (
  `DRPRSchoolVisitId` varchar(36) NOT NULL,
  `DRPDailyReportingId` varchar(36) NOT NULL,
  `SchoolId` varchar(36) NOT NULL,
  `WorkDetails` varchar(350) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`DRPRSchoolVisitId`),
  KEY `FK_DRPRSchoolVisits_DRPDailyReporting_idx` (`DRPDailyReportingId`),
  CONSTRAINT `FK_DRPRSchoolVisits_DRPDailyReporting` FOREIGN KEY (`DRPDailyReportingId`) REFERENCES `drpdailyreporting` (`DRPDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drprschoolvisits`
--

LOCK TABLES `drprschoolvisits` WRITE;
/*!40000 ALTER TABLE `drprschoolvisits` DISABLE KEYS */;
/*!40000 ALTER TABLE `drprschoolvisits` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `drprworkingdaytypes`
--

DROP TABLE IF EXISTS `drprworkingdaytypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `drprworkingdaytypes` (
  `DRPRWorkingDayTypeId` varchar(36) NOT NULL,
  `DRPDailyReportingId` varchar(36) NOT NULL,
  `WorkingTypeId` varchar(5) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`DRPRWorkingDayTypeId`),
  KEY `FK_DRPRWorkingDayTypes_WorkingTypes_idx` (`WorkingTypeId`),
  KEY `FK_DRPRHolidays_DRPDailyReporting_idx` (`DRPDailyReportingId`),
  CONSTRAINT `FK_DRPRWorkingDayTypes_DRPDailyReporting` FOREIGN KEY (`DRPDailyReportingId`) REFERENCES `drpdailyreporting` (`DRPDailyReportingId`),
  CONSTRAINT `FK_DRPRWorkingDayTypes_WorkingTypes` FOREIGN KEY (`WorkingTypeId`) REFERENCES `datavalues` (`DataValueId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `drprworkingdaytypes`
--

LOCK TABLES `drprworkingdaytypes` WRITE;
/*!40000 ALTER TABLE `drprworkingdaytypes` DISABLE KEYS */;
/*!40000 ALTER TABLE `drprworkingdaytypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dsclasses`
--

DROP TABLE IF EXISTS `dsclasses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dsclasses` (
  `DsClassId` bigint NOT NULL AUTO_INCREMENT,
  `AcademicYearId` varchar(36) DEFAULT NULL,
  `VTPId` varchar(36) DEFAULT NULL,
  `SectorId` varchar(36) DEFAULT NULL,
  `DivisionId` varchar(36) DEFAULT NULL,
  `DistrictId` varchar(10) DEFAULT NULL,
  `SchoolManagementId` varchar(36) DEFAULT NULL,
  `TotalClasses` int DEFAULT NULL,
  `Class9` int DEFAULT NULL,
  `Class10` int DEFAULT NULL,
  `Class11` int DEFAULT NULL,
  `Class12` int DEFAULT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `ClassId` varchar(36) DEFAULT NULL,
  `JobRoleId` varchar(36) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`DsClassId`)
) ENGINE=InnoDB AUTO_INCREMENT=462978 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dsclasses`
--

LOCK TABLES `dsclasses` WRITE;
/*!40000 ALTER TABLE `dsclasses` DISABLE KEYS */;
/*!40000 ALTER TABLE `dsclasses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dscoordinatorattendances`
--

DROP TABLE IF EXISTS `dscoordinatorattendances`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dscoordinatorattendances` (
  `DsCoordinatorAttendanceId` bigint NOT NULL AUTO_INCREMENT,
  `AcademicYearId` varchar(36) DEFAULT NULL,
  `VTPId` varchar(36) DEFAULT NULL,
  `SectorId` varchar(36) DEFAULT NULL,
  `DivisionId` varchar(36) DEFAULT NULL,
  `DistrictId` varchar(10) DEFAULT NULL,
  `SchoolManagementId` varchar(36) DEFAULT NULL,
  `VCSchoolSectorId` varchar(36) DEFAULT NULL,
  `SchoolId` varchar(36) DEFAULT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `ReportingMonth` datetime DEFAULT NULL,
  `VCReporting` float DEFAULT NULL,
  `WorkingDays` int DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`DsCoordinatorAttendanceId`)
) ENGINE=InnoDB AUTO_INCREMENT=44155 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dscoordinatorattendances`
--

LOCK TABLES `dscoordinatorattendances` WRITE;
/*!40000 ALTER TABLE `dscoordinatorattendances` DISABLE KEYS */;
/*!40000 ALTER TABLE `dscoordinatorattendances` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dscoursematerials`
--

DROP TABLE IF EXISTS `dscoursematerials`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dscoursematerials` (
  `DsCourseMaterialId` bigint NOT NULL AUTO_INCREMENT,
  `AcademicYearId` varchar(36) DEFAULT NULL,
  `VTPId` varchar(36) DEFAULT NULL,
  `SectorId` varchar(36) DEFAULT NULL,
  `DivisionId` varchar(36) DEFAULT NULL,
  `DistrictId` varchar(10) DEFAULT NULL,
  `SchoolManagementId` varchar(36) DEFAULT NULL,
  `VTClassId` varchar(36) DEFAULT NULL,
  `CourseMaterialId` varchar(36) DEFAULT NULL,
  `CMClassId` varchar(36) DEFAULT NULL,
  `ReceiptDate` datetime DEFAULT NULL,
  `CMStatus` varchar(10) DEFAULT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `ClassId` varchar(36) DEFAULT NULL,
  `JobRoleId` varchar(36) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`DsCourseMaterialId`)
) ENGINE=InnoDB AUTO_INCREMENT=699338 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dscoursematerials`
--

LOCK TABLES `dscoursematerials` WRITE;
/*!40000 ALTER TABLE `dscoursematerials` DISABLE KEYS */;
/*!40000 ALTER TABLE `dscoursematerials` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dsdatamanagement`
--

DROP TABLE IF EXISTS `dsdatamanagement`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dsdatamanagement` (
  `AcademicYearId` varchar(36) NOT NULL,
  `ReportDate` datetime NOT NULL,
  `DataType` varchar(100) NOT NULL,
  `RowCount` int DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`AcademicYearId`,`ReportDate`,`DataType`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dsdatamanagement`
--

LOCK TABLES `dsdatamanagement` WRITE;
/*!40000 ALTER TABLE `dsdatamanagement` DISABLE KEYS */;
/*!40000 ALTER TABLE `dsdatamanagement` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dsfieldvisits`
--

DROP TABLE IF EXISTS `dsfieldvisits`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dsfieldvisits` (
  `DsFieldVisitId` bigint NOT NULL AUTO_INCREMENT,
  `AcademicYearId` varchar(36) DEFAULT NULL,
  `VTPId` varchar(36) DEFAULT NULL,
  `SectorId` varchar(36) DEFAULT NULL,
  `DivisionId` varchar(36) DEFAULT NULL,
  `DistrictId` varchar(10) DEFAULT NULL,
  `SchoolManagementId` varchar(36) DEFAULT NULL,
  `ClassTaughtId` varchar(36) DEFAULT NULL,
  `SchoolId` varchar(36) DEFAULT NULL,
  `VTId` varchar(36) DEFAULT NULL,
  `QuarterInYear` int DEFAULT NULL,
  `ReportMonth` varchar(50) DEFAULT NULL,
  `NoOfSchoolClasses` int DEFAULT NULL,
  `FieldIndustryCount` int DEFAULT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `ClassId` varchar(36) DEFAULT NULL,
  `JobRoleId` varchar(36) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`DsFieldVisitId`)
) ENGINE=InnoDB AUTO_INCREMENT=446814 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dsfieldvisits`
--

LOCK TABLES `dsfieldvisits` WRITE;
/*!40000 ALTER TABLE `dsfieldvisits` DISABLE KEYS */;
/*!40000 ALTER TABLE `dsfieldvisits` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dsguestlectures`
--

DROP TABLE IF EXISTS `dsguestlectures`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dsguestlectures` (
  `DsGuestLectureId` bigint NOT NULL AUTO_INCREMENT,
  `AcademicYearId` varchar(36) DEFAULT NULL,
  `VTPId` varchar(36) DEFAULT NULL,
  `SectorId` varchar(36) DEFAULT NULL,
  `DivisionId` varchar(36) DEFAULT NULL,
  `DistrictId` varchar(10) DEFAULT NULL,
  `SchoolManagementId` varchar(36) DEFAULT NULL,
  `ClassTaughtId` varchar(36) DEFAULT NULL,
  `SchoolId` varchar(36) DEFAULT NULL,
  `VTId` varchar(36) DEFAULT NULL,
  `ReportMonth` varchar(50) DEFAULT NULL,
  `NoOfSchoolClasses` int DEFAULT NULL,
  `GuestLectureCount` int DEFAULT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `ClassId` varchar(36) DEFAULT NULL,
  `JobRoleId` varchar(36) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`DsGuestLectureId`)
) ENGINE=InnoDB AUTO_INCREMENT=831991 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dsguestlectures`
--

LOCK TABLES `dsguestlectures` WRITE;
/*!40000 ALTER TABLE `dsguestlectures` DISABLE KEYS */;
/*!40000 ALTER TABLE `dsguestlectures` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dsschoolclasses`
--

DROP TABLE IF EXISTS `dsschoolclasses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dsschoolclasses` (
  `DsSchoolClassId` bigint NOT NULL AUTO_INCREMENT,
  `AcademicYearId` varchar(36) DEFAULT NULL,
  `VTPId` varchar(36) DEFAULT NULL,
  `SectorId` varchar(36) DEFAULT NULL,
  `DivisionId` varchar(36) DEFAULT NULL,
  `DistrictId` varchar(10) DEFAULT NULL,
  `SchoolManagementId` varchar(36) DEFAULT NULL,
  `VTClassId` varchar(36) DEFAULT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `ClassId` varchar(36) DEFAULT NULL,
  `JobRoleId` varchar(36) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`DsSchoolClassId`)
) ENGINE=InnoDB AUTO_INCREMENT=1397512 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dsschoolclasses`
--

LOCK TABLES `dsschoolclasses` WRITE;
/*!40000 ALTER TABLE `dsschoolclasses` DISABLE KEYS */;
/*!40000 ALTER TABLE `dsschoolclasses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dsschools`
--

DROP TABLE IF EXISTS `dsschools`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dsschools` (
  `DsSchoolId` bigint NOT NULL AUTO_INCREMENT,
  `AcademicYearId` varchar(36) DEFAULT NULL,
  `VTPId` varchar(36) DEFAULT NULL,
  `SectorId` varchar(36) DEFAULT NULL,
  `DivisionId` varchar(36) DEFAULT NULL,
  `DistrictId` varchar(10) DEFAULT NULL,
  `SchoolManagementId` varchar(36) DEFAULT NULL,
  `ApprovedSchoolId` varchar(36) DEFAULT NULL,
  `ImplementedSchoolId` varchar(36) DEFAULT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `ClassId` varchar(36) DEFAULT NULL,
  `JobRoleId` varchar(36) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`DsSchoolId`)
) ENGINE=InnoDB AUTO_INCREMENT=1691386 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dsschools`
--

LOCK TABLES `dsschools` WRITE;
/*!40000 ALTER TABLE `dsschools` DISABLE KEYS */;
/*!40000 ALTER TABLE `dsschools` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dssectorjobroles`
--

DROP TABLE IF EXISTS `dssectorjobroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dssectorjobroles` (
  `DsSectorJobRoleId` bigint NOT NULL AUTO_INCREMENT,
  `AcademicYearId` varchar(36) DEFAULT NULL,
  `VTPId` varchar(36) DEFAULT NULL,
  `SectorId` varchar(36) DEFAULT NULL,
  `DivisionId` varchar(36) DEFAULT NULL,
  `DistrictId` varchar(10) DEFAULT NULL,
  `SchoolManagementId` varchar(36) DEFAULT NULL,
  `VTSchoolSectorId` varchar(36) DEFAULT NULL,
  `VTId` varchar(36) DEFAULT NULL,
  `SchoolId` varchar(36) DEFAULT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `JobRoleId` varchar(36) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`DsSectorJobRoleId`)
) ENGINE=InnoDB AUTO_INCREMENT=583690 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dssectorjobroles`
--

LOCK TABLES `dssectorjobroles` WRITE;
/*!40000 ALTER TABLE `dssectorjobroles` DISABLE KEYS */;
/*!40000 ALTER TABLE `dssectorjobroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dsstudentattendances`
--

DROP TABLE IF EXISTS `dsstudentattendances`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dsstudentattendances` (
  `DsStudentAttendanceId` bigint NOT NULL AUTO_INCREMENT,
  `AcademicYearId` varchar(36) DEFAULT NULL,
  `VTSchoolSectorId` varchar(36) DEFAULT NULL,
  `ClassId` varchar(36) DEFAULT NULL,
  `ReportingMonth` datetime DEFAULT NULL,
  `VTWorkingDays` int DEFAULT NULL,
  `AttendanceDays` int DEFAULT NULL,
  `EnrolledBoys` int DEFAULT NULL,
  `EnrolledGirls` int DEFAULT NULL,
  `EnrolledStudents` int DEFAULT NULL,
  `AttendanceBoys` int DEFAULT NULL,
  `AttendanceGirls` int DEFAULT NULL,
  `StudentAttendances` int DEFAULT NULL,
  `AttendanceBoysInPerc` float DEFAULT NULL,
  `AttendanceGirlsInPerc` float DEFAULT NULL,
  `AttendancesInPerc` float DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`DsStudentAttendanceId`)
) ENGINE=InnoDB AUTO_INCREMENT=138611 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dsstudentattendances`
--

LOCK TABLES `dsstudentattendances` WRITE;
/*!40000 ALTER TABLE `dsstudentattendances` DISABLE KEYS */;
/*!40000 ALTER TABLE `dsstudentattendances` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dsstudents`
--

DROP TABLE IF EXISTS `dsstudents`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dsstudents` (
  `DsStudentId` bigint NOT NULL AUTO_INCREMENT,
  `AcademicYearId` varchar(36) DEFAULT NULL,
  `VTPId` varchar(36) DEFAULT NULL,
  `SectorId` varchar(36) DEFAULT NULL,
  `SchoolId` varchar(36) DEFAULT NULL,
  `DivisionId` varchar(36) DEFAULT NULL,
  `DistrictId` varchar(10) DEFAULT NULL,
  `SchoolManagementId` varchar(36) DEFAULT NULL,
  `Boys` int DEFAULT NULL,
  `Girls` int DEFAULT NULL,
  `Total` int DEFAULT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `ClassId` varchar(36) DEFAULT NULL,
  `JobRoleId` varchar(36) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`DsStudentId`)
) ENGINE=InnoDB AUTO_INCREMENT=262494 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dsstudents`
--

LOCK TABLES `dsstudents` WRITE;
/*!40000 ALTER TABLE `dsstudents` DISABLE KEYS */;
/*!40000 ALTER TABLE `dsstudents` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dstoolsandequipments`
--

DROP TABLE IF EXISTS `dstoolsandequipments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dstoolsandequipments` (
  `DsToolsAndEquipmentId` bigint NOT NULL AUTO_INCREMENT,
  `AcademicYearId` varchar(36) DEFAULT NULL,
  `VTPId` varchar(36) DEFAULT NULL,
  `SectorId` varchar(36) DEFAULT NULL,
  `DivisionId` varchar(36) DEFAULT NULL,
  `DistrictId` varchar(10) DEFAULT NULL,
  `SchoolManagementId` varchar(36) DEFAULT NULL,
  `TESectorId` varchar(36) DEFAULT NULL,
  `TEJobRoleId` varchar(36) DEFAULT NULL,
  `ToolEquipmentId` varchar(36) DEFAULT NULL,
  `ReceiptDate` datetime DEFAULT NULL,
  `TEReceiveStatus` varchar(50) DEFAULT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `JobRoleId` varchar(36) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`DsToolsAndEquipmentId`)
) ENGINE=InnoDB AUTO_INCREMENT=335449 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dstoolsandequipments`
--

LOCK TABLES `dstoolsandequipments` WRITE;
/*!40000 ALTER TABLE `dstoolsandequipments` DISABLE KEYS */;
/*!40000 ALTER TABLE `dstoolsandequipments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dstrainerattendances`
--

DROP TABLE IF EXISTS `dstrainerattendances`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dstrainerattendances` (
  `DsTrainerAttendanceId` bigint NOT NULL AUTO_INCREMENT,
  `AcademicYearId` varchar(36) DEFAULT NULL,
  `VTPId` varchar(36) DEFAULT NULL,
  `VTId` varchar(36) DEFAULT NULL,
  `VTSchoolSectorId` varchar(36) DEFAULT NULL,
  `ReportingMonth` datetime DEFAULT NULL,
  `VTReporting` int DEFAULT NULL,
  `OnLeave` int DEFAULT NULL,
  `Holiday` int DEFAULT NULL,
  `ObservationDay` int DEFAULT NULL,
  `WorkingDays` int DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`DsTrainerAttendanceId`)
) ENGINE=InnoDB AUTO_INCREMENT=126705 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dstrainerattendances`
--

LOCK TABLES `dstrainerattendances` WRITE;
/*!40000 ALTER TABLE `dstrainerattendances` DISABLE KEYS */;
/*!40000 ALTER TABLE `dstrainerattendances` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dstrainers`
--

DROP TABLE IF EXISTS `dstrainers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dstrainers` (
  `DsTrainerId` bigint NOT NULL AUTO_INCREMENT,
  `AcademicYearId` varchar(36) DEFAULT NULL,
  `VTPId` varchar(36) DEFAULT NULL,
  `SectorId` varchar(36) DEFAULT NULL,
  `DivisionId` varchar(36) DEFAULT NULL,
  `DistrictId` varchar(10) DEFAULT NULL,
  `SchoolManagementId` varchar(36) DEFAULT NULL,
  `TotalVT` varchar(36) DEFAULT NULL,
  `PlacedVT` varchar(36) DEFAULT NULL,
  `ReportedVT` varchar(36) DEFAULT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `ClassId` varchar(36) DEFAULT NULL,
  `JobRoleId` varchar(36) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`DsTrainerId`)
) ENGINE=InnoDB AUTO_INCREMENT=182970 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dstrainers`
--

LOCK TABLES `dstrainers` WRITE;
/*!40000 ALTER TABLE `dstrainers` DISABLE KEYS */;
/*!40000 ALTER TABLE `dstrainers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employeeaddresses`
--

DROP TABLE IF EXISTS `employeeaddresses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employeeaddresses` (
  `AddressId` char(36) NOT NULL,
  `EmployeeId` char(36) NOT NULL,
  `SequenceNo` int NOT NULL,
  `AddressType` varchar(25) NOT NULL,
  `StateCode` varchar(15) NOT NULL,
  `DistrictCode` varchar(15) DEFAULT NULL,
  `Address1` varchar(150) NOT NULL,
  `Address2` varchar(150) DEFAULT NULL,
  `Address3` varchar(150) DEFAULT NULL,
  `Pincode` varchar(6) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`AddressId`),
  KEY `FK_EmployeeAddresses_States_StateCode` (`StateCode`),
  KEY `FK_EmployeeAddresses_Employees_EmployeeId` (`EmployeeId`),
  KEY `FK_EmployeeAddresses_Districts_DistrictCode` (`DistrictCode`),
  CONSTRAINT `FK_EmployeeAddresses_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `employees` (`AccountId`) ON DELETE CASCADE,
  CONSTRAINT `FK_EmployeeAddresses_States_StateCode` FOREIGN KEY (`StateCode`) REFERENCES `states` (`StateCode`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employeeaddresses`
--

LOCK TABLES `employeeaddresses` WRITE;
/*!40000 ALTER TABLE `employeeaddresses` DISABLE KEYS */;
/*!40000 ALTER TABLE `employeeaddresses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employeeeducations`
--

DROP TABLE IF EXISTS `employeeeducations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employeeeducations` (
  `EducationId` char(36) NOT NULL,
  `EmployeeId` char(36) NOT NULL,
  `SequenceNo` int NOT NULL,
  `Institution` varchar(150) NOT NULL,
  `DateFrom` datetime(3) NOT NULL,
  `DateTo` datetime(3) DEFAULT NULL,
  `IsCurrent` bit(1) NOT NULL,
  `EducationType` varchar(20) NOT NULL,
  `Grade` varchar(150) DEFAULT NULL,
  `GradeType` varchar(20) DEFAULT NULL,
  `FieldOfStudy` varchar(150) DEFAULT NULL,
  `Summary` varchar(250) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`EducationId`),
  KEY `FK_EmployeeEducations_Employees_EmployeeId` (`EmployeeId`),
  CONSTRAINT `FK_EmployeeEducations_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `employees` (`AccountId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employeeeducations`
--

LOCK TABLES `employeeeducations` WRITE;
/*!40000 ALTER TABLE `employeeeducations` DISABLE KEYS */;
/*!40000 ALTER TABLE `employeeeducations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employeeexperiences`
--

DROP TABLE IF EXISTS `employeeexperiences`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employeeexperiences` (
  `ExperienceId` char(36) NOT NULL,
  `EmployeeId` char(36) NOT NULL,
  `SequenceNo` int NOT NULL,
  `Organization` varchar(150) NOT NULL,
  `Position` varchar(20) DEFAULT NULL,
  `Location` varchar(150) NOT NULL,
  `DateFrom` datetime(3) NOT NULL,
  `DateTo` datetime(3) DEFAULT NULL,
  `IsCurrent` bit(1) NOT NULL,
  `Summary` varchar(250) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`ExperienceId`),
  KEY `FK_EmployeeExperiences_Employees_EmployeeId` (`EmployeeId`),
  CONSTRAINT `FK_EmployeeExperiences_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `employees` (`AccountId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employeeexperiences`
--

LOCK TABLES `employeeexperiences` WRITE;
/*!40000 ALTER TABLE `employeeexperiences` DISABLE KEYS */;
/*!40000 ALTER TABLE `employeeexperiences` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employees`
--

DROP TABLE IF EXISTS `employees`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employees` (
  `AccountId` varchar(36) NOT NULL,
  `EmployeeCode` varchar(15) DEFAULT NULL,
  `FirstName` varchar(25) NOT NULL,
  `MiddleName` varchar(25) DEFAULT NULL,
  `LastName` varchar(25) NOT NULL,
  `Gender` varchar(15) NOT NULL,
  `DateOfBirth` datetime(3) DEFAULT NULL,
  `Department` varchar(100) DEFAULT NULL,
  `Telephone` varchar(15) DEFAULT NULL,
  `Mobile` varchar(15) NOT NULL,
  `EmailId` varchar(75) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`AccountId`),
  CONSTRAINT `FK_dbo.Employees_dbo.Accounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `accounts` (`AccountId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employees`
--

LOCK TABLES `employees` WRITE;
/*!40000 ALTER TABLE `employees` DISABLE KEYS */;
/*!40000 ALTER TABLE `employees` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employers`
--

DROP TABLE IF EXISTS `employers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employers` (
  `EmployerId` varchar(36) NOT NULL,
  `StateCode` varchar(36) NOT NULL,
  `DivisionId` varchar(36) NOT NULL,
  `DistrictCode` varchar(15) NOT NULL,
  `BlockName` varchar(150) NOT NULL,
  `Address` varchar(350) DEFAULT NULL,
  `City` varchar(50) DEFAULT NULL,
  `Pincode` varchar(6) DEFAULT NULL,
  `BusinessType` varchar(50) DEFAULT NULL,
  `EmployeeCount` int DEFAULT '0',
  `Outlets` varchar(350) DEFAULT NULL,
  `Contact1` varchar(150) DEFAULT NULL,
  `Mobile1` varchar(15) DEFAULT NULL,
  `Designation1` varchar(100) DEFAULT NULL,
  `EmailId1` varchar(150) DEFAULT NULL,
  `Contact2` varchar(150) DEFAULT NULL,
  `Mobile2` varchar(15) DEFAULT NULL,
  `Designation2` varchar(100) DEFAULT NULL,
  `EmailId2` varchar(150) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`EmployerId`),
  KEY `FK_Employers_Divisions_idx` (`DivisionId`),
  KEY `FK_Employers_Districts_idx` (`DistrictCode`),
  CONSTRAINT `FK_Employers_Divisions` FOREIGN KEY (`DivisionId`) REFERENCES `divisions` (`DivisionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employers`
--

LOCK TABLES `employers` WRITE;
/*!40000 ALTER TABLE `employers` DISABLE KEYS */;
/*!40000 ALTER TABLE `employers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `errorlogs`
--

DROP TABLE IF EXISTS `errorlogs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `errorlogs` (
  `ErrorLogId` varchar(36) NOT NULL,
  `ModuleName` varchar(100) DEFAULT NULL,
  `ErrorCode` varchar(30) DEFAULT NULL,
  `ErrorSeverity` int DEFAULT NULL,
  `ErrorState` int DEFAULT NULL,
  `ErrorProcedure` varchar(70) DEFAULT NULL,
  `ErrorLine` int DEFAULT NULL,
  `ErrorTime` datetime(3) NOT NULL,
  `ErrorType` varchar(150) DEFAULT NULL,
  `ErrorLocation` varchar(250) DEFAULT NULL,
  `ErrorMessage` varchar(500) NOT NULL,
  `StackTrace` varchar(3500) DEFAULT NULL,
  `ErrorStatus` varchar(50) DEFAULT NULL,
  `IsResolved` bit(1) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`ErrorLogId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `errorlogs`
--

LOCK TABLES `errorlogs` WRITE;
/*!40000 ALTER TABLE `errorlogs` DISABLE KEYS */;
/*!40000 ALTER TABLE `errorlogs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `exitsurveydetails`
--

DROP TABLE IF EXISTS `exitsurveydetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `exitsurveydetails` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ExitStudentId` varchar(36) NOT NULL,
  `AcademicYearId` varchar(36) DEFAULT NULL,
  `SeatNo` varchar(45) DEFAULT NULL,
  `Religion` varchar(45) DEFAULT NULL,
  `ParentName` varchar(45) DEFAULT NULL,
  `StudentMobileNo` varchar(15) DEFAULT NULL,
  `StudentWANo` varchar(15) DEFAULT NULL,
  `ParentMobileNo` varchar(15) DEFAULT NULL,
  `CityOfResidence` varchar(45) DEFAULT NULL,
  `DistrictOfResidence` varchar(45) DEFAULT NULL,
  `BlockOfResidence` varchar(45) DEFAULT NULL,
  `PinCode` varchar(45) DEFAULT NULL,
  `StudentAddress` varchar(250) DEFAULT NULL,
  `WillContHigherStudies` varchar(10) DEFAULT NULL,
  `IsFullTime` varchar(10) DEFAULT NULL,
  `CourseToPursue` varchar(100) DEFAULT NULL,
  `StreamOfEducation` varchar(100) DEFAULT NULL,
  `WillContVocEdu` varchar(10) DEFAULT NULL,
  `WillContVocational11` varchar(45) DEFAULT NULL,
  `ReasonsNOTToContinue` varchar(200) DEFAULT NULL,
  `WillContSameSector` varchar(10) DEFAULT NULL,
  `SectorForTraining` varchar(45) DEFAULT NULL,
  `OtherSector` varchar(100) DEFAULT NULL,
  `CurrentlyEmployed` varchar(10) DEFAULT NULL,
  `WorkTitle` varchar(150) DEFAULT NULL,
  `DetailsOfEmployment` varchar(100) DEFAULT NULL,
  `WillBeFullTime` varchar(10) DEFAULT NULL,
  `SectorsOfEmployment` varchar(100) DEFAULT NULL,
  `IsVSCompleted` varchar(45) DEFAULT NULL,
  `WantToPursueAnySkillTraining` varchar(15) DEFAULT NULL,
  `IsFulltimeWillingness` varchar(50) DEFAULT NULL,
  `HveRegisteredOnEmploymentPortal` varchar(40) DEFAULT NULL,
  `EmploymentPortalName` varchar(100) DEFAULT NULL,
  `WillingToGetRegisteredOnNAPS` varchar(10) DEFAULT NULL,
  `WantToKnowAboutOpportunities` varchar(10) DEFAULT NULL,
  `CanLahiGetInTouch` varchar(20) DEFAULT NULL,
  `CollectedEmailId` varchar(45) DEFAULT NULL,
  `SurveyCompletedByStudentORParent` varchar(45) DEFAULT NULL,
  `DateOfIntv` date DEFAULT NULL,
  `Remark` varchar(500) DEFAULT NULL,
  `DoneInternship` varchar(10) DEFAULT NULL,
  `InternshipCompletedSector` varchar(36) DEFAULT NULL,
  `IntrestedInJobOrSelfEmploymentPost12th` varchar(36) DEFAULT NULL,
  `PreferredLocations` varchar(100) DEFAULT NULL,
  `ParticularLocation` varchar(100) DEFAULT NULL,
  `DifferentProgramOpportunities` varchar(50) DEFAULT NULL,
  `OtherStreamStudying` varchar(36) DEFAULT NULL,
  `TrainingType` varchar(45) DEFAULT NULL,
  `OtherCourse` varchar(45) DEFAULT NULL,
  `WillingToContSkillTraining` varchar(10) DEFAULT NULL,
  `SkillTrainingType` varchar(15) DEFAULT NULL,
  `CourseForTraining` varchar(500) DEFAULT NULL,
  `CourseNameIfOther` varchar(45) DEFAULT NULL,
  `OtherSectorsIfAny` varchar(100) DEFAULT NULL,
  `InterestedInJobOrSelfEmployment` varchar(10) DEFAULT NULL,
  `TopicsOfInterest` varchar(100) DEFAULT NULL,
  `IsRelevantToVocCourse` varchar(10) DEFAULT NULL,
  `SectorForSkillTraining` varchar(45) DEFAULT NULL,
  `OthersIfAny` varchar(45) DEFAULT NULL,
  `WillingToGoForTechHighEdu` varchar(15) DEFAULT NULL,
  `WantToKnowAbtPgmsForJobsNContEdu` varchar(15) DEFAULT NULL,
  `CanSendTheUpdates` varchar(10) DEFAULT NULL,
  `IsOtherCourse` varchar(10) DEFAULT NULL,
  `OtherReasons` varchar(200) DEFAULT NULL,
  `DoesFieldStudyHveVocSub` varchar(10) DEFAULT NULL,
  `InterestedInSkillDevelopmentPgms` varchar(10) DEFAULT NULL,
  `SectorsInterestedIn` varchar(100) DEFAULT NULL,
  `AnyPreferredLocForEmployment` varchar(10) DEFAULT NULL,
  `WantToKnowAbtSkillsUnivByGvt` varchar(15) DEFAULT NULL,
  `VTMobile` varchar(15) DEFAULT NULL,
  `MotherName` varchar(45) DEFAULT NULL,
  `CreatedBy` varchar(45) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(45) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ExitSurveyDetails_ExitStudentId` (`ExitStudentId`)
) ENGINE=InnoDB AUTO_INCREMENT=26475 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `exitsurveydetails`
--

LOCK TABLES `exitsurveydetails` WRITE;
/*!40000 ALTER TABLE `exitsurveydetails` DISABLE KEYS */;
/*!40000 ALTER TABLE `exitsurveydetails` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `forgotpasswordhistories`
--

DROP TABLE IF EXISTS `forgotpasswordhistories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `forgotpasswordhistories` (
  `ForgotPasswordId` varchar(36) NOT NULL,
  `EmailId` varchar(100) DEFAULT NULL,
  `PasswordResetUrl` varchar(500) DEFAULT NULL,
  `UserIPAddress` varchar(30) DEFAULT NULL,
  `RequestDate` datetime(3) DEFAULT NULL,
  `ResetPasswordDate` datetime(3) DEFAULT NULL,
  PRIMARY KEY (`ForgotPasswordId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `forgotpasswordhistories`
--

LOCK TABLES `forgotpasswordhistories` WRITE;
/*!40000 ALTER TABLE `forgotpasswordhistories` DISABLE KEYS */;
/*!40000 ALTER TABLE `forgotpasswordhistories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fvclasssectionstaught`
--

DROP TABLE IF EXISTS `fvclasssectionstaught`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `fvclasssectionstaught` (
  `FVClassSectionsTaughtId` varchar(36) NOT NULL,
  `ClassId` varchar(36) NOT NULL,
  `SectionId` varchar(36) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`FVClassSectionsTaughtId`),
  KEY `FK_FVClassSections_Sections_idx` (`SectionId`),
  KEY `FK_FVClassSections_SchoolClasses_idx` (`ClassId`),
  CONSTRAINT `FK_FVClassSections_SchoolClasses` FOREIGN KEY (`ClassId`) REFERENCES `schoolclasses` (`ClassId`),
  CONSTRAINT `FK_FVClassSections_Sections` FOREIGN KEY (`SectionId`) REFERENCES `sections` (`SectionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fvclasssectionstaught`
--

LOCK TABLES `fvclasssectionstaught` WRITE;
/*!40000 ALTER TABLE `fvclasssectionstaught` DISABLE KEYS */;
/*!40000 ALTER TABLE `fvclasssectionstaught` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fvstudentattendances`
--

DROP TABLE IF EXISTS `fvstudentattendances`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `fvstudentattendances` (
  `FVStudentAttendanceId` varchar(36) NOT NULL,
  `VTFieldIndustryVisitConductedId` varchar(36) NOT NULL,
  `VTId` varchar(36) NOT NULL,
  `ClassId` varchar(36) NOT NULL,
  `StudentId` varchar(36) NOT NULL,
  `IsPresent` bit(1) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`FVStudentAttendanceId`),
  KEY `FK_FVStudentAttendances_VTFieldIndustryVisitConducted_idx` (`VTFieldIndustryVisitConductedId`),
  KEY `FK_FVStudentAttendances_VocationalTrainers_idx` (`VTId`),
  KEY `FK_FVStudentAttendances_SchoolClasses_idx` (`ClassId`),
  KEY `FK_FVStudentAttendances_StudentClasses_idx` (`StudentId`),
  CONSTRAINT `FK_FVStudentAttendances_SchoolClasses` FOREIGN KEY (`ClassId`) REFERENCES `schoolclasses` (`ClassId`),
  CONSTRAINT `FK_FVStudentAttendances_StudentClasses` FOREIGN KEY (`StudentId`) REFERENCES `studentclasses` (`StudentId`),
  CONSTRAINT `FK_FVStudentAttendances_VocationalTrainers` FOREIGN KEY (`VTId`) REFERENCES `vocationaltrainers` (`VTId`),
  CONSTRAINT `FK_FVStudentAttendances_VTFieldIndustryVisitConducted` FOREIGN KEY (`VTFieldIndustryVisitConductedId`) REFERENCES `vtfieldindustryvisitconducted` (`VTFieldIndustryVisitConductedId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fvstudentattendances`
--

LOCK TABLES `fvstudentattendances` WRITE;
/*!40000 ALTER TABLE `fvstudentattendances` DISABLE KEYS */;
/*!40000 ALTER TABLE `fvstudentattendances` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fvunitsessionstaught`
--

DROP TABLE IF EXISTS `fvunitsessionstaught`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `fvunitsessionstaught` (
  `FVUnitSessionsTaughtId` varchar(36) NOT NULL,
  `FVUnitsTaughtId` varchar(36) NOT NULL,
  `SessionId` varchar(36) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`FVUnitSessionsTaughtId`),
  KEY `FK_FVUnitSessionTaughts_FVUnitsTaught_idx` (`FVUnitsTaughtId`),
  KEY `FK_FVUnitSessionTaughts_CourseUnitSessions_idx` (`SessionId`),
  CONSTRAINT `FK_FVUnitSessionTaughts_CourseUnitSessions` FOREIGN KEY (`SessionId`) REFERENCES `courseunitsessions` (`CourseUnitSessionId`),
  CONSTRAINT `FK_FVUnitSessionTaughts_FVUnitsTaught` FOREIGN KEY (`FVUnitsTaughtId`) REFERENCES `fvunitstaught` (`FVUnitsTaughtId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fvunitsessionstaught`
--

LOCK TABLES `fvunitsessionstaught` WRITE;
/*!40000 ALTER TABLE `fvunitsessionstaught` DISABLE KEYS */;
/*!40000 ALTER TABLE `fvunitsessionstaught` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fvunitstaught`
--

DROP TABLE IF EXISTS `fvunitstaught`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `fvunitstaught` (
  `FVUnitsTaughtId` varchar(36) NOT NULL,
  `VTFieldIndustryVisitConductedId` varchar(36) NOT NULL,
  `UnitId` varchar(36) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`FVUnitsTaughtId`),
  KEY `FK_FVUnitsTaughts_VTFieldIndustryVisitConducted_idx` (`VTFieldIndustryVisitConductedId`),
  KEY `FK_FVUnitsTaughts_CourseModules_idx` (`UnitId`),
  CONSTRAINT `FK_FVUnitsTaughts_CourseModules` FOREIGN KEY (`UnitId`) REFERENCES `coursemodules` (`CourseModuleId`),
  CONSTRAINT `FK_FVUnitsTaughts_VTFieldIndustryVisitConducted` FOREIGN KEY (`VTFieldIndustryVisitConductedId`) REFERENCES `vtfieldindustryvisitconducted` (`VTFieldIndustryVisitConductedId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fvunitstaught`
--

LOCK TABLES `fvunitstaught` WRITE;
/*!40000 ALTER TABLE `fvunitstaught` DISABLE KEYS */;
/*!40000 ALTER TABLE `fvunitstaught` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `glclasssectionstaught`
--

DROP TABLE IF EXISTS `glclasssectionstaught`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `glclasssectionstaught` (
  `GLClassSectionsTaughtId` varchar(36) NOT NULL,
  `ClassId` varchar(36) NOT NULL,
  `SectionId` varchar(36) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`GLClassSectionsTaughtId`),
  KEY `FK_GLClassSections_Sections_idx` (`SectionId`),
  KEY `FK_GLClassSections_SchoolClasses_idx` (`ClassId`),
  CONSTRAINT `FK_GLClassSections_SchoolClasses` FOREIGN KEY (`ClassId`) REFERENCES `schoolclasses` (`ClassId`),
  CONSTRAINT `FK_GLClassSections_Sections` FOREIGN KEY (`SectionId`) REFERENCES `sections` (`SectionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `glclasssectionstaught`
--

LOCK TABLES `glclasssectionstaught` WRITE;
/*!40000 ALTER TABLE `glclasssectionstaught` DISABLE KEYS */;
/*!40000 ALTER TABLE `glclasssectionstaught` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `glmethodologies`
--

DROP TABLE IF EXISTS `glmethodologies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `glmethodologies` (
  `GLMethodologyId` varchar(36) NOT NULL,
  `VTGuestLectureId` varchar(36) NOT NULL,
  `MethodologyTypeId` varchar(5) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`GLMethodologyId`),
  KEY `FK_GLMethodologies_VTGuestLectureConducted_idx` (`VTGuestLectureId`),
  KEY `FK_GLMethodologies_MethodologyTypes_idx` (`MethodologyTypeId`),
  CONSTRAINT `FK_GLMethodologies_MethodologyTypes` FOREIGN KEY (`MethodologyTypeId`) REFERENCES `datavalues` (`DataValueId`),
  CONSTRAINT `FK_GLMethodologies_VTGuestLectureConducted` FOREIGN KEY (`VTGuestLectureId`) REFERENCES `vtguestlectureconducted` (`VTGuestLectureId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `glmethodologies`
--

LOCK TABLES `glmethodologies` WRITE;
/*!40000 ALTER TABLE `glmethodologies` DISABLE KEYS */;
/*!40000 ALTER TABLE `glmethodologies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `glstudentattendances`
--

DROP TABLE IF EXISTS `glstudentattendances`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `glstudentattendances` (
  `GLStudentAttendanceId` varchar(36) NOT NULL,
  `VTGuestLectureId` varchar(36) NOT NULL,
  `VTId` varchar(36) NOT NULL,
  `ClassId` varchar(36) NOT NULL,
  `StudentId` varchar(36) NOT NULL,
  `IsPresent` bit(1) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`GLStudentAttendanceId`),
  KEY `FK_GLStudentAttendances_VTGuestLectureConducted_idx` (`VTGuestLectureId`),
  KEY `FK_GLStudentAttendances_VocationalTrainers_idx` (`VTId`),
  KEY `FK_GLStudentAttendances_SchoolClasses_idx` (`ClassId`),
  KEY `FK_GLStudentAttendances_StudentClasses_idx` (`StudentId`),
  CONSTRAINT `FK_GLStudentAttendances_SchoolClasses` FOREIGN KEY (`ClassId`) REFERENCES `schoolclasses` (`ClassId`),
  CONSTRAINT `FK_GLStudentAttendances_StudentClasses` FOREIGN KEY (`StudentId`) REFERENCES `studentclasses` (`StudentId`),
  CONSTRAINT `FK_GLStudentAttendances_VocationalTrainers` FOREIGN KEY (`VTId`) REFERENCES `vocationaltrainers` (`VTId`),
  CONSTRAINT `FK_GLStudentAttendances_VTGuestLectureConducted` FOREIGN KEY (`VTGuestLectureId`) REFERENCES `vtguestlectureconducted` (`VTGuestLectureId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `glstudentattendances`
--

LOCK TABLES `glstudentattendances` WRITE;
/*!40000 ALTER TABLE `glstudentattendances` DISABLE KEYS */;
/*!40000 ALTER TABLE `glstudentattendances` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `glunitsessionstaught`
--

DROP TABLE IF EXISTS `glunitsessionstaught`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `glunitsessionstaught` (
  `GLUnitSessionsTaughtId` varchar(36) NOT NULL,
  `GLUnitsTaughtId` varchar(36) NOT NULL,
  `SessionId` varchar(36) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`GLUnitSessionsTaughtId`),
  KEY `FK_GLUnitSessionTaughts_GLUnitsTaught_idx` (`GLUnitsTaughtId`),
  KEY `FK_GLUnitSessionTaughts_CourseUnitSessions_idx` (`SessionId`),
  CONSTRAINT `FK_GLUnitSessionTaughts_CourseUnitSessions` FOREIGN KEY (`SessionId`) REFERENCES `courseunitsessions` (`CourseUnitSessionId`),
  CONSTRAINT `FK_GLUnitSessionTaughts_GLUnitsTaught` FOREIGN KEY (`GLUnitsTaughtId`) REFERENCES `glunitstaught` (`GLUnitsTaughtId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `glunitsessionstaught`
--

LOCK TABLES `glunitsessionstaught` WRITE;
/*!40000 ALTER TABLE `glunitsessionstaught` DISABLE KEYS */;
/*!40000 ALTER TABLE `glunitsessionstaught` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `glunitstaught`
--

DROP TABLE IF EXISTS `glunitstaught`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `glunitstaught` (
  `GLUnitsTaughtId` varchar(36) NOT NULL,
  `VTGuestLectureId` varchar(36) NOT NULL,
  `UnitId` varchar(36) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`GLUnitsTaughtId`),
  KEY `FK_GLUnitsTaughts_VTGuestLectureConducted_idx` (`VTGuestLectureId`),
  KEY `FK_GLUnitsTaughts_CourseModules_idx` (`UnitId`),
  CONSTRAINT `FK_GLUnitsTaughts_CourseModules` FOREIGN KEY (`UnitId`) REFERENCES `coursemodules` (`CourseModuleId`),
  CONSTRAINT `FK_GLUnitsTaughts_VTGuestLectureConducted` FOREIGN KEY (`VTGuestLectureId`) REFERENCES `vtguestlectureconducted` (`VTGuestLectureId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `glunitstaught`
--

LOCK TABLES `glunitstaught` WRITE;
/*!40000 ALTER TABLE `glunitstaught` DISABLE KEYS */;
/*!40000 ALTER TABLE `glunitstaught` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `headmasters`
--

DROP TABLE IF EXISTS `headmasters`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `headmasters` (
  `HMId` varchar(36) NOT NULL,
  `VTId` varchar(36) DEFAULT NULL,
  `SchoolId` varchar(36) DEFAULT NULL,
  `FirstName` varchar(50) NOT NULL,
  `MiddleName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) NOT NULL,
  `FullName` varchar(150) NOT NULL,
  `Mobile` varchar(15) NOT NULL,
  `Mobile1` varchar(15) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `Gender` varchar(10) NOT NULL,
  `YearsInSchool` int DEFAULT NULL,
  `DateOfJoiningSchool` datetime(3) DEFAULT NULL,
  `DateOfResignationFromSchool` datetime(3) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`HMId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `headmasters`
--

LOCK TABLES `headmasters` WRITE;
/*!40000 ALTER TABLE `headmasters` DISABLE KEYS */;
/*!40000 ALTER TABLE `headmasters` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `hmissuereporting`
--

DROP TABLE IF EXISTS `hmissuereporting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hmissuereporting` (
  `HMIssueReportingId` varchar(36) NOT NULL,
  `AcademicYearId` varchar(36) DEFAULT NULL,
  `HMId` varchar(36) DEFAULT NULL,
  `IssueMappingId` varchar(36) DEFAULT NULL,
  `IssueReportDate` datetime(3) NOT NULL,
  `MainIssue` varchar(50) DEFAULT NULL,
  `SubIssue` varchar(50) DEFAULT NULL,
  `StudentClass` varchar(100) NOT NULL,
  `Month` varchar(100) NOT NULL,
  `StudentType` varchar(50) DEFAULT NULL,
  `NoOfStudents` int NOT NULL,
  `IssueDetails` varchar(350) DEFAULT NULL,
  `GeoLocation` varchar(50) DEFAULT NULL,
  `Latitude` varchar(20) DEFAULT NULL,
  `Longitude` varchar(20) DEFAULT NULL,
  `ApprovalStatus` varchar(50) DEFAULT NULL,
  `ApprovedDate` datetime DEFAULT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`HMIssueReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hmissuereporting`
--

LOCK TABLES `hmissuereporting` WRITE;
/*!40000 ALTER TABLE `hmissuereporting` DISABLE KEYS */;
/*!40000 ALTER TABLE `hmissuereporting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `hmschoolsmap`
--

DROP TABLE IF EXISTS `hmschoolsmap`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hmschoolsmap` (
  `HMSchoolId` varchar(36) NOT NULL,
  `AcademicYearId` varchar(36) NOT NULL,
  `SchoolId` varchar(36) NOT NULL,
  `HMId` varchar(36) NOT NULL,
  `DateOfJoining` datetime DEFAULT NULL,
  `DateOfResignation` datetime DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) DEFAULT NULL,
  PRIMARY KEY (`HMSchoolId`),
  UNIQUE KEY `UC_HMSchoolsMap_AY_SCH_HM` (`AcademicYearId`,`SchoolId`,`HMId`,`IsActive`),
  KEY `FK_HMSchoolsMap_AY` (`AcademicYearId`),
  KEY `FK_HMSchoolsMap_Schools` (`SchoolId`),
  KEY `FK_HMSchoolsMap_HM` (`HMId`),
  KEY `IX_HMSchoolsMap_AY_SCH_HM` (`AcademicYearId`,`SchoolId`,`HMId`,`IsActive`),
  CONSTRAINT `FK_HMSchoolsMap_AY` FOREIGN KEY (`AcademicYearId`) REFERENCES `academicyears` (`AcademicYearId`),
  CONSTRAINT `FK_HMSchoolsMap_HM` FOREIGN KEY (`HMId`) REFERENCES `headmasters` (`HMId`),
  CONSTRAINT `FK_HMSchoolsMap_Schools` FOREIGN KEY (`SchoolId`) REFERENCES `schools` (`SchoolId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hmschoolsmap`
--

LOCK TABLES `hmschoolsmap` WRITE;
/*!40000 ALTER TABLE `hmschoolsmap` DISABLE KEYS */;
/*!40000 ALTER TABLE `hmschoolsmap` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `issueapprovalhistories`
--

DROP TABLE IF EXISTS `issueapprovalhistories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `issueapprovalhistories` (
  `IssueApprovalHistoryId` varchar(36) NOT NULL,
  `IssueId` varchar(36) DEFAULT NULL,
  `IssueType` varchar(15) DEFAULT NULL,
  `ApprovedBy` varchar(36) DEFAULT NULL,
  `ApprovedDate` datetime DEFAULT NULL,
  `ApprovalStatus` varchar(10) DEFAULT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  PRIMARY KEY (`IssueApprovalHistoryId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `issueapprovalhistories`
--

LOCK TABLES `issueapprovalhistories` WRITE;
/*!40000 ALTER TABLE `issueapprovalhistories` DISABLE KEYS */;
/*!40000 ALTER TABLE `issueapprovalhistories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `issueassignforreviews`
--

DROP TABLE IF EXISTS `issueassignforreviews`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `issueassignforreviews` (
  `IssueAssignForReviewId` varchar(36) NOT NULL,
  `IssueMappingId` varchar(36) NOT NULL,
  `ReviewId` varchar(10) NOT NULL,
  `CreatedBy` varchar(30) DEFAULT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`IssueAssignForReviewId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `issueassignforreviews`
--

LOCK TABLES `issueassignforreviews` WRITE;
/*!40000 ALTER TABLE `issueassignforreviews` DISABLE KEYS */;
/*!40000 ALTER TABLE `issueassignforreviews` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `issueforwardedto`
--

DROP TABLE IF EXISTS `issueforwardedto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `issueforwardedto` (
  `IssueForwardedToId` varchar(36) NOT NULL,
  `IssueType` varchar(15) NOT NULL,
  `IssueReportingId` varchar(36) NOT NULL,
  `ReviewId` varchar(10) NOT NULL,
  `CreatedBy` varchar(30) DEFAULT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`IssueForwardedToId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `issueforwardedto`
--

LOCK TABLES `issueforwardedto` WRITE;
/*!40000 ALTER TABLE `issueforwardedto` DISABLE KEYS */;
/*!40000 ALTER TABLE `issueforwardedto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `issuemapping`
--

DROP TABLE IF EXISTS `issuemapping`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `issuemapping` (
  `IssueMappingId` varchar(36) NOT NULL,
  `MainIssueId` varchar(10) NOT NULL,
  `SubIssueId` varchar(10) NOT NULL,
  `IssueCategoryId` varchar(10) NOT NULL,
  `IssuePriority` varchar(30) NOT NULL,
  `IsApplicableForVC` bit(1) NOT NULL,
  `IsApplicableForVT` bit(1) NOT NULL,
  `IsApplicableForHM` bit(1) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`IssueMappingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `issuemapping`
--

LOCK TABLES `issuemapping` WRITE;
/*!40000 ALTER TABLE `issuemapping` DISABLE KEYS */;
/*!40000 ALTER TABLE `issuemapping` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `jobroles`
--

DROP TABLE IF EXISTS `jobroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `jobroles` (
  `JobRoleId` varchar(36) NOT NULL,
  `SectorId` char(36) NOT NULL,
  `JobRoleName` varchar(100) NOT NULL,
  `QPCode` varchar(15) NOT NULL,
  `DisplayOrder` int NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`JobRoleId`),
  KEY `FK_CourseModules_JobRoles_idx` (`JobRoleId`),
  KEY `FK_CourseModules_Sectors_idx` (`SectorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `jobroles`
--

LOCK TABLES `jobroles` WRITE;
/*!40000 ALTER TABLE `jobroles` DISABLE KEYS */;
/*!40000 ALTER TABLE `jobroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lighthouseparams`
--

DROP TABLE IF EXISTS `lighthouseparams`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `lighthouseparams` (
  `LighthouseParamId` int NOT NULL AUTO_INCREMENT,
  `Param1` text,
  `Param2` text,
  `Param3` text,
  `Param4` text,
  `Param5` text,
  `CreatedBy` varchar(45) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`LighthouseParamId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lighthouseparams`
--

LOCK TABLES `lighthouseparams` WRITE;
/*!40000 ALTER TABLE `lighthouseparams` DISABLE KEYS */;
/*!40000 ALTER TABLE `lighthouseparams` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `logouthistories`
--

DROP TABLE IF EXISTS `logouthistories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `logouthistories` (
  `LoginUniqueId` varchar(36) NOT NULL,
  `AccountId` varchar(36) DEFAULT NULL,
  `UserId` varchar(45) DEFAULT NULL,
  `LoginDateTime` datetime DEFAULT NULL,
  `LogoutDateTime` varchar(45) DEFAULT NULL,
  `AuthToken` varchar(500) DEFAULT NULL,
  `IsMobile` bit(1) DEFAULT NULL,
  PRIMARY KEY (`LoginUniqueId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `logouthistories`
--

LOCK TABLES `logouthistories` WRITE;
/*!40000 ALTER TABLE `logouthistories` DISABLE KEYS */;
/*!40000 ALTER TABLE `logouthistories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mainissues`
--

DROP TABLE IF EXISTS `mainissues`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `mainissues` (
  `MainIssueId` varchar(36) NOT NULL,
  `Code` varchar(15) DEFAULT NULL,
  `Name` varchar(150) DEFAULT NULL,
  `Description` varchar(350) DEFAULT NULL,
  `DisplayOrder` int DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) DEFAULT NULL,
  PRIMARY KEY (`MainIssueId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mainissues`
--

LOCK TABLES `mainissues` WRITE;
/*!40000 ALTER TABLE `mainissues` DISABLE KEYS */;
/*!40000 ALTER TABLE `mainissues` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `messagetemplates`
--

DROP TABLE IF EXISTS `messagetemplates`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `messagetemplates` (
  `MessageTemplateId` int NOT NULL AUTO_INCREMENT,
  `TemplateName` varchar(150) NOT NULL,
  `TemplateFlowId` varchar(200) NOT NULL,
  `MessageTypeId` varchar(15) NOT NULL,
  `MessageSubTypeId` varchar(15) NOT NULL,
  `MessageFields` varchar(250) NOT NULL,
  `SMSMessage` varchar(750) DEFAULT NULL,
  `WhatsappMessage` text,
  `EmailMessage` text,
  `ApplicableFor` varchar(150) NOT NULL,
  `CreatedBy` varchar(35) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(35) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`MessageTemplateId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `messagetemplates`
--

LOCK TABLES `messagetemplates` WRITE;
/*!40000 ALTER TABLE `messagetemplates` DISABLE KEYS */;
/*!40000 ALTER TABLE `messagetemplates` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `phases`
--

DROP TABLE IF EXISTS `phases`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `phases` (
  `PhaseId` varchar(36) NOT NULL,
  `PhaseName` varchar(100) NOT NULL,
  `Description` varchar(250) DEFAULT NULL,
  `DisplayOrder` int NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`PhaseId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `phases`
--

LOCK TABLES `phases` WRITE;
/*!40000 ALTER TABLE `phases` DISABLE KEYS */;
INSERT INTO `phases` VALUES ('d4917323-570a-4018-92a5-0a96806d5045','Phase 1',NULL,1,'Somesh','2023-05-04 21:50:41.735',NULL,'2023-05-04 21:50:41.735',_binary '');
/*!40000 ALTER TABLE `phases` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `qrtz_blob_triggers`
--

DROP TABLE IF EXISTS `qrtz_blob_triggers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `qrtz_blob_triggers` (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `TRIGGER_NAME` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `TRIGGER_GROUP` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `BLOB_DATA` longblob,
  PRIMARY KEY (`SCHED_NAME`,`TRIGGER_NAME`,`TRIGGER_GROUP`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `qrtz_blob_triggers`
--

LOCK TABLES `qrtz_blob_triggers` WRITE;
/*!40000 ALTER TABLE `qrtz_blob_triggers` DISABLE KEYS */;
/*!40000 ALTER TABLE `qrtz_blob_triggers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `qrtz_calendars`
--

DROP TABLE IF EXISTS `qrtz_calendars`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `qrtz_calendars` (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `CALENDAR_NAME` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `CALENDAR` longblob NOT NULL,
  PRIMARY KEY (`SCHED_NAME`,`CALENDAR_NAME`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `qrtz_calendars`
--

LOCK TABLES `qrtz_calendars` WRITE;
/*!40000 ALTER TABLE `qrtz_calendars` DISABLE KEYS */;
/*!40000 ALTER TABLE `qrtz_calendars` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `qrtz_cron_triggers`
--

DROP TABLE IF EXISTS `qrtz_cron_triggers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `qrtz_cron_triggers` (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `TRIGGER_NAME` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `TRIGGER_GROUP` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `CRON_EXPRESSION` varchar(120) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `TIME_ZONE_ID` varchar(80) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  PRIMARY KEY (`SCHED_NAME`,`TRIGGER_NAME`,`TRIGGER_GROUP`),
  CONSTRAINT `FK_QRTZ_CRON_TRIGGERS_QRTZ_TRIGGERS` FOREIGN KEY (`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) REFERENCES `qrtz_triggers` (`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `qrtz_cron_triggers`
--

LOCK TABLES `qrtz_cron_triggers` WRITE;
/*!40000 ALTER TABLE `qrtz_cron_triggers` DISABLE KEYS */;
/*!40000 ALTER TABLE `qrtz_cron_triggers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `qrtz_fired_triggers`
--

DROP TABLE IF EXISTS `qrtz_fired_triggers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `qrtz_fired_triggers` (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `ENTRY_ID` varchar(140) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `TRIGGER_NAME` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `TRIGGER_GROUP` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `INSTANCE_NAME` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `FIRED_TIME` bigint NOT NULL,
  `SCHED_TIME` bigint NOT NULL,
  `PRIORITY` int NOT NULL,
  `STATE` varchar(16) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `JOB_NAME` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `JOB_GROUP` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `IS_NONCONCURRENT` bit(1) DEFAULT NULL,
  `REQUESTS_RECOVERY` bit(1) DEFAULT NULL,
  PRIMARY KEY (`SCHED_NAME`,`ENTRY_ID`),
  KEY `IDX_QRTZ_FT_INST_JOB_REQ_RCVRY` (`SCHED_NAME`,`INSTANCE_NAME`,`REQUESTS_RECOVERY`),
  KEY `IDX_QRTZ_FT_J_G` (`SCHED_NAME`,`JOB_NAME`,`JOB_GROUP`),
  KEY `IDX_QRTZ_FT_JG` (`SCHED_NAME`,`JOB_GROUP`),
  KEY `IDX_QRTZ_FT_T_G` (`SCHED_NAME`,`TRIGGER_NAME`,`TRIGGER_GROUP`),
  KEY `IDX_QRTZ_FT_TG` (`SCHED_NAME`,`TRIGGER_GROUP`),
  KEY `IDX_QRTZ_FT_TRIG_INST_NAME` (`SCHED_NAME`,`INSTANCE_NAME`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `qrtz_fired_triggers`
--

LOCK TABLES `qrtz_fired_triggers` WRITE;
/*!40000 ALTER TABLE `qrtz_fired_triggers` DISABLE KEYS */;
/*!40000 ALTER TABLE `qrtz_fired_triggers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `qrtz_job_details`
--

DROP TABLE IF EXISTS `qrtz_job_details`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `qrtz_job_details` (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `JOB_NAME` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `JOB_GROUP` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `DESCRIPTION` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `JOB_CLASS_NAME` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `IS_DURABLE` bit(1) NOT NULL,
  `IS_NONCONCURRENT` bit(1) NOT NULL,
  `IS_UPDATE_DATA` bit(1) NOT NULL,
  `REQUESTS_RECOVERY` bit(1) NOT NULL,
  `JOB_DATA` longblob,
  PRIMARY KEY (`SCHED_NAME`,`JOB_NAME`,`JOB_GROUP`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `qrtz_job_details`
--

LOCK TABLES `qrtz_job_details` WRITE;
/*!40000 ALTER TABLE `qrtz_job_details` DISABLE KEYS */;
/*!40000 ALTER TABLE `qrtz_job_details` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `qrtz_locks`
--

DROP TABLE IF EXISTS `qrtz_locks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `qrtz_locks` (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `LOCK_NAME` varchar(40) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`SCHED_NAME`,`LOCK_NAME`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `qrtz_locks`
--

LOCK TABLES `qrtz_locks` WRITE;
/*!40000 ALTER TABLE `qrtz_locks` DISABLE KEYS */;
/*!40000 ALTER TABLE `qrtz_locks` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `qrtz_paused_trigger_grps`
--

DROP TABLE IF EXISTS `qrtz_paused_trigger_grps`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `qrtz_paused_trigger_grps` (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `TRIGGER_GROUP` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`SCHED_NAME`,`TRIGGER_GROUP`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `qrtz_paused_trigger_grps`
--

LOCK TABLES `qrtz_paused_trigger_grps` WRITE;
/*!40000 ALTER TABLE `qrtz_paused_trigger_grps` DISABLE KEYS */;
/*!40000 ALTER TABLE `qrtz_paused_trigger_grps` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `qrtz_scheduler_state`
--

DROP TABLE IF EXISTS `qrtz_scheduler_state`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `qrtz_scheduler_state` (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `INSTANCE_NAME` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `LAST_CHECKIN_TIME` bigint NOT NULL,
  `CHECKIN_INTERVAL` bigint NOT NULL,
  PRIMARY KEY (`SCHED_NAME`,`INSTANCE_NAME`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `qrtz_scheduler_state`
--

LOCK TABLES `qrtz_scheduler_state` WRITE;
/*!40000 ALTER TABLE `qrtz_scheduler_state` DISABLE KEYS */;
/*!40000 ALTER TABLE `qrtz_scheduler_state` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `qrtz_simple_triggers`
--

DROP TABLE IF EXISTS `qrtz_simple_triggers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `qrtz_simple_triggers` (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `TRIGGER_NAME` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `TRIGGER_GROUP` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `REPEAT_COUNT` int NOT NULL,
  `REPEAT_INTERVAL` bigint NOT NULL,
  `TIMES_TRIGGERED` int NOT NULL,
  PRIMARY KEY (`SCHED_NAME`,`TRIGGER_NAME`,`TRIGGER_GROUP`),
  CONSTRAINT `FK_QRTZ_SIMPLE_TRIGGERS_QRTZ_TRIGGERS` FOREIGN KEY (`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) REFERENCES `qrtz_triggers` (`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `qrtz_simple_triggers`
--

LOCK TABLES `qrtz_simple_triggers` WRITE;
/*!40000 ALTER TABLE `qrtz_simple_triggers` DISABLE KEYS */;
/*!40000 ALTER TABLE `qrtz_simple_triggers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `qrtz_simprop_triggers`
--

DROP TABLE IF EXISTS `qrtz_simprop_triggers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `qrtz_simprop_triggers` (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `TRIGGER_NAME` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `TRIGGER_GROUP` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `STR_PROP_1` varchar(512) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `STR_PROP_2` varchar(512) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `STR_PROP_3` varchar(512) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `INT_PROP_1` int DEFAULT NULL,
  `INT_PROP_2` int DEFAULT NULL,
  `LONG_PROP_1` bigint DEFAULT NULL,
  `LONG_PROP_2` bigint DEFAULT NULL,
  `DEC_PROP_1` decimal(13,4) DEFAULT NULL,
  `DEC_PROP_2` decimal(13,4) DEFAULT NULL,
  `BOOL_PROP_1` bit(1) DEFAULT NULL,
  `BOOL_PROP_2` bit(1) DEFAULT NULL,
  `TIME_ZONE_ID` varchar(80) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  PRIMARY KEY (`SCHED_NAME`,`TRIGGER_NAME`,`TRIGGER_GROUP`),
  CONSTRAINT `FK_QRTZ_SIMPROP_TRIGGERS_QRTZ_TRIGGERS` FOREIGN KEY (`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) REFERENCES `qrtz_triggers` (`SCHED_NAME`, `TRIGGER_NAME`, `TRIGGER_GROUP`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `qrtz_simprop_triggers`
--

LOCK TABLES `qrtz_simprop_triggers` WRITE;
/*!40000 ALTER TABLE `qrtz_simprop_triggers` DISABLE KEYS */;
/*!40000 ALTER TABLE `qrtz_simprop_triggers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `qrtz_triggers`
--

DROP TABLE IF EXISTS `qrtz_triggers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `qrtz_triggers` (
  `SCHED_NAME` varchar(120) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `TRIGGER_NAME` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `TRIGGER_GROUP` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `JOB_NAME` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `JOB_GROUP` varchar(150) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `DESCRIPTION` varchar(250) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `NEXT_FIRE_TIME` bigint DEFAULT NULL,
  `PREV_FIRE_TIME` bigint DEFAULT NULL,
  `PRIORITY` int DEFAULT NULL,
  `TRIGGER_STATE` varchar(16) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `TRIGGER_TYPE` varchar(8) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `START_TIME` bigint NOT NULL,
  `END_TIME` bigint DEFAULT NULL,
  `CALENDAR_NAME` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `MISFIRE_INSTR` int DEFAULT NULL,
  `JOB_DATA` longblob,
  PRIMARY KEY (`SCHED_NAME`,`TRIGGER_NAME`,`TRIGGER_GROUP`),
  KEY `IDX_QRTZ_T_C` (`SCHED_NAME`,`CALENDAR_NAME`),
  KEY `IDX_QRTZ_T_G` (`SCHED_NAME`,`TRIGGER_GROUP`),
  KEY `IDX_QRTZ_T_J` (`SCHED_NAME`,`JOB_NAME`,`JOB_GROUP`),
  KEY `IDX_QRTZ_T_JG` (`SCHED_NAME`,`JOB_GROUP`),
  KEY `IDX_QRTZ_T_N_G_STATE` (`SCHED_NAME`,`TRIGGER_GROUP`,`TRIGGER_STATE`),
  KEY `IDX_QRTZ_T_N_STATE` (`SCHED_NAME`,`TRIGGER_NAME`,`TRIGGER_GROUP`,`TRIGGER_STATE`),
  KEY `IDX_QRTZ_T_NEXT_FIRE_TIME` (`SCHED_NAME`,`NEXT_FIRE_TIME`),
  KEY `IDX_QRTZ_T_NFT_MISFIRE` (`SCHED_NAME`,`MISFIRE_INSTR`,`NEXT_FIRE_TIME`),
  KEY `IDX_QRTZ_T_NFT_ST` (`SCHED_NAME`,`TRIGGER_STATE`,`NEXT_FIRE_TIME`),
  KEY `IDX_QRTZ_T_NFT_ST_MISFIRE` (`SCHED_NAME`,`MISFIRE_INSTR`,`NEXT_FIRE_TIME`,`TRIGGER_STATE`),
  KEY `IDX_QRTZ_T_NFT_ST_MISFIRE_GRP` (`SCHED_NAME`,`MISFIRE_INSTR`,`NEXT_FIRE_TIME`,`TRIGGER_GROUP`,`TRIGGER_STATE`),
  KEY `IDX_QRTZ_T_STATE` (`SCHED_NAME`,`TRIGGER_STATE`),
  CONSTRAINT `FK_QRTZ_TRIGGERS_QRTZ_JOB_DETAILS` FOREIGN KEY (`SCHED_NAME`, `JOB_NAME`, `JOB_GROUP`) REFERENCES `qrtz_job_details` (`SCHED_NAME`, `JOB_NAME`, `JOB_GROUP`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `qrtz_triggers`
--

LOCK TABLES `qrtz_triggers` WRITE;
/*!40000 ALTER TABLE `qrtz_triggers` DISABLE KEYS */;
/*!40000 ALTER TABLE `qrtz_triggers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roles` (
  `RoleId` varchar(36) NOT NULL,
  `Code` varchar(15) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(350) DEFAULT NULL,
  `LandingPageUrl` varchar(100) DEFAULT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`RoleId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roles`
--

LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES ('1b91f252-32c1-43e4-8fa5-6ce0c71f5d45','VC ','Vocational Coordinator','Vocational Coordinator',NULL,NULL,'kranthiND','2023-05-04 21:56:13.142',NULL,'2023-05-04 21:56:13.142',_binary ''),('bc035793-f06f-45ca-a93c-ce97493f78fa','VT','Vocational Trainer','Vocational Trainer',NULL,NULL,'kranthiND','2023-05-04 21:56:13.142',NULL,'2023-05-04 21:56:13.142',_binary ''),('d85259ec-9c98-4b1a-9e9f-9cb92414ee8e','PMU-Admin','PMU-Admin','PMU-Admin','','','','2023-05-04 21:56:13.142','','2023-05-04 21:56:13.142',_binary '');
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roletransactions`
--

DROP TABLE IF EXISTS `roletransactions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roletransactions` (
  `RoleTransactionId` varchar(36) NOT NULL,
  `RoleId` varchar(36) DEFAULT NULL,
  `TransactionId` varchar(36) DEFAULT NULL,
  `Rights` bit(1) NOT NULL,
  `CanAdd` bit(1) NOT NULL,
  `CanEdit` bit(1) NOT NULL,
  `CanDelete` bit(1) NOT NULL,
  `CanView` bit(1) NOT NULL,
  `CanExport` bit(1) NOT NULL,
  `ListView` bit(1) NOT NULL,
  `BasicView` bit(1) NOT NULL,
  `DetailView` bit(1) NOT NULL,
  `IsPublic` bit(1) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`RoleTransactionId`),
  KEY `FK_dbo.RoleTransactionMap_dbo.Roles_RoleId` (`RoleId`),
  KEY `FK_dbo.RoleTransactionMap_dbo.Transactions_TransactionId` (`TransactionId`),
  CONSTRAINT `FK_dbo.RoleTransactionMap_dbo.Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`RoleId`) ON DELETE CASCADE,
  CONSTRAINT `FK_dbo.RoleTransactionMap_dbo.Transactions_TransactionId` FOREIGN KEY (`TransactionId`) REFERENCES `transactions` (`TransactionId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roletransactions`
--

LOCK TABLES `roletransactions` WRITE;
/*!40000 ALTER TABLE `roletransactions` DISABLE KEYS */;
/*!40000 ALTER TABLE `roletransactions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `schoolcategories`
--

DROP TABLE IF EXISTS `schoolcategories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `schoolcategories` (
  `SchoolCategoryId` varchar(36) NOT NULL,
  `CategoryName` varchar(100) NOT NULL,
  `Description` varchar(250) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`SchoolCategoryId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `schoolcategories`
--

LOCK TABLES `schoolcategories` WRITE;
/*!40000 ALTER TABLE `schoolcategories` DISABLE KEYS */;
INSERT INTO `schoolcategories` VALUES ('fb94dafa-fe90-4f22-9d7e-bfbd59d529fd','Non-Composite','string','','2023-05-08 16:34:48.882','','2023-05-08 16:34:48.882',_binary '');
/*!40000 ALTER TABLE `schoolcategories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `schoolclasses`
--

DROP TABLE IF EXISTS `schoolclasses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `schoolclasses` (
  `ClassId` varchar(36) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(350) DEFAULT NULL,
  `DisplayOrder` int NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`ClassId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `schoolclasses`
--

LOCK TABLES `schoolclasses` WRITE;
/*!40000 ALTER TABLE `schoolclasses` DISABLE KEYS */;
/*!40000 ALTER TABLE `schoolclasses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `schools`
--

DROP TABLE IF EXISTS `schools`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `schools` (
  `SchoolId` varchar(36) NOT NULL,
  `SchoolName` varchar(150) NOT NULL,
  `SchoolCategoryId` varchar(36) NOT NULL,
  `SchoolTypeId` varchar(45) NOT NULL,
  `UDISE` varchar(11) NOT NULL,
  `AcademicYearId` varchar(36) NOT NULL,
  `PhaseId` varchar(36) NOT NULL,
  `StateCode` varchar(15) NOT NULL,
  `DivisionId` varchar(36) NOT NULL,
  `DistrictCode` varchar(15) NOT NULL,
  `BlockId` varchar(36) DEFAULT NULL,
  `ClusterId` varchar(36) DEFAULT NULL,
  `BlockName` varchar(100) NOT NULL,
  `Village` varchar(150) DEFAULT NULL,
  `Panchayat` varchar(150) DEFAULT NULL,
  `Pincode` varchar(6) DEFAULT NULL,
  `IsImplemented` bit(1) NOT NULL,
  `Demography` varchar(250) DEFAULT NULL,
  `SchoolManagementId` varchar(20) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`SchoolId`),
  KEY `FK_Schools_Districts` (`DistrictCode`),
  KEY `FK_Schools_States` (`StateCode`),
  KEY `FK_Schools_AcademicYears` (`AcademicYearId`),
  KEY `FK_Schools_Divisions` (`DivisionId`),
  KEY `FK_Schools_Phases` (`PhaseId`),
  KEY `FK_Schools_SchoolCategories` (`SchoolCategoryId`),
  CONSTRAINT `FK_Schools_AcademicYears` FOREIGN KEY (`AcademicYearId`) REFERENCES `academicyears` (`AcademicYearId`),
  CONSTRAINT `FK_Schools_Districts` FOREIGN KEY (`DistrictCode`) REFERENCES `districts` (`DistrictCode`),
  CONSTRAINT `FK_Schools_Divisions` FOREIGN KEY (`DivisionId`) REFERENCES `divisions` (`DivisionId`),
  CONSTRAINT `FK_Schools_Phases` FOREIGN KEY (`PhaseId`) REFERENCES `phases` (`PhaseId`),
  CONSTRAINT `FK_Schools_SchoolCategories` FOREIGN KEY (`SchoolCategoryId`) REFERENCES `schoolcategories` (`SchoolCategoryId`),
  CONSTRAINT `FK_Schools_States` FOREIGN KEY (`StateCode`) REFERENCES `states` (`StateCode`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `schools`
--

LOCK TABLES `schools` WRITE;
/*!40000 ALTER TABLE `schools` DISABLE KEYS */;
INSERT INTO `schools` VALUES ('043f49b6-3e58-4e10-8a12-9ef8655e44f2','ABC PUBLIC SCHOOL','fb94dafa-fe90-4f22-9d7e-bfbd59d529fd','101','27031404411','606ed468-fba1-46ac-ba70-ead58dc4ab29','d4917323-570a-4018-92a5-0a96806d5045','KA','b52fa10d-0e6f-4fc6-af51-f097f7261485','KDHLI','cfcef538-d03c-4452-afb7-56b6ff082587','3c16a1e3-2eb8-4b55-b16f-3634f31688b8','Testblock',NULL,NULL,NULL,_binary '',NULL,'104','kranthiND','2023-05-08 16:34:48.882',NULL,'2023-05-08 16:34:48.882',_binary '');
/*!40000 ALTER TABLE `schools` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `schoolsbyvtpsectorglfv`
--

DROP TABLE IF EXISTS `schoolsbyvtpsectorglfv`;
/*!50001 DROP VIEW IF EXISTS `schoolsbyvtpsectorglfv`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `schoolsbyvtpsectorglfv` AS SELECT 
 1 AS `ImplementedSchoolId`,
 1 AS `ApprovedSchoolId`,
 1 AS `AcademicYearId`,
 1 AS `VTPId`,
 1 AS `SectorId`,
 1 AS `VCId`,
 1 AS `VTId`,
 1 AS `VTSchoolSectorId`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `schoolsbyvtpsectorinfo`
--

DROP TABLE IF EXISTS `schoolsbyvtpsectorinfo`;
/*!50001 DROP VIEW IF EXISTS `schoolsbyvtpsectorinfo`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `schoolsbyvtpsectorinfo` AS SELECT 
 1 AS `ImplementedSchoolId`,
 1 AS `ApprovedSchoolId`,
 1 AS `AcademicYearId`,
 1 AS `VTPId`,
 1 AS `SectorId`,
 1 AS `VCId`,
 1 AS `VTId`,
 1 AS `VTSchoolSectorId`,
 1 AS `VTClassId`,
 1 AS `DivisionId`,
 1 AS `DistrictId`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `schoolveincharges`
--

DROP TABLE IF EXISTS `schoolveincharges`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `schoolveincharges` (
  `VEIId` varchar(36) NOT NULL,
  `VTId` varchar(36) DEFAULT NULL,
  `SchoolId` varchar(36) DEFAULT NULL,
  `FirstName` varchar(50) NOT NULL,
  `MiddleName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) NOT NULL,
  `FullName` varchar(150) NOT NULL,
  `Mobile` varchar(15) NOT NULL,
  `Mobile1` varchar(15) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `Gender` varchar(10) NOT NULL,
  `DateOfJoining` datetime(3) DEFAULT NULL,
  `DateOfResignationFromRoleSchool` datetime(3) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VEIId`),
  KEY `SchoolVEIncharges_Schools_idx` (`SchoolId`),
  CONSTRAINT `SchoolVEIncharges_Schools` FOREIGN KEY (`SchoolId`) REFERENCES `schools` (`SchoolId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `schoolveincharges`
--

LOCK TABLES `schoolveincharges` WRITE;
/*!40000 ALTER TABLE `schoolveincharges` DISABLE KEYS */;
/*!40000 ALTER TABLE `schoolveincharges` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `schoolvtpsectors`
--

DROP TABLE IF EXISTS `schoolvtpsectors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `schoolvtpsectors` (
  `SchoolVTPSectorId` varchar(36) NOT NULL,
  `AcademicYearId` varchar(36) NOT NULL,
  `SectorId` varchar(36) NOT NULL,
  `VTPId` varchar(36) NOT NULL,
  `SchoolId` varchar(36) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `IsAYRollover` bit(1) DEFAULT (0),
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`SchoolVTPSectorId`),
  KEY `FK_SchoolVTPSectors_AcademicYears` (`AcademicYearId`),
  KEY `FK_SchoolVTPSectors_Sectors` (`SectorId`),
  KEY `FK_SchoolVTPSectors_Schools` (`SchoolId`),
  KEY `FK_SchoolVTPSectors_VocationalTrainingProviders` (`VTPId`),
  CONSTRAINT `SchoolVTPSectors_AcademicYears` FOREIGN KEY (`AcademicYearId`) REFERENCES `academicyears` (`AcademicYearId`),
  CONSTRAINT `SchoolVTPSectors_Schools` FOREIGN KEY (`SchoolId`) REFERENCES `schools` (`SchoolId`),
  CONSTRAINT `SchoolVTPSectors_Sectors` FOREIGN KEY (`SectorId`) REFERENCES `sectors` (`SectorId`),
  CONSTRAINT `SchoolVTPSectors_VocationalTrainingProviders` FOREIGN KEY (`VTPId`) REFERENCES `vocationaltrainingproviders` (`VTPId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `schoolvtpsectors`
--

LOCK TABLES `schoolvtpsectors` WRITE;
/*!40000 ALTER TABLE `schoolvtpsectors` DISABLE KEYS */;
INSERT INTO `schoolvtpsectors` VALUES ('99311d9e-8632-4167-aae6-2b789a6b70b9','606ed468-fba1-46ac-ba70-ead58dc4ab29','844ee4d3-1867-453b-873e-26e1654782f2','643142aa-574a-42b2-8f75-dfad884f5fc5','043f49b6-3e58-4e10-8a12-9ef8655e44f2','',_binary '\0','','2023-05-09 12:01:44.173','','2023-05-09 12:01:44.173',_binary '');
/*!40000 ALTER TABLE `schoolvtpsectors` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sections`
--

DROP TABLE IF EXISTS `sections`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sections` (
  `SectionId` varchar(36) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(350) DEFAULT NULL,
  `DisplayOrder` int DEFAULT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`SectionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sections`
--

LOCK TABLES `sections` WRITE;
/*!40000 ALTER TABLE `sections` DISABLE KEYS */;
/*!40000 ALTER TABLE `sections` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sectorjobroles`
--

DROP TABLE IF EXISTS `sectorjobroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sectorjobroles` (
  `SectorJobRoleId` varchar(36) NOT NULL,
  `SectorId` varchar(36) NOT NULL,
  `JobRoleId` varchar(36) NOT NULL,
  `QPCode` varchar(15) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`SectorJobRoleId`),
  KEY `FK_SectorJobRoles_JobRoles` (`JobRoleId`),
  KEY `FK_SectorJobRoles_Sectors` (`SectorId`),
  CONSTRAINT `FK_SectorJobRoles_JobRoles` FOREIGN KEY (`JobRoleId`) REFERENCES `jobroles` (`JobRoleId`),
  CONSTRAINT `FK_SectorJobRoles_Sectors` FOREIGN KEY (`SectorId`) REFERENCES `sectors` (`SectorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sectorjobroles`
--

LOCK TABLES `sectorjobroles` WRITE;
/*!40000 ALTER TABLE `sectorjobroles` DISABLE KEYS */;
/*!40000 ALTER TABLE `sectorjobroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sectors`
--

DROP TABLE IF EXISTS `sectors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sectors` (
  `SectorId` varchar(36) NOT NULL,
  `SectorName` varchar(100) NOT NULL,
  `Description` varchar(250) DEFAULT NULL,
  `DisplayOrder` int NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`SectorId`),
  KEY `FK_CourseModules_Sectors_idx` (`SectorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sectors`
--

LOCK TABLES `sectors` WRITE;
/*!40000 ALTER TABLE `sectors` DISABLE KEYS */;
INSERT INTO `sectors` VALUES ('844ee4d3-1867-453b-873e-26e1654782f2','Agriculture','Agriculture',1,'','2023-05-09 11:53:14.484','','2023-05-09 11:53:14.484',_binary '');
/*!40000 ALTER TABLE `sectors` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `siteheaders`
--

DROP TABLE IF EXISTS `siteheaders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `siteheaders` (
  `SiteHeaderId` varchar(36) NOT NULL,
  `ShortName` varchar(40) NOT NULL,
  `LongName` varchar(100) DEFAULT NULL,
  `Description` varchar(350) DEFAULT NULL,
  `DisplayOrder` tinyint unsigned NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`SiteHeaderId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `siteheaders`
--

LOCK TABLES `siteheaders` WRITE;
/*!40000 ALTER TABLE `siteheaders` DISABLE KEYS */;
/*!40000 ALTER TABLE `siteheaders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sitesubheaders`
--

DROP TABLE IF EXISTS `sitesubheaders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sitesubheaders` (
  `SiteSubHeaderId` varchar(36) NOT NULL,
  `SiteHeaderId` varchar(36) DEFAULT NULL,
  `TransactionId` varchar(36) DEFAULT NULL,
  `IsHeaderMenu` tinyint NOT NULL,
  `DisplayOrder` tinyint unsigned NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`SiteSubHeaderId`),
  KEY `FK_dbo.SiteSubHeaders_dbo.SiteHeaders_SiteHeaderId` (`SiteHeaderId`),
  KEY `FK_dbo.SiteSubHeaders_dbo.Transactions_TransactionId` (`TransactionId`),
  CONSTRAINT `FK_dbo.SiteSubHeaders_dbo.SiteHeaders_SiteHeaderId` FOREIGN KEY (`SiteHeaderId`) REFERENCES `siteheaders` (`SiteHeaderId`) ON DELETE CASCADE,
  CONSTRAINT `FK_dbo.SiteSubHeaders_dbo.Transactions_TransactionId` FOREIGN KEY (`TransactionId`) REFERENCES `transactions` (`TransactionId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sitesubheaders`
--

LOCK TABLES `sitesubheaders` WRITE;
/*!40000 ALTER TABLE `sitesubheaders` DISABLE KEYS */;
/*!40000 ALTER TABLE `sitesubheaders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `states`
--

DROP TABLE IF EXISTS `states`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `states` (
  `StateCode` varchar(15) NOT NULL,
  `StateId` varchar(2) NOT NULL,
  `CountryCode` varchar(15) NOT NULL,
  `StateName` varchar(75) NOT NULL,
  `Description` varchar(350) DEFAULT NULL,
  `SequenceNo` int NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`StateCode`),
  KEY `FK_dbo.States_dbo.Countries_CountryCode` (`CountryCode`),
  CONSTRAINT `FK_dbo.States_dbo.Countries_CountryCode` FOREIGN KEY (`CountryCode`) REFERENCES `countries` (`CountryCode`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `states`
--

LOCK TABLES `states` WRITE;
/*!40000 ALTER TABLE `states` DISABLE KEYS */;
INSERT INTO `states` VALUES ('KA','KA','IN','Karnataka',NULL,1,'Somesh','2023-05-04 21:40:33.569',NULL,'2023-05-04 21:40:33.569',_binary '');
/*!40000 ALTER TABLE `states` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `studentclassdetails`
--

DROP TABLE IF EXISTS `studentclassdetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `studentclassdetails` (
  `StudentId` varchar(36) NOT NULL,
  `SectorId` varchar(36) DEFAULT NULL,
  `JobRoleId` varchar(36) DEFAULT NULL,
  `FatherName` varchar(100) NOT NULL,
  `MotherName` varchar(100) NOT NULL,
  `GuardianName` varchar(100) DEFAULT NULL,
  `DateOfBirth` datetime(3) NOT NULL,
  `AadhaarNumber` varchar(12) DEFAULT NULL,
  `StudentRollNumber` varchar(20) NOT NULL,
  `SocialCategory` varchar(150) NOT NULL,
  `Religion` varchar(150) DEFAULT NULL,
  `CWSNStatus` varchar(45) DEFAULT NULL,
  `Mobile` varchar(15) DEFAULT NULL,
  `Mobile1` varchar(15) DEFAULT NULL,
  `WhatsAppNo` varchar(15) DEFAULT NULL,
  `AssessmentConducted` varchar(50) DEFAULT NULL,
  `StreamId` varchar(36) DEFAULT NULL,
  `IsStudentVE9And10` varchar(50) DEFAULT NULL,
  `IsSameStudentTrade` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`StudentId`),
  CONSTRAINT `FK_StudentClassDetails_StudentClasses` FOREIGN KEY (`StudentId`) REFERENCES `studentclasses` (`StudentId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `studentclassdetails`
--

LOCK TABLES `studentclassdetails` WRITE;
/*!40000 ALTER TABLE `studentclassdetails` DISABLE KEYS */;
/*!40000 ALTER TABLE `studentclassdetails` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `studentclasses`
--

DROP TABLE IF EXISTS `studentclasses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `studentclasses` (
  `StudentId` varchar(36) NOT NULL,
  `VTId` varchar(36) DEFAULT NULL,
  `SchoolId` varchar(36) NOT NULL,
  `AcademicYearId` varchar(36) NOT NULL,
  `ClassId` varchar(36) NOT NULL,
  `SectionId` varchar(36) NOT NULL,
  `DateOfEnrollment` datetime(3) NOT NULL,
  `FirstName` varchar(50) NOT NULL,
  `MiddleName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) NOT NULL,
  `FullName` varchar(150) DEFAULT NULL,
  `Gender` varchar(10) NOT NULL,
  `Mobile` varchar(15) DEFAULT NULL,
  `DateOfDropout` datetime(3) DEFAULT NULL,
  `DropoutReason` varchar(150) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `DeletedBy` varchar(30) DEFAULT NULL,
  `DeletedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`StudentId`),
  KEY `FK_StudentClasses_Classes_idx` (`ClassId`),
  KEY `FK_StudentClasses_Sections_idx` (`SectionId`),
  KEY `FK_StudentClasses_AcademicYears` (`AcademicYearId`),
  KEY `IX_StudentClasses_SchoolId_StudentId` (`SchoolId`,`StudentId`),
  KEY `IX_StudentClasses_SchoolId_StudentId_Name` (`AcademicYearId`,`SchoolId`,`StudentId`,`FullName`),
  KEY `IX_StudentClasses_AY_School_Class_Sec_Student` (`AcademicYearId`,`SchoolId`,`ClassId`,`SectionId`,`StudentId`),
  CONSTRAINT `FK_StudentClasses_AcademicYears` FOREIGN KEY (`AcademicYearId`) REFERENCES `academicyears` (`AcademicYearId`),
  CONSTRAINT `FK_StudentClasses_Classes` FOREIGN KEY (`ClassId`) REFERENCES `schoolclasses` (`ClassId`),
  CONSTRAINT `FK_StudentClasses_Sections` FOREIGN KEY (`SectionId`) REFERENCES `sections` (`SectionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `studentclasses`
--

LOCK TABLES `studentclasses` WRITE;
/*!40000 ALTER TABLE `studentclasses` DISABLE KEYS */;
/*!40000 ALTER TABLE `studentclasses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `studentclassmapping`
--

DROP TABLE IF EXISTS `studentclassmapping`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `studentclassmapping` (
  `StudentClassMappingId` varchar(36) NOT NULL,
  `AcademicYearId` varchar(36) NOT NULL,
  `SchoolId` varchar(36) NOT NULL,
  `ClassId` varchar(36) NOT NULL,
  `SectionId` varchar(36) NOT NULL,
  `VTId` varchar(36) NOT NULL,
  `StudentId` varchar(36) NOT NULL,
  `StudentRollNumber` varchar(30) DEFAULT NULL,
  `IsAYRollover` bit(1) DEFAULT (0),
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`StudentClassMappingId`),
  KEY `IX_StudentClassMapping_SchoolId_StudentId` (`AcademicYearId`,`SchoolId`,`ClassId`,`SectionId`,`StudentId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `studentclassmapping`
--

LOCK TABLES `studentclassmapping` WRITE;
/*!40000 ALTER TABLE `studentclassmapping` DISABLE KEYS */;
/*!40000 ALTER TABLE `studentclassmapping` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `studentsforexitform`
--

DROP TABLE IF EXISTS `studentsforexitform`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `studentsforexitform` (
  `ExitStudentId` varchar(36) NOT NULL,
  `FirstName` varchar(100) DEFAULT NULL,
  `MiddleName` varchar(100) DEFAULT NULL,
  `LastName` varchar(100) DEFAULT NULL,
  `AcademicYear` varchar(45) DEFAULT NULL,
  `StudentFullName` varchar(100) DEFAULT NULL,
  `FatherName` varchar(100) DEFAULT NULL,
  `JobRole` varchar(45) DEFAULT NULL,
  `StudentUniqueId` varchar(45) DEFAULT NULL,
  `NameOfSchool` varchar(100) DEFAULT NULL,
  `UdiseCode` varchar(45) DEFAULT NULL,
  `District` varchar(45) DEFAULT NULL,
  `Class` varchar(45) DEFAULT NULL,
  `Gender` varchar(45) DEFAULT NULL,
  `DOB` datetime NOT NULL,
  `Category` varchar(45) DEFAULT NULL,
  `Sector` varchar(45) DEFAULT NULL,
  `VTPId` varchar(36) DEFAULT NULL,
  `VTPName` varchar(100) DEFAULT NULL,
  `VTId` varchar(36) DEFAULT NULL,
  `VTName` varchar(100) DEFAULT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `VCName` varchar(100) DEFAULT NULL,
  `CreatedBy` varchar(45) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(45) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) DEFAULT NULL,
  `VTMobile` varchar(15) DEFAULT NULL,
  `MotherName` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`ExitStudentId`),
  KEY `IX_StudentsForExitForm_StudentFullName` (`StudentFullName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `studentsforexitform`
--

LOCK TABLES `studentsforexitform` WRITE;
/*!40000 ALTER TABLE `studentsforexitform` DISABLE KEYS */;
/*!40000 ALTER TABLE `studentsforexitform` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `subissues`
--

DROP TABLE IF EXISTS `subissues`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `subissues` (
  `SubIssueId` varchar(36) NOT NULL,
  `MainIssueId` varchar(36) NOT NULL,
  `IssueName` varchar(150) NOT NULL,
  `IssueCategoryId` varchar(15) NOT NULL,
  `IssuePriority` varchar(10) NOT NULL,
  `Description` varchar(350) DEFAULT NULL,
  `DisplayOrder` int NOT NULL,
  `IsApplicableForVT` bit(1) NOT NULL,
  `IsApplicableForVC` bit(1) NOT NULL,
  `IsApplicableForHM` bit(1) NOT NULL,
  `AssignForReviewPMU` bit(1) NOT NULL,
  `AssignForReviewVC` bit(1) NOT NULL,
  `AssignForReviewHM` bit(1) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`SubIssueId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `subissues`
--

LOCK TABLES `subissues` WRITE;
/*!40000 ALTER TABLE `subissues` DISABLE KEYS */;
/*!40000 ALTER TABLE `subissues` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `teandrmlist`
--

DROP TABLE IF EXISTS `teandrmlist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `teandrmlist` (
  `TEAndRMId` varchar(36) NOT NULL,
  `SectorId` varchar(36) DEFAULT NULL,
  `JobRoleId` varchar(36) DEFAULT NULL,
  `ClassId` varchar(36) DEFAULT NULL,
  `TEType` varchar(50) NOT NULL,
  `SrNo` int NOT NULL,
  `ToolEquipmentName` varchar(200) NOT NULL,
  `Specification` varchar(2000) DEFAULT NULL,
  `UnitType` varchar(45) DEFAULT NULL,
  `UnitName` varchar(70) DEFAULT NULL,
  `CreatedBy` varchar(45) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(45) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`TEAndRMId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `teandrmlist`
--

LOCK TABLES `teandrmlist` WRITE;
/*!40000 ALTER TABLE `teandrmlist` DISABLE KEYS */;
/*!40000 ALTER TABLE `teandrmlist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `temateriallists`
--

DROP TABLE IF EXISTS `temateriallists`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `temateriallists` (
  `TEMaterialListId` varchar(36) NOT NULL,
  `ToolEquipmentId` varchar(36) NOT NULL,
  `RawMaterialId` varchar(36) NOT NULL,
  `RawMaterialName` varchar(350) DEFAULT NULL,
  `RawMaterialStatus` varchar(50) DEFAULT NULL,
  `RMLastReceivedDate` datetime DEFAULT NULL,
  `RawMaterialAction` varchar(50) DEFAULT NULL,
  `QuantityCount` int NOT NULL,
  `CreatedBy` varchar(30) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) DEFAULT NULL,
  PRIMARY KEY (`TEMaterialListId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `temateriallists`
--

LOCK TABLES `temateriallists` WRITE;
/*!40000 ALTER TABLE `temateriallists` DISABLE KEYS */;
/*!40000 ALTER TABLE `temateriallists` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `termsconditions`
--

DROP TABLE IF EXISTS `termsconditions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `termsconditions` (
  `TermsConditionId` varchar(36) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` longtext NOT NULL,
  `ApplicableFrom` datetime(3) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`TermsConditionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `termsconditions`
--

LOCK TABLES `termsconditions` WRITE;
/*!40000 ALTER TABLE `termsconditions` DISABLE KEYS */;
/*!40000 ALTER TABLE `termsconditions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tetoollists`
--

DROP TABLE IF EXISTS `tetoollists`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tetoollists` (
  `TEToolListId` varchar(36) NOT NULL,
  `ToolEquipmentId` varchar(36) NOT NULL,
  `ToolListId` varchar(36) NOT NULL,
  `ToolListName` varchar(350) DEFAULT NULL,
  `ToolListStatus` varchar(50) DEFAULT NULL,
  `TLActionNeeded1` varchar(50) DEFAULT NULL,
  `TLActionNeeded2` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(30) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) DEFAULT NULL,
  PRIMARY KEY (`TEToolListId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tetoollists`
--

LOCK TABLES `tetoollists` WRITE;
/*!40000 ALTER TABLE `tetoollists` DISABLE KEYS */;
/*!40000 ALTER TABLE `tetoollists` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `toolequipments`
--

DROP TABLE IF EXISTS `toolequipments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `toolequipments` (
  `ToolEquipmentId` varchar(36) NOT NULL,
  `DivisionId` varchar(36) DEFAULT NULL,
  `DistrictCode` varchar(15) DEFAULT NULL,
  `SchoolId` varchar(36) DEFAULT NULL,
  `VTPId` varchar(36) DEFAULT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `VTId` varchar(36) NOT NULL,
  `AcademicYearId` varchar(36) NOT NULL,
  `SectorId` varchar(36) NOT NULL,
  `JobRoleId` varchar(36) NOT NULL,
  `ReceiptDate` datetime DEFAULT NULL,
  `TEReceiveStatus` varchar(50) DEFAULT NULL,
  `TEStatus` varchar(50) DEFAULT NULL,
  `RMStatus` varchar(50) DEFAULT NULL,
  `RMFundStatus` varchar(50) DEFAULT NULL,
  `Details` varchar(350) DEFAULT NULL,
  `OATEStatus` varchar(50) DEFAULT NULL,
  `OFTEStatus` varchar(50) DEFAULT NULL,
  `Reason` varchar(50) DEFAULT NULL,
  `IsSelected` varchar(50) DEFAULT NULL,
  `IsSpecify` varchar(50) DEFAULT NULL,
  `RFNReceiveStatus` varchar(50) DEFAULT NULL,
  `IsCommunicated` varchar(50) DEFAULT NULL,
  `IsSetUpWorkShop` varchar(50) DEFAULT NULL,
  `RoomType` varchar(50) DEFAULT NULL,
  `AccommodateTools` varchar(50) DEFAULT NULL,
  `RoomSize` int DEFAULT NULL,
  `IsDoorLock` varchar(50) DEFAULT NULL,
  `Flooring` varchar(50) DEFAULT NULL,
  `RoomWindows` varchar(50) DEFAULT NULL,
  `TotalWindowCount` int DEFAULT NULL,
  `IsWindowGrills` varchar(50) DEFAULT NULL,
  `IsWindowLocked` varchar(50) DEFAULT NULL,
  `IsRoomActive` varchar(50) DEFAULT NULL,
  `REFInstalled` varchar(50) DEFAULT NULL,
  `WorkingSwitchBoard` varchar(50) DEFAULT NULL,
  `PSSCount` int DEFAULT NULL,
  `WLCount` int DEFAULT NULL,
  `WFCount` int DEFAULT NULL,
  `RawMaterialRequired` varchar(50) DEFAULT NULL,
  `ToolListId` varchar(36) DEFAULT NULL,
  `ToolListStatus` varchar(50) DEFAULT NULL,
  `TLActionNeeded1` varchar(50) DEFAULT NULL,
  `TLActionNeeded2` varchar(50) DEFAULT NULL,
  `RawMaterialId` varchar(36) DEFAULT NULL,
  `RawMaterialStatus` varchar(50) DEFAULT NULL,
  `RMLastReceivedDate` datetime DEFAULT NULL,
  `RawMaterialAction` varchar(50) DEFAULT NULL,
  `QuantityCount` int DEFAULT NULL,
  `TLFilePath` varchar(100) DEFAULT NULL,
  `LabFilePath` varchar(100) DEFAULT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `toolequipments`
--

LOCK TABLES `toolequipments` WRITE;
/*!40000 ALTER TABLE `toolequipments` DISABLE KEYS */;
/*!40000 ALTER TABLE `toolequipments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `toolequipmentsroomdamaged`
--

DROP TABLE IF EXISTS `toolequipmentsroomdamaged`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `toolequipmentsroomdamaged` (
  `ToolEquipmentRDId` varchar(36) NOT NULL,
  `ToolEquipmentId` varchar(36) NOT NULL,
  `RoomDamaged` varchar(50) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`ToolEquipmentRDId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `toolequipmentsroomdamaged`
--

LOCK TABLES `toolequipmentsroomdamaged` WRITE;
/*!40000 ALTER TABLE `toolequipmentsroomdamaged` DISABLE KEYS */;
/*!40000 ALTER TABLE `toolequipmentsroomdamaged` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `transactions`
--

DROP TABLE IF EXISTS `transactions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `transactions` (
  `TransactionId` varchar(36) NOT NULL,
  `Code` varchar(10) NOT NULL,
  `Name` varchar(70) NOT NULL,
  `PageTitle` varchar(200) NOT NULL,
  `PageDescription` varchar(500) DEFAULT NULL,
  `UrlAction` varchar(100) DEFAULT NULL,
  `UrlController` varchar(50) DEFAULT NULL,
  `UrlPara` varchar(300) DEFAULT NULL,
  `RouteUrl` varchar(150) DEFAULT NULL,
  `DisplayOrder` tinyint unsigned NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`TransactionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transactions`
--

LOCK TABLES `transactions` WRITE;
/*!40000 ALTER TABLE `transactions` DISABLE KEYS */;
/*!40000 ALTER TABLE `transactions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `useracceptances`
--

DROP TABLE IF EXISTS `useracceptances`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `useracceptances` (
  `UserAcceptanceId` varchar(36) NOT NULL,
  `TermsConditionId` varchar(36) DEFAULT NULL,
  `UserMachineId` varchar(150) DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`UserAcceptanceId`),
  KEY `FK_LmsAcceptances_TermsConditions_TermsConditionId` (`TermsConditionId`),
  CONSTRAINT `FK_LmsAcceptances_TermsConditions_TermsConditionId` FOREIGN KEY (`TermsConditionId`) REFERENCES `termsconditions` (`TermsConditionId`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `useracceptances`
--

LOCK TABLES `useracceptances` WRITE;
/*!40000 ALTER TABLE `useracceptances` DISABLE KEYS */;
/*!40000 ALTER TABLE `useracceptances` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userotpdetails`
--

DROP TABLE IF EXISTS `userotpdetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `userotpdetails` (
  `OTPId` varchar(36) NOT NULL,
  `Mobile` varchar(10) NOT NULL,
  `OTPToken` varchar(15) NOT NULL,
  `ExpireOn` datetime(3) NOT NULL,
  `IsRedeemed` tinyint NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`OTPId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userotpdetails`
--

LOCK TABLES `userotpdetails` WRITE;
/*!40000 ALTER TABLE `userotpdetails` DISABLE KEYS */;
/*!40000 ALTER TABLE `userotpdetails` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vcdailyreporting`
--

DROP TABLE IF EXISTS `vcdailyreporting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vcdailyreporting` (
  `VCDailyReportingId` varchar(36) NOT NULL,
  `VCSchoolSectorId` varchar(36) NOT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `ReportDate` datetime(3) DEFAULT NULL,
  `ReportType` varchar(50) DEFAULT NULL,
  `SchoolId` varchar(36) DEFAULT NULL,
  `WorkTypeDetails` varchar(250) DEFAULT NULL,
  `GeoLocation` varchar(50) DEFAULT NULL,
  `Latitude` varchar(20) DEFAULT NULL,
  `Longitude` varchar(20) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VCDailyReportingId`),
  KEY `FK_VCDailyReporting_VCSchoolSectors_idx` (`VCSchoolSectorId`),
  KEY `IX_VCDailyReporting_VCId_ReportType_ReportingDate` (`VCId`,`ReportType`,`ReportDate`),
  CONSTRAINT `FK_VCDailyReporting_VCSchoolSectors` FOREIGN KEY (`VCSchoolSectorId`) REFERENCES `vcschoolsectors` (`VCSchoolSectorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vcdailyreporting`
--

LOCK TABLES `vcdailyreporting` WRITE;
/*!40000 ALTER TABLE `vcdailyreporting` DISABLE KEYS */;
/*!40000 ALTER TABLE `vcdailyreporting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vcissuereporting`
--

DROP TABLE IF EXISTS `vcissuereporting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vcissuereporting` (
  `VCIssueReportingId` varchar(36) NOT NULL,
  `AcademicYearId` varchar(36) DEFAULT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `IssueMappingId` varchar(36) DEFAULT NULL,
  `IssueReportDate` datetime(3) NOT NULL,
  `MainIssue` varchar(50) DEFAULT NULL,
  `SubIssue` varchar(50) DEFAULT NULL,
  `StudentClass` varchar(100) NOT NULL,
  `Month` varchar(100) NOT NULL,
  `StudentType` varchar(50) DEFAULT NULL,
  `NoOfStudents` int NOT NULL,
  `IssueDetails` varchar(350) DEFAULT NULL,
  `GeoLocation` varchar(50) DEFAULT NULL,
  `Latitude` varchar(20) DEFAULT NULL,
  `Longitude` varchar(20) DEFAULT NULL,
  `ApprovalStatus` varchar(50) DEFAULT NULL,
  `ApprovedDate` datetime DEFAULT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VCIssueReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vcissuereporting`
--

LOCK TABLES `vcissuereporting` WRITE;
/*!40000 ALTER TABLE `vcissuereporting` DISABLE KEYS */;
/*!40000 ALTER TABLE `vcissuereporting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vcrholidays`
--

DROP TABLE IF EXISTS `vcrholidays`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vcrholidays` (
  `VCRHolidayId` varchar(36) NOT NULL,
  `VCDailyReportingId` varchar(36) NOT NULL,
  `HolidayTypeId` varchar(5) NOT NULL,
  `HolidayDetails` varchar(350) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VCRHolidayId`),
  KEY `FK_VCRHolidays_VCDailyReporting_idx` (`VCDailyReportingId`),
  CONSTRAINT `FK_VCRHolidays_VCDailyReporting` FOREIGN KEY (`VCDailyReportingId`) REFERENCES `vcdailyreporting` (`VCDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vcrholidays`
--

LOCK TABLES `vcrholidays` WRITE;
/*!40000 ALTER TABLE `vcrholidays` DISABLE KEYS */;
/*!40000 ALTER TABLE `vcrholidays` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vcrindustryexposurevisits`
--

DROP TABLE IF EXISTS `vcrindustryexposurevisits`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vcrindustryexposurevisits` (
  `VCRIndustryExposureVisitId` varchar(36) NOT NULL,
  `VCDailyReportingId` varchar(36) NOT NULL,
  `TypeOfIndustryLinkage` varchar(250) DEFAULT NULL,
  `ContactPersonName` varchar(100) NOT NULL,
  `ContactPersonMobile` varchar(15) NOT NULL,
  `ContactPersonEmail` varchar(100) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VCRIndustryExposureVisitId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vcrindustryexposurevisits`
--

LOCK TABLES `vcrindustryexposurevisits` WRITE;
/*!40000 ALTER TABLE `vcrindustryexposurevisits` DISABLE KEYS */;
/*!40000 ALTER TABLE `vcrindustryexposurevisits` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vcrleaves`
--

DROP TABLE IF EXISTS `vcrleaves`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vcrleaves` (
  `VCRLeaveId` varchar(36) NOT NULL,
  `VCDailyReportingId` varchar(36) NOT NULL,
  `LeaveTypeId` varchar(5) NOT NULL,
  `LeaveModeId` varchar(5) DEFAULT NULL,
  `LeaveApprovalStatus` varchar(20) NOT NULL,
  `LeaveApprover` varchar(5) NOT NULL,
  `LeaveReason` varchar(350) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VCRLeaveId`),
  KEY `FK_VCRLeaves_VCDailyReporting_idx` (`VCDailyReportingId`),
  CONSTRAINT `FK_VCRLeaves_VCDailyReporting` FOREIGN KEY (`VCDailyReportingId`) REFERENCES `vcdailyreporting` (`VCDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vcrleaves`
--

LOCK TABLES `vcrleaves` WRITE;
/*!40000 ALTER TABLE `vcrleaves` DISABLE KEYS */;
/*!40000 ALTER TABLE `vcrleaves` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vcrpraticals`
--

DROP TABLE IF EXISTS `vcrpraticals`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vcrpraticals` (
  `VTRPraticalId` varchar(36) NOT NULL,
  `VCDailyReportingId` varchar(36) DEFAULT NULL,
  `AcademicYearId` varchar(36) DEFAULT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `IsPratical` varchar(36) NOT NULL,
  `SchoolId` varchar(36) NOT NULL,
  `VTId` varchar(36) NOT NULL,
  `SectorId` varchar(36) NOT NULL,
  `JobRoleId` varchar(36) NOT NULL,
  `ClassId` varchar(36) NOT NULL,
  `StudentCount` varchar(50) DEFAULT NULL,
  `VTPresent` varchar(15) DEFAULT NULL,
  `PresentStudentCount` int DEFAULT NULL,
  `AssesorName` varchar(100) DEFAULT NULL,
  `AssesorMobileNo` varchar(15) DEFAULT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) DEFAULT NULL,
  `CreatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) DEFAULT NULL,
  PRIMARY KEY (`VTRPraticalId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vcrpraticals`
--

LOCK TABLES `vcrpraticals` WRITE;
/*!40000 ALTER TABLE `vcrpraticals` DISABLE KEYS */;
/*!40000 ALTER TABLE `vcrpraticals` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vcrschoolvisits`
--

DROP TABLE IF EXISTS `vcrschoolvisits`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vcrschoolvisits` (
  `VCRSchoolVisitId` varchar(36) NOT NULL,
  `VCDailyReportingId` varchar(36) NOT NULL,
  `SchoolId` varchar(36) NOT NULL,
  `WorkDetails` varchar(350) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VCRSchoolVisitId`),
  KEY `FK_VCRSchoolVisits_VCDailyReporting_idx` (`VCDailyReportingId`),
  CONSTRAINT `FK_VCRSchoolVisits_VCDailyReporting` FOREIGN KEY (`VCDailyReportingId`) REFERENCES `vcdailyreporting` (`VCDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vcrschoolvisits`
--

LOCK TABLES `vcrschoolvisits` WRITE;
/*!40000 ALTER TABLE `vcrschoolvisits` DISABLE KEYS */;
/*!40000 ALTER TABLE `vcrschoolvisits` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vcrworkingdaytypes`
--

DROP TABLE IF EXISTS `vcrworkingdaytypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vcrworkingdaytypes` (
  `VCRWorkingDayTypeId` varchar(36) NOT NULL,
  `VCDailyReportingId` varchar(36) NOT NULL,
  `WorkingTypeId` varchar(5) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VCRWorkingDayTypeId`),
  KEY `FK_VCRWorkingDayTypes_WorkingTypes_idx` (`WorkingTypeId`),
  KEY `FK_VCRHolidays_VCDailyReporting_idx` (`VCDailyReportingId`),
  CONSTRAINT `FK_VCRWorkingDayTypes_VCDailyReporting` FOREIGN KEY (`VCDailyReportingId`) REFERENCES `vcdailyreporting` (`VCDailyReportingId`),
  CONSTRAINT `FK_VCRWorkingDayTypes_WorkingTypes` FOREIGN KEY (`WorkingTypeId`) REFERENCES `datavalues` (`DataValueId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vcrworkingdaytypes`
--

LOCK TABLES `vcrworkingdaytypes` WRITE;
/*!40000 ALTER TABLE `vcrworkingdaytypes` DISABLE KEYS */;
/*!40000 ALTER TABLE `vcrworkingdaytypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vcschoolsectors`
--

DROP TABLE IF EXISTS `vcschoolsectors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vcschoolsectors` (
  `VCSchoolSectorId` varchar(36) NOT NULL,
  `AcademicYearId` varchar(36) NOT NULL,
  `VCId` varchar(36) NOT NULL,
  `SchoolVTPSectorId` varchar(36) NOT NULL,
  `DateOfAllocation` datetime(3) NOT NULL,
  `DateOfRemoval` datetime(3) DEFAULT NULL,
  `IsAYRollover` bit(1) DEFAULT (0),
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VCSchoolSectorId`),
  UNIQUE KEY `UC_VCSchoolSectors` (`AcademicYearId`,`VCId`,`SchoolVTPSectorId`,`IsActive`),
  KEY `FK_VCSchoolSectors_AcademicYears` (`AcademicYearId`),
  KEY `FK_VCSchoolSectors_VocationalCoordinators` (`VCId`),
  KEY `FK_VCSchoolSectors_SchoolVTPSectors_idx` (`SchoolVTPSectorId`),
  CONSTRAINT `FK_VCSchoolSectors_AcademicYears` FOREIGN KEY (`AcademicYearId`) REFERENCES `academicyears` (`AcademicYearId`),
  CONSTRAINT `FK_VCSchoolSectors_SchoolVTPSectors` FOREIGN KEY (`SchoolVTPSectorId`) REFERENCES `schoolvtpsectors` (`SchoolVTPSectorId`),
  CONSTRAINT `FK_VCSchoolSectors_VocationalCoordinators` FOREIGN KEY (`VCId`) REFERENCES `vocationalcoordinators` (`VCId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vcschoolsectors`
--

LOCK TABLES `vcschoolsectors` WRITE;
/*!40000 ALTER TABLE `vcschoolsectors` DISABLE KEYS */;
INSERT INTO `vcschoolsectors` VALUES ('38f74a25-b89f-4bb7-9527-0f11ed979d75','606ed468-fba1-46ac-ba70-ead58dc4ab29','4a9c17c7-dd9e-47ee-9ca6-84730396bb9b','99311d9e-8632-4167-aae6-2b789a6b70b9','2023-05-09 08:43:45.493',NULL,_binary '\0','kranthiND','2023-05-09 14:15:03.401','kranthiND','2023-05-09 14:15:03.401',_binary '');
/*!40000 ALTER TABLE `vcschoolsectors` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vcschoolvisitreporting`
--

DROP TABLE IF EXISTS `vcschoolvisitreporting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vcschoolvisitreporting` (
  `VCSchoolVisitReportingId` varchar(36) NOT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `CompanyName` varchar(200) DEFAULT NULL,
  `Month` varchar(20) DEFAULT NULL,
  `VisitDate` datetime DEFAULT NULL,
  `SchoolId` varchar(36) DEFAULT NULL,
  `DistrictCode` varchar(15) DEFAULT NULL,
  `SchoolEmailId` varchar(150) DEFAULT NULL,
  `PrincipalName` varchar(150) DEFAULT NULL,
  `PrincipalPhoneNo` varchar(15) DEFAULT NULL,
  `SectorId` varchar(36) DEFAULT NULL,
  `JobRoleId` varchar(36) DEFAULT NULL,
  `VTId` varchar(36) DEFAULT NULL,
  `VTPhoneNo` varchar(15) DEFAULT NULL,
  `Labs` varchar(100) DEFAULT NULL,
  `Books` varchar(100) DEFAULT NULL,
  `NoOfGLConducted` int NOT NULL,
  `NoOfIndustrialVisits` int NOT NULL,
  `SVPhotoWithPrincipal` varchar(250) DEFAULT NULL,
  `SVPhotoWithStudents` varchar(250) DEFAULT NULL,
  `Class9Boys` int NOT NULL,
  `Class9Girls` int NOT NULL,
  `Class10Boys` int NOT NULL,
  `Class10Girls` int NOT NULL,
  `Class11Boys` int NOT NULL,
  `Class11Girls` int NOT NULL,
  `Class12Boys` int NOT NULL,
  `Class12Girls` int NOT NULL,
  `TotalBoys` int NOT NULL,
  `TotalGirls` int NOT NULL,
  `GeoLocation` varchar(30) DEFAULT NULL,
  `Latitude` varchar(15) DEFAULT NULL,
  `Longitude` varchar(15) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VCSchoolVisitReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vcschoolvisitreporting`
--

LOCK TABLES `vcschoolvisitreporting` WRITE;
/*!40000 ALTER TABLE `vcschoolvisitreporting` DISABLE KEYS */;
/*!40000 ALTER TABLE `vcschoolvisitreporting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vcschoolvisits`
--

DROP TABLE IF EXISTS `vcschoolvisits`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vcschoolvisits` (
  `VCSchoolVisitId` varchar(36) NOT NULL,
  `VCSchoolSectorId` varchar(36) DEFAULT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `ReportDate` datetime(3) DEFAULT NULL,
  `GeoLocation` varchar(50) DEFAULT NULL,
  `Month` varchar(50) DEFAULT NULL,
  `VTReportSubmitted` varchar(50) DEFAULT NULL,
  `VTWorkingDays` int NOT NULL,
  `VTLeaveDays` int NOT NULL,
  `VTTeachingDays` int NOT NULL,
  `ClassVisited` varchar(50) NOT NULL,
  `ClassTeachingDays` int NOT NULL,
  `BoysEnrolledCheck` int NOT NULL,
  `GirlsEnrolledCheck` int NOT NULL,
  `AvgStudentAttendance` int NOT NULL,
  `CMAvailability` varchar(50) DEFAULT NULL,
  `CMDate` datetime(3) DEFAULT NULL,
  `TEAvailability` varchar(50) DEFAULT NULL,
  `TEDate` datetime(3) DEFAULT NULL,
  `NoOfGLConducted` int NOT NULL,
  `NoOfFVConducted` int NOT NULL,
  `SchoolHMVisited` varchar(50) DEFAULT NULL,
  `HMRatingVTattendance` int NOT NULL,
  `HMRatingSyllabuscompletion` int NOT NULL,
  `HMRatingVtreporting` int NOT NULL,
  `HMRatingVtqualityteaching` int NOT NULL,
  `HMRatingVtglfvquality` int NOT NULL,
  `HMRatingInitiativestaken` int NOT NULL,
  `Latitude` varchar(20) DEFAULT NULL,
  `Longitude` varchar(20) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VCSchoolVisitId`),
  KEY `FK_VCSchoolVisits_VCSchoolSectors` (`VCSchoolSectorId`),
  CONSTRAINT `FK_VCSchoolVisits_VCSchoolSectors` FOREIGN KEY (`VCSchoolSectorId`) REFERENCES `vcschoolsectors` (`VCSchoolSectorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vcschoolvisits`
--

LOCK TABLES `vcschoolvisits` WRITE;
/*!40000 ALTER TABLE `vcschoolvisits` DISABLE KEYS */;
/*!40000 ALTER TABLE `vcschoolvisits` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vctrainersmap`
--

DROP TABLE IF EXISTS `vctrainersmap`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vctrainersmap` (
  `VCTrainerId` varchar(36) NOT NULL,
  `AcademicYearId` varchar(36) NOT NULL,
  `VCId` varchar(36) NOT NULL,
  `VTId` varchar(36) NOT NULL,
  `VTPId` varchar(36) DEFAULT NULL,
  `DateOfJoining` datetime NOT NULL,
  `DateOfResignation` datetime DEFAULT NULL,
  `NatureOfAppointment` varchar(100) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VCTrainerId`),
  UNIQUE KEY `UC_VCTrainersMap` (`AcademicYearId`,`VCId`,`VTId`,`VTPId`,`IsActive`),
  KEY `FK_VCTrainersMap_AY` (`AcademicYearId`),
  KEY `FK_VCTrainersMap_VC` (`VCId`),
  KEY `FK_VCTrainersMap_VT` (`VTId`),
  KEY `FK_VCTrainersMap_VTP` (`VTPId`),
  KEY `IX_VCTrainersMap_AY_VTP_VC_VT` (`AcademicYearId`,`VTPId`,`VCId`,`VTId`,`IsActive`),
  KEY `IX_VCTrainersMap_AY_VT` (`AcademicYearId`,`VTId`,`IsActive`),
  CONSTRAINT `FK_VCTrainersMap_AY` FOREIGN KEY (`AcademicYearId`) REFERENCES `academicyears` (`AcademicYearId`),
  CONSTRAINT `FK_VCTrainersMap_VC` FOREIGN KEY (`VCId`) REFERENCES `vocationalcoordinators` (`VCId`),
  CONSTRAINT `FK_VCTrainersMap_VT` FOREIGN KEY (`VTId`) REFERENCES `vocationaltrainers` (`VTId`),
  CONSTRAINT `FK_VCTrainersMap_VTP` FOREIGN KEY (`VTPId`) REFERENCES `vocationaltrainingproviders` (`VTPId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vctrainersmap`
--

LOCK TABLES `vctrainersmap` WRITE;
/*!40000 ALTER TABLE `vctrainersmap` DISABLE KEYS */;
INSERT INTO `vctrainersmap` VALUES ('04e751a1-c750-4cad-9d55-adc9e24944f3','606ed468-fba1-46ac-ba70-ead58dc4ab29','4a9c17c7-dd9e-47ee-9ca6-84730396bb9b','88c24a7c-e985-41ea-bc64-f49b4f5029ef','643142aa-574a-42b2-8f75-dfad884f5fc5','2023-05-10 10:16:48','2023-05-10 10:16:48','107','someshvc','2023-05-10 16:06:24','someshvc','2023-05-10 16:06:24',_binary '\0'),('42322a62-3d76-4b05-a1a4-24d36860f29e','606ed468-fba1-46ac-ba70-ead58dc4ab29','4a9c17c7-dd9e-47ee-9ca6-84730396bb9b','e005e0e6-3bd9-4e3e-92e3-4b72c99599df','643142aa-574a-42b2-8f75-dfad884f5fc5','2023-05-10 10:16:48','2023-05-10 10:16:48','107','','2023-05-10 16:55:34','','2023-05-10 16:55:34',_binary '\0'),('ebac1de4-eced-48c0-932a-59e2e5219b8f','606ed468-fba1-46ac-ba70-ead58dc4ab29','4a9c17c7-dd9e-47ee-9ca6-84730396bb9b','0cfb70ed-3196-40c4-95ae-4b0d92a0ccf9','643142aa-574a-42b2-8f75-dfad884f5fc5','2023-05-10 10:16:48','2023-05-10 10:16:48','107','someshvc','2023-05-10 16:10:05','someshvc','2023-05-10 16:10:05',_binary '\0');
/*!40000 ALTER TABLE `vctrainersmap` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vocationalcoordinators`
--

DROP TABLE IF EXISTS `vocationalcoordinators`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vocationalcoordinators` (
  `VCId` varchar(36) NOT NULL,
  `VTPId` varchar(36) DEFAULT NULL,
  `FirstName` varchar(50) NOT NULL,
  `MiddleName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) NOT NULL,
  `FullName` varchar(150) NOT NULL,
  `Mobile` varchar(50) NOT NULL,
  `Mobile1` varchar(50) DEFAULT NULL,
  `EmailId` varchar(50) NOT NULL,
  `Gender` varchar(50) NOT NULL,
  `DateOfJoining` datetime(3) NOT NULL,
  `DateOfResignation` datetime(3) DEFAULT NULL,
  `NatureOfAppointment` varchar(50) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VCId`),
  KEY `FK_VocationalCoordinators_VocationalTrainingProviders` (`VTPId`),
  CONSTRAINT `FK_VocationalCoordinators_VocationalTrainingProviders` FOREIGN KEY (`VTPId`) REFERENCES `vocationaltrainingproviders` (`VTPId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vocationalcoordinators`
--

LOCK TABLES `vocationalcoordinators` WRITE;
/*!40000 ALTER TABLE `vocationalcoordinators` DISABLE KEYS */;
INSERT INTO `vocationalcoordinators` VALUES ('4a9c17c7-dd9e-47ee-9ca6-84730396bb9b','643142aa-574a-42b2-8f75-dfad884f5fc5','Somesh',NULL,'Reddy','Somesh Reddy','9603071210',NULL,'someshvc@gmail.com','Male','2022-05-09 12:01:44.173','2023-05-09 12:01:44.173','Through VTP','kranthiND','2023-05-09 12:01:44.173',NULL,'2023-05-09 12:01:44.173',_binary '');
/*!40000 ALTER TABLE `vocationalcoordinators` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vocationalcoordinatorshistory`
--

DROP TABLE IF EXISTS `vocationalcoordinatorshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vocationalcoordinatorshistory` (
  `VCId` varchar(36) NOT NULL,
  `VTPId` varchar(36) DEFAULT NULL,
  `FirstName` varchar(50) NOT NULL,
  `MiddleName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) NOT NULL,
  `FullName` varchar(150) NOT NULL,
  `Mobile` varchar(50) NOT NULL,
  `Mobile1` varchar(50) DEFAULT NULL,
  `EmailId` varchar(50) NOT NULL,
  `Gender` varchar(50) NOT NULL,
  `DateOfJoining` datetime(3) NOT NULL,
  `DateOfResignation` datetime(3) DEFAULT NULL,
  `NatureOfAppointment` varchar(50) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vocationalcoordinatorshistory`
--

LOCK TABLES `vocationalcoordinatorshistory` WRITE;
/*!40000 ALTER TABLE `vocationalcoordinatorshistory` DISABLE KEYS */;
/*!40000 ALTER TABLE `vocationalcoordinatorshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vocationaltrainers`
--

DROP TABLE IF EXISTS `vocationaltrainers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vocationaltrainers` (
  `VTId` varchar(36) NOT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `VTPId` varchar(36) DEFAULT NULL,
  `FirstName` varchar(50) NOT NULL,
  `MiddleName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) NOT NULL,
  `FullName` varchar(150) NOT NULL,
  `Mobile` varchar(15) NOT NULL,
  `Mobile1` varchar(15) DEFAULT NULL,
  `Email` varchar(100) NOT NULL,
  `Gender` varchar(10) NOT NULL,
  `DateOfBirth` datetime(3) NOT NULL,
  `SocialCategory` varchar(100) NOT NULL,
  `NatureOfAppointment` varchar(100) DEFAULT NULL,
  `AcademicQualification` varchar(150) NOT NULL,
  `ProfessionalQualification` varchar(150) NOT NULL,
  `ProfessionalQualificationDetails` varchar(350) DEFAULT NULL,
  `IndustryExperienceMonths` int NOT NULL,
  `TrainingExperienceMonths` int NOT NULL,
  `AadhaarNumber` varchar(12) NOT NULL,
  `DateOfJoining` datetime(3) DEFAULT NULL,
  `DateOfResignation` datetime(3) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTId`),
  KEY `FK_VocationalTrainers_VocationalTrainingProviders_idx` (`VTPId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vocationaltrainers`
--

LOCK TABLES `vocationaltrainers` WRITE;
/*!40000 ALTER TABLE `vocationaltrainers` DISABLE KEYS */;
INSERT INTO `vocationaltrainers` VALUES ('0cfb70ed-3196-40c4-95ae-4b0d92a0ccf9',NULL,NULL,'Somesh','','Reddy','Somesh Reddy','9603071210','','someshvt@gmail.com','Male','1996-07-07 10:16:47.964','105',NULL,'606ed468-fba1-46ac-ba70-ead58dc4ab29','106','',3,6,'463534725265',NULL,NULL,'someshvc','2023-05-10 16:10:04.858','someshvc','2023-05-10 16:10:04.858',_binary '\0'),('88c24a7c-e985-41ea-bc64-f49b4f5029ef',NULL,NULL,'Somesh','','Reddy','Somesh Reddy','9603071210','','someshvt@gmail.com','Male','1996-07-07 10:16:47.964','105',NULL,'606ed468-fba1-46ac-ba70-ead58dc4ab29','106','',3,6,'463534725265',NULL,NULL,'someshvc','2023-05-10 16:06:24.175','someshvc','2023-05-10 16:06:24.175',_binary '\0'),('e005e0e6-3bd9-4e3e-92e3-4b72c99599df',NULL,NULL,'Someshwar','','Reddy','Someshwar Reddy','9603071210','','someshwarvt@gmail.com','Male','1996-07-07 10:16:47.964','105',NULL,'606ed468-fba1-46ac-ba70-ead58dc4ab29','106','',3,6,'463534725265',NULL,NULL,'','2023-05-10 16:55:33.955','','2023-05-10 16:55:33.955',_binary '\0');
/*!40000 ALTER TABLE `vocationaltrainers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vocationaltrainershistory`
--

DROP TABLE IF EXISTS `vocationaltrainershistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vocationaltrainershistory` (
  `VTId` varchar(36) NOT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `VTPId` varchar(36) NOT NULL,
  `FirstName` varchar(50) NOT NULL,
  `MiddleName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) NOT NULL,
  `FullName` varchar(150) NOT NULL,
  `Mobile` varchar(15) NOT NULL,
  `Mobile1` varchar(15) DEFAULT NULL,
  `Email` varchar(100) NOT NULL,
  `Gender` varchar(10) NOT NULL,
  `DateOfBirth` datetime(3) NOT NULL,
  `SocialCategory` varchar(100) NOT NULL,
  `NatureOfAppointment` varchar(100) NOT NULL,
  `AcademicQualification` varchar(150) NOT NULL,
  `ProfessionalQualification` varchar(150) NOT NULL,
  `ProfessionalQualificationDetails` varchar(350) DEFAULT NULL,
  `IndustryExperienceMonths` int NOT NULL,
  `TrainingExperienceMonths` int NOT NULL,
  `AadhaarNumber` varchar(12) NOT NULL,
  `DateOfJoining` datetime(3) NOT NULL,
  `DateOfResignation` datetime(3) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vocationaltrainershistory`
--

LOCK TABLES `vocationaltrainershistory` WRITE;
/*!40000 ALTER TABLE `vocationaltrainershistory` DISABLE KEYS */;
/*!40000 ALTER TABLE `vocationaltrainershistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vocationaltrainingproviders`
--

DROP TABLE IF EXISTS `vocationaltrainingproviders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vocationaltrainingproviders` (
  `VTPId` varchar(36) NOT NULL,
  `VTPShortName` varchar(50) NOT NULL,
  `VTPName` varchar(150) NOT NULL,
  `ApprovalYear` varchar(15) DEFAULT NULL,
  `CertificationNo` varchar(30) DEFAULT NULL,
  `CertificationAgency` varchar(150) DEFAULT NULL,
  `VTPMobileNo` varchar(15) DEFAULT NULL,
  `VTPEmailId` varchar(100) DEFAULT NULL,
  `VTPAddress` varchar(350) DEFAULT NULL,
  `PrimaryContactPerson` varchar(100) DEFAULT NULL,
  `PrimaryMobileNumber` varchar(15) DEFAULT NULL,
  `PrimaryContactEmail` varchar(100) DEFAULT NULL,
  `VTPStateCoordinator` varchar(120) DEFAULT NULL,
  `VTPStateCoordinatorMobile` varchar(15) DEFAULT NULL,
  `VTPStateCoordinatorEmail` varchar(100) DEFAULT NULL,
  `ContractApprovalDate` datetime DEFAULT NULL,
  `ContractEndDate` datetime DEFAULT NULL,
  `MOUDocUpload` varchar(250) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTPId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vocationaltrainingproviders`
--

LOCK TABLES `vocationaltrainingproviders` WRITE;
/*!40000 ALTER TABLE `vocationaltrainingproviders` DISABLE KEYS */;
INSERT INTO `vocationaltrainingproviders` VALUES ('643142aa-574a-42b2-8f75-dfad884f5fc5','ACME','Acme India Mirosys Pvt.Ltd.','2022-2023','NSDC/2021-22/7014','National Skill Development Corporation (NSDC)','9603071210','allindiaacme@gmail.com','1st & 2nd floor Zunzarrao building,Zunzarrao market, behind anant   halwai, Kalyan(W), Thane-421301, Maharashtra.','Abhishek Dubey','9664203875','schoolprojects.acme@gmail.com','Abhishek Dubey','9664203875','schoolprojects.acme@gmail.com',NULL,NULL,NULL,'kranthiND','2023-05-08 16:44:48.868',NULL,'2023-05-08 16:44:48.868',_binary '');
/*!40000 ALTER TABLE `vocationaltrainingproviders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtclasses`
--

DROP TABLE IF EXISTS `vtclasses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtclasses` (
  `VTClassId` varchar(36) NOT NULL,
  `VTId` varchar(36) NOT NULL,
  `SchoolId` varchar(36) NOT NULL,
  `AcademicYearId` varchar(36) NOT NULL,
  `ClassId` varchar(36) NOT NULL,
  `SectionId` varchar(36) DEFAULT NULL,
  `IsAYRollover` bit(1) DEFAULT (0),
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTClassId`),
  KEY `FK_VTClasses_AcademicYears` (`AcademicYearId`),
  KEY `FK_VTClasses_VocationalTrainers` (`VTId`),
  KEY `FK_VTClasses_Classes` (`ClassId`),
  KEY `FK_VTClasses_Schools` (`SchoolId`),
  KEY `UC_VTClasses_AY_SCL_VT_CL` (`AcademicYearId`,`SchoolId`,`VTId`,`ClassId`,`IsActive`),
  KEY `IX_VTClasses_AY_School_VT_Class` (`AcademicYearId`,`SchoolId`,`ClassId`,`VTId`),
  CONSTRAINT `FK_VTClasses_AcademicYears` FOREIGN KEY (`AcademicYearId`) REFERENCES `academicyears` (`AcademicYearId`),
  CONSTRAINT `FK_VTClasses_Classes` FOREIGN KEY (`ClassId`) REFERENCES `schoolclasses` (`ClassId`),
  CONSTRAINT `FK_VTClasses_Schools` FOREIGN KEY (`SchoolId`) REFERENCES `schools` (`SchoolId`),
  CONSTRAINT `FK_VTClasses_VocationalTrainers` FOREIGN KEY (`VTId`) REFERENCES `vocationaltrainers` (`VTId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtclasses`
--

LOCK TABLES `vtclasses` WRITE;
/*!40000 ALTER TABLE `vtclasses` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtclasses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtclasssections`
--

DROP TABLE IF EXISTS `vtclasssections`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtclasssections` (
  `VTClassSectionId` varchar(36) NOT NULL,
  `VTClassId` varchar(36) NOT NULL,
  `SectionId` varchar(36) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTClassSectionId`),
  KEY `FK_VTClassSections_Sections_idx` (`SectionId`),
  CONSTRAINT `FK_VTClassSections_Sections` FOREIGN KEY (`SectionId`) REFERENCES `sections` (`SectionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtclasssections`
--

LOCK TABLES `vtclasssections` WRITE;
/*!40000 ALTER TABLE `vtclasssections` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtclasssections` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtclassstudents`
--

DROP TABLE IF EXISTS `vtclassstudents`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtclassstudents` (
  `VTClassStudentId` varchar(36) NOT NULL,
  `VTId` varchar(36) NOT NULL,
  `StudentId` varchar(36) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTClassStudentId`),
  KEY `FK_VTClassStudents_StudentClasses_idx` (`StudentId`),
  KEY `FK_VTClassStudents_VocationalTrainers_idx` (`VTId`),
  CONSTRAINT `FK_VTClassStudents_StudentClasses` FOREIGN KEY (`StudentId`) REFERENCES `studentclasses` (`StudentId`),
  CONSTRAINT `FK_VTClassStudents_VocationalTrainers` FOREIGN KEY (`VTId`) REFERENCES `vocationaltrainers` (`VTId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtclassstudents`
--

LOCK TABLES `vtclassstudents` WRITE;
/*!40000 ALTER TABLE `vtclassstudents` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtclassstudents` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtdailyreporting`
--

DROP TABLE IF EXISTS `vtdailyreporting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtdailyreporting` (
  `VTDailyReportingId` varchar(36) NOT NULL,
  `VTSchoolSectorId` varchar(36) NOT NULL,
  `VTId` varchar(36) DEFAULT NULL,
  `ReportingDate` datetime(3) NOT NULL,
  `ReportType` varchar(50) NOT NULL,
  `WorkingDayType` varchar(5) DEFAULT NULL,
  `SchoolEventCelebration` varchar(350) DEFAULT NULL,
  `WorkAssignedByHeadMaster` varchar(350) DEFAULT NULL,
  `SchoolExamDuty` varchar(350) DEFAULT NULL,
  `OtherWork` varchar(350) DEFAULT NULL,
  `ObservationDetails` varchar(350) DEFAULT NULL,
  `OBStudentCount` int NOT NULL,
  `GeoLocation` varchar(50) DEFAULT NULL,
  `Latitude` varchar(20) DEFAULT NULL,
  `Longitude` varchar(20) DEFAULT NULL,
  `ApprovalStatus` varchar(50) DEFAULT NULL,
  `ApprovedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTDailyReportingId`),
  KEY `FK_VTDailyReporting_VTSchoolSectors` (`VTSchoolSectorId`),
  KEY `IX_VTDailyReporting_VTId_ReportType_ReportingDate` (`VTId`,`ReportType`,`ReportingDate`),
  KEY `IX_VTDailyReporting_VTSchoolSectorId_ReportType_ReportingDate` (`VTSchoolSectorId`,`ReportType`,`ReportingDate`),
  CONSTRAINT `FK_VTDailyReporting_VTSchoolSectors` FOREIGN KEY (`VTSchoolSectorId`) REFERENCES `vtschoolsectors` (`VTSchoolSectorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtdailyreporting`
--

LOCK TABLES `vtdailyreporting` WRITE;
/*!40000 ALTER TABLE `vtdailyreporting` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtdailyreporting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtfieldindustryvisitconducted`
--

DROP TABLE IF EXISTS `vtfieldindustryvisitconducted`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtfieldindustryvisitconducted` (
  `VTFieldIndustryVisitConductedId` varchar(36) NOT NULL,
  `VTSchoolSectorId` varchar(36) NOT NULL,
  `VTId` varchar(36) DEFAULT NULL,
  `ReportingDate` datetime(3) NOT NULL,
  `ClassTaughtId` varchar(36) NOT NULL,
  `SectionTaughtId` varchar(36) DEFAULT NULL,
  `FieldVisitTheme` varchar(150) DEFAULT NULL,
  `FieldVisitActivities` varchar(200) DEFAULT NULL,
  `FVOrganisation` varchar(150) DEFAULT NULL,
  `FVOrganisationAddress` varchar(350) DEFAULT NULL,
  `FVDistance` varchar(100) DEFAULT NULL,
  `FVPicture` varchar(250) DEFAULT NULL,
  `FVContactPersonName` varchar(150) DEFAULT NULL,
  `FVContactPersonMobile` varchar(15) DEFAULT NULL,
  `FVContactPersonEmail` varchar(100) DEFAULT NULL,
  `FVContactPersonDesignation` varchar(150) DEFAULT NULL,
  `FVOrganisationInterestStatus` varchar(10) DEFAULT NULL,
  `FVOrignisationOJTStatus` varchar(10) DEFAULT NULL,
  `FeedbackFromOrgnisation` varchar(350) DEFAULT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `GeoLocation` varchar(50) DEFAULT NULL,
  `Latitude` varchar(20) DEFAULT NULL,
  `Longitude` varchar(20) DEFAULT NULL,
  `ApprovalStatus` varchar(50) DEFAULT NULL,
  `ApprovedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTFieldIndustryVisitConductedId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtfieldindustryvisitconducted`
--

LOCK TABLES `vtfieldindustryvisitconducted` WRITE;
/*!40000 ALTER TABLE `vtfieldindustryvisitconducted` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtfieldindustryvisitconducted` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtfsections`
--

DROP TABLE IF EXISTS `vtfsections`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtfsections` (
  `VTFSectionId` varchar(36) NOT NULL,
  `VTFieldIndustryVisitConductedId` varchar(36) NOT NULL,
  `SectionId` varchar(36) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTFSectionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtfsections`
--

LOCK TABLES `vtfsections` WRITE;
/*!40000 ALTER TABLE `vtfsections` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtfsections` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtfstudentattendances`
--

DROP TABLE IF EXISTS `vtfstudentattendances`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtfstudentattendances` (
  `VTFStudentAttendanceId` varchar(36) NOT NULL,
  `VTFieldIndustryVisitConductedId` varchar(36) NOT NULL,
  `VTId` varchar(36) DEFAULT NULL,
  `ClassId` varchar(36) DEFAULT NULL,
  `StudentId` varchar(36) NOT NULL,
  `IsPresent` bit(1) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTFStudentAttendanceId`),
  KEY `FK_VTFStudentAttendances_VTFieldIndustryVisitConducted_idx` (`VTFieldIndustryVisitConductedId`),
  CONSTRAINT `FK_VTFStudentAttendances_VTFieldIndustryVisitConducted` FOREIGN KEY (`VTFieldIndustryVisitConductedId`) REFERENCES `vtfieldindustryvisitconducted` (`VTFieldIndustryVisitConductedId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtfstudentattendances`
--

LOCK TABLES `vtfstudentattendances` WRITE;
/*!40000 ALTER TABLE `vtfstudentattendances` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtfstudentattendances` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtfunitsessionstaught`
--

DROP TABLE IF EXISTS `vtfunitsessionstaught`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtfunitsessionstaught` (
  `VTFUnitSessionsTaughtId` varchar(36) NOT NULL,
  `VTFieldIndustryVisitConductedId` varchar(36) NOT NULL,
  `ModuleId` varchar(36) NOT NULL,
  `UnitId` varchar(36) NOT NULL,
  `SessionId` varchar(36) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTFUnitSessionsTaughtId`),
  KEY `FK_VTFUnitSessionsTaught_VTFieldIndustryVisitConducted_idx` (`VTFieldIndustryVisitConductedId`),
  CONSTRAINT `FK_VTFUnitSessionsTaught_VTFieldIndustryVisitConducted` FOREIGN KEY (`VTFieldIndustryVisitConductedId`) REFERENCES `vtfieldindustryvisitconducted` (`VTFieldIndustryVisitConductedId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtfunitsessionstaught`
--

LOCK TABLES `vtfunitsessionstaught` WRITE;
/*!40000 ALTER TABLE `vtfunitsessionstaught` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtfunitsessionstaught` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtgmethodologies`
--

DROP TABLE IF EXISTS `vtgmethodologies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtgmethodologies` (
  `VTGMethodologyId` varchar(36) NOT NULL,
  `VTGuestLectureId` varchar(36) NOT NULL,
  `MethodologyId` varchar(36) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTGMethodologyId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtgmethodologies`
--

LOCK TABLES `vtgmethodologies` WRITE;
/*!40000 ALTER TABLE `vtgmethodologies` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtgmethodologies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtgsections`
--

DROP TABLE IF EXISTS `vtgsections`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtgsections` (
  `VTGSectionId` varchar(36) NOT NULL,
  `VTGuestLectureId` varchar(36) NOT NULL,
  `SectionId` varchar(36) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTGSectionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtgsections`
--

LOCK TABLES `vtgsections` WRITE;
/*!40000 ALTER TABLE `vtgsections` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtgsections` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtgstudentattendances`
--

DROP TABLE IF EXISTS `vtgstudentattendances`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtgstudentattendances` (
  `VTGStudentAttendanceId` varchar(36) NOT NULL,
  `VTGuestLectureId` varchar(36) NOT NULL,
  `VTId` varchar(36) DEFAULT NULL,
  `ClassId` varchar(36) DEFAULT NULL,
  `StudentId` varchar(36) NOT NULL,
  `IsPresent` bit(1) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTGStudentAttendanceId`),
  KEY `FK_VTGStudentAttendances_VTGuestLectureConducted_idx` (`VTGuestLectureId`),
  CONSTRAINT `FK_VTGStudentAttendances_VTGuestLectureConducted` FOREIGN KEY (`VTGuestLectureId`) REFERENCES `vtguestlectureconducted` (`VTGuestLectureId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtgstudentattendances`
--

LOCK TABLES `vtgstudentattendances` WRITE;
/*!40000 ALTER TABLE `vtgstudentattendances` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtgstudentattendances` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtguestlectureconducted`
--

DROP TABLE IF EXISTS `vtguestlectureconducted`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtguestlectureconducted` (
  `VTGuestLectureId` varchar(36) NOT NULL,
  `VTSchoolSectorId` varchar(36) NOT NULL,
  `VTId` varchar(36) DEFAULT NULL,
  `ClassTaughtId` varchar(36) NOT NULL,
  `SectionTaughtId` varchar(36) DEFAULT NULL,
  `ReportingDate` datetime(3) NOT NULL,
  `GLType` varchar(36) NOT NULL,
  `GLTopic` varchar(150) NOT NULL,
  `ClassTime` int NOT NULL,
  `GLMethodologyDetails` varchar(350) DEFAULT NULL,
  `GLPhotoInClass` varchar(350) DEFAULT NULL,
  `GLConductedBy` varchar(100) DEFAULT NULL,
  `GLPersonDetails` varchar(350) DEFAULT NULL,
  `GLName` varchar(150) DEFAULT NULL,
  `GLMobile` varchar(15) DEFAULT NULL,
  `GLEmail` varchar(100) DEFAULT NULL,
  `GLQualification` varchar(100) DEFAULT NULL,
  `GLWorkExperience` varchar(50) DEFAULT NULL,
  `GLAddress` varchar(350) DEFAULT NULL,
  `GLWorkStatus` varchar(36) DEFAULT NULL,
  `GLCompany` varchar(200) DEFAULT NULL,
  `GLDesignation` varchar(100) DEFAULT NULL,
  `GLPhoto` varchar(350) DEFAULT NULL,
  `GeoLocation` varchar(50) DEFAULT NULL,
  `Latitude` varchar(20) DEFAULT NULL,
  `Longitude` varchar(20) DEFAULT NULL,
  `ApprovalStatus` varchar(50) DEFAULT NULL,
  `ApprovedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTGuestLectureId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtguestlectureconducted`
--

LOCK TABLES `vtguestlectureconducted` WRITE;
/*!40000 ALTER TABLE `vtguestlectureconducted` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtguestlectureconducted` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtgunitsessionstaught`
--

DROP TABLE IF EXISTS `vtgunitsessionstaught`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtgunitsessionstaught` (
  `VTGUnitSessionsTaughtId` varchar(36) NOT NULL,
  `VTGuestLectureId` varchar(36) NOT NULL,
  `ModuleId` varchar(36) NOT NULL,
  `UnitId` varchar(36) NOT NULL,
  `SessionId` varchar(36) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTGUnitSessionsTaughtId`),
  KEY `FK_VTGUnitSessionsTaught_VTGuestLectureConducted_idx` (`VTGuestLectureId`),
  CONSTRAINT `FK_VTGUnitSessionsTaught_VTGuestLectureConducted` FOREIGN KEY (`VTGuestLectureId`) REFERENCES `vtguestlectureconducted` (`VTGuestLectureId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtgunitsessionstaught`
--

LOCK TABLES `vtgunitsessionstaught` WRITE;
/*!40000 ALTER TABLE `vtgunitsessionstaught` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtgunitsessionstaught` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtissuereporting`
--

DROP TABLE IF EXISTS `vtissuereporting`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtissuereporting` (
  `VTIssueReportingId` varchar(36) NOT NULL,
  `AcademicYearId` varchar(36) DEFAULT NULL,
  `VTId` varchar(36) DEFAULT NULL,
  `IssueMappingId` varchar(36) DEFAULT NULL,
  `IssueReportDate` datetime(3) NOT NULL,
  `MainIssue` varchar(50) DEFAULT NULL,
  `SubIssue` varchar(50) DEFAULT NULL,
  `StudentClass` varchar(100) NOT NULL,
  `Month` varchar(100) NOT NULL,
  `StudentType` varchar(50) DEFAULT NULL,
  `NoOfStudents` int NOT NULL,
  `IssueDetails` varchar(350) DEFAULT NULL,
  `GeoLocation` varchar(50) DEFAULT NULL,
  `Latitude` varchar(20) DEFAULT NULL,
  `Longitude` varchar(20) DEFAULT NULL,
  `ApprovalStatus` varchar(50) DEFAULT NULL,
  `ApprovedDate` datetime DEFAULT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTIssueReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtissuereporting`
--

LOCK TABLES `vtissuereporting` WRITE;
/*!40000 ALTER TABLE `vtissuereporting` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtissuereporting` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtmonthlyteachingplans`
--

DROP TABLE IF EXISTS `vtmonthlyteachingplans`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtmonthlyteachingplans` (
  `VTMonthlyTeachingPlanId` varchar(36) NOT NULL,
  `VTClassId` varchar(36) DEFAULT NULL,
  `Month` varchar(20) DEFAULT NULL,
  `WeekStartDate` datetime(3) DEFAULT NULL,
  `WeekendDate` datetime(3) DEFAULT NULL,
  `ModulesPlanned` varchar(100) DEFAULT NULL,
  `IVPlannedDate` datetime(3) DEFAULT NULL,
  `IVVCAttend` varchar(50) DEFAULT NULL,
  `FVPlannedDate` datetime(3) DEFAULT NULL,
  `FVPurpose` varchar(150) DEFAULT NULL,
  `FVLocation` varchar(100) DEFAULT NULL,
  `GLPlannedDate` datetime(3) DEFAULT NULL,
  `OtherDetails` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTMonthlyTeachingPlanId`),
  KEY `FK_VTMonthlyTeachingPlans_VTClasses` (`VTClassId`),
  CONSTRAINT `FK_VTMonthlyTeachingPlans_VTClasses` FOREIGN KEY (`VTClassId`) REFERENCES `vtclasses` (`VTClassId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtmonthlyteachingplans`
--

LOCK TABLES `vtmonthlyteachingplans` WRITE;
/*!40000 ALTER TABLE `vtmonthlyteachingplans` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtmonthlyteachingplans` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtpacademicyearsmap`
--

DROP TABLE IF EXISTS `vtpacademicyearsmap`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtpacademicyearsmap` (
  `VTPAcademicYearId` varchar(36) NOT NULL,
  `AcademicYearId` varchar(36) NOT NULL,
  `VTPId` varchar(36) NOT NULL,
  `DateOfJoining` datetime DEFAULT NULL,
  `DateOfResignation` datetime DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) DEFAULT NULL,
  PRIMARY KEY (`VTPAcademicYearId`),
  UNIQUE KEY `UC_VTPAcademicYearsMap` (`AcademicYearId`,`VTPId`,`IsActive`),
  KEY `FK_VTPAcademicYearsMap_AY` (`AcademicYearId`),
  KEY `FK_VTPAcademicYearsMap_VTP` (`VTPId`),
  KEY `IX_AY_VTP` (`AcademicYearId`,`VTPId`,`IsActive`),
  CONSTRAINT `FK_VTPAcademicYearsMap_AY` FOREIGN KEY (`AcademicYearId`) REFERENCES `academicyears` (`AcademicYearId`),
  CONSTRAINT `FK_VTPAcademicYearsMap_VTP` FOREIGN KEY (`VTPId`) REFERENCES `vocationaltrainingproviders` (`VTPId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtpacademicyearsmap`
--

LOCK TABLES `vtpacademicyearsmap` WRITE;
/*!40000 ALTER TABLE `vtpacademicyearsmap` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtpacademicyearsmap` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtpcoordinatorsmap`
--

DROP TABLE IF EXISTS `vtpcoordinatorsmap`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtpcoordinatorsmap` (
  `VTPCoordinatorId` varchar(36) NOT NULL,
  `AcademicYearId` varchar(36) NOT NULL,
  `VTPId` varchar(36) NOT NULL,
  `VCId` varchar(36) NOT NULL,
  `DateOfJoining` datetime DEFAULT NULL,
  `DateOfResignation` datetime DEFAULT NULL,
  `NatureOfAppointment` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) DEFAULT NULL,
  PRIMARY KEY (`VTPCoordinatorId`),
  KEY `FK_VTPCoordinatorsMap_AY` (`AcademicYearId`),
  KEY `FK_VTPCoordinatorsMap_VTP` (`VTPId`),
  KEY `FK_VTPCoordinatorsMap_VC` (`VCId`),
  CONSTRAINT `FK_VTPCoordinatorsMap_AY` FOREIGN KEY (`AcademicYearId`) REFERENCES `academicyears` (`AcademicYearId`),
  CONSTRAINT `FK_VTPCoordinatorsMap_VC` FOREIGN KEY (`VCId`) REFERENCES `vocationalcoordinators` (`VCId`),
  CONSTRAINT `FK_VTPCoordinatorsMap_VTP` FOREIGN KEY (`VTPId`) REFERENCES `vocationaltrainingproviders` (`VTPId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtpcoordinatorsmap`
--

LOCK TABLES `vtpcoordinatorsmap` WRITE;
/*!40000 ALTER TABLE `vtpcoordinatorsmap` DISABLE KEYS */;
INSERT INTO `vtpcoordinatorsmap` VALUES ('a76625e7-b2a0-48b4-bdad-418c6bdc9393','606ed468-fba1-46ac-ba70-ead58dc4ab29','643142aa-574a-42b2-8f75-dfad884f5fc5','4a9c17c7-dd9e-47ee-9ca6-84730396bb9b','2022-05-09 12:01:44','2023-05-09 12:01:44','Through VTP','kranthiND','2023-05-09 12:01:44',NULL,'2023-05-09 12:01:44',_binary '');
/*!40000 ALTER TABLE `vtpcoordinatorsmap` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtpmonthlybillsubmissionstatus`
--

DROP TABLE IF EXISTS `vtpmonthlybillsubmissionstatus`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtpmonthlybillsubmissionstatus` (
  `VTPMonthlyBillSubmissionStatusId` varchar(36) NOT NULL,
  `VCId` varchar(36) DEFAULT NULL,
  `Month` varchar(50) NOT NULL,
  `DateSubmission` datetime(3) DEFAULT NULL,
  `Incorrect` varchar(50) DEFAULT NULL,
  `IncorrectDetails` varchar(50) DEFAULT NULL,
  `Final` varchar(50) NOT NULL,
  `ApprovedPMU` varchar(50) DEFAULT NULL,
  `Amount` int NOT NULL,
  `DiaryentryDone` varchar(50) DEFAULT NULL,
  `DiaryentryNumber` varchar(50) DEFAULT NULL,
  `Details` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTPMonthlyBillSubmissionStatusId`),
  KEY `FK_VTPMonthlyBillSubmissionStatus_VocationalCoordinators` (`VCId`),
  CONSTRAINT `FK_VTPMonthlyBillSubmissionStatus_VocationalCoordinators` FOREIGN KEY (`VCId`) REFERENCES `vocationalcoordinators` (`VCId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtpmonthlybillsubmissionstatus`
--

LOCK TABLES `vtpmonthlybillsubmissionstatus` WRITE;
/*!40000 ALTER TABLE `vtpmonthlybillsubmissionstatus` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtpmonthlybillsubmissionstatus` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtpracticalassessments`
--

DROP TABLE IF EXISTS `vtpracticalassessments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtpracticalassessments` (
  `VTPracticalAssessmentId` varchar(36) NOT NULL,
  `VTClassId` varchar(36) DEFAULT NULL,
  `AssessmentDate` datetime(3) NOT NULL,
  `BoysPresent` int DEFAULT NULL,
  `GirlsPresent` int DEFAULT NULL,
  `AssessorName` varchar(100) NOT NULL,
  `AssessorMobile` varchar(15) DEFAULT NULL,
  `AssessorEmail` varchar(100) DEFAULT NULL,
  `AssessorQualification` varchar(150) DEFAULT NULL,
  `AssessorTimeReached` datetime(3) DEFAULT NULL,
  `AssessorIdCheck` varchar(50) DEFAULT NULL,
  `AssessorIdType` varchar(100) DEFAULT NULL,
  `AssessorSSCLetter` varchar(50) DEFAULT NULL,
  `AssessorBehaviour` varchar(50) DEFAULT NULL,
  `AssessorDemands` varchar(50) DEFAULT NULL,
  `AssessorBehaiourFormality` varchar(50) DEFAULT NULL,
  `AssessorGroupPhoto` varchar(350) DEFAULT NULL,
  `VCPMUNameVisit` varchar(50) DEFAULT NULL,
  `RemarksDetails` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTPracticalAssessmentId`),
  KEY `FK_VTPracticalAssessment_VTClasses` (`VTClassId`),
  CONSTRAINT `FK_VTPracticalAssessment_VTClasses` FOREIGN KEY (`VTClassId`) REFERENCES `vtclasses` (`VTClassId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtpracticalassessments`
--

LOCK TABLES `vtpracticalassessments` WRITE;
/*!40000 ALTER TABLE `vtpracticalassessments` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtpracticalassessments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtpsectorjobroles`
--

DROP TABLE IF EXISTS `vtpsectorjobroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtpsectorjobroles` (
  `VTPSectorJobRoleId` varchar(36) NOT NULL,
  `VTPId` varchar(36) DEFAULT NULL,
  `SectorId` varchar(36) DEFAULT NULL,
  `JobRoleId` varchar(36) DEFAULT NULL,
  `VTPSectorJobRoleName` varchar(100) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTPSectorJobRoleId`),
  KEY `FK_VTPSectorJobRoles_JobRoles` (`JobRoleId`),
  KEY `FK_VTPSectorJobRoles_Sectors` (`SectorId`),
  KEY `FK_VTPSectorJobRoles_VocationalTrainingProviders` (`VTPId`),
  CONSTRAINT `FK_VTPSectorJobRoles_JobRoles` FOREIGN KEY (`JobRoleId`) REFERENCES `jobroles` (`JobRoleId`),
  CONSTRAINT `FK_VTPSectorJobRoles_Sectors` FOREIGN KEY (`SectorId`) REFERENCES `sectors` (`SectorId`),
  CONSTRAINT `FK_VTPSectorJobRoles_VocationalTrainingProviders` FOREIGN KEY (`VTPId`) REFERENCES `vocationaltrainingproviders` (`VTPId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtpsectorjobroles`
--

LOCK TABLES `vtpsectorjobroles` WRITE;
/*!40000 ALTER TABLE `vtpsectorjobroles` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtpsectorjobroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtpsectors`
--

DROP TABLE IF EXISTS `vtpsectors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtpsectors` (
  `VTPSectorId` varchar(36) NOT NULL,
  `AcademicYearId` varchar(36) NOT NULL,
  `VTPId` varchar(36) NOT NULL,
  `SectorId` varchar(36) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `IsAYRollover` bit(1) DEFAULT (0),
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTPSectorId`),
  UNIQUE KEY `UC_VTPSectors_AY_VTP_SE` (`AcademicYearId`,`VTPId`,`SectorId`,`IsActive`),
  KEY `FK_VTPSectors_AcademicYears` (`AcademicYearId`),
  KEY `FK_VTPSectors_Sectors` (`SectorId`),
  KEY `FK_VTPSectors_VocationalTrainingProviders` (`VTPId`),
  KEY `IX_VTPSectors_AY_VTP_SE` (`AcademicYearId`,`VTPId`,`SectorId`,`IsActive`),
  CONSTRAINT `FK_VTPSectors_AcademicYears` FOREIGN KEY (`AcademicYearId`) REFERENCES `academicyears` (`AcademicYearId`),
  CONSTRAINT `FK_VTPSectors_SectorJobRoles` FOREIGN KEY (`SectorId`) REFERENCES `sectors` (`SectorId`),
  CONSTRAINT `FK_VTPSectors_VocationalTrainingProviders` FOREIGN KEY (`VTPId`) REFERENCES `vocationaltrainingproviders` (`VTPId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtpsectors`
--

LOCK TABLES `vtpsectors` WRITE;
/*!40000 ALTER TABLE `vtpsectors` DISABLE KEYS */;
INSERT INTO `vtpsectors` VALUES ('bbe2d111-d065-4c77-a18f-9f84258212fd','606ed468-fba1-46ac-ba70-ead58dc4ab29','643142aa-574a-42b2-8f75-dfad884f5fc5','844ee4d3-1867-453b-873e-26e1654782f2','',_binary '\0','','2023-05-09 11:59:11.026','','2023-05-09 11:59:11.026',_binary '');
/*!40000 ALTER TABLE `vtpsectors` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtractivitytypes`
--

DROP TABLE IF EXISTS `vtractivitytypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtractivitytypes` (
  `VTRActivityTypeId` varchar(36) NOT NULL,
  `VTRTeachingVocationalEducationId` varchar(36) NOT NULL,
  `ActivityTypeId` varchar(36) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRActivityTypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtractivitytypes`
--

LOCK TABLES `vtractivitytypes` WRITE;
/*!40000 ALTER TABLE `vtractivitytypes` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtractivitytypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtrassessorinotherschoolforexams`
--

DROP TABLE IF EXISTS `vtrassessorinotherschoolforexams`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtrassessorinotherschoolforexams` (
  `VTRAssessorInOtherSchoolForExamId` varchar(36) NOT NULL,
  `VTDailyReportingId` varchar(36) NOT NULL,
  `SchoolName` varchar(200) NOT NULL,
  `UDISE` varchar(11) NOT NULL,
  `ClassId` varchar(36) NOT NULL,
  `BoysPresent` int NOT NULL,
  `GirlsPresent` int NOT NULL,
  `ExamDetails` varchar(350) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRAssessorInOtherSchoolForExamId`),
  KEY `FK_VTRAssessorInOtherSchoolForExams_VTDailyReporting_idx` (`VTDailyReportingId`),
  CONSTRAINT `FK_VTRAssessorInOtherSchoolForExams_VTDailyReporting` FOREIGN KEY (`VTDailyReportingId`) REFERENCES `vtdailyreporting` (`VTDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtrassessorinotherschoolforexams`
--

LOCK TABLES `vtrassessorinotherschoolforexams` WRITE;
/*!40000 ALTER TABLE `vtrassessorinotherschoolforexams` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtrassessorinotherschoolforexams` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtrassignmentfromvocationaldepartments`
--

DROP TABLE IF EXISTS `vtrassignmentfromvocationaldepartments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtrassignmentfromvocationaldepartments` (
  `VTRAssignmentFromVocationalDepartmentId` varchar(36) NOT NULL,
  `VTDailyReportingId` varchar(36) NOT NULL,
  `AssigmentNumber` varchar(30) DEFAULT NULL,
  `AssignmentDetails` varchar(350) NOT NULL,
  `AssignmentPhoto` varchar(250) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRAssignmentFromVocationalDepartmentId`),
  KEY `FK_VTRAssignmentFromVocationalDepartments_VTDailyReporting_idx` (`VTDailyReportingId`),
  CONSTRAINT `FK_VTRAssignmentFromVocationalDepartments_VTDailyReporting` FOREIGN KEY (`VTDailyReportingId`) REFERENCES `vtdailyreporting` (`VTDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtrassignmentfromvocationaldepartments`
--

LOCK TABLES `vtrassignmentfromvocationaldepartments` WRITE;
/*!40000 ALTER TABLE `vtrassignmentfromvocationaldepartments` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtrassignmentfromvocationaldepartments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtrclasssectionstaught`
--

DROP TABLE IF EXISTS `vtrclasssectionstaught`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtrclasssectionstaught` (
  `VTRClassSectionsTaughtId` varchar(36) NOT NULL,
  `VTRTeachingVocationalEducationId` varchar(36) DEFAULT NULL,
  `ClassId` varchar(36) NOT NULL,
  `SectionId` varchar(36) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRClassSectionsTaughtId`),
  KEY `VTRClassSectionsTaught_Sections_idx` (`SectionId`),
  KEY `VTRClassSectionsTaught_SchoolClasses_idx` (`ClassId`),
  KEY `VTRClassSectionsTaught_VTRTeachingVocationalEducations_idx` (`VTRTeachingVocationalEducationId`),
  CONSTRAINT `VTRClassSectionsTaught_SchoolClasses` FOREIGN KEY (`ClassId`) REFERENCES `schoolclasses` (`ClassId`),
  CONSTRAINT `VTRClassSectionsTaught_Sections` FOREIGN KEY (`SectionId`) REFERENCES `sections` (`SectionId`),
  CONSTRAINT `VTRClassSectionsTaught_VTRTeachingVocationalEducations` FOREIGN KEY (`VTRTeachingVocationalEducationId`) REFERENCES `vtrteachingvocationaleducations` (`VTRTeachingVocationalEducationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtrclasssectionstaught`
--

LOCK TABLES `vtrclasssectionstaught` WRITE;
/*!40000 ALTER TABLE `vtrclasssectionstaught` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtrclasssectionstaught` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtrcommunityhomevisits`
--

DROP TABLE IF EXISTS `vtrcommunityhomevisits`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtrcommunityhomevisits` (
  `VTRCommunityHomeVisitId` varchar(36) NOT NULL,
  `VTDailyReportingId` varchar(36) NOT NULL,
  `VocationalParentsCount` int NOT NULL,
  `OtherParentsCount` int NOT NULL,
  `CommunityVisitDetails` varchar(350) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRCommunityHomeVisitId`),
  KEY `FK_VTRCommunityHomeVisits_VTDailyReporting_idx` (`VTDailyReportingId`),
  CONSTRAINT `FK_VTRCommunityHomeVisits_VTDailyReporting` FOREIGN KEY (`VTDailyReportingId`) REFERENCES `vtdailyreporting` (`VTDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtrcommunityhomevisits`
--

LOCK TABLES `vtrcommunityhomevisits` WRITE;
/*!40000 ALTER TABLE `vtrcommunityhomevisits` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtrcommunityhomevisits` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtreportsubmissions`
--

DROP TABLE IF EXISTS `vtreportsubmissions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtreportsubmissions` (
  `VTReportSubmissionId` int NOT NULL AUTO_INCREMENT,
  `VTSchoolSectorId` varchar(36) NOT NULL,
  `VTId` varchar(36) NOT NULL,
  `ReportingDate` date NOT NULL,
  `IsReportSubmitted` bit(1) NOT NULL DEFAULT (0),
  `IsHoliday` bit(1) NOT NULL DEFAULT (0),
  PRIMARY KEY (`VTReportSubmissionId`)
) ENGINE=InnoDB AUTO_INCREMENT=1147678 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtreportsubmissions`
--

LOCK TABLES `vtreportsubmissions` WRITE;
/*!40000 ALTER TABLE `vtreportsubmissions` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtreportsubmissions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtrholidays`
--

DROP TABLE IF EXISTS `vtrholidays`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtrholidays` (
  `VTRHolidayId` varchar(36) NOT NULL,
  `VTDailyReportingId` varchar(36) NOT NULL,
  `HolidayTypeId` varchar(5) NOT NULL,
  `HolidayDetails` varchar(350) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRHolidayId`),
  KEY `FK_VTRHolidays_VTDailyReporting_idx` (`VTDailyReportingId`),
  CONSTRAINT `FK_VTRHolidays_VTDailyReporting` FOREIGN KEY (`VTDailyReportingId`) REFERENCES `vtdailyreporting` (`VTDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtrholidays`
--

LOCK TABLES `vtrholidays` WRITE;
/*!40000 ALTER TABLE `vtrholidays` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtrholidays` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtrleaves`
--

DROP TABLE IF EXISTS `vtrleaves`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtrleaves` (
  `VTRLeaveId` varchar(36) NOT NULL,
  `VTDailyReportingId` varchar(36) NOT NULL,
  `LeaveTypeId` varchar(5) NOT NULL,
  `LeaveModeId` varchar(5) DEFAULT NULL,
  `LeaveApprovalStatus` varchar(20) NOT NULL,
  `LeaveApprover` varchar(5) NOT NULL,
  `LeaveReason` varchar(350) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRLeaveId`),
  KEY `FK_VTRLeaves_VTDailyReporting_idx` (`VTDailyReportingId`),
  CONSTRAINT `FK_VTRLeaves_VTDailyReporting` FOREIGN KEY (`VTDailyReportingId`) REFERENCES `vtdailyreporting` (`VTDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtrleaves`
--

LOCK TABLES `vtrleaves` WRITE;
/*!40000 ALTER TABLE `vtrleaves` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtrleaves` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtrobservationdays`
--

DROP TABLE IF EXISTS `vtrobservationdays`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtrobservationdays` (
  `VTRObservationDayId` varchar(36) NOT NULL,
  `VTDailyReportingId` varchar(36) NOT NULL,
  `VTId` varchar(36) NOT NULL,
  `ClassId` varchar(36) NOT NULL,
  `StudentId` varchar(36) NOT NULL,
  `IsPresent` bit(1) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRObservationDayId`),
  KEY `FK_VTRObservationDays_VTDailyReporting_idx` (`VTDailyReportingId`),
  KEY `FK_VTRObservationDays_VocationalTrainers_idx` (`VTId`),
  KEY `FK_VTRObservationDays_SchoolClasses_idx` (`ClassId`),
  KEY `FK_VTRObservationDays_StudentClasses_idx` (`StudentId`),
  CONSTRAINT `FK_VTRObservationDays_SchoolClasses` FOREIGN KEY (`ClassId`) REFERENCES `schoolclasses` (`ClassId`),
  CONSTRAINT `FK_VTRObservationDays_StudentClasses` FOREIGN KEY (`StudentId`) REFERENCES `studentclasses` (`StudentId`),
  CONSTRAINT `FK_VTRObservationDays_VocationalTrainers` FOREIGN KEY (`VTId`) REFERENCES `vocationaltrainers` (`VTId`),
  CONSTRAINT `FK_VTRObservationDays_VTDailyReporting` FOREIGN KEY (`VTDailyReportingId`) REFERENCES `vtdailyreporting` (`VTDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtrobservationdays`
--

LOCK TABLES `vtrobservationdays` WRITE;
/*!40000 ALTER TABLE `vtrobservationdays` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtrobservationdays` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtronjobtrainingcoordinations`
--

DROP TABLE IF EXISTS `vtronjobtrainingcoordinations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtronjobtrainingcoordinations` (
  `VTROnJobTrainingCoordinationId` varchar(36) NOT NULL,
  `VTDailyReportingId` varchar(36) NOT NULL,
  `OnJobTrainingActivityId` varchar(5) NOT NULL,
  `IndustryName` varchar(150) DEFAULT NULL,
  `IndustryContactPerson` varchar(100) DEFAULT NULL,
  `IndustryContactNumber` varchar(15) DEFAULT NULL,
  `OJTActivityDetails` varchar(350) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTROnJobTrainingCoordinationId`),
  KEY `FK_VTROnJobTrainingCoordinations_VTDailyReporting_idx` (`VTDailyReportingId`),
  CONSTRAINT `FK_VTRTrainingOfTeachers_VTROnJobTrainingCoordinations` FOREIGN KEY (`VTDailyReportingId`) REFERENCES `vtdailyreporting` (`VTDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtronjobtrainingcoordinations`
--

LOCK TABLES `vtronjobtrainingcoordinations` WRITE;
/*!40000 ALTER TABLE `vtronjobtrainingcoordinations` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtronjobtrainingcoordinations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtrparentteachersmeetings`
--

DROP TABLE IF EXISTS `vtrparentteachersmeetings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtrparentteachersmeetings` (
  `VTRParentTeachersMeetingId` varchar(36) NOT NULL,
  `VTDailyReportingId` varchar(36) NOT NULL,
  `VocationalParentsCount` int NOT NULL,
  `OtherParentsCount` int NOT NULL,
  `PTADetails` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRParentTeachersMeetingId`),
  KEY `FK_VTRParentTeachersMeetings_VTDailyReporting_idx` (`VTDailyReportingId`),
  CONSTRAINT `FK_VTRParentTeachersMeetings_VTDailyReporting` FOREIGN KEY (`VTDailyReportingId`) REFERENCES `vtdailyreporting` (`VTDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtrparentteachersmeetings`
--

LOCK TABLES `vtrparentteachersmeetings` WRITE;
/*!40000 ALTER TABLE `vtrparentteachersmeetings` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtrparentteachersmeetings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtrreasonofnotconductingtheclasses`
--

DROP TABLE IF EXISTS `vtrreasonofnotconductingtheclasses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtrreasonofnotconductingtheclasses` (
  `VTRReasonOfNotConductingTheClassId` varchar(36) NOT NULL,
  `VTRTeachingVocationalEducationId` varchar(36) NOT NULL,
  `ReasonTypeId` varchar(5) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRReasonOfNotConductingTheClassId`),
  KEY `FK_VTRReasonOfNotConductingTheClasses_ReasonTypes_idx` (`ReasonTypeId`),
  CONSTRAINT `FK_VTRReasonOfNotConductingTheClasses_ReasonTypes` FOREIGN KEY (`ReasonTypeId`) REFERENCES `datavalues` (`DataValueId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtrreasonofnotconductingtheclasses`
--

LOCK TABLES `vtrreasonofnotconductingtheclasses` WRITE;
/*!40000 ALTER TABLE `vtrreasonofnotconductingtheclasses` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtrreasonofnotconductingtheclasses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtrstudentattendances`
--

DROP TABLE IF EXISTS `vtrstudentattendances`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtrstudentattendances` (
  `VTRStudentAttendanceId` varchar(36) NOT NULL,
  `VTRTeachingVocationalEducationId` varchar(36) NOT NULL,
  `VTId` varchar(36) DEFAULT NULL,
  `ClassId` varchar(36) DEFAULT NULL,
  `StudentId` varchar(36) NOT NULL,
  `IsPresent` bit(1) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRStudentAttendanceId`),
  KEY `FK_VTRStudentAttendances_VTRTeachingVocationalEducations_idx` (`VTRTeachingVocationalEducationId`),
  CONSTRAINT `FK_VTRStudentAttendances_VTRTeachingVocationalEducations` FOREIGN KEY (`VTRTeachingVocationalEducationId`) REFERENCES `vtrteachingvocationaleducations` (`VTRTeachingVocationalEducationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtrstudentattendances`
--

LOCK TABLES `vtrstudentattendances` WRITE;
/*!40000 ALTER TABLE `vtrstudentattendances` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtrstudentattendances` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtrteachingnonvocationalsubjects`
--

DROP TABLE IF EXISTS `vtrteachingnonvocationalsubjects`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtrteachingnonvocationalsubjects` (
  `VTRTeachingNonVocationalSubjectId` varchar(36) NOT NULL,
  `VTDailyReportingId` varchar(36) NOT NULL,
  `OtherClassTakenDetails` varchar(350) NOT NULL,
  `OtherClassTime` int NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRTeachingNonVocationalSubjectId`),
  KEY `FK_VTRTeachingNonVocationalSubjects_VTDailyReporting_idx` (`VTDailyReportingId`),
  CONSTRAINT `FK_VTRTeachingNonVocationalSubjects_VTDailyReporting` FOREIGN KEY (`VTDailyReportingId`) REFERENCES `vtdailyreporting` (`VTDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtrteachingnonvocationalsubjects`
--

LOCK TABLES `vtrteachingnonvocationalsubjects` WRITE;
/*!40000 ALTER TABLE `vtrteachingnonvocationalsubjects` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtrteachingnonvocationalsubjects` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtrteachingvocationaleducations`
--

DROP TABLE IF EXISTS `vtrteachingvocationaleducations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtrteachingvocationaleducations` (
  `VTRTeachingVocationalEducationId` varchar(36) NOT NULL,
  `VTDailyReportingId` varchar(36) NOT NULL,
  `ClassTaughtId` varchar(36) NOT NULL,
  `SectionTaughtId` varchar(36) DEFAULT NULL,
  `ClassTypeId` varchar(50) DEFAULT NULL,
  `ClassTime` varchar(36) DEFAULT NULL,
  `ClassPicture` varchar(350) DEFAULT NULL,
  `LessonPlanPicture` varchar(350) DEFAULT NULL,
  `ReasonDetails` varchar(36) DEFAULT NULL,
  `IsTeachToday` bit(1) NOT NULL,
  `SequenceNo` int DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRTeachingVocationalEducationId`),
  KEY `FK_VTRTeachingVocationalEducations_VTDailyReporting_idx` (`VTDailyReportingId`),
  CONSTRAINT `FK_VTRTeachingVocationalEducations_VTDailyReporting` FOREIGN KEY (`VTDailyReportingId`) REFERENCES `vtdailyreporting` (`VTDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtrteachingvocationaleducations`
--

LOCK TABLES `vtrteachingvocationaleducations` WRITE;
/*!40000 ALTER TABLE `vtrteachingvocationaleducations` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtrteachingvocationaleducations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtrtrainingofteachers`
--

DROP TABLE IF EXISTS `vtrtrainingofteachers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtrtrainingofteachers` (
  `VTRTrainingOfTeacherId` varchar(36) NOT NULL,
  `VTDailyReportingId` varchar(36) NOT NULL,
  `TrainingTypeId` varchar(5) NOT NULL,
  `TrainingBy` varchar(100) DEFAULT NULL,
  `TrainingDetails` varchar(36) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRTrainingOfTeacherId`),
  KEY `FK_VTRTrainingOfTeachers_VTDailyReporting_idx` (`VTDailyReportingId`),
  KEY `FK_VTRTrainingOfTeachers_TrainingTypes_idx` (`TrainingTypeId`),
  CONSTRAINT `FK_VTRTrainingOfTeachers_VTDailyReporting` FOREIGN KEY (`VTDailyReportingId`) REFERENCES `vtdailyreporting` (`VTDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtrtrainingofteachers`
--

LOCK TABLES `vtrtrainingofteachers` WRITE;
/*!40000 ALTER TABLE `vtrtrainingofteachers` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtrtrainingofteachers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtrtrainingtopics`
--

DROP TABLE IF EXISTS `vtrtrainingtopics`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtrtrainingtopics` (
  `VTRTrainingTopicId` varchar(36) NOT NULL,
  `VTRTrainingOfTeacherId` varchar(36) NOT NULL,
  `TrainingTopicId` varchar(50) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRTrainingTopicId`),
  KEY `FK_VTRTrainingTopic_TrainingTopics_idx` (`TrainingTopicId`),
  KEY `FK_VTRTrainingTopic_VTRTrainingOfTeachers_idx` (`VTRTrainingOfTeacherId`),
  CONSTRAINT `FK_VTRTrainingTopic_TrainingTopics` FOREIGN KEY (`TrainingTopicId`) REFERENCES `datavalues` (`DataValueId`),
  CONSTRAINT `FK_VTRTrainingTopic_VTRTrainingOfTeachers` FOREIGN KEY (`VTRTrainingOfTeacherId`) REFERENCES `vtrtrainingofteachers` (`VTRTrainingOfTeacherId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtrtrainingtopics`
--

LOCK TABLES `vtrtrainingtopics` WRITE;
/*!40000 ALTER TABLE `vtrtrainingtopics` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtrtrainingtopics` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtrunitsessionstaught`
--

DROP TABLE IF EXISTS `vtrunitsessionstaught`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtrunitsessionstaught` (
  `VTRUnitSessionsTaughtId` varchar(36) NOT NULL,
  `VTRTeachingVocationalEducationId` varchar(36) NOT NULL,
  `ModuleId` varchar(50) NOT NULL,
  `UnitId` varchar(36) NOT NULL,
  `SessionId` varchar(36) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRUnitSessionsTaughtId`),
  KEY `FK_VTRUnitsTaughts_CourseUnitSessions_idx` (`SessionId`),
  CONSTRAINT `FK_VTRUnitsTaughts_CourseUnitSessions` FOREIGN KEY (`SessionId`) REFERENCES `courseunitsessions` (`CourseUnitSessionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtrunitsessionstaught`
--

LOCK TABLES `vtrunitsessionstaught` WRITE;
/*!40000 ALTER TABLE `vtrunitsessionstaught` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtrunitsessionstaught` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtrvisittoeducationalinstitutions`
--

DROP TABLE IF EXISTS `vtrvisittoeducationalinstitutions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtrvisittoeducationalinstitutions` (
  `VTRVisitToEducationalInstitutionId` varchar(36) NOT NULL,
  `VTDailyReportingId` varchar(36) NOT NULL,
  `EducationalInstituteVisitCount` int NOT NULL,
  `EducationalInstitute` varchar(150) NOT NULL,
  `InstituteContactPerson` varchar(100) NOT NULL,
  `InstituteContactNumber` varchar(15) NOT NULL,
  `InstituteVisitDetails` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRVisitToEducationalInstitutionId`),
  KEY `FK_VTRVisitToEducationalInstitutions_VTDailyReporting_idx` (`VTDailyReportingId`),
  CONSTRAINT `FK_VTRVisitToEducationalInstitutions_VTDailyReporting` FOREIGN KEY (`VTDailyReportingId`) REFERENCES `vtdailyreporting` (`VTDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtrvisittoeducationalinstitutions`
--

LOCK TABLES `vtrvisittoeducationalinstitutions` WRITE;
/*!40000 ALTER TABLE `vtrvisittoeducationalinstitutions` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtrvisittoeducationalinstitutions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtrvisittoindustries`
--

DROP TABLE IF EXISTS `vtrvisittoindustries`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtrvisittoindustries` (
  `VTRVisitToIndustryId` varchar(36) NOT NULL,
  `VTDailyReportingId` varchar(36) NOT NULL,
  `IndustryVisitCount` int NOT NULL,
  `IndustryName` varchar(150) NOT NULL,
  `IndustryContactPerson` varchar(100) NOT NULL,
  `IndustryContactNumber` varchar(15) NOT NULL,
  `IndustryVisitDetails` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRVisitToIndustryId`),
  KEY `FK_VTRVisitToIndustries_VTDailyReporting_idx` (`VTDailyReportingId`),
  CONSTRAINT `FK_VTRVisitToIndustries_VTDailyReporting` FOREIGN KEY (`VTDailyReportingId`) REFERENCES `vtdailyreporting` (`VTDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtrvisittoindustries`
--

LOCK TABLES `vtrvisittoindustries` WRITE;
/*!40000 ALTER TABLE `vtrvisittoindustries` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtrvisittoindustries` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtrworkassignedbyheadmasters`
--

DROP TABLE IF EXISTS `vtrworkassignedbyheadmasters`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtrworkassignedbyheadmasters` (
  `VTRWorkAssignedByHeadMasterId` varchar(36) NOT NULL,
  `VTDailyReportingId` varchar(36) NOT NULL,
  `TypeOfWork` varchar(5) NOT NULL,
  `OtherWork` varchar(350) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRWorkAssignedByHeadMasterId`),
  KEY `FK_VTRWorkAssignedByHeadMasters_VTDailyReporting_idx` (`VTDailyReportingId`),
  CONSTRAINT `FK_VTRWorkAssignedByHeadMasters_VTDailyReporting` FOREIGN KEY (`VTDailyReportingId`) REFERENCES `vtdailyreporting` (`VTDailyReportingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtrworkassignedbyheadmasters`
--

LOCK TABLES `vtrworkassignedbyheadmasters` WRITE;
/*!40000 ALTER TABLE `vtrworkassignedbyheadmasters` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtrworkassignedbyheadmasters` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtrworkingdaytypes`
--

DROP TABLE IF EXISTS `vtrworkingdaytypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtrworkingdaytypes` (
  `VTRWorkingDayTypeId` varchar(36) NOT NULL,
  `VTDailyReportingId` varchar(36) NOT NULL,
  `WorkingTypeId` varchar(5) NOT NULL,
  `Remarks` varchar(350) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTRWorkingDayTypeId`),
  KEY `FK_VTRWorkingDayTypes_WorkingTypes_idx` (`WorkingTypeId`),
  KEY `FK_VTRHolidays_VTDailyReporting_idx` (`VTDailyReportingId`),
  CONSTRAINT `FK_VTRWorkingDayTypes_VTDailyReporting` FOREIGN KEY (`VTDailyReportingId`) REFERENCES `vtdailyreporting` (`VTDailyReportingId`),
  CONSTRAINT `FK_VTRWorkingDayTypes_WorkingTypes` FOREIGN KEY (`WorkingTypeId`) REFERENCES `datavalues` (`DataValueId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtrworkingdaytypes`
--

LOCK TABLES `vtrworkingdaytypes` WRITE;
/*!40000 ALTER TABLE `vtrworkingdaytypes` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtrworkingdaytypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtschoolclasses`
--

DROP TABLE IF EXISTS `vtschoolclasses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtschoolclasses` (
  `VTSchoolClassId` varchar(36) NOT NULL,
  `VTId` varchar(36) NOT NULL,
  `SchoolId` varchar(36) NOT NULL,
  `ClassId` varchar(36) NOT NULL,
  `SectionId` varchar(36) NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTSchoolClassId`),
  KEY `FK_VTSchoolClasses_VocationalTrainers_idx` (`VTId`),
  KEY `FK_VTSchoolClasses_Schools_idx` (`SchoolId`),
  KEY `FK_VTSchoolClasses_Classes_idx` (`ClassId`),
  CONSTRAINT `FK_VTSchoolClasses_Classes` FOREIGN KEY (`ClassId`) REFERENCES `schoolclasses` (`ClassId`),
  CONSTRAINT `FK_VTSchoolClasses_Schools` FOREIGN KEY (`SchoolId`) REFERENCES `schools` (`SchoolId`),
  CONSTRAINT `FK_VTSchoolClasses_VocationalTrainers` FOREIGN KEY (`VTId`) REFERENCES `vocationaltrainers` (`VTId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtschoolclasses`
--

LOCK TABLES `vtschoolclasses` WRITE;
/*!40000 ALTER TABLE `vtschoolclasses` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtschoolclasses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtschoolsectorjobroles`
--

DROP TABLE IF EXISTS `vtschoolsectorjobroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtschoolsectorjobroles` (
  `VTSchoolSectorJobRoleId` varchar(36) NOT NULL,
  `VTSchoolSectorId` varchar(36) NOT NULL,
  `JobRoleId` varchar(36) NOT NULL,
  `Remarks` varchar(35) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTSchoolSectorJobRoleId`),
  KEY `FK_VTSchoolSectorJobRoles_VTSchoolSectors_idx` (`VTSchoolSectorId`),
  CONSTRAINT `FK_VTSchoolSectorJobRoles_VTSchoolSectors` FOREIGN KEY (`VTSchoolSectorId`) REFERENCES `vtschoolsectors` (`VTSchoolSectorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtschoolsectorjobroles`
--

LOCK TABLES `vtschoolsectorjobroles` WRITE;
/*!40000 ALTER TABLE `vtschoolsectorjobroles` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtschoolsectorjobroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtschoolsectors`
--

DROP TABLE IF EXISTS `vtschoolsectors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtschoolsectors` (
  `VTSchoolSectorId` varchar(36) NOT NULL,
  `AcademicYearId` varchar(36) NOT NULL,
  `VTId` varchar(36) NOT NULL,
  `SchoolId` varchar(36) NOT NULL,
  `SectorId` varchar(36) NOT NULL,
  `JobRoleId` varchar(36) DEFAULT NULL,
  `DateOfAllocation` datetime NOT NULL,
  `DateOfRemoval` datetime DEFAULT NULL,
  `IsAYRollover` bit(1) DEFAULT (0),
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTSchoolSectorId`),
  UNIQUE KEY `UC_VTSchoolSectors` (`AcademicYearId`,`SchoolId`,`SectorId`,`VTId`,`IsActive`),
  UNIQUE KEY `UC_VTSchoolSectorJobRoles` (`AcademicYearId`,`SchoolId`,`SectorId`,`JobRoleId`,`VTId`,`IsActive`),
  KEY `FK_VTSchoolSectors_AcademicYears` (`AcademicYearId`),
  KEY `FK_VTSchoolSectors_Schools` (`SchoolId`),
  KEY `FK_VTSchoolSectors_Sectors` (`SectorId`),
  KEY `FK_VTSchoolSectors_VocationalTrainers` (`VTId`),
  CONSTRAINT `FK_VTSchoolSectors_AcademicYears` FOREIGN KEY (`AcademicYearId`) REFERENCES `academicyears` (`AcademicYearId`),
  CONSTRAINT `FK_VTSchoolSectors_Schools` FOREIGN KEY (`SchoolId`) REFERENCES `schools` (`SchoolId`),
  CONSTRAINT `FK_VTSchoolSectors_Sectors` FOREIGN KEY (`SectorId`) REFERENCES `sectors` (`SectorId`),
  CONSTRAINT `FK_VTSchoolSectors_VocationalTrainers` FOREIGN KEY (`VTId`) REFERENCES `vocationaltrainers` (`VTId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtschoolsectors`
--

LOCK TABLES `vtschoolsectors` WRITE;
/*!40000 ALTER TABLE `vtschoolsectors` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtschoolsectors` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtstatusofinductioninservicetraining`
--

DROP TABLE IF EXISTS `vtstatusofinductioninservicetraining`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtstatusofinductioninservicetraining` (
  `VTStatusOfInductionInserviceTrainingId` varchar(36) NOT NULL,
  `VTSchoolSectorId` varchar(36) DEFAULT NULL,
  `IndustryTrainingStatus` varchar(50) DEFAULT NULL,
  `InserviceTrainingStatus` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTStatusOfInductionInserviceTrainingId`),
  KEY `FK_VTStatusOfInductionInserviceTraining_VTSchoolSectors` (`VTSchoolSectorId`),
  CONSTRAINT `FK_VTStatusOfInductionInserviceTraining_VTSchoolSectors` FOREIGN KEY (`VTSchoolSectorId`) REFERENCES `vtschoolsectors` (`VTSchoolSectorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtstatusofinductioninservicetraining`
--

LOCK TABLES `vtstatusofinductioninservicetraining` WRITE;
/*!40000 ALTER TABLE `vtstatusofinductioninservicetraining` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtstatusofinductioninservicetraining` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtstudentassessments`
--

DROP TABLE IF EXISTS `vtstudentassessments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtstudentassessments` (
  `VTStudentAssessmentId` varchar(36) NOT NULL,
  `VTClassId` varchar(36) DEFAULT NULL,
  `TestimonialType` varchar(50) NOT NULL,
  `StudentName` varchar(100) NOT NULL,
  `StudentGender` varchar(10) NOT NULL,
  `StudentPhoto` varchar(200) DEFAULT NULL,
  `OJTCompany` varchar(150) DEFAULT NULL,
  `OJTCompanyAddress` varchar(350) DEFAULT NULL,
  `OJTFieldSuperName` varchar(150) DEFAULT NULL,
  `OJTFieldSuperMobile` varchar(15) DEFAULT NULL,
  `OJTFieldSuperEmail` varchar(100) DEFAULT NULL,
  `GroupPhoto` varchar(200) DEFAULT NULL,
  `TestimonialTitle` varchar(150) DEFAULT NULL,
  `TestimonialDetails` varchar(250) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTStudentAssessmentId`),
  KEY `FK_VTStudentAssessment_VTClasses` (`VTClassId`),
  CONSTRAINT `FK_VTStudentAssessment_VTClasses` FOREIGN KEY (`VTClassId`) REFERENCES `vtclasses` (`VTClassId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtstudentassessments`
--

LOCK TABLES `vtstudentassessments` WRITE;
/*!40000 ALTER TABLE `vtstudentassessments` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtstudentassessments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtstudentplacementdetails`
--

DROP TABLE IF EXISTS `vtstudentplacementdetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtstudentplacementdetails` (
  `VTStudentPlacementDetailId` varchar(36) NOT NULL,
  `VTClassId` varchar(36) DEFAULT NULL,
  `StudentId` varchar(36) DEFAULT NULL,
  `PlacementApplyStatus` varchar(50) NOT NULL,
  `PlacementStatus` varchar(50) NOT NULL,
  `ApprenticeshipApplyStatus` varchar(50) DEFAULT NULL,
  `ApprenticeshipStatus` varchar(50) DEFAULT NULL,
  `HigherEducationVE` varchar(50) DEFAULT NULL,
  `HigherEductaionOther` varchar(50) DEFAULT NULL,
  `StudentPlacementStatus` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTStudentPlacementDetailId`),
  KEY `FK_VTStudentPlacementDetails_StudentClasses` (`StudentId`),
  KEY `FK_VTStudentPlacementDetails_VTClasses` (`VTClassId`),
  CONSTRAINT `FK_VTStudentPlacementDetails_StudentClasses` FOREIGN KEY (`StudentId`) REFERENCES `studentclasses` (`StudentId`),
  CONSTRAINT `FK_VTStudentPlacementDetails_VTClasses` FOREIGN KEY (`VTClassId`) REFERENCES `vtclasses` (`VTClassId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtstudentplacementdetails`
--

LOCK TABLES `vtstudentplacementdetails` WRITE;
/*!40000 ALTER TABLE `vtstudentplacementdetails` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtstudentplacementdetails` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtstudentresultothersubjects`
--

DROP TABLE IF EXISTS `vtstudentresultothersubjects`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtstudentresultothersubjects` (
  `VTStudentResultOtherSubjectId` varchar(36) NOT NULL,
  `VTClassId` varchar(36) DEFAULT NULL,
  `StudentId` varchar(36) DEFAULT NULL,
  `SubjectName` varchar(150) NOT NULL,
  `SubjectNumber` int NOT NULL,
  `SubjectMarks` int NOT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTStudentResultOtherSubjectId`),
  KEY `FK_VTStudentResultsOtherSubjects_StudentClasses` (`StudentId`),
  KEY `FK_VTStudentResultsOtherSubjects_VTClasses` (`VTClassId`),
  CONSTRAINT `FK_VTStudentResultsOtherSubjects_StudentClasses` FOREIGN KEY (`StudentId`) REFERENCES `studentclasses` (`StudentId`),
  CONSTRAINT `FK_VTStudentResultsOtherSubjects_VTClasses` FOREIGN KEY (`VTClassId`) REFERENCES `vtclasses` (`VTClassId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtstudentresultothersubjects`
--

LOCK TABLES `vtstudentresultothersubjects` WRITE;
/*!40000 ALTER TABLE `vtstudentresultothersubjects` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtstudentresultothersubjects` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vtstudentveresults`
--

DROP TABLE IF EXISTS `vtstudentveresults`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vtstudentveresults` (
  `VTStudentVEResultId` varchar(36) NOT NULL,
  `VTClassId` varchar(36) DEFAULT NULL,
  `StudentId` varchar(36) DEFAULT NULL,
  `DateIssuence` datetime DEFAULT NULL,
  `ExternalMarks` int NOT NULL,
  `TheoryMarks` int NOT NULL,
  `InternalMarks` int NOT NULL,
  `TotalMarks` int NOT NULL,
  `Grade` varchar(50) DEFAULT NULL,
  `CreatedBy` varchar(30) NOT NULL,
  `CreatedOn` datetime(3) NOT NULL,
  `UpdatedBy` varchar(30) DEFAULT NULL,
  `UpdatedOn` datetime(3) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`VTStudentVEResultId`),
  KEY `FK_VTStudentVEResults_StudentClasses` (`StudentId`),
  KEY `FK_VTStudentVEResults_VTClasses` (`VTClassId`),
  CONSTRAINT `FK_VTStudentVEResults_StudentClasses` FOREIGN KEY (`StudentId`) REFERENCES `studentclasses` (`StudentId`),
  CONSTRAINT `FK_VTStudentVEResults_VTClasses` FOREIGN KEY (`VTClassId`) REFERENCES `vtclasses` (`VTClassId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vtstudentveresults`
--

LOCK TABLES `vtstudentveresults` WRITE;
/*!40000 ALTER TABLE `vtstudentveresults` DISABLE KEYS */;
/*!40000 ALTER TABLE `vtstudentveresults` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `vtvc_issue_reporting_for_dashboard`
--

DROP TABLE IF EXISTS `vtvc_issue_reporting_for_dashboard`;
/*!50001 DROP VIEW IF EXISTS `vtvc_issue_reporting_for_dashboard`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vtvc_issue_reporting_for_dashboard` AS SELECT 
 1 AS `MainIssueId`,
 1 AS `SubIssueId`,
 1 AS `VCId`,
 1 AS `Type`,
 1 AS `Name`,
 1 AS `IssuePriority`,
 1 AS `IssueReportDate`,
 1 AS `VCIssueReportingId`,
 1 AS `ApprovalStatus`,
 1 AS `SubIssueName`,
 1 AS `IsActive`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `vwvocationaltrainerdetails`
--

DROP TABLE IF EXISTS `vwvocationaltrainerdetails`;
/*!50001 DROP VIEW IF EXISTS `vwvocationaltrainerdetails`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `vwvocationaltrainerdetails` AS SELECT 
 1 AS `AcademicYearId`,
 1 AS `VTSchoolSectorId`,
 1 AS `VTPId`,
 1 AS `VCId`,
 1 AS `VTId`,
 1 AS `VTPName`,
 1 AS `VCName`,
 1 AS `VCMobile`,
 1 AS `VCEmail`,
 1 AS `VTName`,
 1 AS `VTMobile`,
 1 AS `VTEmail`,
 1 AS `VTDateOfJoining`,
 1 AS `HMName`,
 1 AS `HMMobile`,
 1 AS `HMEmail`,
 1 AS `SectorName`,
 1 AS `JobRoleName`,
 1 AS `UDISE`,
 1 AS `SchoolName`*/;
SET character_set_client = @saved_cs_client;

--
-- Final view structure for view `schoolsbyvtpsectorglfv`
--

/*!50001 DROP VIEW IF EXISTS `schoolsbyvtpsectorglfv`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `schoolsbyvtpsectorglfv` AS select distinct if(((`s`.`IsActive` = 1) and (`s`.`IsImplemented` = 1)),`s`.`SchoolId`,NULL) AS `ImplementedSchoolId`,if((`s`.`IsActive` = 1),`s`.`SchoolId`,NULL) AS `ApprovedSchoolId`,`svtps`.`AcademicYearId` AS `AcademicYearId`,`svtps`.`VTPId` AS `VTPId`,`svtps`.`SectorId` AS `SectorId`,`vcss`.`VCId` AS `VCId`,`vss`.`VTId` AS `VTId`,`vss`.`VTSchoolSectorId` AS `VTSchoolSectorId` from ((((((((`schoolvtpsectors` `svtps` join `schools` `s` on(((`s`.`SchoolId` = `svtps`.`SchoolId`) and (`s`.`IsActive` = 1)))) join `vocationaltrainingproviders` `vtp` on(((`svtps`.`VTPId` = `vtp`.`VTPId`) and (`vtp`.`IsActive` = 1)))) join `sectors` `se` on(((`svtps`.`SectorId` = `se`.`SectorId`) and (`se`.`IsActive` = 1)))) join `academicyears` `ar` on(((`svtps`.`AcademicYearId` = `ar`.`AcademicYearId`) and (`ar`.`IsActive` = 1)))) left join `vcschoolsectors` `vcss` on(((`svtps`.`SchoolVTPSectorId` = `vcss`.`SchoolVTPSectorId`) and (`vcss`.`IsActive` = 1)))) left join `vocationaltrainers` `vt` on(((`vcss`.`VCId` = `vt`.`VCId`) and (`vt`.`IsActive` = 1)))) left join `vtschoolsectors` `vss` on(((`svtps`.`AcademicYearId` = `vss`.`AcademicYearId`) and (`svtps`.`SchoolId` = `vss`.`SchoolId`) and (`svtps`.`SectorId` = `vss`.`SectorId`) and (`vss`.`IsActive` = 1)))) left join `vtclasses` `vtc` on(((`vss`.`AcademicYearId` = `vtc`.`AcademicYearId`) and (`s`.`SchoolId` = `vtc`.`SchoolId`) and (`vss`.`VTId` = `vtc`.`VTId`) and (`vtc`.`IsActive` = 1)))) where (`svtps`.`IsActive` = 1) order by `svtps`.`VTPId`,`svtps`.`SectorId` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `schoolsbyvtpsectorinfo`
--

/*!50001 DROP VIEW IF EXISTS `schoolsbyvtpsectorinfo`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`igmiteadm`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `schoolsbyvtpsectorinfo` AS select distinct if(((`s`.`IsActive` = 1) and (`s`.`IsImplemented` = 1)),`s`.`SchoolId`,NULL) AS `ImplementedSchoolId`,if((`s`.`IsActive` = 1),`s`.`SchoolId`,NULL) AS `ApprovedSchoolId`,`svs`.`AcademicYearId` AS `AcademicYearId`,`svs`.`VTPId` AS `VTPId`,`svs`.`SectorId` AS `SectorId`,`vcss`.`VCId` AS `VCId`,`vtss`.`VTId` AS `VTId`,`vtss`.`VTSchoolSectorId` AS `VTSchoolSectorId`,`vtc`.`VTClassId` AS `VTClassId`,`s`.`DivisionId` AS `DivisionId`,`s`.`DistrictCode` AS `DistrictId` from (((((((((((`schoolvtpsectors` `svs` join `schools` `s` on(((`svs`.`SchoolId` = `s`.`SchoolId`) and (`s`.`IsActive` = 1)))) join `vtpacademicyearsmap` `vtpm` on(((`svs`.`AcademicYearId` = `vtpm`.`AcademicYearId`) and (`svs`.`VTPId` = `vtpm`.`VTPId`) and (`vtpm`.`IsActive` = 1)))) join `vocationaltrainingproviders` `vtp` on(((`vtpm`.`VTPId` = `vtp`.`VTPId`) and (`vtp`.`IsActive` = 1)))) join `sectors` `se` on(((`svs`.`SectorId` = `se`.`SectorId`) and (`se`.`IsActive` = 1)))) join `academicyears` `ay` on(((`svs`.`AcademicYearId` = `ay`.`AcademicYearId`) and (`ay`.`IsActive` = 1)))) left join `vcschoolsectors` `vcss` on(((`svs`.`AcademicYearId` = `vcss`.`AcademicYearId`) and (`svs`.`SchoolVTPSectorId` = `vcss`.`SchoolVTPSectorId`) and (`vcss`.`IsActive` = 1)))) left join `vtpcoordinatorsmap` `vcm` on(((`vcss`.`AcademicYearId` = `vcm`.`AcademicYearId`) and (`vcss`.`VCId` = `vcm`.`VCId`) and (`vcm`.`IsActive` = 1)))) left join `vtschoolsectors` `vtss` on(((`svs`.`AcademicYearId` = `vtss`.`AcademicYearId`) and (`svs`.`SchoolId` = `vtss`.`SchoolId`) and (`svs`.`SectorId` = `vtss`.`SectorId`) and (`vtss`.`IsActive` = 1)))) left join `vtclasses` `vtc` on(((`vtss`.`AcademicYearId` = `vtc`.`AcademicYearId`) and (`s`.`SchoolId` = `vtc`.`SchoolId`) and (`vtss`.`VTId` = `vtc`.`VTId`) and (`vtc`.`IsActive` = 1)))) left join `vctrainersmap` `vtm` on(((`vcm`.`AcademicYearId` = `vtm`.`AcademicYearId`) and (`vcm`.`VTPId` = `vtm`.`VTPId`) and (`vcm`.`VCId` = `vtm`.`VCId`) and (`vtm`.`IsActive` = 1)))) left join `vocationaltrainers` `vt` on(((`vtm`.`VTId` = `vt`.`VTId`) and (`vt`.`IsActive` = 1)))) where (`svs`.`IsActive` = 1) order by `svs`.`AcademicYearId`,`svs`.`SectorId`,`svs`.`VTPId`,`svs`.`SchoolId` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vtvc_issue_reporting_for_dashboard`
--

/*!50001 DROP VIEW IF EXISTS `vtvc_issue_reporting_for_dashboard`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`igmiteadm`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `vtvc_issue_reporting_for_dashboard` AS select distinct `vcis`.`MainIssue` AS `MainIssueId`,`vcis`.`SubIssue` AS `SubIssueId`,`vcis`.`VCId` AS `VCId`,'VC' AS `Type`,`mi`.`Description` AS `Name`,`si`.`IssuePriority` AS `IssuePriority`,`vcis`.`IssueReportDate` AS `IssueReportDate`,`vcis`.`VCIssueReportingId` AS `VCIssueReportingId`,`vcis`.`ApprovalStatus` AS `ApprovalStatus`,`si`.`IssueName` AS `SubIssueName`,`vcis`.`IsActive` AS `IsActive` from ((`vcissuereporting` `vcis` join `mainissues` `mi` on((`vcis`.`MainIssue` = `mi`.`MainIssueId`))) join `subissues` `si` on(((`mi`.`MainIssueId` = `si`.`MainIssueId`) and (`vcis`.`SubIssue` = `si`.`SubIssueId`)))) union select distinct `vtis`.`MainIssue` AS `MainIssueId`,`vtis`.`SubIssue` AS `SubIssueId`,`vtis`.`VTId` AS `VCVTID`,'VT' AS `Type`,`mi`.`Description` AS `Name`,`si`.`IssuePriority` AS `IssuePriority`,`vtis`.`IssueReportDate` AS `IssueReportDate`,`vtis`.`VTIssueReportingId` AS `VTIssueReportingId`,`vtis`.`ApprovalStatus` AS `ApprovalStatus`,`si`.`IssueName` AS `SubIssueName`,`vtis`.`IsActive` AS `IsActive` from ((`vtissuereporting` `vtis` join `mainissues` `mi` on((`vtis`.`MainIssue` = `mi`.`MainIssueId`))) join `subissues` `si` on(((`mi`.`MainIssueId` = `si`.`MainIssueId`) and (`vtis`.`SubIssue` = `si`.`SubIssueId`)))) union select distinct `hmis`.`MainIssue` AS `MainIssueId`,`hmis`.`SubIssue` AS `SubIssueId`,`hmis`.`HMId` AS `VCVTID`,'HM' AS `Type`,`mi`.`Description` AS `Name`,`si`.`IssuePriority` AS `IssuePriority`,`hmis`.`IssueReportDate` AS `IssueReportDate`,`hmis`.`HMIssueReportingId` AS `HMIssueReportingId`,`hmis`.`ApprovalStatus` AS `ApprovalStatus`,`si`.`IssueName` AS `SubIssueName`,`hmis`.`IsActive` AS `IsActive` from ((`hmissuereporting` `hmis` join `mainissues` `mi` on((`hmis`.`MainIssue` = `mi`.`MainIssueId`))) join `subissues` `si` on(((`mi`.`MainIssueId` = `si`.`MainIssueId`) and (`hmis`.`SubIssue` = `si`.`SubIssueId`)))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `vwvocationaltrainerdetails`
--

/*!50001 DROP VIEW IF EXISTS `vwvocationaltrainerdetails`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`igmiteadm`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `vwvocationaltrainerdetails` AS select distinct `vss`.`AcademicYearId` AS `AcademicYearId`,`vss`.`VTSchoolSectorId` AS `VTSchoolSectorId`,`vtp`.`VTPId` AS `VTPId`,`vc`.`VCId` AS `VCId`,`vss`.`VTId` AS `VTId`,`vtp`.`VTPName` AS `VTPName`,`vc`.`FullName` AS `VCName`,`vc`.`Mobile` AS `VCMobile`,`vc`.`EmailId` AS `VCEmail`,`vt`.`FullName` AS `VTName`,`vt`.`Mobile` AS `VTMobile`,`vt`.`Email` AS `VTEmail`,`vtm`.`DateOfJoining` AS `VTDateOfJoining`,`hm`.`FullName` AS `HMName`,`hm`.`Mobile` AS `HMMobile`,`hm`.`Email` AS `HMEmail`,`se`.`SectorName` AS `SectorName`,`jr`.`JobRoleName` AS `JobRoleName`,`s`.`UDISE` AS `UDISE`,`s`.`SchoolName` AS `SchoolName` from (((((((((((`vtschoolsectors` `vss` join `vctrainersmap` `vtm` on(((`vss`.`AcademicYearId` = `vtm`.`AcademicYearId`) and (`vss`.`VTId` = `vtm`.`VTId`) and (`vtm`.`IsActive` = 1)))) join `vtpcoordinatorsmap` `vcm` on(((`vtm`.`AcademicYearId` = `vcm`.`AcademicYearId`) and (`vtm`.`VCId` = `vcm`.`VCId`) and (`vcm`.`IsActive` = 1)))) join `vtpacademicyearsmap` `vtpm` on(((`vcm`.`AcademicYearId` = `vtpm`.`AcademicYearId`) and (`vcm`.`VTPId` = `vtpm`.`VTPId`) and (`vtpm`.`IsActive` = 1)))) join `vocationaltrainers` `vt` on(((`vtm`.`VTId` = `vt`.`VTId`) and (`vt`.`IsActive` = 1)))) join `vocationalcoordinators` `vc` on(((`vcm`.`VCId` = `vc`.`VCId`) and (`vc`.`IsActive` = 1)))) join `vocationaltrainingproviders` `vtp` on(((`vtpm`.`VTPId` = `vtp`.`VTPId`) and (`vtp`.`IsActive` = 1)))) join `schools` `s` on(((`vss`.`SchoolId` = `s`.`SchoolId`) and (`s`.`IsActive` = 1)))) join `sectors` `se` on(((`vss`.`SectorId` = `se`.`SectorId`) and (`se`.`IsActive` = 1)))) join `jobroles` `jr` on(((`vss`.`SectorId` = `jr`.`SectorId`) and (`vss`.`JobRoleId` = `jr`.`JobRoleId`) and (`jr`.`IsActive` = 1)))) left join `hmschoolsmap` `hsm` on(((`vss`.`AcademicYearId` = `hsm`.`AcademicYearId`) and (`vss`.`SchoolId` = `hsm`.`SchoolId`) and (`hsm`.`IsActive` = 1)))) left join `headmasters` `hm` on(((`hsm`.`HMId` = `hm`.`HMId`) and (`hm`.`IsActive` = 1)))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-05-12 19:18:47
