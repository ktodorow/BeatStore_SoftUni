namespace BeatStore_SoftUni.ViewModels.CartDtos
{
    public class CartDTO
    {
        public decimal TotalPrice { get; set; }
        public List<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();
    }

}
