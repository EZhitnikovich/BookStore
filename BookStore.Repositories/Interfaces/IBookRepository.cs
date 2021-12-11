using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using BookStore.Repositories.Repository;

namespace BookStore.Repositories.Interfaces
{
    public interface IBookRepository: IGenericRepository<Book>
    {
        Task<Book> FindByName(string name);
    }
}