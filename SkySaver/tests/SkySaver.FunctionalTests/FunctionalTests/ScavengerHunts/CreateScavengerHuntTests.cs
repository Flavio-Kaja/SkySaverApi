namespace SkySaver.FunctionalTests.FunctionalTests.ScavengerHunts;

using SkySaver.SharedTestHelpers.Fakes.ScavengerHunt;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class CreateScavengerHuntTests : TestBase
{
    [Fact]
    public async Task create_scavengerhunt_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeScavengerHunt = new FakeScavengerHuntForCreationDto().Generate();

        // Act
        var route = ApiRoutes.ScavengerHunts.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeScavengerHunt);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}