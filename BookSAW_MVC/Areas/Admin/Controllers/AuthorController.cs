using BookSAW.BL.DTO;
using BookSAW.business_logic.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BookSAW_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
      
         
            private readonly IImageServices imageServices;    
            private readonly IAuthorService authorService;

            public AuthorController( IImageServices imageServices, IAuthorService authorService)
            {

                this.imageServices = imageServices;
                this.authorService = authorService;
            }
            public IActionResult Index()
            {
                var books = authorService.GetAuthorList();
                return View(books);
            }
            public IActionResult Create()
            {
              
                return View();
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(AuthorDTO authorDTO )
            {
                if (!ModelState.IsValid)
                {

                    return View(authorDTO);
                }

                if (authorDTO.Photo != null)
                {
                    authorDTO.ImageURl = await imageServices.SaveImage(authorDTO.Photo, "authors");
                }

                authorService.addAuthor(authorDTO);

                TempData["Success"] = "Author added successfully";
                return RedirectToAction("Index");
            }
            public IActionResult Edit(int id)
            {


                var author = authorService.GetAuthor(id);


                if (author == null)
                    return NotFound();

                return View(author);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(AuthorDTO  dto)
            {
                if (!ModelState.IsValid)
                {

                    return View(dto);
                }

                var existingAuthor = authorService.GetAuthor(dto.Id);

                if (existingAuthor == null)
                    return NotFound();


                if (dto.Photo != null && dto.Photo.Length > 0)
                {

                    imageServices.DeleteImage(existingAuthor.ImageURl);

                    dto.ImageURl = await imageServices.SaveImage(dto.Photo, "authors");
                }
                else
                {

                    dto.ImageURl = existingAuthor.ImageURl;
                }

                authorService.EditAuthor(dto);

                TempData["Success"] = "Author updated successfully";
                return RedirectToAction("Index");
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Delete(int id)
            {
                var author = authorService.GetAuthor(id);

                if (author != null)
                {
                    imageServices.DeleteImage(author.ImageURl);
                    authorService.removeAuthor(id);
                }

                TempData["Success"] = "Author deleted successfully";
                return RedirectToAction("Index");
            }
        }
    }