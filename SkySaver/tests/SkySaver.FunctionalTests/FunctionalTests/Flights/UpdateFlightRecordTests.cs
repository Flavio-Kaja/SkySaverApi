namespace SkySaver.FunctionalTests.FunctionalTests.Flights;

using SkySaver.SharedTestHelpers.Fakes.Flight;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class UpdateFlightRecordTests : TestBase
{
    [Fact]
    public async Task put_flight_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeFlight = new FakeFlightBuilder().Build();
        var updatedFlightDto = new FakeFlightForUpdateDto().Generate();
        await InsertAsync(fakeFlight);

        // Act
        var route = ApiRoutes.Flights.Put(fakeFlight.Id);
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedFlightDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}