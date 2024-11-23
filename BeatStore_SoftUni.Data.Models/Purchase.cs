using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BeatStore_SoftUni.Data.Models;

public class Purchase
{
    [Key]
    [Comment("Unique purchase identifier")]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual ApplicationUser User { get; set; } = null!;

    [Required]
    public Guid BeatId { get; set; }

    [ForeignKey(nameof(BeatId))]
    public virtual Beat Beat { get; set; } = null!;

    [Required]
    public decimal Price { get; set; }

    [Required]
    public DateTime DatePurchased { get; set; }
}
