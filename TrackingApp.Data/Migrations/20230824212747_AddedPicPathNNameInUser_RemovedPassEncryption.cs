using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingApp.Data.Migrations
{
    public partial class AddedPicPathNNameInUser_RemovedPassEncryption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "trk",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicPath",
                schema: "trk",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "trk",
                table: "User",
                keyColumn: "UserID",
                keyValue: new Guid("ef12ee01-adcf-4a8a-8544-03a592d9e252"),
                columns: new[] { "ContactNo", "Email", "Name", "Password" },
                values: new object[] { null, null, "Admin", "admin1234" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "trk",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ProfilePicPath",
                schema: "trk",
                table: "User");

            migrationBuilder.UpdateData(
                schema: "trk",
                table: "User",
                keyColumn: "UserID",
                keyValue: new Guid("ef12ee01-adcf-4a8a-8544-03a592d9e252"),
                columns: new[] { "ContactNo", "Email", "Password" },
                values: new object[] { "00000000000", "admin@admin.com", "$2a$12$SHURSR0Suafcx5bKkUePQO7ka7IQ3wfBQkrH.xtnrRY8mnu9bgMb6" });
        }
    }
}
