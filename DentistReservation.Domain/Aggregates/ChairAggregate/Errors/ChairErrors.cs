using DentistReservation.Domain.Abstractions;

namespace DentistReservation.Domain.Aggregates.ChairAggregate.Errors;

public static class ChairErrors
{
    public static readonly Error NotFound = new("Chair.NotFound",
        "Unable to find Reservation");
    
    public static readonly Error ReservationAlreadyExists = new("Chair.ReservationAlreadyExists",
        "The reservation already has a reserved time.");  
    
    public static readonly Error ReservationAlreadyExistsNumber = new("Chair.ReservationAlreadyExistsNumber",
        "The reservation already has a reserved number.");
}