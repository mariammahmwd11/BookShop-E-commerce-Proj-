using BookSAW.BL.DTO;
using BookSAW.business_logic.IServices;
using BookSAW.business_logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSAW_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly IImageServices imageServices;
        private readonly ICategoryService categoryService;
        private readonly IAuthorService authorService;

        public BookController(IBookService bookService,IImageServices imageServices,ICategoryService categoryService,IAuthorService authorService)
        {
            this.bookService = bookService;
            this.imageServices = imageServices;
            this.categoryService = categoryService;
            this.authorService = authorService;
        }
        public IActionResult Index()
        {
            var books = bookService.GetAll();
            return View(books);
        }
        public IActionResult Create()
        {
            LoadDropdowns();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookDTO bookDTO)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns();
                return View(bookDTO);
            }

            if (bookDTO.Photo != null)
            {
                bookDTO.ImageUrl = await imageServices.SaveImage(bookDTO.Photo, "books");
            }

          bookService.creat(bookDTO);

            TempData["Success"] = "Book added successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {


            var book = bookService.GetById(id);


            if (book == null)
                return NotFound();
            LoadDropdowns();

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookDTO dto)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns();
                return View(dto);
            }
            LoadDropdowns();

            var existingBook = bookService.GetById(dto.BookID);

            if (existingBook == null)
                return NotFound();


            if (dto.Photo != null && dto.Photo.Length > 0)
            {

                imageServices.DeleteImage(existingBook.ImageUrl);

                dto.ImageUrl = await imageServices.SaveImage(dto.Photo,"books");
            }
            else
            {

                dto.ImageUrl = existingBook.ImageUrl;
            }

            bookService.Update(dto);

            TempData["Success"] = "Book updated successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var book = bookService.GetById(id);

            if (book != null)
            {
                imageServices.DeleteImage(book.ImageUrl);
                bookService.Delete(id);
            }

            TempData["Success"] = "Book deleted successfully";
            return RedirectToAction("Index");
        }
        private void LoadDropdowns()
        {
            ViewBag.Categories = categoryService.GetCategories();
            ViewBag.Authors = authorService.GetAuthorList();
        }
    }
}
