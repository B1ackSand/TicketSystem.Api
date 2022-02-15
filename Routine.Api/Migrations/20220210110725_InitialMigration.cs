using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Routine.Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("e185afad-aa89-4d4e-bba0-391ce821ae9d"),
                column: "Introduction",
                value: "Evil 111");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("e185afad-aa89-4d4e-bba0-391ce821ae9d"),
                column: "Introduction",
                value: "Evil Company");
        }
    }
}
