using System.ComponentModel.DataAnnotations;

using static BeatStore_SoftUni.Common.EntityValidationConstants;

namespace BeatStore_SoftUni.ViewModels.CommentDtos
{
    public class CreateCommentDto
    {
        [Required]
        public Guid BeatId { get; set; }

        [Required]
        [Range(CommentContentMinLength, CommentContentMaxLength)]
        public string Content { get; set; } = null!;
    }
}