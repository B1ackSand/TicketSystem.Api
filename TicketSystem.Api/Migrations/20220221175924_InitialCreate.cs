using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSystem.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bookers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BookerWx = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
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
                    table.PrimaryKey("PK_Bookers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StationName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsTerminal = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TrainName = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TypeOfTrain = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trains", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "IsTerminal", "StationName" },
                values: new object[] { new Guid("07c4638c-48b7-4783-88a5-58f47e2a0458"), true, "哈尔滨站" });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "StationName" },
                values: new object[] { new Guid("0846ff99-37ac-4849-804b-1eefac46d651"), "成都站" });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "IsTerminal", "StationName" },
                values: new object[,]
                {
                    { new Guid("4b501cb3-d168-4cc0-b375-48fb33f318a4"), true, "广州站" },
                    { new Guid("72457e73-ea34-4e02-b575-8d384e82a481"), true, "北京站" }
                });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "StationName" },
                values: new object[] { new Guid("7eaa532c-1be5-472c-a738-94fd26e5fad6"), "重庆站" });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "IsTerminal", "StationName" },
                values: new object[] { new Guid("b091b148-8fc7-4ce5-a6c5-c61dbbb3f91f"), true, "上海站" });

            migrationBuilder.InsertData(
                table: "Trains",
                columns: new[] { "Id", "TrainName", "TypeOfTrain" },
                values: new object[,]
                {
                    { new Guid("99e5b121-ef55-4e35-8d72-89d5622b73db"), "D1868", "D" },
                    { new Guid("cc2a984d-cd07-4329-9b22-84a5c0185ea7"), "K48", "K" },
                    { new Guid("e185afad-aa89-4d4e-bba0-391ce821ae9d"), "K11", "K" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookers");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Trains");
        }
    }
}
