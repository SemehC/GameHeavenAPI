using Microsoft.EntityFrameworkCore.Migrations;

namespace GameHeavenAPI.Migrations
{
    public partial class UpdatedGameRelationWithSystemRequirements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinimumSystemRequirementsId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecommendedSystemRequirementsId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_MinimumSystemRequirementsId",
                table: "Games",
                column: "MinimumSystemRequirementsId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_RecommendedSystemRequirementsId",
                table: "Games",
                column: "RecommendedSystemRequirementsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_MinimumSystemRequirements_MinimumSystemRequirementsId",
                table: "Games",
                column: "MinimumSystemRequirementsId",
                principalTable: "MinimumSystemRequirements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_RecommendedSystemRequirements_RecommendedSystemRequirementsId",
                table: "Games",
                column: "RecommendedSystemRequirementsId",
                principalTable: "RecommendedSystemRequirements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_MinimumSystemRequirements_MinimumSystemRequirementsId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_RecommendedSystemRequirements_RecommendedSystemRequirementsId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_MinimumSystemRequirementsId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_RecommendedSystemRequirementsId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "MinimumSystemRequirementsId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "RecommendedSystemRequirementsId",
                table: "Games");
        }
    }
}
