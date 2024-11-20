using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeatStore_SoftUni.Migrations
{
    /// <inheritdoc />
    public partial class CoverArtSupportBeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverArtUrl",
                table: "Beats",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverArtUrl",
                table: "Beats");
        }
    }
}
