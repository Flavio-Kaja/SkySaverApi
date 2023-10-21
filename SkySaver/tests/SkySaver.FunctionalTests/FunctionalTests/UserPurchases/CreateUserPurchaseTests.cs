namespace SkySaver.FunctionalTests.FunctionalTests.UserPurchases;

using SkySaver.SharedTestHelpers.Fakes.UserPurchase;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class CreateUserPurchaseTests : TestBase
{
    [Fact]
    public async Task create_userpurchase_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeUserPurchase = new FakeUserPurchaseForCreationDto().Generate();

        // Act
        var route = ApiRoutes.UserPurchases.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeUserPurchase);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}