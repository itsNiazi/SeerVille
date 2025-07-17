using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

/// <summary>
/// Represents the object structure and format required from the client for creating predictions.
/// </summary>
public class CreatePredictionDto
{
    [Required]
    public required Guid TopicId { get; set; }

    [StringLength(254, MinimumLength = 2)]
    // [RegularExpression("^[a-zA-Z0-9_.-]+$", ErrorMessage = "Prediction name cannot contain spaces or special characters.")]
    public required string PredictionName { get; set; }

    [Required]
    public required DateTime ResolutionDate { get; set; }
}