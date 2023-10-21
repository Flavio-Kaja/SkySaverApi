namespace SkySaver.FunctionalTests.FunctionalTests.UserPurchases;

using SkySaver.SharedTestHelpers.Fakes.UserPurchase;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetUserPurchaseListTests : TestBase
{
    [Fact]
    public async Task get_userpurchase_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.UserPurchases.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}