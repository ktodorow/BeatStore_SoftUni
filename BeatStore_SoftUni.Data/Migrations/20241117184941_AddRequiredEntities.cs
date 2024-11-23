using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeatStore_SoftUni.Migrations
{
    /// <inheritdoc />
    public partial class AddRequiredEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeatPlaylist");

            migrationBuilder.CreateTable(
                name: "BeatsPlaylists",
                columns: table => new
                {
                    BeatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeatsPlaylists", x => new { x.BeatId, x.PlaylistId });
                    table.ForeignKey(
                        name: "FK_BeatsPlaylists_Beats_BeatId",
                        column: x => x.BeatId,
                        principalTable: "Beats",
                        principalColumn: "BeatId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_BeatsPlaylists_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "PlaylistId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeatsPlaylists_PlaylistId",
                table: "BeatsPlaylists",
                column: "PlaylistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeatsPlaylists");

            migrationBuilder.CreateTable(
                name: "BeatPlaylist",
                columns: table => new
                {
                    BeatsBeatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistsPlaylistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeatPlaylist", x => new { x.BeatsBeatId, x.PlaylistsPlaylistId });
                    table.ForeignKey(
                        name: "FK_BeatPlaylist_Beats_BeatsBeatId",
                        column: x => x.BeatsBeatId,
                        principalTable: "Beats",
                        principalColumn: "BeatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeatPlaylist_Playlists_PlaylistsPlaylistId",
                        column: x => x.PlaylistsPlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "PlaylistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeatPlaylist_PlaylistsPlaylistId",
                table: "BeatPlaylist",
                column: "PlaylistsPlaylistId");
        }
    }
}
