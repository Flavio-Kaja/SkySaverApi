namespace SkySaver.FunctionalTests.FunctionalTests.PurchasableGoods;

using SkySaver.SharedTestHelpers.Fakes.PurchasableGood;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetPurchasableGoodTests : TestBase
{
    [Fact]
    public async Task get_purchasablegood_returns_success_when_entity_exists()
    {
        // Arrange
        var fakePurchasableGood = new FakePurchasableGoodBuilder().Build();
        await InsertAsync(fakePurchasableGood);

        // Act
        var route = ApiRoutes.PurchasableGoods.GetRecord(fakePurchasableGood.Id);
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}