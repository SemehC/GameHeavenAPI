using Microsoft.EntityFrameworkCore.Migrations;

namespace GameHeavenAPI.Migrations
{
    public partial class AddedSomeMoreEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinimumSystemRequirements_DirectX_DirectXId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_MinimumSystemRequirements_Os_OsId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_RecommendedSystemRequirements_DirectX_DirectXId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_RecommendedSystemRequirements_Os_OsId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropTable(
                name: "DirectX");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Os",
                table: "Os");

            migrationBuilder.RenameTable(
                name: "Os",
                newName: "Oses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Oses",
                table: "Oses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DirectXVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectXVersions", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MinimumSystemRequirements_DirectXVersions_DirectXId",
                table: "MinimumSystemRequirements",
                column: "DirectXId",
                principalTable: "DirectXVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MinimumSystemRequirements_Oses_OsId",
                table: "MinimumSystemRequirements",
                column: "OsId",
                principalTable: "Oses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecommendedSystemRequirements_DirectXVersions_DirectXId",
                table: "RecommendedSystemRequirements",
                column: "DirectXId",
                principalTable: "DirectXVersions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecommendedSystemRequirements_Oses_OsId",
                table: "RecommendedSystemRequirements",
                column: "OsId",
                principalTable: "Oses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinimumSystemRequirements_DirectXVersions_DirectXId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_MinimumSystemRequirements_Oses_OsId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_RecommendedSystemRequirements_DirectXVersions_DirectXId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_RecommendedSystemRequirements_Oses_OsId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropTable(
                name: "DirectXVersions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Oses",
                table: "Oses");

            migrationBuilder.RenameTable(
                name: "Oses",
                newName: "Os");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Os",
                table: "Os",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_MinimumSystemRequirements_DirectX_DirectXId",
                table: "MinimumSystemRequirements",
                column: "DirectXId",
                principalTable: "DirectX",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MinimumSystemRequirements_Os_OsId",
                table: "MinimumSystemRequirements",
                column: "OsId",
                principalTable: "Os",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RecommendedSystemRequirements_DirectX_DirectXId",
                table: "RecommendedSystemRequirements",
                column: "DirectXId",
                principalTable: "DirectX",
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
    }
}
