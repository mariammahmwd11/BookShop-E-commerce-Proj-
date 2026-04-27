using BookSAW.Models.Models;
using System.ComponentModel.DataAnnotations;

public class Book
{
    public int BookID { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    public double? Rate { get; set; }

    public int Stock { get; set; }

    public decimal Price { get; set; }

    public decimal DiscountPercent { get; set; } = 0m;

    public bool IsOffered => DiscountPercent > 0;
 

    public bool IsFeatured { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public int AuthorId { get; set; }
    public Author Author { get; set; }
}