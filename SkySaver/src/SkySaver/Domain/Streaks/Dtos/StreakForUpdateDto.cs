namespace SkySaver.Domain.Streaks.Dtos;

public sealed class StreakForUpdateDto
{
    public Guid UserID { get; set; }
    public string StreakLevel { get; set; }
    public bool IsActive { get; set; }
}
