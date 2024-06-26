using DentistReservation.Domain.Aggregates.ChairAggregate.DTOs;

namespace DentistReservation.Application.Chairs.Commands;

public class DeleteChairCommand(Guid id) : IRequest<Result<ChairDto, Error>>
{
    public Guid Id { get; } = id;
}