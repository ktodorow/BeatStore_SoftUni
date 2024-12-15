using BeatStore_SoftUni.Services.Data.Interfaces;
using static BeatStore_SoftUni.Common.Messages;
using static BeatStore_SoftUni.Common.ErrorMessages;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BeatStore_SoftUni.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var cart = await cartService.GetCartAsync(userId);
            return View(cart);
        }

        [HttpPost]
        public async Task<JsonResult> AddToCart(Guid beatId)
        {
            var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

            // Check if the beat is already purchased
            var isPurchased = await cartService.IsBeatPurchasedAsync(userId, beatId);

            if (isPurchased)
            {
                return Json(new { success = false, message = ErrAlreadyPurchasedBeatCart });
            }

            var success = await cartService.AddToCartAsync(userId, beatId);

            if (success)
            {
                return Json(new { success = true, message = ItemAddedSuccessfully });
            }
            else
            {
                return Json(new { success = false, message = ErrItemAlreadyAddedInCart });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(Guid beatId)
        {
            var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            await cartService.RemoveFromCartAsync(userId, beatId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var success = await cartService.CheckoutCartAsync(userId);

            if (!success)
            {
                TempData["ErrorMessage"] = ErrUnableToCompletePurchase;
                return RedirectToAction(nameof(Index));
            }

            if (TempData["WarningMessage"] != null)
            {
                TempData["SuccessMessage"] = CompletePurchasePartly;
            }
            else
            {
                TempData["SuccessMessage"] = CompletePurchase;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<JsonResult> GetCartStatus()
        {
            var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var cart = await cartService.GetCartAsync(userId);

            return Json(new { hasItems = cart.Items.Any() });
        }

    }
}
