using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using BookStore.Persistence;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Book> FindByName(string name)
        {
            var books = await GetAll();

            Book book = null;
            
            foreach (var item in books)
            {
                if (item.BookName == name)
                {
                    book = item;
                }
            }

            return book;
        }

        public override async Task<IReadOnlyList<Book>> GetAll()
        {
            return await DbSet.Include(x => x.Marks).ThenInclude(u=>u.User).Include(c => c.Category).ToListAsync();
        }
        
        public override async Task<Book> GetById(int id)
        {
            return (await GetAll()).FirstOrDefault(x => x.Id == id);
        }
    }
}