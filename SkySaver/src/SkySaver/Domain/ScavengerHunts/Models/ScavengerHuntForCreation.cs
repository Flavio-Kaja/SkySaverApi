namespace SkySaver.Domain.ScavengerHunts.Models;

public sealed class ScavengerHuntForCreation
{
    public Guid UserID { get; set; }
    public int PointsEarned { get; set; }
    public DateTime CompletionDate { get; set; }
}
