using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalEnv.Migrations
{
    /// <inheritdoc />
    public partial class paramvalue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParameterValue_Parameters_ParameterId",
                table: "ParameterValue");

            migrationBuilder.DropForeignKey(
                name: "FK_ParameterValue_SeedInfos_SeedInfoId",
                table: "ParameterValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParameterValue",
                table: "ParameterValue");

            migrationBuilder.RenameTable(
                name: "ParameterValue",
                newName: "ParameterValues");

            migrationBuilder.RenameIndex(
                name: "IX_ParameterValue_SeedInfoId",
                table: "ParameterValues",
                newName: "IX_ParameterValues_SeedInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_ParameterValue_ParameterId",
                table: "ParameterValues",
                newName: "IX_ParameterValues_ParameterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParameterValues",
                table: "ParameterValues",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterValues_Parameters_ParameterId",
                table: "ParameterValues",
                column: "ParameterId",
                principalTable: "Parameters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterValues_SeedInfos_SeedInfoId",
                table: "ParameterValues",
                column: "SeedInfoId",
                principalTable: "SeedInfos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParameterValues_Parameters_ParameterId",
                table: "ParameterValues");

            migrationBuilder.DropForeignKey(
                name: "FK_ParameterValues_SeedInfos_SeedInfoId",
                table: "ParameterValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParameterValues",
                table: "ParameterValues");

            migrationBuilder.RenameTable(
                name: "ParameterValues",
                newName: "ParameterValue");

            migrationBuilder.RenameIndex(
                name: "IX_ParameterValues_SeedInfoId",
                table: "ParameterValue",
                newName: "IX_ParameterValue_SeedInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_ParameterValues_ParameterId",
                table: "ParameterValue",
                newName: "IX_ParameterValue_ParameterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParameterValue",
                table: "ParameterValue",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterValue_Parameters_ParameterId",
                table: "ParameterValue",
                column: "ParameterId",
                principalTable: "Parameters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterValue_SeedInfos_SeedInfoId",
                table: "ParameterValue",
                column: "SeedInfoId",
                principalTable: "SeedInfos",
                principalColumn: "Id");
        }
    }
}
