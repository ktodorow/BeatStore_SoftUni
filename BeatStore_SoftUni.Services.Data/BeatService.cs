using BeatStore_SoftUni.Data.Models;
using BeatStore_SoftUni.Data.Repository.Interfaces;
using BeatStore_SoftUni.Services.Data.Interfaces;
using BeatStore_SoftUni.ViewModels.BeatDtos;

using Microsoft.EntityFrameworkCore;

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
                .Where(b => b.IsActive)
                .Include(b => b.Artist)
                .Select(b => new BeatIndexDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Genre = string.Join(", ", b.BeatGenres.Select(bg => bg.Genre.Name)),
                    CoverArtUrl = b.CoverArtUrl!,
                    Price = b.Price,
                    DateUploaded = b.DateUploaded,
                    AudioFileUrl = b.AudioFileUrl,
                    ArtistUsername = b.Artist.UserName,
                    IsActive = b.IsActive
                })
                .ToList());

            return beats;
        }
        public async Task CreateBeatAsync(CreateBeatDTO model, Guid artistId)
        {
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

            await this.beatRepository.AddAsync(beat);

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
            return await this.genreRepository.GetAllAsync();
        }
        public async Task<BeatDetailsDTO?> GetBeatDetailsAsync(Guid id, Guid userId)
        {
            var beat = await this.beatRepository.GetAllAttached()
                .Include(b => b.Artist)
                .Include(b => b.BeatPlaylists)
                .FirstOrDefaultAsync(b => b.Id == id && b.IsActive);

            if (beat == null)
            {
                return null;
            }

            return new BeatDetailsDTO
            {
                Id = beat.Id,
                Title = beat.Title,
                CoverArtUrl = beat.CoverArtUrl!,
                AudioFileUrl = beat.AudioFileUrl!,
                Price = beat.Price,
                UploadedBy = beat.Artist.UserName,
                DateUploaded = beat.DateUploaded,
                PlaylistsCount = beat.BeatPlaylists.Count,
                IsOwner = beat.ArtistId == userId,
                IsActive = beat.IsActive
            };
        }

        public async Task<EditBeatDTO?> GetBeatForEditAsync(Guid beatId, Guid userId)
        {
            var beat = await this.beatRepository
                .GetAllAttached()
                .Include(b => b.BeatGenres)
                .FirstOrDefaultAsync(b => b.Id == beatId && b.ArtistId == userId && b.IsActive);

            if (beat == null)
            {
                return null; 
            }

            return new EditBeatDTO
            {
                Id = beat.Id,
                Title = beat.Title,
                Price = beat.Price,
                AudioFileUrl = beat.AudioFileUrl,
                CoverArtUrl = beat.CoverArtUrl,
                GenreIds = beat.BeatGenres.Select(bg => bg.GenreId).ToList(),
                IsActive = beat.IsActive
            };
        }

        public async Task<bool> EditBeatAsync(EditBeatDTO model, Guid userId)
        {
            var beat = this.beatRepository
                .GetAllAttached()
                .Include(b => b.BeatGenres)
                .ThenInclude(bg => bg.Genre)
                .FirstOrDefault(b => b.Id == model.Id && b.ArtistId == userId);

            if (beat == null)
            {
                return false;
            }

            beat.Title = model.Title;
            beat.Price = model.Price;
            beat.AudioFileUrl = model.AudioFileUrl;
            beat.CoverArtUrl = model.CoverArtUrl;

            var currentGenreIds = beat.BeatGenres.Select(bg => bg.GenreId).ToHashSet();

            foreach (var genreId in model.GenreIds.Except(currentGenreIds))
            {
                var beatGenre = new BeatGenre
                {
                    BeatId = beat.Id,
                    GenreId = genreId
                };
                await this.beatGenreRepository.AddAsync(beatGenre);
            }

            foreach (var genreId in currentGenreIds.Except(model.GenreIds))
            {
                var beatGenreToRemove = beat.BeatGenres.FirstOrDefault(bg => bg.GenreId == genreId);
                if (beatGenreToRemove != null)
                {
                    await this.beatGenreRepository.DeleteAsync(beatGenreToRemove);
                }
            }

            return await this.beatRepository.UpdateAsync(beat);
        }

        public async Task<bool> SoftDeleteBeatAsync(Guid beatId, Guid userId)
        {
            var beat = await this.beatRepository.FirstOrDefaultAsync(b => b.Id == beatId && b.ArtistId == userId);

            if (beat == null)
            {
                return false;
            }

            beat.IsActive = false;

            return await this.beatRepository.UpdateAsync(beat);
        }
    }
}
