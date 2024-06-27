using DentistReservation.Application.Chairs.Handlers;
using DentistReservation.Application.Chairs.Queries;

namespace DentistReservation.UnitTests.Chairs;

public class ReadChairTests
{
    private readonly Mock<IChairRepository> _chairRepository = new();
    private readonly Mock<IReservationRepository> _reservationRepository = new();

    [Fact]
    public async void GetSingleChair()
    {
        var chair = Chair.CreateInstance(
            "Description", 1,
            9,
            0,
            10,
            0,
            10,
            10);

        _chairRepository
            .Setup(c => c.GetAsync(
                It.IsAny<Guid>(),
                default))
            .ReturnsAsync(chair);
        
        _reservationRepository
            .Setup(c => c.ListByChairId(
                It.IsAny<Guid>(),
                default))
            .ReturnsAsync(new List<Reservation>());

        var query = new GetChairQuery(Guid.NewGuid());
        var handler = new GetChairQueryHandler(
            _chairRepository.Object,
            _reservationRepository.Object);

        var result = await handler.Handle(query, default);

        result.HasError.Should().BeFalse();
        result.Value.TotalReservations.Should().Be(0);
        result.Value.Id.Should().NotBe(new Guid());
        result.Value.Description.Should().Be(chair.Description);
        result.Value.Number.Should().Be(chair.Number);
        result.Value.StartAt.Should().Be("09:00");
        result.Value.EndAt.Should().Be("10:00");
        result.Value.AverageDuration.Should().Be(chair.AverageDuration);
        result.Value.AverageSetupInMinutes.Should().Be(chair.AverageSetupInMinutes);
    }
}