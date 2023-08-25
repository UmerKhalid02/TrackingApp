using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingApp.Data.Migrations
{
    public partial class FixedIdColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserRoleId",
                schema: "trk",
                table: "UserRole",
                newName: "UserRoleID");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "trk",
                table: "Role",
                newName: "RoleID");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                schema: "trk",
                table: "Order",
                newName: "OrderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserRoleID",
                schema: "trk",
                table: "UserRole",
                newName: "UserRoleId");

            migrationBuilder.RenameColumn(
                name: "RoleID",
                schema: "trk",
                table: "Role",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                schema: "trk",
                table: "Order",
                newName: "OrderId");
        }
    }
}
