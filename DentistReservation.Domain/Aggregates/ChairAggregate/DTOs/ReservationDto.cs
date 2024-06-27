using DentistReservation.Domain.Aggregates.ChairAggregate.Reservations;

namespace DentistReservation.Domain.Aggregates.ChairAggregate.DTOs;

public class ReservationDto
{
    public Guid Id { get; private set; }
    public DateTime From { get; private set; }
    public DateTime Until { get; private set; }

    public static implicit operator ReservationDto(Reservation reservation)
    {
        return new ReservationDto
        {
            Id = reservation.Id,
            From = reservation.From,
            Until = reservation.Until
        };
    }
}