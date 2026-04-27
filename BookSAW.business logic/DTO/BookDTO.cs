using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookSAW.BL.DTO
{
    public class BookDTO
    {
        public int BookID { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }
        public IFormFile? Photo { get; set; }

        public string? ImageUrl { get; set; }

        public int Stock { get; set; }

        public decimal Price { get; set; }

        public decimal? DiscountPercent { get; set; }
        public bool IsAvaillable => Stock > 0;

        public bool IsFeatured { get; set; } = false;
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public string? CategoryName { get; set; }
        public string? AuthorName { get; set; }
    }
}
