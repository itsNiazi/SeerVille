namespace Backend.DTOs;

/// <summary>
/// Represents the prediction vote object sent to the client.
/// </summary>
public class PredictionVoteDto
{
    public required Guid VoteId { get; set; }
    public required Guid PredictionId { get; set; }
    public required Guid UserId { get; set; }
    public required bool PredictedOutcome { get; set; }
    public required DateTime VotedAt { get; set; }
}