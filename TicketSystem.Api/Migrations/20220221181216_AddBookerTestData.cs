using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketSystem.Api.Migrations
{
    public partial class AddBookerTestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bookers",
                columns: new[] { "Id", "BookerPwd", "BookerWx", "DateOfBirth", "FirstName", "Gender", "IsDeleted", "LastName", "PhoneNum", "TimeOfRegister", "UserName" },
                values: new object[] { new Guid("99e5b121-ef55-4e35-8d72-89d5622b73d1"), "123456", "1", new DateTime(2000, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "李", 1, false, "黑沙", "12345678901", new DateTime(2022, 2, 22, 2, 12, 16, 193, DateTimeKind.Local).AddTicks(9309), "黑沙" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookers",
                keyColumn: "Id",
                keyValue: new Guid("99e5b121-ef55-4e35-8d72-89d5622b73d1"));
        }
    }
}
