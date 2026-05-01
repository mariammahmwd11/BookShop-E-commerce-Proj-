using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

public class CategoryDTO
{
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Category name is required")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; }


    public IFormFile? Photo { get; set; }

    public string? ImageUrl { get; set; }
    public int? DisplayOrder { get; set; }
    public int BookCount { get; set; }
}
