using static BeatStore_SoftUni.Common.ErrorMessages;

using Microsoft.AspNetCore.Mvc;

namespace BeatStore_SoftUni.Controllers
{
    public abstract class BaseController : Controller
    {
        protected Guid GetUserId()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Guid.Empty;
            }

            return userId;
        }

        protected bool ValidateUserId(out Guid userId)
        {
            userId = GetUserId();
            if (userId == Guid.Empty)
            {
                TempData["ErrorMessage"] = ErrUserInfo;
                return false;
            }

            return true;
        }
    }
}
