using DentistReservation.Domain.Aggregates.ChairAggregate.Reservations;

namespace DentistReservation.Application.Reservations.Handlers;

public class AutoCreateReservationCommandHandler(
    IChairRepository chairRepository,
    IReservationRepository reservationRepository)
    : IRequestHandler<AutoCreateReservationCommand, Result<CreateReservationResponse, Error>>
{
    public async Task<Result<CreateReservationResponse, Error>> Handle(AutoCreateReservationCommand request,
        CancellationToken cancellationToken)
    {
        var chairs = await chairRepository.ListAsync(0, 100, cancellationToken);

        if (chairs.Count is 0)
            return ChairErrors.IsEmpty;

        foreach (var chair in chairs)
            await SetReservation(chair, cancellationToken);

        var chairWithLowestReservation = chairs.First();

        foreach (var chair in chairs)
        {
            var totalReservations = chair.Reservations.Count;

            if (totalReservations < chairWithLowestReservation.Reservations.Count)
                chairWithLowestReservation = chair;
        }

        var reservation = chairWithLowestReservation.AddAutomaticReservation();

        await reservationRepository.AddAsync(reservation, cancellationToken);

        return new CreateReservationResponse(reservation.Id,
            chairWithLowestReservation.Number,
            reservation.From,
            reservation.Until,
            chairWithLowestReservation.Reservations.Count);
    }

    private async Task SetReservation(Chair chair, CancellationToken cancellationToken)
    {
        if (chair.Reservations.Count == 0)
        {
            var result = await reservationRepository.ListByChairId(chair.Id, cancellationToken);
            chair.AddReservations(result);
        }
    }
}