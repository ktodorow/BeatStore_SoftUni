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

        public async Task<bool> AddRatingAsync(RatingDTO ratingDto)
        {
            var existingRating = await ratingRepository
                .GetAllAttached()
                .AnyAsync(r => r.UserId == ratingDto.UserId && r.BeatId == ratingDto.BeatId);

            if (existingRating)
            {
                return false; 
            }

            var newRating = new Rating
            {
                Id = Guid.NewGuid(),
                UserId = ratingDto.UserId,
                BeatId = ratingDto.BeatId,
                Value = ratingDto.Value,
                DateRated = DateTime.UtcNow
            };

            await ratingRepository.AddAsync(newRating);
            return true;
        }

        public async Task<double> GetAverageRatingAsync(Guid beatId)
        {
            var ratings = await ratingRepository
                .GetAllAttached()
                .Where(r => r.BeatId == beatId)
                .ToListAsync();

            return ratings.Any() ? ratings.Average(r => r.Value) : 0.0;
        }

        public async Task<int?> GetUserRatingAsync(Guid beatId, Guid userId)
        {
            var rating = await ratingRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(r => r.BeatId == beatId && r.UserId == userId);

            return rating?.Value;
        }
    }
}
