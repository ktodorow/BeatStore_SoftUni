using static BeatStore_SoftUni.Common.EntityValidationConstants;
using System.ComponentModel.DataAnnotations;

namespace BeatStore_SoftUni.ViewModels.RatingDtos
{
    public class RatingDTO
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid BeatId { get; set; }

        [Range(RatingMinValue, RatingMaxValue)]
        public int Value { get; set; }
    }
}
