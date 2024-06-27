using System.Collections.ObjectModel;
using DentistReservation.Domain.Abstractions;
using DentistReservation.Domain.Aggregates.ChairAggregate.Reservations;
using DentistReservation.Domain.SharedKernel;

namespace DentistReservation.Domain.Aggregates.ChairAggregate;

public class Chair : BaseEntity<Guid>, IAggregateRoot
{
    public string? Description { get; private set; }

    public int Number { get; private set; }

    public int StartHour { get; private set; }

    public int StartMinute { get; private set; }

    public int EndHour { get; private set; }

    public int EndMinute { get; private set; }

    public int AverageDuration { get; private set; }

    public int AverageSetupInMinutes { get; private set; }

    public List<Reservation> Reservations { get; set; } = new List<Reservation>();

    private int NecessaryTimeInMinutes => AverageSetupInMinutes + AverageDuration;

    public Chair()
    {
    }

    private Chair(
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

    public Reservation AddAutomaticReservation()
    {
        var reservations =
            Reservations
                .OrderBy(r => r.From)
                .ToList();

        var reservation = new Reservation(Id, Number);

        SearchForAValidateReservation(0, reservations, out DateTime from, out DateTime until);

        if (from.Hour >= EndHour)
        {
            var lastDay = Reservations.Max(r => r.From.DayOfYear);
            var skipDays = lastDay + 1;
            DateTime dateTime = new DateTime(DateTime.UtcNow.Year, 1, 1).AddDays(lastDay);
            FirstOfTheDay(out from, out until, dateTime);
        }

        reservation.SetFromUntil(from, until);

        Reservations.Add(reservation);

        return reservation;
    }

    private bool SearchForAValidateReservation(int dayOfTheYear, List<Reservation> reservations,
        out DateTime from, out DateTime until)
    {
        from = new DateTime();
        until = new DateTime();

        DateTime referenceDay = DateTime.Today;

        bool hasValidReservationForToday = false;

        if (reservations.Count == 0)
        {
            FirstOfTheDay(out from, out until, referenceDay);

            hasValidReservationForToday = true;
        }

        if (reservations.Count == 1)
        {
            SameDay(reservations[0], out from, out until);

            hasValidReservationForToday = true;
        }

        if (reservations.Count > 1)
        {
            for (int index = 0; index < Reservations.Count; index++)
            {
                if (index == Reservations.Count - 1)
                {
                    var lastReservation = reservations[index];

                    SameDay(lastReservation, out from, out until);

                    hasValidReservationForToday = true;
                    break;
                }
            }
        }

        return hasValidReservationForToday;
    }

    private void SameDay(Reservation reservation, out DateTime from, out DateTime until)
    {
        from = reservation.Until;
        until = reservation.Until.AddMinutes(NecessaryTimeInMinutes);
    }

    private void FirstOfTheDay(out DateTime from, out DateTime until, DateTime referenceDay)
    {
        var startHour = referenceDay
            .AddHours(StartHour)
            .AddMinutes(StartMinute);

        from = startHour;
        until = startHour.AddMinutes(NecessaryTimeInMinutes);
    }

    public bool HasAnyAvailableReservations()
    {
        return SearchForAValidateReservation(0, Reservations, out _, out _);
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

    public void Update(string description, int number, int startHour, int startMinute, int endHour, int endMinute,
        int averageDuration, int averageSetupInMinutes)
    {
        Description = description;
        Number = number;
        StartHour = startHour;
        StartMinute = startMinute;
        EndHour = endHour;
        EndMinute = endMinute;
        AverageSetupInMinutes = averageSetupInMinutes;
        AverageDuration = averageDuration;
        Description = description;
    }
}