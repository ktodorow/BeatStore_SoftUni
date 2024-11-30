namespace BeatStore_SoftUni.ViewModels.PurchaseDtos
{
    public class PurchaseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string CoverArtUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime DatePurchased { get; set; }
    }

}
