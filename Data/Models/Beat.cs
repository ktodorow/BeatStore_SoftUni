using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BeatStore_SoftUni.Common.EntityValidationConstants;

namespace BeatStore_SoftUni.Data.Models;

public class Beat
{
    [Key]
    public Guid BeatId { get; set; }

    [Required]
    [MaxLength(BeatTitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    public Guid ArtistId { get; set; }

    [ForeignKey(nameof(ArtistId))]
    public virtual ApplicationUser Artist { get; set; } = null!;

    [Required]
    [MaxLength(BeatGenreMaxLength)]
    public string Genre { get; set; } = null!;

    public decimal Price { get; set; } // Required by default.

    [Required]
    [Range(BeatDurationMinValue, BeatDurationMaxValue)]
    public int Duration { get; set; } // Duration in seconds

    [Required]
    [MaxLength(BeatAudioFileUrlMaxLength)]
    [Url]
    public string AudioFileUrl { get; set; } = null!;

    [Required]
    public DateTime DateUploaded { get; set; }

    // Navigation properties
    public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    public virtual ICollection<Playlist> Playlists { get; set; } = new HashSet<Playlist>();
}