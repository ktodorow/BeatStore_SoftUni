using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static BeatStore_SoftUni.Common.EntityValidationConstants;

namespace BeatStore_SoftUni.Data.Models;

public class Playlist
{
    [Key]
    public Guid PlaylistId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = null!;

    [Required]
    [MaxLength(PlaylistNameMaxLength)]
    public string Name { get; set; } = null!;

    [MaxLength(PlaylistDescriptionMaxLength)]
    public string? Description { get; set; }

    [Required]
    public DateTime DateCreated { get; set; }

    // Navigation properties
    public virtual ICollection<Beat> Beats { get; set; } = new HashSet<Beat>();
}