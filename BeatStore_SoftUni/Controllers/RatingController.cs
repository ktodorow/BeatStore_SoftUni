using BeatStore_SoftUni.ViewModels.RatingDtos;
using static BeatStore_SoftUni.Common.ErrorMessages;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeatStore_SoftUni.Controllers
{
    [Authorize]
    public class RatingController : BaseController
    {
        private readonly IRatingService ratingService;

        public RatingController(IRatingService ratingService)
        {
            this.ratingService = ratingService;
        }

        [HttpPost]
        public async Task<IActionResult> AddRating([FromBody] RatingDTO ratingDto)
        {
            if (!ValidateUserId(out var userId))
            {
                return Json(new { success = false, message = ErrUserInfo });
            }

            ratingDto.UserId = userId;

            var success = await ratingService.AddRatingAsync(ratingDto);

            if (!success)
            {
                return Json(new { success = false, message =  });
            }

            var averageRating = await ratingService.GetAverageRatingAsync(ratingDto.BeatId);
            return Json(new { success = true, averageRating });
        }

        [HttpGet]
        public async Task<PartialViewResult> AverageRating(Guid beatId)
        {
            var averageRating = await ratingService.GetAverageRatingAsync(beatId);
            return PartialView("_AverageRating", averageRating);
        }

        [HttpGet]
        public async Task<PartialViewResult> UserRating(Guid beatId)
        {
            if (!ValidateUserId(out var userId))
            {
                return PartialView("_UserRating", 0);
            }

            var userRating = await ratingService.GetUserRatingAsync(beatId, userId);
            return PartialView("_UserRating", userRating ?? 0);
        }
    }
}
