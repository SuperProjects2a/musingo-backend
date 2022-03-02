using musingo_backend.Data;

namespace musingo_backend.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly RepositoryContext repositoryContext;

    public Repository(RepositoryContext context)
    {
        repositoryContext = context;
    }
    
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await repositoryContext.AddAsync(entity);
        await repositoryContext.SaveChangesAsync();
        return entity;
    }

    public IQueryable<TEntity> GetAll()
    {
        return repositoryContext.Set<TEntity>();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        repositoryContext.Update(entity);
        await repositoryContext.SaveChangesAsync();
        return entity;
    }
}