namespace SkySaver.UnitTests.Domain.Streaks;

using SkySaver.SharedTestHelpers.Fakes.Streak;
using SkySaver.Domain.Streaks;
using SkySaver.Domain.Streaks.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class UpdateStreakTests
{
    private readonly Faker _faker;

    public UpdateStreakTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_update_streak()
    {
        // Arrange
        var fakeStreak = new FakeStreakBuilder().Build();
        var updatedStreak = new FakeStreakForUpdate().Generate();
        
        // Act
        fakeStreak.Update(updatedStreak);

        // Assert
        fakeStreak.StreakID.Should().Be(updatedStreak.StreakID);
        fakeStreak.UserID.Should().Be(updatedStreak.UserID);
        fakeStreak.StreakLevel.Should().Be(updatedStreak.StreakLevel);
        fakeStreak.IsActive.Should().Be(updatedStreak.IsActive);
    }
    
    [Fact]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeStreak = new FakeStreakBuilder().Build();
        var updatedStreak = new FakeStreakForUpdate().Generate();
        fakeStreak.DomainEvents.Clear();
        
        // Act
        fakeStreak.Update(updatedStreak);

        // Assert
        fakeStreak.DomainEvents.Count.Should().Be(1);
        fakeStreak.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(StreakUpdated));
    }
}