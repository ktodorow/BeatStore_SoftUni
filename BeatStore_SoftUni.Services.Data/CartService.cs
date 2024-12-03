using BeatStore_SoftUni.Data.Models;
using BeatStore_SoftUni.Data.Repository.Interfaces;
using BeatStore_SoftUni.Services.Data.Interfaces;
using BeatStore_SoftUni.ViewModels.CartDtos;
using Microsoft.EntityFrameworkCore;

namespace BeatStore_SoftUni.Services.Data
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart, Guid> cartRepository;
        private readonly IRepository<CartItem, Guid> cartItemRepository;
        private readonly IRepository<ApplicationUser, Guid> userRepository;
        private readonly IRepository<Beat, Guid> beatRepository;
        private readonly IRepository<Purchase, Guid> purchaseRepository;

        public CartService(
            IRepository<Cart, Guid> cartRepository,
            IRepository<CartItem, Guid> cartItemRepository,
            IRepository<ApplicationUser, Guid> userRepository,
            IRepository<Beat, Guid> beatRepository,
            IRepository<Purchase, Guid> purchaseRepository)

        {
            this.cartRepository = cartRepository;
            this.cartItemRepository = cartItemRepository;
            this.userRepository = userRepository;
            this.beatRepository = beatRepository;
            this.purchaseRepository = purchaseRepository;
        }

        public async Task<CartDTO> GetCartAsync(Guid userId)
        {
            var cart = await cartRepository.GetAllAttached()
                .Where(c => c.UserId == userId)
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Beat)
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                return new CartDTO();
            }

            return new CartDTO
            {
                TotalPrice = cart.CartItems
                    .Where(ci => ci.Beat.IsActive)
                    .Sum(ci => ci.Beat.Price),
                Items = cart.CartItems
                    .Where(ci => ci.Beat.IsActive)
                    .Select(ci => new CartItemDTO
                    {
                        BeatId = ci.Beat.Id,
                        Title = ci.Beat.Title,
                        Price = ci.Beat.Price,
                        CoverArtUrl = ci.Beat.CoverArtUrl
                    })
                    .ToList()
            };
        }


        public async Task<bool> AddToCartAsync(Guid userId, Guid beatId)
        {
            var cart = await cartRepository.GetAllAttached()
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await cartRepository.AddAsync(cart);
            }

            var isAlreadyInCart = cart.CartItems.Any(ci => ci.BeatId == beatId);
            if (isAlreadyInCart) return false;

            var cartItem = new CartItem { BeatId = beatId, CartId = cart.Id };
            await cartItemRepository.AddAsync(cartItem);
            return true;
        }

        public async Task RemoveFromCartAsync(Guid userId, Guid beatId)
        {
            var cart = await cartRepository.GetAllAttached()
                .Where(c => c.UserId == userId)
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync();

            if (cart != null)
            {
                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.BeatId == beatId);
                if (cartItem != null)
                {
                    await cartItemRepository.DeleteAsync(cartItem);
                }
            }
        }

        public async Task<bool> CheckoutCartAsync(Guid userId)
        {
            var cart = await cartRepository.GetAllAttached()
                .Where(c => c.UserId == userId)
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Beat)
                .FirstOrDefaultAsync();

            if (cart == null || !cart.CartItems.Any()) return false;

            var user = await userRepository.GetByIdAsync(userId);
            var totalPrice = cart.CartItems.Sum(ci => ci.Beat.Price);

            if (user.Balance < totalPrice) return false;

            user.Balance -= totalPrice;
            await userRepository.UpdateAsync(user);

            foreach (var item in cart.CartItems)
            {
                await cartItemRepository.DeleteAsync(item);

                await purchaseRepository.AddAsync(new Purchase
                {
                    UserId = userId,
                    BeatId = item.BeatId,
                    Price = item.Beat.Price,
                    DatePurchased = DateTime.UtcNow
                });
            }

            return true;
        }
    }
}
