using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

using static BeatStore_SoftUni.Common.EntityValidationConstants;

namespace BeatStore_SoftUni.Data.Models;
public class ApplicationUser : IdentityUser<Guid>
{
    [MaxLength(ProfilePicturePathMaxLength)]
    public string? ProfilePicture { get; set; } //TODO: Set Default Profile Picture
    public DateTime DateJoined { get; set; }
    public string Role { get; set; } = null!; // e.g., artist, listener

}