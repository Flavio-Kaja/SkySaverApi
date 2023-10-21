namespace SkySaver.SharedTestHelpers.Fakes.Flight;

using AutoBogus;
using SkySaver.Domain.Flights;
using SkySaver.Domain.Flights.Models;

public sealed class FakeFlightForUpdate : AutoFaker<FlightForUpdate>
{
    public FakeFlightForUpdate()
    {
    }
}