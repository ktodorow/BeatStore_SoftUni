using BeatStore_SoftUni.ViewModels.RatingDtos;
public interface IRatingService
{
    Task<double> GetAverageRatingAsync(Guid beatId);
    Task<bool> AddOrUpdateRatingAsync(RatingDTO ratingDto);
    Task<int?> GetUserRatingAsync(Guid userId, Guid beatId);
}
