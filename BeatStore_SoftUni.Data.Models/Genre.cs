using System.ComponentModel.DataAnnotations;

using static BeatStore_SoftUni.Common.EntityValidationConstants;

namespace BeatStore_SoftUni.Data.Models
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(GenreNameMaxLength)] 
        public string Name { get; set; } = null!;

        [MaxLength(GenreDescriptionMaxLength)] 
        public string? Description { get; set; }

        public virtual ICollection<Beat> Beats { get; set; } = new HashSet<Beat>();
        public virtual ICollection<BeatGenre> BeatGenres { get; set; } = new HashSet<BeatGenre>();

    }
}
