namespace SkySaver.IntegrationTests.FeatureTests.ScavengerHunts;

using SkySaver.Domain.ScavengerHunts.Dtos;
using SkySaver.SharedTestHelpers.Fakes.ScavengerHunt;
using SharedKernel.Exceptions;
using SkySaver.Domain.ScavengerHunts.Features;
using FluentAssertions;
using Domain;
using Xunit;
using System.Threading.Tasks;

public class ScavengerHuntListQueryTests : TestBase
{
    
    [Fact]
    public async Task can_get_scavengerhunt_list()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeScavengerHuntOne = new FakeScavengerHuntBuilder().Build();
        var fakeScavengerHuntTwo = new FakeScavengerHuntBuilder().Build();
        var queryParameters = new ScavengerHuntParametersDto();

        await testingServiceScope.InsertAsync(fakeScavengerHuntOne, fakeScavengerHuntTwo);

        // Act
        var query = new GetScavengerHuntList.Query(queryParameters);
        var scavengerHunts = await testingServiceScope.SendAsync(query);

        // Assert
        scavengerHunts.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}