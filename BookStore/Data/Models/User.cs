using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Data.Models
{
    public class User : IdentityUser
    {
        public List<Book> MarkedBooks { get; set; }
    }
}