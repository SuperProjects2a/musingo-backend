namespace musingo_backend.Repositories;

public interface IRepository<TEntity>
{
    public IQueryable<TEntity> GetAll();
    public Task<TEntity> AddAsync(TEntity entity);
    public Task<TEntity> UpdateAsync(TEntity entity);
    public Task<TEntity> RemoveAsync(TEntity entity);
    public Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities);
    public Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);
}