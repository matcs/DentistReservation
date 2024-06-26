using DentistReservation.Application.Reservations.Responses;

namespace DentistReservation.Application.Reservations.Commands;

public class AutoCreateReservationCommand : IRequest<Result<AutoCreateReservationResponse, Error>>;