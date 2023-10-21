namespace SkySaver.UnitTests.Domain.PurchasableGoods;

using SkySaver.SharedTestHelpers.Fakes.PurchasableGood;
using SkySaver.Domain.PurchasableGoods;
using SkySaver.Domain.PurchasableGoods.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class CreatePurchasableGoodTests
{
    private readonly Faker _faker;

    public CreatePurchasableGoodTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_create_valid_purchasableGood()
    {
        // Arrange
        var purchasableGoodToCreate = new FakePurchasableGoodForCreation().Generate();
        
        // Act
        var fakePurchasableGood = PurchasableGood.Create(purchasableGoodToCreate);

        // Assert
        fakePurchasableGood.GoodID.Should().Be(purchasableGoodToCreate.GoodID);
        fakePurchasableGood.Name.Should().Be(purchasableGoodToCreate.Name);
        fakePurchasableGood.Description.Should().Be(purchasableGoodToCreate.Description);
        fakePurchasableGood.PointsCost.Should().Be(purchasableGoodToCreate.PointsCost);
    }

    [Fact]
    public void queue_domain_event_on_create()
    {
        // Arrange
        var purchasableGoodToCreate = new FakePurchasableGoodForCreation().Generate();
        
        // Act
        var fakePurchasableGood = PurchasableGood.Create(purchasableGoodToCreate);

        // Assert
        fakePurchasableGood.DomainEvents.Count.Should().Be(1);
        fakePurchasableGood.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(PurchasableGoodCreated));
    }
}