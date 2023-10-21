namespace SkySaver.Domain.Flights.Mappings;

using SkySaver.Domain.Flights.Dtos;
using SkySaver.Domain.Flights.Models;
using Riok.Mapperly.Abstractions;

[Mapper]
public static partial class FlightMapper
{
    public static partial FlightForCreation ToFlightForCreation(this FlightForCreationDto flightForCreationDto);
    public static partial FlightForUpdate ToFlightForUpdate(this FlightForUpdateDto flightForUpdateDto);
    public static partial FlightDto ToFlightDto(this Flight flight);
    public static partial IQueryable<FlightDto> ToFlightDtoQueryable(this IQueryable<Flight> flight);
}