using DentistReservation.Application.Chairs.Commands;
using DentistReservation.Application.Chairs.Handlers;

namespace DentistReservation.UnitTests.Chairs;

public class CreateChairTests
{
    private readonly Mock<IChairRepository> _chairRepository = new();

    [Fact]
    public async Task ShouldCreateAChairSuccessfully()
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
    
    [Theory]
    [InlineData(10, 0, 10, 30)]
    public async Task IfAvailableTimeInMinutesGreaterThanNecessaryTimeInMinutesShouldNotCreate(
        int startHour,
        int startMinute,
        int endHour,
        int endMinute)
    {
        var command = new CreateChairCommand(
            "Description",
            1,
            startHour,
            startMinute,
            endHour,
            endMinute,
            60,
            60
        );
        
        var handler = new CreateChairCommandHandler(
            _chairRepository.Object);

        var result = await handler.Handle(command, default);

        result.HasError.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(10, 0, 11, 30)]
    public async Task IfAvailableTimeInMinutesLessThanNecessaryTimeInMinutesShouldCreate(
        int startHour,
        int startMinute,
        int endHour,
        int endMinute)
    {
        var command = new CreateChairCommand(
            "Description",
            1,
            startHour,
            startMinute,
            endHour,
            endMinute,
            60,
            30
        );
        
        var handler = new CreateChairCommandHandler(
            _chairRepository.Object);

        var result = await handler.Handle(command, default);

        result.HasError.Should().BeFalse();
    }
}