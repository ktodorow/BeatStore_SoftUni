using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeatStore_SoftUni.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("04162ce0-bc10-433e-b84c-bcf66343ea0d"), "A genre known for swing and blue notes, and improvisation.", "Jazz" },
                    { new Guid("3002c4e9-875a-4ed6-8d1c-1d51e8a4721d"), "A genre with heavy use of guitars and a strong rhythm.", "Rock" },
                    { new Guid("8efe3ca8-203a-4c33-b311-5063c017a30a"), "A genre characterized by rhythmic speech and beats.", "Hip Hop" },
                    { new Guid("90d2a529-5650-4d3c-aeee-384c4c17e17a"), "A genre focused on electronic instruments and sound manipulation.", "Electronic" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("04162ce0-bc10-433e-b84c-bcf66343ea0d"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("3002c4e9-875a-4ed6-8d1c-1d51e8a4721d"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("8efe3ca8-203a-4c33-b311-5063c017a30a"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("90d2a529-5650-4d3c-aeee-384c4c17e17a"));
        }
    }
}
