using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using BookStore.Persistence;
using BookStore.Repositories.Interfaces;

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

            for (var i = 0; i < books.Count; i++)
                if (books[i].BookName.ToLower() == name.ToLower())
                {
                    book = books[i];
                    break;
                }

            return book;
        }

        public async Task<IReadOnlyList<Book>> GetByIds(IEnumerable<int> ids)
        {
            var books = await GetAll();

            var list = new List<Book>();
            var enumerable = ids.ToList();

            for (int i = 0; i < books.Count; i++)
            {
                if (enumerable.Any(e => books[i].Id == e))
                {
                    list.Add(books[i]);
                }
            }

            return list;
        }
    }
}