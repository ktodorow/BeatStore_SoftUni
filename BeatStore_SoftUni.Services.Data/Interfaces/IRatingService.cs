using BeatStore_SoftUni.ViewModels.RatingDtos;

public interface IRatingService
{
    Task<bool> AddRatingAsync(RatingDTO ratingDto);
    Task<double> GetAverageRatingAsync(Guid beatId);
    Task<int?> GetUserRatingAsync(Guid beatId, Guid userId);
}