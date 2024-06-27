namespace DentistReservation.Domain.Abstractions;

public record Error(string Code, string Description = "", IEnumerable<string>? Errors = null)
{
    public static readonly Error None = new(string.Empty);
}