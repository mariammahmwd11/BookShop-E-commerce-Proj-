namespace BookSAW.business_logic.DTO.Cart
{
    public class CartItemDTO
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }   
        public int DiscountPercent { get; set; }  
        public int Quantity { get; set; }
    }
}