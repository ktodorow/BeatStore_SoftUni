namespace BeatStore_SoftUni.ViewModels.CommentDTOs;

public class CreateCommentDTO
{
    public Guid BeatId { get; set; }
    public string Content { get; set; } = null!;
}