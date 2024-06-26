namespace DentistReservation.Application.Reservations.Responses;

public class AutoCreateReservationResponse(
    Guid reservationId,
    int chairNumber,
    DateTime startAt,
    DateTime until,
    int totalReservations)
{
    public Guid ReservationId { get; } = reservationId;
    public int ChairNumber { get; } = chairNumber;
    public DateTime StartAt { get; } = startAt;
    public DateTime Until { get; } = until;
    public int TotalReservations { get; } = totalReservations;
}