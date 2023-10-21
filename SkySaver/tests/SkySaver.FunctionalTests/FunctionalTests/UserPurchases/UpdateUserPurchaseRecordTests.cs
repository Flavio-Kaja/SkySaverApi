namespace SkySaver.FunctionalTests.FunctionalTests.UserPurchases;

using SkySaver.SharedTestHelpers.Fakes.UserPurchase;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class UpdateUserPurchaseRecordTests : TestBase
{
    [Fact]
    public async Task put_userpurchase_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeUserPurchase = new FakeUserPurchaseBuilder().Build();
        var updatedUserPurchaseDto = new FakeUserPurchaseForUpdateDto().Generate();
        await InsertAsync(fakeUserPurchase);

        // Act
        var route = ApiRoutes.UserPurchases.Put(fakeUserPurchase.Id);
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedUserPurchaseDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}