using System.Linq.Expressions;
using ModularMonolithTemplate.Api.Modules.Reminder.Infrastructure.Persistance;

namespace ModularMonolithTemplate.Api.Common.Infrastructure.Persistance;

public abstract class BaseRepository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly ReminderDbContext DbContext;
    protected readonly DbSet<TEntity> DbSet;

    protected BaseRepository(ReminderDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<TEntity>();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Add(entity);

        await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking()
                        .AnyAsync(expression, cancellationToken);
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);

        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await DbSet.FindAsync([id], cancellationToken);

        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Attach(entity);

        await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }
}