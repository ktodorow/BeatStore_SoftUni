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
        public async Task<IActionResult> AddToCart(Guid beatId)
        {
            var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            await cartService.AddToCartAsync(userId, beatId);
            return RedirectToAction(nameof(Index));
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
                TempData["ErrorMessage"] = "You do not have enough balance to complete the purchase.";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index", "Purchase");
        }
    }
}
