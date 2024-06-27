using DentistReservation.Application.Chairs.Queries;
using DentistReservation.Domain.Aggregates.ChairAggregate;
using DentistReservation.Domain.Aggregates.ChairAggregate.DTOs;
using DentistReservation.Domain.Aggregates.ChairAggregate.Errors;
using DentistReservation.Domain.Aggregates.ChairAggregate.Reservations;

namespace DentistReservation.Application.Chairs.Handlers;

public class GetChairQueryHandler(IChairRepository chairRepository, IReservationRepository reservationRepository)
    : IRequestHandler<GetChairQuery, Result<ChairDto, Error>>
{
    public async Task<Result<ChairDto, Error>> Handle(GetChairQuery request,
        CancellationToken cancellationToken)
    {
        var chair = await chairRepository.GetAsync(request.Id, cancellationToken);

        if (chair is null)
            return ChairErrors.NotFound;

        chair.Reservations = await reservationRepository.ListByChairId(chair.Id, cancellationToken);
        
        ChairDto chairDto = chair;

        return chairDto;
    }
}