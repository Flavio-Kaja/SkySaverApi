namespace SkySaver.FunctionalTests.FunctionalTests.Streaks;

using SkySaver.SharedTestHelpers.Fakes.Streak;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class UpdateStreakRecordTests : TestBase
{
    [Fact]
    public async Task put_streak_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeStreak = new FakeStreakBuilder().Build();
        var updatedStreakDto = new FakeStreakForUpdateDto().Generate();
        await InsertAsync(fakeStreak);

        // Act
        var route = ApiRoutes.Streaks.Put(fakeStreak.Id);
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedStreakDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}