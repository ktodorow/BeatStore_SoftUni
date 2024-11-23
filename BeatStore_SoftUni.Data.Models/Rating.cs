using static BeatStore_SoftUni.Common.EntityValidationConstants;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeatStore_SoftUni.Data.Models;

public class Rating
{
    [Key]
    [Comment("Unique rating identifier")]
    public int Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = null!;

    [Required]
    public Guid BeatId { get; set; }

    [ForeignKey(nameof(BeatId))]
    public virtual Beat Beat { get; set; } = null!;

    [Required]
    [Range(RatingMinValue, RatingMaxValue)]
    public int Value { get; set; }

    [Required]
    public DateTime DateRated { get; set; }
}