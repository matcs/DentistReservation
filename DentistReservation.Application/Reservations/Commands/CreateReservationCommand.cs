using DentistReservation.Application.Reservations.Responses;

namespace DentistReservation.Application.Reservations.Commands;

public class CreateReservationCommand(int chairNumber, DateTime from, DateTime until)
    : IRequest<Result<CreateReservationResponse, Error>>
{
    public int ChairNumber { get; } = chairNumber;
    public DateTime From { get; } = from;
    public DateTime Until { get; } = until;
}