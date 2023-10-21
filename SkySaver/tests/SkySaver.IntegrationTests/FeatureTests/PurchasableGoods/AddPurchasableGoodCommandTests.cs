namespace SkySaver.IntegrationTests.FeatureTests.PurchasableGoods;

using SkySaver.SharedTestHelpers.Fakes.PurchasableGood;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using SkySaver.Domain.PurchasableGoods.Features;
using SharedKernel.Exceptions;

public class AddPurchasableGoodCommandTests : TestBase
{
    [Fact]
    public async Task can_add_new_purchasablegood_to_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakePurchasableGoodOne = new FakePurchasableGoodForCreationDto().Generate();

        // Act
        var command = new AddPurchasableGood.Command(fakePurchasableGoodOne);
        var purchasableGoodReturned = await testingServiceScope.SendAsync(command);
        var purchasableGoodCreated = await testingServiceScope.ExecuteDbContextAsync(db => db.PurchasableGoods
            .FirstOrDefaultAsync(p => p.Id == purchasableGoodReturned.Id));

        // Assert
        purchasableGoodReturned.GoodID.Should().Be(fakePurchasableGoodOne.GoodID);
        purchasableGoodReturned.Name.Should().Be(fakePurchasableGoodOne.Name);
        purchasableGoodReturned.Description.Should().Be(fakePurchasableGoodOne.Description);
        purchasableGoodReturned.PointsCost.Should().Be(fakePurchasableGoodOne.PointsCost);

        purchasableGoodCreated.GoodID.Should().Be(fakePurchasableGoodOne.GoodID);
        purchasableGoodCreated.Name.Should().Be(fakePurchasableGoodOne.Name);
        purchasableGoodCreated.Description.Should().Be(fakePurchasableGoodOne.Description);
        purchasableGoodCreated.PointsCost.Should().Be(fakePurchasableGoodOne.PointsCost);
    }
}