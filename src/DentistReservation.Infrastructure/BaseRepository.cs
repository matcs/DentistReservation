using DentistReservation.Domain.Abstractions;
using DentistReservation.Domain.SharedKernel;
using DentistReservation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DentistReservation.Infrastructure;

public class BaseRepository<TEntity, TKey>(ApplicationDbContext context) :
    IBaseRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
{
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task<TEntity?> GetAsync(TKey id, CancellationToken cancellationToken)
    {
        return _dbSet.FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        context.Entry(entity).State = EntityState.Deleted;
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task<List<TEntity>> ListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var result = _dbSet
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return result;
    }
}