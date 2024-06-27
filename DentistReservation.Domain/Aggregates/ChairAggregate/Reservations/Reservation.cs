using DentistReservation.Domain.Abstractions;

namespace DentistReservation.Domain.Aggregates.ChairAggregate.Reservations;

public class Reservation : BaseEntity<Guid>
{
    public Guid AggregateRootId { get; private set; }

    public int ReservationChairNumber { get; private set; }

    public DateTime From { get; private set; }

    public DateTime Until { get; private set; }
    
    public Chair Chair { get; private set; }
    
    public Reservation(Guid aggregateRootId, int reservationChairNumber)
    {
        Id = Guid.NewGuid();
        ReservationChairNumber = reservationChairNumber;
        AggregateRootId = aggregateRootId;
    }

    public Reservation SetFromUntil(DateTime from, DateTime until)
    {
        From = from;
        Until = until;

        return this;
    }
}