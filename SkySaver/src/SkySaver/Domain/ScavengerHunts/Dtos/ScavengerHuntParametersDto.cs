namespace SkySaver.Domain.ScavengerHunts.Dtos;

using SharedKernel.Dtos;

public sealed class ScavengerHuntParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}
