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
        var chairs = await chairRepository.ListAsync(1, 100, cancellationToken);

        if (chairs.Count is 0)
            return ChairErrors.NotFound;

        var availableReservations = chairs
            .Where(c => c.HasAnyAvailableReservations()).ToList();

        if (availableReservations.Count is 0)
            return ChairErrors.NotFound;

        var chairWithLowestReservation = availableReservations.First();

        foreach (var chair in availableReservations)
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
}