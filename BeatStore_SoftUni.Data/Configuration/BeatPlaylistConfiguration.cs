namespace BeatStore_SoftUni.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class BeatPlaylistConfiguration : IEntityTypeConfiguration<BeatPlaylist>
    {
        public void Configure(EntityTypeBuilder<BeatPlaylist> builder)
        {
            builder.HasKey(bp => new { bp.BeatId, bp.PlaylistId });

            builder
                .HasOne(bp => bp.Beat)
                .WithMany(b => b.BeatPlaylists)
                .HasForeignKey(bp => bp.BeatId)
                .OnDelete(DeleteBehavior.NoAction); // Retain cascade here

            builder
                .HasOne(bp => bp.Playlist)
                .WithMany(p => p.BeatPlaylists)
                .HasForeignKey(bp => bp.PlaylistId)
                .OnDelete(DeleteBehavior.NoAction); // Disable cascade here

            builder
                .ToTable("BeatsPlaylists");
        }
    }
}
