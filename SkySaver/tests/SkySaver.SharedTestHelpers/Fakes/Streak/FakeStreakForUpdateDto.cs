namespace SkySaver.SharedTestHelpers.Fakes.Streak;

using AutoBogus;
using SkySaver.Domain.Streaks;
using SkySaver.Domain.Streaks.Dtos;

public sealed class FakeStreakForUpdateDto : AutoFaker<StreakForUpdateDto>
{
    public FakeStreakForUpdateDto()
    {
    }
}