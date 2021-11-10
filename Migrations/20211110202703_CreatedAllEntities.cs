using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameHeavenAPI.Migrations
{
    public partial class CreatedAllEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_developers_Games_GameId",
                table: "developers");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_publisher_PublisherId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_publisher_AspNetUsers_UserId",
                table: "publisher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_publisher",
                table: "publisher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_developers",
                table: "developers");

            migrationBuilder.RenameTable(
                name: "publisher",
                newName: "Publisher");

            migrationBuilder.RenameTable(
                name: "developers",
                newName: "Developers");

            migrationBuilder.RenameIndex(
                name: "IX_publisher_UserId",
                table: "Publisher",
                newName: "IX_Publisher_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_developers_GameId",
                table: "Developers",
                newName: "IX_Developers_GameId");

            migrationBuilder.AddColumn<int>(
                name: "FranchiseId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GamesCartId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publisher",
                table: "Publisher",
                column: "PublisherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Developers",
                table: "Developers",
                column: "DeveloperId");

            migrationBuilder.CreateTable(
                name: "Franchises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Franchises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameId = table.Column<int>(type: "int", nullable: true),
                    Alt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameImages_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MinimumSystemRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Storage = table.Column<int>(type: "int", nullable: false),
                    Ram = table.Column<int>(type: "int", nullable: false),
                    AdditionalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinimumSystemRequirements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecommendedSystemRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Storage = table.Column<int>(type: "int", nullable: false),
                    Ram = table.Column<int>(type: "int", nullable: false),
                    AdditionalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendedSystemRequirements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoinDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GamesCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamesCarts_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PcPartsCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcPartsCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PcPartsCarts_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PcParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PcPartsCartId = table.Column<int>(type: "int", nullable: true),
                    CPU_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPU_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPU_Price = table.Column<double>(type: "float", nullable: true),
                    Case_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Case_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Case_Price = table.Column<double>(type: "float", nullable: true),
                    Cooler_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cooler_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cooler_Price = table.Column<double>(type: "float", nullable: true),
                    GPU_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPU_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPU_Price = table.Column<double>(type: "float", nullable: true),
                    Memory_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Memory_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Memory_Price = table.Column<double>(type: "float", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    PowerSupply_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PowerSupply_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wattage = table.Column<int>(type: "int", nullable: true),
                    PowerSupply_Price = table.Column<double>(type: "float", nullable: true),
                    Storage_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Storage_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Storage_Price = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PcParts_PcPartsCarts_PcPartsCartId",
                        column: x => x.PcPartsCartId,
                        principalTable: "PcPartsCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PCSpecifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MotherBoardId = table.Column<int>(type: "int", nullable: true),
                    StorageId = table.Column<int>(type: "int", nullable: true),
                    CaseId = table.Column<int>(type: "int", nullable: true),
                    PowerSupplyId = table.Column<int>(type: "int", nullable: true),
                    CPUId = table.Column<int>(type: "int", nullable: true),
                    GPUId = table.Column<int>(type: "int", nullable: true),
                    MemoryId = table.Column<int>(type: "int", nullable: true),
                    CoolerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCSpecifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PCSpecifications_PcParts_CaseId",
                        column: x => x.CaseId,
                        principalTable: "PcParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PCSpecifications_PcParts_CoolerId",
                        column: x => x.CoolerId,
                        principalTable: "PcParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PCSpecifications_PcParts_CPUId",
                        column: x => x.CPUId,
                        principalTable: "PcParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PCSpecifications_PcParts_GPUId",
                        column: x => x.GPUId,
                        principalTable: "PcParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PCSpecifications_PcParts_MemoryId",
                        column: x => x.MemoryId,
                        principalTable: "PcParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PCSpecifications_PcParts_MotherBoardId",
                        column: x => x.MotherBoardId,
                        principalTable: "PcParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PCSpecifications_PcParts_PowerSupplyId",
                        column: x => x.PowerSupplyId,
                        principalTable: "PcParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PCSpecifications_PcParts_StorageId",
                        column: x => x.StorageId,
                        principalTable: "PcParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PCBuilds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PCSpecificationsId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PCBuilds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PCBuilds_PCSpecifications_PCSpecificationsId",
                        column: x => x.PCSpecificationsId,
                        principalTable: "PCSpecifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PCBuilds_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_FranchiseId",
                table: "Games",
                column: "FranchiseId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_GamesCartId",
                table: "Games",
                column: "GamesCartId");

            migrationBuilder.CreateIndex(
                name: "IX_GameImages_GameId",
                table: "GameImages",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesCarts_UserId",
                table: "GamesCarts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PCBuilds_PCSpecificationsId",
                table: "PCBuilds",
                column: "PCSpecificationsId");

            migrationBuilder.CreateIndex(
                name: "IX_PCBuilds_UserId",
                table: "PCBuilds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PcParts_PcPartsCartId",
                table: "PcParts",
                column: "PcPartsCartId");

            migrationBuilder.CreateIndex(
                name: "IX_PcPartsCarts_UserId",
                table: "PcPartsCarts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PCSpecifications_CaseId",
                table: "PCSpecifications",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PCSpecifications_CoolerId",
                table: "PCSpecifications",
                column: "CoolerId");

            migrationBuilder.CreateIndex(
                name: "IX_PCSpecifications_CPUId",
                table: "PCSpecifications",
                column: "CPUId");

            migrationBuilder.CreateIndex(
                name: "IX_PCSpecifications_GPUId",
                table: "PCSpecifications",
                column: "GPUId");

            migrationBuilder.CreateIndex(
                name: "IX_PCSpecifications_MemoryId",
                table: "PCSpecifications",
                column: "MemoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PCSpecifications_MotherBoardId",
                table: "PCSpecifications",
                column: "MotherBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_PCSpecifications_PowerSupplyId",
                table: "PCSpecifications",
                column: "PowerSupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_PCSpecifications_StorageId",
                table: "PCSpecifications",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Developers_Games_GameId",
                table: "Developers",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Franchises_FranchiseId",
                table: "Games",
                column: "FranchiseId",
                principalTable: "Franchises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GamesCarts_GamesCartId",
                table: "Games",
                column: "GamesCartId",
                principalTable: "GamesCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Publisher_PublisherId",
                table: "Games",
                column: "PublisherId",
                principalTable: "Publisher",
                principalColumn: "PublisherId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Publisher_AspNetUsers_UserId",
                table: "Publisher",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Developers_Games_GameId",
                table: "Developers");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Franchises_FranchiseId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_GamesCarts_GamesCartId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Publisher_PublisherId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Publisher_AspNetUsers_UserId",
                table: "Publisher");

            migrationBuilder.DropTable(
                name: "Franchises");

            migrationBuilder.DropTable(
                name: "GameImages");

            migrationBuilder.DropTable(
                name: "GamesCarts");

            migrationBuilder.DropTable(
                name: "MinimumSystemRequirements");

            migrationBuilder.DropTable(
                name: "PCBuilds");

            migrationBuilder.DropTable(
                name: "RecommendedSystemRequirements");

            migrationBuilder.DropTable(
                name: "PCSpecifications");

            migrationBuilder.DropTable(
                name: "PcParts");

            migrationBuilder.DropTable(
                name: "PcPartsCarts");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publisher",
                table: "Publisher");

            migrationBuilder.DropIndex(
                name: "IX_Games_FranchiseId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_GamesCartId",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Developers",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "FranchiseId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GamesCartId",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "Publisher",
                newName: "publisher");

            migrationBuilder.RenameTable(
                name: "Developers",
                newName: "developers");

            migrationBuilder.RenameIndex(
                name: "IX_Publisher_UserId",
                table: "publisher",
                newName: "IX_publisher_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Developers_GameId",
                table: "developers",
                newName: "IX_developers_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_publisher",
                table: "publisher",
                column: "PublisherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_developers",
                table: "developers",
                column: "DeveloperId");

            migrationBuilder.AddForeignKey(
                name: "FK_developers_Games_GameId",
                table: "developers",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_publisher_PublisherId",
                table: "Games",
                column: "PublisherId",
                principalTable: "publisher",
                principalColumn: "PublisherId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_publisher_AspNetUsers_UserId",
                table: "publisher",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
