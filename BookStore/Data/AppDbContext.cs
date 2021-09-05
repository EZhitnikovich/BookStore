using BookStore.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class AppDbContext: IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
        
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}