namespace SkySaver.FunctionalTests.FunctionalTests.Users;

using SkySaver.SharedTestHelpers.Fakes.User;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class CreateUserTests : TestBase
{
    [Fact]
    public async Task create_user_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeUser = new FakeUserForCreationDto().Generate();

        // Act
        var route = ApiRoutes.Users.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeUser);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}