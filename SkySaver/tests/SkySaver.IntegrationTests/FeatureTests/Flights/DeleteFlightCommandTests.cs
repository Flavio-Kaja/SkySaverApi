namespace SkySaver.IntegrationTests.FeatureTests.Flights;

using SkySaver.SharedTestHelpers.Fakes.Flight;
using SkySaver.Domain.Flights.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Domain;
using SharedKernel.Exceptions;
using System.Threading.Tasks;

public class DeleteFlightCommandTests : TestBase
{
    [Fact]
    public async Task can_delete_flight_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeFlightOne = new FakeFlightBuilder().Build();
        await testingServiceScope.InsertAsync(fakeFlightOne);
        var flight = await testingServiceScope.ExecuteDbContextAsync(db => db.Flights
            .FirstOrDefaultAsync(f => f.Id == fakeFlightOne.Id));

        // Act
        var command = new DeleteFlight.Command(flight.Id);
        await testingServiceScope.SendAsync(command);
        var flightResponse = await testingServiceScope.ExecuteDbContextAsync(db => db.Flights.CountAsync(f => f.Id == flight.Id));

        // Assert
        flightResponse.Should().Be(0);
    }

    [Fact]
    public async Task delete_flight_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteFlight.Command(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task can_softdelete_flight_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeFlightOne = new FakeFlightBuilder().Build();
        await testingServiceScope.InsertAsync(fakeFlightOne);
        var flight = await testingServiceScope.ExecuteDbContextAsync(db => db.Flights
            .FirstOrDefaultAsync(f => f.Id == fakeFlightOne.Id));

        // Act
        var command = new DeleteFlight.Command(flight.Id);
        await testingServiceScope.SendAsync(command);
        var deletedFlight = await testingServiceScope.ExecuteDbContextAsync(db => db.Flights
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == flight.Id));

        // Assert
        deletedFlight?.IsDeleted.Should().BeTrue();
    }
}