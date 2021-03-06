using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain;

namespace BookStore.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<int> Add(TEntity entity);
        Task Delete(TEntity entity);
        Task RemoveRange(IEnumerable<TEntity> entities);
        Task Update(TEntity entity);
        Task<TEntity> GetById(int id);
        Task<IReadOnlyList<TEntity>> GetAll();
    }
}