using Microsoft.AspNetCore.Http;

namespace BeatStore_SoftUni.ViewModels.ApplicationUserDTO
{
    public class ProfileViewModel
    {
        public string Username { get; set; } = null!;
        public string? ProfilePicturePath { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public decimal Balance { get; set; }
    }
}
