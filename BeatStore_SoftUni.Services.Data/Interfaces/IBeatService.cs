using BeatStore_SoftUni.Data.Models;
using BeatStore_SoftUni.ViewModels.BeatDtos;

namespace BeatStore_SoftUni.Services.Data.Interfaces
{
    public interface IBeatService
    {
        Task CreateBeatAsync(CreateBeatDTO model, Guid artistId);
        Task<IEnumerable<BeatIndexDTO>> GetAllBeatsAsync();
        Task<IEnumerable<Genre>> GetGenresAsync();
    }
}
