using DentistReservation.Application.Chairs.Commands;
using DentistReservation.Domain.Aggregates.ChairAggregate;

namespace DentistReservation.Application.Chairs.Handlers;

public class AutoCreateReservationCommandHandler(IChairRepository chairRepository)
    : IRequestHandler<AutoCreateReservationCommand, Result<CreateChairCommand, Error>>
{
    public async Task<Result<CreateChairCommand, Error>> Handle(AutoCreateReservationCommand request,
        CancellationToken cancellationToken)
    {
        var reservation = await chairRepository.GetAvailable(cancellationToken);
        if (reservation.Count is 0)
        {
            //return error
        }
        throw new NotImplementedException();
    }
}