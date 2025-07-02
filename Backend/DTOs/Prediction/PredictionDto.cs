using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public class PredictionDto
{
    [Required]
    public Guid PredictionId { get; set; }

    [Required]
    public required Guid CreatorId { get; set; }

    [Required]
    public required Guid TopicId { get; set; }

    [Required]
    public required string PredictionName { get; set; }

    [Required]
    public required DateTime PredictionDate { get; set; }

    [Required]
    public required DateTime ResolutionDate { get; set; }

    [Required]
    public required bool IsResolved { get; set; }

    public bool? IsCorrect { get; set; }
    public DateTime? ResolvedAt { get; set; }
}
