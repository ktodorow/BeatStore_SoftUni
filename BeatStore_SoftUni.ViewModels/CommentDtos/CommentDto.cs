namespace BeatStore_SoftUni.ViewModels.CommentDtos
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!; 
        public string Username { get; set; } = null!;
        public string? ProfilePictureUrl { get; set; }
        public DateTime DatePosted { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}