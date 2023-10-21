namespace SkySaver.Domain.Streaks.DomainEvents;

public sealed class StreakCreated : DomainEvent
{
    public Streak Streak { get; set; } 
}
            