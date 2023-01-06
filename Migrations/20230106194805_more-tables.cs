using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalEnv.Migrations
{
    /// <inheritdoc />
    public partial class moretables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agent_Games_GameId",
                table: "Agent");

            migrationBuilder.DropForeignKey(
                name: "FK_Parameter_Games_GameId",
                table: "Parameter");

            migrationBuilder.DropForeignKey(
                name: "FK_ParameterRange_Parameter_ParameterId",
                table: "ParameterRange");

            migrationBuilder.DropForeignKey(
                name: "FK_TestcaseResult_Agent_AgentId",
                table: "TestcaseResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParameterRange",
                table: "ParameterRange");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parameter",
                table: "Parameter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agent",
                table: "Agent");

            migrationBuilder.RenameTable(
                name: "ParameterRange",
                newName: "Ranges");

            migrationBuilder.RenameTable(
                name: "Parameter",
                newName: "Parameters");

            migrationBuilder.RenameTable(
                name: "Agent",
                newName: "Agents");

            migrationBuilder.RenameIndex(
                name: "IX_ParameterRange_ParameterId",
                table: "Ranges",
                newName: "IX_Ranges_ParameterId");

            migrationBuilder.RenameIndex(
                name: "IX_Parameter_GameId",
                table: "Parameters",
                newName: "IX_Parameters_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Agent_GameId",
                table: "Agents",
                newName: "IX_Agents_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ranges",
                table: "Ranges",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parameters",
                table: "Parameters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agents",
                table: "Agents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Games_GameId",
                table: "Agents",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parameters_Games_GameId",
                table: "Parameters",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ranges_Parameters_ParameterId",
                table: "Ranges",
                column: "ParameterId",
                principalTable: "Parameters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestcaseResult_Agents_AgentId",
                table: "TestcaseResult",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Games_GameId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_Parameters_Games_GameId",
                table: "Parameters");

            migrationBuilder.DropForeignKey(
                name: "FK_Ranges_Parameters_ParameterId",
                table: "Ranges");

            migrationBuilder.DropForeignKey(
                name: "FK_TestcaseResult_Agents_AgentId",
                table: "TestcaseResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ranges",
                table: "Ranges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parameters",
                table: "Parameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agents",
                table: "Agents");

            migrationBuilder.RenameTable(
                name: "Ranges",
                newName: "ParameterRange");

            migrationBuilder.RenameTable(
                name: "Parameters",
                newName: "Parameter");

            migrationBuilder.RenameTable(
                name: "Agents",
                newName: "Agent");

            migrationBuilder.RenameIndex(
                name: "IX_Ranges_ParameterId",
                table: "ParameterRange",
                newName: "IX_ParameterRange_ParameterId");

            migrationBuilder.RenameIndex(
                name: "IX_Parameters_GameId",
                table: "Parameter",
                newName: "IX_Parameter_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Agents_GameId",
                table: "Agent",
                newName: "IX_Agent_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParameterRange",
                table: "ParameterRange",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parameter",
                table: "Parameter",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agent",
                table: "Agent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agent_Games_GameId",
                table: "Agent",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parameter_Games_GameId",
                table: "Parameter",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParameterRange_Parameter_ParameterId",
                table: "ParameterRange",
                column: "ParameterId",
                principalTable: "Parameter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestcaseResult_Agent_AgentId",
                table: "TestcaseResult",
                column: "AgentId",
                principalTable: "Agent",
                principalColumn: "Id");
        }
    }
}
