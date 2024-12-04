using BeatStore_SoftUni.ViewModels.CartDtos;

namespace BeatStore_SoftUni.Services.Data.Interfaces
{
    public interface ICartService
    {
        Task<CartDTO> GetCartAsync(Guid userId);
        Task<bool> AddToCartAsync(Guid userId, Guid beatId);
        Task RemoveFromCartAsync(Guid userId, Guid beatId);
        Task<bool> CheckoutCartAsync(Guid userId);
        Task<bool> IsBeatPurchasedAsync(Guid userId, Guid beatId);
    }
}
