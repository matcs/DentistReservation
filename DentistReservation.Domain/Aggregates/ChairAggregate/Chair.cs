using DentistReservation.Domain.Abstractions;
using DentistReservation.Domain.SharedKernel;

namespace DentistReservation.Domain.Aggregates.ChairAggregate;

public class Chair : BaseEntity<Guid>, IAggregateRoot
{
    public string? Description { get; private set; }
    public int Number { get; private set; }
    public DateTime From { get; private set; }
    public DateTime Until { get; private set; }

    private Chair(string? description, int number, DateTime from, DateTime until)
    {
        Id = Guid.NewGuid();
        Number = number;
        Description = description;
        From = from;
        Until = until;
    }

    public static Chair CreateInstance(string description, int number, DateTime from, DateTime until)
    {
        return new Chair(description, number, from, until);
    }
}