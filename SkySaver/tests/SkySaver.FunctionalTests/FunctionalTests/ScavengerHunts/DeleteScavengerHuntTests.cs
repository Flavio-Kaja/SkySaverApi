namespace SkySaver.FunctionalTests.FunctionalTests.ScavengerHunts;

using SkySaver.SharedTestHelpers.Fakes.ScavengerHunt;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class DeleteScavengerHuntTests : TestBase
{
    [Fact]
    public async Task delete_scavengerhunt_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeScavengerHunt = new FakeScavengerHuntBuilder().Build();
        await InsertAsync(fakeScavengerHunt);

        // Act
        var route = ApiRoutes.ScavengerHunts.Delete(fakeScavengerHunt.Id);
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}