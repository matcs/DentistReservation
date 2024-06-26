using DentistReservation.Domain.SharedKernel;

namespace DentistReservation.Domain.Abstractions;

public abstract class BaseEntity<TKey>
{
    public TKey Id { get; set; }

    private readonly List<BaseEvent> _domainEvents = new List<BaseEvent>();

    public IEnumerable<BaseEvent> DomainEvents =>
        _domainEvents.AsReadOnly();

    protected void AddDomainEvent(BaseEvent domainEvent) =>
        _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() =>
        _domainEvents.Clear();
}