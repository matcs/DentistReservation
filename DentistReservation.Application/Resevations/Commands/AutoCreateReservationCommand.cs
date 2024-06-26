namespace DentistReservation.Application.Chairs.Commands;

public class AutoCreateReservationCommand : IRequest<Result<CreateChairCommand, Error>>
{
}