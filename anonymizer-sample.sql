CREATE DATABASE  IF NOT EXISTS `identifeye_test` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `identifeye_test`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: identifeye_test
-- ------------------------------------------------------
-- Server version	5.7.20-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `bans`
--

DROP TABLE IF EXISTS `bans`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `bans` (
  `BanId` bigint(20) NOT NULL AUTO_INCREMENT,
  `Type` int(11) DEFAULT NULL,
  `CIDRMask` int(11) DEFAULT NULL,
  `Username` varchar(64) COLLATE utf8mb4_bin DEFAULT NULL,
  `CharacterName` varchar(20) COLLATE utf8mb4_bin DEFAULT NULL,
  `IP` varchar(64) COLLATE utf8mb4_bin DEFAULT NULL,
  `UUID` varchar(128) COLLATE utf8mb4_bin DEFAULT NULL,
  `Issued` bigint(20) DEFAULT NULL,
  `Expiry` bigint(20) DEFAULT NULL,
  `Issuer` varchar(64) COLLATE utf8mb4_bin DEFAULT NULL,
  `Reason` varchar(512) COLLATE utf8mb4_bin DEFAULT NULL,
  `UnbanType` int(11) DEFAULT NULL,
  `UnbanDate` bigint(20) DEFAULT NULL,
  `UnbanIssuer` varchar(64) COLLATE utf8mb4_bin DEFAULT NULL,
  `UnbanReason` varchar(512) COLLATE utf8mb4_bin DEFAULT NULL,
  `Parent` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`BanId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bans`
--

LOCK TABLES `bans` WRITE;
/*!40000 ALTER TABLE `bans` DISABLE KEYS */;
INSERT INTO `bans` VALUES (1,NULL,NULL,'Test',NULL,NULL,NULL,NULL,1549641943,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(2,NULL,NULL,'Test',NULL,NULL,NULL,NULL,1640643075,NULL,NULL,NULL,1549641943,NULL,NULL,NULL);
/*!40000 ALTER TABLE `bans` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `knownips`
--

DROP TABLE IF EXISTS `knownips`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `knownips` (
  `Username` varchar(64) DEFAULT NULL,
  `IP` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `knownips`
--

LOCK TABLES `knownips` WRITE;
/*!40000 ALTER TABLE `knownips` DISABLE KEYS */;
INSERT INTO `knownips` VALUES ('Test','192.192.192.192'),('Test','193.192.192.192');
/*!40000 ALTER TABLE `knownips` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `Username` varchar(64) DEFAULT NULL,
  `UUID` varchar(100) DEFAULT NULL,
  `ActivePlaytime` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES ('Test','ABCDE',4),('testy','ACC',10);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-04-23 15:00:41
