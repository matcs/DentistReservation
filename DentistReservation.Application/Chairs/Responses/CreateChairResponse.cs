using DentistReservation.Domain.Aggregates.ChairAggregate;

namespace DentistReservation.Application.Chairs.Responses;

public class CreateChairResponse
{
    public static implicit operator CreateChairResponse(Chair chair)
    {
        return new CreateChairResponse()
        {

        };
    }
}