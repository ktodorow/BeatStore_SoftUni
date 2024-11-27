namespace BeatStore_SoftUni.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class BeatGenreConfiguration : IEntityTypeConfiguration<BeatGenre>
    {
        public void Configure(EntityTypeBuilder<BeatGenre> builder)
        {
            builder
                .HasKey(bg => new { bg.BeatId, bg.GenreId });

            builder
                .HasOne(bg => bg.Beat)
                .WithMany(b => b.BeatGenres)
                .HasForeignKey(bg => bg.BeatId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(bg => bg.Genre)
                .WithMany(g => g.BeatGenres)
                .HasForeignKey(bg => bg.GenreId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .ToTable("BeatsGenres");
        }
    }
}
