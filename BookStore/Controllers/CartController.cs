using System;
using System.Threading.Tasks;
using BookStore.Domain.Auth;
using BookStore.Domain.Entities;
using BookStore.Persistence;
using BookStore.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Controllers
{
    [Authorize]
    public class CartController: Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISessionCartService _cartService;

        public CartController(ApplicationDbContext context, ISessionCartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public ViewResult Index()
        {
            var items = _cartService.GetCartItems();
            return View(items);
        }

        public RedirectToActionResult AddToCart(int id)
        {
            var book = _context.Books.Find(id);

            if (book != null)
            {
                _cartService.AddToCart(book);
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromCart(int id)
        {
            var book = _context.Books.Find(id);

            if (book != null)
            {
                _cartService.RemoveFromCart(book);
            }

            return RedirectToAction("Index");
        }
    }
}