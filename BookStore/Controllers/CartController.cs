using BookStore.Persistence;
using BookStore.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ISessionCartService _cartService;
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context, ISessionCartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public IActionResult AddToCart(int id)
        {
            var book = _context.Books.Find(id);

            if (book != null) _cartService.ChangeAmountInCart(book, 1);

            return RedirectToAction("Index", "Account");
        }

        public IActionResult ChangeAmountInCart(int id, int amount)
        {
            var book = _context.Books.Find(id);

            if (book != null) _cartService.ChangeAmountInCart(book, amount);

            return RedirectToAction("Index", "Account");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var book = _context.Books.Find(id);

            if (book != null) _cartService.RemoveFromCart(book);

            return RedirectToAction("Index", "Account");
        }
    }
}