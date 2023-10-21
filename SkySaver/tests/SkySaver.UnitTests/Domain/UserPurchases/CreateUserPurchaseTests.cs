namespace SkySaver.UnitTests.Domain.UserPurchases;

using SkySaver.SharedTestHelpers.Fakes.UserPurchase;
using SkySaver.Domain.UserPurchases;
using SkySaver.Domain.UserPurchases.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class CreateUserPurchaseTests
{
    private readonly Faker _faker;

    public CreateUserPurchaseTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_create_valid_userPurchase()
    {
        // Arrange
        var userPurchaseToCreate = new FakeUserPurchaseForCreation().Generate();
        
        // Act
        var fakeUserPurchase = UserPurchase.Create(userPurchaseToCreate);

        // Assert
        fakeUserPurchase.PurchaseID.Should().Be(userPurchaseToCreate.PurchaseID);
        fakeUserPurchase.UserID.Should().Be(userPurchaseToCreate.UserID);
        fakeUserPurchase.GoodID.Should().Be(userPurchaseToCreate.GoodID);
        fakeUserPurchase.PurchaseDate.Should().BeCloseTo(userPurchaseToCreate.PurchaseDate, 1.Seconds());
    }

    [Fact]
    public void queue_domain_event_on_create()
    {
        // Arrange
        var userPurchaseToCreate = new FakeUserPurchaseForCreation().Generate();
        
        // Act
        var fakeUserPurchase = UserPurchase.Create(userPurchaseToCreate);

        // Assert
        fakeUserPurchase.DomainEvents.Count.Should().Be(1);
        fakeUserPurchase.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(UserPurchaseCreated));
    }
}