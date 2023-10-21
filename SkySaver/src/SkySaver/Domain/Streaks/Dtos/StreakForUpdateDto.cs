namespace SkySaver.Domain.Streaks.Dtos;

public sealed class StreakForUpdateDto
{
    public int StreakID { get; set; }
    public int UserID { get; set; }
    public string StreakLevel { get; set; }
    public bool IsActive { get; set; }
}
