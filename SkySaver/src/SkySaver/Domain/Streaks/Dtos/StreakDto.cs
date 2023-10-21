namespace SkySaver.Domain.Streaks.Dtos;

public sealed class StreakDto
{
    public Guid Id { get; set; }
    public int StreakID { get; set; }
    public int UserID { get; set; }
    public string StreakLevel { get; set; }
    public bool IsActive { get; set; }
}
