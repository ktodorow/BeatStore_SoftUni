namespace BeatStore_SoftUni.ViewModels.CommentDTOs;

public class CommentDTO
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string? ProfilePicture { get; set; }
    public string Content { get; set; } = null!;
    public DateTime DatePosted { get; set; }
    public DateTime? EditedOn { get; set; }
    public bool IsOwner { get; set; } 
    public bool IsBeatOwner { get; set; }
}