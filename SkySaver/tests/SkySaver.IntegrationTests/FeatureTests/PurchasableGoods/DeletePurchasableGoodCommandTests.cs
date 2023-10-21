namespace SkySaver.IntegrationTests.FeatureTests.PurchasableGoods;

using SkySaver.SharedTestHelpers.Fakes.PurchasableGood;
using SkySaver.Domain.PurchasableGoods.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Domain;
using SharedKernel.Exceptions;
using System.Threading.Tasks;

public class DeletePurchasableGoodCommandTests : TestBase
{
    [Fact]
    public async Task can_delete_purchasablegood_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakePurchasableGoodOne = new FakePurchasableGoodBuilder().Build();
        await testingServiceScope.InsertAsync(fakePurchasableGoodOne);
        var purchasableGood = await testingServiceScope.ExecuteDbContextAsync(db => db.PurchasableGoods
            .FirstOrDefaultAsync(p => p.Id == fakePurchasableGoodOne.Id));

        // Act
        var command = new DeletePurchasableGood.Command(purchasableGood.Id);
        await testingServiceScope.SendAsync(command);
        var purchasableGoodResponse = await testingServiceScope.ExecuteDbContextAsync(db => db.PurchasableGoods.CountAsync(p => p.Id == purchasableGood.Id));

        // Assert
        purchasableGoodResponse.Should().Be(0);
    }

    [Fact]
    public async Task delete_purchasablegood_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var command = new DeletePurchasableGood.Command(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task can_softdelete_purchasablegood_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakePurchasableGoodOne = new FakePurchasableGoodBuilder().Build();
        await testingServiceScope.InsertAsync(fakePurchasableGoodOne);
        var purchasableGood = await testingServiceScope.ExecuteDbContextAsync(db => db.PurchasableGoods
            .FirstOrDefaultAsync(p => p.Id == fakePurchasableGoodOne.Id));

        // Act
        var command = new DeletePurchasableGood.Command(purchasableGood.Id);
        await testingServiceScope.SendAsync(command);
        var deletedPurchasableGood = await testingServiceScope.ExecuteDbContextAsync(db => db.PurchasableGoods
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == purchasableGood.Id));

        // Assert
        deletedPurchasableGood?.IsDeleted.Should().BeTrue();
    }
}