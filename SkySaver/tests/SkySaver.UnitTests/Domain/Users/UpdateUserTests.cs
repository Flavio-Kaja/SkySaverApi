namespace SkySaver.UnitTests.Domain.Users;

using SkySaver.SharedTestHelpers.Fakes.User;
using SkySaver.Domain.Users;
using SkySaver.Domain.Users.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class UpdateUserTests
{
    private readonly Faker _faker;

    public UpdateUserTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_update_user()
    {
        // Arrange
        var fakeUser = new FakeUserBuilder().Build();
        var updatedUser = new FakeUserForUpdate().Generate();
        
        // Act
        fakeUser.Update(updatedUser);

        // Assert
        fakeUser.UserID.Should().Be(updatedUser.UserID);
        fakeUser.Email.Should().Be(updatedUser.Email);
        fakeUser.FirstName.Should().Be(updatedUser.FirstName);
        fakeUser.LastName.Should().Be(updatedUser.LastName);
        fakeUser.SkyPoints.Should().Be(updatedUser.SkyPoints);
        fakeUser.CurrentStreak.Should().Be(updatedUser.CurrentStreak);
        fakeUser.StreakLevel.Should().Be(updatedUser.StreakLevel);
    }
    
    [Fact]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeUser = new FakeUserBuilder().Build();
        var updatedUser = new FakeUserForUpdate().Generate();
        fakeUser.DomainEvents.Clear();
        
        // Act
        fakeUser.Update(updatedUser);

        // Assert
        fakeUser.DomainEvents.Count.Should().Be(1);
        fakeUser.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(UserUpdated));
    }
}