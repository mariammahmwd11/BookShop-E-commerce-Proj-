using BookSAW.business_logic.DTO.Cart;
using BookSAW.business_logic.IServices;
using BookSAW.DataAccess.Repositories.IRepositories;
using BookSAW.DataAccess.Repositories.Repository;
using BookSAW.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.business_logic.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void AddToCart(string userId, int bookId)
        {
            var cart = unitOfWork.Cart.GetByUserId(userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>()
                };
                unitOfWork.Cart.Add(cart);
            }

            var item = cart.Items.FirstOrDefault(i => i.BookId == bookId);
            if (item != null)
            {
                return;
            }

            cart.Items.Add(new CartItem
            {
                BookId = bookId,
                Quantity = 1
            });


            unitOfWork.Save();
        }

        public void IncreaseQuantity(string userId, int itemId)
        {
            var cart = unitOfWork.Cart.GetByUserId(userId);

            var item = cart.Items.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                item.Quantity++;
                unitOfWork.Save();
            }
        }

        public void DecreaseQuantity(string userId, int itemId)
        {
            var cart = unitOfWork.Cart.GetByUserId(userId);

            var item = cart.Items.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                if (item.Quantity > 1)
                    item.Quantity--;
                else
                    cart.Items.Remove(item);

                unitOfWork.Save();
            }
        }

        public void RemoveItem(string userId, int itemId)
        {
            var cart = unitOfWork.Cart.GetByUserId(userId);

            var item = cart.Items.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                cart.Items.Remove(item);
                unitOfWork.Save();
            }
        }

        public CartDTO GetCart(string userId)
        {
            var cart = unitOfWork.Cart.GetByUserId(userId);

            if (cart == null)
                return new CartDTO();

            return new CartDTO
            {
                Items = cart.Items.Select(i => new CartItemDTO
                {
                    Id= i.Id,
                    BookId = i.BookId,
                    Title = i.Book.Name,
                    AuthorName = i.Book.Author.Name,
                    ImageUrl = i.Book.ImageUrl,

                    Price = i.Book.DiscountPercent > 0
                        ? i.Book.Price - (i.Book.Price * i.Book.DiscountPercent / 100)
                        : i.Book.Price,

                    Quantity = i.Quantity,
                    
                }).ToList(),

                TotalPrice = cart.Items.Sum(i =>
                {
                    var price = i.Book.DiscountPercent > 0
                        ? i.Book.Price - (i.Book.Price * i.Book.DiscountPercent / 100)
                        : i.Book.Price;

                    return price * i.Quantity;
                })
            };
        }

        public decimal GetCartTotal(string userId)
        {
            var cart = unitOfWork.Cart.GetByUserId(userId);

            if (cart == null)
                return 0;

            return cart.Items.Sum(i =>
            {
                var price = i.Book.DiscountPercent > 0
                    ? i.Book.Price - (i.Book.Price * i.Book.DiscountPercent / 100)
                    : i.Book.Price;

                return price * i.Quantity;
            });
        }
        
        public int GetCartItemsCount(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return 0;

            var cart = GetCart(userId);
                
            return cart?.Items?.Sum(i => i.Quantity) ?? 0;
        }
    }
}
