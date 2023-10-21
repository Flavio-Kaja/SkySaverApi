namespace SkySaver.Domain.Streaks.Mappings;

using SkySaver.Domain.Streaks.Dtos;
using SkySaver.Domain.Streaks.Models;
using Riok.Mapperly.Abstractions;

[Mapper]
public static partial class StreakMapper
{
    public static partial StreakForCreation ToStreakForCreation(this StreakForCreationDto streakForCreationDto);
    public static partial StreakForUpdate ToStreakForUpdate(this StreakForUpdateDto streakForUpdateDto);
    public static partial StreakDto ToStreakDto(this Streak streak);
    public static partial IQueryable<StreakDto> ToStreakDtoQueryable(this IQueryable<Streak> streak);
}