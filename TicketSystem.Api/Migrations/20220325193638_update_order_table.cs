﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSystem.Api.Migrations
{
    public partial class update_order_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bookers",
                columns: table => new
                {
                    BookerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CardId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BookerPwd = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    PhoneNum = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TimeOfRegister = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookers", x => x.BookerId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    LineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StartTerminal = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EndTerminal = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StopStation = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TrainName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.LineId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BookerId = table.Column<int>(type: "int", nullable: false),
                    TrainId = table.Column<int>(type: "int", nullable: false),
                    StartTerminalId = table.Column<int>(type: "int", nullable: false),
                    EndTerminalId = table.Column<int>(type: "int", nullable: false),
                    StartTerminal = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EndTerminal = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TrainName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Price = table.Column<double>(type: "double", nullable: false),
                    DateBook = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StationName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsTerminal = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    Latitude = table.Column<double>(type: "double", nullable: false),
                    Longitude = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.StationId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    TrainId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LineId = table.Column<int>(type: "int", nullable: false),
                    TrainName = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TypeOfTrain = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Time = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trains", x => x.TrainId);
                    table.ForeignKey(
                        name: "FK_Trains_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Lines",
                        principalColumn: "LineId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Bookers",
                columns: new[] { "BookerId", "BookerPwd", "CardId", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "LastName", "PhoneNum", "TimeOfRegister", "UserName" },
                values: new object[] { 1, "123456", "453009200001013710", new DateTime(2000, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "李", 1, false, "黑沙", "13600291522", new DateTime(2022, 3, 26, 3, 36, 38, 649, DateTimeKind.Local).AddTicks(8929), "黑沙" });

            migrationBuilder.InsertData(
                table: "Lines",
                columns: new[] { "LineId", "EndTerminal", "StartTerminal", "StopStation", "TrainName" },
                values: new object[,]
                {
                    { 1, "哈尔滨站", "广州站", "广州站,武汉站,北京站,哈尔滨站", null },
                    { 2, "成都站", "广州站", "广州站,重庆站,成都站", null },
                    { 3, "上海站", "广州站", "广州站,武汉站,上海站", null },
                    { 4, "哈尔滨站", "上海站", "上海站,北京站,哈尔滨站", null },
                    { 5, "成都站", "上海站", "上海站,武汉站,重庆站,成都站", null },
                    { 6, "广州站", "上海站", "上海站,武汉站,广州站", null },
                    { 7, "广州站", "哈尔滨站", "哈尔滨站,北京站,武汉站,广州站", null },
                    { 8, "上海站", "哈尔滨站", "哈尔滨站,北京站,上海站", null },
                    { 9, "成都站", "哈尔滨站", "哈尔滨站,北京站,武汉站,重庆站,成都站", null },
                    { 10, "广州站", "成都站", "成都站,重庆站,广州站", null },
                    { 11, "上海站", "成都站", "成都站,重庆站,武汉站,上海站", null },
                    { 12, "哈尔滨站", "成都站", "成都站,重庆站,武汉站,北京站,哈尔滨站", null }
                });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "StationId", "IsTerminal", "Latitude", "Longitude", "StationName" },
                values: new object[] { 1, true, 23.148721999999999, 113.25765199999999, "广州站" });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "StationId", "Latitude", "Longitude", "StationName" },
                values: new object[,]
                {
                    { 2, 29.549520000000001, 106.547546, "重庆站" },
                    { 3, 39.904217000000003, 116.427162, "北京站" }
                });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "StationId", "IsTerminal", "Latitude", "Longitude", "StationName" },
                values: new object[,]
                {
                    { 4, true, 31.249600999999998, 121.455704, "上海站" },
                    { 5, true, 30.629023, 104.154915, "成都站" },
                    { 6, true, 45.761088999999998, 126.631905, "哈尔滨站" }
                });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "StationId", "Latitude", "Longitude", "StationName" },
                values: new object[] { 7, 30.607346, 114.42449999999999, "武汉站" });

            migrationBuilder.InsertData(
                table: "Trains",
                columns: new[] { "TrainId", "LineId", "Time", "TrainName", "TypeOfTrain" },
                values: new object[,]
                {
                    { 1, 1, "07:41,09:06,15:03,21:01", "D112", "D" },
                    { 2, 2, "10:16,17:43,19:17", "D1849", "D" },
                    { 3, 3, "07:50,16:38,04:29(+1)", "K528", "K" },
                    { 4, 4, "09:33,16:12,21:36", "G1204", "G" },
                    { 5, 5, "06:32,12:00,18:44,20:42", "D636", "D" },
                    { 6, 6, "19:15,10:12(+1),17:21(+1)", "K527", "K" },
                    { 7, 7, "07:41,10:06,15:03,21:01", "D728", "D" },
                    { 8, 8, "08:52,15:01,21:40", "G1202", "G" },
                    { 9, 9, "19:15,06:12(+1),17:21(+1),23:50(+1)", "K518", "K" },
                    { 10, 10, "07:15,15:12,23:21", "K488", "K" },
                    { 11, 11, "06:10,11:27,15:25,19:27", "G2195", "G" },
                    { 12, 12, "18:15,05:12(+1),16:21(+1),22:50(+1)", "K546", "K" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trains_LineId",
                table: "Trains",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_Trains_TrainName",
                table: "Trains",
                column: "TrainName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Trains");

            migrationBuilder.DropTable(
                name: "Lines");
        }
    }
}
