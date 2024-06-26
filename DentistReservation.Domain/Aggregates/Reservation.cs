using DentistReservation.Domain.Abstractions;

namespace DentistReservation.Domain.Aggregates;

public class Reservation : BaseEntity<Guid>
{
    public Guid AggregateRootId { get; private set; }

    public int ReservationChairNumber { get; private set; }

    public DateTime From { get; private set; }

    public DateTime Until { get; private set; }

    public Reservation(Guid aggregateRootId, int reservationChairNumber)
    {
        Id = Guid.NewGuid();
        
        ReservationChairNumber = reservationChairNumber;
        AggregateRootId = aggregateRootId;
    }

    public void SetFromUntil(DateTime from, DateTime until)
    {
        From = from;
        Until = until;
    }
}