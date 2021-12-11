using System;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Auth;
using BookStore.Domain.Entities;
using BookStore.Domain.ViewModels;
using BookStore.Persistence;
using BookStore.Service;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly ISessionCartService _cartService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICustomValidator _validator;

        public OrderController(ISessionCartService cartService, ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, ICustomValidator validator)
        {
            _cartService = cartService;
            _context = context;
            _userManager = userManager;
            _validator = validator;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            var orders = _context.Orders.Include(u => u.User).Include(c => c.CartItems).ThenInclude(c => c.Book)
                .ThenInclude(p => p.Category).ToList();
            return View(orders);
        }

        [Authorize(Roles = "user, admin")]
        [HttpGet]
        public IActionResult MakeOrder()
        {
            return View();
        }

        [Authorize(Roles = "user, admin")]
        [HttpPost]
        public async Task<IActionResult> MakeOrder(OrderViewModel model)
        {
            if (!_validator.IsValidLength(model.Address, 2, 50))
                ModelState.AddModelError("", "Адрес должен содержать от 2 до 50 символов");
            else if (!_validator.IsValidLength(model.Information, 0, 250))
                ModelState.AddModelError("", "Слишком длинное сообщение");
            else if (!_validator.IsValidPhoneNumber(model.PhoneNumber))
                ModelState.AddModelError("", "Мобильный телефон должен иметь формат +XХХХХХХХХ");
            else if (ModelState.IsValid)
            {
                var cartItems = _cartService.GetCartItems();

                if (cartItems.Any())
                {
                    var user = await _userManager.FindByEmailAsync(User.Identity.Name);

                    if (user == null)
                    {
                        return RedirectToAction("Index", "Account");
                    }

                    var order = new Order()
                    {
                        CartItems = cartItems,
                        Address = model.Address,
                        Information = model.Information,
                        OrderDate = DateTime.Now,
                        PhoneNumber = model.PhoneNumber,
                        User = user
                    };

                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (ModelState.IsValid)
            {
                var order = _context.Orders.Include(x => x.CartItems).FirstOrDefault(c => c.Id == id);

                if (order != null)
                {
                    _context.RemoveRange(order.CartItems);
                    _context.Remove(order);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Order");
            }

            return NoContent();
        }
    }
}