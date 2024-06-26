using DentistReservation.Application.Chairs.Commands;
using DentistReservation.Domain.Aggregates.ChairAggregate;
using DentistReservation.Domain.Aggregates.ChairAggregate.DTOs;
using DentistReservation.Domain.Aggregates.ChairAggregate.Errors;

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

        await chairRepository.UpdateAsync(chair, cancellationToken);

        ChairDto chairDto = chair;
        
        return chairDto;
    }
}