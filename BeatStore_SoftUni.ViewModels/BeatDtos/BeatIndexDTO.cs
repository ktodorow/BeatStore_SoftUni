namespace BeatStore_SoftUni.ViewModels.BeatDtos
{
    public class BeatIndexDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string CoverArtUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime DateUploaded { get; set; }
        public string AudioFileUrl { get; set; } = null!; 
        public bool IsActive { get; set; }
    }
}
