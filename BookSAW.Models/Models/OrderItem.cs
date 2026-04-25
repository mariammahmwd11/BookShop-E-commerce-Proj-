using System.ComponentModel.DataAnnotations.Schema;

namespace BookSAW.Models.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book book { get; set; }
        public int orderId { get; set; }
        [ForeignKey("orderId")]
        public Order order { get; set; }
    }
}