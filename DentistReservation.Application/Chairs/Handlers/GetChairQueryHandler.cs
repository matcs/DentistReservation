using DentistReservation.Application.Chairs.Queries;
using DentistReservation.Domain.Aggregates.ChairAggregate;
using DentistReservation.Domain.Aggregates.ChairAggregate.DTOs;
using DentistReservation.Domain.Aggregates.ChairAggregate.Errors;

namespace DentistReservation.Application.Chairs.Handlers;

public class GetChairQueryHandler(IChairRepository chairRepository)
    : IRequestHandler<GetChairQuery, Result<ChairDto, Error>>
{
    public async Task<Result<ChairDto, Error>> Handle(GetChairQuery request,
        CancellationToken cancellationToken)
    {
        var reservation = await chairRepository.GetAsync(request.Id, cancellationToken);

        if (reservation is null)
            return ChairErrors.NotFound;

        ChairDto chairDto = reservation;

        return chairDto;
    }
}