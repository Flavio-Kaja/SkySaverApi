namespace SkySaver.Domain.ScavengerHunts.Dtos;

public sealed class ScavengerHuntForUpdateDto
{
    public Guid UserID { get; set; }
    public int PointsEarned { get; set; }
    public DateTime CompletionDate { get; set; }
}
