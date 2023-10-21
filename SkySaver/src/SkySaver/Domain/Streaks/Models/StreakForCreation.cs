namespace SkySaver.Domain.Streaks.Models;

public sealed class StreakForCreation
{
    public int StreakID { get; set; }
    public int UserID { get; set; }
    public string StreakLevel { get; set; }
    public bool IsActive { get; set; }
}
