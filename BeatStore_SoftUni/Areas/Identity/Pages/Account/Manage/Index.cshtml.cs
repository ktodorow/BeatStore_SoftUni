using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BeatStore_SoftUni.Data.Models;
using BeatStore_SoftUni.ViewModels;
using System.IO;
using System.Threading.Tasks;
using BeatStore_SoftUni.ViewModels.ApplicationUserDTO;
using System;

namespace BeatStore_SoftUni.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public ProfileViewModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Input = new ProfileViewModel
            {
                Username = user.UserName,
                ProfilePicturePath = string.IsNullOrEmpty(user.ProfilePicture) ? "/images/avatar.jpg" : user.ProfilePicture
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string action)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (action == "remove")
            {
                if (!string.IsNullOrEmpty(user.ProfilePicture) && user.ProfilePicture != "/images/avatar.jpg")
                {
                    var oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, user.ProfilePicture.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                user.ProfilePicture = "/images/avatar.jpg";
                await _userManager.UpdateAsync(user);

                TempData["StatusMessage"] = "Profile picture removed successfully.";
                return RedirectToPage();
            }

            if (!string.IsNullOrWhiteSpace(Input.Username) && Input.Username != user.UserName)
            {
                var usernameResult = await _userManager.SetUserNameAsync(user, Input.Username);
                if (!usernameResult.Succeeded)
                {
                    foreach (var error in usernameResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
            }

            if (Input.ProfilePicture != null)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/profiles");
                Directory.CreateDirectory(uploadsFolder);

                if (!string.IsNullOrEmpty(user.ProfilePicture) && user.ProfilePicture != "/images/avatar.jpg")
                {
                    var oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, user.ProfilePicture.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                var uniqueFileName = $"{user.Id}_{Path.GetFileName(Input.ProfilePicture.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Input.ProfilePicture.CopyToAsync(stream);
                }

                user.ProfilePicture = $"/images/profiles/{uniqueFileName}";
                await _userManager.UpdateAsync(user);

                TempData["StatusMessage"] = "Profile picture updated successfully.";
            }

            return RedirectToPage();
        }
    }
}