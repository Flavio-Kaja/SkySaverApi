namespace SkySaver.IntegrationTests.FeatureTests.Flights;

using SkySaver.SharedTestHelpers.Fakes.Flight;
using SkySaver.Domain.Flights.Dtos;
using SharedKernel.Exceptions;
using SkySaver.Domain.Flights.Features;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class UpdateFlightCommandTests : TestBase
{
    [Fact]
    public async Task can_update_existing_flight_in_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeFlightOne = new FakeFlightBuilder().Build();
        var updatedFlightDto = new FakeFlightForUpdateDto().Generate();
        await testingServiceScope.InsertAsync(fakeFlightOne);

        var flight = await testingServiceScope.ExecuteDbContextAsync(db => db.Flights
            .FirstOrDefaultAsync(f => f.Id == fakeFlightOne.Id));

        // Act
        var command = new UpdateFlight.Command(flight.Id, updatedFlightDto);
        await testingServiceScope.SendAsync(command);
        var updatedFlight = await testingServiceScope.ExecuteDbContextAsync(db => db.Flights.FirstOrDefaultAsync(f => f.Id == flight.Id));

        // Assert
        updatedFlight.FlightID.Should().Be(updatedFlightDto.FlightID);
        updatedFlight.UserID.Should().Be(updatedFlightDto.UserID);
        updatedFlight.Departure.Should().Be(updatedFlightDto.Departure);
        updatedFlight.Arrival.Should().Be(updatedFlightDto.Arrival);
        updatedFlight.FlightDate.Should().BeCloseTo(updatedFlightDto.FlightDate, 1.Seconds());
        updatedFlight.PointsEarned.Should().Be(updatedFlightDto.PointsEarned);
    }
}