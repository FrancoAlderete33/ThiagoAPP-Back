using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FullStack.API.Migrations
{
    /// <inheritdoc />
    public partial class durationInMinutes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "duration",
                table: "Sleeps",
                newName: "durationInMinutes");

            migrationBuilder.RenameColumn(
                name: "duration",
                table: "Breastfeedings",
                newName: "durationInMinutes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "durationInMinutes",
                table: "Sleeps",
                newName: "duration");

            migrationBuilder.RenameColumn(
                name: "durationInMinutes",
                table: "Breastfeedings",
                newName: "duration");
        }
    }
}
