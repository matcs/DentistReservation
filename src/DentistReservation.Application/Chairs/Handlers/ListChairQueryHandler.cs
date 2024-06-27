using DentistReservation.Application.Chairs.Queries;
using DentistReservation.Domain.Aggregates.ChairAggregate.Reservations;

namespace DentistReservation.Application.Chairs.Handlers;

public class ListChairQueryHandler(
    IChairRepository chairRepository,
    IReservationRepository reservationRepository)
    : IRequestHandler<ListChairQuery, IEnumerable<ChairDto>>
{
    public async Task<IEnumerable<ChairDto>> Handle(ListChairQuery request,
        CancellationToken cancellationToken)
    {
        var chairs = await chairRepository.ListAsync(request.PageIndex, request.PageSize, cancellationToken);

        foreach (var chair in chairs)
            await reservationRepository.ListByChairId(chair.Id, cancellationToken);

        var reservations = chairs.Select<Chair, ChairDto>(x => x);

        return reservations;
    }
}