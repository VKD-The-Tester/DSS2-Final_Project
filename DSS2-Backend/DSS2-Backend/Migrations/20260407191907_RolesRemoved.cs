using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSS2_Backend.Migrations
{
    /// <inheritdoc />
    public partial class RolesRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
