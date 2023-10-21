namespace SkySaver.SharedTestHelpers.Fakes.Flight;

using AutoBogus;
using SkySaver.Domain.Flights;
using SkySaver.Domain.Flights.Dtos;

public sealed class FakeFlightForCreationDto : AutoFaker<FlightForCreationDto>
{
    public FakeFlightForCreationDto()
    {
    }
}