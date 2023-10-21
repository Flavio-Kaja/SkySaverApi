namespace SkySaver.FunctionalTests.FunctionalTests.PurchasableGoods;

using SkySaver.SharedTestHelpers.Fakes.PurchasableGood;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetPurchasableGoodListTests : TestBase
{
    [Fact]
    public async Task get_purchasablegood_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.PurchasableGoods.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}