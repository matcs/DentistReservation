using DentistReservation.Domain.Aggregates.ChairAggregate.Reservations;
using Microsoft.EntityFrameworkCore;

namespace DentistReservation.Infrastructure.Data.Repositories;

public class ReservationRepository(ApplicationDbContext context)
    : BaseRepository<Reservation, Guid>(context), IReservationRepository
{
    public Task<bool> CheckIfReservationIsAvailableReservationAsync(int chairNumber, DateTime from, DateTime until,
        CancellationToken cancellationToken = default)
    {
        return _dbSet.AnyAsync(r => r.ReservationChairNumber == chairNumber || r.From >= from && r.Until <= until,
            cancellationToken);
    }

    public async Task<List<Reservation>> ListByChairId(Guid chairId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(r => r.AggregateRootId.Equals(chairId)).ToListAsync(cancellationToken);
    }
}