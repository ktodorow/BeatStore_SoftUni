using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeatStore_SoftUni.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedGenreAndIsActiveBeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Beats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("3a0de8f5-7b99-40cc-9690-39941f0efbdf"), "A genre with heavy use of guitars and a strong rhythm.", "Rock" },
                    { new Guid("7a2e7756-263a-43ae-b005-cbb00cb28a82"), "A genre characterized by rhythmic speech and beats.", "Hip Hop" },
                    { new Guid("c7a077a6-7810-4f24-b207-3a8a454d67aa"), "A genre known for swing and blue notes, and improvisation.", "Jazz" },
                    { new Guid("d86c5ac2-225b-4785-bf37-384006842893"), "A genre focused on electronic instruments and sound manipulation.", "Electronic" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("3a0de8f5-7b99-40cc-9690-39941f0efbdf"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("7a2e7756-263a-43ae-b005-cbb00cb28a82"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("c7a077a6-7810-4f24-b207-3a8a454d67aa"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("d86c5ac2-225b-4785-bf37-384006842893"));

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Beats");
        }
    }
}
