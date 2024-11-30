using BeatStore_SoftUni.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class CartItem
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid CartId { get; set; }

    [ForeignKey(nameof(CartId))]
    public virtual Cart Cart { get; set; } = null!;

    [Required]
    public Guid BeatId { get; set; }

    [ForeignKey(nameof(BeatId))]
    public virtual Beat Beat { get; set; } = null!;
}
