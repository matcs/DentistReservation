using DentistReservation.Domain.Aggregates.ChairAggregate.DTOs;

namespace DentistReservation.Application.Chairs.Queries;

public class GetChairQuery(Guid id) : IRequest<Result<ChairDto, Error>>
{
    public Guid Id { get; } = id;
}