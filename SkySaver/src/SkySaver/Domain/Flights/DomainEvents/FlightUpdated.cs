namespace SkySaver.Domain.Flights.DomainEvents;

public sealed class FlightUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            