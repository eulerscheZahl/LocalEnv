using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalEnv.Migrations
{
    /// <inheritdoc />
    public partial class gameseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestcaseResult_Agents_AgentId",
                table: "TestcaseResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestcaseResult",
                table: "TestcaseResult");

            migrationBuilder.RenameTable(
                name: "TestcaseResult",
                newName: "TestcaseResults");

            migrationBuilder.RenameIndex(
                name: "IX_TestcaseResult_AgentId",
                table: "TestcaseResults",
                newName: "IX_TestcaseResults_AgentId");

            migrationBuilder.AddColumn<int>(
                name: "SeedCount",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeedStart",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestcaseResults",
                table: "TestcaseResults",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestcaseResults_Agents_AgentId",
                table: "TestcaseResults",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestcaseResults_Agents_AgentId",
                table: "TestcaseResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestcaseResults",
                table: "TestcaseResults");

            migrationBuilder.DropColumn(
                name: "SeedCount",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "SeedStart",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "TestcaseResults",
                newName: "TestcaseResult");

            migrationBuilder.RenameIndex(
                name: "IX_TestcaseResults_AgentId",
                table: "TestcaseResult",
                newName: "IX_TestcaseResult_AgentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestcaseResult",
                table: "TestcaseResult",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestcaseResult_Agents_AgentId",
                table: "TestcaseResult",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id");
        }
    }
}
