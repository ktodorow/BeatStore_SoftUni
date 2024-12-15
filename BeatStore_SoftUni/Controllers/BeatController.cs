using BeatStore_SoftUni.Services.Data.Interfaces;
using BeatStore_SoftUni.ViewModels.BeatDtos;
using static BeatStore_SoftUni.Common.ErrorMessages;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeatStore_SoftUni.Controllers
{
    [Authorize]
    public class BeatController : BaseController
    {
        private readonly IBeatService beatService;

        public BeatController(IBeatService beatService)
        {
            this.beatService = beatService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var beats = await beatService.GetAllBeatsAsync();
            return View(beats);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Genres = await beatService.GetGenresAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBeatDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Genres = await beatService.GetGenresAsync();
                return View(model);
            }

            if (!ValidateUserId(out var userId))
            {
                return RedirectToAction("Index", "Beat");
            }

            await beatService.CreateBeatAsync(model, userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (!Guid.TryParse(id, out var beatId))
            {
                return NotFound();
            }

            if (!ValidateUserId(out var userId))
            {
                return Unauthorized();
            }

            var beatDetails = await beatService.GetBeatDetailsAsync(beatId, userId);
            if (beatDetails == null || !beatDetails.IsActive)
            {
                TempData["ErrorMessage"] = ErrBeatNoLongerAvailable;
                return RedirectToAction("Index", "Beat");
            }

            return View(beatDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ValidateUserId(out var userId))
            {
                return Unauthorized();
            }

            var success = await beatService.SoftDeleteBeatAsync(id, userId);
            if (!success)
            {
                TempData["ErrorMessage"] = ErrBeatNoLongerAvailable;
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}