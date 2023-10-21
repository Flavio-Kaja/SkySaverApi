namespace SkySaver.Domain.Flights.DomainEvents;

public sealed class FlightCreated : DomainEvent
{
    public Flight Flight { get; set; } 
}
            