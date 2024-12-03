using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BeatStore_SoftUni.Services.Data.Interfaces;
using System.Threading.Tasks;

namespace BeatStore_SoftUni.Controllers
{
    [Authorize]
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            this.purchaseService = purchaseService;
        }

        [HttpGet]
        public async Task<IActionResult> Buy(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var purchaseDetails = await purchaseService.GetPurchaseDetailsAsync(id, userId);

            if (purchaseDetails == null)
            {
                return RedirectToAction("Index", "Beat");
            }

            return View(purchaseDetails);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(Guid beatId)
        {
            var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var success = await purchaseService.PlaceDirectOrderAsync(beatId, userId);

            if (!success)
            {
                TempData["ErrorMessage"] = "You do not have enough balance to complete the purchase.";
                return RedirectToAction("Buy", new { id = beatId }); 
            }

            TempData["SuccessMessage"] = "Purchase completed successfully!";
            return RedirectToAction("BoughtBeats");
        }


        [HttpGet]
        public async Task<IActionResult> BoughtBeats()
        {
            var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var purchases = await purchaseService.GetPurchasesAsync(userId);
            return View(purchases);
        }

    }
}
