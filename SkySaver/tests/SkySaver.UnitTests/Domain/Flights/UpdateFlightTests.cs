namespace SkySaver.UnitTests.Domain.Flights;

using SkySaver.SharedTestHelpers.Fakes.Flight;
using SkySaver.Domain.Flights;
using SkySaver.Domain.Flights.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class UpdateFlightTests
{
    private readonly Faker _faker;

    public UpdateFlightTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_update_flight()
    {
        // Arrange
        var fakeFlight = new FakeFlightBuilder().Build();
        var updatedFlight = new FakeFlightForUpdate().Generate();
        
        // Act
        fakeFlight.Update(updatedFlight);

        // Assert
        fakeFlight.FlightID.Should().Be(updatedFlight.FlightID);
        fakeFlight.UserID.Should().Be(updatedFlight.UserID);
        fakeFlight.Departure.Should().Be(updatedFlight.Departure);
        fakeFlight.Arrival.Should().Be(updatedFlight.Arrival);
        fakeFlight.FlightDate.Should().BeCloseTo(updatedFlight.FlightDate, 1.Seconds());
        fakeFlight.PointsEarned.Should().Be(updatedFlight.PointsEarned);
    }
    
    [Fact]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeFlight = new FakeFlightBuilder().Build();
        var updatedFlight = new FakeFlightForUpdate().Generate();
        fakeFlight.DomainEvents.Clear();
        
        // Act
        fakeFlight.Update(updatedFlight);

        // Assert
        fakeFlight.DomainEvents.Count.Should().Be(1);
        fakeFlight.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(FlightUpdated));
    }
}