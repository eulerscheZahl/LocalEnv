using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalEnv.Migrations
{
    /// <inheritdoc />
    public partial class seedinfo2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeedInfos_Parameters_ParameterId",
                table: "SeedInfos");

            migrationBuilder.DropIndex(
                name: "IX_SeedInfos_ParameterId",
                table: "SeedInfos");

            migrationBuilder.DropColumn(
                name: "ParameterId",
                table: "SeedInfos");

            migrationBuilder.DropColumn(
                name: "ParameterValue",
                table: "SeedInfos");

            migrationBuilder.AddColumn<bool>(
                name: "Maximize",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ParameterValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParameterId = table.Column<int>(type: "INTEGER", nullable: true),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    SeedInfoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParameterValue_Parameters_ParameterId",
                        column: x => x.ParameterId,
                        principalTable: "Parameters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ParameterValue_SeedInfos_SeedInfoId",
                        column: x => x.SeedInfoId,
                        principalTable: "SeedInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParameterValue_ParameterId",
                table: "ParameterValue",
                column: "ParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterValue_SeedInfoId",
                table: "ParameterValue",
                column: "SeedInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParameterValue");

            migrationBuilder.DropColumn(
                name: "Maximize",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "ParameterId",
                table: "SeedInfos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ParameterValue",
                table: "SeedInfos",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_SeedInfos_ParameterId",
                table: "SeedInfos",
                column: "ParameterId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeedInfos_Parameters_ParameterId",
                table: "SeedInfos",
                column: "ParameterId",
                principalTable: "Parameters",
                principalColumn: "Id");
        }
    }
}
