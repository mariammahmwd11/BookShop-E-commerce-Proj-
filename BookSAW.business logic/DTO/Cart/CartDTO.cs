using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.business_logic.DTO.Cart
{
    public class CartDTO
    {
        public List<CartItemDTO> Items { get; set; }
        public Decimal TotalPrice { get; set; }
    }
}
