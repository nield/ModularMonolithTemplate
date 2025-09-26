using System.Linq.Expressions;
using ModularMonolithTemplate.Api.Modules.Reminder.Infrastructure.Persistance;

namespace ModularMonolithTemplate.Api.Common.Infrastructure.Persistance;

public abstract class BaseRepository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly ReminderDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(ReminderDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Add(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
                        .AnyAsync(expression, cancellationToken);
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FindAsync([id], cancellationToken);

        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Attach(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }
}