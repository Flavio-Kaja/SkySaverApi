namespace SkySaver.FunctionalTests.FunctionalTests.Users;

using SkySaver.SharedTestHelpers.Fakes.User;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class UpdateUserRecordTests : TestBase
{
    [Fact]
    public async Task put_user_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeUser = new FakeUserBuilder().Build();
        var updatedUserDto = new FakeUserForUpdateDto().Generate();
        await InsertAsync(fakeUser);

        // Act
        var route = ApiRoutes.Users.Put(fakeUser.Id);
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedUserDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}