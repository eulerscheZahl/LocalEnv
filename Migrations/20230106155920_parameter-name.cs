using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalEnv.Migrations
{
    /// <inheritdoc />
    public partial class parametername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Parameter",
                newName: "InternalName");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Parameter",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Parameter");

            migrationBuilder.RenameColumn(
                name: "InternalName",
                table: "Parameter",
                newName: "Name");
        }
    }
}
