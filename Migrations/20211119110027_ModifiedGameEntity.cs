using Microsoft.EntityFrameworkCore.Migrations;

namespace GameHeavenAPI.Migrations
{
    public partial class ModifiedGameEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameImages");

            migrationBuilder.AddColumn<string>(
                name: "CoverPath",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagesPath",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideosPath",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverPath",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ImagesPath",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "VideosPath",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "GameImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameId = table.Column<int>(type: "int", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameImages_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameImages_GameId",
                table: "GameImages",
                column: "GameId");
        }
    }
}
