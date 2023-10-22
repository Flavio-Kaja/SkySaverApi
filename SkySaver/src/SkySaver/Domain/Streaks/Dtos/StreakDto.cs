namespace SkySaver.Domain.Streaks.Dtos;

public sealed class StreakDto
{
    public Guid Id { get; set; }
    public Guid UserID { get; set; }
    public string StreakLevel { get; set; }
    public bool IsActive { get; set; }
}
