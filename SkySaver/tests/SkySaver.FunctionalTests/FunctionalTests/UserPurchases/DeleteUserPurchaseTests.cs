namespace SkySaver.FunctionalTests.FunctionalTests.UserPurchases;

using SkySaver.SharedTestHelpers.Fakes.UserPurchase;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class DeleteUserPurchaseTests : TestBase
{
    [Fact]
    public async Task delete_userpurchase_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeUserPurchase = new FakeUserPurchaseBuilder().Build();
        await InsertAsync(fakeUserPurchase);

        // Act
        var route = ApiRoutes.UserPurchases.Delete(fakeUserPurchase.Id);
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}