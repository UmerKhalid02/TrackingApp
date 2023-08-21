using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingApp.Data.Migrations
{
    public partial class AddedEmailInAdminSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "trk",
                table: "User",
                keyColumn: "UserID",
                keyValue: new Guid("ef12ee01-adcf-4a8a-8544-03a592d9e252"),
                column: "Email",
                value: "admin@admin.com");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "trk",
                table: "User",
                keyColumn: "UserID",
                keyValue: new Guid("ef12ee01-adcf-4a8a-8544-03a592d9e252"),
                column: "Email",
                value: null);
        }
    }
}
