using BookSAW.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookSAW.Models.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public ICollection<CartItem> Items { get; set; }
    }
}
