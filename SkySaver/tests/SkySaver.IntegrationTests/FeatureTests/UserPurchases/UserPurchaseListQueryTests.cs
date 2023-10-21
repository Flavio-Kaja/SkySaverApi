namespace SkySaver.IntegrationTests.FeatureTests.UserPurchases;

using SkySaver.Domain.UserPurchases.Dtos;
using SkySaver.SharedTestHelpers.Fakes.UserPurchase;
using SharedKernel.Exceptions;
using SkySaver.Domain.UserPurchases.Features;
using FluentAssertions;
using Domain;
using Xunit;
using System.Threading.Tasks;

public class UserPurchaseListQueryTests : TestBase
{
    
    [Fact]
    public async Task can_get_userpurchase_list()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserPurchaseOne = new FakeUserPurchaseBuilder().Build();
        var fakeUserPurchaseTwo = new FakeUserPurchaseBuilder().Build();
        var queryParameters = new UserPurchaseParametersDto();

        await testingServiceScope.InsertAsync(fakeUserPurchaseOne, fakeUserPurchaseTwo);

        // Act
        var query = new GetUserPurchaseList.Query(queryParameters);
        var userPurchases = await testingServiceScope.SendAsync(query);

        // Assert
        userPurchases.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}