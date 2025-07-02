using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public class PredictionVoteDto
{
    [Required]
    public Guid VoteId { get; set; }

    [Required]
    public required Guid PredictionId { get; set; }

    [Required]
    public required Guid UserId { get; set; }

    [Required]
    public required bool PredictedOutcome { get; set; }

    [Required]
    public required DateTime VotedAt { get; set; }
}
