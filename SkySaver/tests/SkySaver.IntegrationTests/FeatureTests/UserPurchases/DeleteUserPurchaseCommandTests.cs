namespace SkySaver.IntegrationTests.FeatureTests.UserPurchases;

using SkySaver.SharedTestHelpers.Fakes.UserPurchase;
using SkySaver.Domain.UserPurchases.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Domain;
using SharedKernel.Exceptions;
using System.Threading.Tasks;

public class DeleteUserPurchaseCommandTests : TestBase
{
    [Fact]
    public async Task can_delete_userpurchase_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserPurchaseOne = new FakeUserPurchaseBuilder().Build();
        await testingServiceScope.InsertAsync(fakeUserPurchaseOne);
        var userPurchase = await testingServiceScope.ExecuteDbContextAsync(db => db.UserPurchases
            .FirstOrDefaultAsync(u => u.Id == fakeUserPurchaseOne.Id));

        // Act
        var command = new DeleteUserPurchase.Command(userPurchase.Id);
        await testingServiceScope.SendAsync(command);
        var userPurchaseResponse = await testingServiceScope.ExecuteDbContextAsync(db => db.UserPurchases.CountAsync(u => u.Id == userPurchase.Id));

        // Assert
        userPurchaseResponse.Should().Be(0);
    }

    [Fact]
    public async Task delete_userpurchase_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteUserPurchase.Command(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task can_softdelete_userpurchase_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserPurchaseOne = new FakeUserPurchaseBuilder().Build();
        await testingServiceScope.InsertAsync(fakeUserPurchaseOne);
        var userPurchase = await testingServiceScope.ExecuteDbContextAsync(db => db.UserPurchases
            .FirstOrDefaultAsync(u => u.Id == fakeUserPurchaseOne.Id));

        // Act
        var command = new DeleteUserPurchase.Command(userPurchase.Id);
        await testingServiceScope.SendAsync(command);
        var deletedUserPurchase = await testingServiceScope.ExecuteDbContextAsync(db => db.UserPurchases
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == userPurchase.Id));

        // Assert
        deletedUserPurchase?.IsDeleted.Should().BeTrue();
    }
}