namespace DentistReservation.Domain.Abstractions;

public abstract class BaseEntity<TKey>
{
    public TKey Id { get; set; }
}