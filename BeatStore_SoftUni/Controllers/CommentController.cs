using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BeatStore_SoftUni.Services.Data.Interfaces;
using BeatStore_SoftUni.ViewModels.CommentDTOs;
using System.Security.Claims;

namespace BeatStore_SoftUni.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(Guid beatId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var comments = await _commentService.GetCommentsForBeatAsync(beatId, userId != null ? Guid.Parse(userId) : (Guid?)null);

            return Json(comments); // Return comments as JSON, no redirect or view rendering.
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment([FromBody] CreateCommentDTO createCommentDTO)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid comment data." });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Json(new { success = false, message = "You must be logged in to add a comment." });
            }

            await _commentService.AddCommentAsync(createCommentDTO, Guid.Parse(userId));
            return Json(new { success = true, message = "Comment added successfully." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditComment([FromBody] EditCommentDTO editCommentDTO)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid comment data." });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await _commentService.EditCommentAsync(editCommentDTO, Guid.Parse(userId));
                return Json(new { success = true, message = "Comment updated successfully." });
            }
            catch (UnauthorizedAccessException)
            {
                return Json(new { success = false, message = "You are not authorized to edit this comment." });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment([FromBody] Guid commentId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await _commentService.DeleteCommentAsync(commentId, Guid.Parse(userId));
                return Json(new { success = true, message = "Comment deleted successfully." });
            }
            catch (UnauthorizedAccessException)
            {
                return Json(new { success = false, message = "You are not authorized to delete this comment." });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}