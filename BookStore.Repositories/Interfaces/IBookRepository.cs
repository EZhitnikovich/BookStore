using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain.Entities;

namespace BookStore.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<int> AddBook(Book book);
        Task DeleteBook(Book book);
        Task UpdateBook(Book book);
        Task<Book> GetBookById(int id);
        Task<IReadOnlyList<Book>> GetAllBooks();
    }
}