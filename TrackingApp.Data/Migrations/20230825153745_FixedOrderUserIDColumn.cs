using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingApp.Data.Migrations
{
    public partial class FixedOrderUserIDColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_UserId",
                schema: "trk",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "trk",
                table: "Order",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Order_UserId",
                schema: "trk",
                table: "Order",
                newName: "IX_Order_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_UserID",
                schema: "trk",
                table: "Order",
                column: "UserID",
                principalSchema: "trk",
                principalTable: "User",
                principalColumn: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_UserID",
                schema: "trk",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "UserID",
                schema: "trk",
                table: "Order",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_UserID",
                schema: "trk",
                table: "Order",
                newName: "IX_Order_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_UserId",
                schema: "trk",
                table: "Order",
                column: "UserId",
                principalSchema: "trk",
                principalTable: "User",
                principalColumn: "UserID");
        }
    }
}
