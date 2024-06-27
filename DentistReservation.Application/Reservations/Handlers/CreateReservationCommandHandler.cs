using DentistReservation.Domain.Aggregates.ChairAggregate.Reservations;

namespace DentistReservation.Application.Reservations.Handlers;

public class CreateReservationCommandHandler(
    IChairRepository chairRepository,
    IReservationRepository reservationRepository
) : IRequestHandler<CreateReservationCommand, Result<CreateReservationResponse, Error>>
{
    public async Task<Result<CreateReservationResponse, Error>> Handle(CreateReservationCommand request,
        CancellationToken cancellationToken)
    {
        var chair = await chairRepository.GetByNumberAsync(request.ChairNumber, cancellationToken);

        if (chair is null)
            return ChairErrors.NotFound;

        var isAvailable =
            await reservationRepository.CheckIfReservationIsAvailableReservationAsync(
                request.ChairNumber,
                request.From,
                request.Until, cancellationToken);

        if (!isAvailable)
            throw new NotImplementedException();

        var reservation = new Reservation(chair.Id, chair.Number)
            .SetFromUntil(request.From, request.Until);

        await reservationRepository.AddAsync(reservation, cancellationToken);

        return new CreateReservationResponse(
            reservation.Id,
            reservation.ReservationChairNumber,
            reservation.From,
            reservation.Until,
            chair.Reservations.Count + 1
        );
    }
}