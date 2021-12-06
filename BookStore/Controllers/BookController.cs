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
        
        public IActionResult Index()
        {
            return View();
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
    }
}