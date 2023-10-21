namespace SkySaver.IntegrationTests.FeatureTests.Flights;

using SkySaver.Domain.Flights.Dtos;
using SkySaver.SharedTestHelpers.Fakes.Flight;
using SharedKernel.Exceptions;
using SkySaver.Domain.Flights.Features;
using FluentAssertions;
using Domain;
using Xunit;
using System.Threading.Tasks;

public class FlightListQueryTests : TestBase
{
    
    [Fact]
    public async Task can_get_flight_list()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeFlightOne = new FakeFlightBuilder().Build();
        var fakeFlightTwo = new FakeFlightBuilder().Build();
        var queryParameters = new FlightParametersDto();

        await testingServiceScope.InsertAsync(fakeFlightOne, fakeFlightTwo);

        // Act
        var query = new GetFlightList.Query(queryParameters);
        var flights = await testingServiceScope.SendAsync(query);

        // Assert
        flights.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}