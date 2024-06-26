namespace DentistReservation.Application.Reservations.Responses;

public class CreateReservationResponse(
    Guid reservationId,
    int chairNumber,
    DateTime from,
    DateTime until,
    int totalReservations)
{
    public Guid ReservationId { get; } = reservationId;
    public int ChairNumber { get; } = chairNumber;
    public DateTime From { get; } = from;
    public DateTime Until { get; } = until;
    public int TotalReservations { get; } = totalReservations;
}