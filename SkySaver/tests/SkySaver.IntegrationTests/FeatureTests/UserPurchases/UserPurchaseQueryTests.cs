namespace SkySaver.IntegrationTests.FeatureTests.UserPurchases;

using SkySaver.SharedTestHelpers.Fakes.UserPurchase;
using SkySaver.Domain.UserPurchases.Features;
using SharedKernel.Exceptions;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class UserPurchaseQueryTests : TestBase
{
    [Fact]
    public async Task can_get_existing_userpurchase_with_accurate_props()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserPurchaseOne = new FakeUserPurchaseBuilder().Build();
        await testingServiceScope.InsertAsync(fakeUserPurchaseOne);

        // Act
        var query = new GetUserPurchase.Query(fakeUserPurchaseOne.Id);
        var userPurchase = await testingServiceScope.SendAsync(query);

        // Assert
        userPurchase.PurchaseID.Should().Be(fakeUserPurchaseOne.PurchaseID);
        userPurchase.UserID.Should().Be(fakeUserPurchaseOne.UserID);
        userPurchase.GoodID.Should().Be(fakeUserPurchaseOne.GoodID);
        userPurchase.PurchaseDate.Should().BeCloseTo(fakeUserPurchaseOne.PurchaseDate, 1.Seconds());
    }

    [Fact]
    public async Task get_userpurchase_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var query = new GetUserPurchase.Query(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}