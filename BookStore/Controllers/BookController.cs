using System;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Auth;
using BookStore.Domain.Entities;
using BookStore.Domain.ViewModels;
using BookStore.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            return View();
        }
        
        [Authorize(Roles = "admin")]
        public IActionResult BookList()
        {
            return View(_context.Books.ToList());
        }
        
        [Authorize(Roles = "admin")]
        public IActionResult CategoryList()
        {
            return View(_context.Categories.ToList());
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AddBook()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "CategoryName");
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookViewModel model)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "CategoryName");
            if (ModelState.IsValid)
            {
                if (_context.Books.Any(x => x.BookName == model.BookName))
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
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult EditBook(int id)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "CategoryName");
            var book = _context.Books.Find(id);

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
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "CategoryName");

            if (ModelState.IsValid)
            {
                var book = _context.Books.Find(id);

                book.CategoryId = model.CategoryId;
                book.BookName = model.BookName;
                book.Image = model.Image;
                book.Description = model.Description;
                book.Price = model.Price;

                _context.Update(book);
                
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            
            return View(model);
        }
        
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.Find(id);

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
                    _context.Remove(_context.Books.Find(id));
                    await _context.SaveChangesAsync();
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
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
        
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var category = _context.Categories.Find(id);

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
                var category = _context.Categories.Find(id);

                category.CategoryName = model.CategoryName;
                category.Description = model.Description;

                _context.Update(category);
                
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }
        
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);

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
        public async Task<IActionResult> DeleteCategory(int id, bool ready)
        {
            if (ModelState.IsValid)
            {
                if (ready)
                {
                    _context.Remove(_context.Categories.Find(id));
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("CategoryList", "Book");
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> AddRating(int id, int value)
        {
            var book = _context.Books.Find(id);

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            if (book != null && user != null)
            {
                var rating = _context.Marks.SingleOrDefault(x=> x.Book.Id == book.Id && x.User.Id == user.Id);

                if (rating != null)
                {
                    rating.Mark = value;
                }
                else
                {
                    rating = new Rating()
                    {
                        User = user,
                        Book = book,
                        Mark = value
                    };
                    
                    _context.Marks.Add(rating);
                }
                
                await _context.SaveChangesAsync();
            }
            
            return NoContent();
        }
    }
}