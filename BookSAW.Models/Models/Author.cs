using System.ComponentModel.DataAnnotations;

namespace BookSAW.Models.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public string Bio { get; set; }
        public string ImageURl { get; set; }
        public List<Book> Books { get; set; }
    }
}