using DentistReservation.Domain.Aggregates.ChairAggregate.Errors;

namespace DentistReservation.Application.Chairs.Handlers;

public class DeleteChairCommandHandler(IChairRepository chairRepository)
    : IRequestHandler<DeleteChairCommand, Result<ChairDto, Error>>
{
    public async Task<Result<ChairDto, Error>> Handle(DeleteChairCommand request, CancellationToken cancellationToken)
    {
        var chair = await chairRepository.GetAsync(request.Id, cancellationToken);

        if (chair is null)
            return ChairErrors.NotFound;

        if (chair.Reservations.Any())
            return ChairErrors.NotFound;

        await chairRepository.DeleteAsync(chair, cancellationToken);

        ChairDto chairDto = chair;

        return chairDto;
    }
}