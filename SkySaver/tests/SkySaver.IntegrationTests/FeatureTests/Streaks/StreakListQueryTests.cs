namespace SkySaver.IntegrationTests.FeatureTests.Streaks;

using SkySaver.Domain.Streaks.Dtos;
using SkySaver.SharedTestHelpers.Fakes.Streak;
using SharedKernel.Exceptions;
using SkySaver.Domain.Streaks.Features;
using FluentAssertions;
using Domain;
using Xunit;
using System.Threading.Tasks;

public class StreakListQueryTests : TestBase
{
    
    [Fact]
    public async Task can_get_streak_list()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeStreakOne = new FakeStreakBuilder().Build();
        var fakeStreakTwo = new FakeStreakBuilder().Build();
        var queryParameters = new StreakParametersDto();

        await testingServiceScope.InsertAsync(fakeStreakOne, fakeStreakTwo);

        // Act
        var query = new GetStreakList.Query(queryParameters);
        var streaks = await testingServiceScope.SendAsync(query);

        // Assert
        streaks.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}