namespace SkySaver.Domain.UserPurchases.Dtos;

using SharedKernel.Dtos;

public sealed class UserPurchaseParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}
