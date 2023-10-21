namespace SkySaver.FunctionalTests.FunctionalTests.ScavengerHunts;

using SkySaver.SharedTestHelpers.Fakes.ScavengerHunt;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class UpdateScavengerHuntRecordTests : TestBase
{
    [Fact]
    public async Task put_scavengerhunt_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeScavengerHunt = new FakeScavengerHuntBuilder().Build();
        var updatedScavengerHuntDto = new FakeScavengerHuntForUpdateDto().Generate();
        await InsertAsync(fakeScavengerHunt);

        // Act
        var route = ApiRoutes.ScavengerHunts.Put(fakeScavengerHunt.Id);
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedScavengerHuntDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}