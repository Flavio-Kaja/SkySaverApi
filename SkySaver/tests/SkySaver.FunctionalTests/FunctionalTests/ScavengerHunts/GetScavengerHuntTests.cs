namespace SkySaver.FunctionalTests.FunctionalTests.ScavengerHunts;

using SkySaver.SharedTestHelpers.Fakes.ScavengerHunt;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetScavengerHuntTests : TestBase
{
    [Fact]
    public async Task get_scavengerhunt_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeScavengerHunt = new FakeScavengerHuntBuilder().Build();
        await InsertAsync(fakeScavengerHunt);

        // Act
        var route = ApiRoutes.ScavengerHunts.GetRecord(fakeScavengerHunt.Id);
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}