-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               8.0.32 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Version:             12.3.0.6589
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for library-management-system
CREATE DATABASE IF NOT EXISTS `library-management-system` /*!40100 DEFAULT CHARACTER SET utf8mb3 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `library-management-system`;

-- Dumping structure for table library-management-system.authors
CREATE TABLE IF NOT EXISTS `authors` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8mb3;

-- Dumping data for table library-management-system.authors: ~14 rows (approximately)
INSERT INTO `authors` (`id`, `name`) VALUES
	(1, 'marartin wickramasinghe'),
	(2, 'Philip K'),
	(22, 'Celeste Ng'),
	(23, 'John Bunyan'),
	(24, 'Daniel Defoe'),
	(25, 'Jonathan Swift'),
	(26, 'Samuel Richardson'),
	(27, 'Henry Fielding'),
	(28, 'Chathura'),
	(29, 'Sudaraka'),
	(30, 'yfuyjfyyu'),
	(31, 'gkhjhj'),
	(32, 'Dimuthu'),
	(33, 'ABc');

-- Dumping structure for table library-management-system.books
CREATE TABLE IF NOT EXISTS `books` (
  `id` int NOT NULL AUTO_INCREMENT,
  `titles_id` int NOT NULL,
  `authors_id` int NOT NULL,
  `IsAvailable` tinyint NOT NULL DEFAULT '0',
  `IsDeleted` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `fk_books_titles1_idx` (`titles_id`),
  KEY `fk_books_authors1_idx` (`authors_id`),
  CONSTRAINT `fk_books_authors1` FOREIGN KEY (`authors_id`) REFERENCES `authors` (`id`),
  CONSTRAINT `fk_books_titles1` FOREIGN KEY (`titles_id`) REFERENCES `titles` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb3;

-- Dumping data for table library-management-system.books: ~17 rows (approximately)
INSERT INTO `books` (`id`, `titles_id`, `authors_id`, `IsAvailable`, `IsDeleted`) VALUES
	(1, 1, 1, 2, 1),
	(15, 1, 1, 3, 1),
	(16, 23, 22, 3, 1),
	(17, 22, 2, 5, 1),
	(18, 24, 23, 10, 1),
	(19, 25, 24, 4, 0),
	(20, 26, 25, 9, 1),
	(21, 27, 26, 7, 0),
	(22, 28, 27, 5, 1),
	(23, 29, 28, 1, 0),
	(24, 30, 29, 1, 0),
	(25, 30, 29, 1, 1),
	(26, 31, 30, 1, 1),
	(27, 32, 31, 1, 1),
	(28, 33, 32, 1, 0),
	(29, 34, 28, 1, 0),
	(30, 35, 33, 1, 0);

-- Dumping structure for table library-management-system.borrowed_books
CREATE TABLE IF NOT EXISTS `borrowed_books` (
  `id` int NOT NULL AUTO_INCREMENT,
  `borrowed_date` date NOT NULL,
  `is_returned` tinyint NOT NULL,
  `users_id` int NOT NULL,
  `books_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_borrowed_books_users1_idx` (`users_id`),
  KEY `fk_borrowed_books_books1_idx` (`books_id`),
  CONSTRAINT `fk_borrowed_books_books1` FOREIGN KEY (`books_id`) REFERENCES `books` (`id`),
  CONSTRAINT `fk_borrowed_books_users1` FOREIGN KEY (`users_id`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb3;

-- Dumping data for table library-management-system.borrowed_books: ~0 rows (approximately)
INSERT INTO `borrowed_books` (`id`, `borrowed_date`, `is_returned`, `users_id`, `books_id`) VALUES
	(1, '2023-06-30', 2, 1, 1);

-- Dumping structure for table library-management-system.cities
CREATE TABLE IF NOT EXISTS `cities` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;

-- Dumping data for table library-management-system.cities: ~4 rows (approximately)
INSERT INTO `cities` (`id`, `name`) VALUES
	(1, 'Kandy'),
	(2, 'Colombo'),
	(3, 'kiribathgoda'),
	(4, 'Mabima');

-- Dumping structure for table library-management-system.genders
CREATE TABLE IF NOT EXISTS `genders` (
  `id` int NOT NULL AUTO_INCREMENT,
  `type` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;

-- Dumping data for table library-management-system.genders: ~2 rows (approximately)
INSERT INTO `genders` (`id`, `type`) VALUES
	(1, 'Male'),
	(2, 'Female');

-- Dumping structure for table library-management-system.roles
CREATE TABLE IF NOT EXISTS `roles` (
  `id` int NOT NULL AUTO_INCREMENT,
  `type` varchar(10) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;

-- Dumping data for table library-management-system.roles: ~2 rows (approximately)
INSERT INTO `roles` (`id`, `type`) VALUES
	(1, 'student'),
	(2, 'lecturer'),
	(3, 'admin');

-- Dumping structure for table library-management-system.titles
CREATE TABLE IF NOT EXISTS `titles` (
  `id` int NOT NULL AUTO_INCREMENT,
  `title` varchar(100) NOT NULL,
  `copies` int NOT NULL DEFAULT '0',
  `borrowings` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8mb3;

-- Dumping data for table library-management-system.titles: ~15 rows (approximately)
INSERT INTO `titles` (`id`, `title`, `copies`, `borrowings`) VALUES
	(1, 'aba yaluwo', 2, 1),
	(22, ' Do Androids Dream of Electric Sheep', 5, 0),
	(23, ' Everything I Never Told You', 0, 0),
	(24, ' The Pilgrim’s Progress', 0, 0),
	(25, 'Robinson Crusoe', 0, 0),
	(26, ' Gulliver’s Travels', 0, 0),
	(27, 'Clarissa', 0, 0),
	(28, ' Tom Jones', 0, 0),
	(29, 'C# (Sinhala) ', 0, 0),
	(30, 'C++', 0, 0),
	(31, '78y8o87yo8', 0, 0),
	(32, 'gfhfhf', 0, 0),
	(33, 'ABC', 0, 0),
	(34, 'java', 0, 0),
	(35, 'Dimuthu', 0, 0);

-- Dumping structure for table library-management-system.users
CREATE TABLE IF NOT EXISTS `users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `email` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `mobile` varchar(10) NOT NULL,
  `address` varchar(100) NOT NULL,
  `role_id` int NOT NULL,
  `gender_id` int NOT NULL,
  `city_id` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_users_roles_idx` (`role_id`),
  KEY `fk_users_genders1_idx` (`gender_id`),
  KEY `fk_users_cities1_idx` (`city_id`),
  CONSTRAINT `fk_users_cities1` FOREIGN KEY (`city_id`) REFERENCES `cities` (`id`),
  CONSTRAINT `fk_users_genders1` FOREIGN KEY (`gender_id`) REFERENCES `genders` (`id`),
  CONSTRAINT `fk_users_roles` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb3;

-- Dumping data for table library-management-system.users: ~8 rows (approximately)
INSERT INTO `users` (`id`, `name`, `email`, `password`, `mobile`, `address`, `role_id`, `gender_id`, `city_id`) VALUES
	(1, 'Samarawickrama', 'Chathura@gmail.com', '123', '0718457935', '224/12,Mabima', 3, 1, 4),
	(4, 'Dimuthu Dilanga', 'dimuthudilanga2005@gmail.com', 'Dimuthu0000', '0704358933', ' 42/2,parakandeniya imbulgoda', 1, 1, 3),
	(7, 'Nelith', 'Nelith@123', '123', '0705321516', 'u6i6riufiyubgfuy', 3, 1, 4),
	(11, 'danul', 'aahsa', '2005', '077655', ' sasa', 2, 1, 2),
	(12, 'tharani', 'qwqw', 'tharani', '6867', ' qw', 3, 2, 2),
	(13, 'Yenuka', 'Yenuka@gmail.com', '123', '7777777777', ' Yenuka@gmail.com', 1, 1, 2),
	(14, 'Tharindu', 'Tharindu@gmail.com', 'nelith0814@', '22222222', ' agaagd', 1, 1, 4),
	(15, 'jkj', 'hgfhgfhg', '555', '000', ' kgiugug', 1, 1, 1),
	(16, 'ukhuikhuk', 'jkkj', 'jhjhbjhb', '0000', 'ghgfhgf', 1, 1, 2);

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
