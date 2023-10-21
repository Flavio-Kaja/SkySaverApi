namespace SkySaver.Domain.Streaks.Dtos;

using SharedKernel.Dtos;

public sealed class StreakParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}
