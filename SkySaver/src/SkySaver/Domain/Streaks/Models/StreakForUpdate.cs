namespace SkySaver.Domain.Streaks.Models;

public sealed class StreakForUpdate
{
    public Guid UserID { get; set; }
    public string StreakLevel { get; set; }
    public bool IsActive { get; set; }
}
