namespace DentistReservation.Infrastructure.Data.Helpers;

public record Pagination(int PageIndex, int PageSize)
{
    public int PageIndex { get; } = PageIndex;
    
    public int PageSize { get; } = PageSize;
}