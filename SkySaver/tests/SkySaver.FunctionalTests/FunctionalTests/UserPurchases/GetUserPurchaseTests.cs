namespace SkySaver.FunctionalTests.FunctionalTests.UserPurchases;

using SkySaver.SharedTestHelpers.Fakes.UserPurchase;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetUserPurchaseTests : TestBase
{
    [Fact]
    public async Task get_userpurchase_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeUserPurchase = new FakeUserPurchaseBuilder().Build();
        await InsertAsync(fakeUserPurchase);

        // Act
        var route = ApiRoutes.UserPurchases.GetRecord(fakeUserPurchase.Id);
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}