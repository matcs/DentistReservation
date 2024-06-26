using DentistReservation.Domain.SharedKernel;

namespace DentistReservation.Domain.Aggregates.ChairAggregate;

public interface IChairRepository : IBaseRepository<Chair, Guid>
{
    Task<List<Chair>> GetAvailable(CancellationToken cancellationToken = default);
    Task<bool> FindByNumberAsync(int number, CancellationToken cancellationToken = default);
    Task<bool> FindByNumberAndStartDateAsync(int number, DateTime from, CancellationToken cancellationToken = default);
}