using DentistReservation.Domain.Aggregates.ChairAggregate.Reservations;

namespace DentistReservation.Domain.Aggregates.ChairAggregate.DTOs;

public class ChairDto
{
    public Guid Id { get; private set; }

    public string Description { get; private set; }

    public int Number { get; private set; }

    public string StartAt { get; private set; }

    public string EndAt { get; private set; }

    public int AverageDuration { get; private set; }

    public int AverageSetupInMinutes { get; private set; }
    public int TotalReservations => Reservations.Count(); 
    public IEnumerable<ReservationDto> Reservations { get; private set; }

    public static implicit operator ChairDto(Chair chair)
    {
        return new ChairDto
        {
            Id = chair.Id,
            Description = chair.Description,
            Number = chair.Number,
            StartAt = $"{FormatZeros(chair.StartHour)}:{FormatZeros(chair.StartMinute)}",
            EndAt = $"{FormatZeros(chair.EndHour)}:{FormatZeros(chair.EndMinute)}",
            AverageDuration = chair.AverageDuration,
            AverageSetupInMinutes = chair.AverageSetupInMinutes,
            Reservations = chair.Reservations.OrderBy(x => x.From).Select<Reservation, ReservationDto>(x => x)
        };
    }

    private static string FormatZeros(int time) => time < 10 ? $"0{time}" : time.ToString();
}