namespace SkySaver.FunctionalTests.FunctionalTests.Flights;

using SkySaver.SharedTestHelpers.Fakes.Flight;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class DeleteFlightTests : TestBase
{
    [Fact]
    public async Task delete_flight_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeFlight = new FakeFlightBuilder().Build();
        await InsertAsync(fakeFlight);

        // Act
        var route = ApiRoutes.Flights.Delete(fakeFlight.Id);
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}