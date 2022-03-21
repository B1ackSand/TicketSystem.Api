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
    `price` double NOT NULL,
    CONSTRAINT `PK_Orders` PRIMARY KEY (`OrderId`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Stations` (
    `StationId` int NOT NULL AUTO_INCREMENT,
    `StationName` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `IsTerminal` tinyint(1) NOT NULL DEFAULT FALSE,
    CONSTRAINT `PK_Stations` PRIMARY KEY (`StationId`),
    CONSTRAINT `AK_Stations_StationName` UNIQUE (`StationName`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Lines` (
    `LineId` int NOT NULL AUTO_INCREMENT,
    `StartTerminal` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `EndTerminal` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `StopStation` varchar(500) CHARACTER SET utf8mb4 NOT NULL,
    `TrainName` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Lines` PRIMARY KEY (`LineId`),
    CONSTRAINT `FK_Lines_Stations_EndTerminal` FOREIGN KEY (`EndTerminal`) REFERENCES `Stations` (`StationName`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Trains` (
    `TrainId` int NOT NULL AUTO_INCREMENT,
    `LineId` int NOT NULL,
    `TrainName` varchar(5) CHARACTER SET utf8mb4 NOT NULL,
    `TypeOfTrain` varchar(10) CHARACTER SET utf8mb4 NOT NULL,
    `Time` time(6) NOT NULL,
    CONSTRAINT `PK_Trains` PRIMARY KEY (`TrainId`),
    CONSTRAINT `FK_Trains_Lines_LineId` FOREIGN KEY (`LineId`) REFERENCES `Lines` (`LineId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

INSERT INTO `Bookers` (`BookerId`, `BookerPwd`, `CardId`, `DateOfBirth`, `FirstName`, `Gender`, `IsDeleted`, `LastName`, `PhoneNum`, `TimeOfRegister`, `UserName`)
VALUES (1, '123456', '453009200001013710', TIMESTAMP '2000-01-09 00:00:00', '李', 1, False, '黑沙', '13600291522', TIMESTAMP '2022-03-21 17:08:25', '黑沙');

INSERT INTO `Stations` (`StationId`, `IsTerminal`, `StationName`)
VALUES (1, TRUE, '广州站');

INSERT INTO `Stations` (`StationId`, `StationName`)
VALUES (2, '重庆站');
INSERT INTO `Stations` (`StationId`, `StationName`)
VALUES (3, '北京站');

INSERT INTO `Stations` (`StationId`, `IsTerminal`, `StationName`)
VALUES (4, TRUE, '上海站');
INSERT INTO `Stations` (`StationId`, `IsTerminal`, `StationName`)
VALUES (5, TRUE, '成都站');
INSERT INTO `Stations` (`StationId`, `IsTerminal`, `StationName`)
VALUES (6, TRUE, '哈尔滨站');

INSERT INTO `Stations` (`StationId`, `StationName`)
VALUES (7, '武汉站');

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

INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (1, 1, TIME '14:30:00', 'Z112', 'Z');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (2, 2, TIME '12:30:00', 'D1849', 'D');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (3, 3, TIME '08:50:00', 'K528', 'K');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (4, 4, TIME '19:12:00', 'G1204', 'G');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (5, 5, TIME '11:45:00', 'D636', 'D');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (6, 6, TIME '07:10:00', 'K527', 'K');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (7, 7, TIME '15:55:00', 'K728', 'K');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (8, 8, TIME '14:03:00', 'G1202', 'G');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (9, 9, TIME '08:20:00', 'K518', 'K');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (10, 10, TIME '10:10:00', 'K488', 'K');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (11, 11, TIME '17:00:00', 'G2195', 'G');
INSERT INTO `Trains` (`TrainId`, `LineId`, `Time`, `TrainName`, `TypeOfTrain`)
VALUES (12, 12, TIME '18:40:00', 'K546', 'K');

CREATE INDEX `IX_Lines_EndTerminal` ON `Lines` (`EndTerminal`);

CREATE INDEX `IX_Trains_LineId` ON `Trains` (`LineId`);

CREATE UNIQUE INDEX `IX_Trains_TrainName` ON `Trains` (`TrainName`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220321090825_rebuild_table_id', '6.0.3');

COMMIT;

