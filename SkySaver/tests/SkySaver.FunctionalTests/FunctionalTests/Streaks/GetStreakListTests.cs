namespace SkySaver.FunctionalTests.FunctionalTests.Streaks;

using SkySaver.SharedTestHelpers.Fakes.Streak;
using SkySaver.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetStreakListTests : TestBase
{
    [Fact]
    public async Task get_streak_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Streaks.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}