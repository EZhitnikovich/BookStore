using System.Threading.Tasks;
using BookStore.Domain.Auth;
using BookStore.Service.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Service
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public Task<IdentityResult> CreateUserAsync(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email
            };

            return _userManager.CreateAsync(user, request.Password);
        }

        public async Task<SignInResult> PasswordLoginAsync(AuthenticationRequest request)
        {
            return await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}