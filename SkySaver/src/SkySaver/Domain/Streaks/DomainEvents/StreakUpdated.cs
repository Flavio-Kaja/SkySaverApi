namespace SkySaver.Domain.Streaks.DomainEvents;

public sealed class StreakUpdated : DomainEvent
{
    public Guid Id { get; set; } 
}
            