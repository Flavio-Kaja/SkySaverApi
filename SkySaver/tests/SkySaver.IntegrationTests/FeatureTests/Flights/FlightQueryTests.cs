namespace SkySaver.IntegrationTests.FeatureTests.Flights;

using SkySaver.SharedTestHelpers.Fakes.Flight;
using SkySaver.Domain.Flights.Features;
using SharedKernel.Exceptions;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class FlightQueryTests : TestBase
{
    [Fact]
    public async Task can_get_existing_flight_with_accurate_props()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeFlightOne = new FakeFlightBuilder().Build();
        await testingServiceScope.InsertAsync(fakeFlightOne);

        // Act
        var query = new GetFlight.Query(fakeFlightOne.Id);
        var flight = await testingServiceScope.SendAsync(query);

        // Assert
        flight.FlightID.Should().Be(fakeFlightOne.FlightID);
        flight.UserID.Should().Be(fakeFlightOne.UserID);
        flight.Departure.Should().Be(fakeFlightOne.Departure);
        flight.Arrival.Should().Be(fakeFlightOne.Arrival);
        flight.FlightDate.Should().BeCloseTo(fakeFlightOne.FlightDate, 1.Seconds());
        flight.PointsEarned.Should().Be(fakeFlightOne.PointsEarned);
    }

    [Fact]
    public async Task get_flight_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var query = new GetFlight.Query(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}