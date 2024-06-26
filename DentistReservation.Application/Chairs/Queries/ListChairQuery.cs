using DentistReservation.Domain.Aggregates.ChairAggregate.DTOs;

namespace DentistReservation.Application.Chairs.Queries;

public class ListChairQuery(int pageIndex, int pageSize) : IRequest<IEnumerable<ChairDto>>
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
}