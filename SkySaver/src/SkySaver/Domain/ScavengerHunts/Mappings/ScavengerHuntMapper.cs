namespace SkySaver.Domain.ScavengerHunts.Mappings;

using SkySaver.Domain.ScavengerHunts.Dtos;
using SkySaver.Domain.ScavengerHunts.Models;
using Riok.Mapperly.Abstractions;

[Mapper]
public static partial class ScavengerHuntMapper
{
    public static partial ScavengerHuntForCreation ToScavengerHuntForCreation(this ScavengerHuntForCreationDto scavengerHuntForCreationDto);
    public static partial ScavengerHuntForUpdate ToScavengerHuntForUpdate(this ScavengerHuntForUpdateDto scavengerHuntForUpdateDto);
    public static partial ScavengerHuntDto ToScavengerHuntDto(this ScavengerHunt scavengerHunt);
    public static partial IQueryable<ScavengerHuntDto> ToScavengerHuntDtoQueryable(this IQueryable<ScavengerHunt> scavengerHunt);
}