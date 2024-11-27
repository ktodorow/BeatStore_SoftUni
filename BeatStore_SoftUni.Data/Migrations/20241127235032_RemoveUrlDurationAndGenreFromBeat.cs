using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeatStore_SoftUni.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUrlDurationAndGenreFromBeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Beats");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Beats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Beats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Beats",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
