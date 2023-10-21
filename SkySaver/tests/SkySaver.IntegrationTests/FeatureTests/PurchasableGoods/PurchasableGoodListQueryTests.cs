namespace SkySaver.IntegrationTests.FeatureTests.PurchasableGoods;

using SkySaver.Domain.PurchasableGoods.Dtos;
using SkySaver.SharedTestHelpers.Fakes.PurchasableGood;
using SharedKernel.Exceptions;
using SkySaver.Domain.PurchasableGoods.Features;
using FluentAssertions;
using Domain;
using Xunit;
using System.Threading.Tasks;

public class PurchasableGoodListQueryTests : TestBase
{
    
    [Fact]
    public async Task can_get_purchasablegood_list()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakePurchasableGoodOne = new FakePurchasableGoodBuilder().Build();
        var fakePurchasableGoodTwo = new FakePurchasableGoodBuilder().Build();
        var queryParameters = new PurchasableGoodParametersDto();

        await testingServiceScope.InsertAsync(fakePurchasableGoodOne, fakePurchasableGoodTwo);

        // Act
        var query = new GetPurchasableGoodList.Query(queryParameters);
        var purchasableGoods = await testingServiceScope.SendAsync(query);

        // Assert
        purchasableGoods.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}