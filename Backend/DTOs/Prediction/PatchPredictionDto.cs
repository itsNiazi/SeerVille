using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Prediction;

public class PatchPredictionDto
{
    [Required]
    public required bool IsResolved { get; set; }

    [Required]
    public required bool IsCorrect { get; set; }
}
