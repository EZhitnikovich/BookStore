using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain;
using BookStore.Persistence;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Repository
{
    public class GenericRepository<TEntity>: IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }
        
        public async Task<int> Add(TEntity entity)
        {
            if (entity is null)
                //TODO: add custom exception
                throw new Exception($"{nameof(entity)} is null");

            await DbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Delete(TEntity entity)
        {
            if (entity is null)
                //TODO: add custom exception
                throw new Exception($"{nameof(entity)} is null");
            
            DbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IReadOnlyList<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }
    }
}