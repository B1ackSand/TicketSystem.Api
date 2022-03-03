using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSystem.Api.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrainName",
                table: "Lines",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Bookers",
                keyColumn: "BookerId",
                keyValue: new Guid("99e5b121-ef55-4e35-8d72-89d5622b73d1"),
                columns: new[] { "IsDeleted", "TimeOfRegister" },
                values: new object[] { false, new DateTime(2022, 3, 2, 1, 39, 27, 458, DateTimeKind.Local).AddTicks(5093) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrainName",
                table: "Lines");

            migrationBuilder.UpdateData(
                table: "Bookers",
                keyColumn: "BookerId",
                keyValue: new Guid("99e5b121-ef55-4e35-8d72-89d5622b73d1"),
                columns: new[] { "IsDeleted", "TimeOfRegister" },
                values: new object[] { (sbyte)0, new DateTime(2022, 3, 1, 21, 43, 9, 43, DateTimeKind.Local).AddTicks(2386) });
        }
    }
}
