namespace SkySaver.UnitTests.Domain.Flights;

using SkySaver.SharedTestHelpers.Fakes.Flight;
using SkySaver.Domain.Flights;
using SkySaver.Domain.Flights.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class CreateFlightTests
{
    private readonly Faker _faker;

    public CreateFlightTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_create_valid_flight()
    {
        // Arrange
        var flightToCreate = new FakeFlightForCreation().Generate();
        
        // Act
        var fakeFlight = Flight.Create(flightToCreate);

        // Assert
        fakeFlight.FlightID.Should().Be(flightToCreate.FlightID);
        fakeFlight.UserID.Should().Be(flightToCreate.UserID);
        fakeFlight.Departure.Should().Be(flightToCreate.Departure);
        fakeFlight.Arrival.Should().Be(flightToCreate.Arrival);
        fakeFlight.FlightDate.Should().BeCloseTo(flightToCreate.FlightDate, 1.Seconds());
        fakeFlight.PointsEarned.Should().Be(flightToCreate.PointsEarned);
    }

    [Fact]
    public void queue_domain_event_on_create()
    {
        // Arrange
        var flightToCreate = new FakeFlightForCreation().Generate();
        
        // Act
        var fakeFlight = Flight.Create(flightToCreate);

        // Assert
        fakeFlight.DomainEvents.Count.Should().Be(1);
        fakeFlight.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(FlightCreated));
    }
}