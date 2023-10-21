namespace SkySaver.FunctionalTests.FunctionalTests.Flights;

using SkySaver.SharedTestHelpers.Fakes.Flight;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetFlightListTests : TestBase
{
    [Fact]
    public async Task get_flight_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Flights.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}