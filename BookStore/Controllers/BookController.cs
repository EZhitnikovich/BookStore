using System;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Auth;
using BookStore.Domain.Entities;
using BookStore.Domain.ViewModels;
using BookStore.Persistence;
using BookStore.Repositories.Interfaces;
using BookStore.Repositories.Repository;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICustomValidator _validator;

        public BookController(UserManager<ApplicationUser> userManager, ICategoryRepository categoryRepository,
            IBookRepository bookRepository, IRatingRepository ratingRepository,
            ICartItemRepository cartItemRepository, ICustomValidator validator)
        {
            _userManager = userManager;
            _categoryRepository = categoryRepository;
            _bookRepository = bookRepository;
            _ratingRepository = ratingRepository;
            _cartItemRepository = cartItemRepository;
            _validator = validator;
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> BookList()
        {
            return View(await _bookRepository.GetAll());
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CategoryList()
        {
            return View(await _categoryRepository.GetAll());
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> AddBook()
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAll(), "Id", "CategoryName");
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookViewModel model)
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAll(), "Id", "CategoryName");

            if (!_validator.IsValidLength(model.BookName, 2, 50))
                ModelState.AddModelError("", "Название книги должно содержать от 2 до 50 символов");
            else if (!_validator.IsValidLength(model.Description, 25, 250))
                ModelState.AddModelError("", "Описание книги должно содержать от 25 до 250 символов");
            else if (!_validator.IsValidPrice(model.Price))
                ModelState.AddModelError("", "Цена должна принимать значение больше нуля");
            else if (ModelState.IsValid)
            {
                if (await _bookRepository.FindByName(model.BookName) != null)
                {
                    ModelState.AddModelError("", "Данная книга уже существует");
                    return View();
                }

                var book = new Book()
                {
                    BookName = model.BookName,
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    Image = model.Image,
                    Price = model.Price,
                    PublicationDate = model.PublicationDate
                };
                await _bookRepository.Add(book);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> EditBook(int id)
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAll(), "Id", "CategoryName");
            var book = await _bookRepository.GetById(id);

            if (book != null)
            {
                var addBookViewModel = new AddBookViewModel()
                {
                    BookName = book.BookName,
                    CategoryId = book.CategoryId,
                    Description = book.Description,
                    Image = book.Image,
                    Price = book.Price,
                    PublicationDate = book.PublicationDate
                };
                return View(addBookViewModel);
            }

            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditBook(int id, AddBookViewModel model)
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAll(), "Id", "CategoryName");

            if (!_validator.IsValidLength(model.BookName, 2, 50))
                ModelState.AddModelError("", "Название книги должно содержать от 2 до 50 символов");
            else if (!_validator.IsValidLength(model.Description, 25, 250))
                ModelState.AddModelError("", "Описание книги должно содержать от 25 до 250 символов");
            else if (!_validator.IsValidPrice(model.Price))
                ModelState.AddModelError("", "Цена должна принимать значение больше нуля");
            else if (ModelState.IsValid)
            {
                var book = await _bookRepository.GetById(id);

                if (book != null)
                {
                    book.CategoryId = model.CategoryId;
                    book.BookName = model.BookName;
                    book.Image = model.Image;
                    book.Description = model.Description;
                    book.Price = model.Price;
                    book.PublicationDate = model.PublicationDate;

                    await _bookRepository.Update(book);
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteBook(int id)
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAll(), "Id", "CategoryName");
            var book = await _bookRepository.GetById(id);

            if (book != null)
            {
                var addBookViewModel = new AddBookViewModel()
                {
                    BookName = book.BookName,
                    CategoryId = book.CategoryId,
                    Description = book.Description,
                    Image = book.Image,
                    Price = book.Price,
                    PublicationDate = book.PublicationDate
                };
                return View(addBookViewModel);
            }

            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteBook(int id, bool ready)
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAll(), "Id", "CategoryName");

            var category = (await _bookRepository.GetById(id)).Category;

            ViewBag.CategoryName = category.CategoryName;

            if (ModelState.IsValid)
            {
                if (ready)
                {
                    var book = await _bookRepository.GetById(id);

                    if (book != null && book.Marks.Any())
                    {
                        await _ratingRepository.RemoveRange(book.Marks);
                        await _cartItemRepository.RemoveRange(
                            (await _cartItemRepository.GetAll()).Where(x => x.Book.Id == id));
                        await _bookRepository.Delete(book);
                    }
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
            if (!_validator.IsValidLength(model.CategoryName, 2, 50))
                ModelState.AddModelError("", "Название категории должно содержать от 2 до 25 символов");
            else if (!_validator.IsValidLength(model.Description, 25, 250))
                ModelState.AddModelError("", "Описание категории должно содержать от 25 до 250 символов");
            else if (ModelState.IsValid)
            {
                if ((await _categoryRepository.GetAll()).Any(x => x.CategoryName == model.CategoryName))
                {
                    ModelState.AddModelError("", "Название категории уже существует");
                    return View();
                }

                var category = new Category()
                {
                    CategoryName = model.CategoryName,
                    Description = model.Description
                };

                await _categoryRepository.Add(category);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _categoryRepository.GetById(id);

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
            if (!_validator.IsValidLength(model.CategoryName, 2, 50))
                ModelState.AddModelError("", "Название категории должно содержать от 2 до 25 символов");
            else if (!_validator.IsValidLength(model.Description, 25, 250))
                ModelState.AddModelError("", "Описание категории должно содержать от 25 до 250 символов");
            else if (ModelState.IsValid)
            {
                var category = await _categoryRepository.GetById(id);

                if (category != null)
                {
                    category.CategoryName = model.CategoryName;
                    category.Description = model.Description;

                    await _categoryRepository.Update(category);
                }

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetById(id);

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
                    await _categoryRepository.Delete(await _categoryRepository.GetById(id));
                }

                return RedirectToAction("CategoryList", "Book");
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> AddRating(int id, int value)
        {
            value = value <= 0 ? 1 : value;
            value = value > 5 ? 5 : value;

            var book = await _bookRepository.GetById(id);

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            if (book != null && user != null)
            {
                var rating =
                    (await _ratingRepository.GetAll()).SingleOrDefault(
                        x => x.Book.Id == book.Id && x.User.Id == user.Id);

                if (rating != null)
                {
                    rating.Mark = value;
                    await _ratingRepository.Update(rating);
                }
                else
                {
                    rating = new Rating()
                    {
                        User = user,
                        Book = book,
                        Mark = value
                    };

                    await _ratingRepository.Add(rating);
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}