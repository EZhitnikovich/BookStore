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

        public BookRepository()
        {
        }

        public async Task<Book> FindByName(string name, List<Book> books)
        {
            Book book = null;

            for (var i = 0; i < books.Count; i++)
                if (books[i].BookName.ToLower() == name.ToLower())
                {
                    book = books[i];
                    break;
                }

            return book;
        }

        public async Task<IReadOnlyList<Book>> GetByIds(ISet<int> ids, List<Book> books)
        {
            var list = new List<Book>();
            var enumerable = ids.ToList();

            foreach (var id in enumerable)
            {
                foreach (var book in books)
                {
                    if (book.Id == id)
                    {
                        list.Add(book);
                        break;
                    }
                }
            }

            return list;
        }

        public async Task<IReadOnlyList<Book>> GetWithFilter(string name, Category category, List<Book> books)
        {
            var list = new List<Book>();

            foreach (var book in books)
                if (book.BookName.ToLower().Contains(name.ToLower()) &&
                    book.Category.Equals(category))
                    list.Add(book);

            return list;
        }

        public async Task<IReadOnlyList<Book>> GetSortedBooks(List<Book> books)
        {
            for (var s = books.Count/2; s > 0; s/=2)
            {
                for (var i = 0; i < books.Count; i++)
                {
                    for (var j = i+s; j < books.Count; j+=s)
                    {
                        if (books[i].Id > books[j].Id)
                        {
                            var book = books[j];
                            books[j] = books[i];
                            books[i] = book;
                        }
                    }
                }
            }

            return books;
        }

        public async Task<IReadOnlyList<Book>> GetByPrice(float lower, float higher, List<Book> books)
        {
            var list = new List<Book>();
            
            foreach (var book in books)
            {
                if (book.Price > lower && book.Price < higher)
                {
                    list.Add(book);
                }
            }
            
            return list;
        }
    }
}