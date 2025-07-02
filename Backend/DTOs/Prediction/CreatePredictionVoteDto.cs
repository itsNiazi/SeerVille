using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public class CreatePredictionVoteDto
{
    [Required]
    public Guid PredictionId { get; set; }

    [Required]
    public required Guid UserId { get; set; }

    [Required]
    public required bool PredictedOutcome { get; set; }
}
