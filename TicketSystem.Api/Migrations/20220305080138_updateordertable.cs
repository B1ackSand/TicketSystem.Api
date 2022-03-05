using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSystem.Api.Migrations
{
    public partial class updateordertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bookers",
                columns: table => new
                {
                    BookerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
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
                    table.PrimaryKey("PK_Bookers", x => x.BookerId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BookerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TrainId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StartTerminalId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EndTerminalId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StartTerminal = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EndTerminal = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TrainName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                    StationId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StationName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsTerminal = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.StationId);
                    table.UniqueConstraint("AK_Stations_StationName", x => x.StationName);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    LineId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
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
                    table.ForeignKey(
                        name: "FK_Lines_Stations_EndTerminal",
                        column: x => x.EndTerminal,
                        principalTable: "Stations",
                        principalColumn: "StationName",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    TrainId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LineId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TrainName = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TypeOfTrain = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
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
                columns: new[] { "BookerId", "BookerPwd", "BookerWx", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "LastName", "PhoneNum", "TimeOfRegister", "UserName" },
                values: new object[] { new Guid("99e5b121-ef55-4e35-8d72-89d5622b73d1"), "123456", "1", new DateTime(2000, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "李", 1, false, "黑沙", "12345678901", new DateTime(2022, 3, 5, 16, 1, 38, 596, DateTimeKind.Local).AddTicks(9623), "黑沙" });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "StationId", "IsTerminal", "StationName" },
                values: new object[,]
                {
                    { new Guid("07c4638c-48b7-4783-88a5-58f47e2a0458"), true, "哈尔滨站" },
                    { new Guid("0846ff99-37ac-4849-804b-1eefac46d651"), true, "成都站" }
                });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "StationId", "StationName" },
                values: new object[] { new Guid("09626794-5565-452e-85a4-b924805588ba"), "武汉站" });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "StationId", "IsTerminal", "StationName" },
                values: new object[] { new Guid("4b501cb3-d168-4cc0-b375-48fb33f318a4"), true, "广州站" });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "StationId", "StationName" },
                values: new object[,]
                {
                    { new Guid("72457e73-ea34-4e02-b575-8d384e82a481"), "北京站" },
                    { new Guid("7eaa532c-1be5-472c-a738-94fd26e5fad6"), "重庆站" }
                });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "StationId", "IsTerminal", "StationName" },
                values: new object[] { new Guid("b091b148-8fc7-4ce5-a6c5-c61dbbb3f91f"), true, "上海站" });

            migrationBuilder.InsertData(
                table: "Lines",
                columns: new[] { "LineId", "EndTerminal", "StartTerminal", "StopStation", "TrainName" },
                values: new object[,]
                {
                    { new Guid("10687777-24de-4a07-a677-633031ae1009"), "哈尔滨站", "上海站", "上海站,北京站,哈尔滨站", null },
                    { new Guid("18c9ecbb-dc2c-43e8-ba77-9a6cef3ac9bc"), "成都站", "广州站", "广州站,重庆站,成都站", null },
                    { new Guid("804edb5e-2bce-43e7-b34b-6db68a9ceb27"), "广州站", "成都站", "成都站,重庆站,广州站", null },
                    { new Guid("92d0ada0-2cd0-4cc9-b03d-3eccf17ab1a5"), "哈尔滨站", "广州站", "广州站,武汉站,北京站,哈尔滨站", null },
                    { new Guid("b2187869-2f9f-4ea0-99b4-b8e5c8f34f3d"), "广州站", "哈尔滨站", "哈尔滨站,北京站,武汉站,广州站", null },
                    { new Guid("ba2b1c71-bff6-4507-ad15-99c6e13bb5fa"), "上海站", "成都站", "成都站,重庆站,武汉站,上海站", null },
                    { new Guid("c9c55cc8-2185-40b8-b85b-55c34c918f66"), "哈尔滨站", "成都站", "成都站,重庆站,武汉站,北京站,哈尔滨站", null },
                    { new Guid("cbead21b-0681-4a1a-853f-d5b61fd48f54"), "上海站", "广州站", "广州站,武汉站,上海站", null },
                    { new Guid("e7ff44ba-c4f9-40c8-a5a0-9ddc557f6093"), "成都站", "上海站", "上海站,武汉站,重庆站,成都站", null },
                    { new Guid("ee3e7e33-2c85-46c9-98e5-b4bf10f32576"), "广州站", "上海站", "上海站,武汉站,广州站", null },
                    { new Guid("ee9e796d-fbfe-42c2-8eb4-b9674206ebc7"), "上海站", "哈尔滨站", "哈尔滨站,北京站,上海站", null },
                    { new Guid("fec134b0-8623-42db-8602-b64cce2912c2"), "成都站", "哈尔滨站", "哈尔滨站,北京站,武汉站,重庆站,成都站", null }
                });

            migrationBuilder.InsertData(
                table: "Trains",
                columns: new[] { "TrainId", "LineId", "TrainName", "TypeOfTrain" },
                values: new object[,]
                {
                    { new Guid("146dae5c-7912-45bc-9e5c-60cfc5d77b6a"), new Guid("e7ff44ba-c4f9-40c8-a5a0-9ddc557f6093"), "D636", "D" },
                    { new Guid("40843c33-3050-437d-9749-73c7823be7a1"), new Guid("10687777-24de-4a07-a677-633031ae1009"), "G1204", "G" },
                    { new Guid("5d0c96b6-b3eb-497d-8c4c-f12e05fb5e29"), new Guid("b2187869-2f9f-4ea0-99b4-b8e5c8f34f3d"), "K728", "K" },
                    { new Guid("5ee7f9cd-279f-4c5b-83bf-034f6419be7a"), new Guid("ee9e796d-fbfe-42c2-8eb4-b9674206ebc7"), "G1202", "G" },
                    { new Guid("639031e7-cd65-466f-9e8b-f67c14801973"), new Guid("804edb5e-2bce-43e7-b34b-6db68a9ceb27"), "K488", "K" },
                    { new Guid("7971f095-300c-4628-b2a8-4e64ba04cbc3"), new Guid("fec134b0-8623-42db-8602-b64cce2912c2"), "K548", "K" },
                    { new Guid("88f68a2e-d574-4dd5-b5dd-e5048b82e867"), new Guid("c9c55cc8-2185-40b8-b85b-55c34c918f66"), "K546", "K" },
                    { new Guid("99e5b121-ef55-4e35-8d72-89d5622b73db"), new Guid("cbead21b-0681-4a1a-853f-d5b61fd48f54"), "K528", "K" },
                    { new Guid("cc2a984d-cd07-4329-9b22-84a5c0185ea7"), new Guid("92d0ada0-2cd0-4cc9-b03d-3eccf17ab1a5"), "Z112", "Z" },
                    { new Guid("e185afad-aa89-4d4e-bba0-391ce821ae9d"), new Guid("18c9ecbb-dc2c-43e8-ba77-9a6cef3ac9bc"), "D1849", "D" },
                    { new Guid("f4abb3d9-873b-44ff-90cd-860a36fc259f"), new Guid("ee3e7e33-2c85-46c9-98e5-b4bf10f32576"), "K527", "K" },
                    { new Guid("f5d6e132-c4df-43fe-91c2-39f390dadab7"), new Guid("ba2b1c71-bff6-4507-ad15-99c6e13bb5fa"), "G2195", "G" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lines_EndTerminal",
                table: "Lines",
                column: "EndTerminal");

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
                name: "Trains");

            migrationBuilder.DropTable(
                name: "Lines");

            migrationBuilder.DropTable(
                name: "Stations");
        }
    }
}
