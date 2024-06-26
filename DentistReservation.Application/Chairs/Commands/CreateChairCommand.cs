using DentistReservation.Application.Chairs.Responses;

namespace DentistReservation.Application.Chairs.Commands;

public class CreateChairCommand(string description, int number, DateTime from, DateTime until)
    : IRequest<Result<CreateChairResponse, Error>>
{
    public string Description { get; } = description;
    public int Number { get; } = number;
}