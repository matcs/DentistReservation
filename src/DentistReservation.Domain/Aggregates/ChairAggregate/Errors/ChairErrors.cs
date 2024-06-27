using DentistReservation.Domain.Abstractions;

namespace DentistReservation.Domain.Aggregates.ChairAggregate.Errors;

public static class ChairErrors
{
    public static readonly Error NotFound = new("Chair.NotFound",
        "Unable to find Reservation");
    
    public static readonly Error IsEmpty = new("Chair.IsEmpty",
        "There is no chair the reserve.");  
    
    public static readonly Error HasAlreadyExistingNumberNumber = new("Chair.HasAlreadyExistingNumberNumber",
        "The reservation already has a reserved number.");
    
    public static readonly Error ThereReservationsAlready = new("Chair.ThereReservationsAlready",
        "The chair cannot be deleted as there are still reservations.");
}