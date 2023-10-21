namespace SkySaver.UnitTests.Domain.Streaks;

using SkySaver.SharedTestHelpers.Fakes.Streak;
using SkySaver.Domain.Streaks;
using SkySaver.Domain.Streaks.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class CreateStreakTests
{
    private readonly Faker _faker;

    public CreateStreakTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_create_valid_streak()
    {
        // Arrange
        var streakToCreate = new FakeStreakForCreation().Generate();
        
        // Act
        var fakeStreak = Streak.Create(streakToCreate);

        // Assert
        fakeStreak.StreakID.Should().Be(streakToCreate.StreakID);
        fakeStreak.UserID.Should().Be(streakToCreate.UserID);
        fakeStreak.StreakLevel.Should().Be(streakToCreate.StreakLevel);
        fakeStreak.IsActive.Should().Be(streakToCreate.IsActive);
    }

    [Fact]
    public void queue_domain_event_on_create()
    {
        // Arrange
        var streakToCreate = new FakeStreakForCreation().Generate();
        
        // Act
        var fakeStreak = Streak.Create(streakToCreate);

        // Assert
        fakeStreak.DomainEvents.Count.Should().Be(1);
        fakeStreak.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(StreakCreated));
    }
}