using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using BookStore.Persistence;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Repository
{
    public class BookRepository: IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<int> AddBook(Book book)
        {
            if (book is null)
                //TODO: add custom exception
                throw new Exception($"{nameof(book)} is null");

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return book.Id;
        }

        public async Task DeleteBook(Book book)
        {
            if (book is null)
                //TODO: add custom exception
                throw new Exception($"{nameof(book)} is null");
            
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBook(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<IReadOnlyList<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }
    }
}