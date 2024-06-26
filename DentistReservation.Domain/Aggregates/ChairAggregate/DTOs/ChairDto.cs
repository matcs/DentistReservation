namespace DentistReservation.Domain.Aggregates.ChairAggregate.DTOs;

public class ChairDto
{
    public static implicit operator ChairDto(Chair chair)
    {
        return new ChairDto();
    }
}