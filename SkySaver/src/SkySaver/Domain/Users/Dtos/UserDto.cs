namespace SkySaver.Domain.Users.Dtos;

public sealed class UserDto
{
    public Guid Id { get; set; }
    public int UserID { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int SkyPoints { get; set; }
    public int CurrentStreak { get; set; }
    public string StreakLevel { get; set; }
}
