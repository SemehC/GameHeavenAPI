using Microsoft.EntityFrameworkCore.Migrations;

namespace GameHeavenAPI.Migrations
{
    public partial class ChangedGamesCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "GamesCarts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GamesCarts_UserId",
                table: "GamesCarts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamesCarts_AspNetUsers_UserId",
                table: "GamesCarts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamesCarts_AspNetUsers_UserId",
                table: "GamesCarts");

            migrationBuilder.DropIndex(
                name: "IX_GamesCarts_UserId",
                table: "GamesCarts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GamesCarts");
        }
    }
}
