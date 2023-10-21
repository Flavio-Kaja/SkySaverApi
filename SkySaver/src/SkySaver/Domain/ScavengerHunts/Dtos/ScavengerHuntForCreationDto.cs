namespace SkySaver.Domain.ScavengerHunts.Dtos;

public sealed class ScavengerHuntForCreationDto
{
    public int HuntID { get; set; }
    public int UserID { get; set; }
    public int PointsEarned { get; set; }
    public DateTime CompletionDate { get; set; }
}
