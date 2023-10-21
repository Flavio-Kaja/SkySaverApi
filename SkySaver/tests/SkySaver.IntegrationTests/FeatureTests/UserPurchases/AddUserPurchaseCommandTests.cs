namespace SkySaver.IntegrationTests.FeatureTests.UserPurchases;

using SkySaver.SharedTestHelpers.Fakes.UserPurchase;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using SkySaver.Domain.UserPurchases.Features;
using SharedKernel.Exceptions;

public class AddUserPurchaseCommandTests : TestBase
{
    [Fact]
    public async Task can_add_new_userpurchase_to_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserPurchaseOne = new FakeUserPurchaseForCreationDto().Generate();

        // Act
        var command = new AddUserPurchase.Command(fakeUserPurchaseOne);
        var userPurchaseReturned = await testingServiceScope.SendAsync(command);
        var userPurchaseCreated = await testingServiceScope.ExecuteDbContextAsync(db => db.UserPurchases
            .FirstOrDefaultAsync(u => u.Id == userPurchaseReturned.Id));

        // Assert
        userPurchaseReturned.PurchaseID.Should().Be(fakeUserPurchaseOne.PurchaseID);
        userPurchaseReturned.UserID.Should().Be(fakeUserPurchaseOne.UserID);
        userPurchaseReturned.GoodID.Should().Be(fakeUserPurchaseOne.GoodID);
        userPurchaseReturned.PurchaseDate.Should().BeCloseTo(fakeUserPurchaseOne.PurchaseDate, 1.Seconds());

        userPurchaseCreated.PurchaseID.Should().Be(fakeUserPurchaseOne.PurchaseID);
        userPurchaseCreated.UserID.Should().Be(fakeUserPurchaseOne.UserID);
        userPurchaseCreated.GoodID.Should().Be(fakeUserPurchaseOne.GoodID);
        userPurchaseCreated.PurchaseDate.Should().BeCloseTo(fakeUserPurchaseOne.PurchaseDate, 1.Seconds());
    }
}