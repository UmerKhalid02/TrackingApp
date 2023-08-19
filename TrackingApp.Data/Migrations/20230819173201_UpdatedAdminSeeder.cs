using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingApp.Data.Migrations
{
    public partial class UpdatedAdminSeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserRole_UserId",
                schema: "trk",
                table: "UserRole");

            migrationBuilder.UpdateData(
                schema: "trk",
                table: "User",
                keyColumn: "UserID",
                keyValue: new Guid("ef12ee01-adcf-4a8a-8544-03a592d9e252"),
                column: "Password",
                value: "$2a$12$SHURSR0Suafcx5bKkUePQO7ka7IQ3wfBQkrH.xtnrRY8mnu9bgMb6");

            migrationBuilder.InsertData(
                schema: "trk",
                table: "UserRole",
                columns: new[] { "UserRoleId", "CreatedAt", "DeletedAt", "IsActive", "RoleId", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("70cdb88c-ca74-48e0-a597-162479301c9e"), new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, new Guid("35dc76b5-8de7-4eb3-a29c-9a05686a6f89"), null, new Guid("ef12ee01-adcf-4a8a-8544-03a592d9e252") });

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                schema: "trk",
                table: "UserRole",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserRole_UserId",
                schema: "trk",
                table: "UserRole");

            migrationBuilder.DeleteData(
                schema: "trk",
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: new Guid("70cdb88c-ca74-48e0-a597-162479301c9e"));

            migrationBuilder.UpdateData(
                schema: "trk",
                table: "User",
                keyColumn: "UserID",
                keyValue: new Guid("ef12ee01-adcf-4a8a-8544-03a592d9e252"),
                column: "Password",
                value: "$admin1234*");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                schema: "trk",
                table: "UserRole",
                column: "UserId");
        }
    }
}
