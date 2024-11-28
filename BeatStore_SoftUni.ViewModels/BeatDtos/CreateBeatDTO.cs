using System.ComponentModel.DataAnnotations;
using static BeatStore_SoftUni.Common.EntityValidationConstants;

namespace BeatStore_SoftUni.ViewModels.BeatDtos
{
    public class CreateBeatDTO
    {
        [Required]
        [StringLength(BeatTitleMaxLength, MinimumLength = BeatTitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [Range((double)BeatPriceMinValue, (double)BeatPriceMaxValue)]
        public decimal Price { get; set; }

        [Required]
        public IEnumerable<Guid> GenreIds { get; set; } = new List<Guid>();

        [Required]
        [Url]
        public string AudioFileUrl { get; set; } = null!;

        [Required]
        [Url]
        public string CoverArtUrl { get; set; } = null!;
    }
}
