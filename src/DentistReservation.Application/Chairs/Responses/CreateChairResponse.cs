using DentistReservation.Domain.Aggregates.ChairAggregate;

namespace DentistReservation.Application.Chairs.Responses;

public class CreateChairResponse
{
    public Guid Id { get; private set; }

    public string Description { get; private set; }

    public int Number { get; private set; }

    public string StartAt { get; private set; }

    public string EndAt { get; private set; }

    public int AverageDuration { get; private set; }

    public int AverageSetupInMinutes { get; private set; }

    public static implicit operator CreateChairResponse(Chair chair)
    {
        return new CreateChairResponse
        {
            Id = chair.Id,
            Description = chair.Description,
            Number = chair.Number,
            StartAt = $"{chair.StartHour}:{chair.StartMinute}",
            EndAt = $"{chair.EndHour}:{chair.EndMinute}",
            AverageDuration = chair.AverageDuration,
            AverageSetupInMinutes = chair.AverageSetupInMinutes,
        };
    }
}
