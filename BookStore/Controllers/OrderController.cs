using System;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Auth;
using BookStore.Domain.Entities;
using BookStore.Domain.ViewModels;
using BookStore.Persistence;
using BookStore.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class OrderController: Controller
    {
        private readonly ISessionCartService _cartService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(ISessionCartService cartService, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _context = context;
            _userManager = userManager;
        }
        
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            var orders = _context.Orders.Include(u=>u.User).
                Include(c => c.CartItems).ThenInclude(c => c.Book).ToList();
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
            if (ModelState.IsValid)
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
                        Email = model.Email,
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
    }
}