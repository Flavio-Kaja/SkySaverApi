namespace SkySaver.FunctionalTests.FunctionalTests.Flights;

using SkySaver.SharedTestHelpers.Fakes.Flight;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetFlightTests : TestBase
{
    [Fact]
    public async Task get_flight_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeFlight = new FakeFlightBuilder().Build();
        await InsertAsync(fakeFlight);

        // Act
        var route = ApiRoutes.Flights.GetRecord(fakeFlight.Id);
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}