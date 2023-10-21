namespace SkySaver.Domain.ScavengerHunts.Models;

public sealed class ScavengerHuntForUpdate
{
    public int HuntID { get; set; }
    public int UserID { get; set; }
    public int PointsEarned { get; set; }
    public DateTime CompletionDate { get; set; }
}
