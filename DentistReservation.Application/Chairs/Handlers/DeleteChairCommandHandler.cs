using DentistReservation.Domain.Aggregates.ChairAggregate.Reservations;

namespace DentistReservation.Application.Chairs.Handlers;

public class DeleteChairCommandHandler(
    IChairRepository chairRepository,
    IReservationRepository reservationRepository
)
    : IRequestHandler<DeleteChairCommand, Result<ChairDto, Error>>
{
    public async Task<Result<ChairDto, Error>> Handle(DeleteChairCommand request, CancellationToken cancellationToken)
    {
        var chair = await chairRepository.GetAsync(request.Id, cancellationToken);

        if (chair is null)
            return ChairErrors.NotFound;

        chair.Reservations = await reservationRepository.ListByChairId(chair.Id, cancellationToken);

        if (chair.Reservations.Any(r => r.From > DateTime.Now))
            return ChairErrors.ThereReservationsAlready;

        await chairRepository.DeleteAsync(chair, cancellationToken);

        ChairDto chairDto = chair;

        return chairDto;
    }
}