using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain.Entities;

namespace BookStore.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> FindByName(string name, List<Book> books);
        Task<IReadOnlyList<Book>> GetByIds(ISet<int> ids, List<Book> books);
        Task<IReadOnlyList<Book>> GetWithFilter(string name, Category category, List<Book> books);
        public Task<IReadOnlyList<Book>> GetSortedBooks(List<Book> books);
    }
}