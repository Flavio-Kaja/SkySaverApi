namespace SkySaver.IntegrationTests.FeatureTests.Streaks;

using SkySaver.SharedTestHelpers.Fakes.Streak;
using SkySaver.Domain.Streaks.Features;
using SharedKernel.Exceptions;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class StreakQueryTests : TestBase
{
    [Fact]
    public async Task can_get_existing_streak_with_accurate_props()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeStreakOne = new FakeStreakBuilder().Build();
        await testingServiceScope.InsertAsync(fakeStreakOne);

        // Act
        var query = new GetStreak.Query(fakeStreakOne.Id);
        var streak = await testingServiceScope.SendAsync(query);

        // Assert
        streak.StreakID.Should().Be(fakeStreakOne.StreakID);
        streak.UserID.Should().Be(fakeStreakOne.UserID);
        streak.StreakLevel.Should().Be(fakeStreakOne.StreakLevel);
        streak.IsActive.Should().Be(fakeStreakOne.IsActive);
    }

    [Fact]
    public async Task get_streak_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var query = new GetStreak.Query(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}