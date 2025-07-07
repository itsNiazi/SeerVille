using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

/// <summary>
/// Represents the object structure and format required from the client to create prediction vote.
/// </summary>
public class CreatePredictionVoteDto
{
    [Required]
    public Guid PredictionId { get; set; }

    [Required]
    public required bool PredictedOutcome { get; set; }
}