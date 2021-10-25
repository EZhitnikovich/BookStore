using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using BookStore.Persistence;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Repository
{
    public class BookRepository: GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Book> FindByName(string name)
        {
            Book book = null;
            
            for (var i = 0; i < DbSet.Count(); i++)
            {
                if (DbSet.ElementAt(i).BookName.ToLower() == name.ToLower())
                {
                    book = DbSet.ElementAt(i);
                }
            }

            return book;
        }
    }
}