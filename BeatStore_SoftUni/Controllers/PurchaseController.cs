using BeatStore_SoftUni.Services.Data.Interfaces;
using static BeatStore_SoftUni.Common.Messages;
using static BeatStore_SoftUni.Common.ErrorMessages;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeatStore_SoftUni.Controllers
{
    [Authorize]
    public class PurchaseController : BaseController
    {
        private readonly IPurchaseService purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            this.purchaseService = purchaseService;
        }

        [HttpGet]
        public async Task<IActionResult> Buy(Guid id)
        {
            if (!ValidateUserId(out var userId))
            {
                return RedirectToAction("Index", "Beat");
            }

            var purchaseDetails = await purchaseService.GetPurchaseDetailsAsync(id, userId);

            if (purchaseDetails == null)
            {
                TempData["ErrorMessage"] = ErrBeatNoLongerAvailable;
                return RedirectToAction("Index", "Beat");
            }

            return View(purchaseDetails);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(Guid beatId)
        {
            if (!ValidateUserId(out var userId))
            {
                return RedirectToAction("Index", "Beat");
            }

            var success = await purchaseService.PlaceDirectOrderAsync(beatId, userId);

            if (!success)
            {
                TempData["ErrorMessage"] = ErrPurchase;
                return RedirectToAction("Buy", new { id = beatId });
            }

            TempData["SuccessMessage"] = CompletePurchase;
            return RedirectToAction("BoughtBeats");
        }

        [HttpGet]
        public async Task<IActionResult> BoughtBeats()
        {
            if (!ValidateUserId(out var userId))
            {
                return Unauthorized();
            }

            var purchases = await purchaseService.GetPurchasesAsync(userId);
            return View(purchases);
        }
    }
}
