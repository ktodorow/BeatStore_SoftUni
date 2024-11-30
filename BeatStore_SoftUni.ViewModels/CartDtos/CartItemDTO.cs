namespace BeatStore_SoftUni.ViewModels.CartDtos
{
    public class CartItemDTO
    {
        public Guid BeatId { get; set; }
        public string Title { get; set; } = null!;
        public string CoverArtUrl { get; set; } = null!;
        public decimal Price { get; set; }
    }

}
