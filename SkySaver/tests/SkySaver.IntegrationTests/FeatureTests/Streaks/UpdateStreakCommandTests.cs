namespace SkySaver.IntegrationTests.FeatureTests.Streaks;

using SkySaver.SharedTestHelpers.Fakes.Streak;
using SkySaver.Domain.Streaks.Dtos;
using SharedKernel.Exceptions;
using SkySaver.Domain.Streaks.Features;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

public class UpdateStreakCommandTests : TestBase
{
    [Fact]
    public async Task can_update_existing_streak_in_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeStreakOne = new FakeStreakBuilder().Build();
        var updatedStreakDto = new FakeStreakForUpdateDto().Generate();
        await testingServiceScope.InsertAsync(fakeStreakOne);

        var streak = await testingServiceScope.ExecuteDbContextAsync(db => db.Streaks
            .FirstOrDefaultAsync(s => s.Id == fakeStreakOne.Id));

        // Act
        var command = new UpdateStreak.Command(streak.Id, updatedStreakDto);
        await testingServiceScope.SendAsync(command);
        var updatedStreak = await testingServiceScope.ExecuteDbContextAsync(db => db.Streaks.FirstOrDefaultAsync(s => s.Id == streak.Id));

        // Assert
        updatedStreak.StreakID.Should().Be(updatedStreakDto.StreakID);
        updatedStreak.UserID.Should().Be(updatedStreakDto.UserID);
        updatedStreak.StreakLevel.Should().Be(updatedStreakDto.StreakLevel);
        updatedStreak.IsActive.Should().Be(updatedStreakDto.IsActive);
    }
}