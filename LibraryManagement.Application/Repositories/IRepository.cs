using System.Linq.Expressions;

namespace LibraryManagement.Application.Repositories;

public interface IRepository<TEntity>
{
    IQueryable<TEntity> GetAll();
    Task<TEntity?> FindAsync(int id); // Melhor performance ao buscar por Primary Keys
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> CreateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
}
