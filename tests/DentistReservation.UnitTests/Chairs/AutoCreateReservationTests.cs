namespace DentistReservation.UnitTests.Chairs;

public class AutoCreateReservationTests
{
    private readonly Mock<IChairRepository> _chairRepository = new();
    private readonly Mock<IReservationRepository> _reservationRepository = new();

    [Fact]
    public async void CreateSimpleReservation()
    {
        var chairOne = Chair.CreateInstance(
            "Chair One",
            1,
            8,
            0,
            18,
            0,
            45,
            5
        );

        var chairTwo = Chair.CreateInstance(
            "Chair Two",
            2,
            8,
            0,
            18,
            0,
            45,
            5
        );

        _chairRepository.Setup(cr => cr.ListAsync(
            It.IsAny<int>(),
            It.IsAny<int>(),
            default
        )).ReturnsAsync([
            chairOne,
            chairTwo
        ]);

        _reservationRepository.Setup(rr => rr.AddAsync(
                It.IsAny<Reservation>(),
                default))
            .Returns(Task.CompletedTask);

        var command = new AutoCreateReservationCommand();
        var handler = new AutoCreateReservationCommandHandler(
            _chairRepository.Object,
            _reservationRepository.Object);

        var result = await handler.Handle(command, default);
        await handler.Handle(command, default);

        result.HasError.Should().BeFalse();
        result.Value?.ReservationId.Should().NotBe(Guid.Empty);
        result.Value?.ChairNumber.Should().Be(1);
        result.Value?.TotalReservations.Should().BeGreaterThan(0);
    }

    [Fact]
    public async void CreateDistributedReservation()
    {
        var chairOne = Chair.CreateInstance(
            "Chair One",
            1,
            8,
            0,
            18,
            0,
            45,
            5
        );

        chairOne.AddAutomaticReservation();
        chairOne.AddAutomaticReservation();
        chairOne.AddAutomaticReservation();
        chairOne.AddAutomaticReservation();

        var chairTwo = Chair.CreateInstance(
            "Chair Two",
            2,
            8,
            0,
            18,
            0,
            45,
            5
        );

        chairTwo.AddAutomaticReservation();
        chairTwo.AddAutomaticReservation();

        var chairTree = Chair.CreateInstance(
            "Chair Tree",
            3,
            8,
            0,
            18,
            0,
            45,
            5
        );

        chairTree.AddAutomaticReservation();
        chairTree.AddAutomaticReservation();

        _chairRepository.Setup(cr => cr.ListAsync(
            It.IsAny<int>(),
            It.IsAny<int>(),
            default
        )).ReturnsAsync([
            chairOne,
            chairTwo,
            chairTree
        ]);

        _reservationRepository.Setup(rr => rr.AddAsync(
                It.IsAny<Reservation>(),
                default))
            .Returns(Task.CompletedTask);


        var command = new AutoCreateReservationCommand();
        var handler = new AutoCreateReservationCommandHandler(
            _chairRepository.Object,
            _reservationRepository.Object);

        var result = await handler.Handle(command, default);
        await handler.Handle(command, default);

        result.HasError.Should().BeFalse();
        result.Value?.ReservationId.Should().NotBe(Guid.Empty);
        result.Value?.ChairNumber.Should().Be(2);
        result.Value?.TotalReservations.Should().BeGreaterThan(2);
        result.Value?.From.Hour.Should().Be(9);
        result.Value?.From.Minute.Should().Be(40);
        result.Value?.Until.Hour.Should().Be(10);
        result.Value?.Until.Minute.Should().Be(30);
    }

    [Fact]
    public async void AllocateReservationToAnotherDayIfIsFull()
    {
        var chairOne = Chair.CreateInstance(
            "Chair One",
            1,
            8,
            0,
            9,
            0,
            50,
            10
        );

        chairOne.AddAutomaticReservation();
        chairOne.AddAutomaticReservation();
        chairOne.AddAutomaticReservation();

        _chairRepository.Setup(cr => cr.ListAsync(
            It.IsAny<int>(),
            It.IsAny<int>(),
            default
        )).ReturnsAsync([
            chairOne
        ]);

        _reservationRepository.Setup(rr => rr.AddAsync(
                It.IsAny<Reservation>(),
                default))
            .Returns(Task.CompletedTask);


        var command = new AutoCreateReservationCommand();
        var handler = new AutoCreateReservationCommandHandler(
            _chairRepository.Object,
            _reservationRepository.Object);

        var result = await handler.Handle(command, default);
        await handler.Handle(command, default);

        result.HasError.Should().BeFalse();
        result.Value?.ReservationId.Should().NotBe(Guid.Empty);
        result.Value?.TotalReservations.Should().BeGreaterThan(2);
        result.Value?.From.Hour.Should().Be(8);
        result.Value?.Until.Hour.Should().Be(9);
    }
}