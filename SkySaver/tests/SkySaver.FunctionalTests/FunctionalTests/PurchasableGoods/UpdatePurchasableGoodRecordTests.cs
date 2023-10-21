namespace SkySaver.FunctionalTests.FunctionalTests.PurchasableGoods;

using SkySaver.SharedTestHelpers.Fakes.PurchasableGood;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class UpdatePurchasableGoodRecordTests : TestBase
{
    [Fact]
    public async Task put_purchasablegood_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakePurchasableGood = new FakePurchasableGoodBuilder().Build();
        var updatedPurchasableGoodDto = new FakePurchasableGoodForUpdateDto().Generate();
        await InsertAsync(fakePurchasableGood);

        // Act
        var route = ApiRoutes.PurchasableGoods.Put(fakePurchasableGood.Id);
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedPurchasableGoodDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}