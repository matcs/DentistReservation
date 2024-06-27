using DentistReservation.Domain.Aggregates.ChairAggregate;
using Microsoft.EntityFrameworkCore;

namespace DentistReservation.Infrastructure.Data.Repositories;

public class ChainRepository(ApplicationDbContext context)
    : BaseRepository<Chair, Guid>(context), IChairRepository
{
    public Task<Chair?> GetByNumberAsync(int number, CancellationToken cancellationToken = default)
    {
        return _dbSet.SingleOrDefaultAsync(r => r.Number == number, cancellationToken);
    }
}