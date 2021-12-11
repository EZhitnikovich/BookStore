using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using BookStore.Persistence;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Repository
{
    public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
        
        public override async Task<IReadOnlyList<Category>> GetAll()
        {
            return await DbSet.Include(x => x.Books).ToListAsync();
        }
        
        public override async Task<Category> GetById(int id)
        {
            return (await GetAll()).FirstOrDefault(x => x.Id == id);
        }
    }
}