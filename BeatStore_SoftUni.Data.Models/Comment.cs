using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static BeatStore_SoftUni.Common.EntityValidationConstants;

namespace BeatStore_SoftUni.Data.Models;

public class Comment
{
    [Key]
    public Guid CommentId { get; set; }

    [Required]
    public Guid UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; } = null!;


    [Required]
    public Guid BeatId { get; set; }

    [ForeignKey(nameof(BeatId))]
    public Beat Beat { get; set; } = null!;


    [Required]
    [MaxLength(CommentContentMaxLength)]
    public string Content { get; set; } = null!;

    [Required]
    public DateTime DatePosted { get; set; }
}