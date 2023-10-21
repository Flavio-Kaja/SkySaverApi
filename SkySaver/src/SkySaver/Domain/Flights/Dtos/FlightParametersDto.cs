namespace SkySaver.Domain.Flights.Dtos;

using SharedKernel.Dtos;

public sealed class FlightParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}
