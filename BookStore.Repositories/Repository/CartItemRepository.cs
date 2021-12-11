using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using BookStore.Persistence;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Repository
{
    public class CartItemRepository: GenericRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IReadOnlyList<CartItem>> GetAll()
        {
            return await DbSet.Include(x => x.Book).ThenInclude(c=>c.Category).ToListAsync();
        }
        
        public override async Task<CartItem> GetById(int id)
        {
            return (await GetAll()).FirstOrDefault(x => x.Id == id);
        }
    }
}