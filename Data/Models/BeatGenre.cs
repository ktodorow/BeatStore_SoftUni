using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeatStore_SoftUni.Data.Models;
public class BeatGenre
{
    [Required]
    public Guid BeatId { get; set; }

    [ForeignKey(nameof(BeatId))]
    public virtual Beat Beat { get; set; } = null!;

    [Required]
    public Guid GenreId { get; set; }

    [ForeignKey(nameof(GenreId))]
    public virtual Genre Genre { get; set; } = null!;
}