namespace SkySaver.FunctionalTests.FunctionalTests.Streaks;

using SkySaver.SharedTestHelpers.Fakes.Streak;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class CreateStreakTests : TestBase
{
    [Fact]
    public async Task create_streak_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeStreak = new FakeStreakForCreationDto().Generate();

        // Act
        var route = ApiRoutes.Streaks.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeStreak);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}