using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Prediction;

/// <summary>
/// Represents the object structure and format required from the client for updating prediction.
/// Excl. resolving predictions.
/// </summary>
public class UpdatePredictionDto //Not same as PatchPredictionDto (IsResolved, IsCorrect, ResolvedBy, ResolvedAt)
{
    [Required]
    [StringLength(254, MinimumLength = 2)]
    // [RegularExpression("^[a-zA-Z0-9_.-]+$", ErrorMessage = "Prediction name cannot contain spaces or special characters.")]
    public required string PredictionName { get; set; }

    [Required]
    public required DateTime ResolutionDate { get; set; }
}