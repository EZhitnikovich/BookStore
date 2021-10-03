using Microsoft.AspNetCore.Identity;

namespace BookStore.Domain.Auth
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}