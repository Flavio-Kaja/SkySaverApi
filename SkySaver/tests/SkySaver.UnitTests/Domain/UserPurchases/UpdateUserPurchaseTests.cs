namespace SkySaver.UnitTests.Domain.UserPurchases;

using SkySaver.SharedTestHelpers.Fakes.UserPurchase;
using SkySaver.Domain.UserPurchases;
using SkySaver.Domain.UserPurchases.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class UpdateUserPurchaseTests
{
    private readonly Faker _faker;

    public UpdateUserPurchaseTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_update_userPurchase()
    {
        // Arrange
        var fakeUserPurchase = new FakeUserPurchaseBuilder().Build();
        var updatedUserPurchase = new FakeUserPurchaseForUpdate().Generate();
        
        // Act
        fakeUserPurchase.Update(updatedUserPurchase);

        // Assert
        fakeUserPurchase.PurchaseID.Should().Be(updatedUserPurchase.PurchaseID);
        fakeUserPurchase.UserID.Should().Be(updatedUserPurchase.UserID);
        fakeUserPurchase.GoodID.Should().Be(updatedUserPurchase.GoodID);
        fakeUserPurchase.PurchaseDate.Should().BeCloseTo(updatedUserPurchase.PurchaseDate, 1.Seconds());
    }
    
    [Fact]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeUserPurchase = new FakeUserPurchaseBuilder().Build();
        var updatedUserPurchase = new FakeUserPurchaseForUpdate().Generate();
        fakeUserPurchase.DomainEvents.Clear();
        
        // Act
        fakeUserPurchase.Update(updatedUserPurchase);

        // Assert
        fakeUserPurchase.DomainEvents.Count.Should().Be(1);
        fakeUserPurchase.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(UserPurchaseUpdated));
    }
}