namespace SkySaver.FunctionalTests.FunctionalTests.Streaks;

using SkySaver.SharedTestHelpers.Fakes.Streak;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetStreakTests : TestBase
{
    [Fact]
    public async Task get_streak_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeStreak = new FakeStreakBuilder().Build();
        await InsertAsync(fakeStreak);

        // Act
        var route = ApiRoutes.Streaks.GetRecord(fakeStreak.Id);
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}