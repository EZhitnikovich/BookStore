using System.Threading.Tasks;
using BookStore.Domain.Auth;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Service.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<IdentityResult> CreateUserAsync(RegisterRequest model);
        Task<SignInResult> PasswordLoginAsync(AuthenticationRequest request);
        Task SignOutAsync();
        Task<ApplicationUser> FindById(string id);
        Task<ApplicationUser> FindByEmail(string email);
    }
}