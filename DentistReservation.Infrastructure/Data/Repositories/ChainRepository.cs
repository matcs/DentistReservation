using DentistReservation.Domain.Aggregates.ChairAggregate;
using Microsoft.EntityFrameworkCore;

namespace DentistReservation.Infrastructure.Data.Repositories;

public class ChainRepository(ApplicationDbContext context)
    : BaseRepository<Chair, Guid>(context), IChairRepository
{
    public Task<List<Chair>> GetAvailable(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> FindByNumberAsync(int number, CancellationToken cancellationToken = default)
    {
        return _dbSet.AnyAsync(r => r.Number == number, cancellationToken);
    }

    public Task<bool> FindByNumberAndStartDateAsync(int number, DateTime from,
        CancellationToken cancellationToken = default)
    {
        return _dbSet.AnyAsync(r =>
                r.Number == number || r.From >= from || r.Until <= from
            , cancellationToken);
    }
}