using System.Threading.Tasks;
using BookStore.Domain.Auth;
using BookStore.Repositories.Interfaces;
using BookStore.Service;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ISessionCartService _cartService;

        public AccountController(IAccountService accountService, ISessionCartService cartService)
        {
            _accountService = accountService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _accountService.FindByEmail(User.Identity.Name);

                if (user != null)
                {
                    ViewBag.User = user;
                    ViewBag.Cart = _cartService.GetCartItems();

                    return View();
                }
            }

            return NotFound();
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountService.FindByEmail(model.Email);

                if (user != null)
                {
                    ModelState.AddModelError("", "Аккаунт с такой почтной уже существует");
                    return View(model);
                }
                
                var result = await _accountService.CreateUserAsync(model);

                if (result.Succeeded) return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new AuthenticationRequest { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthenticationRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.PasswordLoginAsync(model);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl)) return LocalRedirect(model.ReturnUrl);
                    return RedirectToAction("Index", "Home");
                }

                if (result.IsNotAllowed)
                    ModelState.AddModelError("", "Not allowed to login");
                else if (result.IsLockedOut)
                    ModelState.AddModelError("", "Account blocked. Try after some time.");
                else
                    ModelState.AddModelError("", "Неверная почта или пароль");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _accountService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}