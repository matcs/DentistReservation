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
            await chairRepository.GetByNumberAsync(request.Number, cancellationToken);

        if (existingReservation is not null)
            return ChairErrors.HasAlreadyExistingNumberNumber;

        var chair = Chair.CreateInstance(
            request.Description,
            request.Number,
            request.StartHour,
            request.StartMinute,
            request.EndHour,
            request.EndMinute,
            request.AverageDuration,
            request.AverageSetupInMinutes);

        var reservationValidator = new ChairValidator();

        var validationResult = await reservationValidator.ValidateAsync(chair, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(s => s.ErrorMessage).ToArray();
            return new Error("Could not create chair", "One or more error occurred", errors);

        }

        await chairRepository.AddAsync(chair, cancellationToken);

        CreateChairResponse response = chair;

        return response;
    }
}