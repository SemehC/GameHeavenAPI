using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameHeavenAPI.Migrations
{
    public partial class addeddevlopertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publisher_AspNetUsers_UserId",
                table: "Publisher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publisher",
                table: "Publisher");

            migrationBuilder.RenameTable(
                name: "Publisher",
                newName: "publisher");

            migrationBuilder.RenameIndex(
                name: "IX_Publisher_UserId",
                table: "publisher",
                newName: "IX_publisher_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_publisher",
                table: "publisher",
                column: "PublisherId");

            migrationBuilder.CreateTable(
                name: "developers",
                columns: table => new
                {
                    DeveloperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeveloperName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeveloperEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeveloperDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeveloperPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoinDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_developers", x => x.DeveloperId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_publisher_AspNetUsers_UserId",
                table: "publisher",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_publisher_AspNetUsers_UserId",
                table: "publisher");

            migrationBuilder.DropTable(
                name: "developers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_publisher",
                table: "publisher");

            migrationBuilder.RenameTable(
                name: "publisher",
                newName: "Publisher");

            migrationBuilder.RenameIndex(
                name: "IX_publisher_UserId",
                table: "Publisher",
                newName: "IX_Publisher_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publisher",
                table: "Publisher",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publisher_AspNetUsers_UserId",
                table: "Publisher",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
