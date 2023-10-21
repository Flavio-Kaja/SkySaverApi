namespace SkySaver.FunctionalTests.FunctionalTests.PurchasableGoods;

using SkySaver.SharedTestHelpers.Fakes.PurchasableGood;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class CreatePurchasableGoodTests : TestBase
{
    [Fact]
    public async Task create_purchasablegood_returns_created_using_valid_dto()
    {
        // Arrange
        var fakePurchasableGood = new FakePurchasableGoodForCreationDto().Generate();

        // Act
        var route = ApiRoutes.PurchasableGoods.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakePurchasableGood);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}