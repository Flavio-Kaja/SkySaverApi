namespace SkySaver.IntegrationTests.FeatureTests.Streaks;

using SkySaver.SharedTestHelpers.Fakes.Streak;
using SkySaver.Domain.Streaks.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Domain;
using SharedKernel.Exceptions;
using System.Threading.Tasks;

public class DeleteStreakCommandTests : TestBase
{
    [Fact]
    public async Task can_delete_streak_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeStreakOne = new FakeStreakBuilder().Build();
        await testingServiceScope.InsertAsync(fakeStreakOne);
        var streak = await testingServiceScope.ExecuteDbContextAsync(db => db.Streaks
            .FirstOrDefaultAsync(s => s.Id == fakeStreakOne.Id));

        // Act
        var command = new DeleteStreak.Command(streak.Id);
        await testingServiceScope.SendAsync(command);
        var streakResponse = await testingServiceScope.ExecuteDbContextAsync(db => db.Streaks.CountAsync(s => s.Id == streak.Id));

        // Assert
        streakResponse.Should().Be(0);
    }

    [Fact]
    public async Task delete_streak_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteStreak.Command(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task can_softdelete_streak_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeStreakOne = new FakeStreakBuilder().Build();
        await testingServiceScope.InsertAsync(fakeStreakOne);
        var streak = await testingServiceScope.ExecuteDbContextAsync(db => db.Streaks
            .FirstOrDefaultAsync(s => s.Id == fakeStreakOne.Id));

        // Act
        var command = new DeleteStreak.Command(streak.Id);
        await testingServiceScope.SendAsync(command);
        var deletedStreak = await testingServiceScope.ExecuteDbContextAsync(db => db.Streaks
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == streak.Id));

        // Assert
        deletedStreak?.IsDeleted.Should().BeTrue();
    }
}