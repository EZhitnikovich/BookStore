using System.Threading.Tasks;
using BookStore.Domain.Auth;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<IdentityResult> CreateUserAsync(RegisterRequest model);
        Task<SignInResult> PasswordLoginAsync(AuthenticationRequest request);
        Task SignOutAsync();
    }
}