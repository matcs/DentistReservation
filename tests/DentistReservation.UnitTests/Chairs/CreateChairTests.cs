using DentistReservation.Application.Chairs.Commands;
using DentistReservation.Application.Chairs.Handlers;

namespace DentistReservation.UnitTests.Chairs;

public class CreateChairTests
{
    private readonly Mock<IChairRepository> _chairRepository = new();

    [Fact]
    public async Task ShouldCreateAChairSuccefully()
    {
        var command = new CreateChairCommand(
            "Description",
            1,
            9,
            0,
            10,
            0,
            60,
            60
        );
        
        _chairRepository.Setup(cr => cr.AddAsync(
            It.IsAny<Chair>(),
            default
        )).Returns(Task.CompletedTask);
        
        var handler = new CreateChairCommandHandler(
            _chairRepository.Object);

        var result = await handler.Handle(command, default);

        result.HasError.Should().BeFalse();
    }
    
    [Fact]
    public async Task IfStartDateIsGreaterThanStartShouldNotCreate()
    {
        var command = new CreateChairCommand(
            "Description",
            1,
            10,
            0,
            9,
            0,
            60,
            60
        );
        
        var handler = new CreateChairCommandHandler(
            _chairRepository.Object);

        var result = await handler.Handle(command, default);

        result.HasError.Should().BeTrue();
    }
}