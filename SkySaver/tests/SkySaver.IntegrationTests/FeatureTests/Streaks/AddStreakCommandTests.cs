namespace SkySaver.IntegrationTests.FeatureTests.Streaks;

using SkySaver.SharedTestHelpers.Fakes.Streak;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using SkySaver.Domain.Streaks.Features;
using SharedKernel.Exceptions;

public class AddStreakCommandTests : TestBase
{
    [Fact]
    public async Task can_add_new_streak_to_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeStreakOne = new FakeStreakForCreationDto().Generate();

        // Act
        var command = new AddStreak.Command(fakeStreakOne);
        var streakReturned = await testingServiceScope.SendAsync(command);
        var streakCreated = await testingServiceScope.ExecuteDbContextAsync(db => db.Streaks
            .FirstOrDefaultAsync(s => s.Id == streakReturned.Id));

        // Assert
        streakReturned.StreakID.Should().Be(fakeStreakOne.StreakID);
        streakReturned.UserID.Should().Be(fakeStreakOne.UserID);
        streakReturned.StreakLevel.Should().Be(fakeStreakOne.StreakLevel);
        streakReturned.IsActive.Should().Be(fakeStreakOne.IsActive);

        streakCreated.StreakID.Should().Be(fakeStreakOne.StreakID);
        streakCreated.UserID.Should().Be(fakeStreakOne.UserID);
        streakCreated.StreakLevel.Should().Be(fakeStreakOne.StreakLevel);
        streakCreated.IsActive.Should().Be(fakeStreakOne.IsActive);
    }
}