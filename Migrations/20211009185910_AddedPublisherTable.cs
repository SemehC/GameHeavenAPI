using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameHeavenAPI.Migrations
{
    public partial class AddedPublisherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    PublisherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublisherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublisherEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublisherDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublisherPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoinDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.PublisherId);
                    table.ForeignKey(
                        name: "FK_Publisher_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publisher_UserId",
                table: "Publisher",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Publisher");
        }
    }
}
