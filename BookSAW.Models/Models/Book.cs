using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookSAW.Models.Models
{
    public class Book
    {
        public int BookID { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURl { get; set; }
        public double? Rate { get; set; }
        public bool isAvailable { get; set; }
        public int NumberOfAvaBooks { get; set; }
        [Required]
        public decimal Price { get; set; }

        public bool? isOffered { get; set; }=false;
        public bool? isFeatured { get; set; }=false;

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = new Category();

        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; } = new Author();



    }
}
