namespace SkySaver.IntegrationTests.FeatureTests.PurchasableGoods;

using SkySaver.SharedTestHelpers.Fakes.PurchasableGood;
using SkySaver.Domain.PurchasableGoods.Features;
using SharedKernel.Exceptions;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class PurchasableGoodQueryTests : TestBase
{
    [Fact]
    public async Task can_get_existing_purchasablegood_with_accurate_props()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakePurchasableGoodOne = new FakePurchasableGoodBuilder().Build();
        await testingServiceScope.InsertAsync(fakePurchasableGoodOne);

        // Act
        var query = new GetPurchasableGood.Query(fakePurchasableGoodOne.Id);
        var purchasableGood = await testingServiceScope.SendAsync(query);

        // Assert
        purchasableGood.GoodID.Should().Be(fakePurchasableGoodOne.GoodID);
        purchasableGood.Name.Should().Be(fakePurchasableGoodOne.Name);
        purchasableGood.Description.Should().Be(fakePurchasableGoodOne.Description);
        purchasableGood.PointsCost.Should().Be(fakePurchasableGoodOne.PointsCost);
    }

    [Fact]
    public async Task get_purchasablegood_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var query = new GetPurchasableGood.Query(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}