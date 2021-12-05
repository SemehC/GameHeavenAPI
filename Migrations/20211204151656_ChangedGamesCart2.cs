using Microsoft.EntityFrameworkCore.Migrations;

namespace GameHeavenAPI.Migrations
{
    public partial class ChangedGamesCart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GamesCarts_GamesCartId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_GamesCartId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GamesCartId",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "GameGamesCart",
                columns: table => new
                {
                    CartsId = table.Column<int>(type: "int", nullable: false),
                    GamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGamesCart", x => new { x.CartsId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_GameGamesCart_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGamesCart_GamesCarts_CartsId",
                        column: x => x.CartsId,
                        principalTable: "GamesCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameGamesCart_GamesId",
                table: "GameGamesCart",
                column: "GamesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameGamesCart");

            migrationBuilder.AddColumn<int>(
                name: "GamesCartId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_GamesCartId",
                table: "Games",
                column: "GamesCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GamesCarts_GamesCartId",
                table: "Games",
                column: "GamesCartId",
                principalTable: "GamesCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
