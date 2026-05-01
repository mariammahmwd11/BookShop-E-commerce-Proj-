using BookSAW.BL.DTO;

namespace BookSAW_MVC.Areas.User.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<BookDTO> FeaturedBooks { get; set; }
        public IEnumerable<BookDTO> OfferedBooks { get; set; }
        public IEnumerable<BookDTO> AllBooks { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }
        
        public HashSet<int> CartBookIds { get; set; } = new HashSet<int>();
    }
}
