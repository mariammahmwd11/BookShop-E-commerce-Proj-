using BookSAW.business_logic.DTO.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.business_logic.IServices
{
    public interface ICartService
    {
        void AddToCart(string userId, int bookId);

        void IncreaseQuantity(string userId, int itemId);
        void DecreaseQuantity(string userId, int itemId);

        void RemoveItem(string userId, int itemId);

        CartDTO GetCart(string userId);

        decimal GetCartTotal(string userId);
        int GetCartItemsCount(string userId);
    }
}
