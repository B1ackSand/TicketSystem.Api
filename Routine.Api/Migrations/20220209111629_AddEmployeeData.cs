using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Routine.Api.Migrations
{
    public partial class AddEmployeeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[,]
                {
                    { new Guid("1861341e-b42b-410c-ae21-cf11f36fc574"), new Guid("99e5b121-ef55-4e35-8d72-89d5622b73db"), new DateTime(1957, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "A404", "Not", 1, "Man" },
                    { new Guid("4b501cb3-d168-4cc0-b375-48fb33f318a4"), new Guid("cc2a984d-cd07-4329-9b22-84a5c0185ea7"), new DateTime(1976, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "MSFT231", "Nick", 1, "Carter" },
                    { new Guid("679dfd33-32e4-4393-b061-f7abb8956f53"), new Guid("99e5b121-ef55-4e35-8d72-89d5622b73db"), new DateTime(1967, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "A009", "卡", 2, "里" },
                    { new Guid("72457e73-ea34-4e02-b575-8d384e82a481"), new Guid("e185afad-aa89-4d4e-bba0-391ce821ae9d"), new DateTime(1986, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "G003", "Mary", 2, "King" },
                    { new Guid("7644b71d-d74e-43e2-ac32-8cbadd7b1c3a"), new Guid("e185afad-aa89-4d4e-bba0-391ce821ae9d"), new DateTime(1977, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "G097", "Kevin", 1, "Richardson" },
                    { new Guid("7eaa532c-1be5-472c-a738-94fd26e5fad6"), new Guid("cc2a984d-cd07-4329-9b22-84a5c0185ea7"), new DateTime(1981, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "MSFT245", "Vince", 1, "Carter" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("1861341e-b42b-410c-ae21-cf11f36fc574"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("4b501cb3-d168-4cc0-b375-48fb33f318a4"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("679dfd33-32e4-4393-b061-f7abb8956f53"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("72457e73-ea34-4e02-b575-8d384e82a481"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("7644b71d-d74e-43e2-ac32-8cbadd7b1c3a"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("7eaa532c-1be5-472c-a738-94fd26e5fad6"));
        }
    }
}
