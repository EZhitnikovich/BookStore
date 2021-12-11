using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using BookStore.Persistence;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Repository
{
    public class RatingRepository: GenericRepository<Rating>, IRatingRepository
    {
        public RatingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<IReadOnlyList<Rating>> GetAll()
        {
            return await DbSet.Include(x=>x.User).Include(x=>x.Book).ThenInclude(c=>c.Category).ToListAsync();
        }

        public override async Task<Rating> GetById(int id)
        {
            return (await GetAll()).FirstOrDefault(x => x.Id == id);
        }
    }
}