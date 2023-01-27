using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalEnv.Migrations
{
    /// <inheritdoc />
    public partial class testcaseinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalOutput",
                table: "TestcaseResults",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalOutput",
                table: "TestcaseResults");
        }
    }
}
