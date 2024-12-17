using BeatStore_SoftUni.Data.Models;
using BeatStore_SoftUni.ViewModels.BeatDtos;

namespace BeatStore_SoftUni.Services.Data.Interfaces
{
    public interface IBeatService
    {
        Task CreateBeatAsync(CreateBeatDTO model, Guid artistId);
        Task<IEnumerable<BeatIndexDTO>> GetAllBeatsAsync();
        Task<IEnumerable<Genre>> GetGenresAsync();
        Task<BeatDetailsDTO?> GetBeatDetailsAsync(Guid id, Guid userId);
        Task<EditBeatDTO?> GetBeatForEditAsync(Guid beatId, Guid userId);
        Task<bool> EditBeatAsync(EditBeatDTO model, Guid userId);
        Task<bool> SoftDeleteBeatAsync(Guid beatId, Guid userId);
        Task<IEnumerable<BeatIndexDTO>> SearchBeatsAsync(string searchQuery, Guid? genreId, string sortOption);

    }
}
