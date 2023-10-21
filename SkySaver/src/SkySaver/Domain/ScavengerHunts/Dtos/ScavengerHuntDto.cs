namespace SkySaver.Domain.ScavengerHunts.Dtos;

public sealed class ScavengerHuntDto
{
    public Guid Id { get; set; }
    public int HuntID { get; set; }
    public Guid UserID { get; set; }
    public int PointsEarned { get; set; }
    public DateTime CompletionDate { get; set; }
}
