using Microsoft.EntityFrameworkCore.Migrations;

namespace GameHeavenAPI.Migrations
{
    public partial class GeneratedSomeMoreEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameGenre");

            migrationBuilder.DropColumn(
                name: "DirectX",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropColumn(
                name: "DirectX",
                table: "MinimumSystemRequirements");

            migrationBuilder.AddColumn<int>(
                name: "DirectXId",
                table: "RecommendedSystemRequirements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DirectXId",
                table: "MinimumSystemRequirements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Genre",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DirectX",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectX", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecommendedSystemRequirements_DirectXId",
                table: "RecommendedSystemRequirements",
                column: "DirectXId");

            migrationBuilder.CreateIndex(
                name: "IX_MinimumSystemRequirements_DirectXId",
                table: "MinimumSystemRequirements",
                column: "DirectXId");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_GameId",
                table: "Genre",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlatformId",
                table: "Games",
                column: "PlatformId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_MinimumSystemRequirements_DirectX_DirectXId",
                table: "MinimumSystemRequirements",
                column: "DirectXId",
                principalTable: "DirectX",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecommendedSystemRequirements_DirectX_DirectXId",
                table: "RecommendedSystemRequirements",
                column: "DirectXId",
                principalTable: "DirectX",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Platform_PlatformId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Games_GameId",
                table: "Genre");

            migrationBuilder.DropForeignKey(
                name: "FK_MinimumSystemRequirements_DirectX_DirectXId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_RecommendedSystemRequirements_DirectX_DirectXId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropTable(
                name: "DirectX");

            migrationBuilder.DropTable(
                name: "Platform");

            migrationBuilder.DropIndex(
                name: "IX_RecommendedSystemRequirements_DirectXId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropIndex(
                name: "IX_MinimumSystemRequirements_DirectXId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropIndex(
                name: "IX_Genre_GameId",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Games_PlatformId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "DirectXId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropColumn(
                name: "DirectXId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "DirectX",
                table: "RecommendedSystemRequirements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DirectX",
                table: "MinimumSystemRequirements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GameGenre",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    GenresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenre", x => new { x.GamesId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_GameGenre_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGenre_Genre_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameGenre_GenresId",
                table: "GameGenre",
                column: "GenresId");
        }
    }
}
