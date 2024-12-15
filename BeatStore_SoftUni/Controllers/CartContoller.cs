using BeatStore_SoftUni.Services.Data.Interfaces;
using static BeatStore_SoftUni.Common.Messages;
using static BeatStore_SoftUni.Common.ErrorMessages;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeatStore_SoftUni.Controllers
{
    [Authorize]
    public class CartController : BaseController
    {
        private readonly ICartService cartService;
        private readonly IPurchaseService purchaseService;

        public CartController(ICartService cartService, IPurchaseService purchaseService)
        {
            this.cartService = cartService;
            this.purchaseService = purchaseService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!ValidateUserId(out var userId))
            {
                return RedirectToAction("Index", "Beat");
            }

            var cart = await cartService.GetCartAsync(userId);
            return View(cart);
        }

        [HttpPost]
        public async Task<JsonResult> AddToCart(Guid beatId)
        {
            if (!ValidateUserId(out var userId))
            {
                return Json(new { success = false, message = ErrUnableToRetrieveInformation });
            }

            var isPurchased = await purchaseService.IsBeatPurchasedAsync(userId, beatId);

            if (isPurchased)
            {
                return Json(new { success = false, message = ErrAlreadyPurchasedBeatCart });
            }

            var success = await cartService.AddToCartAsync(userId, beatId);

            return Json(new { success, message = success ? ItemAddedSuccessfully : ErrItemAlreadyAddedInCart });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(Guid beatId)
        {
            if (!ValidateUserId(out var userId))
            {
                return RedirectToAction("Index", "Beat");
            }

            await cartService.RemoveFromCartAsync(userId, beatId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            if (!ValidateUserId(out var userId))
            {
                return RedirectToAction("Index", "Beat");
            }

            var success = await purchaseService.CheckoutCartAsync(userId);

            if (!success)
            {
                TempData["ErrorMessage"] = ErrUnableToCompletePurchase;
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = CompletePurchase;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<JsonResult> GetCartStatus()
        {
            if (!ValidateUserId(out var userId))
            {
                return Json(new { hasItems = false });
            }

            var cart = await cartService.GetCartAsync(userId);
            return Json(new { hasItems = cart.Items.Any() });
        }
    }
}