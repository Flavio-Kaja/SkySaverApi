namespace SkySaver.UnitTests.Domain.PurchasableGoods;

using SkySaver.SharedTestHelpers.Fakes.PurchasableGood;
using SkySaver.Domain.PurchasableGoods;
using SkySaver.Domain.PurchasableGoods.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class UpdatePurchasableGoodTests
{
    private readonly Faker _faker;

    public UpdatePurchasableGoodTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_update_purchasableGood()
    {
        // Arrange
        var fakePurchasableGood = new FakePurchasableGoodBuilder().Build();
        var updatedPurchasableGood = new FakePurchasableGoodForUpdate().Generate();
        
        // Act
        fakePurchasableGood.Update(updatedPurchasableGood);

        // Assert
        fakePurchasableGood.GoodID.Should().Be(updatedPurchasableGood.GoodID);
        fakePurchasableGood.Name.Should().Be(updatedPurchasableGood.Name);
        fakePurchasableGood.Description.Should().Be(updatedPurchasableGood.Description);
        fakePurchasableGood.PointsCost.Should().Be(updatedPurchasableGood.PointsCost);
    }
    
    [Fact]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakePurchasableGood = new FakePurchasableGoodBuilder().Build();
        var updatedPurchasableGood = new FakePurchasableGoodForUpdate().Generate();
        fakePurchasableGood.DomainEvents.Clear();
        
        // Act
        fakePurchasableGood.Update(updatedPurchasableGood);

        // Assert
        fakePurchasableGood.DomainEvents.Count.Should().Be(1);
        fakePurchasableGood.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(PurchasableGoodUpdated));
    }
}