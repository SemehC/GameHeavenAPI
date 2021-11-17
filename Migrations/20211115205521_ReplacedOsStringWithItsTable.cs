using Microsoft.EntityFrameworkCore.Migrations;

namespace GameHeavenAPI.Migrations
{
    public partial class ReplacedOsStringWithItsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Os",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropColumn(
                name: "Os",
                table: "MinimumSystemRequirements");

            migrationBuilder.AddColumn<int>(
                name: "OsId",
                table: "RecommendedSystemRequirements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OsId",
                table: "MinimumSystemRequirements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Os",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Os", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecommendedSystemRequirements_OsId",
                table: "RecommendedSystemRequirements",
                column: "OsId");

            migrationBuilder.CreateIndex(
                name: "IX_MinimumSystemRequirements_OsId",
                table: "MinimumSystemRequirements",
                column: "OsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MinimumSystemRequirements_Os_OsId",
                table: "MinimumSystemRequirements",
                column: "OsId",
                principalTable: "Os",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecommendedSystemRequirements_Os_OsId",
                table: "RecommendedSystemRequirements",
                column: "OsId",
                principalTable: "Os",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinimumSystemRequirements_Os_OsId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_RecommendedSystemRequirements_Os_OsId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropTable(
                name: "Os");

            migrationBuilder.DropIndex(
                name: "IX_RecommendedSystemRequirements_OsId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropIndex(
                name: "IX_MinimumSystemRequirements_OsId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropColumn(
                name: "OsId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropColumn(
                name: "OsId",
                table: "MinimumSystemRequirements");

            migrationBuilder.AddColumn<string>(
                name: "Os",
                table: "RecommendedSystemRequirements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Os",
                table: "MinimumSystemRequirements",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
