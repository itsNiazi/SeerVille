namespace Backend.DTOs;

public class PredictionDto
{
    public Guid PredictionId { get; set; }
    public Guid CreatorId { get; set; }
    public Guid TopicId { get; set; }
    public required string PredictionName { get; set; }
    public DateTime PredictionDate { get; set; }
    public DateTime ResolutionDate { get; set; }
    public bool IsResolved { get; set; }
    public bool IsCorrect { get; set; }
    public DateTime ResolvedAt { get; set; }
}
