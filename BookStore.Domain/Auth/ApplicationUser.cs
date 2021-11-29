using System.Collections.Generic;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Domain.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Rating> MarkedBooks { get; set; }
    }
}