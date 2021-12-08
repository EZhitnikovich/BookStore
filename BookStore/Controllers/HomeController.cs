using System.Linq;
using BookStore.Persistence;
using Microsoft.AspNetCore.Mvc;

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
            return View(_context.Books.ToList());
        }
    }
}