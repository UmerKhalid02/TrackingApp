using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingApp.Data.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "trk",
                table: "Role",
                columns: new[] { "RoleId", "CreatedAt", "DeletedAt", "Description", "IsActive", "RoleName", "UpdatedAt" },
                values: new object[] { new Guid("2bd8f739-13c4-46e2-b0cc-5888851f373a"), new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "User", true, "US", null });

            migrationBuilder.InsertData(
                schema: "trk",
                table: "Role",
                columns: new[] { "RoleId", "CreatedAt", "DeletedAt", "Description", "IsActive", "RoleName", "UpdatedAt" },
                values: new object[] { new Guid("35dc76b5-8de7-4eb3-a29c-9a05686a6f89"), new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admin", true, "AD", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "trk",
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("2bd8f739-13c4-46e2-b0cc-5888851f373a"));

            migrationBuilder.DeleteData(
                schema: "trk",
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("35dc76b5-8de7-4eb3-a29c-9a05686a6f89"));
        }
    }
}
