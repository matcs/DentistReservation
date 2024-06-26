namespace DentistReservation.Domain.SharedKernel;

public interface IBaseRepository<TEntity, in TKey>
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity?> GetAsync(TKey id, CancellationToken cancellationToken = default);
    Task<List<TEntity>> ListAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
}