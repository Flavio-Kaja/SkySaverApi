namespace SkySaver.IntegrationTests.FeatureTests.UserPurchases;

using SkySaver.SharedTestHelpers.Fakes.UserPurchase;
using SkySaver.Domain.UserPurchases.Dtos;
using SharedKernel.Exceptions;
using SkySaver.Domain.UserPurchases.Features;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class UpdateUserPurchaseCommandTests : TestBase
{
    [Fact]
    public async Task can_update_existing_userpurchase_in_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserPurchaseOne = new FakeUserPurchaseBuilder().Build();
        var updatedUserPurchaseDto = new FakeUserPurchaseForUpdateDto().Generate();
        await testingServiceScope.InsertAsync(fakeUserPurchaseOne);

        var userPurchase = await testingServiceScope.ExecuteDbContextAsync(db => db.UserPurchases
            .FirstOrDefaultAsync(u => u.Id == fakeUserPurchaseOne.Id));

        // Act
        var command = new UpdateUserPurchase.Command(userPurchase.Id, updatedUserPurchaseDto);
        await testingServiceScope.SendAsync(command);
        var updatedUserPurchase = await testingServiceScope.ExecuteDbContextAsync(db => db.UserPurchases.FirstOrDefaultAsync(u => u.Id == userPurchase.Id));

        // Assert
        updatedUserPurchase.PurchaseID.Should().Be(updatedUserPurchaseDto.PurchaseID);
        updatedUserPurchase.UserID.Should().Be(updatedUserPurchaseDto.UserID);
        updatedUserPurchase.GoodID.Should().Be(updatedUserPurchaseDto.GoodID);
        updatedUserPurchase.PurchaseDate.Should().BeCloseTo(updatedUserPurchaseDto.PurchaseDate, 1.Seconds());
    }
}