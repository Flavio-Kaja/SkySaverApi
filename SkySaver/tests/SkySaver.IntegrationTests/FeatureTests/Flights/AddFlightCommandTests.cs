namespace SkySaver.IntegrationTests.FeatureTests.Flights;

using SkySaver.SharedTestHelpers.Fakes.Flight;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using SkySaver.Domain.Flights.Features;
using SharedKernel.Exceptions;

public class AddFlightCommandTests : TestBase
{
    [Fact]
    public async Task can_add_new_flight_to_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeFlightOne = new FakeFlightForCreationDto().Generate();

        // Act
        var command = new AddFlight.Command(fakeFlightOne);
        var flightReturned = await testingServiceScope.SendAsync(command);
        var flightCreated = await testingServiceScope.ExecuteDbContextAsync(db => db.Flights
            .FirstOrDefaultAsync(f => f.Id == flightReturned.Id));

        // Assert
        flightReturned.FlightID.Should().Be(fakeFlightOne.FlightID);
        flightReturned.UserID.Should().Be(fakeFlightOne.UserID);
        flightReturned.Departure.Should().Be(fakeFlightOne.Departure);
        flightReturned.Arrival.Should().Be(fakeFlightOne.Arrival);
        flightReturned.FlightDate.Should().BeCloseTo(fakeFlightOne.FlightDate, 1.Seconds());
        flightReturned.PointsEarned.Should().Be(fakeFlightOne.PointsEarned);

        flightCreated.FlightID.Should().Be(fakeFlightOne.FlightID);
        flightCreated.UserID.Should().Be(fakeFlightOne.UserID);
        flightCreated.Departure.Should().Be(fakeFlightOne.Departure);
        flightCreated.Arrival.Should().Be(fakeFlightOne.Arrival);
        flightCreated.FlightDate.Should().BeCloseTo(fakeFlightOne.FlightDate, 1.Seconds());
        flightCreated.PointsEarned.Should().Be(fakeFlightOne.PointsEarned);
    }
}