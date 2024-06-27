using DentistReservation.Application.Chairs.Commands;
using DentistReservation.Application.Chairs.Handlers;

namespace DentistReservation.UnitTests.Chairs;

public class UpdateChairTests
{
    private readonly Mock<IChairRepository> _chairRepository = new();

    [Theory]
    [InlineData(1, -1, 0, 0, 0, 0, 0)]
    [InlineData(1, 0, -1, 0, 0, 0, 0)]
    [InlineData(1, 0, 0, -1, 0, 0, 0)]
    [InlineData(1, 0, 0, 0, -1, 0, 0)]
    [InlineData(1, 0, 0, 0, 0, -1, 0)]
    [InlineData(1, 0, 0, 0, 0, 0, -1)]
    public async void NegativeNumberForTimeShouldNotUpdate(
        int number,
        int startHour,
        int startMinute,
        int endHour,
        int endMinute,
        int averageDuration,
        int averageSetupInMinutes)
    {
        var chair = Chair.CreateInstance(
            "Chair One",
            1,
            8,
            0,
            18,
            0,
            45,
            5
        );

        _chairRepository.Setup(cr => cr.GetAsync(
            It.IsAny<Guid>(),
            default
        )).ReturnsAsync(chair);

        _chairRepository.Setup(cr => cr.GetByNumberAsync(
            It.IsAny<int>(),
            default
        )).ReturnsAsync(new Chair());

        var id = new Guid();
        var description = "description";

        var command = new UpdateChairCommand(
            id, description, number,
            startHour, startMinute,
            endHour, endMinute, averageDuration, averageSetupInMinutes);

        var handler = new UpdateChairCommandHandler(
            _chairRepository.Object);

        var result = await handler.Handle(command, default);

        result.HasError.Should().BeTrue();
    }
    
    [Theory]
    [InlineData(1, 1, 20, 2, 20, 0, 0)]
    public async void PositiveNumberForTimeShouldUpdate(
        int number,
        int startHour,
        int startMinute,
        int endHour,
        int endMinute,
        int averageDuration,
        int averageSetupInMinutes)
    {
        var chair = Chair.CreateInstance(
            "Chair One",
            1,
            8,
            0,
            18,
            0,
            45,
            5
        );

        _chairRepository.Setup(cr => cr.GetAsync(
            It.IsAny<Guid>(),
            default
        )).ReturnsAsync(chair);

        _chairRepository.Setup(cr => cr.GetByNumberAsync(
            It.IsAny<int>(),
            default
        )).ReturnsAsync(new Chair());

        var id = new Guid();
        var description = "description";

        var command = new UpdateChairCommand(
            id, description, number,
            startHour, startMinute,
            endHour, endMinute, averageDuration, averageSetupInMinutes);

        var handler = new UpdateChairCommandHandler(
            _chairRepository.Object);

        var result = await handler.Handle(command, default);

        result.HasError.Should().BeFalse();
    }
}