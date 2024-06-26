using DentistReservation.Application.Chairs.Commands;
using DentistReservation.Application.Chairs.Responses;
using DentistReservation.Domain.Aggregates.ChairAggregate;
using DentistReservation.Domain.Aggregates.ChairAggregate.Errors;
using DentistReservation.Domain.Aggregates.ChairAggregate.Validators;

namespace DentistReservation.Application.Chairs.Handlers;

public class CreateChairCommandHandler(IChairRepository chairRepository)
    : IRequestHandler<CreateChairCommand, Result<CreateChairResponse, Error>>
{
    public async Task<Result<CreateChairResponse, Error>> Handle(CreateChairCommand request,
        CancellationToken cancellationToken)
    {
        var existingReservation =
            await chairRepository.FindByNumberAsync(request.Number, cancellationToken);

        if (existingReservation)
            return ChairErrors.ReservationAlreadyExistsNumber;

        var reservation = Chair.CreateInstance(
            request.Description,
            request.Number,
            8, 0, 18, 0, 45, 10);

        var reservationValidator = new ChairValidator();

        var validationResult = await reservationValidator.ValidateAsync(reservation, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(s => s.ErrorMessage).ToArray();
            return new Error("Could not create reservation", string.Join(",", errors));
        }

        await chairRepository.AddAsync(reservation, cancellationToken);

        CreateChairResponse response = reservation;

        return response;
    }
}