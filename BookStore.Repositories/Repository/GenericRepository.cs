using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain;
using BookStore.Persistence;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public virtual async Task<int> Add(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException($"{nameof(entity)} is null");

            await DbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public virtual async Task Delete(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException($"{nameof(entity)} is null");

            DbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Update(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException($"{nameof(entity)} is null");

            DbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }
    }
}