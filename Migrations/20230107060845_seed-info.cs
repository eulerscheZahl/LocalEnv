using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalEnv.Migrations
{
    /// <inheritdoc />
    public partial class seedinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SeedInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Seed = table.Column<int>(type: "INTEGER", nullable: false),
                    ParameterId = table.Column<int>(type: "INTEGER", nullable: true),
                    ParameterValue = table.Column<double>(type: "REAL", nullable: false),
                    GameId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeedInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeedInfos_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SeedInfos_Parameters_ParameterId",
                        column: x => x.ParameterId,
                        principalTable: "Parameters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeedInfos_GameId",
                table: "SeedInfos",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_SeedInfos_ParameterId",
                table: "SeedInfos",
                column: "ParameterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeedInfos");
        }
    }
}
