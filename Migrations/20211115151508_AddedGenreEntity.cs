using Microsoft.EntityFrameworkCore.Migrations;

namespace GameHeavenAPI.Migrations
{
    public partial class AddedGenreEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CPUId",
                table: "RecommendedSystemRequirements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DirectX",
                table: "RecommendedSystemRequirements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GPUId",
                table: "RecommendedSystemRequirements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Os",
                table: "RecommendedSystemRequirements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CPUId",
                table: "MinimumSystemRequirements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DirectX",
                table: "MinimumSystemRequirements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GPUId",
                table: "MinimumSystemRequirements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Os",
                table: "MinimumSystemRequirements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

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
                name: "IX_RecommendedSystemRequirements_CPUId",
                table: "RecommendedSystemRequirements",
                column: "CPUId");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendedSystemRequirements_GPUId",
                table: "RecommendedSystemRequirements",
                column: "GPUId");

            migrationBuilder.CreateIndex(
                name: "IX_MinimumSystemRequirements_CPUId",
                table: "MinimumSystemRequirements",
                column: "CPUId");

            migrationBuilder.CreateIndex(
                name: "IX_MinimumSystemRequirements_GPUId",
                table: "MinimumSystemRequirements",
                column: "GPUId");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenre_GenresId",
                table: "GameGenre",
                column: "GenresId");

            migrationBuilder.AddForeignKey(
                name: "FK_MinimumSystemRequirements_PcParts_CPUId",
                table: "MinimumSystemRequirements",
                column: "CPUId",
                principalTable: "PcParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MinimumSystemRequirements_PcParts_GPUId",
                table: "MinimumSystemRequirements",
                column: "GPUId",
                principalTable: "PcParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecommendedSystemRequirements_PcParts_CPUId",
                table: "RecommendedSystemRequirements",
                column: "CPUId",
                principalTable: "PcParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecommendedSystemRequirements_PcParts_GPUId",
                table: "RecommendedSystemRequirements",
                column: "GPUId",
                principalTable: "PcParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinimumSystemRequirements_PcParts_CPUId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_MinimumSystemRequirements_PcParts_GPUId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_RecommendedSystemRequirements_PcParts_CPUId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_RecommendedSystemRequirements_PcParts_GPUId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropTable(
                name: "GameGenre");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_RecommendedSystemRequirements_CPUId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropIndex(
                name: "IX_RecommendedSystemRequirements_GPUId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropIndex(
                name: "IX_MinimumSystemRequirements_CPUId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropIndex(
                name: "IX_MinimumSystemRequirements_GPUId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropColumn(
                name: "CPUId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropColumn(
                name: "DirectX",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropColumn(
                name: "GPUId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropColumn(
                name: "Os",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropColumn(
                name: "CPUId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropColumn(
                name: "DirectX",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropColumn(
                name: "GPUId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropColumn(
                name: "Os",
                table: "MinimumSystemRequirements");
        }
    }
}
