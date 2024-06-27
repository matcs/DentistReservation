using DentistReservation.Application.Chairs.Commands;
using DentistReservation.Domain.Aggregates.ChairAggregate;
using DentistReservation.Domain.Aggregates.ChairAggregate.DTOs;
using DentistReservation.Domain.Aggregates.ChairAggregate.Errors;
using DentistReservation.Domain.Aggregates.ChairAggregate.Validators;

namespace DentistReservation.Application.Chairs.Handlers;

public class UpdateChairCommandHandler(IChairRepository chairRepository)
    : IRequestHandler<UpdateChairCommand, Result<ChairDto, Error>>
{
    public async Task<Result<ChairDto, Error>> Handle(UpdateChairCommand request, CancellationToken cancellationToken)
    {
        var chair = await chairRepository.GetAsync(request.Id, cancellationToken);

        if (chair is null)
            return ChairErrors.NotFound;

        chair.Update(
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
            return new Error("Could not update chair", "One or more error occurred", errors);
        }

        await chairRepository.UpdateAsync(chair, cancellationToken);

        ChairDto chairDto = chair;

        return chairDto;
    }
}