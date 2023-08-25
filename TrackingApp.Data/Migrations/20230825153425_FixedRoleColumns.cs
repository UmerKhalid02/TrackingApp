using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingApp.Data.Migrations
{
    public partial class FixedRoleColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleId",
                schema: "trk",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserId",
                schema: "trk",
                table: "UserRole");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "trk",
                table: "UserRole",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "trk",
                table: "UserRole",
                newName: "RoleID");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_UserId",
                schema: "trk",
                table: "UserRole",
                newName: "IX_UserRole_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_RoleId",
                schema: "trk",
                table: "UserRole",
                newName: "IX_UserRole_RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleID",
                schema: "trk",
                table: "UserRole",
                column: "RoleID",
                principalSchema: "trk",
                principalTable: "Role",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserID",
                schema: "trk",
                table: "UserRole",
                column: "UserID",
                principalSchema: "trk",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleID",
                schema: "trk",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserID",
                schema: "trk",
                table: "UserRole");

            migrationBuilder.RenameColumn(
                name: "UserID",
                schema: "trk",
                table: "UserRole",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "RoleID",
                schema: "trk",
                table: "UserRole",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_UserID",
                schema: "trk",
                table: "UserRole",
                newName: "IX_UserRole_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_RoleID",
                schema: "trk",
                table: "UserRole",
                newName: "IX_UserRole_RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId",
                schema: "trk",
                table: "UserRole",
                column: "RoleId",
                principalSchema: "trk",
                principalTable: "Role",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserId",
                schema: "trk",
                table: "UserRole",
                column: "UserId",
                principalSchema: "trk",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
