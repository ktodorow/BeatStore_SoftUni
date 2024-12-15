using BeatStore_SoftUni.Services.Data.Interfaces;
using BeatStore_SoftUni.ViewModels.BeatDtos;
using static BeatStore_SoftUni.Common.ErrorMessages;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeatStore_SoftUni.Controllers
{
    [Authorize]
    public class BeatController : Controller
    {
        private readonly IBeatService beatService;

        public BeatController(IBeatService beatService)
        {
            this.beatService = beatService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var beats = await this.beatService.GetAllBeatsAsync();
            return View(beats);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var genres = await this.beatService.GetGenresAsync();
            ViewBag.Genres = genres;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBeatDTO model)
        {
            if (!ModelState.IsValid)
            {
                var genres = await this.beatService.GetGenresAsync();
                ViewBag.Genres = genres;
                return View(model);
            }

            var artistIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(artistIdClaim) || !Guid.TryParse(artistIdClaim, out var artistId))
            {
                ModelState.AddModelError("", ErrUnableToRetrieveInformation);
                var genres = await this.beatService.GetGenresAsync();
                ViewBag.Genres = genres;
                return View(model);
            }

            await this.beatService.CreateBeatAsync(model, artistId);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (!Guid.TryParse(id, out var beatId))
            {
                return NotFound();
            }

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(); 
            }

            var beatDetails = await this.beatService.GetBeatDetailsAsync(beatId, userId);
            if (beatDetails == null || !beatDetails.IsActive)
            {
                if (beatDetails == null || !beatDetails.IsActive)
                {
                    TempData["ErrorMessage"] = ErrBeatNoLongerAvailable;
                    return RedirectToAction("Index", "Beat");
                }
            }

            return View(beatDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

            var model = await this.beatService.GetBeatForEditAsync(id, userId);

            if (model == null || !model.IsActive)
            {
                TempData["ErrorMessage"] = ErrBeatNoLongerAvailable;
                return RedirectToAction("Index", "Beat");
            }

            ViewBag.Genres = await this.beatService.GetGenresAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBeatDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Genres = await this.beatService.GetGenresAsync();
                return View(model);
            }

            var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

            var success = await this.beatService.EditBeatAsync(model, userId);

            if (!success)
            {
                return Unauthorized();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

            var success = await this.beatService.SoftDeleteBeatAsync(id, userId);

            if (!success)
            {
                return Unauthorized();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
