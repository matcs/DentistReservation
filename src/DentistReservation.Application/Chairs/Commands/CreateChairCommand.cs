using DentistReservation.Application.Chairs.Responses;

namespace DentistReservation.Application.Chairs.Commands;

public class CreateChairCommand(
    string description,
    int number,
    int startHour,
    int startMinute,
    int endHour,
    int endMinute,
    int averageDuration,
    int averageSetupInMinutes)
    : IRequest<Result<CreateChairResponse, Error>>
{
    public string Description { get; } = description;
    public int Number { get; } = number;
    public int StartHour { get; } = startHour;
    public int StartMinute { get; } = startMinute;
    public int EndHour { get; } = endHour;
    public int EndMinute { get; } = endMinute;
    public int AverageDuration { get; } = averageDuration;
    public int AverageSetupInMinutes { get; } = averageSetupInMinutes;
}