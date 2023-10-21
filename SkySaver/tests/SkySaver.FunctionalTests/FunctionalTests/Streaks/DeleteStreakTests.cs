namespace SkySaver.FunctionalTests.FunctionalTests.Streaks;

using SkySaver.SharedTestHelpers.Fakes.Streak;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class DeleteStreakTests : TestBase
{
    [Fact]
    public async Task delete_streak_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeStreak = new FakeStreakBuilder().Build();
        await InsertAsync(fakeStreak);

        // Act
        var route = ApiRoutes.Streaks.Delete(fakeStreak.Id);
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}