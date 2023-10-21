namespace SkySaver.FunctionalTests.FunctionalTests.ScavengerHunts;

using SkySaver.SharedTestHelpers.Fakes.ScavengerHunt;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetScavengerHuntListTests : TestBase
{
    [Fact]
    public async Task get_scavengerhunt_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.ScavengerHunts.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}