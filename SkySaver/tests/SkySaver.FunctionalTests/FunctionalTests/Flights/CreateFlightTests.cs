namespace SkySaver.FunctionalTests.FunctionalTests.Flights;

using SkySaver.SharedTestHelpers.Fakes.Flight;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class CreateFlightTests : TestBase
{
    [Fact]
    public async Task create_flight_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeFlight = new FakeFlightForCreationDto().Generate();

        // Act
        var route = ApiRoutes.Flights.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeFlight);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}