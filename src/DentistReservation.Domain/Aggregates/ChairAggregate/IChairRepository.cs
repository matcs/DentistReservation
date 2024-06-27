using DentistReservation.Domain.SharedKernel;

namespace DentistReservation.Domain.Aggregates.ChairAggregate;

public interface IChairRepository : IBaseRepository<Chair, Guid>
{
    Task<Chair?> GetByNumberAsync(int number, CancellationToken cancellationToken);
}