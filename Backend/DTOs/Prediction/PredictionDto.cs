namespace Backend.DTOs;

/// <summary>
/// Represents the prediction object sent to the client.
/// </summary>
public class PredictionDto
{
    public required Guid PredictionId { get; set; }
    public required Guid CreatorId { get; set; }
    public required Guid TopicId { get; set; }
    public required string PredictionName { get; set; }
    public required string PredictionRules { get; set; }
    public required DateTime PredictionDate { get; set; }
    public required DateTime ResolutionDate { get; set; }
    public required bool IsResolved { get; set; }

    public bool? IsCorrect { get; set; }
    public DateTime? ResolvedAt { get; set; }
}