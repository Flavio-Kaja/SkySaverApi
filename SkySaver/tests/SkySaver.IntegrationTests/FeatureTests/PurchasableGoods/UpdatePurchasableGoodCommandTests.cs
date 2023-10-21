namespace SkySaver.IntegrationTests.FeatureTests.PurchasableGoods;

using SkySaver.SharedTestHelpers.Fakes.PurchasableGood;
using SkySaver.Domain.PurchasableGoods.Dtos;
using SharedKernel.Exceptions;
using SkySaver.Domain.PurchasableGoods.Features;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class UpdatePurchasableGoodCommandTests : TestBase
{
    [Fact]
    public async Task can_update_existing_purchasablegood_in_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakePurchasableGoodOne = new FakePurchasableGoodBuilder().Build();
        var updatedPurchasableGoodDto = new FakePurchasableGoodForUpdateDto().Generate();
        await testingServiceScope.InsertAsync(fakePurchasableGoodOne);

        var purchasableGood = await testingServiceScope.ExecuteDbContextAsync(db => db.PurchasableGoods
            .FirstOrDefaultAsync(p => p.Id == fakePurchasableGoodOne.Id));

        // Act
        var command = new UpdatePurchasableGood.Command(purchasableGood.Id, updatedPurchasableGoodDto);
        await testingServiceScope.SendAsync(command);
        var updatedPurchasableGood = await testingServiceScope.ExecuteDbContextAsync(db => db.PurchasableGoods.FirstOrDefaultAsync(p => p.Id == purchasableGood.Id));

        // Assert
        updatedPurchasableGood.GoodID.Should().Be(updatedPurchasableGoodDto.GoodID);
        updatedPurchasableGood.Name.Should().Be(updatedPurchasableGoodDto.Name);
        updatedPurchasableGood.Description.Should().Be(updatedPurchasableGoodDto.Description);
        updatedPurchasableGood.PointsCost.Should().Be(updatedPurchasableGoodDto.PointsCost);
    }
}