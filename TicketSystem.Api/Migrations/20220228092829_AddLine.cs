using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSystem.Api.Migrations
{
    public partial class AddLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StartTerminal = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EndTerminal = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StopStation = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Bookers",
                keyColumn: "Id",
                keyValue: new Guid("99e5b121-ef55-4e35-8d72-89d5622b73d1"),
                columns: new[] { "IsDeleted", "TimeOfRegister" },
                values: new object[] { false, new DateTime(2022, 2, 28, 17, 28, 29, 439, DateTimeKind.Local).AddTicks(868) });

            migrationBuilder.InsertData(
                table: "Lines",
                columns: new[] { "Id", "EndTerminal", "StartTerminal", "StopStation" },
                values: new object[,]
                {
                    { new Guid("10687777-24de-4a07-a677-633031ae1009"), "哈尔滨站", "上海站", "上海站,北京站,哈尔滨站" },
                    { new Guid("18c9ecbb-dc2c-43e8-ba77-9a6cef3ac9bc"), "成都站", "广州站", "广州站,重庆站,成都站" },
                    { new Guid("804edb5e-2bce-43e7-b34b-6db68a9ceb27"), "广州站", "成都站", "成都站,重庆站,广州站" },
                    { new Guid("92d0ada0-2cd0-4cc9-b03d-3eccf17ab1a5"), "哈尔滨站", "广州站", "广州站,武汉站,北京站,哈尔滨站" },
                    { new Guid("b2187869-2f9f-4ea0-99b4-b8e5c8f34f3d"), "广州站", "哈尔滨站", "哈尔滨站,北京站,武汉站,广州站" },
                    { new Guid("ba2b1c71-bff6-4507-ad15-99c6e13bb5fa"), "上海站", "成都站", "成都站,重庆站,武汉站,上海站" },
                    { new Guid("c9c55cc8-2185-40b8-b85b-55c34c918f66"), "哈尔滨站", "成都站", "成都站,重庆站,武汉站,北京站,哈尔滨" },
                    { new Guid("cbead21b-0681-4a1a-853f-d5b61fd48f54"), "上海站", "广州站", "广州站,武汉站,上海站" },
                    { new Guid("e7ff44ba-c4f9-40c8-a5a0-9ddc557f6093"), "成都站", "上海站", "上海站,武汉站,重庆站,成都站" },
                    { new Guid("ee3e7e33-2c85-46c9-98e5-b4bf10f32576"), "广州站", "上海站", "上海站,武汉站,广州站" },
                    { new Guid("ee9e796d-fbfe-42c2-8eb4-b9674206ebc7"), "上海站", "哈尔滨站", "哈尔滨站,北京站,上海站" },
                    { new Guid("fec134b0-8623-42db-8602-b64cce2912c2"), "成都站", "哈尔滨站", "哈尔滨站,北京站,武汉站,重庆站,成都站" }
                });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: new Guid("0846ff99-37ac-4849-804b-1eefac46d651"),
                column: "IsTerminal",
                value: true);

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: new Guid("72457e73-ea34-4e02-b575-8d384e82a481"),
                column: "IsTerminal",
                value: false);

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "StationName" },
                values: new object[] { new Guid("09626794-5565-452e-85a4-b924805588ba"), "武汉站" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lines");

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: new Guid("09626794-5565-452e-85a4-b924805588ba"));

            migrationBuilder.UpdateData(
                table: "Bookers",
                keyColumn: "Id",
                keyValue: new Guid("99e5b121-ef55-4e35-8d72-89d5622b73d1"),
                columns: new[] { "IsDeleted", "TimeOfRegister" },
                values: new object[] { (sbyte)0, new DateTime(2022, 2, 22, 2, 12, 16, 193, DateTimeKind.Local).AddTicks(9309) });

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: new Guid("0846ff99-37ac-4849-804b-1eefac46d651"),
                column: "IsTerminal",
                value: false);

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: new Guid("72457e73-ea34-4e02-b575-8d384e82a481"),
                column: "IsTerminal",
                value: true);
        }
    }
}
