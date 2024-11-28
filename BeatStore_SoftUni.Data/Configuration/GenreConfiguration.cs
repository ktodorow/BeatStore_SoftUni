namespace BeatStore_SoftUni.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(this.SeedGenres());
        }

        private List<Genre> SeedGenres()
        {
            List<Genre> movies = new List<Genre>()
            {
               new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "Hip Hop",
                    Description = "A genre characterized by rhythmic speech and beats."
                },
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "Jazz",
                    Description = "A genre known for swing and blue notes, and improvisation."
                },
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "Electronic",
                    Description = "A genre focused on electronic instruments and sound manipulation."
                },
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "Rock",
                    Description = "A genre with heavy use of guitars and a strong rhythm."
                }
            };

            return movies;
        }
    }
}
