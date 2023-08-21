using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingApp.Data.Migrations
{
    public partial class AddedContactInAdminSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "trk",
                table: "User",
                keyColumn: "UserID",
                keyValue: new Guid("ef12ee01-adcf-4a8a-8544-03a592d9e252"),
                column: "ContactNo",
                value: "00000000000");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "trk",
                table: "User",
                keyColumn: "UserID",
                keyValue: new Guid("ef12ee01-adcf-4a8a-8544-03a592d9e252"),
                column: "ContactNo",
                value: null);
        }
    }
}
