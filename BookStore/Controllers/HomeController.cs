using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BookStore.Domain.Entities;
using BookStore.Domain.ViewModels;
using BookStore.Persistence;
using Microsoft.AspNetCore.Mvc;
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
            var books = _context.Books.Include(x => x.Category).Include(m => m.Marks).ToList();

            return View(books);
        }
        
        [HttpPost]
        public IActionResult Index(string bookName, float startRating, float endRating, DateTime startDate, DateTime endDate)
        {
            bookName = string.IsNullOrEmpty(bookName) ? string.Empty : bookName;
            endDate = endDate == DateTime.MinValue ? DateTime.MaxValue : endDate;
            
            var books = _context.Books.Include(x => x.Category).Include(m => m.Marks).ToList();

            books = books.Where(x => x.BookName.ToLower().Contains(bookName.ToLower())).ToList();

            if (startRating != 0)
            {
                books = books.Where(x =>
                    x.Marks.Any() && x.Marks.Average(k => k.Mark) >= startRating &&
                    x.Marks.Average(k => k.Mark) <= endRating).ToList();
            }

            books = books.Where(e => e.PublicationDate >= startDate && e.PublicationDate <= endDate).ToList();

            return View(books);
        }
    }
}