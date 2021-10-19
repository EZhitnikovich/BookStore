using System.Threading.Tasks;
using BookStore.Domain.Auth;
using BookStore.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Repositories.Repository
{
    public class AccountRepository: IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterRequest request)
        {
            var user = new ApplicationUser()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            return result;
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