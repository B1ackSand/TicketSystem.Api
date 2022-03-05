CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Bookers` (
    `BookerId` char(36) COLLATE ascii_general_ci NOT NULL,
    `BookerWx` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
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

CREATE TABLE `Stations` (
    `StationId` char(36) COLLATE ascii_general_ci NOT NULL,
    `StationName` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `IsTerminal` tinyint(1) NOT NULL DEFAULT FALSE,
    CONSTRAINT `PK_Stations` PRIMARY KEY (`StationId`),
    CONSTRAINT `AK_Stations_StationName` UNIQUE (`StationName`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Lines` (
    `LineId` char(36) COLLATE ascii_general_ci NOT NULL,
    `StartTerminal` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `EndTerminal` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `StopStation` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Lines` PRIMARY KEY (`LineId`),
    CONSTRAINT `FK_Lines_Stations_EndTerminal` FOREIGN KEY (`EndTerminal`) REFERENCES `Stations` (`StationName`) ON DELETE CASCADE,
    CONSTRAINT `FK_Lines_Stations_StartTerminal` FOREIGN KEY (`StartTerminal`) REFERENCES `Stations` (`StationName`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Trains` (
    `TrainId` char(36) COLLATE ascii_general_ci NOT NULL,
    `LineId` char(36) COLLATE ascii_general_ci NOT NULL,
    `TrainName` varchar(5) CHARACTER SET utf8mb4 NOT NULL,
    `TypeOfTrain` varchar(10) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Trains` PRIMARY KEY (`TrainId`),
    CONSTRAINT `FK_Trains_Lines_LineId` FOREIGN KEY (`LineId`) REFERENCES `Lines` (`LineId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

INSERT INTO `Bookers` (`BookerId`, `BookerPwd`, `BookerWx`, `DateOfBirth`, `FirstName`, `Gender`, `IsDeleted`, `LastName`, `PhoneNum`, `TimeOfRegister`, `UserName`)
VALUES ('99e5b121-ef55-4e35-8d72-89d5622b73d1', '123456', '1', TIMESTAMP '2000-01-09 00:00:00', '李', 1, False, '黑沙', '12345678901', TIMESTAMP '2022-03-01 21:43:09', '黑沙');

INSERT INTO `Stations` (`StationId`, `IsTerminal`, `StationName`)
VALUES ('07c4638c-48b7-4783-88a5-58f47e2a0458', TRUE, '哈尔滨站');
INSERT INTO `Stations` (`StationId`, `IsTerminal`, `StationName`)
VALUES ('0846ff99-37ac-4849-804b-1eefac46d651', TRUE, '成都站');

INSERT INTO `Stations` (`StationId`, `StationName`)
VALUES ('09626794-5565-452e-85a4-b924805588ba', '武汉站');

INSERT INTO `Stations` (`StationId`, `IsTerminal`, `StationName`)
VALUES ('4b501cb3-d168-4cc0-b375-48fb33f318a4', TRUE, '广州站');

INSERT INTO `Stations` (`StationId`, `StationName`)
VALUES ('72457e73-ea34-4e02-b575-8d384e82a481', '北京站');
INSERT INTO `Stations` (`StationId`, `StationName`)
VALUES ('7eaa532c-1be5-472c-a738-94fd26e5fad6', '重庆站');

INSERT INTO `Stations` (`StationId`, `IsTerminal`, `StationName`)
VALUES ('b091b148-8fc7-4ce5-a6c5-c61dbbb3f91f', TRUE, '上海站');

INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`)
VALUES ('10687777-24de-4a07-a677-633031ae1009', '哈尔滨站', '上海站', '上海站,北京站,哈尔滨站');
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`)
VALUES ('18c9ecbb-dc2c-43e8-ba77-9a6cef3ac9bc', '成都站', '广州站', '广州站,重庆站,成都站');
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`)
VALUES ('804edb5e-2bce-43e7-b34b-6db68a9ceb27', '广州站', '成都站', '成都站,重庆站,广州站');
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`)
VALUES ('92d0ada0-2cd0-4cc9-b03d-3eccf17ab1a5', '哈尔滨站', '广州站', '广州站,武汉站,北京站,哈尔滨站');
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`)
VALUES ('b2187869-2f9f-4ea0-99b4-b8e5c8f34f3d', '广州站', '哈尔滨站', '哈尔滨站,北京站,武汉站,广州站');
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`)
VALUES ('ba2b1c71-bff6-4507-ad15-99c6e13bb5fa', '上海站', '成都站', '成都站,重庆站,武汉站,上海站');
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`)
VALUES ('c9c55cc8-2185-40b8-b85b-55c34c918f66', '哈尔滨站', '成都站', '成都站,重庆站,武汉站,北京站,哈尔滨站');
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`)
VALUES ('cbead21b-0681-4a1a-853f-d5b61fd48f54', '上海站', '广州站', '广州站,武汉站,上海站');
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`)
VALUES ('e7ff44ba-c4f9-40c8-a5a0-9ddc557f6093', '成都站', '上海站', '上海站,武汉站,重庆站,成都站');
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`)
VALUES ('ee3e7e33-2c85-46c9-98e5-b4bf10f32576', '广州站', '上海站', '上海站,武汉站,广州站');
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`)
VALUES ('ee9e796d-fbfe-42c2-8eb4-b9674206ebc7', '上海站', '哈尔滨站', '哈尔滨站,北京站,上海站');
INSERT INTO `Lines` (`LineId`, `EndTerminal`, `StartTerminal`, `StopStation`)
VALUES ('fec134b0-8623-42db-8602-b64cce2912c2', '成都站', '哈尔滨站', '哈尔滨站,北京站,武汉站,重庆站,成都站');

INSERT INTO `Trains` (`TrainId`, `LineId`, `TrainName`, `TypeOfTrain`)
VALUES ('146dae5c-7912-45bc-9e5c-60cfc5d77b6a', 'e7ff44ba-c4f9-40c8-a5a0-9ddc557f6093', 'D636', 'D');
INSERT INTO `Trains` (`TrainId`, `LineId`, `TrainName`, `TypeOfTrain`)
VALUES ('40843c33-3050-437d-9749-73c7823be7a1', '10687777-24de-4a07-a677-633031ae1009', 'G1204', 'G');
INSERT INTO `Trains` (`TrainId`, `LineId`, `TrainName`, `TypeOfTrain`)
VALUES ('5d0c96b6-b3eb-497d-8c4c-f12e05fb5e29', 'b2187869-2f9f-4ea0-99b4-b8e5c8f34f3d', 'K728', 'K');
INSERT INTO `Trains` (`TrainId`, `LineId`, `TrainName`, `TypeOfTrain`)
VALUES ('5ee7f9cd-279f-4c5b-83bf-034f6419be7a', 'ee9e796d-fbfe-42c2-8eb4-b9674206ebc7', 'G1202', 'G');
INSERT INTO `Trains` (`TrainId`, `LineId`, `TrainName`, `TypeOfTrain`)
VALUES ('639031e7-cd65-466f-9e8b-f67c14801973', '804edb5e-2bce-43e7-b34b-6db68a9ceb27', 'K488', 'K');
INSERT INTO `Trains` (`TrainId`, `LineId`, `TrainName`, `TypeOfTrain`)
VALUES ('7971f095-300c-4628-b2a8-4e64ba04cbc3', 'fec134b0-8623-42db-8602-b64cce2912c2', 'K548', 'K');
INSERT INTO `Trains` (`TrainId`, `LineId`, `TrainName`, `TypeOfTrain`)
VALUES ('88f68a2e-d574-4dd5-b5dd-e5048b82e867', 'c9c55cc8-2185-40b8-b85b-55c34c918f66', 'K546', 'K');
INSERT INTO `Trains` (`TrainId`, `LineId`, `TrainName`, `TypeOfTrain`)
VALUES ('99e5b121-ef55-4e35-8d72-89d5622b73db', 'cbead21b-0681-4a1a-853f-d5b61fd48f54', 'K528', 'K');
INSERT INTO `Trains` (`TrainId`, `LineId`, `TrainName`, `TypeOfTrain`)
VALUES ('cc2a984d-cd07-4329-9b22-84a5c0185ea7', '92d0ada0-2cd0-4cc9-b03d-3eccf17ab1a5', 'Z112', 'Z');
INSERT INTO `Trains` (`TrainId`, `LineId`, `TrainName`, `TypeOfTrain`)
VALUES ('e185afad-aa89-4d4e-bba0-391ce821ae9d', '18c9ecbb-dc2c-43e8-ba77-9a6cef3ac9bc', 'D1849', 'D');
INSERT INTO `Trains` (`TrainId`, `LineId`, `TrainName`, `TypeOfTrain`)
VALUES ('f4abb3d9-873b-44ff-90cd-860a36fc259f', 'ee3e7e33-2c85-46c9-98e5-b4bf10f32576', 'K527', 'K');
INSERT INTO `Trains` (`TrainId`, `LineId`, `TrainName`, `TypeOfTrain`)
VALUES ('f5d6e132-c4df-43fe-91c2-39f390dadab7', 'ba2b1c71-bff6-4507-ad15-99c6e13bb5fa', 'G2195', 'G');

CREATE INDEX `IX_Lines_EndTerminal` ON `Lines` (`EndTerminal`);

CREATE INDEX `IX_Trains_LineId` ON `Trains` (`LineId`);

CREATE UNIQUE INDEX `IX_Trains_TrainName` ON `Trains` (`TrainName`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220301134309_UpdateticketDbContext', '6.0.2');

COMMIT;

START TRANSACTION;

ALTER TABLE `Lines` ADD `TrainName` longtext CHARACTER SET utf8mb4 NULL;

UPDATE `Bookers` SET `IsDeleted` = False, `TimeOfRegister` = TIMESTAMP '2022-03-02 01:39:27'
WHERE `BookerId` = '99e5b121-ef55-4e35-8d72-89d5622b73d1';
SELECT ROW_COUNT();


INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220301173927_update', '6.0.2');

COMMIT;

START TRANSACTION;

CREATE TABLE `Orders` (
    `OrderId` char(36) COLLATE ascii_general_ci NOT NULL,
    `BookerId` char(36) COLLATE ascii_general_ci NOT NULL,
    `TrainId` char(36) COLLATE ascii_general_ci NOT NULL,
    `CreatedDate` datetime(6) NOT NULL,
    CONSTRAINT `PK_Orders` PRIMARY KEY (`OrderId`)
) CHARACTER SET=utf8mb4;

UPDATE `Bookers` SET `IsDeleted` = False, `TimeOfRegister` = TIMESTAMP '2022-03-03 01:25:06'
WHERE `BookerId` = '99e5b121-ef55-4e35-8d72-89d5622b73d1';
SELECT ROW_COUNT();


INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220302172507_updateorder', '6.0.2');

COMMIT;

START TRANSACTION;

ALTER TABLE `Orders` ADD `EndTerminalId` char(36) COLLATE ascii_general_ci NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

ALTER TABLE `Orders` ADD `StartTerminalId` char(36) COLLATE ascii_general_ci NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

UPDATE `Bookers` SET `IsDeleted` = False, `TimeOfRegister` = TIMESTAMP '2022-03-03 02:17:29'
WHERE `BookerId` = '99e5b121-ef55-4e35-8d72-89d5622b73d1';
SELECT ROW_COUNT();


INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220302181729_addterminalfororderstable', '6.0.2');

COMMIT;

