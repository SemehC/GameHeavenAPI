using Microsoft.EntityFrameworkCore.Migrations;

namespace GameHeavenAPI.Migrations
{
    public partial class AddedSomeMoreEntitiesAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_StatusId",
                table: "Games",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Statuses_StatusId",
                table: "Games",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Statuses_StatusId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_StatusId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
