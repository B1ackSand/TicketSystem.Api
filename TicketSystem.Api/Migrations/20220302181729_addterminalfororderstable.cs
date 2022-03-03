using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSystem.Api.Migrations
{
    public partial class addterminalfororderstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EndTerminalId",
                table: "Orders",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "StartTerminalId",
                table: "Orders",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "Bookers",
                keyColumn: "BookerId",
                keyValue: new Guid("99e5b121-ef55-4e35-8d72-89d5622b73d1"),
                columns: new[] { "IsDeleted", "TimeOfRegister" },
                values: new object[] { false, new DateTime(2022, 3, 3, 2, 17, 29, 229, DateTimeKind.Local).AddTicks(2863) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTerminalId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StartTerminalId",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Bookers",
                keyColumn: "BookerId",
                keyValue: new Guid("99e5b121-ef55-4e35-8d72-89d5622b73d1"),
                columns: new[] { "IsDeleted", "TimeOfRegister" },
                values: new object[] { (sbyte)0, new DateTime(2022, 3, 3, 1, 25, 6, 706, DateTimeKind.Local).AddTicks(1827) });
        }
    }
}
