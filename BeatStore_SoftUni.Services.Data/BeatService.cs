using BeatStore_SoftUni.Data.Models;
using BeatStore_SoftUni.Data.Repository.Interfaces;
using BeatStore_SoftUni.Services.Data.Interfaces;
using BeatStore_SoftUni.ViewModels.BeatDtos;

namespace BeatStore_SoftUni.Services.Data
{
    public class BeatService : IBeatService
    {
        private readonly IRepository<Beat, Guid> beatRepository;
        private readonly IRepository<BeatGenre, Guid> beatGenreRepository;
        private readonly IRepository<Genre, Guid> genreRepository;

        public BeatService(
            IRepository<Beat, Guid> beatRepository,
            IRepository<BeatGenre, Guid> beatGenreRepository,
            IRepository<Genre, Guid> genreRepository)
        {
            this.beatRepository = beatRepository;
            this.beatGenreRepository = beatGenreRepository;
            this.genreRepository = genreRepository;
        }
        public async Task<IEnumerable<BeatIndexDTO>> GetAllBeatsAsync()
        {
            var beats = await Task.Run(() => this.beatRepository
                .GetAllAttached()
                .Select(b => new BeatIndexDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Genre = string.Join(", ", b.BeatGenres.Select(bg => bg.Genre.Name)),
                    CoverArtUrl = b.CoverArtUrl!,
                    Price = b.Price,
                    DateUploaded = b.DateUploaded,
                    AudioFileUrl = b.AudioFileUrl // Populate the new property
                })
                .ToList());

            return beats;
        }

        public async Task CreateBeatAsync(CreateBeatDTO model, Guid artistId)
        {
            // Create the beat entity
            var beat = new Beat
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                ArtistId = artistId,
                Price = model.Price,
                AudioFileUrl = model.AudioFileUrl,
                CoverArtUrl = model.CoverArtUrl,
                DateUploaded = DateTime.UtcNow,
            };

            // Save the beat to the database
            await this.beatRepository.AddAsync(beat);

            // Add the associated genres
            foreach (var genreId in model.GenreIds)
            {
                var beatGenre = new BeatGenre
                {
                    BeatId = beat.Id,
                    GenreId = genreId
                };

                await this.beatGenreRepository.AddAsync(beatGenre);
            }
        }

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            // Return all available genres
            return await this.genreRepository.GetAllAsync();
        }
    }
}
