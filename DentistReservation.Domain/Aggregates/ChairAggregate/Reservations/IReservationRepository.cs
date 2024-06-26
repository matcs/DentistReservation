using DentistReservation.Domain.SharedKernel;

namespace DentistReservation.Domain.Aggregates.ChairAggregate.Reservations;

public interface IReservationRepository : IBaseRepository<Reservation, Guid>
{
    Task<bool> CheckIfReservationIsAvailableReservationAsync(int chairNumber, DateTime from, DateTime until,
        CancellationToken cancellationToken = default);
}