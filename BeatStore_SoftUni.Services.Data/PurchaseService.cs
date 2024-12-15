using BeatStore_SoftUni.Data.Models;
using BeatStore_SoftUni.Data.Repository.Interfaces;
using BeatStore_SoftUni.Services.Data.Interfaces;
using BeatStore_SoftUni.ViewModels.PurchaseDtos;

using Microsoft.EntityFrameworkCore;


namespace BeatStore_SoftUni.Services.Data
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IRepository<Purchase, Guid> purchaseRepository;
        private readonly IRepository<ApplicationUser, Guid> userRepository;
        private readonly IRepository<Beat, Guid> beatRepository;

        public PurchaseService(
            IRepository<Purchase, Guid> purchaseRepository,
            IRepository<ApplicationUser, Guid> userRepository,
            IRepository<Beat, Guid> beatRepository)
        {
            this.purchaseRepository = purchaseRepository;
            this.userRepository = userRepository;
            this.beatRepository = beatRepository;
        }

        public async Task<PurchaseDTO?> GetPurchaseDetailsAsync(Guid beatId, Guid userId)
        {
            var beat = await beatRepository.GetAllAttached()
                .Where(b => b.Id == beatId && b.IsActive) // Ensure beat is active
                .FirstOrDefaultAsync();

            if (beat == null) return null;

            return new PurchaseDTO
            {
                Id = beat.Id,
                Title = beat.Title,
                Price = beat.Price,
                CoverArtUrl = beat.CoverArtUrl
            };
        }

        public async Task<bool> PlaceDirectOrderAsync(Guid beatId, Guid userId)
        {
            var alreadyPurchased = await purchaseRepository.GetAllAttached()
                .AnyAsync(p => p.UserId == userId && p.BeatId == beatId);

            if (alreadyPurchased) return false;

            var user = await userRepository.GetByIdAsync(userId);
            var beat = await beatRepository.GetAllAttached()
                .Where(b => b.Id == beatId && b.IsActive) // Ensure beat is active
                .FirstOrDefaultAsync();

            if (beat == null || user.Balance < beat.Price) return false;

            user.Balance -= beat.Price;
            await userRepository.UpdateAsync(user);

            await purchaseRepository.AddAsync(new Purchase
            {
                UserId = userId,
                BeatId = beatId,
                Price = beat.Price,
                DatePurchased = DateTime.UtcNow
            });

            return true;
        }

        public async Task<IEnumerable<PurchaseDTO>> GetPurchasesAsync(Guid userId)
        {
            var purchases = await Task.Run(() =>
                purchaseRepository.GetAllAttached()
                    .Where(p => p.UserId == userId)
                    .Select(p => new PurchaseDTO
                    {
                        Id = p.BeatId,
                        Title = p.Beat.Title,
                        Price = p.Price,
                        //CoverArtUrl = p.Beat.CoverArtUrl,
                        DatePurchased = p.DatePurchased
                    })
                    .ToList()
            );

            return purchases;
        }
    }
}
