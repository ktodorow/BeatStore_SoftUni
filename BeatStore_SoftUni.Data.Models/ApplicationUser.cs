using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

using static BeatStore_SoftUni.Common.EntityValidationConstants;

namespace BeatStore_SoftUni.Data.Models;
public class ApplicationUser : IdentityUser<Guid>
{
    [MaxLength(ProfilePicturePathMaxLength)]
    public string? ProfilePicture { get; set; } 
    public DateTime DateJoined { get; set; }
    public decimal Balance { get; set; } = 100.00m;
}