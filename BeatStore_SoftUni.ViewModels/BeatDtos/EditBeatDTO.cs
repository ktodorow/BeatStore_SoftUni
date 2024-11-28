using System.ComponentModel.DataAnnotations;

using static BeatStore_SoftUni.Common.EntityValidationConstants;

namespace BeatStore_SoftUni.ViewModels.BeatDtos
{
    public class EditBeatDTO
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(BeatTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [Range((double)BeatPriceMinValue,(double)BeatPriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(BeatAudioFileUrlMaxLength)]
        public string AudioFileUrl { get; set; } = null!;

        [Required]
        [MaxLength(BeatCoverArtUrlMaxLength)]
        public string CoverArtUrl { get; set; } = null!;

        [Required]
        public List<Guid> GenreIds { get; set; } = new List<Guid>();

        public bool IsActive { get; set; }
    }

}
