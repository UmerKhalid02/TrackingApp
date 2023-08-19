using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingApp.Data.Migrations
{
    public partial class AddBaseEntityInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "trk",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "trk",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "trk",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "trk",
                table: "User",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "trk",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "trk",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "trk",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "trk",
                table: "User");
        }
    }
}
