using BeatStore_SoftUni.ViewModels.PurchaseDtos;

namespace BeatStore_SoftUni.Services.Data.Interfaces
{
    public interface IPurchaseService
    {
        Task<PurchaseDTO?> GetPurchaseDetailsAsync(Guid beatId, Guid userId);
        Task<bool> PlaceDirectOrderAsync(Guid beatId, Guid userId);
        Task<IEnumerable<PurchaseDTO>> GetPurchasesAsync(Guid userId); // Add this method
    }
}
