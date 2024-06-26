using DentistReservation.Application.Chairs.Queries;
using DentistReservation.Domain.Aggregates.ChairAggregate;
using DentistReservation.Domain.Aggregates.ChairAggregate.DTOs;

namespace DentistReservation.Application.Chairs.Handlers;

public class ListChairQueryHandler(IChairRepository chairRepository)
    : IRequestHandler<ListChairQuery, IEnumerable<ChairDto>>
{
    public async Task<IEnumerable<ChairDto>> Handle(ListChairQuery request,
        CancellationToken cancellationToken)
    {
        var result = await chairRepository.ListAsync(request.PageIndex, request.PageSize, cancellationToken);

        var reservations = result.Select<Chair, ChairDto>(x => x);

        return reservations;
    }
}