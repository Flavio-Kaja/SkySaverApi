namespace SkySaver.FunctionalTests.FunctionalTests.PurchasableGoods;

using SkySaver.SharedTestHelpers.Fakes.PurchasableGood;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class DeletePurchasableGoodTests : TestBase
{
    [Fact]
    public async Task delete_purchasablegood_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakePurchasableGood = new FakePurchasableGoodBuilder().Build();
        await InsertAsync(fakePurchasableGood);

        // Act
        var route = ApiRoutes.PurchasableGoods.Delete(fakePurchasableGood.Id);
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}