namespace Backend.DTOs;

public class CreatePredictionVoteDto
{
    public Guid PredictionId { get; set; }
    public Guid UserId { get; set; }
    public bool PredictedOutcome { get; set; }
}
