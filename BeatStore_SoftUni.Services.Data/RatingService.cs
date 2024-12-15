using BeatStore_SoftUni.Data.Models;
using BeatStore_SoftUni.Data.Repository.Interfaces;
using BeatStore_SoftUni.ViewModels.RatingDtos;

using Microsoft.EntityFrameworkCore;

namespace BeatStore_SoftUni.Services.Data
{
    public class RatingService : IRatingService
    {
        private readonly IRepository<Rating, Guid> ratingRepository;

        public RatingService(IRepository<Rating, Guid> ratingRepository)
        {
            this.ratingRepository = ratingRepository;
        }

        public async Task<bool> AddOrUpdateRatingAsync(RatingDTO ratingDto)
        {
            var existingRating = await ratingRepository.GetAllAttached()
                .FirstOrDefaultAsync(r => r.UserId == ratingDto.UserId && r.BeatId == ratingDto.BeatId);

            if (existingRating != null)
            {
                existingRating.Value = ratingDto.Value;
                existingRating.DateRated = DateTime.UtcNow;
                await ratingRepository.UpdateAsync(existingRating);
            }
            else
            {
                var newRating = new Rating
                {
                    Id = Guid.NewGuid(),
                    UserId = ratingDto.UserId,
                    BeatId = ratingDto.BeatId,
                    Value = ratingDto.Value,
                    DateRated = DateTime.UtcNow
                };
                await ratingRepository.AddAsync(newRating);
            }

            return true;
        }

        public async Task<double> GetAverageRatingAsync(Guid beatId)
        {
            var ratings = await ratingRepository.GetAllAttached()
                .Where(r => r.BeatId == beatId)
                .Select(r => r.Value)
                .ToListAsync();

            return ratings.Any() ? ratings.Average() : 0;
        }

        public async Task<int?> GetUserRatingAsync(Guid userId, Guid beatId)
        {
            var rating = await ratingRepository.GetAllAttached()
                .FirstOrDefaultAsync(r => r.UserId == userId && r.BeatId == beatId);

            return rating?.Value;
        }
    }
}
