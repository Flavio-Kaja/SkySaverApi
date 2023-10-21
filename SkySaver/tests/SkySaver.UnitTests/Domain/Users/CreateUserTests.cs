namespace SkySaver.UnitTests.Domain.Users;

using SkySaver.SharedTestHelpers.Fakes.User;
using SkySaver.Domain.Users;
using SkySaver.Domain.Users.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class CreateUserTests
{
    private readonly Faker _faker;

    public CreateUserTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_create_valid_user()
    {
        // Arrange
        var userToCreate = new FakeUserForCreation().Generate();
        
        // Act
        var fakeUser = User.Create(userToCreate);

        // Assert
        fakeUser.UserID.Should().Be(userToCreate.UserID);
        fakeUser.Email.Should().Be(userToCreate.Email);
        fakeUser.FirstName.Should().Be(userToCreate.FirstName);
        fakeUser.LastName.Should().Be(userToCreate.LastName);
        fakeUser.SkyPoints.Should().Be(userToCreate.SkyPoints);
        fakeUser.CurrentStreak.Should().Be(userToCreate.CurrentStreak);
        fakeUser.StreakLevel.Should().Be(userToCreate.StreakLevel);
    }

    [Fact]
    public void queue_domain_event_on_create()
    {
        // Arrange
        var userToCreate = new FakeUserForCreation().Generate();
        
        // Act
        var fakeUser = User.Create(userToCreate);

        // Assert
        fakeUser.DomainEvents.Count.Should().Be(1);
        fakeUser.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(UserCreated));
    }
}