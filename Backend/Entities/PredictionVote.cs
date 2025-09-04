
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Entities;

public class PredictionVote
{
    [Key]
    public Guid VoteId { get; set; }
    public required Guid PredictionId { get; set; }
    public required Guid UserId { get; set; }
    public required bool PredictedOutcome { get; set; }
    public required DateTime VotedAt { get; set; }

    [ForeignKey("PredictionId")]
    public Prediction? Prediction { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }
}

/// <summary>
/// Represents a prediction along with its yes/no vote counts.
/// </summary>
public class PredictionVoteCountDto
{
    public required Guid PredictionId { get; set; }
    public required string PredictionName { get; set; }
    public required Guid CreatorId { get; set; }
    public required Guid TopicId { get; set; }
    public required DateTime PredictionDate { get; set; }
    public required DateTime ResolutionDate { get; set; }
    public required bool IsResolved { get; set; }
    public bool? IsCorrect { get; set; }
    public DateTime? ResolvedAt { get; set; }
    public required int YesVotes { get; set; }
    public required int NoVotes { get; set; }
}