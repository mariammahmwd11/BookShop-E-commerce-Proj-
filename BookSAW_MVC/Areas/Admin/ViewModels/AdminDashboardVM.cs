using BookSAW.BL.DTO;
using BookSAW.Models.Models;

namespace BookSAW_MVC.Areas.Admin.ViewModels
{
    public class AdminDashboardVM
    {
        public int TotalBooks { get; set; }
        public int TotalCategories { get; set; }
        public int TotalUsers { get; set; }
        public int TotalAuthors { get; set; }
        public int TotalFeaturedBooks { get; set; }
        public int BooksWithDiscount { get; set; }
        public IEnumerable<BookDTO> RecentBooks { get; set; }
        public IEnumerable<CategoryDTO> RecentCategories { get; set; }
        public IEnumerable<AuthorDTO> RecentAuthors { get; set; }

    }
}
