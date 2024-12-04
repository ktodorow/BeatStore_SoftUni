using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BeatStore_SoftUni.Services.Data.Interfaces;
using System.Threading.Tasks;

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
            var success = await cartService.AddToCartAsync(userId, beatId);

            if (success)
            {
                return Json(new { success = true, message = "Item added to your cart successfully." });
            }
            else
            {
                return Json(new { success = false, message = "This item is already in your cart." });
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
                TempData["ErrorMessage"] = "Unable to complete the purchase. Please check your balance or the items in your cart.";
                return RedirectToAction(nameof(Index));
            }

            if (TempData["WarningMessage"] != null)
            {
                TempData["SuccessMessage"] = "Your purchase was completed successfully! However, some items were already purchased.";
            }
            else
            {
                TempData["SuccessMessage"] = "Your purchase was completed successfully!";
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
