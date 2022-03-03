using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSystem.Api.Migrations
{
    public partial class updateorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BookerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TrainId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Bookers",
                keyColumn: "BookerId",
                keyValue: new Guid("99e5b121-ef55-4e35-8d72-89d5622b73d1"),
                columns: new[] { "IsDeleted", "TimeOfRegister" },
                values: new object[] { false, new DateTime(2022, 3, 3, 1, 25, 6, 706, DateTimeKind.Local).AddTicks(1827) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.UpdateData(
                table: "Bookers",
                keyColumn: "BookerId",
                keyValue: new Guid("99e5b121-ef55-4e35-8d72-89d5622b73d1"),
                columns: new[] { "IsDeleted", "TimeOfRegister" },
                values: new object[] { (sbyte)0, new DateTime(2022, 3, 2, 1, 39, 27, 458, DateTimeKind.Local).AddTicks(5093) });
        }
    }
}
