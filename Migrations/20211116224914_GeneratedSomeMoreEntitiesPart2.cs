using Microsoft.EntityFrameworkCore.Migrations;

namespace GameHeavenAPI.Migrations
{
    public partial class GeneratedSomeMoreEntitiesPart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Platform_PlatformId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Games_GameId",
                table: "Genre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Platform",
                table: "Platform");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genre",
                table: "Genre");

            migrationBuilder.RenameTable(
                name: "Platform",
                newName: "Platforms");

            migrationBuilder.RenameTable(
                name: "Genre",
                newName: "Genres");

            migrationBuilder.RenameIndex(
                name: "IX_Genre_GameId",
                table: "Genres",
                newName: "IX_Genres_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Platforms",
                table: "Platforms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Platforms_PlatformId",
                table: "Games",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Games_GameId",
                table: "Genres",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Platforms_PlatformId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Games_GameId",
                table: "Genres");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Platforms",
                table: "Platforms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.RenameTable(
                name: "Platforms",
                newName: "Platform");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Genre");

            migrationBuilder.RenameIndex(
                name: "IX_Genres_GameId",
                table: "Genre",
                newName: "IX_Genre_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Platform",
                table: "Platform",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genre",
                table: "Genre",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Platform_PlatformId",
                table: "Games",
                column: "PlatformId",
                principalTable: "Platform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Games_GameId",
                table: "Genre",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
