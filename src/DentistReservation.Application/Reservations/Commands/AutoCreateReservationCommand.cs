namespace DentistReservation.Application.Reservations.Commands;

public class AutoCreateReservationCommand : IRequest<Result<CreateReservationResponse, Error>>;