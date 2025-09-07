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

/// <summary>
/// Represents a prediction along with its voting details
/// </summary>
public class PredictionSummaryDto
{
    // Prediction Entity Data
    public required Guid PredictionId { get; set; }
    public required string PredictionName { get; set; }
    public required string PredictionRules { get; set; }
    public required Guid CreatorId { get; set; }
    public required Guid TopicId { get; set; }
    public required DateTime PredictionDate { get; set; }
    public required DateTime ResolutionDate { get; set; }
    public required bool IsResolved { get; set; }
    public bool? IsCorrect { get; set; }
    public DateTime? ResolvedAt { get; set; }

    // Included from PredictionVote
    public required int YesVotes { get; set; }
    public required int NoVotes { get; set; }
    public required int TotalVotes { get; set; }
    public bool? PredictedOutcome { get; set; }
}