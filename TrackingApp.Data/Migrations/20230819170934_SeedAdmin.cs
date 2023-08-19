using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingApp.Data.Migrations
{
    public partial class SeedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "trk",
                table: "User",
                columns: new[] { "UserID", "Address", "City", "ContactNo", "Country", "CreatedAt", "DeletedAt", "Email", "IsActive", "Password", "State", "UpdatedAt", "UserName" },
                values: new object[] { new Guid("ef12ee01-adcf-4a8a-8544-03a592d9e252"), null, null, null, null, new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "$admin1234*", null, null, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "trk",
                table: "User",
                keyColumn: "UserID",
                keyValue: new Guid("ef12ee01-adcf-4a8a-8544-03a592d9e252"));
        }
    }
}
