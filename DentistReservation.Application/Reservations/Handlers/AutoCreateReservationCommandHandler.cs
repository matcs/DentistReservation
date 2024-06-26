using DentistReservation.Application.Reservations.Commands;
using DentistReservation.Application.Reservations.Responses;
using DentistReservation.Domain.Aggregates.ChairAggregate;
using DentistReservation.Domain.Aggregates.ChairAggregate.Errors;

namespace DentistReservation.Application.Reservations.Handlers;

public class AutoCreateReservationCommandHandler(IChairRepository chairRepository)
    : IRequestHandler<AutoCreateReservationCommand, Result<AutoCreateReservationResponse, Error>>
{
    public async Task<Result<AutoCreateReservationResponse, Error>> Handle(AutoCreateReservationCommand request,
        CancellationToken cancellationToken)
    {
        var chairs = await chairRepository.ListAsync(1, 100, cancellationToken);

        if (chairs.Count is 0)
            return ChairErrors.NotFound;

        var chairWithMostAvailableReservations = chairs.First();

        foreach (var chair in chairs)
        {
            var totalReservations = chair.Reservations.Count;

            if (totalReservations < chairWithMostAvailableReservations.Reservations.Count)
            {
                chairWithMostAvailableReservations = chair;
            }
        }

        var reservationId = chairWithMostAvailableReservations.AddAutomaticReservation();

        if (reservationId is null)
            return ChairErrors.NotFound;

        return new AutoCreateReservationResponse(reservationId.Id,
            chairWithMostAvailableReservations.Number,
            reservationId.From,
            reservationId.Until,
            chairWithMostAvailableReservations.Reservations.Count);
    }
}