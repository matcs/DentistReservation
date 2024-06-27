using DentistReservation.Domain.Aggregates.ChairAggregate.DTOs;

namespace DentistReservation.Application.Chairs.Commands;

public class UpdateChairCommand(
    Guid id,
    string description,
    int number,
    int startHour,
    int startMinute,
    int endHour,
    int endMinute,
    int averageDuration,
    int averageSetupInMinutes) : IRequest<Result<ChairDto, Error>>
{
    public Guid Id { get; } = id;
    public string Description { get; } = description;
    public int Number { get; } = number;
    public int StartHour { get; } = startHour;
    public int StartMinute { get; } = startMinute;
    public int EndHour { get; } = endHour;
    public int EndMinute { get; } = endMinute;
    public int AverageDuration { get; } = averageDuration;
    public int AverageSetupInMinutes { get; } = averageSetupInMinutes;
}