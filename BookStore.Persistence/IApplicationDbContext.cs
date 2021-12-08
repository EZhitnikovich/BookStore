using System.Threading.Tasks;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<CartItem> CartItems { get; set; }

    }
}