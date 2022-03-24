CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Bookers` (
    `BookerId` int NOT NULL AUTO_INCREMENT,
    `CardId` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `UserName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `BookerPwd` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `FirstName` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `LastName` longtext CHARACTER SET utf8mb4 NULL,
    `Gender` int NOT NULL,
    `PhoneNum` varchar(11) CHARACTER SET utf8mb4 NULL,
    `TimeOfRegister` datetime(6) NOT NULL,
    `DateOfBirth` datetime(6) NOT NULL,
    `IsDeleted` tinyint(10) NOT NULL,
    CONSTRAINT `PK_Bookers` PRIMARY KEY (`BookerId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Lines` (
    `LineId` int NOT NULL AUTO_INCREMENT,
    `StartTerminal` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `EndTerminal` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `StopStation` varchar(500) CHARACTER SET utf8mb4 NOT NULL,
    `TrainName` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Lines` PRIMARY KEY (`LineId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Orders` (
    `OrderId` char(36) COLLATE ascii_general_ci NOT NULL,
    `BookerId` int NOT NULL,
    `TrainId` int NOT NULL,
    `StartTerminalId` int NOT NULL,
    `EndTerminalId` int NOT NULL,
    `StartTerminal` longtext CHARACTER SET utf8mb4 NOT NULL,
    `EndTerminal` longtext CHARACTER SET utf8mb4 NOT NULL,
    `TrainName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `Price` double NOT NULL,
    CONSTRAINT `PK_Orders` PRIMARY KEY (`OrderId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Stations` (
    `StationId` int NOT NULL AUTO_INCREMENT,
    `StationName` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `IsTerminal` tinyint(1) NOT NULL DEFAULT FALSE,
    `Latitude` double NOT NULL,
    `Longitude` double NOT NULL,
    CONSTRAINT `PK_Stations` PRIMARY KEY (`StationId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Trains` (
    `TrainId` int NOT NULL AUTO_INCREMENT,
    `LineId` int NOT NULL,
    `TrainName` varchar(5) CHARACTER SET utf8mb4 NOT NULL,
    `TypeOfTrain` varchar(10) CHARACTER SET utf8mb4 NOT NULL,
    `Time` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Trains` PRIMARY KEY (`TrainId`),
    CONSTRAINT `FK_Trains_Lines_LineId` FOREIGN KEY (`LineId`) REFERENCES `Lines` (`LineId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

INSERT INTO `Bookers` (`BookerId`, `BookerPwd`, `CardId`, `DateOfBirth`, `FirstName`, `Gender`, `IsDeleted`, `LastName`, `PhoneNum`, `TimeOfRegister`, `UserName`)
VALUES (1, '123456', '453009200001013710', TIMESTAMP '2000-01-09 00:00:00', '李', 1, False, '黑沙', '13600291522', TIMESTAMP '2022-03-25 03:19:39', '黑沙');

INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`, `TrainName`)
VALUES (1, '哈尔滨站', '广州站', '广州站,武汉站,北京站,哈尔滨站', NULL);
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`, `TrainName`)
VALUES (2, '成都站', '广州站', '广州站,重庆站,成都站', NULL);
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`, `TrainName`)
VALUES (3, '上海站', '广州站', '广州站,武汉站,上海站', NULL);
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`, `TrainName`)
VALUES (4, '哈尔滨站', '上海站', '上海站,北京站,哈尔滨站', NULL);
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`, `TrainName`)
VALUES (5, '成都站', '上海站', '上海站,武汉站,重庆站,成都站', NULL);
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`, `TrainName`)
VALUES (6, '广州站', '上海站', '上海站,武汉站,广州站', NULL);
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`, `TrainName`)
VALUES (7, '广州站', '哈尔滨站', '哈尔滨站,北京站,武汉站,广州站', NULL);
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`, `TrainName`)
VALUES (8, '上海站', '哈尔滨站', '哈尔滨站,北京站,上海站', NULL);
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`, `TrainName`)
VALUES (9, '成都站', '哈尔滨站', '哈尔滨站,北京站,武汉站,重庆站,成都站', NULL);
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`, `TrainName`)
VALUES (10, '广州站', '成都站', '成都站,重庆站,广州站', NULL);
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`, `TrainName`)
VALUES (11, '上海站', '成都站', '成都站,重庆站,武汉站,上海站', NULL);
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`, `TrainName`)
VALUES (12, '哈尔滨站', '成都站', '成都站,重庆站,武汉站,北京站,哈尔滨站', NULL);

INSERT INTO `Stations` (`StationId`, `IsTerminal`, `Latitude`, `Longitude`, `StationName`)
VALUES (1, TRUE, 23.148721999999999, 113.25765199999999, '广州站');

INSERT INTO `Stations` (`StationId`, `Latitude`, `Longitude`, `StationName`)
VALUES (2, 29.549520000000001, 106.547546, '重庆站');
INSERT INTO `Stations` (`StationId`, `Latitude`, `Longitude`, `StationName`)
VALUES (3, 39.904217000000003, 116.427162, '北京站');

INSERT INTO `Stations` (`StationId`, `IsTerminal`, `Latitude`, `Longitude`, `StationName`)
VALUES (4, TRUE, 31.249600999999998, 121.455704, '上海站');
INSERT INTO `Stations` (`StationId`, `IsTerminal`, `Latitude`, `Longitude`, `StationName`)
VALUES (5, TRUE, 30.629023, 104.154915, '成都站');
INSERT INTO `Stations` (`StationId`, `IsTerminal`, `Latitude`, `Longitude`, `StationName`)
VALUES (6, TRUE, 45.761088999999998, 126.631905, '哈尔滨站');

INSERT INTO `Stations` (`StationId`, `Latitude`, `Longitude`, `StationName`)
VALUES (7, 30.607346, 114.42449999999999, '武汉站');

INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (1, 1, '07:41,09:06,15:03,21:01', 'D112', 'D');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (2, 2, '10:16,17:43,19:17', 'D1849', 'D');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (3, 3, '07:50,16:38,04:29(+1)', 'K528', 'K');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (4, 4, '09:33,16:12,21:36', 'G1204', 'G');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (5, 5, '06:32,12:00,18:44,20:42', 'D636', 'D');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (6, 6, '19:15,10:12(+1),17:21(+1)', 'K527', 'K');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (7, 7, '07:41,10:06,15:03,21:01', 'D728', 'D');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (8, 8, '08:52,15:01,21:40', 'G1202', 'G');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (9, 9, '19:15,06:12(+1),17:21(+1),23:50(+1)', 'K518', 'K');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (10, 10, '07:15,15:12,23:21', 'K488', 'K');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (11, 11, '06:10,11:27,15:25,19:27', 'G2195', 'G');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (12, 12, '18:15,05:12(+1),16:21(+1),22:50(+1)', 'K546', 'K');

CREATE INDEX `IX_Trains_LineId` ON `Trains` (`LineId`);

CREATE UNIQUE INDEX `IX_Trains_TrainName` ON `Trains` (`TrainName`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220324191939_update_train_time', '6.0.3');

COMMIT;

