using System.Collections.ObjectModel;
using DentistReservation.Domain.Abstractions;
using DentistReservation.Domain.SharedKernel;

namespace DentistReservation.Domain.Aggregates.ChairAggregate;

public class Chair : BaseEntity<Guid>, IAggregateRoot
{
    public string? Description { get; private set; }

    public int Number { get; private set; }

    public int StartHour { get; }

    public int StartMinute { get; }

    public int EndHour { get; }

    public int EndMinute { get; }

    public int AverageDuration { get; set; }

    public int AverageSetupInMinutes { get; }
    public List<Reservation> Reservations { get; }

    public Chair(
        string? description, int number,
        int startHour, int startMinute,
        int endHour, int endMinute,
        int averageDuration, int averageSetupInMinutes,
        List<Reservation>? reservations = null)
    {
        Id = Guid.NewGuid();
        Number = number;
        StartHour = startHour;
        StartMinute = startMinute;
        EndHour = endHour;
        EndMinute = endMinute;
        AverageSetupInMinutes = averageSetupInMinutes;
        AverageDuration = averageDuration;
        Description = description;
        Reservations = reservations ?? [];
    }

    public Guid AddReservation(DateTime from, DateTime until)
    {
        var reservation = new Reservation(Id, Number);

        reservation.SetFromUntil(from, until);

        Reservations.Add(reservation);

        return reservation.Id;
    }

    public Reservation? AddAutomaticReservation()
    {
        var necessaryTimeInMinutes = AverageSetupInMinutes + AverageDuration;

        var reservations =
            Reservations
                .OrderBy(r => r.From)
                .ToList();

        var reservation = new Reservation(Id, Number);

        if (reservations.Count == 0)
        {
            var startHour = DateTime.Today
                .AddHours(StartHour)
                .AddMinutes(StartMinute);

            reservation.SetFromUntil(
                from: startHour,
                until: startHour.AddMinutes(necessaryTimeInMinutes));
        }
        else if (reservations.Count == 1)
        {
            reservation.SetFromUntil(
                from: reservations[0].Until,
                until: reservations[0].Until.AddMinutes(necessaryTimeInMinutes));
        }
        else if (reservations.Count > 1)
        {
            for (int index = 0; index < Reservations.Count; index++)
            {
                if (index == Reservations.Count - 1)
                {
                    var lastReservation = reservations[index];
                    reservation.SetFromUntil(
                        lastReservation.Until,
                        lastReservation.Until.AddMinutes(necessaryTimeInMinutes));
                    break;
                }

                var currentReservation = reservations[index];
                var nextReservation = reservations[index + 1];

                var timeSpan = currentReservation.From.Subtract(nextReservation.From);

                var difference = (int)timeSpan.TotalMinutes;

                if (difference <= necessaryTimeInMinutes)
                    continue;

                var from = currentReservation.From.AddMinutes(AverageSetupInMinutes);
                var until = from.AddMinutes(AverageDuration);

                reservation.SetFromUntil(from, until);
                break;
            }
        }

        Reservations.Add(reservation);

        return reservation;
    }

    public static Chair CreateInstance(
        string description, int number,
        int startHour, int startMinute,
        int endHour, int endMinute, 
        int averageDuration, int averageSetupInMinutes)
    {
        return new Chair(
            description, number,
            startHour, startMinute,
            endHour, endMinute,
            averageDuration, averageSetupInMinutes);
    }
}