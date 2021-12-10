using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BookStore.Domain.Entities;
using BookStore.Domain.ViewModels;
using BookStore.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            categories.Insert(0, new Category(){CategoryName = "Все категории", Id = 0});
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
            
            var books = _context.Books.Include(x => x.Category).Include(m => m.Marks).ToList();

            return View(books);
        }
        
        [HttpPost]
        public IActionResult Index(string bookName, float startRating, float endRating, DateTime startDate, DateTime endDate, int categoryId)
        {
            var categories = _context.Categories.ToList();
            categories.Insert(0, new Category(){CategoryName = "Все категории", Id = 0});
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
            
            bookName = string.IsNullOrEmpty(bookName) ? string.Empty : bookName;
            endDate = endDate == DateTime.MinValue ? DateTime.MaxValue : endDate;
            startRating = startRating < 0 ? 0 : startRating;
            endRating = endRating > 5 ? 5 : endRating;
            
            var books = _context.Books.Include(x => x.Category).Include(m => m.Marks).ToList();

            books = books.Where(x => x.BookName.ToLower().Contains(bookName.ToLower())).ToList();

            books = books.Where(x =>
                    x.Marks.Any() && x.Marks.Average(k => k.Mark) >= startRating &&
                    x.Marks.Average(k => k.Mark) <= endRating).ToList();

            books = books.Where(x => x.PublicationDate >= startDate && x.PublicationDate <= endDate).ToList();

            if (categoryId != 0)
            {
                books = books.Where(x => x.CategoryId == categoryId).ToList();
            }

            return View(books);
        }

        [Authorize(Roles = "admin")]
        [Route("Administrator")]
        public IActionResult Administrator()
        {
            return View(_context);
        }
    }
}