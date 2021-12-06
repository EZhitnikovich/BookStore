using System;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using BookStore.Domain.ViewModels;
using BookStore.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BookController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View();
        }
        
        [Authorize(Roles = "admin")]
        public IActionResult BookList()
        {
            return View(_applicationDbContext.Books.ToList());
        }
        
        [Authorize(Roles = "admin")]
        public IActionResult CategoryList()
        {
            return View(_applicationDbContext.Categories.ToList());
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddBook()
        {
            ViewBag.Categories = new SelectList(_applicationDbContext.Categories, "Id", "CategoryName");
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookViewModel model)
        {
            ViewBag.Categories = new SelectList(_applicationDbContext.Categories, "Id", "CategoryName");
            if (ModelState.IsValid)
            {
                if (_applicationDbContext.Books.Any(x => x.BookName == model.BookName))
                {
                    ModelState.AddModelError("", "Книга уже есть в списке");
                    return View();
                }

                var book = new Book()
                {
                    BookName = model.BookName,
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    Image = model.Image,
                    Price = model.Price
                };
                _applicationDbContext.Add(book);
                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult EditBook(int id)
        {
            ViewBag.Categories = new SelectList(_applicationDbContext.Categories, "Id", "CategoryName");
            var book = _applicationDbContext.Books.Find(id);

            if (book != null)
            {
                var addBookViewModel = new AddBookViewModel()
                {
                    BookName = book.BookName,
                    CategoryId = book.CategoryId,
                    Description = book.Description,
                    Image = book.Image,
                    Price = book.Price
                };
                return View(addBookViewModel);
            }

            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditBook(int id, AddBookViewModel model)
        {
            ViewBag.Categories = new SelectList(_applicationDbContext.Categories, "Id", "CategoryName");

            if (ModelState.IsValid)
            {
                var book = _applicationDbContext.Books.Find(id);

                book.CategoryId = model.CategoryId;
                book.BookName = model.BookName;
                book.Image = model.Image;
                book.Description = model.Description;
                book.Price = model.Price;

                _applicationDbContext.Update(book);
                
                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            
            return View(model);
        }
        
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult DeleteBook(int id)
        {
            var book = _applicationDbContext.Books.Find(id);

            if (book != null)
            {
                var addBookViewModel = new AddBookViewModel()
                {
                    BookName = book.BookName,
                    CategoryId = book.CategoryId,
                    Description = book.Description,
                    Image = book.Image,
                    Price = book.Price
                };
                return View(addBookViewModel);
            }
            
            return NotFound();
        }
        
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteBook(int id, bool ready)
        {
            if (ModelState.IsValid)
            {
                if (ready)
                {
                    _applicationDbContext.Remove(_applicationDbContext.Books.Find(id));
                    await _applicationDbContext.SaveChangesAsync();
                }
                return RedirectToAction("BookList", "Book");
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category()
                {
                    CategoryName = model.CategoryName,
                    Description = model.Description
                };
                _applicationDbContext.Add(category);
                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
        
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var category = _applicationDbContext.Categories.Find(id);

            if (category != null)
            {
                var addCategoryViewModel = new AddCategoryViewModel()
                {
                    CategoryName = category.CategoryName,
                    Description = category.Description
                };
                return View(addCategoryViewModel);
            }

            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditCategory(int id, AddCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = _applicationDbContext.Categories.Find(id);

                category.CategoryName = model.CategoryName;
                category.Description = model.Description;

                _applicationDbContext.Update(category);
                
                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }
    }
}