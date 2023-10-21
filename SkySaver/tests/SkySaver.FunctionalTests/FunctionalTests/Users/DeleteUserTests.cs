namespace SkySaver.FunctionalTests.FunctionalTests.Users;

using SkySaver.SharedTestHelpers.Fakes.User;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class DeleteUserTests : TestBase
{
    [Fact]
    public async Task delete_user_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeUser = new FakeUserBuilder().Build();
        await InsertAsync(fakeUser);

        // Act
        var route = ApiRoutes.Users.Delete(fakeUser.Id);
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}