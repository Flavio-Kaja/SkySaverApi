namespace SkySaver.FunctionalTests.FunctionalTests.Users;

using SkySaver.SharedTestHelpers.Fakes.User;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetUserTests : TestBase
{
    [Fact]
    public async Task get_user_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeUser = new FakeUserBuilder().Build();
        await InsertAsync(fakeUser);

        // Act
        var route = ApiRoutes.Users.GetRecord(fakeUser.Id);
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}