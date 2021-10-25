using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain.Entities;

namespace BookStore.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> FindByName(string name);
    }
}