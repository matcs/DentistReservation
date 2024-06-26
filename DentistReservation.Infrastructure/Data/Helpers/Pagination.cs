namespace DentistReservation.Infrastructure.Data.Helpers;

public class Pagination(int pageIndex, int pageSize)
{
    public int PageIndex { get; } = pageIndex;
    
    public int PageSize { get; } = pageSize;
}