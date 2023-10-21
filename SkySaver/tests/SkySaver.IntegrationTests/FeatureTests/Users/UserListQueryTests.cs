namespace SkySaver.IntegrationTests.FeatureTests.Users;

using SkySaver.Domain.Users.Dtos;
using SkySaver.SharedTestHelpers.Fakes.User;
using SharedKernel.Exceptions;
using SkySaver.Domain.Users.Features;
using FluentAssertions;
using Domain;
using Xunit;
using System.Threading.Tasks;

public class UserListQueryTests : TestBase
{
    
    [Fact]
    public async Task can_get_user_list()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserOne = new FakeUserBuilder().Build();
        var fakeUserTwo = new FakeUserBuilder().Build();
        var queryParameters = new UserParametersDto();

        await testingServiceScope.InsertAsync(fakeUserOne, fakeUserTwo);

        // Act
        var query = new GetUserList.Query(queryParameters);
        var users = await testingServiceScope.SendAsync(query);

        // Assert
        users.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}