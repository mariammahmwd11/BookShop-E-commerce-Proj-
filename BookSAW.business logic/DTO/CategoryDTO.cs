using System.ComponentModel.DataAnnotations;

public class CategoryDTO
{
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Category name is required")]
    [MaxLength(100)]
    public string Name { get; set; }
}