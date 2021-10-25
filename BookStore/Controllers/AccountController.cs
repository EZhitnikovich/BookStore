using System.Threading.Tasks;
using BookStore.Domain.Auth;
using BookStore.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
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
                var result = await _accountRepository.CreateUserAsync(model);

                if (result.Succeeded) return RedirectToAction("Index", "Home");
            }

            return View(model);
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
                var result = await _accountRepository.PasswordLoginAsync(model);
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
                    ModelState.AddModelError("", "Invalid credentials");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}