namespace Backend.DTOs;

public class PredictionVoteDto
{
    public Guid VoteId { get; set; }
    public Guid PredictionId { get; set; }
    public Guid UserId { get; set; }
    public bool PredictedOutcome { get; set; }
    public DateTime VotedAt { get; set; }
}
