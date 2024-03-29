namespace SkySaver.Domain.Users.Dtos;

public sealed class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public int DailyGoal { get; set; }

}
