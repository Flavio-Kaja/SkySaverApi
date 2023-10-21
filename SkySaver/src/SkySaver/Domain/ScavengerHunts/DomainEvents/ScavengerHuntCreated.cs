namespace SkySaver.Domain.ScavengerHunts.DomainEvents;

public sealed class ScavengerHuntCreated : DomainEvent
{
    public ScavengerHunt ScavengerHunt { get; set; } 
}
            