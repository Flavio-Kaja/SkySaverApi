namespace SkySaver.SharedTestHelpers.Fakes.Streak;

using AutoBogus;
using SkySaver.Domain.Streaks;
using SkySaver.Domain.Streaks.Dtos;

public sealed class FakeStreakForCreationDto : AutoFaker<StreakForCreationDto>
{
    public FakeStreakForCreationDto()
    {
    }
}