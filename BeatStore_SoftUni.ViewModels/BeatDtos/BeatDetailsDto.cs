namespace BeatStore_SoftUni.ViewModels.BeatDtos
{
    public class BeatDetailsDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string CoverArtUrl { get; set; } = null!;
        public string AudioFileUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public string UploadedBy { get; set; } = null!;
        public DateTime DateUploaded { get; set; }
        public int PlaylistsCount { get; set; }
        public bool IsOwner { get; set; } 
        public bool IsActive { get; set; }
    }
}
