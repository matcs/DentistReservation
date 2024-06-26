using MediatR;

namespace DentistReservation.Domain.Abstractions;

public abstract class BaseEvent : INotification
{
    public int AggregateId { get; protected init; }
    public byte Type { get; protected init; }
}